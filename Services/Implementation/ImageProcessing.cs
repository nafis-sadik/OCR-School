using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Services.Implementation
{
    public class ImageProcessing : IImageProcessing
    {
        public string CropImage(string srcImagePath)
        {
            float widthPercentile = 0.25f;
          
            Image img = Image.FromFile(srcImagePath);
            Rectangle CropArea = new Rectangle(x: Convert.ToInt32(img.Width * (1-widthPercentile)), y: 0, width: img.Width, height: img.Height);
            string markSheetForOCRPath = "";
            try
            {
                using (Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        graphics.DrawImage(img, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                    }

                    markSheetForOCRPath = @"D:\OCR-School\Images\Marksheet\MarkSheetForOCR_" + Stopwatch.GetTimestamp().ToString() + ".bmp";
                    bitMap.Save(markSheetForOCRPath);
                }

                return markSheetForOCRPath;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return ex.Message;
                else
                    return ex.InnerException.Message;

            }
        }
    }
}
