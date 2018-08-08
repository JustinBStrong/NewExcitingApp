using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Exciting_App;
using Exciting_App.Controllers;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exciting_App {
    public class UploadFiles : Controller {
        // GET: /<controller>/
        public IActionResult Index() {
            return View();
            //test
        }
        [HttpPost("UploadFiles")]
        public IActionResult Post(IFormFile CMDB, string CMDBSourceName, string CMDBSheetName, IFormFile DataMart, string DataMartSourceName, string DataMartSheetName, int CMDBGSIIndex, int CMDBNodeIndex, int DataMartGSIIndex, int DataMartNodeIndex) {
            IWorkbook DataMartWorkbook;
            AppToNodeRelationDataHolder DataMartDataHolder = null;
            if (true) {
                int gsiIndex = DataMartGSIIndex;
                string source = DataMartSourceName;

                int nodeIndex = DataMartNodeIndex;
                string sheetName = DataMartSheetName;
                DataMartWorkbook = WorkbookFactory.Create(DataMart.OpenReadStream());
                DataMartDataHolder = AppToServerComparison.GetAppToNodeRelationshipsFromWorkbook(DataMartWorkbook, sheetName, source, gsiIndex, nodeIndex);
            }
            AppToNodeRelationDataHolder CMDBDataHolder = null; ;
            IWorkbook CMDBWorkbook;
            if (true) {
                string sheetName = CMDBSheetName;
                string source = CMDBSourceName;

                int gsiIndex = CMDBGSIIndex;
                int nodeIndex = CMDBNodeIndex;
                CMDBWorkbook = WorkbookFactory.Create(CMDB.OpenReadStream());
                CMDBDataHolder = AppToServerComparison.GetAppToNodeRelationshipsFromWorkbook(CMDBWorkbook, sheetName, source, gsiIndex, nodeIndex);
            }
            if (CMDBDataHolder != null && DataMartDataHolder != null) {
                string newstring = AppToServerComparison.CompareUploadedFiles(CMDBDataHolder, CMDBSourceName, DataMartDataHolder, DataMartSourceName);
                ViewData["string"] = newstring;
                ViewBag.String = newstring;
                return View();
            }
            else {
                return View("UploadProcessed", "Error");
            }
        }
    }
}