﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Abstraction;
using Services.Implementation;
using OCR_School_Web_App.Client;
using System.Net;
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
            _userSaveScoreService = new UserSaveScoreService();
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
                marksheet.MarkSheetId = _saveScore.SaveScore(marksheet);
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

                var questions = formCollection["Question"];
                var marks = formCollection["Marks"];
                int val = 0;
                // scribe data from formCollection
                for (int i = 0; i < questions.Count; i++)
                {
                    if (int.TryParse(questions[i], out val))
                        marksheet.Question.Add(val);
                    if (int.TryParse(marks[i], out val))
                        marksheet.Marks.Add(val);
                }

                if (int.TryParse(formCollection["MarkSheetId"], out val))
                    marksheet.MarkSheetId = val;

                if (_userSaveScoreService.UserSaveScore(marksheet, out string msg))
                    return Ok("<script> "+
                        "Swal.fire({title:'Submitted Successfully! Database Updated!',html:'Alert will closed in <b></b> seconds.',timer:9e3,timerProgressBar:!0,didOpen:()=>{Swal.showLoading(),timerInterval=setInterval(()=>{const e=Swal.getContent();if(e){const t=e.querySelector('b');t&&(t.textContent=Math.floor(Swal.getTimerLeft()/1e3)+1)}},100)},willClose:()=>{clearInterval(timerInterval),alert('success')}}).then(e=>{e.dismiss===Swal.DismissReason.timer&&console.log('Alert Closed by Timer : Submission Successful')});"
                        + "</script>");
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
