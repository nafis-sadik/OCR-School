using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Student
    {
        public Student()
        {
            Marksheet = new HashSet<Marksheet>();
        }

        public int StudentId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Marksheet> Marksheet { get; set; }
    }
}
