using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL.DataObjects;
using CruiseDAL.Schema;


namespace CheckCruise
{
    public partial class MainMenu : Form
    {
        #region
            dataBaseCommands dbc = new dataBaseCommands();
            public string checkFileName;
        #endregion

        public MainMenu()
        {
            InitializeComponent();
            tolerances_button.Enabled = false;
            analysis_button.Enabled = false;
            reports_button.Enabled = false;
        }

        private void onAbout(object sender, EventArgs e)
        {
            //  Show version number etc here
            MessageBox.Show("CruiseProcessing Program Version 2019.12.04\nForest Management Service Center\nFort Collins, Colorado", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;

        }   //  end onAbout


        private void createFile(object sender, EventArgs e)
        {
            createCheckFile ccf = new createCheckFile();
            ccf.ShowDialog();
            checkFileName = ccf.cc_fileName;
            tolerances_button.Enabled = true;
            analysis_button.Enabled = true;
            reports_button.Enabled = true;
        }   //  end createFile

        private void onTolerances(object sender, EventArgs e)
        {
            RegionalTolerances rt = new RegionalTolerances();
            rt.checkFileName = checkFileName;
            rt.setupDialog();
            rt.ShowDialog();

        }   //  end onTolerances

        private void onAnalysis(object sender, EventArgs e)
        {
            CheckCruiseAnalysis cca = new CheckCruiseAnalysis();
            cca.checkCruiseFile = checkFileName;
            int nResult = cca.setupDialog();
            if(nResult == 1)
                cca.ShowDialog();

        }   //  end onAnalysis

        private void onReports(object sender, EventArgs e)
        {
            CheckCruiseReports ccr = new CheckCruiseReports();
            ccr.checkCruiseFile = checkFileName;
            int nthResult = ccr.setupDialog();
            if(nthResult > 0) ccr.ShowDialog();

        }   //  end onReports

        private void onExit(object sender, EventArgs e)
        {
            //  update MRU in Globals for check cruise program

            Close();
        }  //  end on Exit


        private void onOpenExisting(object sender, EventArgs e)
        {
            //  clear filename in case user wants to change files
            checkFileName = "";
            //  Create an instance of the open file dialog
            OpenFileDialog browseDialog = new OpenFileDialog();

            //  Set filter options and filter index
            browseDialog.Filter = "Check cruise files (_CC.cruise)|*_CC.cruise|All Files (*.*)|*.*";
            browseDialog.FilterIndex = 1;

            browseDialog.Multiselect = false;

            //  capture filename selected
            while(checkFileName == "" || checkFileName == null)
            {
                DialogResult dResult = browseDialog.ShowDialog();

                if(dResult == DialogResult.Cancel)
                {
                    DialogResult dnr = MessageBox.Show("No filename selected.\nDo you really want to cancel?","WARNING",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if(dnr == DialogResult.Yes)
                        return;
                }
                else if(dResult == DialogResult.OK)
                {
                    checkFileName = browseDialog.FileName;
                    //  confirm it is a check cruise filename
                    if(!checkFileName.EndsWith("_CC.cruise") && !checkFileName.EndsWith("_CC.CRUISE"))
                    {
                        MessageBox.Show("File selected is not a check cruise file.","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }   //  endif
                }   //  endif
                //  Is the file already open by another program?
                if (Utilities.IsFileOpen(checkFileName) == 1)
                {
                    MessageBox.Show("File is open by another program or process.\nClose the program and try again.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkFileName = "";
                }   //  endif
            }   //  end while

            tolerances_button.Enabled = true;
            analysis_button.Enabled = true;
            reports_button.Enabled = true;
        }   //  end onOpenExisting

    }
}
