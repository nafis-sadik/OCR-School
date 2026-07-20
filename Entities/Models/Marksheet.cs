namespace Entities.Models
{
    public partial class Marksheet
    {
        public int? StudentId { get; set; }
        public int MarksheetId { get; set; }
        public int? Question { get; set; }
        public int? Marks { get; set; }
        public int? SubjectId { get; set; }
        public int MarksheetTableId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
