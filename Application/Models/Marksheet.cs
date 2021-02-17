using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCR_School_Web_App.Models
{
    public class Marksheet
    {
        public IList<int> Question, Marks;
        public Marksheet(IList<int> question, IList<int> marks)
        {
            Question = question;
            Marks = marks;
        }
    }
}
