using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstraction;
using Repository;
using System.Linq;
using Entities.Models;

namespace Services.Implementation
{
    public class UserSaveScoreService : IUserSaveScoreService
    {
        IUsermarksheetRepo _usermarksheetRepo;
        
        public UserSaveScoreService()
        {
            _usermarksheetRepo = new UsermarksheetRepo();
        }

        public bool UserSaveScore(Entities.Application.Marksheet marksheet, out string msg)
        {
            try
            {
                //StudentID, UserId (System User) and SubjectId will be provided by API
                int _userID = 1;
                int _markSheetId = marksheet.MarkSheetId;
                int idUserTable = _usermarksheetRepo.AsQueryable().Max(x => x.IdUserTable) + 1;

                for (int i = 0; i < marksheet.Question.Count; i++)
                {
                    _usermarksheetRepo.Add(new Usermarksheet
                    {
                        IdUserTable = idUserTable + i, 
                        UserId = _userID,
                        MarksheetId = _markSheetId,
                        StudentId = 1,
                        SubjectId = 1,
                        Question = marksheet.Question[i],
                        Marks = marksheet.Marks[i]
                    });
                }
                _usermarksheetRepo.Save();
                msg = "";
                return true;
            } 
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    msg = ex.Message;
                else
                    msg = ex.InnerException.Message;
                return false;
            }
        }
    }
}
