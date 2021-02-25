using Entities.Application;

namespace Services.Abstraction
{
    public interface ISaveScoreService
    {
        public int SaveScore(Marksheet markSheet);
    }
}
