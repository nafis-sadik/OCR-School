using Services.Abstraction;
using System.Collections.Generic;
using Repository;
using System.Linq;


namespace Services.Implementation
{
    public class SaveScoreService : ISaveScoreService
    {
        IMarksheetRepo _marksheetRepo;

        public SaveScoreService() => _marksheetRepo = new MarksheetRepo();

        public int SaveScore(Entities.Application.Marksheet markSheet)
        {
            IList<int> Questions = markSheet.Question;
            IList<int> Scores = markSheet.Marks;

            int markSheetid = _marksheetRepo.AsQueryable().Max(x => x.MarksheetId) + 1;
            for (int i = 0; i < Questions.Count; i++)
            {
                //StudentID and SubjectId will be provided by API
                _marksheetRepo.Add(new Entities.Models.Marksheet
                {
                    MarksheetTableId = _marksheetRepo.AsQueryable().Max(x => x.MarksheetTableId) + i + 1,
                    MarksheetId = markSheetid,
                    StudentId = 1,
                    SubjectId = 1,
                    Question = Questions[i],
                    Marks = Scores[i]
                });
            }
            _marksheetRepo.Save();
            return markSheetid;
        }
    }
}
