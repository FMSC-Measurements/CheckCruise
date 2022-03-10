using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using CruiseDAL.DataObjects;

namespace CheckCruise
{
    public class dataBaseCommands
    {
        #region
        public string fileName;
        public CruiseDAL.DAL DAL;
        public string checkCruiseFilename;
        StringBuilder sb = new StringBuilder();
        private List<TreeDO> checkTrees = new List<TreeDO>();
        #endregion


        public bool doesTableExist(string tableName)
        {
            //  make sure check cruise filename is complete
            checkCruiseFilename = checkFileName(checkCruiseFilename);

            //  find table in check cruise database
            using (SQLiteConnection sqlconn = new SQLiteConnection(checkCruiseFilename))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();
                sb.Remove(0, sb.Length);
                sb.Append("SELECT name FROM sqlite_master WHERE type='table' AND name='");
                sb.Append(tableName);
                sb.Append("';");

                sqlcmd.CommandText = sb.ToString();
                sqlcmd.ExecuteNonQuery();
                SQLiteDataReader sdrReader = sqlcmd.ExecuteReader();
                if (sdrReader.HasRows)
                {
                    string currentState = sqlconn.State.ToString();
                    sqlconn.Close();
                    currentState = sqlconn.State.ToString();
                    return true;
                }
                else
                {
                    sqlconn.Close();
                    return false;
                }
            }   //  end using
        }   //  end doesTableExist


