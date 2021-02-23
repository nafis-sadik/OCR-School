using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IMarksheetRepo : IRepositoryBase<Marksheet> { }
    public class MarksheetRepo : RepositoryBase<Marksheet>, IMarksheetRepo
    {
        public MarksheetRepo() : base() { }
    }
    public interface IAnswerscriptlocRepo : IRepositoryBase<Answerscriptloc> { }
    public class AnswerscriptlocRepo : RepositoryBase<Answerscriptloc>, IAnswerscriptlocRepo
    {
        public AnswerscriptlocRepo() : base() { }
    }

    public interface IStudentRepo : IRepositoryBase<Student> { }
    public class StudentRepo : RepositoryBase<Student>, IStudentRepo
    {
        public StudentRepo() : base() { }
    }

    public interface ISubjectRepo : IRepositoryBase<Subject> { }
    public class SubjectRepo : RepositoryBase<Subject>, ISubjectRepo
    {
        public SubjectRepo() : base() { }
    }

    public interface IMainRepo : IRepositoryBase<Main> { }
    public class MainRepo : RepositoryBase<Main>, IMainRepo
    {
        public MainRepo() : base() { }
    }

    public interface IUsermarksheetRepo : IRepositoryBase<Usermarksheet> { }
    public class UsermarksheetRepo : RepositoryBase<Usermarksheet>, IUsermarksheetRepo
    {
        public UsermarksheetRepo() : base() { }
    }

}
