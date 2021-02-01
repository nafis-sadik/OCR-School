using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstraction;
using Services.Implementation;

namespace OCR_School_Web_App.Controllers
{
    public class ScannerController : Controller
    {
        IFileService _fileService;
        public ScannerController()
        {
            _fileService = new FileService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadScannedImage(string image)
        {
            if (_fileService.SaveImageFile(image))
            {
                // OCR Service
                // Redirect to next page
            }
            else
                // Redirect to previous page
            throw new NotImplementedException();
        }
    }
}
