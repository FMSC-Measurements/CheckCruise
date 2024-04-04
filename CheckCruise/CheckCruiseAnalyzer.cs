using CruiseDAL.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckCruise
{
    public class CheckCruiseAnalyzer
    {

        public dataBaseCommands CheckCruiseDataService { get; }
        public dataBaseCommands CruiseDataService { get; }

        public CheckCruiseAnalyzer(dataBaseCommands checkCruiseDataService, dataBaseCommands cruiseDataService)
        {
            CheckCruiseDataService = checkCruiseDataService;
            CruiseDataService = cruiseDataService;
        }


        public Task AnalyzeAsync(IProgress<string> progress = null)
        {
            return Task.Run(() => Analyze(progress));
        }

        public void Analyze(IProgress<string> progress = null)
        {
            List<string> warnings = new List<string>();

            List<TreeDO> checkTrees = CheckCruiseDataService.getTrees();
            List<TreeCalculatedValuesDO> checkCalculatedTrees = CheckCruiseDataService.getCalculatedTrees();
            List<TolerancesList> cruiseTolerances = CheckCruiseDataService.getTolerances();
            List<LogStockDO> checkLogs = CheckCruiseDataService.getLogStock();
            var justStrataUnits = CheckCruiseDataService.getStrataUnit();
            string currRegion = CheckCruiseDataService.getRegion();
            string checkCruiserInitials = CheckCruiseDataService.getCheckCruiserInitials();

            if (String.IsNullOrEmpty(checkCruiserInitials))
            {
                throw new ArgumentException("No Value For Check Cruiser Initials");
            }

            if (!cruiseTolerances.Any())
            { throw new InvalidOperationException("Check Cruise Has No Tolerances"); }

            if (checkCalculatedTrees.Sum(c => c.GrossCUFTPP) == 0.0 && checkCalculatedTrees.Sum(c => c.GrossBDFTPP) == 0.0)
            {
                throw new InvalidOperationException("No volume detected!\nPlease process the check cruise file in CruiseProcessing\nand retry Check Cruise.");
            }


            List<TreeDO> originalTrees = CruiseDataService.getTrees();
            List<TreeCalculatedValuesDO> originalCalculatedTrees = CruiseDataService.getCalculatedTrees();
            List<LogStockDO> originalLogs = CruiseDataService.getLogStock();

            if (originalCalculatedTrees.Sum(o => o.GrossCUFTPP) == 0.0 && originalCalculatedTrees.Sum(o => o.GrossBDFTPP) == 0.0)
            {
                throw new InvalidOperationException("No volume detected!\nPlease process the cruise file in CruiseProcessing\nand retry Check Cruise.");
            }

            var longestInitialValue = originalTrees.Select(i => i.Initials.Length).Max();
            if(longestInitialValue > 3)
            {
                warnings.Add("Tree with longer than three character initials found");
            }

            //var hasMultipleInitials = originalTrees.GroupBy(x => x.Initials).Count() > 1;
            //if(hasMultipleInitials)
            //{
                  
            //}

            if( !checkTrees.Any(t => t.Initials == checkCruiserInitials))
            {
                throw new InvalidOperationException("NO TREES IN THE CHECK CRUISE FILE HAVE CHECK CRUISER INITIALS!\nCannot continue with anaylsis.\n Please record check cruiser initials on checked trees.");
            }

            var initialiedTrees = checkTrees.Where(t => t.Initials == checkCruiserInitials).ToArray();
            var justCheckTrees = initialiedTrees.Where( t => t.CountOrMeasure == "M").ToArray();


            //  fill results table -- clear table first
            //  July 2015 -- per conversation with C.Bodenhausen, looks like all count and measure
            //  trees need to go in the results table.  Mainly because a count record could be
            //  called in by the check cruiser and needs to be noted in the results table
            CheckCruiseDataService.clearResults();
            var checkResults = fillResultsTable(initialiedTrees);
            CheckCruiseDataService.saveResults(checkResults);
            // TODO this is redundant can we get rid of this?
            checkResults = CheckCruiseDataService.getResultsTable("", "");

            //  loop through tolerance elements to compare
            foreach (TolerancesList ct in cruiseTolerances)
            {
                //  if current element is In/Out trees need to do analysis differently
                //  need original trees and check trees complete for comparison
                if (ct.T_Element == "In/Out Trees")
                    CheckInOutTrees(ct, checkTrees, originalTrees, checkCruiserInitials, checkResults, justStrataUnits);
                else if (ct.T_Element == "Second Height Accuracy")
                {
                    //  element is used for Region 8 only and based on upper stem height
                    foreach (TreeDO jc in justCheckTrees)
                    {
                        //  find cruiser tree for current check tree
                        TreeDO cruiserTree = originalTrees.Find(
                            delegate (TreeDO t)
                            {
                                return t.Tree_CN == jc.Tree_CN;
                            });
                        var nthResult = plusOrMinus(cruiserTree.UpperStemHeight, jc.UpperStemHeight, ct.T_Tolerance, ct.T_Units);

                    }

                }
                else if (ct.T_Element == "Second Height Bias")
                {
                    //  element is used for Region 8 only and is based on an average height for upper tem HT
                    //  find average height for upper stem height in checked trees
                    int numChecked = checkTrees.Count;
                    float sumCheckedUSH = checkTrees.Sum(c => c.UpperStemHeight);
                    int numCruiser = originalTrees.Count;
                    float sumCruiserUSH = originalTrees.Sum(o => o.UpperStemHeight);
                    float checkedAverage = sumCheckedUSH / numChecked;
                    float cruiserAverage = sumCruiserUSH / numCruiser;
                    var nthResult = plusOrMinus((float)cruiserAverage, (float)checkedAverage, ct.T_Tolerance, ct.T_Units);
                }
                else
                {
                    foreach (TreeDO td in justCheckTrees)
                    {
                        //  find cruiser tree for current check tree
                        TreeDO cruiserTree = originalTrees.Find(
                            delegate (TreeDO t)
                            {
                                return t.Tree_CN == td.Tree_CN;
                            });
                        if (cruiserTree != null)
                        {
                            var nthResult = 0;
                            switch (ct.T_Element)
                            {
                                case "Species":
                                    nthResult = straightComparison(cruiserTree.Species, td.Species);
                                    break;
                                case "Live/Dead":
                                    nthResult = straightComparison(cruiserTree.LiveDead, td.LiveDead);
                                    break;
                                case "Product":
                                    nthResult = straightComparison(cruiserTree.SampleGroup.PrimaryProduct, td.SampleGroup.PrimaryProduct);
                                    break;
                                case "DBH":
                                    nthResult = DBHanalysis(td, cruiserTree, ct);
                                    break;
                                case "Total Height <= 100 feet":
                                case "Total Height > 100 feet":
                                case "Total Height":
                                    nthResult = totalHeightAnalysis(td, cruiserTree, ct);
                                    break;
                                case "Merch Height Primary":
                                    nthResult = plusOrMinus(cruiserTree.MerchHeightPrimary, td.MerchHeightPrimary, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Merch Height Secondary":
                                    nthResult = plusOrMinus(cruiserTree.MerchHeightSecondary, td.MerchHeightSecondary, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Height to First Live Limb":
                                    nthResult = plusOrMinus(cruiserTree.HeightToFirstLiveLimb, td.HeightToFirstLiveLimb, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Top DIB Primary":
                                    nthResult = plusOrMinus(cruiserTree.TopDIBPrimary, td.TopDIBPrimary, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Seen Defect % Primary":
                                    if (currRegion == "5" || currRegion == "05")
                                    {
                                        //  find current tree in calculated values
                                        TreeCalculatedValuesDO oct = originalCalculatedTrees.Find(
                                            delegate (TreeCalculatedValuesDO tcv)
                                            {
                                                return tcv.Tree_CN == td.Tree_CN;
                                            });
                                        TreeCalculatedValuesDO cct = checkCalculatedTrees.Find(
                                            delegate (TreeCalculatedValuesDO tcv)
                                            {
                                                return tcv.Tree_CN == td.Tree_CN;
                                            });
                                        nthResult = plusOrMinus(ct.T_Tolerance, oct, cct);
                                    }
                                    else nthResult = plusOrMinus(cruiserTree.SeenDefectPrimary, td.SeenDefectPrimary, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Seen Defect % Secondary":
                                    nthResult = plusOrMinus(cruiserTree.SeenDefectSecondary, td.SeenDefectSecondary, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Recoverable %":
                                    nthResult = plusOrMinus(cruiserTree.RecoverablePrimary, td.RecoverablePrimary, ct.T_Tolerance, ct.T_Units);
                                    break;
                                case "Form class":
                                    nthResult = straightComparison(cruiserTree.FormClass, td.FormClass);
                                    break;
                                case "Clear Face":
                                    nthResult = straightComparison(cruiserTree.ClearFace, td.ClearFace);
                                    break;
                                case "Tree Grade":
                                    nthResult = straightComparison(cruiserTree.Grade, td.Grade);
                                    break;
                                case "Log Grade":
                                    {
                                        //  find logs for current tree
                                        var checkTreeLogs = checkLogs.FindAll(
                                            delegate (LogStockDO l)
                                            {
                                                return l.Tree_CN == td.Tree_CN;
                                            });
                                        var cruiserTreeLogs = originalLogs.FindAll(
                                            delegate (LogStockDO ls)
                                            {
                                                return ls.Tree_CN == td.Tree_CN;
                                            });
                                        if (currRegion == "10")
                                            checkR10logs(checkTreeLogs, cruiserTreeLogs, td, ct, cruiserTree.Initials, checkResults);
                                        else
                                        {
                                            foreach (LogStockDO ctl in checkTreeLogs)
                                            {
                                                int nthRow = cruiserTreeLogs.FindIndex(
                                                    delegate (LogStockDO ls)
                                                    {
                                                        return ls.LogNumber == ctl.LogNumber;
                                                    });
                                                if (nthRow >= 0)
                                                {
                                                    nthResult = straightComparison(cruiserTreeLogs[nthRow].Grade, ctl.Grade);
                                                    updateLogChecks(td, ctl, nthResult, 1, cruiserTree.Initials, checkResults);
                                                }   //  endif
                                            }   //  end foreach loop
                                        }   //  endif                                    
                                        break;
                                    }
                                case "Log Defect %":
                                    {
                                        //  find logs for current tree
                                        var checkTreeLogs = checkLogs.FindAll(
                                            delegate (LogStockDO l)
                                            {
                                                return l.Tree_CN == td.Tree_CN;
                                            });
                                        var cruiserTreeLogs = originalLogs.FindAll(
                                            delegate (LogStockDO ls)
                                            {
                                                return ls.Tree_CN == td.Tree_CN;
                                            });
                                        if (currRegion == "10")
                                            checkR10logs(checkTreeLogs, cruiserTreeLogs, td, ct, cruiserTree.Initials, checkResults);
                                        else if (currRegion == "6" || currRegion == "06")
                                            checkR6logs(checkTreeLogs, cruiserTreeLogs, td, ct, cruiserTree.Initials, checkResults);
                                        else
                                        {
                                            foreach (LogStockDO ctl in checkTreeLogs)
                                            {
                                                int nthRow = cruiserTreeLogs.FindIndex(
                                                    delegate (LogStockDO ls)
                                                    {
                                                        return ls.LogNumber == ctl.LogNumber;
                                                    });
                                                if (nthRow >= 0)
                                                {
                                                    nthResult = straightComparison(cruiserTreeLogs[nthRow].Grade, ctl.Grade);
                                                    updateLogChecks(td, ctl, nthResult, 2, cruiserTree.Initials, checkResults);
                                                }   //  endif
                                            }   //  end foreach loop
                                        }   //  endif

                                        break;
                                    }
                            }   //  end switch

                            //  update results except for log grade and log defect
                            //  already done in the seperate calling routine
                            if (ct.T_Element != "Log Grade" && ct.T_Element != "Log Defect %")
                                updateResults(td, ct.T_Element, nthResult, checkResults);
                        }   // endif on cruiserTree   
                    }   //  end foreach loop on check trees
                }
            }   //  end for each loop

            progress?.Report("Capturing volumes");

            // TODO are we removing volumes from tolarances? 
            var includeVolumes = cruiseTolerances[0].T_IncludeVolume == 1;
            LoadVolume(checkTrees, checkCalculatedTrees, originalCalculatedTrees, checkResults, includeVolumes);

            CheckCruiseDataService.saveResults(checkResults);
            progress?.Report("Analysis complete");

        }

        private void LoadVolume(List<TreeDO> checkTrees, List<TreeCalculatedValuesDO> checkCalculatedTrees,
                                List<TreeCalculatedValuesDO> originalCalculatedTrees, IReadOnlyList<ResultsList> checkResults, bool includeVolumes)
        {
            //  load cubic and board volume into check results
            //  comparison is done when reports are generated
            foreach (TreeDO ct in checkTrees)
            {

                //  find tree_CN in check trees -- should be the same as in check calculated trees
                //  only measured trees will have volume so make sure this is a measured tree
                if (ct.CountOrMeasure == "M")
                {
                    TreeCalculatedValuesDO checkTree = checkCalculatedTrees.Find(
                        delegate (TreeCalculatedValuesDO t)
                        {
                            return t.Tree_CN == ct.Tree_CN;
                        });

                    // for testing purposes checkTree could be null meaning it wasn't processed so skip it for now
                    if (checkTree == null) break;

                    //  then find row in checkResults to update
                    //  correct plot if needed
                    string tempPlot;
                    if (ct.Plot == null)
                        tempPlot = "";
                    else tempPlot = ct.Plot.PlotNumber.ToString();


                    var checkResult = checkResults.FirstOrDefault(r => r.R_Stratum == ct.Stratum.Code && r.R_CuttingUnit == ct.CuttingUnit.Code &&
                                r.R_Plot == tempPlot && r.R_SampleGroup == ct.SampleGroup.Code &&
                                r.R_TreeNumber == ct.TreeNumber.ToString());

                    //  total volume is gross plus net primary and secondary (separately) for the species product recorded in check results
                    //  primary and secondary product are stored separately for each volume type

                    if (checkResult != null)
                    {
                        checkResult.R_TreeSpecies = ct.Species;
                        checkResult.R_TreeProduct = ct.SampleGroup.PrimaryProduct;

                        checkResult.R_CC_GrossCUFTPP = checkTree.GrossCUFTPP;
                        checkResult.R_CC_NetCUFTPP = checkTree.NetCUFTPP;
                        checkResult.R_CC_GrossBDFTPP = checkTree.GrossBDFTPP;
                        checkResult.R_CC_NetBDFTPP = checkTree.NetBDFTPP;

                        checkResult.R_CC_GrossCUFTSP = checkTree.GrossCUFTSP;
                        checkResult.R_CC_NetCUFTSP = checkTree.NetCUFTSP;
                        checkResult.R_CC_GrossBDFTSP = checkTree.GrossBDFTSP;
                        checkResult.R_CC_NetBDFTSP = checkTree.NetBDFTSP;

                        //  add primary and secondary together for total volume fields
                        checkResult.R_CC_GrossCUFTPSP = checkTree.GrossCUFTPP + checkTree.GrossCUFTSP;
                        checkResult.R_CC_NetCUFTPSP = checkTree.NetCUFTPP + checkTree.NetCUFTSP;
                        checkResult.R_CC_GrossBDFTPSP = checkTree.GrossBDFTPP + checkTree.GrossBDFTSP;
                        checkResult.R_CC_NetBDFTPSP = checkTree.NetBDFTPP + checkTree.NetBDFTSP;

                        //  now need original tree data
                        TreeCalculatedValuesDO origTree = originalCalculatedTrees.Find(
                            delegate (TreeCalculatedValuesDO tcv)
                            {
                                return tcv.Tree_CN == ct.Tree_CN;
                            });

                        if (origTree != null)
                        {
                            checkResult.R_GrossCUFTPP = origTree.GrossCUFTPP;
                            checkResult.R_NetCUFTPP = origTree.NetCUFTPP;
                            checkResult.R_GrossBDFTPP = origTree.GrossBDFTPP;
                            checkResult.R_NetBDFTPP = origTree.NetBDFTPP;

                            checkResult.R_GrossCUFTSP = origTree.GrossCUFTSP;
                            checkResult.R_NetCUFTSP = origTree.NetCUFTSP;
                            checkResult.R_GrossBDFTSP = origTree.GrossBDFTSP;
                            checkResult.R_NetBDFTSP = origTree.NetBDFTSP;

                            checkResult.R_GrossCUFTPSP = origTree.GrossCUFTPP + origTree.GrossCUFTSP;
                            checkResult.R_NetCUFTPSP = origTree.NetCUFTPP + origTree.NetCUFTSP;
                            checkResult.R_GrossBDFTPSP = origTree.GrossBDFTPP + origTree.GrossBDFTSP;
                            checkResult.R_NetBDFTPSP = origTree.NetBDFTPP + origTree.NetBDFTSP;

                            //  add check cruiser inititals to results table
                            checkResult.R_MarkerInitials = origTree.Tree.Initials;
                        }   //  endif
                        //  and the include volume flag so volume differences are calculated
                        checkResult.R_IncludeVol = (includeVolumes) ? 1 : 0;
                    }   //  endif nthRow
                }   //  endif measured tree
            }   //  end foreach loop
            return;
        }   //  end LoadVolume

        private void CheckInOutTrees(TolerancesList currTolerance, List<TreeDO> checkTrees, List<TreeDO> cruiserTrees, string checkCruiserInitials, IList<ResultsList> checkResults, IEnumerable<TreeDO> justStrataUnits)
        {
            //  NOTE -- tree-based methods are not evaluated for in/out trees since those trees usually fall into
            //  plot-based methods such as those checked for below.  the new system registers a tree count of 1 for each tree
            //  so the check on tree count is removed and simplifies this whole process.
            int nthRow;
            foreach (TreeDO td in checkTrees)
            {
                switch (td.Stratum.Method)
                {

                    case "FIX":
                    case "PNT":
                    case "3PPNT":
                    case "FIXCNT":
                    case "P3P":
                    case "F3P":
                    case "PCM":
                    case "FCM":
                        nthRow = cruiserTrees.FindIndex(
                            delegate (TreeDO t)
                            {
                                return t.Tree_CN == td.Tree_CN;
                            });
                        if (nthRow < 0)
                            updateResults(td, "in", 1, checkResults);
                        break;
                }   //  end switch on method
            }   //  end foreach loop

            //  then check original data against check cruise data for offsetting numbering using cutting unit and plot
            //  need distinct stratum and cutting unit otherwise get a lot of out trees for units not checked
            //  according to Craig B. need to include count records in the check
            string tempPlot;
            foreach (TreeDO jsu in justStrataUnits)
            {
                List<TreeDO> justUnits = cruiserTrees.FindAll(
                    delegate (TreeDO t)
                    {
                        return t.Stratum_CN == jsu.Stratum_CN && t.CuttingUnit_CN == jsu.CuttingUnit_CN;
                    });
                foreach (TreeDO ju in justUnits)
                {
                    switch (ju.Stratum.Method)
                    {
                        case "FIX":
                        case "PNT":
                        case "3PPNT":
                        case "FIXCNT":
                        case "P3P":
                        case "F3P":
                        case "PCM":
                        case "FCM":
                            nthRow = checkTrees.FindIndex(
                                delegate (TreeDO t)
                                {
                                    if (t.Plot == null)
                                        tempPlot = "";
                                    else tempPlot = t.Plot.PlotNumber.ToString();
                                    //  seems like the check cruiser initials aren't needed for this check but comment out the code
                                    //  in case this comes up later
                                    return t.Stratum.Code == ju.Stratum.Code && t.CuttingUnit.Code == ju.CuttingUnit.Code &&
                                           tempPlot == ju.Plot.PlotNumber.ToString() && t.Tree_CN == ju.Tree_CN;
                                });
                            if (nthRow < 0)
                                updateResults(ju, "out", 1, checkResults);
                            break;
                    }   //  end switch
                }   //  end foreach loop
            }   //  end for unique  unit
            return;
        }

        private static void checkR10logs(List<LogStockDO> checkTreeLogs, List<LogStockDO> cruiserTreeLogs,
                        TreeDO td, TolerancesList ct, string cruiserInitials, IList<ResultsList> checkResults)
        {
            if (ct.T_Element == "Log Grade")
            {
                //  check first log for exact match
                if (checkTreeLogs[0].Grade != cruiserTreeLogs[0].Grade)
                    updateLogChecks(td, checkTreeLogs[0], 1, 1, cruiserInitials, checkResults);

                //  continue checking rest of logs
                for (int k = 1; k < cruiserTreeLogs.Count; k++)
                {
                    //  convert to a number for comparison
                    int OC_gradeNumber = Convert.ToInt16(cruiserTreeLogs[k].Grade);
                    //  find log in check cruiser logs
                    int nthRow = checkTreeLogs.FindIndex(
                        delegate (LogStockDO ls)
                        {
                            return ls.LogNumber == cruiserTreeLogs[k].LogNumber;
                        });
                    if (nthRow >= 0)
                    {
                        //  convert to a number
                        int CC_gradeNumber = Convert.ToInt16(checkTreeLogs[nthRow].Grade);
                        if (CC_gradeNumber - OC_gradeNumber > 1)
                            updateLogChecks(td, checkTreeLogs[nthRow], 1, 1, cruiserInitials, checkResults);
                        else
                            updateLogChecks(td, checkTreeLogs[nthRow], 0, 1, cruiserInitials, checkResults);
                    }   //  endif
                }   //  end for k loop
            }
            else if (ct.T_Element == "Log Defect %")
            {
                //  check all logs for defect difference
                foreach (LogStockDO ctl in checkTreeLogs)
                {
                    float itemTolerance = (float)Convert.ToDouble(ct.T_Tolerance);
                    itemTolerance = ctl.SeenDefect * itemTolerance / 100;

                    //  find corresponding log in cruiser logs
                    int nthRow = cruiserTreeLogs.FindIndex(
                        delegate (LogStockDO ls)
                        {
                            return ls.LogNumber == ctl.LogNumber;
                        });
                    int itemDifference = Convert.ToInt16(ctl.SeenDefect - cruiserTreeLogs[nthRow].SeenDefect);
                    if (itemDifference >= itemTolerance)
                        updateLogChecks(td, ctl, 1, 2, cruiserInitials, checkResults);
                    else updateLogChecks(td, ctl, 0, 2, cruiserInitials, checkResults);
                }   //  end foreach loop

            }   //  endif
            return;
        }   //  end checkR10logs


        private static void checkR6logs(List<LogStockDO> checkTreeLogs, List<LogStockDO> cruiserTreeLogs,
                                TreeDO td, TolerancesList ct, string cruiserInitials, IList<ResultsList> checkResults)
        {
            int nthResult = 0;
            foreach (LogStockDO ctl in checkTreeLogs)
            {
                //  find corresponding log in cruiser logs
                int nthRow = cruiserTreeLogs.FindIndex(
                    delegate (LogStockDO ls)
                    {
                        return ls.LogNumber == ctl.LogNumber;
                    });

                if (nthRow >= 0)
                {
                    if (cruiserTreeLogs[nthRow].SeenDefect > 0 && ctl.SeenDefect > 0)
                    {
                        //  tolerance changes for this condition
                        nthResult = plusOrMinus(cruiserTreeLogs[nthRow].SeenDefect, ctl.SeenDefect, "15.0", ct.T_Units);
                    }
                    else nthResult = plusOrMinus(cruiserTreeLogs[nthRow].SeenDefect, ctl.SeenDefect, ct.T_Tolerance, ct.T_Units);

                    updateLogChecks(td, ctl, nthResult, 2, cruiserInitials, checkResults);
                }   //  endif nthRow
            }   //  end foreach loop
            return;
        }   //  end checkR6logs


        private static void updateLogChecks(TreeDO td, LogStockDO currLog, int nthResult, int whichType, string cruiserInitials, IList<ResultsList> checkResults)
        {
            //  find tree in results table
            //  if it doesn't exist, add it
            string currPlot;
            if (td.Plot == null)
                currPlot = "";
            else currPlot = td.Plot.PlotNumber.ToString();

            var checkResult = checkResults.FirstOrDefault(rr => rr.R_Stratum == td.Stratum.Code && rr.R_CuttingUnit == td.CuttingUnit.Code &&
                            rr.R_Plot == currPlot && rr.R_SampleGroup == td.SampleGroup.Code &&
                            rr.R_TreeNumber == td.TreeNumber.ToString() &&
                            rr.R_LogNumber == currLog.LogNumber);

            if (checkResult != null)
            {
                checkResult.R_LogNumber = currLog.LogNumber;
                if (whichType == 1)
                {
                    //  update log grade
                    checkResult.R_CC_LogGrade = currLog.Grade;
                    checkResult.R_LogGrade_R = nthResult;
                    //  need cruiser initials for reports
                    checkResult.R_MarkerInitials = cruiserInitials;
                }
                else if (whichType == 2)
                {
                    //  update log defect
                    checkResult.R_CC_LogSeenDef = currLog.SeenDefect;
                    checkResult.R_LogSeenDef_R = nthResult;
                    //  ditto above on initials
                    checkResult.R_MarkerInitials = cruiserInitials;
                }   //  endif
            }
            else
            {
                ResultsList rl = new ResultsList();
                rl.R_Stratum = td.Stratum.Code;
                rl.R_CuttingUnit = td.CuttingUnit.Code.ToString();
                if (td.Plot != null)
                    rl.R_Plot = td.Plot.PlotNumber.ToString();
                else rl.R_Plot = "";
                rl.R_SampleGroup = td.SampleGroup.Code;
                rl.R_TreeNumber = td.TreeNumber.ToString();
                rl.R_LogNumber = currLog.LogNumber;
                rl.R_MarkerInitials = cruiserInitials;
                if (whichType == 1)
                {
                    rl.R_CC_LogGrade = currLog.Grade;
                    rl.R_LogGrade_R = nthResult;
                }
                else if (whichType == 2)
                {
                    rl.R_CC_LogSeenDef = currLog.SeenDefect;
                    rl.R_LogSeenDef_R = nthResult;
                }   //  endif
                checkResults.Add(rl);
            }   //  endif
            return;
        }

        private static int plusOrMinus(float originalItem, float checkItem, string itemTolerance, string itemUnits)
        {
            //  should work for everything except seen defect % primary for Region 5
            //  Merch Height Primary, Merch Height Secondary
            //  Height to First Live Limb, Top FIB Primary, Recoverable %
            //  and Seen Defect % Secondary
            //  Log defect %
            float tolerancePlus = 0;
            float toleranceMinus = 0;

            //  convert tolerance to int
            int convertedTolerance = (int)Convert.ToDouble(itemTolerance);
            if (itemUnits == "percent" && checkItem > 0.0)
            {
                tolerancePlus = checkItem + (checkItem * (convertedTolerance / 100));
                toleranceMinus = checkItem - (checkItem * (convertedTolerance / 100));
            }
            else if (checkItem == 0.0)
            {
                tolerancePlus = convertedTolerance;
                toleranceMinus = 0;
            }   //  endif

            float itemDifference = checkItem - originalItem;
            if (itemUnits == "percent")
            {
                if (originalItem > tolerancePlus || originalItem < toleranceMinus)
                    return 1;
            }
            else if (Math.Abs(itemDifference) > convertedTolerance)
                return 1;

            return 0;
        }   //  end plusOrMinus


        //  overloaded plus or minus for Region 5 seen defect comparison
        private static int plusOrMinus(string itemTolerance, TreeCalculatedValuesDO cruiserTree, TreeCalculatedValuesDO checkTree)
        {
            //  calculates seen defect for Region 5 for comparison
            float checkItem = 0;
            float originalItem = 0;

            //  check item
            if (checkTree.GrossCUFTPP > 0)
                checkItem = ((checkTree.GrossCUFTPP - checkTree.NetCUFTPP) / checkTree.GrossCUFTPP) * 100;
            if (cruiserTree.GrossCUFTPP > 0)
                originalItem = ((cruiserTree.GrossCUFTPP - cruiserTree.NetCUFTPP) / cruiserTree.GrossCUFTPP) * 100;

            // TODO not really sure what is going on here is this value getting saved or retrieved elsewhere?
            // 
            //  need to store the item result for the summary report
            //  but original will need to be calculated when the summary report is created
            checkTree.Tree.SeenDefectPrimary = checkItem;

            int convertedTolerance = (int)Convert.ToDecimal(itemTolerance);
            float tolerPlus = checkItem + convertedTolerance;
            float tolerMinus = checkItem - convertedTolerance;
            if (Math.Abs(originalItem) > tolerPlus || Math.Abs(originalItem) < tolerMinus)
                return 1;

            return 0;
        }

        private static int DBHanalysis(TreeDO checkTree, TreeDO cruiserTree, TolerancesList currentTolerance)
        {
            //  DBH analysis
            float modifiedTolerance = 0;
            float tolerancePlus = 0;
            float toleranceMinus = 0;

            //  convert item tolerance from string to float
            float itemTolerance = (float)Convert.ToDouble(currentTolerance.T_Tolerance);

            float itemDifference = checkTree.DBH - cruiserTree.DBH;
            //  need to drop itemDifference to exactly one decimal place
            itemDifference = (float)Math.Round(itemDifference, 1);
            switch (currentTolerance.T_AddParam)
            {
                case "7% whichever >":
                    modifiedTolerance = (float)0.07 * checkTree.DBH;
                    if (modifiedTolerance > itemTolerance)
                        itemTolerance = modifiedTolerance;
                    //  itemTolerance = checkTree.DBH + modifiedTolerance;
                    break;
                case "3% whichever >":
                    modifiedTolerance = (float)0.03 * checkTree.DBH;
                    if (modifiedTolerance > itemTolerance)
                        itemTolerance = modifiedTolerance;
                    //  itemTolerance = checkTree.DBH + modifiedTolerance;  
                    break;
            }   //  end switch

            switch (currentTolerance.T_Units)
            {
                case "inches":
                    if (Math.Abs(itemDifference) > itemTolerance)
                        return 1;
                    break;
                case "percent":
                    tolerancePlus = checkTree.DBH + (checkTree.DBH * (itemTolerance / 100));
                    toleranceMinus = checkTree.DBH - (checkTree.DBH * (itemTolerance / 100));
                    if (cruiserTree.DBH > tolerancePlus || cruiserTree.DBH < toleranceMinus)
                        return 1;
                    break;
            }   //  end switch

            return 0;
        }


        private static int totalHeightAnalysis(TreeDO checkTree, TreeDO cruiserTree, TolerancesList currentTolerance)
        {
            //  total height conditions checked
            float itemDifference = 0;

            //  convert item tolerance from string to float
            float itemTolerance = (float)Convert.ToDouble(currentTolerance.T_Tolerance);


            switch (currentTolerance.T_Element)
            {
                case "Total Height <= 100 feet":
                    if (checkTree.TotalHeight <= 100)
                    {
                        if (currentTolerance.T_AddParam == "None")
                        {
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > itemTolerance)
                                return 1;
                        }
                        else if (currentTolerance.T_AddParam == "7% whichever >")
                        {
                            //  modify tolerance accordingly
                            double modifiedTolerance = 0.07 * checkTree.TotalHeight;
                            if (modifiedTolerance > itemTolerance) itemTolerance = (float)modifiedTolerance;
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > itemTolerance)
                                return 1;
                        }   //  endif
                    }   //  endif
                    break;
                case "Total Height > 100 feet":
                    if (checkTree.TotalHeight > 100)
                    {
                        if (currentTolerance.T_AddParam == "None")
                        {
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > itemTolerance)
                                return 1;
                        }
                        else if (currentTolerance.T_AddParam == "+1 per 25 feet > 100 feet")
                        {
                            float modifiedTolerance = ((checkTree.TotalHeight - 100) / 25) * 1 + itemTolerance;
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > modifiedTolerance)
                                return 1;
                        }   //  endif
                    }   //  endif
                    break;
                case "Total Height":
                    itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                    if (currentTolerance.T_AddParam == "None")
                    {
                        if (currentTolerance.T_Units == "feet")
                        {
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > itemTolerance)
                                return 1;
                        }
                        else if (currentTolerance.T_Units == "percent")
                        {
                            itemTolerance = itemTolerance / 100 * checkTree.TotalHeight;
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > itemTolerance)
                                return 1;
                        }   //  endif
                    }
                    else if (currentTolerance.T_AddParam == "+1 per 25 feet > 100 feet")
                    {
                        if (checkTree.TotalHeight > 100)
                        {
                            float modifiedTolerance = ((checkTree.TotalHeight - 100) / 25) * 1 + itemTolerance;
                            itemDifference = checkTree.TotalHeight - cruiserTree.TotalHeight;
                            if (Math.Abs(itemDifference) > modifiedTolerance)
                                return 1;
                        }   // endif tree height over 100
                    }   //  endif
                    break;
            }   //  end switch
            return 0;
        }

        private static int straightComparison(string originalItem, string checkItem)
        {
            //  this comparison will work for species, product, live/dead
            //  tree grade, log grade (except Region 10),  clear face, 
            if (originalItem != checkItem)
                return 1;
            else return 0;
        }

        private int straightComparison(float originalItem, float checkItem)
        {
            //  compares two floats
            //  used for form class
            if (originalItem != checkItem)
                return 1;
            else return 0;
        }   //  end straightComparison

        private void updateResults(TreeDO checkTree, string elementChecked, int iResult, IList<ResultsList> checkResults)
        {
            //  should work for every element except volume
            //  first find tree in results table
            //  check for null plot and change to empty
            string tempPlot;
            if (checkTree.Plot == null)
                tempPlot = "";
            else tempPlot = checkTree.Plot.PlotNumber.ToString();

            var checkResult = checkResults.FirstOrDefault(r => r.R_Stratum == checkTree.Stratum.Code && r.R_CuttingUnit == checkTree.CuttingUnit.Code &&
                        r.R_Plot == tempPlot && r.R_SampleGroup == checkTree.SampleGroup.Code &&
                        r.R_TreeNumber == checkTree.TreeNumber.ToString());

            if (checkResult != null)
            {
                //  update element checked
                switch (elementChecked)
                {
                    case "Species":
                        checkResult.R_CC_Species = checkTree.Species;
                        checkResult.R_Species_R = iResult;
                        break;
                    case "Live/Dead":
                        checkResult.R_CC_LiveDead = checkTree.LiveDead;
                        checkResult.R_LiveDead_R = iResult;
                        break;
                    case "Product":
                        checkResult.R_CC_Product = checkTree.SampleGroup.PrimaryProduct;
                        checkResult.R_Product_R = iResult;
                        break;
                    case "DBH":
                        checkResult.R_CC_DBHOB = checkTree.DBH;
                        checkResult.R_DBHOB_R = iResult;
                        break;
                    case "Total Height <= 100 feet":
                        checkResult.R_CC_TotHeightUnder = checkTree.TotalHeight;
                        checkResult.R_TotHeightUnder_R = iResult;
                        break;
                    case "Total Height > 100 feet":
                        checkResult.R_CC_TotHeightOver = checkTree.TotalHeight;
                        checkResult.R_TotHeightOver_R = iResult;
                        break;
                    case "Total Height":
                        checkResult.R_CC_TotalHeight = checkTree.TotalHeight;
                        checkResult.R_TotalHeight_R = iResult;
                        break;
                    case "Merch Height Primary":
                        checkResult.R_CC_MerchHgtPP = checkTree.MerchHeightPrimary;
                        checkResult.R_MerchHgtPP_R = iResult;
                        break;
                    case "Merch Height Secondary":
                        checkResult.R_CC_MerchHgtSP = checkTree.MerchHeightSecondary;
                        checkResult.R_MerchHgtSP_R = iResult;
                        break;
                    case "Height to First Live Limb":
                        checkResult.R_CC_HgtFLL = checkTree.HeightToFirstLiveLimb;
                        checkResult.R_HgtFLL_R = iResult;
                        break;
                    case "Top DIB Primary":
                        checkResult.R_CC_TopDIBPP = checkTree.TopDIBPrimary;
                        checkResult.R_TopDIBPP_R = iResult;
                        break;
                    case "Seen Defect % Primary":
                        checkResult.R_CC_SeenDefPP = checkTree.SeenDefectPrimary;
                        checkResult.R_SeenDefPP_R = iResult;
                        break;
                    case "Seen Defect % Secondary":
                        checkResult.R_CC_SeenDefSP = checkTree.SeenDefectSecondary;
                        checkResult.R_SeenDefSP_R = iResult;
                        break;
                    case "Recoverable %":
                        checkResult.R_CC_RecDef = checkTree.RecoverablePrimary;
                        checkResult.R_RecDef_R = iResult;
                        break;
                    case "Form class":
                        checkResult.R_CC_FormClass = (int)checkTree.FormClass;
                        checkResult.R_FormClass_R = iResult;
                        break;
                    case "Clear Face":
                        checkResult.R_CC_Clear = checkTree.ClearFace;
                        checkResult.R_Clear_R = iResult;
                        break;
                    case "Tree Grade":
                        checkResult.R_CC_TreeGrade = checkTree.Grade;
                        checkResult.R_TreeGrade_R = iResult;
                        break;
                    case "in":
                        checkResult.R_InResult = iResult;
                        checkResult.R_TreeSpecies = checkTree.Species;
                        checkResult.R_TreeProduct = checkTree.SampleGroup.PrimaryProduct;
                        break;
                    case "out":
                        checkResult.R_OutResult = iResult;
                        checkResult.R_CountMeasure = checkTree.CountOrMeasure;
                        checkResult.R_TreeSpecies = checkTree.Species;
                        checkResult.R_TreeProduct = checkTree.SampleGroup.PrimaryProduct;
                        break;
                }   //  end switch on element checked
            }
            else
            {
                //  this probably only works for in/out trees
                //  when cruiser calls a tree in but check cruiser calls it out
                //  means the tree is not in the results table
                ResultsList rl = new ResultsList();
                rl.R_Stratum = checkTree.Stratum.Code;
                rl.R_CuttingUnit = checkTree.CuttingUnit.Code;
                if (checkTree.Plot != null)
                    rl.R_Plot = checkTree.Plot.PlotNumber.ToString();
                else rl.R_Plot = " ";
                rl.R_SampleGroup = checkTree.SampleGroup.Code;
                rl.R_TreeNumber = checkTree.TreeNumber.ToString();
                rl.R_CountMeasure = checkTree.CountOrMeasure;
                rl.R_TreeSpecies = checkTree.Species;
                rl.R_TreeProduct = checkTree.SampleGroup.PrimaryProduct;
                rl.R_LogNumber = "0";
                switch (elementChecked)
                {
                    case "in":
                        rl.R_InResult = iResult;
                        break;
                    case "out":
                        rl.R_OutResult = iResult;
                        break;
                }   //  end switch
                checkResults.Add(rl);
            }   //  endif on nthRow
            return;
        }

        private List<ResultsList> fillResultsTable(IEnumerable<TreeDO> checkTrees)
        {
            var checkResults = new List<ResultsList>();
            foreach (TreeDO ct in checkTrees)
            {
                ResultsList rl = new ResultsList();
                rl.R_Stratum = ct.Stratum.Code;
                rl.R_CuttingUnit = ct.CuttingUnit.Code;
                if (ct.Plot != null)
                    rl.R_Plot = ct.Plot.PlotNumber.ToString();
                else rl.R_Plot = "";
                rl.R_SampleGroup = ct.SampleGroup.Code;
                rl.R_CountMeasure = ct.CountOrMeasure;
                rl.R_TreeNumber = ct.TreeNumber.ToString();
                rl.R_LogNumber = "";
                rl.R_CC_Species = "";
                rl.R_CC_LiveDead = "";
                rl.R_CC_Product = "";
                rl.R_CC_Clear = "";
                rl.R_CC_TreeGrade = "";
                rl.R_CC_LogGrade = "";
                rl.R_TreeSpecies = "";
                rl.R_TreeProduct = "";
                rl.R_MarkerInitials = "";

                checkResults.Add(rl);
            }   //  end foreach loop
            return checkResults;
        }   //  end fillResultsTable
    }
}
