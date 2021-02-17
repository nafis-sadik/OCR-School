using Microsoft.AspNetCore.Http;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class FileService : IFileService
    {
        private IImageProcessing _imageProcessing;
        public FileService()
        {
            _imageProcessing = new ImageProcessing();
        }
        public async Task<IEnumerable<string>> SaveFormFiles(IFormFileCollection files)
        {
            List<string> SavedImagePaths = new List<string>();
            string filePath = "";
            foreach (var file in files)
            {
                filePath = @"D:\OCR-School\Images\AnswerScript\AnswerScript_" + Stopwatch.GetTimestamp().ToString() + "_" + file.FileName;
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                SavedImagePaths.Add(filePath);
            }
            IEnumerable<string> CropedImagePaths = _imageProcessing.BulkCropImage(SavedImagePaths);
            return CropedImagePaths;
        }

        public bool SaveImageFile(List<string> images, out List<string> ResponseMsg)
        {
            ResponseMsg = new List<string>();
            try
            {
                string path = "";
                Image _image;

                foreach (string image in images)
                {
                    
                    var trimmed = image.Trim();
                    string final = trimmed.Substring(23, trimmed.Length - 23);

                    //byte [] base64confirm = Convert.FromBase64String(final);
                    var dada = Convert.FromBase64String(final);
                    

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(final)))
                    {
                        path = @"D:\OCR-School\Images\AnswerScript\AnswerScript_" + Stopwatch.GetTimestamp().ToString() + ".jpg";
                        _image = Image.FromStream(ms);
                        _image.Save(path);
                        ResponseMsg.Add(path);
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    ResponseMsg.Add(ex.Message);
                else
                    ResponseMsg.Add(ex.InnerException.Message);
                return false;
            }
        }
    }
}
