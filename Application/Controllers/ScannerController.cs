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
using Microsoft.AspNetCore.Http;
using Entities.Application;

namespace OCR_School_Web_App.Controllers
{
    public class ScannerController : Controller
    {
        IFileService _fileService;
        ISaveScoreService _saveScore;
        GCP_Vission_Client gcpClient;
        IUserSaveScoreService _userSaveScoreService;

        public ScannerController()
        {
            _fileService = new FileService();
            _saveScore = new SaveScoreService();
            gcpClient = new GCP_Vission_Client();
            _userSaveScoreService = new UserSaveScoreServive();
        }

        [HttpPost]
        public async Task<IActionResult> UploadScannedImage(List<string> image)
        {
            string OCR_Output = "";
            Marksheet marksheet = new Marksheet(new List<int>(), new List<int>());

            try
            {
                if (_fileService.SaveImageFile(image, out List<string> srcImagePath))
                {
                    foreach (string imgPath in srcImagePath)
                    {
                        //string markSheetImagePath = _imageProcessing.CropImage(imgPath);

                        //marksheet = await new GCP_Vission_Client().LoadImg(markSheetImagePath);
                        // implementation requires to be removed = CommonServices.GenerateMarksheetFromOCR(OCR_Output);
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
        public async Task<IActionResult> UploadScannedImage2(List<string> image)
        {
            string OCR_Output = "";
            Marksheet marksheet = new Marksheet(new List<int>(), new List<int>());

            try
            {
                if (_fileService.SaveImageFile(image, out List<string> srcImagePath))
                {
                    foreach (string imgPath in srcImagePath)
                    {
                        //string markSheetImagePath = _imageProcessing.CropImage(imgPath);
                        //marksheet = await new GCP_Vission_Client().LoadImg(markSheetImagePath);
                        // CommonServices.GenerateMarksheetFromOCR(OCR_Output);
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
        public async Task<IActionResult> ProcessMarksheet(IFormCollection formCollection)
        {
            try
            {
                Marksheet marksheet = new Marksheet(new List<int>(), new List<int>());
                IEnumerable<string> srcImagePath = await _fileService.SaveFormFiles(formCollection.Files);
                foreach (string imgPath in srcImagePath)
                {
                    marksheet = gcpClient.OCR_Client(imgPath);
                }
                _saveScore.SaveScore(marksheet);
                return View(marksheet);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult UserSubmit(IFormCollection formCollection)
        {
            try
            {
                Marksheet marksheet = new Marksheet(new List<int>(), new List<int>());
                marksheet.MarkSheetId = new int();


                // scribe data from formCollection
                while ()
                {
                    if (int.TryParse(formCollection[""], out int val))
                        marksheet.Question.Add(val);
                    if (int.TryParse(formCollection[""], out val))
                        marksheet.Marks.Add(val);
                }


                if (_userSaveScoreService.UserSaveScore(marksheet, out string msg))
                    return Ok();
                else
                    return Problem(msg);
            }
            catch(Exception ex)
            {
                if (ex.InnerException == null)
                    return Problem(ex.Message);
                else
                    return Problem(ex.InnerException.Message);
            }
            throw new NotImplementedException();
        }
    }
}
