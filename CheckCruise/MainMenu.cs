using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;
using CruiseDAL.Schema;


namespace CheckCruise
{
    public partial class MainMenu : Form
    {
        private dataBaseCommands _checkCruiseDataService;
        private dataBaseCommands _cruiseDataservice;

        public dataBaseCommands CheckCruiseDataservice
        {
            get => _checkCruiseDataService;
            set
            {
                _checkCruiseDataService = value;
            }
        }
        public dataBaseCommands CruiseDataservice
        {
            get => _cruiseDataservice;
            set
            {
                _cruiseDataservice = value;
                if(value != null)
                {
                    var cruisePath = value.DAL.Path;
                    if(cruisePath.Length > 40)
                    {
                        cruisePath = "..." + cruisePath.Substring(cruisePath.Length - 41);
                    }
                    _cruisePathTextBox.Text = cruisePath;
                }
                else
                {
                    _cruisePathTextBox.Text = "Select Cruise File";
                }
            }
        }
        public string WorkingDirectory { get; set; }


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
            MessageBox.Show("CruiseProcessing Program Version " + Program.AppVersion + "\nForest Management Service Center\nFort Collins, Colorado", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;

        }   //  end onAbout


        private void createFile(object sender, EventArgs e)
        {
            createCheckFile ccf = new createCheckFile();
            ccf.ShowDialog();

            if(ccf.DialogResult == DialogResult.OK)
            {
                OpenCheckCruise(ccf.CheckCruiseFilePath);
            }
            
        }   //  end createFile

        private void onTolerances(object sender, EventArgs e)
        {
            using RegionalTolerances rt = new RegionalTolerances(CheckCruiseDataservice);
            rt.setupDialog();
            rt.ShowDialog();

        }   //  end onTolerances

        private void onAnalysis(object sender, EventArgs e)
        {
            if(CheckCruiseDataservice == null || CruiseDataservice == null) { return; }

            var analizer = new CheckCruiseAnalyzer(CheckCruiseDataservice, CruiseDataservice);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                analizer.Analyze();
                Cursor.Current = this.Cursor;
                MessageBox.Show("Analysis complete\nClick EXIT to return to Main Menu", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                Cursor.Current = this.Cursor;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                Cursor.Current = this.Cursor;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                Cursor.Current = this.Cursor;
                throw;
            }

        }   //  end onAnalysis

        private void onReports(object sender, EventArgs e)
        {

            if (CheckCruiseDataservice == null || CruiseDataservice == null) { return; }

            using var ccr = new CheckCruiseReports(CheckCruiseDataservice, CruiseDataservice, WorkingDirectory);
            int nthResult = ccr.setupDialog();
            if (nthResult > 0) ccr.ShowDialog();

        }   //  end onReports

        private void onExit(object sender, EventArgs e)
        {
            //  update MRU in Globals for check cruise program

            Close();
        }  //  end on Exit


        private void onOpenExisting(object sender, EventArgs e)
        {
            //  Create an instance of the open file dialog
            using OpenFileDialog browseDialog = new OpenFileDialog()
            {
                Filter = "Check cruise files (_CC.cruise)|*_CC.cruise|All Files (*.*)|*.*",
                Multiselect = false
            };


            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                OpenCheckCruise(browseDialog.FileName);
            }
        }   //  end onOpenExisting

        protected void OpenCheckCruise(string checkFilePath)
        {
            if (String.IsNullOrEmpty(checkFilePath)) throw new ArgumentException("checkFilePath was null or empty");

            try
            {
                
                var checkDb = new DAL(checkFilePath);
                var checkDatasservice = new dataBaseCommands(checkDb);

                var sale = checkDatasservice.getSale();
                if(false)
                //if (sale.Purpose != "Check Cruise")
                {
                    MessageBox.Show("File selected is not a check cruise file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    var checkDir = Path.GetDirectoryName(checkFilePath);
                    CheckCruiseDataservice = checkDatasservice;
                    WorkingDirectory = checkDir;

                    var checkFileName = Path.GetFileName(checkFilePath);
                    var cruiseFileName = checkFileName.Replace("_CC.cruise", ".cruise");
                    var cruiseFilePath = Path.Combine(checkDir, cruiseFileName);

                    OpenCruiseFile(cruiseFilePath);
                }
            }
            catch (Exception ex)
            {
                CheckCruiseDataservice = null;

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            tolerances_button.Enabled = (CheckCruiseDataservice != null);
            analysis_button.Enabled = (CheckCruiseDataservice != null && CruiseDataservice != null);
            reports_button.Enabled = (CheckCruiseDataservice != null && CruiseDataservice != null);
        }

        public void OpenCruiseFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath)) return;
            if (!File.Exists(filePath)) return;

            try
            {
                var cruiseDb = new DAL(filePath);
                var cruiseDataservice = new dataBaseCommands(cruiseDb);
                CruiseDataservice = cruiseDataservice;
            }
            catch (Exception ex)
            {
                CruiseDataservice = null;

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            analysis_button.Enabled = (CheckCruiseDataservice != null && CruiseDataservice != null);
            reports_button.Enabled = (CheckCruiseDataservice != null && CruiseDataservice != null);
        }

        private void _browseCruiseButton_Click(object sender, EventArgs e)
        {



            using OpenFileDialog browseDialog = new OpenFileDialog()
            {
                Filter = "cruise files (.cruise)|*.cruise|All Files (*.*)|*.*",
                Multiselect = false
            };


            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                OpenCruiseFile(browseDialog.FileName);
            }
        }


    }
}
