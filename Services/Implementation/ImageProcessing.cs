using Repository;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Entities.Models;


namespace Services.Implementation
{
    public class ImageProcessing : IImageProcessing
    {
        private IAnswerscriptlocRepo _answerScriptLocRepo;
        public ImageProcessing()
        {
            _answerScriptLocRepo = new AnswerscriptlocRepo();
        }
        public string CropImage(string srcImagePath)
        {
            float widthPercentile = 0.3f;

            Image img = Image.FromFile(srcImagePath);
            Rectangle CropArea = new Rectangle(x: Convert.ToInt32(img.Width * (1 - widthPercentile)), y: 0, width: img.Width, height: img.Height);
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

        public IEnumerable<string> BulkCropImage(IEnumerable<string> imageforCrop)
        {
            List<string> CroppedImagePaths = new List<string>();
            string croppedImagePath = "";
            int idAnswerScriptLoc = _answerScriptLocRepo.AsQueryable().Max(x => x.IdAnswerScriptLoc) + 1;
            foreach (string savedImages in imageforCrop)
            {
                croppedImagePath = CropImage(savedImages);
                CroppedImagePaths.Add(croppedImagePath);
                
                _answerScriptLocRepo.Add(new Answerscriptloc
                {
                    IdAnswerScriptLoc = ++idAnswerScriptLoc,
                    AnswerScriptLoc1 = savedImages,
                    CropImgLoc = croppedImagePath
                });
            }
            _answerScriptLocRepo.Save();
            return CroppedImagePaths;
        }
    }
}
