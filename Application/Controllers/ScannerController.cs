using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstraction;
using Services.Implementation;
using OCR_School_Web_App.Client;

namespace OCR_School_Web_App.Controllers
{
    public class ScannerController : Controller
    {
        IFileService _fileService;
        IConfiguration _config;
        
        public ScannerController(IConfiguration config)
        {
            _fileService = new FileService();
            _config = config;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadScannedImage(string image)
        {
            if (_fileService.SaveImageFile(image, out string SaveStatusMsg)) {
                
                string OCR_Output = GCP_Vission_Client.LoadImg("D:/OCR-School/Services/Images/ImageFromScanner.jpg");

                //string Output = _config.GetValue<string>("AppSettings:***********write code:no definitons in AppSettings");

                // Redirect to next page
            }
            else {
                // Redirect to previous page
            }

            return View(SaveStatusMsg);
        }
    }
}
