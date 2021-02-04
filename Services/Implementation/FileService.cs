using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace Services.Implementation
{
    public class FileService : IFileService
    {
        public bool SaveImageFile(string image, out string ResponseMsg)
        {
            try
            {
                string fileName = Stopwatch.GetTimestamp().ToString();
                string _path = @"D:\OCR-School\Images\AnswerScript\AnswerScript_" + fileName + ".jpg";
                Image _image;

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(image)))
                {
                    _image = Image.FromStream(ms);
                    _image.Save(_path);
                }

                ResponseMsg = _path;
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    ResponseMsg = ex.Message;
                else
                    ResponseMsg = ex.InnerException.Message;
                return false;
            }
        }
    }
}
