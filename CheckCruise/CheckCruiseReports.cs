using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using CruiseDAL.DataObjects;
using CruiseDAL.Schema;


namespace CheckCruise
{
    public partial class CheckCruiseReports : Form
    {
        #region
        public string checkCruiseFile;
        public string currSaleName;
        public string currRegion;
        public List<ResultsList> checkResults = new List<ResultsList>();
        public List<TolerancesList> cruiseTolerances = new List<TolerancesList>();
        public List<TreeDO> checkTrees = new List<TreeDO>();
        public List<LogStockDO> checkLogs = new List<LogStockDO>();
        public List<TreeDO> cruiserTrees = new List<TreeDO>();
        public List<TreeCalculatedValuesDO> cruiserCalculatedTrees = new List<TreeCalculatedValuesDO>();
        public List<LogStockDO> cruiserLogs = new List<LogStockDO>();
        public string checkCruiserInitials;
        private string originalCruiseFile;
        private string evaluationReportFile;
        private int passFail = 0;
        private float totalAccuracyScore = 0;
        private float totalOverallErr = 0;
        private int overallTotalPossible = 0;
        public dataBaseCommands db = new dataBaseCommands();
        #endregion

        public CheckCruiseReports()
        {
            InitializeComponent();
        }


        public int setupDialog()
        {
            if (checkCruiseFile != "" || checkCruiseFile != null)
            {
                if (checkCruiseFile.Length > 40)
                {
                    string tempFile = checkCruiseFile.Substring(checkCruiseFile.Length - 40, 40);
                    tempFile = tempFile.Insert(0, "...");
                    checkCruiseFilename.Text = tempFile;
                }
                else checkCruiseFilename.Text = checkCruiseFile;
            }   //  endif

            db.checkCruiseFilename = checkCruiseFile;
            db.DAL = new CruiseDAL.DAL(checkCruiseFile);
            //  if the check file has not been processed, no volumes will be in TreeCalculatedValues
            //  let user know the file needs to be processed before continuing
            List<TreeCalculatedValuesDO> checkCalculatedTrees = db.getCalculatedTrees();
            if (checkCalculatedTrees.Count == 0)
            {
                MessageBox.Show("The check cruise file does not have any volumes.\nIt must be processed in CruiseProcessing before reports can be created.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
                return -1;
            }   //  endif

            // pull cruiser initials from results table and populate pulldown list
            ArrayList justInitials = db.getCruiserInitials();
            if (justInitials.Count > 0)
            {
                foreach (object obj in justInitials)
                    cruiserInitials.Items.Add(obj);
            }
            else if(justInitials.Count == 0)
            {
                MessageBox.Show("No cruiser initials found.\nCannot produce reports by cruiser.\nHowever, reports by sale can be created.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reportByCruiser.Enabled = false;
                createSummaryByCruiser.Enabled = false;
                return 2;
            }   //  endif
            return 1; 
        }   //  end setupDialog


        private void onBrowse(object sender, EventArgs e)
        {
            //  does check filename already have a name?  Ask if user wants to continue
            if (checkCruiseFile != "" || checkCruiseFile != null)
            {
                DialogResult nResult = 
                    MessageBox.Show("A check cruise filename has already been selected.\nBrowsing for a filename will overwrite the existing filename.  Continue?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (nResult == DialogResult.No) return;
                else if (nResult == DialogResult.Yes)
                {
                    checkCruiseFilename.Clear();
                    checkCruiseFilename.Focus();
                    return;
                }   //  endif
            }   //  endif


            //  create an instance of the open file dialog
            OpenFileDialog browseDialog = new OpenFileDialog();

            //  set fileter options and filter indead
            browseDialog.Filter = "Check cruise files (_CC.cruise)|*_CC.cruise|All Files (*.*)|*.*";
            browseDialog.FilterIndex = 1;
            browseDialog.Multiselect = false;

            //  capture filename selected
            while (checkCruiseFile == "" || checkCruiseFile == null)
            {
                DialogResult dResult = browseDialog.ShowDialog();
                if(dResult == DialogResult.Cancel)
                {
                    DialogResult dnr = MessageBox.Show("No filename selected.\nDo you really want to cancel?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dnr == DialogResult.Yes)
                        return;
                }
                else if(dResult == DialogResult.OK)
                {
                    checkCruiseFile = browseDialog.FileName;
                    //  confirm it is a check cruise filename
                    if (!checkCruiseFile.EndsWith("_CC.cruise") && !checkCruiseFile.EndsWith("_CC.CRUISE"))
                    {
                        MessageBox.Show("File selected is not a check cruise file.\nCannot create reports.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }   //  endif
                }   //  endif
            }   //  end while

            //  make sure the results table has something in it.  If not, means the analysis was not completed.
            db.checkCruiseFilename = checkCruiseFile;
            db.DAL = new CruiseDAL.DAL(checkCruiseFile);
            List<ResultsList> resultsList = db.getResultsTable("","");
            if (resultsList.Count == 0)
            {
                MessageBox.Show("Analysis results are missing.\nCannot create any reports.\nRun Check Cruise Analysis to continue.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }   //  endif

            //  put truncated filename in text box
            int iLenght = checkCruiseFile.Length;
            if (checkCruiseFile.Length > 40)
            {
                string tempFile = checkCruiseFile.Substring(checkCruiseFile.Length - 40, 40);
                tempFile = tempFile.Insert(0, "...");
                checkCruiseFilename.Text = tempFile;
            }
            else checkCruiseFilename.Text = checkCruiseFile;
            return;
        }   //  end onBrowse


        private void onMultipleBrowse(object sender, EventArgs e)
        {
            return;
        }   //  end onMultipleBrowse


        private void onEvaluationReport(object sender, EventArgs e)
        {
            if (reportBySale.Checked == false && reportByCruiser.Checked == false)
            {
                MessageBox.Show("Please select By Sale or By Cruiser for this report.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }   //  endif
            //  create output filename
            if (reportBySale.Checked == true)
                evaluationReportFile = checkCruiseFile.Replace("_CC.cruise", "_BySale.RTF");
            else if (reportByCruiser.Checked == true)
            {
                StringBuilder addCruiser = new StringBuilder();
                addCruiser.Append("_ByCruiser_");
                addCruiser.Append(cruiserInitials.Text);
                addCruiser.Append(".RTF");
                evaluationReportFile = checkCruiseFile.Replace("_CC.cruise", addCruiser.ToString());
            }
            if(Utilities.IsFileOpen(evaluationReportFile) == 1)
            {
                MessageBox.Show("RTF OUTPUT FILE IS OPEN!\nPLEASE CLOSE THE FILE BEFORE CONTINUING.","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Close();
                return;
            }   //  endif
            

            if (reportBySale.Checked == true)
            {
                //  call evaluation report by sale
                getTables();
                //  make sure the trees in check cruise file have check cruiser initials
                List<TreeDO> justInitials = checkTrees.FindAll(
                    delegate(TreeDO t)
                    {
                        return t.Initials == checkCruiserInitials;
                    });
                if (justInitials.Count == 0)
                {
                    MessageBox.Show("Missing check cruiser initials.\nCannot produce reports.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }   //  endif

                createReport();
            }   //  endif
            if (reportByCruiser.Checked == true)
            {
                //  check for cruiser initials in this window
                //  then check cruiser initials in the original file
                if (cruiserInitials.Text == "" || cruiserInitials == null)
                {
                    MessageBox.Show("Cruiser initials need to be entered\nin order to produce the Evaluation\nReport by cruiser.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reportByCruiser.Focus();
                    return;
                }   //  endif

                //  pull needed tables by cruiser initials
                getTables();

                //  find cruiser initials in original file
                List<TreeDO> justCruiserData = cruiserTrees.FindAll(
                    delegate(TreeDO t)
                    {
                        return t.Initials == cruiserInitials.Text.ToString();
                    });
                if (justCruiserData.Count == 0)
                {
                    MessageBox.Show("The cruiser initials entered have no tree records in the original file.\nThey are case sensitive so check the original file for cruiser initials.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    Close();
                    return;
                }   //  endif


                createReport();
            }   //  endif
            return;
        }   //  end onEvaluationReport


        private void onCreateSummaryReports(object sender, EventArgs e)
        {
            createSummaryReports csr = new createSummaryReports();
            csr.cruiserFile = checkCruiseFile.Replace("_CC.cruise", ".cruise");
            csr.checkCruiseFile = checkCruiseFile;
            csr.db.checkCruiseFilename = checkCruiseFile;
            csr.db.DAL = new CruiseDAL.DAL(checkCruiseFile);
            if (createSummaryBySale.Checked == true)
                csr.reportToOutput = 1;
            else if (createSummaryByCruiser.Checked == true && createByCruiserSingleSale.Checked == true)
            {
                if (cruiserInitials.Text != "" && cruiserInitials.Text != null)
                {
                    checkResults = csr.db.getResultsTable("", cruiserInitials.Text);
                    if (checkResults.Count == 0)
                    {
                        MessageBox.Show("No data found for the cruiser initials entered.\nMake sure initials entered are correct.\nInitials are case sensitive.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cruiserName.Clear();
                        cruiserInitials.Focus();
                        return;
                    }   //  endif 
                }
                else if(cruiserInitials.Text == "" || cruiserInitials.Text == null)
                {
                    MessageBox.Show("No cruiser initials entered.\nPlease enter cruiser initials.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cruiserName.Clear();
                    cruiserInitials.Focus();
                    return;
                }   //  endif                   
                csr.reportToOutput = 2;
            }
            else if (createSummaryByCruiser.Checked == true && createByCruiserMultipleSales.Checked == true)
            {
                MessageBox.Show("UNDER CONSTRUCTION\nCannot create a multiple sales report.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                //csr.reportToOutput = 3;
            }   //  endif
            //  ppull log stock table for any log elements
            db.fileName = csr.cruiserFile;
            db.DAL = new CruiseDAL.DAL(db.fileName);
            List<LogStockDO> checkLogs = db.getLogStock();
            if (checkLogs.Count > 0)
                csr.checkLogs = checkLogs;
            if(cruiserInitials.Text != "" || cruiserInitials.Text != null)
               csr.markInits = cruiserInitials.Text;

            string textFile = csr.generateTextReports();
            StringBuilder textMessage = new StringBuilder();
            textMessage.Append("Text file has been created and can\nbe found at\n");
            textMessage.Append(textFile);
            MessageBox.Show(textMessage.ToString(), "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }   //  end onCreateSummaryReports


        private void getTables()
        {
            //  pull data by sale or by cruiser
            if(reportBySale.Checked == true)
            {
                //  check cruiser data
                db.checkCruiseFilename = checkCruiseFile;
                db.DAL = new CruiseDAL.DAL(checkCruiseFile);
                checkTrees = db.getTrees();
                checkLogs = db.getLogStock();
                cruiseTolerances = db.getTolerances();
                checkResults = db.getResultsTable("", "");
                currRegion = db.getRegion();
                currSaleName = db.getSaleName();
                checkCruiserInitials = db.getCheckCruiserInitials();

                //  cruiser data
                originalCruiseFile = checkCruiseFile.Replace("_CC.cruise",".cruise");
                db.fileName = originalCruiseFile;
                db.DAL = new CruiseDAL.DAL(originalCruiseFile);
                cruiserTrees = db.getTrees();
                cruiserCalculatedTrees = db.getCalculatedTrees();
                cruiserLogs = db.getLogStock();
            }
            else if(reportByCruiser.Checked == true)
            {
                //  pull data by check cruiser initials
                db.checkCruiseFilename = checkCruiseFile;
                db.DAL = new CruiseDAL.DAL(checkCruiseFile);
                cruiseTolerances = db.getTolerances();
                currRegion = db.getRegion();
                currSaleName = db.getSaleName();
                checkCruiserInitials = db.getCheckCruiserInitials();
                //  pull results for cruiser
                checkResults = db.getResultsTable("",cruiserInitials.Text.ToString());
                //  then pull check cruiser data to match results data
                List<TreeDO> justCheckCruiseData = db.getTrees();
                foreach (ResultsList cr in checkResults)
                {
                    int nthRow = justCheckCruiseData.FindIndex(
                        delegate(TreeDO td)
                        {
                            return td.Stratum.Code == cr.R_Stratum && td.CuttingUnit.Code == cr.R_CuttingUnit &&
                                    td.TreeNumber.ToString() == cr.R_TreeNumber;
                        });
                    if (nthRow >= 0)
                        checkTrees.Add(justCheckCruiseData[nthRow]);
                }   //  end foreach loop
                checkLogs = db.getLogStock();

                //  and cruiser data
                originalCruiseFile = checkCruiseFile.Replace("_CC.cruise", ".cruise");
                db.fileName = originalCruiseFile;
                db.DAL = new CruiseDAL.DAL(originalCruiseFile);
                cruiserTrees = db.getTrees(cruiserInitials.Text.ToString());
                cruiserCalculatedTrees = db.getCalculatedTrees(cruiserInitials.Text.ToString());
                cruiserLogs = db.getLogStock();
            }   //  endif
            return;
        }   //  end getTables


        private void onExit(object sender, EventArgs e)
        {
            MessageBox.Show("Reports are complete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
            return;
        }   //  end onExit


        private void createReport()
        {
            //  first, check if the entire cruise has tree-based methods
            //  the reason being, in/out trees are not checked and need to be
            //  suppressed from the evaluation report and the pass/fail
            //  calculation --  July 2015
            int treeBasedFlag = CheckStrata();

            //  create RTF objer
            EvaluationReportRTF evalRTF = new EvaluationReportRTF();
            StringBuilder tempString = new StringBuilder();

            //  need a small array for element line so replacement doesn't mess up origina;
            string hold15 = evalRTF.ElementLine[15];
            string hold16 = evalRTF.ElementLine[16];
            string hold17 = evalRTF.ElementLine[17];
            string hold18 = evalRTF.ElementLine[18];
            string hold19 = evalRTF.ElementLine[19];
            string hold20 = evalRTF.ElementLine[20];
            string hold21 = evalRTF.ElementLine[21];
            int numElements = 0;
            totalOverallErr = 0;
            totalAccuracyScore = 0;
            passFail = 0;

            //  open output file for writing
            using (StreamWriter strWriteOut = new StreamWriter(evaluationReportFile))
            {
                //  update main header and print
                evalRTF.MainHeader[13] = evalRTF.MainHeader[13].Replace("XXXXX", currSaleName);
                evalRTF.MainHeader[16] = evalRTF.MainHeader[16].Replace("XX", currRegion);
                printLines(strWriteOut, evalRTF.MainHeader, 18);
                if (reportByCruiser.Checked == true)
                {
                    //  update and print cruiser ID and name
                    evalRTF.CruiserID[7] = evalRTF.CruiserID[7].Replace("XXXX", cruiserInitials.Text);
                    printLines(strWriteOut, evalRTF.CruiserID, 9);
                    evalRTF.CruiserName[7] = evalRTF.CruiserName[7].Replace("XXXX", cruiserName.Text);
                    printLines(strWriteOut, evalRTF.CruiserName, 9);
                }   //  endif

                //  print remaining headers
                printLines(strWriteOut, evalRTF.FirstHeader, 25);
                printLines(strWriteOut, evalRTF.SecondHeader, 23);
                printLines(strWriteOut, evalRTF.ThirdHeader, 23);

                Cursor.Current = Cursors.WaitCursor;
                // output tolerances list
                foreach (TolerancesList tl in cruiseTolerances)
                {
                    //  if the cruise is all tree-based starta and element is in/out trees
                    //  skip printing of this element
                    if (treeBasedFlag > 0 && tl.T_Element == "In/Out Trees")
                        treeBasedFlag = 0;
                    else
                    {
                        //  insert element
                        if (tl.T_Element != "" || tl.T_Element != null)
                            evalRTF.ElementLine[15] = evalRTF.ElementLine[15].Replace("XXXXX", tl.T_Element);

                        //  insert tolerance, units and additional parameter
                        if (tl.T_Tolerance == "None")
                        {
                            evalRTF.ElementLine[16] = evalRTF.ElementLine[16].Replace("TTTT", tl.T_Tolerance);
                        }
                        else if (tl.T_Tolerance != "None")
                        {
                            tempString.Remove(0, tempString.Length);
                            tempString.Append("+/- ");
                            tempString.Append(tl.T_Tolerance);
                            tempString.Append(" ");
                            tempString.Append(tl.T_Units);
                            if (tl.T_AddParam != "None")
                            {
                                tempString.Append(" or ");
                                tempString.Append(tl.T_AddParam);
                            }   //  endif
                            evalRTF.ElementLine[16] = evalRTF.ElementLine[16].Replace("TTTT", tempString.ToString());
                        }   //  endif

                        //  put weight in correct osition
                        tempString.Remove(0, tempString.Length);
                        tempString.Append(Utilities.FormatField(tl.T_Weight, "{0,3:F0}").ToString());
                        evalRTF.ElementLine[19] = evalRTF.ElementLine[19].Replace("AAAA", tempString.ToString());

                        //  Calculate values for each element except in/out trees
                        float totalPossible = 0;
                        int numberIncorrect = 0;
                        float totalError = 0;
                        float accuracyScore = 0;
                        CalculateValues(tl, ref totalPossible, ref numberIncorrect, ref totalError, ref accuracyScore,
                                                cruiseTolerances[0].T_ElementAccuracy);


                        //  load element line with values
                        evalRTF.ElementLine[17] = evalRTF.ElementLine[17].Replace("YYYY", Utilities.FormatField(totalPossible, "{0,4:F0}").ToString());
                        evalRTF.ElementLine[18] = evalRTF.ElementLine[18].Replace("ZZZZ", Utilities.FormatField(numberIncorrect, "{0,4:F0}").ToString());
                        evalRTF.ElementLine[20] = evalRTF.ElementLine[20].Replace("BBBB", Utilities.FormatField(totalError, "{0,4:F0}").ToString());
                        evalRTF.ElementLine[21] = evalRTF.ElementLine[21].Replace("DDDD", Utilities.FormatField(accuracyScore, "{0,4:F0}").ToString());
                        //  When an area-based stratum is not checked, the in/out element shows zero for everything
                        //  The accuracy of zero causes the check to fail so need to suppress  printing that element 
                        //  If there are multiple strata that are area-based, even one checked would prevent the
                        //  element from being suppressed since total possible will be greater than zero
                        //  February 2016
                        if(totalPossible > 0)
                            printLines(strWriteOut, evalRTF.ElementLine, 23);

                        //  add values to overall totals
                        overallTotalPossible += (int)totalPossible;
                        totalOverallErr += totalError;
                        totalAccuracyScore += accuracyScore;
                        numElements++;

                        //  replace lines in the element line to get originals back for next element store
                        evalRTF.ElementLine[15] = hold15;
                        evalRTF.ElementLine[16] = hold16;
                        evalRTF.ElementLine[17] = hold17;
                        evalRTF.ElementLine[18] = hold18;
                        evalRTF.ElementLine[19] = hold19;
                        evalRTF.ElementLine[20] = hold20;
                        evalRTF.ElementLine[21] = hold21;
                    }   //  endif all tree-based strata
                }   //  end foreach

                //  print total line
                evalRTF.TotalLine[17] = evalRTF.TotalLine[17].Replace("ZZZZ", Utilities.FormatField(overallTotalPossible, "{0,4:F0}").ToString());
                evalRTF.TotalLine[20] = evalRTF.TotalLine[20].Replace("BBBB", Utilities.FormatField(totalOverallErr, "{0,3:F0}").ToString());
                if (totalAccuracyScore > 0)
                {
                    float calcValue = (float)totalAccuracyScore / numElements;
                    evalRTF.TotalLine[21] = evalRTF.TotalLine[21].Replace("DDDD",
                        Utilities.FormatField(calcValue, "{0,3:F0}").ToString());
                }   //  endif
                printLines(strWriteOut, evalRTF.TotalLine, 23);

                //  printing volume goes here
                //  June 2015 -- volume section has been revised
                //  check for include volume is moved to volume output
                EvaluationDisplayVolume edv = new EvaluationDisplayVolume();
                edv.db.checkCruiseFilename = db.checkCruiseFilename;
                edv.db.DAL = db.DAL;
                edv.checkResults = checkResults;
                edv.cruiserCalculatedTrees = cruiserCalculatedTrees;
                int nResult = 0;
                if (reportBySale.Checked == true)
                {
                    nResult = edv.DisplayVolume(strWriteOut, cruiseTolerances[0], "");
                }
                else if (reportByCruiser.Checked == true)
                {
                    nResult = edv.DisplayVolume(strWriteOut, cruiseTolerances[0], cruiserInitials.Text.ToString());
                    if (nResult == 0)
                    {
                        cruiserName.Clear();
                        cruiserInitials.Focus();
                    }   //  endif
                }   //  endif

                //  printing rest of page goes here
                float calcError = (float)((1.0 - totalOverallErr / overallTotalPossible)) * 100;
                if (passFail == 1 || calcError < cruiseTolerances[0].T_OverallAccuracy || nResult == 1)
                    evalRTF.EvaluationLines[1] = evalRTF.EvaluationLines[1].Replace("XXXX", "FAIL");
                else evalRTF.EvaluationLines[1] = evalRTF.EvaluationLines[1].Replace("XXXX", "PASS");
                printLines(strWriteOut,evalRTF.EvaluationLines, 2);

                printLines(strWriteOut, evalRTF.FirstText, 6);
                printLines(strWriteOut, evalRTF.SecondText, 5);

                evalRTF.ThirdText[2] = evalRTF.ThirdText[2].Replace("XXX", Utilities.FormatField(cruiseTolerances[0].T_ElementAccuracy, "{0,3:F0}").ToString());
                evalRTF.ThirdText[2] = evalRTF.ThirdText[2].Replace("ZZZ", Utilities.FormatField(cruiseTolerances[0].T_OverallAccuracy, "{0,3:F0}").ToString());
                printLines(strWriteOut, evalRTF.ThirdText, 5);

                printLines(strWriteOut, evalRTF.CommentSection, 27);
                printLines(strWriteOut, evalRTF.SignatureBlock, 22);

                strWriteOut.WriteLine("}");

                strWriteOut.Close();
            }   //  end using

            Cursor.Current = this.Cursor;
            //  let user know the report is complete
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append("Evaluation report  is complete and\nmay be found at ");
            sbMessage.Append(evaluationReportFile);
            MessageBox.Show(sbMessage.ToString(), "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }   //  end createReportBySale


        private void CalculateValues(TolerancesList currTL, ref float totalPossible, ref int numberIncorrect, 
                                    ref float totalError, ref float accuracyScore, float elementAccuracy)
        {
            //  this has to change slightly for log elements
            //  total possible is the total number of logs in the selected units.  even though only three trees may have been checked
            if (currTL.T_Element == "Log Grade" || currTL.T_Element == "Log Defect %")
            {
                List<ResultsList> justLogs = checkResults.FindAll(
                    delegate(ResultsList r)
                    {
                        return r.R_CountMeasure == "" && r.R_LogGrade_R >= 0;
                    });
                totalPossible = justLogs.Count();
            }
            else
            {
                //  find total possible
                totalPossible = TrueNumberOfTrees(currTL.T_Element, checkCruiserInitials);
            }   //  endif

            //  number incorrect
            List<ResultsList> justIncorrect = findNumberIncorrect(currTL.T_Element);
            numberIncorrect = justIncorrect.Count();

            //  calculate total error
            totalError = justIncorrect.Count() * currTL.T_Weight;
            if ((currRegion == "10" && currTL.T_Element == "Log Defect %") ||
                (currRegion == "10" && currTL.T_Element == "Log Grade"))
                totalError = (float) (justIncorrect.Count * 0.85);
            
            //  Calculate accuracy score
            if(totalPossible > 0.0)
                accuracyScore = (float)(1.00 - (totalError / totalPossible)) * 100;

            //  if score is less than individual accuracy score, cruiser fails on in/out trees
            if(accuracyScore < elementAccuracy && totalPossible > 0) passFail = 1;
            return;
        }   //  end CalculateValues



        private float TrueNumberOfTrees(string currElement, string CCinitials)
        {
            float returnTrees = 0;
            //  pull stratum list to get two stage methods separated from other methods
            List<StratumDO> sList = db.getStrata();
            if (currElement == "In/Out Trees")
            {
                foreach (StratumDO sl in sList)
                {
                    switch (sl.Method)
                    {
                        case "FIX":
                        case "PNT":
                        case "FIXCNT":
                            //  pull strata from checkResults
                            List<ResultsList> justStrata = checkResults.FindAll(
                                delegate(ResultsList rl)
                                {
                                    return rl.R_Stratum == sl.Code && 
                                        (rl.R_LogNumber == "" || rl.R_LogNumber == "0");
                                });
                            returnTrees += justStrata.Count();
                            break;
                        case "P3P":         case "F3P":         case "PCM":
                        case "FCM":         case "3PPNT":
                            //  pull strata from check cruiser trees
                            List<ResultsList> justCounts = checkResults.FindAll(
                                delegate(ResultsList r)
                                {
                                    return r.R_Stratum == sl.Code &&
                                        r.R_CountMeasure == "C";
                                });
                            returnTrees += justCounts.Count();
                            List<ResultsList> justMeasured = checkResults.FindAll(
                                delegate(ResultsList r)
                                {
                                    return r.R_Stratum == sl.Code &&
                                        r.R_CountMeasure == "M";
                                });
                            returnTrees += justMeasured.Count();
                            break;
                    }   //  end switch on method
                }   // end foreach loop
                //  pull any out trees from check cruiser results and delete from returnTrees
                List<ResultsList> justInOut = checkResults.FindAll(
                    delegate(ResultsList r)
                    {
                        return r.R_OutResult == 1;
                    });
                returnTrees -= justInOut.Count;
            }
            else
            {
                List<ResultsList> justStratum = new List<ResultsList>();
                foreach (StratumDO sl in sList)
                {
                    switch (sl.Method)
                    {
                        case "PCM":             case "FCM":             case "3PPNT":
                        case "F3P":             case "P3P":             case "S3P":
                            //  pull check cruiser measured trees for two-stage methods
                            List<ResultsList> justMeasured = checkResults.FindAll(
                                delegate(ResultsList r)
                                {
                                    return r.R_Stratum == sl.Code &&
                                        r.R_CountMeasure == "M";
                                });
                            returnTrees += justMeasured.Count();

                            //  now add counts for species element
                            if (currElement == "Species")
                            {
                                List<ResultsList> justCounts = checkResults.FindAll(
                                    delegate(ResultsList r)
                                    {
                                        return r.R_Stratum == sl.Code &&
                                            r.R_CountMeasure == "C";
                                    });
                                returnTrees += justCounts.Count();
                                //  any in/out errors to be deducted?
                                List<ResultsList> justInOut = checkResults.FindAll(
                                    delegate(ResultsList js)
                                    {
                                        return js.R_Stratum == sl.Code && js.R_OutResult == 1;
                                    });
                                returnTrees -= justInOut.Count();
                            }   //  endif
                            break;
                        default:
                            //  find all check records for current stratum
                            justStratum = checkResults.FindAll(
                                delegate(ResultsList cr)
                                {
                                    return cr.R_Stratum == sl.Code && cr.R_CountMeasure == "M";
                                });
                            returnTrees += justStratum.Count();

                            break;
                    }   //  end switch
                }   //  end foreach loop

            }   //  endif on element

            return returnTrees;
        }   //  end TrueNumberOfTrees


        private List<ResultsList> findNumberIncorrect(string currElement)
        {
            List<ResultsList> justIncorrect = new List<ResultsList>();
            switch (currElement)
            {
                case "Species":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_Species_R == 1;
                        });
                    break;
                case "Product":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_Product_R == 1;
                        });
                    break;
                case "LiveDead":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_LiveDead_R == 1;
                        });
                    break;
                case "DBH":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_DBHOB_R == 1;
                        });
                    break;
                case "Total Height":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_TotalHeight_R == 1;
                        });
                    break;
                case "Total Height <= 100 feet":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_TotHeightUnder_R == 1;
                        });
                    break;
                case "Total Height > 100 feet":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_TotHeightOver_R == 1;
                        });
                    break;
                case "Merch Height Primary":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_MerchHgtPP_R == 1;
                        });
                    break;
                case "Merch Height Secondary":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_MerchHgtSP_R == 1;
                        });
                    break;
                case "Height to First Live Limb":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_HgtFLL_R == 1;
                        });
                    break;
                case "Top DIB Primary":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_TopDIBPP_R == 1;
                        });
                    break;
                case "Seen Defect % Primary":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_SeenDefPP_R == 1;
                        });
                    break;
                case "Seen Defect % Secondary":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_SeenDefSP_R == 1;
                        });
                    break;
                case "Recoverable %":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_RecDef_R == 1;
                        });
                    break;
                case "Form class":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_FormClass_R == 1;
                        });
                    break;
                case "Clear Face":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_Clear_R == 1;
                        });
                    break;
                case "Tree Grade":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_TreeGrade_R == 1;
                        });
                    break;
                case "Log Grade":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_LogGrade_R == 1;
                        });
                    break;
                case "Log Defect %":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_LogSeenDef_R == 1;
                        });
                    break;
                case "In/Out Trees":
                    justIncorrect = checkResults.FindAll(
                        delegate(ResultsList cr)
                        {
                            return cr.R_InResult == 1 || cr.R_OutResult == 1;
                        });
                    break;
            }   //  end switch
            return justIncorrect;
        }   // findNumberIncorrect


        public void printLines(StreamWriter strWriteOut, string[] arrayToPrint, int nRows)
        {
            for (int k = 0; k < nRows; k++)
            {
                strWriteOut.WriteLine(arrayToPrint[k]);
            }   //  end for k loop
            return;
        }

        private void onReportBySale(object sender, EventArgs e)
        {
            reportByCruiser.Enabled = false;
        }

        private void onReportByCruiser(object sender, EventArgs e)
        {
            reportBySale.Enabled = false;
        }

        private void onSummaryBySale(object sender, EventArgs e)
        {
            createSummaryByCruiser.Enabled = false;
            createByCruiserSingleSale.Enabled = false;
            createByCruiserMultipleSales.Enabled = false;
            multipleFileBrowse.Enabled = false;
            multipleSalesFileName.Enabled = false;

        }

        private void onSummaryByCruiser(object sender, EventArgs e)
        {
            createSummaryBySale.Enabled = false;

        }

        private void onCreateSingleSale(object sender, EventArgs e)
        {
            createByCruiserMultipleSales.Enabled = false;
            multipleFileBrowse.Enabled = false;
            multipleSalesFileName.Enabled = false;
        }

        private void oCreateMultipleSale(object sender, EventArgs e)
        {
            createByCruiserSingleSale.Enabled = false;
        }   //  end printLines

        private int CheckStrata()
        {
            //  check each stratum in checkResults for all tree-based methods
            List<StratumDO> justStrata = db.getStrata();
            int numTreeBased = 0;
            foreach (StratumDO s in justStrata)
            {
                if (s.Method == "100" || s.Method == "STR" ||
                    s.Method == "S3P" || s.Method == "3P")
                    numTreeBased++;
            }   //  end foreach
            if (numTreeBased == justStrata.Count)
            {
                //  can't remove in/out element since that line stores other
                //  flags for volume
                //  so set a flag to indicate all strata were tree-based
                return numTreeBased;
            }   //  endif
            return 0;
        }   //  end CheckStrata
    }
}
