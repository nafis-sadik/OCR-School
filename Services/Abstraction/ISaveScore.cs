using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface ISaveScoreService
    {
        public void SaveScore(IList<int> questions, IList<int> scores); 
        public void SaveScore(dynamic questions);
    }
}
