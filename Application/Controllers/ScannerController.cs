using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR_School_Web_App.Controllers
{
    public class ScannerController : Controller
    {

        public ScannerController()
        {
                
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ScannedImg(string img)
        {
            throw new NotImplementedException();
        }
    }
}
