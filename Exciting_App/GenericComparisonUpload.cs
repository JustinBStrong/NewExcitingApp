using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using System.IO;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using Exciting_App.Controllers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exciting_App {
    public class GenericComparisonUpload : Controller {
        // GET: /<controller>/
        public IActionResult Index() {
            return View();
        }
        [HttpPost("GenericComparisonUpload")]
        public IActionResult Post(IFormFile fileOne, string FileOneSourceName, string FileOneSheetName, int FileOnePrimaryKeyIndex, int FileOneComparisonOneIndex, int FileOneComparisonTwoIndex, int FileOneComparisonThreeIndex, IFormFile fileTwo, string FileTwoSheetName, string FileTwoSourceName, int FileTwoPrimaryKeyIndex, int FileTwoComparisonOneIndex, int FileTwoComparisonTwoIndex, int FileTwoComparisonThreeIndex) {
            IWorkbook FileOneWorkbook;
            try {
                FileOneWorkbook = WorkbookFactory.Create(fileOne.OpenReadStream());
            } catch(Exception ) {
                ViewBag.String = "One of the Excel files is either not an .XLS file or was not uploaded.";
                return View();

            }
            IWorkbook FileTwoWorkbook = WorkbookFactory.Create(fileTwo.OpenReadStream());
            ISheet sheetOne = FileOneWorkbook.GetSheet(FileOneSheetName);
            ISheet sheetTwo = FileTwoWorkbook.GetSheet(FileTwoSheetName);
            string result = GenericComparison.compare(FileOneWorkbook, FileOneSheetName, FileOneSourceName, FileOnePrimaryKeyIndex, FileTwoWorkbook, FileTwoSheetName, FileTwoSourceName, FileTwoPrimaryKeyIndex, FileOneComparisonOneIndex, FileTwoComparisonOneIndex, FileOneComparisonTwoIndex, FileTwoComparisonTwoIndex, FileOneComparisonThreeIndex, FileTwoComparisonThreeIndex);
            ViewBag.String = result;
            return View();
        }
    }
}
