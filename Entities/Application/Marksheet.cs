using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Application
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
