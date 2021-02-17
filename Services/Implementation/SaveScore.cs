using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using System.Linq;
using Entities.Models;

namespace Services.Implementation
{
     public class SaveScoreService : ISaveScoreService
    {
        private IMarksheetRepo _marksheetRepo;

        public SaveScoreService()
        {
            _marksheetRepo = new MarksheetRepo();
        }
        public void SaveScore(IList<int> questions , IList<int> scores) //List <dynamic> Marks = new List<dynamic>()(List<dynamic> questions ,List<dynamic> scores)
        {
            for (var i=0; i<=questions.Count; i++)
            {
                _marksheetRepo.Add(new Entities.Models.Marksheet
                {
                    MarksheetId = _marksheetRepo.AsQueryable().Max(x => x.MarksheetId),
                    StudentId = _marksheetRepo.AsQueryable().Max(x=> x.StudentId),
                    SubjectId = _marksheetRepo.AsQueryable().Max(x => x.SubjectId),
                    Question = questions[i],
                    Marks = scores[i]                  
                }) ;
            }
        }
        public void SaveScore(dynamic Marksheet)
        {
            IList<int> Questions = Marksheet.Question;
            IList<int> Scores = Marksheet.Marks;
            int marksheetId = _marksheetRepo.AsQueryable().Max(x => x.MarksheetId) + 1;
            for (var i = 0; i <= Questions.Count; i++)
            {
                _marksheetRepo.Add(new Marksheet
                {
                    MarksheetId = marksheetId,
                    //StudentId = _marksheetRepo.AsQueryable().Max(x => x.StudentId),
                    //SubjectId = _marksheetRepo.AsQueryable().Max(x => x.SubjectId),
                    StudentId = i,
                    SubjectId = i,
                    Question = Questions[i],
                    Marks = Scores[i]
                });
            }
        }
    }
}
