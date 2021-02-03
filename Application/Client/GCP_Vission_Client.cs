using System;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;


namespace OCR_School_Web_App.Client
{
    public static class GCP_Vission_Client
    {
       
        
        public static string ImageText
        {
            get { return ImageText; }
            set { ImageText = value; }
        }
       

        public static string LoadImg(string imgPath)
        {
           
            Image image = Image.FromFile(imgPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> response = client.DetectText(image);

            string result = "";

            for (int i = 1; i < response.Count; i++)
            {
                if (response[i].Description != null)
                    result += response[i].Description;                
            }
            return result;           
        }
    }
}
