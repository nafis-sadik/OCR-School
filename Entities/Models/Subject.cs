using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Marksheet = new HashSet<Marksheet>();
        }

        public int IdSubjec { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Marksheet> Marksheet { get; set; }
    }
}
