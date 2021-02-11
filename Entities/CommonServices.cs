using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public static class CommonServices
    {
        /*
         * For our provided hard copy OCR is meant to return output in the following format
         * ScoreQ15ScoreQ26ScoreQ310ScoreQ4
         * After split by Score you get the following format
         * "Q15""Q26""Q310""Q4"
         */
        public static IDictionary<string, int> GenerateMarksheetFromOCR(string OCR_Output)
        {
            IDictionary<string, int> response = new Dictionary<string, int>();
            string[] Outputs = OCR_Output.Split("Score");

            for (int i = 0; i < Outputs.Length; i++)
            {
                response.Add(Outputs[i], i);
            }
            response.Add("Nafis", 5);

            return response;
        }
    }
}
