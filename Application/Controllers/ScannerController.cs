using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Abstraction;
using Services.Implementation;
using OCR_School_Web_App.Client;
using System.Net;
using System.IO;
using Entities;
using Microsoft.AspNetCore.Http;

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

        [HttpPost]
        public async Task<IActionResult> UploadScannedImage(List<string> image)
        {
            string OCR_Output = "";
            IDictionary<string, int> marksheet = new Dictionary<string, int>();

            try {
                if (_fileService.SaveImageFile(image, out List<string> srcImagePath)) {
                    foreach(string imgPath in srcImagePath)
                    {
                        string markSheetImagePath = _imageProcessing.CropImage(imgPath);
                        OCR_Output = await GCP_Vission_Client.LoadImg(markSheetImagePath);
                        IDictionary<string, int> marks = CommonServices.GenerateMarksheetFromOCR(OCR_Output);
                        foreach(KeyValuePair<string, int> value in marks)
                            marksheet.Add(value);
                    }
                } else {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return View(marksheet);
            } catch(Exception ex) {
                if(ex.InnerException != null)
                    OCR_Output = ex.InnerException.Message;
                else
                    OCR_Output = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadScannedImage2(List<string> image)
        {
            string OCR_Output = "";
            IDictionary<string, int> marksheet = new Dictionary<string, int>();

            try
            {
                if (_fileService.SaveImageFile(image, out List<string> srcImagePath))
                {
                    foreach (string imgPath in srcImagePath)
                    {
                        string markSheetImagePath = _imageProcessing.CropImage(imgPath);
                        OCR_Output = await GCP_Vission_Client.LoadImg(markSheetImagePath);
                        IDictionary<string, int> marks = CommonServices.GenerateMarksheetFromOCR(OCR_Output);
                        foreach (KeyValuePair<string, int> value in marks)
                            marksheet.Add(value);
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }

                return View(marksheet);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    OCR_Output = ex.InnerException.Message;
                else
                    OCR_Output = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveMarksheet(IFormCollection formCollection)
        {
            try
            {
                string OCR_Output = "";
                IDictionary<string, int> marksheet = new Dictionary<string, int>();
                List<string> srcImagePath = await _fileService.SaveFormFiles(formCollection.Files);
                foreach (string imgPath in srcImagePath)
                {
                    string markSheetImagePath = _imageProcessing.CropImage(imgPath);
                    OCR_Output = await GCP_Vission_Client.LoadImg(markSheetImagePath);
                    marksheet = CommonServices.GenerateMarksheetFromOCR(OCR_Output);
                }
                return View(marksheet);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
