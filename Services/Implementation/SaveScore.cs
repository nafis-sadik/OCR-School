using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using System.Linq;
using Entities.Models;
using Entities.Application;

namespace Services.Implementation
{
    public class SaveScoreService : ISaveScoreService
    {
        IMarksheetRepo _marksheetRepo;

        public SaveScoreService() => _marksheetRepo = new MarksheetRepo();

        public void SaveScore(Entities.Application.Marksheet markSheet)
        {
            IList<int> Questions = markSheet.Question;
            IList<int> Scores = markSheet.Marks;
            Random randMSID = new Random();

            int randMarkSheetid = _marksheetRepo.AsQueryable().Max(x => x.MarksheetId) + 1;
            for (var i = 0; i < Questions.Count; i++)
            {
                //StudentID and SubjectId will be provided by API
                _marksheetRepo.Add(new Entities.Models.Marksheet
                {
                    MarksheetTableId = _marksheetRepo.AsQueryable().Max(x => x.MarksheetTableId) + i + 1,
                    MarksheetId = randMarkSheetid,
                    StudentId = 1,
                    SubjectId = 1,
                    Question = Questions[i],
                    Marks = Scores[i]
                });
            }
            _marksheetRepo.Save();
        }
    }
}
