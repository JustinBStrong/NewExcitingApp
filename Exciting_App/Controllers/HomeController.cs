using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exciting_App.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using Exciting_App.Controllers;

namespace Exciting_App.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View("UploadFiles");
        }

        public IActionResult Upload() {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult UploadFiles() {
            return View();
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GenericComparison() {
            return View();
        }

        public IActionResult Justin() {
            return View();
        }
        public IActionResult Justin2() {
            return View();
        }
        public IActionResult Justin3() {
            return View();
        }
    }
}
