using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Answerscriptloc
    {
        public Answerscriptloc()
        {
            Main = new HashSet<Main>();
        }

        public int IdAnswerScriptLoc { get; set; }
        public string AnswerScriptLoc1 { get; set; }
        public string CropImgLoc { get; set; }

        public virtual ICollection<Main> Main { get; set; }
    }
}
