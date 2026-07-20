using System;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using Services.Abstraction;
using Services.Implementation;
using Entities.Application;

namespace OCR_School_Web_App.Client
{
    public class GCP_Vission_Client
    {
        private readonly IProcessOutput _processOutput;
        public GCP_Vission_Client()
        {
            _processOutput = new ProcessOutput();
        }

        private bool StringToInt(string str, out int val)
        {
            val = 0;
            foreach (char ch in str)
            {
                if (@"0123456789".Contains(ch))
                {
                    val *= 10;
                    val += Convert.ToInt32(ch.ToString());
                }
            }
            if (val > 0) return true;
            else return false;
        }
        public async Task<Marksheet> LoadImg(string imgPath)
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
            IList<int> alternativeCollection = null;
            string nextExpectedType = "";

            // Processing output
            for (int i = 1; i < response.Count; i++)
            {
                description = response[i].Description.ToLower();
                if (description.Contains("question") || description.Contains("score"))
                {
                    // Loading Collection
                    if(string.IsNullOrEmpty(nextExpectedType) && !description.Contains("question")) {
                        alternativeCollection = result.Question;
                        collection = result.Marks;
                        nextExpectedType = "question";
                    } 
                    else if (description.Contains("question") && nextExpectedType != "question" && !string.IsNullOrEmpty(nextExpectedType)) {
                        collection = result.Question;
                        alternativeCollection = result.Marks;
                        nextExpectedType = "score";
                    } 
                    else if(description.Contains("score") && nextExpectedType != "score" && !string.IsNullOrEmpty(nextExpectedType)) {
                        collection = result.Marks;
                        alternativeCollection = result.Question;
                        nextExpectedType = "question";
                    } 
                    else {
                        if (description.Contains("question"))
                        {
                            collection = result.Question;
                            nextExpectedType = "score";
                        }
                        else {
                            collection = result.Marks;
                            nextExpectedType = "question";
                        }
                    }

                    // Moving to next description object to retrieve value
                    if (++i < response.Count)
                        description = response[i].Description;
                    else
                        continue;

                    if (StringToInt(description, out value))
                    {
                        // Checking if next exists. If does, it might be a score seperated by space while scanning 
                        if (++i >= response.Count) { continue; }
                        bool flag = false;
                        if(StringToInt(response[i].Description, out int _value))
                        {
                            while (StringToInt(response[i].Description, out _value))
                            {
                                value *= 10;
                                value += _value;
                                i++;
                                flag = true;
                            }
                        } 
                        else
                            if (!flag) {
                                i--;
                                flag = false;
                            }
                        
                        collection.Add(value);
                        if (alternativeCollection != null)
                            alternativeCollection.Add(0);
                    }
                    else
                    {
                        collection.Add(0);
                        if (alternativeCollection != null)
                            alternativeCollection.Add(0);
                        i--;
                    }
                }
                else
                {
                    value = 0;
                    continue;
                }
            }


            while (result.Question.Count > result.Marks.Count)
            {
                result.Marks.Add(0);
            }
            return result;
        }

        public Marksheet OCR_Client(string imgPath)
        {
            // Requesting GCP
            Marksheet marksheet = new Marksheet(new List<int>(), new List<int>());
            Image image = Image.FromFile(imgPath);
            ImageAnnotatorClient client = ImageAnnotatorClient.Create();
            IReadOnlyList<EntityAnnotation> response = client.DetectText(image);
            marksheet = _processOutput.OutputProcessing(response);
            return marksheet;
        }
    }
}