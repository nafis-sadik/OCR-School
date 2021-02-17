﻿using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Marksheet
    {
        public Marksheet()
        {
            Main = new HashSet<Main>();
        }

        public int? StudentId { get; set; }
        public int MarksheetId { get; set; }
        public int? Question { get; set; }
        public int? Marks { get; set; }
        public int? SubjectId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Main> Main { get; set; }
    }
}
