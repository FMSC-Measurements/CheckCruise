using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CruiseDAL.DataObjects;
using CruiseDAL.Schema;

namespace CheckCruise
{
    class createSummaryReports : CheckCruiseReports
    {
        #region
            public string cruiserFile;
            public int reportToOutput;
            private TextReportHeaders TRH = new TextReportHeaders();
            private string columnSeparator = "     |";
            private int numOlines = 0;
            private string outputFileName;
            private ArrayList prtArray = new ArrayList();
            private int[] fieldLengths;
            private string cruiserString = "";
            private string checkCruiserString = "";
            private string outOfTolerance = "0";
            private string diff1 = "";
            private string diff2 = "";
            private string diff3 = "";
            public string markInits = "";
            private List<TreeCalculatedValuesDO> calculatedTrees = new List<TreeCalculatedValuesDO>();
        #endregion

        public string generateTextReports()
        {
            db.checkCruiseFilename = checkCruiseFile;
            db.DAL = new CruiseDAL.DAL(checkCruiseFile);
            checkResults = db.getResultsTable("",markInits);
            cruiseTolerances = db.getTolerances();
            //  need stratum table to determine if cruise is all tree-based
            //  to suppress printing in/out trees element -- August 2015
            List<StratumDO> sList = db.getStrata();
            int treeBasedFlag = CheckStrata();
            //  need original trees
            db.DAL = new CruiseDAL.DAL(cruiserFile);
            cruiserTrees = db.getTrees();
            calculatedTrees = db.getCalculatedTrees();
            currSaleName = db.getSaleName();
            currRegion = db.getRegion();
            //  setup items needed for main header
            //TRH.TextHeader[1] = TRH.TextHeader[1].Replace("XXXX", "DRAFT.2018");
            TRH.TextHeader[1] = TRH.TextHeader[1].Replace("XXXX", "12.04.2019");
            TRH.TextHeader[2] = TRH.TextHeader[2].Replace("XXXX", DateTime.Now.ToString());
            switch (reportToOutput)
            {
                case 1:
                    //  create output filename
                    outputFileName = checkCruiseFile.Replace("_CC.cruise", "_SaleSummary.txt");

                    TRH.saleSummaryHeader[1] = TRH.saleSummaryHeader[1].Replace("XXXX", currSaleName);
                    TRH.saleSummaryHeader[2] = TRH.saleSummaryHeader[2].Replace("XXXX", cruiserFile);
                    TRH.saleSummaryHeader[3] = TRH.saleSummaryHeader[3].Replace("XXXX", checkCruiseFile);
                    TRH.saleSummaryHeader[4] = TRH.saleSummaryHeader[4].Replace("XX", cruiseTolerances.Count.ToString());
                    TRH.saleSummaryHeader[5] = TRH.saleSummaryHeader[5].Replace("XXXX", cruiseTolerances[0].T_DateStamp);

                    //  fill fieldLengths
                    fieldLengths = new int[17] { 1, 4, 9, 9, 8, 6, 14, 12, 9, 5, 6, 3, 6, 3, 6, 3, 6 };
                    break;
                case 2:
                    //  Output filename
                    StringBuilder addCruiser = new StringBuilder();
                    addCruiser.Append("_CruiserSingleSummary_");
                    addCruiser.Append(markInits);
                    addCruiser.Append(".txt");
                    outputFileName = checkCruiseFile.Replace("_CC.cruise", addCruiser.ToString());

                    TRH.singleSaleHeader[2] = TRH.singleSaleHeader[2].Replace("XXXX", currSaleName);
                    TRH.singleSaleHeader[3] = TRH.singleSaleHeader[3].Replace("XXXX", cruiserFile);
                    TRH.singleSaleHeader[4] = TRH.singleSaleHeader[4].Replace("XXXX", checkCruiseFile);
                    TRH.singleSaleHeader[5] = TRH.singleSaleHeader[5].Replace("XX", cruiseTolerances.Count.ToString());
                    TRH.singleSaleHeader[6] = TRH.singleSaleHeader[6].Replace("XXXX", cruiseTolerances[0].T_DateStamp);

                    //  fill fieldLengths
                    fieldLengths = new int[15] { 1, 8, 8, 6, 11, 10, 9, 8, 6, 3, 6, 3, 6, 3, 6 };
                    break;
                case 3:
                    //  output filename
                    outputFileName = checkCruiseFile.Replace("_CC.cruise", "_CruiserMultipleSummary.txt");

                    TRH.multipleSaleHeader[3] = TRH.multipleSaleHeader[3].Replace("XX", cruiseTolerances.Count.ToString());
                    TRH.multipleSaleHeader[4] = TRH.multipleSaleHeader[4].Replace("XXXX", cruiseTolerances[0].T_DateStamp);

                    //  fill fieldLengths
                    fieldLengths = new int[14] { 1, 8, 10, 10, 11, 9, 5, 6, 3, 6, 3, 6, 3, 6 };
                    break;
            }   //  end switch on reportToOutput

            Cursor.Current = Cursors.WaitCursor;
            using (StreamWriter strTextOut = new StreamWriter(outputFileName))
            {
                //  process by elements
                foreach (TolerancesList tl in cruiseTolerances)
                {
                    if (treeBasedFlag > 0 && tl.T_Element == "In/Out Trees")
                        treeBasedFlag = 0;
                    else
                    {
                        //  write report heading and column headers for each new element
                        WriteReportHeading(strTextOut);
                        //  then write column headigns
                        WriteColumnHeaders(strTextOut, tl.T_Element);
                        //  print records for current element
                        PrintElement(strTextOut, tl);
                        numOlines = 0;
                    }   //  endif
                }   //  end foreach

                //  print volume whether or not include volume was selected
                foreach (TolerancesList ct in cruiseTolerances)
                {
                    if (ct.T_CUFTPGtolerance > 0 || ct.T_CUFTPGtolerance == -1)
                    {
                        //  create output for both gross and net primary product volume
                        //  gross cuft
                        PrintVolume(1, strTextOut, checkResults, ct.T_CUFTPGtolerance);
                        //  net cuft
                        PrintVolume(2, strTextOut, checkResults, ct.T_CUFTPNtolerance);
                    }   //  endif
                    if(ct.T_CUFTSGtolerance > 0 || ct.T_CUFTSGtolerance == -1)
                    {
                        //  create output for both gross and net secondary product volume
                        //  gross cuft
                        PrintVolume(3, strTextOut, checkResults, ct.T_CUFTSGtolerance);
                        //  net cuft
                        PrintVolume(4, strTextOut, checkResults, ct.T_CUFTSNtolerance);
                    }   //  endif
                    if(ct.T_BDFTPGtolerance > 0 || ct.T_BDFTPGtolerance == -1)
                    {
                        //  create output for both gross and net primary product volume
                        //  gross BDFT
                        PrintVolume(5, strTextOut, checkResults, ct.T_BDFTPGtolerance);
                        //  net BDFT
                        PrintVolume(6, strTextOut, checkResults, ct.T_BDFTPNtolerance);
                    }   //  endif
                    if (ct.T_BDFTSGtolerance > 0 || ct.T_BDFTSGtolerance == -1)
                    {
                        //  create output for both gross and net secondary product volume
                        //  gross BDFT
                        PrintVolume(7, strTextOut, checkResults, ct.T_BDFTSGtolerance);
                        //  net BDFT
                        PrintVolume(8, strTextOut, checkResults, ct.T_BDFTSNtolerance);
                    }   //  endif
                    if (ct.T_CUFTPSGtolerance > 0 || ct.T_CUFTPSGtolerance == -1)
                    {
                        //  create output for just gross CUFT which includes primary and secondary
                        PrintVolume(9, strTextOut, checkResults, ct.T_CUFTPSGtolerance);
                    }   //  endif
                    if (ct.T_CUFTPSNtolerance > 0 || ct.T_CUFTPSNtolerance == -1)
                    {
                        //  and net CUFT including primary and secondary
                        PrintVolume(10, strTextOut, checkResults, ct.T_CUFTPSNtolerance);
                    }   //  endif
                    if (ct.T_BDFTPSGtolerance > 0 || ct.T_BDFTPSGtolerance == -1)
                    {
                        //  gross BDFT including primary and secondary
                        PrintVolume(11, strTextOut, checkResults, ct.T_BDFTPSGtolerance);
                    }   //  endif
                    if (ct.T_BDFTPSNtolerance > 0 || ct.T_BDFTPSNtolerance == -1)
                    {
                        // net BDFT including primary and secondary
                        PrintVolume(12, strTextOut, checkResults, ct.T_BDFTPSNtolerance);
                    }   //  endif
                }   //  end for each loop

                strTextOut.Close();
            }   //  end using
            Cursor.Current = this.Cursor;
            
            return outputFileName.ToString();
        }   //  end generateTextReports


