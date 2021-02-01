using System;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;


namespace OCR_School_Web_App.Client
{
    public class Client
    {
        public static string[] textFromImage = { };

        
        
        public static string ImageText
        {
            get { return ImageText; }
            set { ImageText = value; }
        }
       

        public static void LoadImg(string imgPath)
        {
           
            Image image = Image.FromFile(imgPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> response = client.DetectText(image);

            //string[] textOutput = { };

            for (int i = 1; i < response.Count; i++)
            {
                
                if (response[i].Description != null)
                   Console.WriteLine(response[i].Description);
                 textFromImage[i] = response[i].Description;
                ImageText = response[i].Description;
                  
                                      
            }
            
            Console.WriteLine(textFromImage);
        }
    }
}
