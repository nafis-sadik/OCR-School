//using System;
//using Google.Cloud.Vision.V1;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using OCR_School_Web_App.Models;
//using OCR_School_Web_App.Client;

//namespace OCR_School_Web_App.Controllers
//{
//    public class ProcessOutput
//    {
//        public static async Task<Marksheet> OutputProcessing(string pathToImage)
//        {
//            IReadOnlyList<EntityAnnotation> _response;
//            _response = await GCP_Vission_Client.OCR_Client(pathToImage);
//            // Data Types
//            Marksheet result = new Marksheet(new List<int>(), new List<int>());
//            int value = 0;
//            string description = "";
//            IList<int> collection;

//            // Processing output
//            for (int i = 1; i < _response.Count; i++)
//            {
//                description = _response[i].Description.ToLower();
//                if (description.Contains("question") || description.Contains("score"))
//                {
//                    collection = description.Contains("question") ? result.Question : result.Marks;
//                    if (++i < _response.Count)
//                        description = _response[i].Description;
//                    else
//                    {
//                        collection.Add(0);
//                        continue;
//                    }
//                    if (int.TryParse(description, out value))
//                    {
//                        if (++i >= _response.Count) { continue; }
//                        while (int.TryParse(_response[i].Description, out int _value))
//                        {
//                            value *= 10;
//                            value += _value;
//                        }
//                        collection.Add(value);
//                    }
//                    else
//                    {
//                        collection.Add(0);
//                    }
//                    i--;
//                }
//                else
//                {
//                    value = 0;
//                    continue;
//                }
//            }
//            return result;

          

//        }
//    }
//}
