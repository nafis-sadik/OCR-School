using Entities.Application;

namespace Services.Abstraction
{
    public interface IUserSaveScoreService
    {
        bool UserSaveScore(Marksheet marksheet, out string msg);
    }
}
