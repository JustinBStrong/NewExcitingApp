using System;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using Exciting_App;
using System.Collections;
namespace Exciting_App.Controllers {
    public class AppToServerComparison {
        public void EmptyClass() {
        }

        public static string RunTrueUp() {
            return DoubleCompare();
            AppToNodeRelationDataHolder vwAllApps = GetvwAppToServer();
            AppToNodeRelationDataHolder DataMartExport = GetDataMartExport();
            //string problems = compareSources(DataMartExport, "CMDB", DataMartExport, "DataMart"); //
            string problems = compareSources(vwAllApps, "CMDB", DataMartExport, "DataMart");
            return problems;
        }

        public static AppToNodeRelationDataHolder GetvwAppToServer() {
            int gsiIndex = 1;
            int nodeIndex = 4;
            string path = "";//"/Users/Justin/Downloads/vwAppToServersOriginal_Development.xls";//vwAppToServersOriginal_2018-07-23_15-28-45.xlsx";
            //path = "/Users/Justin/Downloads/Book1_Development.xlsx";//
            string source = "CMDB";
            string sheetName = "Report Results";
            return GetAppToNodeRelationships(path, sheetName, source, gsiIndex, nodeIndex);
        }

        public static AppToNodeRelationDataHolder GetDataMartExport() {
            int gsiIndex = 0;
            int nodeIndex = 1;
            string path = "";//"/Users/Justin/Downloads/Book1_Development.xls";//vwAppToServersOriginal_2018-07-23_15-28-45.xlsx";
            string source = "DataMart";
            string sheetName = "Query";
            return GetAppToNodeRelationships(path, sheetName, source, gsiIndex, nodeIndex);
        }
        public static AppToNodeRelationDataHolder GetAppToNodeRelationshipsFromWorkbook(IWorkbook workbook, string sheetName, string source, int GsiIndex, int NodeIndex) {
            ISheet sheet = workbook.GetSheet(sheetName);
            string badAdds = "";
            int DuplicateCount = 0;
            //badAdds += " test";
            string app = "";
            Dictionary<string, AppToNodeRelation> map = new Dictionary<string, AppToNodeRelation>(3000);
            for (int i = 0; i < sheet.LastRowNum; i++) {
                //badAdds += " at the start of iteration app was: " + app + "<br>";
                string node = "";
                if (string.Format(sheet.GetRow(i).GetCell(NodeIndex).ToString()) != null) {
                    node = string.Format(sheet.GetRow(i).GetCell(NodeIndex).ToString().ToLowerInvariant());
                }
                if (sheet.GetRow(i).GetCell(GsiIndex).ToString() != null && sheet.GetRow(i).GetCell(GsiIndex).ToString() != "") {
                    if (sheet.GetRow(i).GetCell(GsiIndex).ToString() != "")
                        if (sheet.GetRow(i).GetCell(GsiIndex).ToString().Contains("[Busi") || sheet.GetRow(i).GetCell(GsiIndex).ToString().Contains("gsi_id")) {
                            continue;
                        }
                    app = sheet.GetRow(i).GetCell(GsiIndex).ToString().ToUpperInvariant();
                }
                if (map.ContainsKey(app)) {
                    AppToNodeRelation current = map[app];
                    if (!current.AddNode(node)) {
                        badAdds += "Duplicate app to node relationship between " + app + " and " + node + " in the " + source + "<br>";
                        DuplicateCount++;
                    }
                    else {
                        //badAdds += "Adding app " + app + " to " + node;
                    }
                }
                else {
                    AppToNodeRelation current = new AppToNodeRelation(app, node);
                    map.Add(app, current);
                }
            }
            badAdds = "<h3>" + source + " has " + DuplicateCount + " Duplicates: </h3>" + badAdds;
            AppToNodeRelationDataHolder dataHolder = new AppToNodeRelationDataHolder(map, badAdds);
            return dataHolder;
        }

