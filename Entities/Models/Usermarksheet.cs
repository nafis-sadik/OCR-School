namespace Entities.Models
{
    public partial class Usermarksheet
    {
        public int IdUserTable { get; set; }
        public int? UserId { get; set; }
        public int? MarksheetId { get; set; }
        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
        public int? Question { get; set; }
        public int? Marks { get; set; }
        public string UserName { get; set; }
    }
}
