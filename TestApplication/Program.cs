using OCR_School_Web_App.Client;
using OCR_School_Web_App.Controllers;
using OCR_School_Web_App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Marksheet response = ProcessOutput.OutputProcessing("D:/OCR-School/Images/Marksheet/MarkSheetForOCR_33895542679.bmp").Result;
                
                for (int i = 0; i < response.Question.Count; i++)
                {
                    Console.Write(response.Question[i]);
                    Console.Write(" <====> ");
                    Console.WriteLine(response.Marks[i]);
                }
            }
            catch (Exception ex) {
                if (!string.IsNullOrWhiteSpace(ex.Message))
                    Console.WriteLine(ex.Message);
                else
                    Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
