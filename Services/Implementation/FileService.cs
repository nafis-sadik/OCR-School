using Microsoft.AspNetCore.Http;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
    }
}
