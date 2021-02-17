using System;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using OCR_School_Web_App.Models;

namespace OCR_School_Web_App.Client
{
    public static class GCP_Vission_Client
    {
        public static async Task<Marksheet> LoadImg(string imgPath)
        {
            // Requesting GCP
            Image image = Image.FromFile(imgPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> response = client.DetectText(image);

            // Data Types
            Marksheet result = new Marksheet(new List<int>(), new List<int>());
            int value = 0;
            string description = "";
            IList<int> collection;

            // Processing output
            for (int i = 1; i < response.Count; i++)
            {
                description = response[i].Description.ToLower();
                if (description.Contains("question") || description.Contains("score"))
                {
                    collection = description.Contains("question") ? result.Question : result.Marks;
                    if (++i < response.Count)
                        description = response[i].Description;
                    else
                        continue;
                    if (int.TryParse(description, out value))
                    {
                        if (++i >= response.Count) { continue; }
                        while (int.TryParse(response[i].Description, out int _value))
                        {
                            value *= 10;
                            value += _value;
                        }
                        collection.Add(value);
                    }
                    else
                    {
                        collection.Add(0);
                    }
                    i--;
                }
                else
                {
                    value = 0;
                    continue;
                }
            }
            return result;
        }
    }
}
