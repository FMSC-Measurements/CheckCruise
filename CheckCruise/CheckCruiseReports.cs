using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using CruiseDAL.DataObjects;
using CheckCruise.Reports;


namespace CheckCruise
{
    public partial class CheckCruiseReports : Form
    {
        private const string TIMESTAMP_FORMAT = "yyyyMMddhhmm";

        public string WorkingDirectory { get; }
        public dataBaseCommands CheckCruiseDataService { get; }
        public dataBaseCommands CruiseDataService { get; }

        protected CheckCruiseReports()
        {
            InitializeComponent();
        }

        public CheckCruiseReports(dataBaseCommands checkCruiseDataservice, dataBaseCommands cruiseDataservice, string workingDirectory)
            : this()
        {
            CheckCruiseDataService = checkCruiseDataservice ?? throw new ArgumentNullException(nameof(checkCruiseDataservice));
            CruiseDataService = cruiseDataservice ?? throw new ArgumentNullException(nameof(cruiseDataservice));
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
        }

        public int setupDialog()
        {
            List<ResultsList> resultsList = CheckCruiseDataService.getResultsTable("", "");
            if (resultsList.Count == 0)
            {
                MessageBox.Show("Analysis results are missing.\nCannot create any reports.\nRun Check Cruise Analysis to continue.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }   //  endif


            //  if the check file has not been processed, no volumes will be in TreeCalculatedValues
            //  let user know the file needs to be processed before continuing
            List<TreeCalculatedValuesDO> checkCalculatedTrees = CheckCruiseDataService.getCalculatedTrees();
            if (checkCalculatedTrees.Count == 0)
            {
                MessageBox.Show("The check cruise file does not have any volumes.\nIt must be processed in CruiseProcessing before reports can be created.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
                return -1;
            }   //  endif

            // pull cruiser initials from results table and populate pulldown list
            ArrayList justInitials = CheckCruiseDataService.getCruiserInitials();
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


        private void onEvaluationReport(object sender, EventArgs e)
        {
            if (reportBySale.Checked ^ reportByCruiser.Checked) // ensure one and only one of the two is checked
            {
                MessageBox.Show("Please select By Sale or By Cruiser for this report.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reportLevel = ReportLevel.BySale;
            string cruisersInitialsValue = null;
            if (reportByCruiser.Checked)
            {
                if(!String.IsNullOrEmpty(cruiserInitials.Text))
                {
                    cruisersInitialsValue = cruiserInitials.Text;
                    reportLevel = ReportLevel.ByCruiser;
                }
            }


            var sale = CheckCruiseDataService.getSale();
            var saleNumber = sale.SaleNumber;
            var saleName = sale.Name;
            var timestamp = DateTime.Now.ToString(TIMESTAMP_FORMAT);

            var reportLevelStr = reportLevel switch
            {
                ReportLevel.BySale => "BySale",
                ReportLevel.ByCruiser => "ByCruiser",
                _ => throw new InvalidEnumArgumentException(nameof(reportLevel)),
            };

            //  create output filename
            var evaluationReportFileName = $"{sale.SaleNumber}_{sale.Name}_{timestamp}_Eval_{reportLevelStr}.rtf";
            var evaluationReportFilePath = Path.Combine(WorkingDirectory, evaluationReportFileName);

            if(Utilities.IsFileOpen(evaluationReportFilePath) == 1)
            {
                MessageBox.Show("RTF OUTPUT FILE IS OPEN!\nPLEASE CLOSE THE FILE BEFORE CONTINUING.","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }   //  endif

            var checkCruiseDataService = CheckCruiseDataService;

            if(reportLevel == ReportLevel.BySale)
            {
                var checkTrees = checkCruiseDataService.getTrees();
                var checkCruiserInitials = checkCruiseDataService.getCheckCruiserInitials();
                if(!checkTrees.Any(t => t.Initials == checkCruiserInitials))
                {
                    MessageBox.Show("Missing check cruiser initials.\nCannot produce reports.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if(reportLevel == ReportLevel.ByCruiser)
            {
                //var cruiserTrees = cruiseDataService.getTrees();
                //if(!cruiserTrees.Any(t => t.Initials == cruisersInitialsValue))
                //{
                //    MessageBox.Show("The cruiser initials entered have no tree records in the original file.\nThey are case sensitive so check the original file for cruiser initials.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Question);
                //    return;
                //}


                var cruserResults = checkCruiseDataService.getResultsTable("", cruisersInitialsValue);
                if(!cruserResults.Any())
                {
                    MessageBox.Show("No Trees Found With Cruisers Initials.\r\nInitials are case sensitive.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var reportBuilder = new EvaluationReportBuilder(checkCruiseDataService, reportLevel, cruiserName.Text, cruisersInitialsValue);

            
            using StreamWriter strWriteOut = new StreamWriter(evaluationReportFilePath);

            reportBuilder.CreateReport(strWriteOut);

            //  let user know the report is complete
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append("Evaluation report  is complete and\nmay be found at ");
            sbMessage.Append(evaluationReportFilePath);
            MessageBox.Show(sbMessage.ToString(), "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }   //  end onEvaluationReport


        private void onCreateSummaryReports(object sender, EventArgs e)
        {
            var cruiseDataService = CruiseDataService;
            var checkCruiseDataService = CheckCruiseDataService;

            var markersInitials = (!String.IsNullOrEmpty(cruiserInitials.Text)) ? cruiserInitials.Text : "";

            int reportToOutput = 0;
            if (createSummaryBySale.Checked == true)
            { reportToOutput = 1; }
            else if (createSummaryByCruiser.Checked == true)
            {
                if(markersInitials != "")
                {
                    var checkResults = checkCruiseDataService.getResultsTable("", markersInitials);
                    if(checkResults.Any())
                    {
                        reportToOutput = 2;
                    }
                    else
                    {
                        MessageBox.Show("No data found for the cruiser initials entered.\nMake sure initials entered are correct.\nInitials are case sensitive.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cruiserName.Clear();
                        cruiserInitials.Focus();
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("No cruiser initials entered.\nPlease enter cruiser initials.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cruiserName.Clear();
                    cruiserInitials.Focus();
                    return;
                }
            }
            else
            {
                throw new InvalidOperationException("unreachable code");
            }

            var reportLevel = reportToOutput switch
            {
                1 => ReportLevel.BySale,
                2 => ReportLevel.ByCruiser,
                _ => throw new InvalidEnumArgumentException(nameof(reportToOutput)),
            };



            var timestamp = DateTime.Now.ToString(TIMESTAMP_FORMAT);
            string appVersion = Program.AppVersion;
            var sale = CheckCruiseDataService.getSale();
            var reportFileName = reportLevel switch
            {
                ReportLevel.BySale => $"{sale.SaleNumber}_{sale.Name}_{timestamp}_SaleSummary.txt",
                ReportLevel.ByCruiser => $"{sale.SaleNumber}_{sale.Name}_{timestamp}_CruiserSummary_{markersInitials}.txt",
            };
            var reportFilePath = Path.Combine(WorkingDirectory, reportFileName);


            var summaryReportBuilder = new SummaryReportBuilder(checkCruiseDataService, cruiseDataService, markersInitials, reportToOutput, appVersion);
            summaryReportBuilder.GenerateSummaryReport(reportFilePath);

            StringBuilder textMessage = new StringBuilder();
            textMessage.Append("Text file has been created and can\nbe found at\n");
            textMessage.Append(reportFilePath);
            MessageBox.Show(textMessage.ToString(), "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }   //  end onCreateSummaryReports

        private void onExit(object sender, EventArgs e)
        {
            MessageBox.Show("Reports are complete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
            return;
        }   //  end onExit

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


        }

        private void onSummaryByCruiser(object sender, EventArgs e)
        {
            createSummaryBySale.Enabled = false;

        }

        private void onCreateSingleSale(object sender, EventArgs e)
        {
            //createByCruiserMultipleSales.Enabled = false;
            //multipleFileBrowse.Enabled = false;
            //multipleSalesFileName.Enabled = false;
        }

        private void oCreateMultipleSale(object sender, EventArgs e)
        {
            //createByCruiserSingleSale.Enabled = false;
        }   //  end printLines

    }
}
