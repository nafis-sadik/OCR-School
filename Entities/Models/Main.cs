using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Main
    {
        public int MarksheetId { get; set; }
        public int? AnswerScriptId { get; set; }

        public virtual Answerscriptloc AnswerScript { get; set; }
        public virtual Marksheet Marksheet { get; set; }
    }
}
