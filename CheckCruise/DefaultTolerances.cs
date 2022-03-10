using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckCruise
{
    public class DefaultTolerances
    {
        //  loads tolerance list with default for current region
        public List<TolerancesList> getDefaultTolerances(string currRegion)
        {
            List<TolerancesList> defaultList = new List<TolerancesList>();
            switch (currRegion)
            {
                case "01":
                    loadDefaultList(defaultList, elementTolerance_01, volumeTolerance_01, otherTolerance_01, 6, currRegion);
                    break;
                case "02":
                    loadDefaultList(defaultList, elementTolerance_02, volumeTolerance_02, otherTolerance_02, 5, currRegion);
                    break;
                case "03":
                    loadDefaultList(defaultList, elementTolerance_03, volumeTolerance_03, othertolerance_03, 6, currRegion);
                    break;
                case "04":
                    loadDefaultList(defaultList, elementTolerance_04, volumeTolerance_04, otherTolerance_04, 5, currRegion);
                    break;
                case "05":
                    loadDefaultList(defaultList, elementTolerance_05, volumeTolerance_05, otherTolerance_05, 5, currRegion);
                    break;
                case "06":
                    loadDefaultList(defaultList, elementTolerance_06, volumeTolerance_06, otherTolerance_06, 7, currRegion);
                    break;
                case "08":
                    loadDefaultList(defaultList, elementTolerance_08, volumeTolerance_08, otherTolerance_08, 7, currRegion);
                    break;
                case "09":
                    loadDefaultList(defaultList, elementTolerance_09, volumeTolerance_09, otherTolerance_09, 8, currRegion);
                    break;
                case "10":
                    loadDefaultList(defaultList, elementTolerance_10, volumeTolerance_10, otherTolerance_10, 6, currRegion);
                    break;
            }   //  end switch
            return defaultList;
        }   //  end getDefaultTolerances


        private void loadDefaultList(List<TolerancesList> defaultList, string[,] currentElements, float[] currentVolumes, 
                                    string[] currentOthers, int numRows, string currRegion)
        {
            //  remove anything that might be in the default list
            if (defaultList.Count > 0)
                defaultList.Clear();

            for (int k = 0; k < numRows; k++)
            {
                TolerancesList d = new TolerancesList();
                d.T_Element = currentElements[k, 0];
                d.T_Tolerance = currentElements[k, 1];
                d.T_Units = currentElements[k, 2];
                d.T_AddParam = currentElements[k, 3];
                d.T_Weight = (float)Convert.ToDouble(currentElements[k, 4]);
                defaultList.Add(d);
            }   //  end for k loop

            //  add volume tolerances
            defaultList[0].T_CUFTPGtolerance = currentVolumes[0];
            defaultList[0].T_CUFTPNtolerance = currentVolumes[1];
            defaultList[0].T_CUFTSGtolerance = currentVolumes[2];
            defaultList[0].T_CUFTSNtolerance = currentVolumes[3];
            defaultList[0].T_CUFTPSGtolerance = currentVolumes[4];
            defaultList[0].T_CUFTPSNtolerance = currentVolumes[5];
            defaultList[0].T_BDFTPGtolerance = currentVolumes[6];
            defaultList[0].T_BDFTPNtolerance = currentVolumes[7];
            defaultList[0].T_BDFTSGtolerance = currentVolumes[8];
            defaultList[0].T_BDFTSNtolerance = currentVolumes[9];
            defaultList[0].T_BDFTPSGtolerance = currentVolumes[10];
            defaultList[0].T_BDFTPSNtolerance = currentVolumes[11];

            //  and the other tolerances
            defaultList[0].T_IncludeVolume = Convert.ToInt16(currentOthers[0]);
            defaultList[0].T_BySpecies = Convert.ToInt16(currentOthers[1]);
            defaultList[0].T_ByProduct = Convert.ToInt16(currentOthers[2]);
            defaultList[0].T_ElementAccuracy = (float)Convert.ToDouble(currentOthers[3]);
            defaultList[0].T_OverallAccuracy = (float)Convert.ToDouble(currentOthers[4]);
            defaultList[0].T_DateStamp = currentOthers[5];

            //  special condtion for region 10 on two elements
            if (currRegion == "10")
            {
                foreach (TolerancesList dl in defaultList)
                {
                    if (dl.T_Element == "Seen Defect % Primary")
                        dl.T_ElementAccuracy = 85;
                    else if (dl.T_Element == "Log Grade")
                        dl.T_ElementAccuracy = 85;
                    else dl.T_ElementAccuracy = 80;
                }   //  end foreach loop
            }   //  endif            
            //  and special conditions for Region 6 on two elements
            if (currRegion == "6" || currRegion == "06")
            {
                foreach (TolerancesList dl in defaultList)
                {
                    if (dl.T_Element == "In/Out Trees")
                        dl.T_ElementAccuracy = 95;
                    else if (dl.T_Element == "Species")
                        dl.T_ElementAccuracy = 95;
                    else dl.T_ElementAccuracy = 90;
                }   //  end foreach loop
            }   //  endif
            return;
        }   //  end loadDefaultList


        //  Region 1
        public string[,] elementTolerance_01 = new string[6,5] {{"In/Out Trees",            "None","None",   "None",          "5.0"},
                                                                {"Species",                 "None","None",   "None",          "5.0"},
                                                                {"DBH",                     "0.2", "inches", "3% whichever >","1.0"},
                                                                {"Total Height <= 100 feet","4",   "feet",   "7% whichever >","1.0"},
                                                                {"Total Height > 100 feet", "7",   "percent","None",          "1.0"},
                                                                {"Seen Defect % Primary",   "10",  "percent","None",          "1.0"}};
        public float[] volumeTolerance_01 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] otherTolerance_01 = new string[6] { "0", "0", "0", "80.0", "85.0", "01-16-2015" };

        //  Region 2
        public string[,] elementTolerance_02 = new string[5, 5] {{"In/Out Trees",         "None", "None",  "None","5.0"},
                                                                {"Species",               "None", "None",  "None","5.0"},
                                                                {"DBH",                   "0.2",  "inches","None","1.0"},
                                                                {"Total Height",          "4",    "feet",  "None","1.0"},
                                                                {"Seen Defect % Primary", "10",   "None",  "None","1.0"}};
        public float[] volumeTolerance_02 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] otherTolerance_02 = new string[6] { "0", "1", "0", "85.0", "95.0", "11-28-2017" };

        //  Region 3
        public string[,] elementTolerance_03 = new string[6, 5] {{"In/Out Trees",            "None","None",   "None",                     "5.0"},
                                                                 {"Species",                 "None","None",   "None",                     "5.0"},
                                                                 {"DBH",                     "0.2", "inches", "None",                     "1.0"},
                                                                 {"Total Height <= 100 feet","4",   "feet",   "None",                     "1.0"},
                                                                 {"Total Height > 100 feet", "4",   "feet",   "+1 per 25 feet > 100 feet","1.0"},
                                                                 {"Seen Defect % Primary",   "10",  "abs",    "None",                     "1.0"}};
        public float[] volumeTolerance_03 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] othertolerance_03 = new string[6] { "0", "0", "0", "80.0", "85.0", "01-16-2015" };

        //  Region 4
        public string[,] elementTolerance_04 = new string[5, 5] {{ "In/Out Trees",         "None", "None",    "None",                      "5.0" },
                                                                 {"Species",               "None", "None",    "None",                      "5.0"},
                                                                 {"DBH",                   "0.2",  "inches",  "None",                      "1.0"},
                                                                 {"Total Height",          "4",    "feet",    "4 feet +1 per 25 feet > 1", "1.0"},
                                                                 {"Seen Defect % Primary", "10",   "abs", "None",                      "1.0"}};
        public float[] volumeTolerance_04 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] otherTolerance_04 = new string[6] { "0", "0", "0", "80.0", "85.0", "01-16-2015" };

        //  Region 5
        public string[,] elementTolerance_05 = new string[5,5] {{"In/Out Trees",          "None", "None",    "None", "5.0"},
                                                                {"Species",               "None", "None",    "None", "5.0"},
                                                                {"DBH",                   "3",    "percent", "None", "1.0"},
                                                                {"Total Height",          "7",    "percent", "None", "1.0"},
                                                                {"Seen Defect % Primary", "10",   "abs", "None", "1.0"}};
        public float[] volumeTolerance_05 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] otherTolerance_05 = new string[6] { "0", "1", "0", "80.0", "85.0", "01-16-2015" };

        //  Region 6
        public string[,] elementTolerance_06 = new string[7, 5] {{"In/Out Trees",           "None", "None",    "None", "1.0"},
                                                                {"Species",                 "None", "None",    "None", "1.0"},
                                                                {"DBH",                     "3",    "percent", "None", "1.0"},
                                                                {"Total Height",            "8",    "percent", "None", "1.0"},
                                                                {"Top DIB Primary",         "25",   "percent", "None", "1.0"},
                                                                {"Seen Defect % Primary",   "8",    "percent", "None", "1.0"},
                                                                {"Log Grade",               "None", "None",    "None", "1.0"}};
        public float[] volumeTolerance_06 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] otherTolerance_06 = new string[6] { "0", "0", "0", "90.0", "90.0", "04-07-2015" };


        //  Region 8
        public string[,] elementTolerance_08 = new string[7, 5] {{"In/Out Trees",         "None", "None",    "None", "5.0"},
                                                                {"Species",               "None", "None",    "None", "5.0"},
                                                                {"DBH",                   "0.2",  "inches",  "None", "2.0"},
                                                                {"Seen Defect % Primary", "20",   "percent", "None", "1.0"},
                                                                {"Merch Height Primary",  "10",   "feet",    "None", "3.0"},
                                                                {"Second Height Accuracy","0.10", "feet",    "None", "1.0"},
                                                                {"Second Height Bias",    "0.95", "feet",    "<=>1.05", "1.0"}};               
        public float[] volumeTolerance_08 = new float[12] { 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
        public string[] otherTolerance_08 = new string[6] { "0", "0", "0", "75.0", "80.0", "01-16-2015" };

        //  Region 9
        public string[,] elementTolerance_09 = new string[8, 5] {{"In/Out Trees",         "None", "None",    "None", "5.0"},
                                                                {"Species",               "None", "None",    "None", "5.0"},
                                                                {"Product",               "None", "None",    "None", "3.0"},
                                                                {"DBH",                   "0.2",  "inches",  "None", "1.0"},
                                                                {"Merch Height Primary",  "8",    "feet",    "None", "1.0"},
                                                                {"Merch Height Secondary","8",    "feet",    "None", "1.0"},
                                                                {"Total Height",          "8",    "feet",    "None", "1.0"},
                                                                {"Seen Defect % Primary", "10",   "percent", "None", "1.0"}};
        public float[] volumeTolerance_09 = new float[12] { 10, 10, 0, 0, 0, 0, 10, 10, 0, 0, 0, 0 };
        public string[] otherTolerance_09 = new string[6] { "1", "0", "0", "80.0", "85.0", "01-16-2015" };

        //  Region 10
        public string[,] elementTolerance_10 = new string[6, 5] {{"In/Out Trees",         "None", "None",    "None", "5.0"},
                                                                {"Species",               "None", "None",    "None", "3.0"},
                                                                {"DBH",                   "3",    "percent", "None", "1.0"},
                                                                {"Total Height",          "8",    "percent", "None", "1.0"},
                                                                {"Log Defect %",          "10",   "percent", "None", "1.0"},
                                                                {"Log Grade",             "None", "None",    "None", "1.0"}};
        public float[] volumeTolerance_10 = new float[12] { 5, 10, 0, 0, 0, 0, 5, 10, 0, 0, 0, 0 };
        public string[] otherTolerance_10 = new string[6] { "1", "1", "0", "80.0", "85.0", "01-16-2015" };

    }
}