        /*public static string TakeTempTiles() {
            AppToNodeRelationDataHolder vwAllApps = GetvwAppToServer();
            AppToNodeRelationDataHolder DataMartExport = GetDataMartExport();
            string DataMarttoCMDB = compareSources(vwAllApps, "CMDB", DataMartExport, "DataMart");
            string CMDBtoDataMart = compareSources(DataMartExport, "DataMart", vwAllApps, "CMDB");
            string DataMartDuplicates = DataMartExport.GetBadAdds();
            string CMDBDuplicates = vwAllApps.GetBadAdds();
            return CMDBtoDataMart + DataMarttoCMDB + DataMartDuplicates + CMDBDuplicates;
        }*/
        /*public static Dictionary<string, AppToNodeRelation> GetDatamartExport() {
            string path = "/Users/Justin/Projects/Exciting_App/Exciting_App/bin/Copy of DataMartExport06142018.xls";
            string sheetName = "CMDB- UPDOwn attributes";
            int gsiIndex = 1;
            int upstreamIndex = 2;
            int downstreamIndex = 3;
            return GetDictionaryOfApps(path, sheetName, gsiIndex, upstreamIndex, downstreamIndex);
        }*/

        //This method creates a dictionary which associates gsi IDs with App objects which contain the strings of the upstream/downstream lists
        /*public static Dictionary<String, App> GetDictionaryOfApps(String path, String sheetName, int gsiIdIndex, int upstreamIndex, int downstreamIndex) {
            HSSFWorkbook workbook;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                workbook = new HSSFWorkbook(file);
            }
            ISheet sheet = workbook.GetSheet(sheetName);
            Dictionary<String, App> map = new Dictionary<String, App>();
            string returnString = "<table style=\"width: 100 % \">";
            for (int i = 0; i < sheet.LastRowNum; i++) {
                string upstream;
                string downstream;
                if (sheet.GetRow(i) != null) {
                    string gsiId = string.Format(sheet.GetRow(i).GetCell(gsiIdIndex).ToString());
                    if (sheet.GetRow(i).GetCell(2).ToString() != null) {
                        upstream = string.Format(sheet.GetRow(i).GetCell(2).ToString());
                    }
                    else {
                        upstream = "";
                    }
                    if (sheet.GetRow(i).GetCell(3).ToString() != null) {
                        downstream = string.Format(sheet.GetRow(i).GetCell(3).ToString());
                    }
                    else {
                        downstream = "";
                    }
                    Exciting_App.Controllers.App newApp = new App(gsiId);
                    newApp.SetUpstreamList(upstream);
                    newApp.SetDownstreamList(downstream);
                    map[gsiId] = newApp;
                    returnString += "<tr><td>GSI: " + gsiId + "</td><td>Up: " + upstream + "</td><td>Down: " + downstream + "</td></tr>\n";
                }
            }

            return map; //sheet.LastRowNum.ToString();
        }*/

        public static AppToNodeRelationDataHolder GetAppToNodeRelationships(string path,string sheetName, string source, int GsiIndex, int NodeIndex) {
            IWorkbook workbook = Exciting_App.ExcelMethods.GetWorkbook(path);
            ISheet sheet = workbook.GetSheet(sheetName);
            string badAdds = "";
            int DuplicateCount = 0; //Jennifer will win
            //badAdds += " test";
            string app = "";
            Dictionary<string, AppToNodeRelation> map = new Dictionary<string, AppToNodeRelation>(3000);
            for (int i = 0; i < sheet.LastRowNum; i++) {
                //badAdds += " at the start of iteration app was: " + app + "<br>";
                string node= "";
                if (string.Format(sheet.GetRow(i).GetCell(NodeIndex).ToString()) != null) {
                    node = string.Format(sheet.GetRow(i).GetCell(NodeIndex).ToString().ToLowerInvariant());
                }
                if (sheet.GetRow(i).GetCell(GsiIndex).ToString() != null && sheet.GetRow(i).GetCell(GsiIndex).ToString() != "") {
                    if (sheet.GetRow(i).GetCell(GsiIndex).ToString() != "")
                        if (sheet.GetRow(i).GetCell(GsiIndex).ToString().Contains("[Busi") || sheet.GetRow(i).GetCell(GsiIndex).ToString().Contains("gsi_id")) {
                            continue;
                        }
                    app = sheet.GetRow(i).GetCell(GsiIndex).ToString().ToUpperInvariant();
                }
                if (map.ContainsKey(app)) {
                    AppToNodeRelation current = map[app];
                    if (!current.AddNode(node)) {
                        badAdds += "Duplicate app to node relationship between " + app + " and " + node + " in the " + source + "<br>";
                        DuplicateCount++;
                    } else {
                        //badAdds += "Adding app " + app + " to " + node;
                    }
                } else {
                    AppToNodeRelation current = new AppToNodeRelation(app, node);
                    map.Add(app, current);
                }
            }
            badAdds = "<h3>" + source + " has " + DuplicateCount + " Duplicates: </h3>" + badAdds;
            AppToNodeRelationDataHolder dataHolder = new AppToNodeRelationDataHolder(map, badAdds);
            return dataHolder;
        }