        public void createNewTable(string tableName)
        {
            //make sure check cruise filename is complete
            checkCruiseFilename = checkFileName(checkCruiseFilename);

            //  build query for new table
            string[] valuesList;
            string queryString = "";
            if (tableName == "Tolerances")
            {
                valuesList = createTolerancesList();
                queryString = createNewTableQuery(tableName, valuesList);
            }
            else if (tableName == "Results")
            {
                valuesList = createResultsList();
                queryString = createNewTableQuery(tableName, valuesList);
            }


            using(SQLiteConnection sqlconn = new SQLiteConnection(checkCruiseFilename))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = queryString;
                sqlcmd.ExecuteNonQuery();

                sqlconn.Close();
            }   //  end using
            return;
        }   //  end createNewTable


        public string createNewTableQuery(string tableName, string[] valuesList)
        {
            sb.Remove(0, sb.Length);
            sb.Append("CREATE TABLE ");
            sb.Append(tableName);
            sb.Append(" (");

            //  add values to string
            foreach (string s in valuesList)
                sb.Append(s);
            //  add close parens and semi colon
            sb.Append(");");
            return sb.ToString();
        }   //  end createNewTableQuery


        public string[] createTolerancesList()
        {
            string[] tolerancesList = new string[23] {"T_Element TEXT,",
                                                      "T_Tolerance TEXT,",
                                                      "T_Units TEXT,",
                                                      "T_AddParam TEXT,",
                                                      "T_Weight REAL,",
                                                      "T_CUFTPGtolerance REAL,",
                                                      "T_CUFTPNtolerance REAL,",
                                                      "T_CUFTSGtolerance REAL,",
                                                      "T_CUFTSNtolerance REAL,",
                                                      "T_CUFTPSGtolerance REAL,",
                                                      "T_CUFTPSNtolerance REAL,",
                                                      "T_BDFTPGtolerance REAL,",
                                                      "T_BDFTPNtolerance REAL,",
                                                      "T_BDFTSGtolerance REAL,",
                                                      "T_BDFTSNtolerance REAL,",
                                                      "T_BDFTPSGtolerance REAL,",
                                                      "T_BDFTPSNtolerance REAL,",
                                                      "T_IncludeVolume INT,",
                                                      "T_BySpecies INT,",
                                                      "T_ByProduct INT,",
                                                      "T_ElementAccuracy REAL,",
                                                      "T_OverallAccuracy REAL,",
                                                      "T_DateStamp TEXT"};
            return tolerancesList;
        }   //  end createTolerancesList


        public string[] createResultsList()
        {
            string[] resultsList = new string[75] {"R_Stratum TEXT,",
                                                   "R_CuttingUnit TEXT,",
                                                   "R_Plot TEXT,",
                                                   "R_SampleGroup TEXT,",
                                                   "R_CountMeasure TEXT,",
                                                   "R_TreeNumber TEXT,",
                                                   "R_LogNumber TEXT,",
                                                   "R_CC_Species TEXT,",
                                                   "R_Species_R INT,",
                                                   "R_CC_LiveDead TEXT,",
                                                   "R_LiveDead_R INT,",
                                                   "R_CC_Product TEXT,",
                                                   "R_Product_R INT,",
                                                   "R_CC_DBHOB REAL,",
                                                   "R_DBHOB_R INT,",
                                                   "R_CC_TotalHeight REAL,",
                                                   "R_TotalHeight_R INT,",
                                                   "R_CC_TotHeightUnder REAL,",
                                                   "R_TotHeightUnder_R INT,",
                                                   "R_CC_TotHeightOver REAL,",
                                                   "R_TotHeightOver_R INT,",
                                                   "R_CC_MerchHgtPP REAL,",
                                                   "R_MerchHgtPP_R INT,",
                                                   "R_CC_MerchHgtSP REAL,",
                                                   "R_MerchHgtSP_R INT,",
                                                   "R_CC_HgtFLL REAL,",
                                                   "R_HgtFLL_R INT,",
                                                   "R_CC_SeenDefPP REAL,",
                                                   "R_SeenDefPP_R INT,",
                                                   "R_CC_SeenDefSP REAL,",
                                                   "R_SeenDefSP_R INT,",
                                                   "R_CC_RecDef REAL,",
                                                   "R_RecDef_R INT,",
                                                   "R_CC_TopDIBPP INT,",
                                                   "R_TopDIBPP_R INT,",
                                                   "R_CC_FormClass INT,",
                                                   "R_FormClass_R INT,",
                                                   "R_CC_Clear TEXT,",
                                                   "R_Clear_R INT,",
                                                   "R_CC_TreeGrade TEXT,",
                                                   "R_TreeGrade_R INT,",
                                                   "R_CC_LogGrade TEXT,",
                                                   "R_LogGrade_R INT,",
                                                   "R_CC_LogSeenDef REAL,",
                                                   "R_LogSeenDef_R INT,",
                                                   "R_IncludeVol INT,",
                                                   "R_TreeSpecies TEXT,",
                                                   "R_TreeProduct TEXT,",
                                                   "R_MarkerInitials TEXT,",
                                                   "R_CC_GrossCUFTPP REAL,",
                                                   "R_GrossCUFTPP REAL,",
                                                   "R_CC_NetCUFTPP REAL,",
                                                   "R_NetCUFTPP REAL,",
                                                   "R_CC_GrossCUFTSP REAL,",
                                                   "R_GrossCUFTSP REAL,",
                                                   "R_CC_NetCUFTSP REAL,",
                                                   "R_NetCUFTSP REAL,",
                                                   "R_CC_GrossCUFTPSP REAL,",
                                                   "R_GrossCUFTPSP REAL,",
                                                   "R_CC_NetCUFTPSP REAL,",
                                                   "R_NetCUFTPSP REAL,",
                                                   "R_CC_GrossBDFTPP REAL,",
                                                   "R_GrossBDFTPP REAL,",
                                                   "R_CC_NetBDFTPP REAL,",
                                                   "R_NetBDFTPP REAL,",
                                                   "R_CC_GrossBDFTSP REAL,",
                                                   "R_GrossBDFTSP REAL,",
                                                   "R_CC_NetBDFTSP REAL,",
                                                   "R_NetBDFTSP REAL,",
                                                   "R_CC_GrossBDFTPSP REAL,",
                                                   "R_GrossBDFTPSP REAL,",
                                                   "R_CC_NetBDFTPSP REAL,",
                                                   "R_NetBDFTPSP REAL,",
                                                   "R_InResult INT,",
                                                   "R_OutResult INT"};
            return resultsList;
        }   //  end createResultsList


        public List<SaleDO> getSale()
        {
            return DAL.Read<SaleDO>("Sale", null, null);
        }   //  end getSale

        public List<StratumDO> getStrata()
        {
            return DAL.Read<StratumDO>("Stratum", "ORDER BY Code", null);
        }   //  end getStrata

        public List<CuttingUnitDO> getUnits()
        {
            return DAL.Read<CuttingUnitDO>("CuttingUnit", null, null);
        }   //  end getUnits

        public List<LogStockDO> getLogStock()
        {
            return DAL.Read<LogStockDO>("LogStock", null, null);
        }   //  end getLogs

        public List<LogDO> getLogs()
        {
            return DAL.Read<LogDO>("Log", null, null);
        }   //  end getLogs

        public List<TreeDO> getTrees()
        {
            return DAL.Read<TreeDO>("Tree", null, null);
        }   //  end getTrees


        public List<TreeCalculatedValuesDO> getCalculatedTrees()
        {
            return DAL.Read<TreeCalculatedValuesDO>("TreeCalculatedValues", null, null);
        }   //  end getCalculatedTrees



        public List<TreeCalculatedValuesDO> getCalculatedTrees(string cruiseInit)
        {
            sb.Remove(0, sb.Length);
            sb.Append("JOIN Tree WHERE Tree.Tree_CN = TreeCalculatedValues.Tree_CN");
            sb.Append(" AND Initials = ?");
            return DAL.Read<TreeCalculatedValuesDO>("TreeCalculatedValues", sb.ToString(), cruiseInit);
        }   //  end getCalcTrees


        public List<TreeDO> getTrees(string cruiseInit)
        {
            return DAL.Read<TreeDO>("Tree", "WHERE Initials = ?", cruiseInit);
        }   //  end getTrees by cruiser


        public string getCheckCruiserInitials()
        {
            List<GlobalsDO> globList = DAL.Read<GlobalsDO>("Globals", null, null);
            int nthRow = globList.FindIndex(
                delegate(GlobalsDO g)
                {
                    return g.Block == "CheckCruise" && g.Key == "Check Cruiser Initials";
                });
            return globList[nthRow].Value.ToString();
        }   //  end getCheckCruiserInitials


        public void saveTrees(List<TreeDO> tList)
        {
            foreach (TreeDO tdo in tList)
            {
                if (tdo.DAL == null)
                    tdo.DAL = DAL;
                tdo.Save();
            }   //  end foreach loop
            return;
        }   //  end saveTrees

        
        public List<StratumDO> getCurrentStratum(string stratumCode)
        {
            return DAL.Read<StratumDO>("Stratum", "WHERE Code = ?", stratumCode);
        }   //  end getCurrentStratum

        
        public ArrayList getCruiserInitials()
        {
            //  pull cruiser initials from results table
            ArrayList justInitials = new ArrayList();
            //  check filename
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "SELECT DISTINCT R_MarkerInitials FROM Results";
                sqlcmd.ExecuteNonQuery();

                SQLiteDataReader sdrReader = sqlcmd.ExecuteReader();
                if (sdrReader.HasRows)
                {
                    while (sdrReader.Read())
                    {
                        string temp = sdrReader.GetString(0);
                        if(temp != "")
                            justInitials.Add(temp);
                    }   //  end read
                }   //  endif hasRows
                sqlconn.Close();
            }   //  end using
            return justInitials;
        }   //  end getCruiserInitials


        public void deleteUnit(string unitToDelete, string stratumToDelete)
        {
            //  delete specific unit not used from tree and ;og tables
            //  need CuttingUnit_CN to complete the delete
            long currUnit_CN = 0;
            long currStratum_CN = 0;
            List<StratumDO> strList = getCurrentStratum(stratumToDelete);
            foreach (StratumDO s in strList)
            {
                s.CuttingUnits.Populate();
                currStratum_CN = (long) s.Stratum_CN;
                foreach (CuttingUnitDO c in s.CuttingUnits)
                {
                    if (c.Code == unitToDelete)
                    {
                        currUnit_CN = (long)c.CuttingUnit_CN;
                        break;
                    }   //  endif
                }   //  end foreach loop
            }   //  end foreach loop            

            //  check filename
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();

                //  Tree table is a little more tricky since any associated logs will need to be deleted
                //  so pull log table first to see if there are log records to delete
                List<LogDO> justLogs = getLogs();
                if(justLogs.Count > 0)
                {
                    List<TreeDO> unitTrees = getTrees();
                    List<TreeDO> justUnit = unitTrees.FindAll(
                        delegate(TreeDO t)
                        {
                            return t.Stratum_CN == currStratum_CN && t.CuttingUnit_CN == currUnit_CN;
                        });
                    //  delete log records for each tree
                    foreach(TreeDO ju in justUnit)
                    {
                        sb.Remove(0,sb.Length);
                        sb.Append("DELETE FROM Log WHERE Tree_CN = ");
                        sb.Append(ju.Tree_CN);
                        sqlcmd.CommandText = sb.ToString();
                        sqlcmd.ExecuteNonQuery();
                    }   //  end foreach
                }   //  endif
                
                //  Finally all trees for the unit to delete
                sb.Remove(0,sb.Length);
                sb.Append("DELETE FROM Tree WHERE Stratum_CN = ");
                sb.Append(currStratum_CN);
                sb.Append(" AND CuttingUnit_CN = ");
                sb.Append(currUnit_CN);
                sqlcmd.CommandText = sb.ToString();
                int nRows = sqlcmd.ExecuteNonQuery();

                //  DONE!!

                sqlconn.Close();
            }   //  end using

            return;
        }   //  end deleteUnit


        public void deleteTreeCalculatedValues()
        {
            //  the following code is what Ben Campbell suggested but it runs REALLY SLOW
            //  Switched back to my method which runs faster
            //List<TreeCalculatedValuesDO> tcvList = getTreeCalculatedValues();
            //foreach (TreeCalculatedValuesDO tcvdo in tcvList)
            //    tcvdo.Delete();

            //  make sure filename is complete
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            //   open connection and delete data
            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                //  open connection
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();

                //  delete all rows
                sqlcmd.CommandText = "DELETE FROM TreeCalculatedValues WHERE TreeCalcValues_CN>0";
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();
            }   //  end using
            return;
        }   //  end deleteTreeCalculatedValues


        public void deleteLogStock()
        {
            //  see note above concerning this code

            //  make sure filename is complete
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            //   open connection and delete data
            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                //  open connection
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();

                //  delete all rows
                sqlcmd.CommandText = "DELETE FROM LogStock WHERE LogStock_CN>0";
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();
            }   //  end using
            return;
        }   //  end deleteLogStock


        public string getRegion()
        {
            List<SaleDO> saleList = getSale();
            return saleList[0].Region;
        }   //  end getRegion


        public string getSaleName()
        {
            List<SaleDO> saleList = getSale();
            return saleList[0].Name;
        }   //  end getSaleName


        public void clearLogTable()
        {
            List<LogDO> logList = getLogs();
            if (logList.Count > 0)
            {
                foreach (LogDO l in logList)
                    l.Delete();
            }   //    endif
        }   //  end clearLogTable


        public void saveInitials(string checkCruiseInititals)
        {
            List<GlobalsDO> globList = DAL.Read<GlobalsDO>("Globals", null, null);
            GlobalsDO g = new GlobalsDO();
            g.Block = "CheckCruise";
            g.Key = "Check Cruiser Initials";
            g.Value = checkCruiseInititals;
            globList.Add(g);
            foreach (GlobalsDO gdo in globList)
            {
                if (gdo.DAL == null)
                    gdo.DAL = DAL;
                gdo.Save();
            }   //  end foreach loop
            return;
        }   //  end saveInititals


        public void updateTreeMeasurements()
        {
            sb.Remove(0, sb.Length);
            //  This saves the updated tree list
            sb.Append("INSERT OR REPLACE INTO Tree(Tree_CN,CuttingUnit_CN,Stratum_CN,Plot_CN,TreeDefaultValue_CN,SampleGroup_CN,TreeNumber,Species,CountOrMeasure,CreatedBy,CreatedDate)");
            sb.Append(" SELECT Tree_CN,CuttingUnit_CN,Stratum_CN,Plot_CN,TreeDefaultValue_CN,SampleGroup_CN,TreeNumber,Species,CountOrMeasure,CreatedBy,CreatedDate FROM Tree;");
            DAL.Execute(sb.ToString());
            return;
        }   //  end saveUpdatedTrees


        public List<TreeDO> getStrataUnit()
        {
            return DAL.Read<TreeDO>("Tree", "GROUP BY Stratum_CN,CuttingUnit_CN", null);
        }   //  end getStrataUnit


        public List<TolerancesList> getTolerances()
        {
            List<TolerancesList> returnList = new List<TolerancesList>();
            //  make sure filename is complete
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();

                sb.Remove(0, sb.Length);
                sb.Append("SELECT * FROM Tolerances");
                sqlcmd.CommandText = sb.ToString();
                sqlcmd.ExecuteNonQuery();

                SQLiteDataReader sdrReader = sqlcmd.ExecuteReader();
                if (sdrReader.HasRows)
                {
                    while (sdrReader.Read())
                    {
                        TolerancesList tl = new TolerancesList();
                        tl.T_Element = sdrReader.GetString(0);
                        tl.T_Tolerance = sdrReader.GetString(1);
                        tl.T_Units = sdrReader.GetString(2);
                        tl.T_AddParam = sdrReader.GetString(3);
                        tl.T_Weight = sdrReader.GetFloat(4);
                        tl.T_CUFTPGtolerance = sdrReader.GetFloat(5);
                        tl.T_CUFTPNtolerance = sdrReader.GetFloat(6);
                        tl.T_CUFTSGtolerance = sdrReader.GetFloat(7);
                        tl.T_CUFTSNtolerance = sdrReader.GetFloat(8);
                        tl.T_CUFTPSGtolerance = sdrReader.GetFloat(9);
                        tl.T_CUFTPSNtolerance = sdrReader.GetFloat(10);
                        tl.T_BDFTPGtolerance = sdrReader.GetFloat(11);
                        tl.T_BDFTPNtolerance = sdrReader.GetFloat(12);
                        tl.T_BDFTSGtolerance = sdrReader.GetFloat(13);
                        tl.T_BDFTSNtolerance = sdrReader.GetFloat(14);
                        tl.T_BDFTPSGtolerance = sdrReader.GetFloat(15);
                        tl.T_BDFTPSNtolerance = sdrReader.GetFloat(16);
                        tl.T_IncludeVolume = sdrReader.GetInt16(17);
                        tl.T_BySpecies = sdrReader.GetInt16(18);
                        tl.T_ByProduct = sdrReader.GetInt16(19);
                        tl.T_ElementAccuracy = sdrReader.GetFloat(20);
                        tl.T_OverallAccuracy = sdrReader.GetFloat(21);
                        tl.T_DateStamp = sdrReader.GetString(22);
                        returnList.Add(tl);
                    }   // end while read
                }   //  endif has rows
                sqlconn.Close();
            }   //  end using
            return returnList;
        }   //  end getTolerances


        public void saveTolerances(List<TolerancesList> currentList)
        {

            checkCruiseFilename = checkFileName(checkCruiseFilename);
            using (SQLiteConnection sqlconn = new SQLiteConnection(checkCruiseFilename))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();
                //  firest remove all records in the table
                sb.Remove(0, sb.Length);
                sb.Append("DELETE FROM Tolerances");
                sqlcmd.CommandText = sb.ToString();
                sqlcmd.ExecuteNonQuery();

                //  then replace records with updated tolerances list
                foreach (TolerancesList tl in currentList)
                {
                    sb.Remove(0, sb.Length);
                    sb.Append("INSERT OR REPLACE INTO Tolerances(T_Element,T_Tolerance,T_Units,T_AddParam,T_Weight,");
                    sb.Append("T_CUFTPGtolerance,T_CUFTPNtolerance,T_CUFTSGtolerance,T_CUFTSNtolerance,T_CUFTPSGtolerance,T_CUFTPSNtolerance,");
                    sb.Append("T_BDFTPGtolerance,T_BDFTPNtolerance,T_BDFTSGtolerance,T_BDFTSNtolerance,T_BDFTPSGtolerance,T_BDFTPSNtolerance,");
                    sb.Append("T_IncludeVolume,T_BySpecies,T_ByProduct,T_ElementAccuracy,T_OverallAccuracy,T_DateStamp) VALUES ('");
                    sb.Append(tl.T_Element);
                    sb.Append("','");
                    sb.Append(tl.T_Tolerance);
                    sb.Append("','");
                    sb.Append(tl.T_Units);
                    sb.Append("','");
                    sb.Append(tl.T_AddParam);
                    sb.Append("','");
                    sb.Append(tl.T_Weight);
                    sb.Append("','");
                    sb.Append(tl.T_CUFTPGtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_CUFTPNtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_CUFTSGtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_CUFTSNtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_CUFTPSGtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_CUFTPSNtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_BDFTPGtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_BDFTPNtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_BDFTSGtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_BDFTSNtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_BDFTPSGtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_BDFTPSNtolerance);
                    sb.Append("','");
                    sb.Append(tl.T_IncludeVolume);
                    sb.Append("','");
                    sb.Append(tl.T_BySpecies);
                    sb.Append("','");
                    sb.Append(tl.T_ByProduct);
                    sb.Append("','");
                    sb.Append(tl.T_ElementAccuracy);
                    sb.Append("','");
                    sb.Append(tl.T_OverallAccuracy);
                    sb.Append("','");
                    sb.Append(tl.T_DateStamp);
                    sb.Append("');");

                    sqlcmd.CommandText = sb.ToString();
                    sqlcmd.ExecuteNonQuery();
                }   //  end foreach loop
                sqlconn.Close();
            }   //  end using
            return;
        }   //  end saveTolerances


        public List<ResultsList> getResultsTable(string groupToOrder, string whereBy)
        {
            List<ResultsList> resultsTable = new List<ResultsList>();
            //  make sure check cruise filename is complate
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            using(SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();

                sb.Remove(0,sb.Length);
                sb.Append("SELECT * FROM Results");
                if (groupToOrder != "")
                {
                    sb.Append(" GROUP BY ");
                    sb.Append(groupToOrder);
                }
                else if(whereBy != "")
                {
                    sb.Append(" WHERE R_MarkerInitials = '");
                    sb.Append(whereBy);
                    sb.Append("';");
                }   //  endif
                sqlcmd.CommandText = sb.ToString();
                sqlcmd.ExecuteNonQuery();

                SQLiteDataReader sdrReader = sqlcmd.ExecuteReader();
                if(sdrReader.HasRows)
                {
                    while(sdrReader.Read())
                    {
                        ResultsList rl = new ResultsList();
                        rl.R_Stratum = sdrReader.GetString(0);
                        rl.R_CuttingUnit = sdrReader.GetString(1);
                        rl.R_Plot = sdrReader.GetString(2);
                        rl.R_SampleGroup = sdrReader.GetString(3);
                        rl.R_CountMeasure = sdrReader.GetString(4);
                        rl.R_TreeNumber = sdrReader.GetString(5);
                        rl.R_LogNumber = sdrReader.GetString(6);
                        rl.R_CC_Species = sdrReader.GetString(7);
                        rl.R_Species_R = sdrReader.GetInt16(8);
                        rl.R_CC_LiveDead = sdrReader.GetString(9);
                        rl.R_LiveDead_R =sdrReader.GetInt16(10);
                        rl.R_CC_Product = sdrReader.GetString(11);
                        rl.R_Product_R = sdrReader.GetInt16(12);
                        rl.R_CC_DBHOB = sdrReader.GetFloat(13);
                        rl.R_DBHOB_R = sdrReader.GetInt16(14);
                        rl.R_CC_TotalHeight = sdrReader.GetFloat(15);
                        rl.R_TotalHeight_R = sdrReader.GetInt16(16);
                        rl.R_CC_TotHeightUnder = sdrReader.GetFloat(17);
                        rl.R_TotHeightUnder_R = sdrReader.GetInt16(18);
                        rl.R_CC_TotHeightOver = sdrReader.GetFloat(19);
                        rl.R_TotHeightOver_R = sdrReader.GetInt16(20);
                        rl.R_CC_MerchHgtPP = sdrReader.GetFloat(21);
                        rl.R_MerchHgtPP_R = sdrReader.GetInt16(22);
                        rl.R_CC_MerchHgtSP = sdrReader.GetFloat(23);
                        rl.R_MerchHgtSP_R = sdrReader.GetInt16(24);
                        rl.R_CC_HgtFLL = sdrReader.GetFloat(25);
                        rl.R_HgtFLL_R = sdrReader.GetInt16(26);
                        rl.R_CC_SeenDefPP = sdrReader.GetFloat(27);
                        rl.R_SeenDefPP_R = sdrReader.GetInt16(28);
                        rl.R_CC_SeenDefSP = sdrReader.GetFloat(29);
                        rl.R_SeenDefSP_R = sdrReader.GetInt16(30);
                        rl.R_CC_RecDef = sdrReader.GetFloat(31);
                        rl.R_RecDef_R = sdrReader.GetInt16(32);
                        rl.R_CC_TopDIBPP = sdrReader.GetFloat(33);
                        rl.R_TopDIBPP_R  = sdrReader.GetInt16(34);
                        rl.R_CC_FormClass  = sdrReader.GetInt16(35);
                        rl.R_FormClass_R = sdrReader.GetInt16(36);
                        rl.R_CC_Clear = sdrReader.GetString(37);
                        rl.R_Clear_R = sdrReader.GetInt16(38);
                        rl.R_CC_TreeGrade = sdrReader.GetString(39);
                        rl.R_TreeGrade_R = sdrReader.GetInt16(40);
                        rl.R_CC_LogGrade = sdrReader.GetString(41);
                        rl.R_LogGrade_R = sdrReader.GetInt16(42);
                        rl.R_CC_LogSeenDef = sdrReader.GetFloat(43);
                        rl.R_LogSeenDef_R = sdrReader.GetInt16(44);
                        rl.R_IncludeVol = sdrReader.GetInt16(45);
                        rl.R_TreeSpecies = sdrReader.GetString(46);
                        rl.R_TreeProduct = sdrReader.GetString(47);
                        rl.R_MarkerInitials = sdrReader.GetString(48);
                        rl.R_CC_GrossCUFTPP = sdrReader.GetDouble(49);
                        rl.R_GrossCUFTPP = sdrReader.GetDouble(50);
                        rl.R_CC_NetCUFTPP = sdrReader.GetDouble(51);
                        rl.R_NetCUFTPP = sdrReader.GetDouble(52);
                        rl.R_CC_GrossCUFTSP  = sdrReader.GetDouble(53);
                        rl.R_GrossCUFTSP = sdrReader.GetDouble(54);
                        rl.R_CC_NetCUFTSP = sdrReader.GetDouble(55);
                        rl.R_NetCUFTSP = sdrReader.GetDouble(56);
                        rl.R_CC_GrossCUFTPSP = sdrReader.GetDouble(57);
                        rl.R_GrossCUFTPSP = sdrReader.GetDouble(58);
                        rl.R_CC_NetCUFTPSP = sdrReader.GetDouble(59);
                        rl.R_NetCUFTPSP = sdrReader.GetDouble(60);
                        rl.R_CC_GrossBDFTPP = sdrReader.GetDouble(61);
                        rl.R_GrossBDFTPP = sdrReader.GetDouble(62);
                        rl.R_CC_NetBDFTPP = sdrReader.GetDouble(63);
                        rl.R_NetBDFTPP = sdrReader.GetDouble(64);
                        rl.R_CC_GrossBDFTSP = sdrReader.GetDouble(65);
                        rl.R_GrossBDFTSP = sdrReader.GetDouble(66);
                        rl.R_CC_NetBDFTSP = sdrReader.GetDouble(67);
                        rl.R_NetBDFTSP = sdrReader.GetDouble(68);
                        rl.R_CC_GrossBDFTPSP = sdrReader.GetDouble(69);
                        rl.R_GrossBDFTPSP = sdrReader.GetDouble(70);
                        rl.R_CC_NetBDFTPSP = sdrReader.GetDouble(71);
                        rl.R_NetBDFTPSP = sdrReader.GetDouble(72);
                        rl.R_InResult = sdrReader.GetInt16(73);
                        rl.R_OutResult = sdrReader.GetInt16(74);
                        resultsTable.Add(rl);
                    }   //  end while read
                }   //  endif reader has rows
                sqlconn.Close();
            }   //  end using

            return resultsTable;
        }   //  end getResults



        public void clearResults()
        {
            //  make sure file is complete
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();
                //  remove all records from the results table
                sqlcmd.CommandText = "DELETE FROM Results";
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();
            }   //  end using

            return;
        }   // end clearResults
        

        public void saveResults(List<ResultsList> checkResults)
        {
            //  make sure check cruise filename is complete
            string completeFileName = checkCruiseFilename;
            completeFileName = checkFileName(completeFileName);

            //  clear out the results table first.  when volumes get updated
            //  INSERT OR REPLACE doesn't work properly.  Just inserts because
            //  volume was added
            clearResults();

            using (SQLiteConnection sqlconn = new SQLiteConnection(completeFileName))
            {
                sqlconn.Open();
                SQLiteCommand sqlcmd = sqlconn.CreateCommand();
                //  replace or insert records with updated results list
                foreach (ResultsList cr in checkResults
                    )
                {
                    sb.Remove(0, sb.Length);
                    sb.Append("INSERT OR REPLACE INTO Results(R_Stratum,R_CuttingUnit,R_Plot,R_SampleGroup,R_CountMeasure,R_TreeNumber,R_LogNumber,"); 
                    sb.Append("R_CC_Species,R_Species_R,R_CC_LiveDead,R_LiveDead_R,R_CC_Product,R_Product_R,R_CC_DBHOB,R_DBHOB_R,R_CC_TotalHeight,");
                    sb.Append("R_TotalHeight_R,R_CC_TotHeightUnder,R_TotHeightUnder_R,R_CC_TotHeightOver,R_TotHeightOver_R,R_CC_MerchHgtPP,");
                    sb.Append("R_MerchHgtPP_R,R_CC_MerchHgtSP,R_MerchHgtSP_R,R_CC_HgtFLL,R_HgtFLL_R,R_CC_SeenDefPP,R_SeenDefPP_R,");
                    sb.Append("R_CC_SeenDefSP,R_SeenDefSP_R,R_CC_RecDef,R_RecDef_R,R_CC_TopDIBPP,R_TopDIBPP_R,R_CC_FormClass,R_FormClass_R,");
                    sb.Append("R_CC_Clear,R_Clear_R,R_CC_TreeGrade,R_TreeGrade_R,R_CC_LogGrade,R_LogGrade_R,R_CC_LogSeenDef,R_LogSeenDef_R,");
                    sb.Append("R_IncludeVol,R_TreeSpecies,R_TreeProduct,R_MarkerInitials,R_CC_GrossCUFTPP,R_GrossCUFTPP,R_CC_GrossBDFTPP,");
                    sb.Append("R_GrossBDFTPP,R_CC_NetCUFTPP,R_NetCUFTPP,R_CC_NetBDFTPP,R_NetBDFTPP,R_CC_GrossCUFTSP,R_GrossCUFTSP,R_CC_GrossBDFTSP,");
                    sb.Append("R_GrossBDFTSP,R_CC_NetCUFTSP,R_NetCUFTSP,R_CC_NetBDFTSP,R_NetBDFTSP,R_CC_GrossCUFTPSP,R_GrossCUFTPSP,R_CC_NetCUFTPSP,");
                    sb.Append("R_NetCUFTPSP,R_CC_GrossBDFTPSP,R_GrossBDFTPSP,R_CC_NetBDFTPSP,R_NetBDFTPSP,R_InResult,R_OutResult) VALUES ('");
                    sb.Append(cr.R_Stratum);                sb.Append("','");                     
                    sb.Append(cr.R_CuttingUnit);            sb.Append("','");                            
                    sb.Append(cr.R_Plot);                   sb.Append("','");
                    sb.Append(cr.R_SampleGroup);            sb.Append("','");
                    sb.Append(cr.R_CountMeasure);           sb.Append("','");  
                    sb.Append(cr.R_TreeNumber);             sb.Append("','");                       
                    sb.Append(cr.R_LogNumber);              sb.Append("','");                      
                    sb.Append(cr.R_CC_Species);             sb.Append("','");                       
                    sb.Append(cr.R_Species_R);              sb.Append("','");                     
                    sb.Append(cr.R_CC_LiveDead);            sb.Append("','");                      
                    sb.Append(cr.R_LiveDead_R);             sb.Append("','");                      
                    sb.Append(cr.R_CC_Product);             sb.Append("','");                       
                    sb.Append(cr.R_Product_R);              sb.Append("','");                        
                    sb.Append(cr.R_CC_DBHOB);               sb.Append("','");                         
                    sb.Append(cr.R_DBHOB_R);                sb.Append("','");                  
                    sb.Append(cr.R_CC_TotalHeight);         sb.Append("','");                   
                    sb.Append(cr.R_TotalHeight_R);          sb.Append("','");               
                    sb.Append(cr.R_CC_TotHeightUnder);      sb.Append("','");                
                    sb.Append(cr.R_TotHeightUnder_R);       sb.Append("','");                
                    sb.Append(cr.R_CC_TotHeightOver);       sb.Append("','");                 
                    sb.Append(cr.R_TotHeightOver_R);        sb.Append("','");
                    sb.Append(cr.R_CC_MerchHgtPP);          sb.Append("','");                    
                    sb.Append(cr.R_MerchHgtPP_R);           sb.Append("','");                   
                    sb.Append(cr.R_CC_MerchHgtSP);          sb.Append("','");                    
                    sb.Append(cr.R_MerchHgtSP_R);           sb.Append("','");                       
                    sb.Append(cr.R_CC_HgtFLL);              sb.Append("','");                        
                    sb.Append(cr.R_HgtFLL_R);               sb.Append("','");                    
                    sb.Append(cr.R_CC_SeenDefPP);           sb.Append("','");                     
                    sb.Append(cr.R_SeenDefPP_R);            sb.Append("','");                    
                    sb.Append(cr.R_CC_SeenDefSP);           sb.Append("','");                     
                    sb.Append(cr.R_SeenDefSP_R);            sb.Append("','");                       
                    sb.Append(cr.R_CC_RecDef);              sb.Append("','");                        
                    sb.Append(cr.R_RecDef_R);               sb.Append("','");                     
                    sb.Append(cr.R_CC_TopDIBPP);            sb.Append("','");                      
                    sb.Append(cr.R_TopDIBPP_R);             sb.Append("','");
                    sb.Append(cr.R_CC_FormClass);           sb.Append("','");                     
                    sb.Append(cr.R_FormClass_R);            sb.Append("','");                        
                    sb.Append(cr.R_CC_Clear);               sb.Append("','");                         
                    sb.Append(cr.R_Clear_R);                sb.Append("','");                    
                    sb.Append(cr.R_CC_TreeGrade);           sb.Append("','");                     
                    sb.Append(cr.R_TreeGrade_R);            sb.Append("','");                     
                    sb.Append(cr.R_CC_LogGrade);            sb.Append("','");                      
                    sb.Append(cr.R_LogGrade_R);             sb.Append("','");                   
                    sb.Append(cr.R_CC_LogSeenDef);          sb.Append("','");                    
                    sb.Append(cr.R_LogSeenDef_R);           sb.Append("','");                      
                    sb.Append(cr.R_IncludeVol);             sb.Append("','");                     
                    sb.Append(cr.R_TreeSpecies);            sb.Append("','");                     
                    sb.Append(cr.R_TreeProduct);            sb.Append("','");                  
                    sb.Append(cr.R_MarkerInitials);         sb.Append("','");                  
                    sb.Append(cr.R_CC_GrossCUFTPP);         sb.Append("','");                     
                    sb.Append(cr.R_GrossCUFTPP);            sb.Append("','");                  
                    sb.Append(cr.R_CC_GrossBDFTPP);         sb.Append("','");                     
                    sb.Append(cr.R_GrossBDFTPP);            sb.Append("','");                    
                    sb.Append(cr.R_CC_NetCUFTPP);           sb.Append("','");                       
                    sb.Append(cr.R_NetCUFTPP);              sb.Append("','");                    
                    sb.Append(cr.R_CC_NetBDFTPP);           sb.Append("','");                       
                    sb.Append(cr.R_NetBDFTPP);              sb.Append("','");                  
                    sb.Append(cr.R_CC_GrossCUFTSP);         sb.Append("','");                     
                    sb.Append(cr.R_GrossCUFTSP);            sb.Append("','");                  
                    sb.Append(cr.R_CC_GrossBDFTSP);         sb.Append("','");                     
                    sb.Append(cr.R_GrossBDFTSP);            sb.Append("','");                    
                    sb.Append(cr.R_CC_NetCUFTSP);           sb.Append("','");                       
                    sb.Append(cr.R_NetCUFTSP);              sb.Append("','");                    
                    sb.Append(cr.R_CC_NetBDFTSP);           sb.Append("','");                       
                    sb.Append(cr.R_NetBDFTSP);              sb.Append("','");                    
                    sb.Append(cr.R_CC_GrossCUFTPSP);        sb.Append("','");                       
                    sb.Append(cr.R_GrossCUFTPSP);           sb.Append("','");                      
                    sb.Append(cr.R_CC_NetCUFTPSP);          sb.Append("','");
                    sb.Append(cr.R_NetCUFTPSP);             sb.Append("','");                    
                    sb.Append(cr.R_CC_GrossBDFTPSP);        sb.Append("','");                       
                    sb.Append(cr.R_GrossBDFTPSP);           sb.Append("','");                      
                    sb.Append(cr.R_CC_NetBDFTPSP);          sb.Append("','");
                    sb.Append(cr.R_NetBDFTPSP);             sb.Append("','");                        
                    sb.Append(cr.R_InResult);               sb.Append("','");
                    sb.Append(cr.R_OutResult);              sb.Append("');");

                    sqlcmd.CommandText = sb.ToString();
                    sqlcmd.ExecuteNonQuery();
                }   //  end foreach
                sqlconn.Close();
            }   //  end using
            return;
        }   //  end saveResults


        private string checkFileName(string nameToCheck)
        {
            if (!nameToCheck.StartsWith("Data Source"))
                return nameToCheck.Insert(0, "Data Source = ");
            return nameToCheck;
        }   //  end checkFileName
    }
}
