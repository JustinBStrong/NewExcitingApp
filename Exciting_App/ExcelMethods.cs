using System;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF;

namespace Exciting_App {
    public class ExcelMethods {
        public static IWorkbook GetWorkbook(string path) {
            IWorkbook workbook;
            using (System.IO.FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                workbook = WorkbookFactory.Create(file);
            }
            return workbook;
        }
        //public static IWorkbook FromUpload(FileStream fileStream) {
            

    }
}