        private void PrintElement(StreamWriter strTextOut, TolerancesList currElement)
        {
            //  Change to checkResults list added all record to the table
            //  So this is fine for In/out Trees and species but the rest of the elements
            //  need to be measured trees only
            //  need to pull measured trees for other elements
            //  and just log records for log elements
            List<ResultsList> currentResults = new List<ResultsList>();
            if (currElement.T_Element != "In/Out Trees" && currElement.T_Element != "Species" &&
                currElement.T_Element != "Log Grade" && currElement.T_Element != "Log Defect %")
                currentResults = checkResults.FindAll(
                    delegate(ResultsList rl)
                    {
                        return rl.R_CountMeasure == "M";
                    });
            else if (currElement.T_Element == "Log Grade" || currElement.T_Element == "Log Defect %")
            {
                currentResults = checkResults.FindAll(
                    delegate(ResultsList rl)
                    {
                        return rl.R_LogNumber != "0";
                    });
                //  reset fieldLengths                            
                fieldLengths = new int[18] { 1, 8, 9, 9, 8, 6, 5, 12, 11, 9, 5, 6, 3, 6, 3, 6, 3, 6 };
            }
            else if (currElement.T_Element == "In/Out Trees" || currElement.T_Element == "Species")
                currentResults = checkResults.FindAll(
                    delegate(ResultsList rl)
                    {
                        return rl.R_CountMeasure == "M" || rl.R_CountMeasure == "C";
                    });

            //  print each element
            string oot = "";
            switch (currElement.T_Element)
            {
                case "Species":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_Species_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].Species.PadLeft(6, ' '), cr.R_CC_Species.PadLeft(6, ' '), "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "LiveDead":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_LiveDead_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].LiveDead.PadLeft(6, ' '), cr.R_CC_LiveDead.PadLeft(6, ' '), "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Product":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_Product_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].SampleGroup.PrimaryProduct.PadLeft(6, ' '), cr.R_CC_Product.PadLeft(6, ' '), "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Clear Face":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_Clear_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].ClearFace.PadLeft(6, ' '), cr.R_CC_Clear.PadLeft(6, ' '), "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Tree Grade":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_TreeGrade_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].Grade.PadLeft(6, ' '), cr.R_CC_TreeGrade.PadLeft(6, ' '), "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "DBH":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_DBHOB_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].DBH.ToString().PadLeft(6, ' '), cr.R_CC_DBHOB.ToString().PadLeft(6, ' '),
                                        Utilities.FormatField(cr.R_CC_DBHOB - cruiserTrees[nthRow].DBH, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Total Height":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_TotalHeight_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].TotalHeight.ToString().PadLeft(6, ' '), cr.R_CC_TotalHeight.ToString().PadLeft(6, ' '),
                                         Utilities.FormatField(cr.R_CC_TotalHeight - cruiserTrees[nthRow].TotalHeight, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Total Height <= 100 feet":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_TotHeightUnder_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].TotalHeight.ToString().PadLeft(6, ' '), cr.R_CC_TotHeightUnder.ToString().PadLeft(6, ' '),
                                            Utilities.FormatField(cr.R_CC_TotHeightUnder - cruiserTrees[nthRow].TotalHeight, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Total Height > 100 feet":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_TotHeightOver_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].TotalHeight.ToString().PadLeft(6, ' '), cr.R_CC_TotHeightOver.ToString().PadLeft(6, ' '),
                                            Utilities.FormatField(cr.R_CC_TotHeightOver - cruiserTrees[nthRow].TotalHeight, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Merch Height Primary":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_MerchHgtPP_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].MerchHeightPrimary.ToString().PadLeft(6, ' '), cr.R_CC_MerchHgtPP.ToString().PadLeft(6, ' '),
                                           Utilities.FormatField(cr.R_CC_MerchHgtPP - cruiserTrees[nthRow].MerchHeightPrimary, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Merch Height Secondary":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_MerchHgtSP_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].MerchHeightSecondary.ToString().PadLeft(6, ' '), cr.R_CC_MerchHgtSP.ToString().PadLeft(6, ' '),
                                           Utilities.FormatField(cr.R_CC_MerchHgtSP - cruiserTrees[nthRow].MerchHeightSecondary, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Height To First Live Limb":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_HgtFLL_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].HeightToFirstLiveLimb.ToString().PadLeft(6, ' '), cr.R_CC_HgtFLL.ToString().PadLeft(6, ' '),
                                           Utilities.FormatField(cr.R_CC_HgtFLL - cruiserTrees[nthRow].HeightToFirstLiveLimb, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Top DIB Primary":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_TopDIBPP_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].TopDIBPrimary.ToString().PadLeft(6, ' '), cr.R_CC_TopDIBPP.ToString().PadLeft(6, ' '),
                                           Utilities.FormatField(cr.R_CC_TopDIBPP - cruiserTrees[nthRow].TopDIBPrimary, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Recoverable %":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_RecDef_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserTrees[nthRow].RecoverablePrimary.ToString().PadLeft(6, ' '), cr.R_CC_RecDef.ToString().PadLeft(6, ' '),
                                            Utilities.FormatField(cr.R_CC_RecDef - cruiserTrees[nthRow].RecoverablePrimary, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Seen Defect % Primary":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_SeenDefPP_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            //  for region 5, need to calculate seen defect before printing
                            float seenDefect = 0;
                            if (currRegion == "05" || currRegion == "5")
                            {
                                seenDefect = getCruiserDefect((long) cruiserTrees[nthRow].Tree_CN);
                                LoadElementVariables(Utilities.FormatField(seenDefect,"{0,6:F1}").ToString().PadLeft(6,' '), 
                                                Utilities.FormatField(cr.R_CC_SeenDefPP,"{0,6:F1}").ToString().PadLeft(6, ' '),
                                                Utilities.FormatField(cr.R_CC_SeenDefPP - seenDefect, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            }
                            else
                            LoadElementVariables(Utilities.FormatField(cruiserTrees[nthRow].SeenDefectPrimary,"{0,6:F1}").ToString().PadLeft(6, ' '), 
                                            Utilities.FormatField(cr.R_CC_SeenDefPP,"{0,6:F1}").ToString().PadLeft(6, ' '),
                                            Utilities.FormatField(cr.R_CC_SeenDefPP - cruiserTrees[nthRow].SeenDefectPrimary, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                           
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Seen Defect % Secondary":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_SeenDefSP_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(Utilities.FormatField(cruiserTrees[nthRow].SeenDefectSecondary,"{0,6:F1}").ToString().PadLeft(6, ' '), 
                                                Utilities.FormatField(cr.R_CC_SeenDefSP,"{0,6:F1}").ToString().PadLeft(6, ' '),
                                        Utilities.FormatField(cr.R_CC_SeenDefSP - cruiserTrees[nthRow].SeenDefectSecondary, "{0,6:F1}").ToString().PadLeft(6, ' '), " ", " ", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "Log Grade":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Log");
                        //  find cruiser tree
                        int nthRow = findCruiserLog(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (checkLogs[nthRow].Tree.Initials != null)
                                markInits = checkLogs[nthRow].Tree.Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_LogGrade_R == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables(checkLogs[nthRow].Grade.PadLeft(6, ' '), cr.R_CC_LogGrade.PadLeft(6, ' '), "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;       
                case "Log Defect %":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Log");
                        //  find cruiser tree
                        int nthRow = findCruiserLog(cr);
                        if(nthRow >= 0)
                        {
                            markInits = "";   
                            //  new page needed?
                            if(numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (checkLogs[nthRow].Tree.Initials != null)
                                markInits = checkLogs[nthRow].Tree.Initials;
                            else markInits = " ";
                            if (nthRow >= 0)
                                cruiserString = Utilities.FormatField(checkLogs[nthRow].SeenDefect, "{0,6:F0}").ToString().PadLeft(6, ' ');
                            else cruiserString = "**";
                            checkCruiserString = Utilities.FormatField(cr.R_CC_LogSeenDef, "{0,6:F0}").ToString().PadLeft(6, ' ');
                            diff1 = Utilities.FormatField(cr.R_CC_LogSeenDef - checkLogs[nthRow].SeenDefect, "{0,6:F0}").ToString().PadLeft(6, ' ');
                            diff2 = " ";
                            diff3 = " ";
                            //  finish print array and print line
                            if (cr.R_LogSeenDef_R == 1)
                                outOfTolerance = "1 **";
                            else oot = "0";
                            LoadElementVariables(cruiserString, checkCruiserString, diff1, diff2, diff3, oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
                case "In/Out Trees":
                    foreach (ResultsList cr in currentResults)
                    {     
                        //  clear print array
                        prtArray.Clear();
                        prtArray.Add("");
                        //  load Identifying information
                        LoadIdentifyingInfo(cr, "Tree");
                        //  find cruiser tree
                        int nthRow = findCruiserTree(cr);
                        if (nthRow >= 0)
                        {
                            markInits = "";
                            //  new page needed?
                            if (numOlines >= 50 || numOlines == 0)
                            {
                                WriteReportHeading(strTextOut);
                                WriteColumnHeaders(strTextOut, currElement.T_Element);
                            }   //  endif
                            if (cruiserTrees[nthRow].Initials != null)
                                markInits = cruiserTrees[nthRow].Initials;
                            else markInits = " ";
                            //  finish print array and print line
                            if (cr.R_OutResult == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables("      ", "      ", "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }
                        else if (nthRow < 0)
                        {
                            //  probably an out tree called in by check cruiser
                            if (cr.R_InResult == 1)
                                oot = "1 **";
                            else oot = "0";
                            LoadElementVariables("      ", "      ", "na", "na", "na", oot);
                            LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                            printLine(strTextOut, currElement.T_Element);
                        }   //  endif on nthRow
                    }   //  end foreach loop
                    break;
            }   //  end switch on element
        
            return;
        }   //  end PrintElements


        private int findCruiserTree(ResultsList cr)
        {
            string tempPlot;
            int nthRow = cruiserTrees.FindIndex(
                delegate(TreeDO t)
                {
                    if (t.Plot == null)
                        tempPlot = "";
                    else tempPlot = t.Plot.PlotNumber.ToString();
                    return t.Stratum.Code == cr.R_Stratum && t.CuttingUnit.Code == cr.R_CuttingUnit &&
                                tempPlot == cr.R_Plot && t.TreeNumber.ToString() == cr.R_TreeNumber;
                });

            return nthRow;
        }   //  end findCruiserTree

        
        private int findCruiserLog(ResultsList cr)
        {
            string tempPlot = "";
            //  pull log for this check cruiser result
            int nthRow = checkLogs.FindIndex(
                delegate(LogStockDO lsd)
                {
                    if (lsd.Tree.Plot.PlotNumber.ToString() == null)
                        tempPlot = "";
                    else tempPlot = lsd.Tree.Plot.PlotNumber.ToString();
                    
                    return lsd.Tree.Stratum.Code == cr.R_Stratum && lsd.Tree.CuttingUnit.Code == cr.R_CuttingUnit &&
                                               tempPlot == cr.R_Plot && lsd.LogNumber == cr.R_LogNumber;
                });
            return nthRow;
        }   //  end findCruiserLog


        private void LoadIdentifyingInfo(ResultsList cr, string logElement)
        {
            //  loads identifying information for specified report
            switch (reportToOutput)
            {
                case 1:
                    prtArray.Add(cr.R_Stratum.PadLeft(4, ' '));
                    prtArray.Add(cr.R_CuttingUnit.PadLeft(9, ' '));
                    prtArray.Add(cr.R_Plot.PadLeft(9, ' '));
                    prtArray.Add(cr.R_TreeNumber.PadLeft(8, ' '));
                    prtArray.Add(cr.R_CountMeasure.PadLeft(6, ' '));
                    if (logElement == "Log") prtArray.Add(cr.R_LogNumber.PadLeft(5, ' '));
                    break;
                case 2:     case 3:
                    prtArray.Add(cr.R_CuttingUnit.PadLeft(8, ' '));
                    prtArray.Add(cr.R_TreeNumber.PadLeft(8, ' '));
                    prtArray.Add(cr.R_CountMeasure.PadLeft(6, ' '));
                    break;
            }   //  end switcu
            return;
        }   //  end LoadIdentifyingInto


        private void LoadPrintArray(string cruiserString, string checkCruiserString, string outOfTolerance, 
                                    string diff1, string diff2, string diff3)
        {
            prtArray.Add(cruiserString);
            prtArray.Add(checkCruiserString);
            prtArray.Add(markInits);
            prtArray.Add(columnSeparator);
            prtArray.Add(diff1);
            prtArray.Add(columnSeparator);
            prtArray.Add(diff2);
            prtArray.Add(columnSeparator);
            prtArray.Add(diff3);
            prtArray.Add(columnSeparator);
            prtArray.Add(outOfTolerance);

            return;
        }   //  end LoadPrintArray


        private void WriteReportHeading(StreamWriter strTextOut)
        {
            if (numOlines == 0 || numOlines >= 50)
            {
                //  page break
                strTextOut.WriteLine("\f");
                numOlines = 0;

                //  output main header
                for (int k = 0; k < 3; k++)
                {
                    strTextOut.WriteLine(TRH.TextHeader[k]);
                    numOlines++;
                }   //  end for k loop

                switch (reportToOutput)
                {
                    case 1:
                        for (int k = 0; k < TRH.saleSummaryHeader.Count(); k++)
                        {
                            strTextOut.WriteLine(TRH.saleSummaryHeader[k]);
                            numOlines++;
                        }   //  end foreach
                        break;
                    case 2:
                        for (int k = 0; k < TRH.singleSaleHeader.Count(); k++)
                        {
                            strTextOut.WriteLine(TRH.singleSaleHeader[k]);
                            numOlines++;
                        }   //  end foreach
                        break;
                    case 3:
                        for (int k = 0; k < TRH.multipleSaleHeader.Count(); k++)
                        {
                            strTextOut.WriteLine(TRH.multipleSaleHeader[k]);
                            numOlines++;
                        }   //  end foreach
                        break;
                }   //  end switch
                //  write two blank lines before headers
                strTextOut.WriteLine("");
                numOlines++;
                strTextOut.WriteLine("");
                numOlines++;
            }   //  endif
            return;
        }   //  end WriteReportHeading


        private void WriteColumnHeaders(StreamWriter strTextOut, string currElement)
        {
            string holdHeading;
            //  log element headings
            if (currElement == "Log Grade" || currElement == "Log Defect %")
            {
                holdHeading = TRH.summaryLogElementHeaders[0];
                TRH.summaryLogElementHeaders[0] = TRH.summaryLogElementHeaders[0].Replace("XXX", currElement);
                for (int k = 0; k < TRH.summaryLogElementHeaders.Count(); k++)
                {
                    strTextOut.WriteLine(TRH.summaryLogElementHeaders[k]);
                    numOlines++;
                }   //  end for k loop
            }
            else
            {
                switch (reportToOutput)
                {
                    case 1:
                        holdHeading = TRH.summaryColumnHeaders[0];
                        TRH.summaryColumnHeaders[0] = TRH.summaryColumnHeaders[0].Replace("XXX", currElement);
                        for (int k = 0; k < TRH.summaryColumnHeaders.Count(); k++)
                        {
                            strTextOut.WriteLine(TRH.summaryColumnHeaders[k]);
                            numOlines++;
                        }   //  end for k loop
                        //  replace first line of heading for next page
                        TRH.summaryColumnHeaders[0] = holdHeading;
                        break;
                    case 2:
                    case 3:
                        holdHeading = TRH.columnHeadings[0];
                        TRH.columnHeadings[0] = TRH.columnHeadings[0].Replace("XXX", currElement);
                        for (int k = 0; k < TRH.columnHeadings.Count(); k++)
                        {
                            strTextOut.WriteLine(TRH.columnHeadings[k]);
                            numOlines++;
                        }   //  end for k loop
                        //  replace first line of heading for next page
                        TRH.columnHeadings[0] = holdHeading;
                        break;
                }   // end switch
            }   //  endif
            //  write long line or medium  line whatever you call it
            strTextOut.WriteLine("___________________________________________________________________________________________________________________________________");
            numOlines++;
                        
            return;
        }   //  end WriteColumnHeaders


        private void printLine(StreamWriter strTextOut, string currentElement)
        {
            //  new page needed?
            if (numOlines >= 50 || numOlines == 0)
            {
                WriteReportHeading(strTextOut);
                WriteColumnHeaders(strTextOut, currentElement);
            }   //  endif
            StringBuilder printLine = new StringBuilder();
            int k = 0;
            foreach (object obj in prtArray)
            {
                printLine.Append(obj.ToString().PadLeft(fieldLengths[k]));
                k++;
            }   //  end foreach loop
            strTextOut.WriteLine(printLine.ToString());
            numOlines++;
            return;
        }   //  end printLine


        private void LoadElementVariables(string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            cruiserString = Value1;
            checkCruiserString = Value2;
            diff1 = Value3;
            diff2 = Value4;
            diff3 = Value5;
            outOfTolerance = Value6;
            return;
        }   //  end LoadElementVariables


        private void PrintVolume(int whichVolume, StreamWriter strTextOut, List<ResultsList> checkResults, double currTolerance)
        {
            //  reset field lengths
            fieldLengths = new int[17] { 1, 4, 9, 9, 8, 6, 14, 12, 9, 5, 6, 3, 6, 3, 6, 3, 6 };
            string currElement = "";
            if (numOlines > 0) numOlines = 0;
            //  load values from checkResults for measured trees only
            List<ResultsList> currentResults = checkResults.FindAll(
                delegate(ResultsList rl)
                {
                    return rl.R_CountMeasure == "M";
                });

            foreach (ResultsList rl in currentResults)
            {
                //  clear print array
                prtArray.Clear();
                prtArray.Add("");
                //  identifying data
                switch (reportToOutput)
                {
                    case 1:
                        prtArray.Add(rl.R_Stratum.PadLeft(4, ' '));
                        prtArray.Add(rl.R_CuttingUnit.PadLeft(9, ' '));
                        prtArray.Add(rl.R_Plot.PadLeft(9, ' '));
                        prtArray.Add(rl.R_TreeNumber.PadLeft(8, ' '));
                        prtArray.Add(rl.R_CountMeasure.PadLeft(6, ' '));
                        break;
                    case 2:
                    case 3:
                        prtArray.Add(rl.R_CuttingUnit.PadLeft(8, ' '));
                        prtArray.Add(rl.R_TreeNumber.PadLeft(10, ' '));
                        prtArray.Add(rl.R_CountMeasure.PadLeft(6, ' '));
                        break;
                }   //  end switch

                //  need cruiser initials for current tree
                if(rl.R_MarkerInitials != null)
                    markInits = rl.R_MarkerInitials;
                else markInits = " ";

                //  key to whichVolume
                //  1 = Gross CUFT Primary
                //  2 = Net CUFT Primary
                //  3 = Gross CUFT Secondary
                //  4 = Net CUFT Secondary
                //  5 = Gross BDFT Primary
                //  6 = Net BDFT Primary
                //  7 = Gross BDFT Secondary
                //  8 = Net BDFT Secondary
                //  9 = Gross CUFT including primary and secondary
                // 10 = Net CUFT including primary and secondary
                // 11 = Gross BDFT including primary and secondary
                // 12 = Net BDFT including primary and secondary

                switch(whichVolume)
                {
                    case 1:
                        //  Primary Gross CUFT
                        LoadVolumeVariables(rl.R_GrossCUFTPP, rl.R_CC_GrossCUFTPP, currTolerance);
                        currElement = "Gross CUFT Primary";
                        break;
                    case 2:
                        //  Primary net CUFT
                        LoadVolumeVariables(rl.R_NetCUFTPP, rl.R_CC_NetCUFTPP, currTolerance);
                        currElement = "Net CUFT Primary";
                        break;
                    case 3:
                        //  Secondary gross CUFT
                        LoadVolumeVariables(rl.R_GrossCUFTSP, rl.R_CC_GrossCUFTSP, currTolerance);
                        currElement = "Gross CUFT Secondary";
                        break;
                    case 4:
                        //  Secondary net CUFT
                        LoadVolumeVariables(rl.R_NetCUFTSP, rl.R_CC_NetCUFTSP, currTolerance);
                        currElement = "Net CUFT Secondary";
                        break;
                    case 5:
                        //  Primary Gross BDFT
                        LoadVolumeVariables(rl.R_GrossBDFTPP, rl.R_CC_GrossBDFTPP, currTolerance);
                        currElement = "Gross BDFT Primary";
                        break;
                    case 6:
                        //  Primary net BDFT
                        LoadVolumeVariables(rl.R_NetBDFTPP, rl.R_CC_NetBDFTPP, currTolerance);
                        currElement = "Net BDFT Primary";
                        break;
                    case 7:
                        //  Secondary gross BDFT
                        LoadVolumeVariables(rl.R_GrossBDFTSP, rl.R_CC_GrossBDFTSP, currTolerance);
                        currElement = "Gross BDFT Secondary";
                        break;
                    case 8:
                        // Secondary net BDFT
                        LoadVolumeVariables(rl.R_NetBDFTSP, rl.R_CC_NetBDFTSP, currTolerance);
                        currElement = "Net BDFT Secondary";
                        break;
                    case 9:
                        //  Gross CUFT which includes primary and secondary
                        LoadVolumeVariables(rl.R_GrossCUFTPSP, rl.R_CC_GrossCUFTPSP, currTolerance);
                        currElement = "Gross CUFT Primary + Secondary";
                        break;
                    case 10:
                        //  Net CUFT including primary and secondary
                        LoadVolumeVariables(rl.R_NetCUFTPSP, rl.R_CC_NetCUFTPSP, currTolerance);
                        currElement = "Net CUFT Primary + Secondary";
                        break;
                    case 11:
                        //  Gross BDFT including primary and secondary
                        LoadVolumeVariables(rl.R_GrossBDFTPSP, rl.R_CC_GrossBDFTPSP, currTolerance);
                        currElement = "Gross BDFT Primary + Secondary";
                        break;
                    case 12:
                        //  Net BDFT including primary and secondary
                        LoadVolumeVariables(rl.R_NetBDFTPSP, rl.R_CC_NetBDFTPSP, currTolerance);
                        currElement = "Net BDFT Primary + Secondary";
                        break;                    
                }   //  end switch

                //  load print array
                LoadPrintArray(cruiserString, checkCruiserString, outOfTolerance, diff1, diff2, diff3);
                printLine(strTextOut, currElement);
            }   //  end foreach loop
            return;
        }   //  end PrintVolume


        private void LoadVolumeVariables(double cruiserVolume, double checkCruiserVolume, double currTolerance)
        {
            double tolerPlus = 0;
            double tolerMinus = 0;
            double actualDiff = 0;
            // these two differences will always be blank
            diff2 = " ";
            diff3 = " ";
            cruiserString = Utilities.FormatField(cruiserVolume, "{0,6:F0}").ToString().PadLeft(6, ' ');
            checkCruiserString = Utilities.FormatField(checkCruiserVolume, "{0,6:F0}").ToString().PadLeft(6, ' ');
            //  calculate tolerance range
            if (currTolerance > 0)
            {
                tolerPlus = checkCruiserVolume + (checkCruiserVolume * (currTolerance / 100));
                tolerMinus = checkCruiserVolume - (checkCruiserVolume * (currTolerance / 100));
                actualDiff = checkCruiserVolume - cruiserVolume;
                diff1 = Utilities.FormatField(actualDiff, "{0,3:F0}").ToString().PadLeft(3, ' ');
                if (Math.Abs(actualDiff) < tolerMinus && Math.Abs(actualDiff) > tolerPlus)
                    outOfTolerance = "1 **";
                else outOfTolerance = "0";
            }
            else outOfTolerance = "na";
            return;
        }   //  end LoadVolumeVariables


        private float getCruiserDefect(long currTree_CN)
        {
            int nthRow = calculatedTrees.FindIndex(
                delegate(TreeCalculatedValuesDO tcv)
                {
                    return tcv.Tree_CN == currTree_CN;
                });
            if (nthRow >= 0)
            {
                if(calculatedTrees[nthRow].GrossCUFTPP > 0)
                    return ((calculatedTrees[nthRow].GrossCUFTPP - calculatedTrees[nthRow].NetCUFTPP)/calculatedTrees[nthRow].GrossCUFTPP) * 100;
            }   // endif
            return 0;
        }   //  end getCruiserDefect


        private int CheckStrata()
        {
            //  check each stratum in checkResults for all tree-based methods
            List<StratumDO> sList = db.getStrata();
            int numTreeBased = 0;
            foreach (StratumDO s in sList)
            {
                if (s.Method == "100" || s.Method == "STR" ||
                    s.Method == "S3P" || s.Method == "3P")
                    numTreeBased++;
            }   //  end foreach
            if (numTreeBased == sList.Count)
            {
                //  can't remove in/out element since that line stores other
                //  flags for volume
                //  so set a flag to indicate all strata were tree-based
                return numTreeBased;
            }   //  endif
            return 0;
        }   //  end CheckStrata

    }   //  end createSummaryReports
}
