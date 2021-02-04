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
        IImageProcessing _imageProcessing;

        public ScannerController()
        {
            _fileService = new FileService();
            _imageProcessing = new ImageProcessing();
            
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadScannedImage(string image)
        {
            string OCR_Output = "";
            if (_fileService.SaveImageFile(image, out string srcImagePath)) {

                string markSheetImagePath = _imageProcessing.CropImage(srcImagePath);
                OCR_Output = await GCP_Vission_Client.LoadImg(markSheetImagePath);
            }
            else {
                // Redirect to previous page
            }
            
            Dictionary<string, string> marksheet = new Dictionary<string, string>();
            marksheet.Add("Question", OCR_Output);
            return View(marksheet);
        }
    }
}
