using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CruiseDAL.DataObjects;
using CruiseDAL.Schema;

namespace CheckCruise
{
    public partial class createCheckFile : Form
    {
        #region
        public string cc_fileName;
        List<OriginalCruiseUnits> oc_Units = new List<OriginalCruiseUnits>();
        List<CheckCruiseUnits> cc_Units = new List<CheckCruiseUnits>();
        dataBaseCommands dbc = new dataBaseCommands();
        private string fileName = "";
        private int logsIncluded = 0;
        private int measureExcluded = 0;
        BindingSource checkCruiseBindingSource = new BindingSource();
        BindingSource originalCruiseBindingSource = new BindingSource();
        #endregion

        public createCheckFile()
        {
            InitializeComponent();
        }


        private void onBrowse(object sender, EventArgs e)
        {
            //  Create instance of open file dialog
            OpenFileDialog browseDialog = new OpenFileDialog();

            //  set filter options and filter index
            browseDialog.Filter = "Cruise files (.cruise)|*.cruise|Cruise Files - V3|*.crz3|All Files (*.*)|*.*";
            browseDialog.FilterIndex = 1;

            browseDialog.Multiselect = false;

            //  capture selected filename
            while (fileName == "" || fileName == null)
            {
                DialogResult dResult = browseDialog.ShowDialog();
                if(dResult == DialogResult.Cancel)
                {
                    DialogResult dnr = MessageBox.Show("No filename selected.\nDo you really want to cancel?","WARNING",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if(dnr == DialogResult.Yes)
                        return;
                }   //  endif user selects cancel
                if(dResult == DialogResult.OK)
                {
                    fileName = browseDialog.FileName;
                    //  confirm it is a cruise file
                    if(!fileName.EndsWith(".cruise") && !fileName.EndsWith(".CRUISE") && !fileName.EndsWith(".crz3") && !fileName.EndsWith(".CRZ3"))
                    {
                        MessageBox.Show("File selected is not a cruise file.","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }   //  endif not a cruise file
                    else if (fileName.EndsWith(".crz3") || fileName.EndsWith(".CRZ3"))
                    {

                        string processFileName = fileName;

                        processFileName = processFileName.Substring(0, processFileName.Length - 5);

                        processFileName = processFileName + ".process";

                        if(File.Exists(processFileName))
                        {
                            fileName = processFileName;
                        }//end if
                        else
                        {
                            MessageBox.Show("The file selected has not been processed; please process file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }//end else


                    }//END ELSE IF
                }   //  endif user selects no                
            };  //  end while

            // display last portion of filename
            int nLength = fileName.Length;
            if (fileName.Length >= 40)
                cruiseFilename.Text = fileName.Substring(nLength - 40, 40);

            //  check for fatal errors before continuing
            string[] errors;
            dbc.DAL = new CruiseDAL.DAL(fileName);
            //bool nthResult = dbc.DAL.HasCruiseErrors(out errors);
            bool nthResult = false;
            if (nthResult)
            {
                MessageBox.Show("This file has errors which affect program processing.\nCannot continue until these are fixed in CruiseManager.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }   //  endif

            //  fill original cruise side with method, stratum and units
            fillFields();
        }   //  end onBrowse

        private void onAddAll(object sender, EventArgs e)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
            DialogResult nResult = MessageBox.Show("ALL UNITS ARE INCLUDED IN THE\nCHECK CRUISE FILE BY USING THIS BUTTON! CONTINUE?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(nResult == DialogResult.Yes)
            {
                foreach (OriginalCruiseUnits ocu in oc_Units)
                {
                    CheckCruiseUnits ccu = new CheckCruiseUnits();
                    ccu.CC_Method = ocu.OC_Method;
                    ccu.CC_Stratum = ocu.OC_Stratum;
                    ccu.CC_Unit = ocu.OC_Unit;
                    cc_Units.Add(ccu);
                }   //  end foreach loop on original cruise units
                checkCruiseBindingSource.ResetBindings(false);
                oc_Units.Clear();
                originalCruiseBindingSource.ResetBindings(false);
            }   //  user wants to continue

            return;
        }   //  end onAddAll


        private void onAddOne(object sender, EventArgs e)
        {           
            //  or onRowEnter captures current row which is added to check cruise grid
            int nextRow = 0;
            if (checkCruise.RowCount > 0)
                nextRow = checkCruise.RowCount;
            
            int nthRow = originalCruise.CurrentCell.RowIndex;
            CheckCruiseUnits ccu = new CheckCruiseUnits();
            ccu.CC_Method = originalCruise.Rows[nthRow].Cells[0].Value.ToString();
            ccu.CC_Stratum = originalCruise.Rows[nthRow].Cells[1].Value.ToString();
            ccu.CC_Unit = originalCruise.Rows[nthRow].Cells[2].Value.ToString();
            cc_Units.Add(ccu);
            checkCruiseBindingSource.ResetBindings(false);

            //  remove row from origianl cruise list
            oc_Units.RemoveAt(nthRow);
            originalCruiseBindingSource.ResetBindings(false);
            return;
        }   //  end onAddOne


        private void onRemoveOne(object sender, EventArgs e)
        {
            int nthRow = checkCruise.CurrentCell.RowIndex;
            //  add back to original cruise
            OriginalCruiseUnits ocu = new OriginalCruiseUnits();
            ocu.OC_Method = checkCruise.Rows[nthRow].Cells[0].Value.ToString();
            ocu.OC_Stratum = checkCruise.Rows[nthRow].Cells[1].Value.ToString();
            ocu.OC_Unit = checkCruise.Rows[nthRow].Cells[2].Value.ToString();
            oc_Units.Add(ocu);
            originalCruiseBindingSource.ResetBindings(false);

            //  remove from cc_Units
            cc_Units.RemoveAt(nthRow);
            checkCruiseBindingSource.ResetBindings(false);
        }   //  end onRemoveOne


        private void onRemoveAll(object sender, EventArgs e)
        {
            DialogResult nResult = MessageBox.Show("ALL UNITS WILL BE REMOVED AND NOT INCLUDED IN THE\nCHECK CRUISE FILE BY USING THIS BUTTON!  CONTINUE?","WARNING",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (nResult == DialogResult.Yes)
            {
                foreach (CheckCruiseUnits ccu in cc_Units)
                {
                    OriginalCruiseUnits ocu = new OriginalCruiseUnits();
                    ocu.OC_Method = ccu.CC_Method;
                    ocu.OC_Stratum = ccu.CC_Stratum;
                    ocu.OC_Unit = ccu.CC_Unit;
                    oc_Units.Add(ocu);
                }   //  end foreach

                cc_Units.Clear();
                checkCruiseBindingSource.ResetBindings(false);
                originalCruiseBindingSource.ResetBindings(false);
            }   //  endif
            return;
        }   //  end onRemoveAll

        private void onIncludeLogs(object sender, EventArgs e)
        {
            if (includeLogs.Checked)
                logsIncluded = 1;
            else logsIncluded = 0;
        }   //  end onIncludeLogs

        private void onExcludeMeasurements(object sender, EventArgs e)
        {
            if (excludeMeasurements.Checked)
                measureExcluded = 1;
            else measureExcluded = 0;
        }   //  end onExcludeMeasurements

        private void onCreateFile(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //  first make sure some units were selected for the check cruise file
            if (cc_Units.Count == 0)
            {
                MessageBox.Show("NO UNITS SELECTED FOR CHECK CRUISE FILE.\nPlease select units or exit.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }   //  endif

            //  make sure check cruiser initials have been entered.
            if (checkCruiserInitials.Text == "" || checkCruiserInitials.Text == " ")
            {
                MessageBox.Show("Please enter initials for the check cruiser.\nMake sure check cruiser initials are recorded\nwhen data is collected in the field.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkCruiserInitials.Focus();
                return;
            }   //  endif
            
            //  make a copy of the cruise file and change name for check cruise file
            cc_fileName = fileName;
            int dotLength = cc_fileName.IndexOf(".cruise", 0);
            if (dotLength == -1) dotLength = cc_fileName.IndexOf(".CRUISE", 0);
            cc_fileName = cc_fileName.Insert(dotLength, "_CC");
            System.IO.File.Copy(fileName, cc_fileName, true);

            //  if the check cruise file exists, ask if user wants to overwrite it
            if(File.Exists(cc_fileName))
            {
                DialogResult nResult = MessageBox.Show("The Check Cruise file already exists.\nDo you want to overwrite the file?","WARNING",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(nResult == DialogResult.No)
                {
                    Close();
                    return;
                }   //  endif
            }   //  endif

            //  create a list of units to delete
            List<CheckCruiseUnits> unitsToDelete = new List<CheckCruiseUnits>();
            foreach (OriginalCruiseUnits ocu in oc_Units)
            {
                if(!cc_Units.Exists(c=>c.CC_Method == ocu.OC_Method && c.CC_Stratum == ocu.OC_Stratum && c.CC_Unit == ocu.OC_Unit))
                {
                    CheckCruiseUnits c = new CheckCruiseUnits();
                    c.CC_Method = ocu.OC_Method;
                    c.CC_Stratum = ocu.OC_Stratum;
                    c.CC_Unit = ocu.OC_Unit;
                    unitsToDelete.Add(c);
                }   //  endif
            }   //  end foreach loop

            //  delete these units from the check cruise tree table
            dbc.DAL = new CruiseDAL.DAL(cc_fileName);
            foreach(CheckCruiseUnits utd in unitsToDelete)
                dbc.deleteUnit(utd.CC_Unit, utd.CC_Stratum);

            // are logs included?
            if (logsIncluded != 1)
                dbc.clearLogTable();

            //  exclude measurements from trees
            if (measureExcluded == 1)
            {
                //  user selected to exclude measurements so tree table needs to be created with just tree record identification information
                dbc.updateTreeMeasurements();
                //  June 2015 -- ask user if they would like a text file of measurements (tree and log) to print and take to the field
                //  Requested by Jeff Penman, Region 6
                DialogResult dResult = MessageBox.Show("Would you like a text file of tree and log measurements\nto be printed and taken to the field?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dResult == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //  call create text file here
                    createMeasurementsTextFile();
                    Cursor.Current = this.Cursor;
                }   //  endif
            }

            //  Save check cruiser initials
            dbc.saveInitials(checkCruiserInitials.Text.ToString());

            //  Need to create two new tables -- results and regional tolerances
            bool tableExist = dbc.doesTableExist("Tolerances");
            if (!tableExist)
            {
                //  tolerances table doesn't exist -- create it in the check cruise file
                dbc.createNewTable("Tolerances");
                MessageBox.Show("This is a new check cruise file.\nIt does not contain regional tolerances.\nBe sure to enter those before doing any analysis.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   //  endif on tolerances table

            tableExist = dbc.doesTableExist("Results");
            if (!tableExist)
            {
                //  results table doesn't exist --  create it
                dbc.createNewTable("Results");
            }   //  endif on results table

            //  clear out tables
            dbc.deleteLogStock();
            dbc.deleteTreeCalculatedValues();
            Cursor.Current = this.Cursor;
            return;
        }   //  end onCreateFile


        private void onExit(object sender, EventArgs e)
        {
            //  make sure if no original filename entered that the user really wants to exit.
            if (cruiseFilename.Text == "" || cruiseFilename.Text == " ")
            {
                DialogResult dResult = MessageBox.Show("Are you sure you want to exit?\nAny changes made will NOT be saved.", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dResult == DialogResult.Yes)
                {
                    Close();
                    return;
                }   //  endif
            }   //  endif
            Close();
            return;
        }   //  end onExit


        private void fillFields()
        {
            //  pull strata/units for original cruise side
            List<StratumDO> strList = dbc.getStrata();

            foreach (StratumDO str in strList)
            {
                //  pull unita
                str.CuttingUnits.Populate();
                foreach (CuttingUnitDO cud in str.CuttingUnits)
                {
                    OriginalCruiseUnits ocu = new OriginalCruiseUnits();
                    ocu.OC_Method = str.Method;
                    ocu.OC_Stratum = str.Code;
                    ocu.OC_Unit = cud.Code;
                    oc_Units.Add(ocu);
                }   //  end foreach on cutting unit
            }   //  end foreach on stratum

            originalCruiseBindingSource.DataSource = oc_Units;
            originalCruise.DataSource = originalCruiseBindingSource;
            checkCruiseBindingSource.DataSource = cc_Units;
            checkCruise.DataSource = checkCruiseBindingSource;
            
        }   //  end fillFields

        
        private void createMeasurementsTextFile()
        {
            //  creates a CSV file the check cruiser can print and take to the file
            string[] textHeader = new string[17] { "ST", "CU", "PL", "SG", "TN", "SP", "PP", "LD", "DBH", "TH", "MHP", "MHS", "HFLL", "SD", "LN", "GR", " LSD" };
            string singleComma = ",";
            StringBuilder sb = new StringBuilder();

            string textFileName = System.IO.Path.GetDirectoryName(fileName);
            textFileName += "\\";
            textFileName += "CruiserMeasurements.csv";

            //  loop by check cruiser units selected and retrieve tree data for that unit
            dataBaseCommands db = new dataBaseCommands();
            db.DAL = new CruiseDAL.DAL(fileName);
            List<TreeDO> tList = db.getTrees();
            List<LogDO> logList = db.getLogs();

            using (StreamWriter strTextOut = new StreamWriter(textFileName))
            {
                //  write headaer line to file
                for (int k = 0; k < 16; k++)
                {
                    sb.Append(textHeader[k]);
                    sb.Append(singleComma);
                }   //  end for k loop
                sb.Append(textHeader[16]);
                strTextOut.WriteLine(sb);
                sb.Remove(0,sb.Length);

                //  loop through units selected list to retrieve proper tree data
                foreach (CheckCruiseUnits ccu in cc_Units)
                {
                    List<TreeDO> justUnitData = tList.FindAll(
                        delegate(TreeDO t)
                        {
                            return t.Stratum.Code == ccu.CC_Stratum && t.CuttingUnit.Code == ccu.CC_Unit;
                        });
                    foreach (TreeDO jud in justUnitData)
                    {
                        //  build print line
                        sb.Append(jud.Stratum.Code);
                        sb.Append(singleComma);
                        sb.Append(jud.CuttingUnit.Code);
                        sb.Append(singleComma);
                        if (jud.Plot == null)
                            sb.Append("    ");
                        else sb.Append(jud.Plot.PlotNumber);
                        sb.Append(singleComma);
                        sb.Append(jud.SampleGroup.Code);
                        sb.Append(singleComma);
                        sb.Append(jud.TreeNumber);
                        sb.Append(singleComma);
                        sb.Append(jud.Species);
                        sb.Append(singleComma);
                        sb.Append(jud.SampleGroup.PrimaryProduct);
                        sb.Append(singleComma);
                        sb.Append(jud.LiveDead);
                        sb.Append(singleComma);
                        sb.Append(jud.DBH.ToString());
                        sb.Append(singleComma);
                        sb.Append(jud.TotalHeight.ToString());
                        sb.Append(singleComma);
                        sb.Append(jud.MerchHeightPrimary.ToString());
                        sb.Append(singleComma);
                        sb.Append(jud.MerchHeightSecondary.ToString());
                        sb.Append(singleComma);
                        sb.Append(jud.HeightToFirstLiveLimb.ToString());
                        sb.Append(singleComma);
                        sb.Append(jud.SeenDefectPrimary.ToString());
                        //  find any logs associated with current tree
                        List<LogDO> justLogs = logList.FindAll(
                            delegate(LogDO ld)
                            {
                                return jud.Tree_CN == ld.Tree_CN;
                            });
                        if(justLogs.Count > 0)
                        {
                            sb.Append(singleComma);
                            foreach(LogDO jl in justLogs)
                            {
                                strTextOut.Write(sb.ToString());
                                strTextOut.Write(jl.LogNumber);
                                strTextOut.Write(singleComma);
                                strTextOut.Write(jl.Grade);
                                strTextOut.Write(singleComma);
                                strTextOut.WriteLine(jl.SeenDefect.ToString());
                            }   //  end foreach loop
                            sb.Remove(0,sb.Length);
                        }
                        else 
                        {
                            //  write just tree record
                            strTextOut.WriteLine(sb.ToString());
                            sb.Remove(0,sb.Length);
                        }   //  endif
                    }   //  end foreach loop of just trees in unit
                }   //  end foreach loop of selected units
                strTextOut.Close();
            }   //  end using
            sb.Remove(0,sb.Length);
            sb.Append("Measurements text file has been created\nand can be found at\n");
            sb.Append(textFileName);
            MessageBox.Show(sb.ToString(),"INFORMATION",MessageBoxButtons.OK,MessageBoxIcon.Information);
            return;
        }   //  end createMeasurementsTxtFile


        public class OriginalCruiseUnits
        {
            public string OC_Method { get; set; }
            public string OC_Stratum { get; set; }
            public string OC_Unit { get; set; }
        }   //  end OriginalCruiseUnits

        public class CheckCruiseUnits
        {
            public string CC_Method { get; set; }
            public string CC_Stratum { get; set; }
            public string CC_Unit { get; set; }
        }
    }
}
