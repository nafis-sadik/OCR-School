using Services.Abstraction;
using System;
using System.Collections.Generic;
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
                string fileName = "";
                string _path = @"D:/OCR-School/Services/Images/" + fileName + ".jpg";
                Image _image;

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(image)))
                {
                    _image = Image.FromStream(ms);
                    _image.Save(_path);
                }

                ResponseMsg = "Successful";
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