        public static string DoubleCompare() {
            AppToNodeRelationDataHolder vwAllApps = GetvwAppToServer();
            AppToNodeRelationDataHolder DataMartExport = GetDataMartExport();
            string DataMarttoCMDB = compareSources(vwAllApps, "CMDB", DataMartExport, "DataMart");
            string CMDBtoDataMart = compareSources(DataMartExport, "DataMart", vwAllApps, "CMDB");
            string DataMartDuplicates = DataMartExport.GetBadAdds();
            string CMDBDuplicates = vwAllApps.GetBadAdds();
            return CMDBtoDataMart + DataMarttoCMDB + DataMartDuplicates + CMDBDuplicates;
        }
        public static string CompareUploadedFiles(AppToNodeRelationDataHolder CMDB, string CMDBSourceName, AppToNodeRelationDataHolder DataMart, string DataMartSourceName) {
            string DataMartToCMDB = compareSources(CMDB, CMDBSourceName, DataMart, DataMartSourceName);
            string CMDBToDataMart = compareSources(DataMart, DataMartSourceName, CMDB, CMDBSourceName);
            string DataMartDuplicates = DataMart.GetBadAdds();
            string CMDBDuplicates = CMDB.GetBadAdds();
            return CMDBToDataMart + DataMartToCMDB + DataMartDuplicates + CMDBDuplicates;
        }

        public static void CompareAppObjects() {
            
        }
        public static string compareSources(AppToNodeRelationDataHolder checkInData, string checkInSourceName, AppToNodeRelationDataHolder checkForData, string checkForSourceName) {
            Dictionary<string, AppToNodeRelation> checkInDictionary = checkInData.GetDictionary();
            Dictionary<string, AppToNodeRelation> checkForDictionary = checkForData.GetDictionary();
            string problems = "";
            //problems += checkInSourceName + " has " + checkInDictionary.Count + " entries<br> " + checkForSourceName + " has " + checkForDictionary.Count + "<br>";
            string Missing = "";
            int MissingCount = 0;
            int ProblemCount = 0;
            IDictionaryEnumerator checkForEnumerator = checkForDictionary.GetEnumerator();
            while(checkForEnumerator.MoveNext()) {
                DictionaryEntry currentEntry = checkForEnumerator.Entry;
                string gsi = (string)currentEntry.Key;
                if(checkInDictionary.ContainsKey(gsi)) {
                    AppToNodeRelation checkInRelation = checkInDictionary[gsi];
                    AppToNodeRelation checkForRelation = (AppToNodeRelation)currentEntry.Value;
                    HashSet<string> checkForNodeList = checkForRelation.GetNodeList();
                    IEnumerator<string> nodeEnumerator = (IEnumerator<string>)checkForNodeList.GetEnumerator();
                    while(nodeEnumerator.MoveNext()) {
                        string currentNode = nodeEnumerator.Current;

                        if(!checkInRelation.HasNode(currentNode)) {
                            problems += "The " + checkInSourceName + " is missing the relation " + gsi + " " + currentNode + "<br>";

                            ProblemCount++;
                        } else {
                            //problems += "The " + checkInSourceName + " has a matching relation " + gsi + " " + currentNode + "\n";
                        }
                    }
                } else {
                    MissingCount++;
                    Missing += checkForSourceName + " does not contain the app " + gsi + " but " + checkInSourceName + " does.<br>";
                }
            }
            Missing = "<h3>Found " + MissingCount + " Missing Apps in " + checkForSourceName + " but not in " + checkInSourceName + ": </h3>" + Missing;
            string complete = "<h3>Found a total of " + ProblemCount + " missing relations which are in " + checkForSourceName + " but not in " + checkInSourceName + ".</h3>" + problems + Missing;// + checkInData.GetBadAdds() + checkForData.GetBadAdds();
            return complete;
        }
    }
}
