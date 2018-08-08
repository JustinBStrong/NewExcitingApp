using System;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using Exciting_App;
using System.Collections;
using SelfComparingGenericNamespace;

namespace Exciting_App.Controllers {
    //string debugString = "";
    public class GenericComparison {
        public static ISheet GetSheet(IWorkbook WorkbookOne, string SheetOneName) {
            return WorkbookOne.GetSheet(SheetOneName);
        }
        public static string compare(IWorkbook workbookOne, string sheetNameOne, string sourceName, int FileOnePrimaryKeyIndex, IWorkbook workbookTwo, string sheetNameTwo, string SourceNameTwo, int FileTwoPrimaryKeyIndex, int FileOneComparisonOneIndex, int FileTwoComparisonOneIndex, int FileOneComparisonTwoIndex, int FileTwoComparisonTwoIndex, int FileOneComparisonThreeIndex, int FileTwoComparisonThreeIndex) {
            Dictionary<string, SelfComparingGeneric> data = new Dictionary<string, SelfComparingGeneric>();
            GenericData dataObject = GetData(workbookOne, sheetNameOne, sourceName, SourceNameTwo, FileOnePrimaryKeyIndex, FileOneComparisonOneIndex, FileOneComparisonTwoIndex, FileOneComparisonThreeIndex, data, true);
            GetData(workbookTwo, sheetNameTwo, sourceName, SourceNameTwo, FileTwoPrimaryKeyIndex, FileTwoComparisonOneIndex, FileTwoComparisonTwoIndex, FileTwoComparisonThreeIndex, data, false);
            return DoTheComparison(dataObject);
        }


        public static GenericData GetData(IWorkbook workbook, string sheetName, string sourceName, string sourceTwoName, int PrimaryKeyIndex, int ComparisonOneIndex, int ComparisonTwoIndex, int ComparisonThreeIndex, Dictionary<string, SelfComparingGeneric> dataParameter, Boolean one) {
            string duplicates = "";
            string[] attributeNames = new string[3];
            string primaryKeyName = "";
            ISheet sheet = GetSheet(workbook, sheetName);
            Dictionary<string, SelfComparingGeneric> data;
            HashSet<string> keys = new HashSet<string>();
            if (dataParameter != null) {
                data = dataParameter;
            } else {
                data = new Dictionary<string, SelfComparingGeneric>();
            }
            SelfComparingGeneric currentItem;
            for (int i = 0; i < sheet.LastRowNum; i++) {
                string[] stringArray = new string[3];
                string PrimaryKey;
                if (sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString() != null && sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString() != "") {
                    /*if(i==0) {
                        if(sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString() != null && sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString() != "") {
                            primaryKeyName = sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString();
                        }
                        if (sheet.GetRow(i).GetCell(FileOneC).ToString() != null && sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString() != "") {
                            primaryKeyName = sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString();
                        }
                        continue;
                    }*/
                    PrimaryKey = sheet.GetRow(i).GetCell(PrimaryKeyIndex).ToString();

                    if (data.ContainsKey(PrimaryKey)) {
                        duplicates += sourceName + " contains multiple entries for " + PrimaryKey;
                    } else {
                        keys.Add(PrimaryKey);

                    }
                    if (!data.ContainsKey(PrimaryKey)) {
                        currentItem = new SelfComparingGeneric(PrimaryKey, sourceName, sourceTwoName, 3);
                    } else {
                        currentItem = data[PrimaryKey];
                    }
                    if (sheet.GetRow(i).GetCell(ComparisonOneIndex).ToString() != null && sheet.GetRow(i).GetCell(ComparisonOneIndex).ToString() != "") {
                        if (one) {
                            currentItem.AddToOne(sheet.GetRow(i).GetCell(ComparisonOneIndex).ToString(), 0);
                        }
                        else {
                            currentItem.AddToTwo(sheet.GetRow(i).GetCell(ComparisonOneIndex).ToString(), 0);
                        }
                    }
                    if (sheet.GetRow(i).GetCell(ComparisonTwoIndex).ToString() != null && sheet.GetRow(i).GetCell(ComparisonTwoIndex).ToString() != "") {
                        if (one) {
                            currentItem.AddToOne(sheet.GetRow(i).GetCell(ComparisonTwoIndex).ToString(), 1);
                        }
                        else {
                            currentItem.AddToTwo(sheet.GetRow(i).GetCell(ComparisonTwoIndex).ToString(), 1);
                        }
                    }
                    if (sheet.GetRow(i).GetCell(ComparisonThreeIndex).ToString() != null && sheet.GetRow(i).GetCell(ComparisonThreeIndex).ToString() != "") {
                        if (one) {
                            currentItem.AddToOne(sheet.GetRow(i).GetCell(ComparisonThreeIndex).ToString(), 2);
                        }
                        else {
                            currentItem.AddToTwo(sheet.GetRow(i).GetCell(ComparisonThreeIndex).ToString(), 2);
                        }
                    }
                    data[PrimaryKey] = currentItem;

                }
            }
            GenericData result = new GenericData();
            result.AddDictionary(data);
            result.SetDuplicates(duplicates);
            return result;
        }

        public static string DoTheComparison(GenericData result) {
            Dictionary<string, SelfComparingGeneric> data = result.GetDictionary();
            IEnumerator iter = data.GetEnumerator();
            string resultString = "";
            int i = 0;
            while(iter.MoveNext()) {
                
                KeyValuePair<string, SelfComparingGeneric> currentRow = (KeyValuePair<string, SelfComparingGeneric>)iter.Current;
                SelfComparingGeneric current = (SelfComparingGeneric)currentRow.Value;
                resultString += current.Compare() + "<br>" + i + "<br>";
                i++;
            }
            return resultString + "Working";
        }
       
    }
}