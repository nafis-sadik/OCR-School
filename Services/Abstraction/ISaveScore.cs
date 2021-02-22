using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface ISaveScoreService
    {
        public void SaveScore(dynamic questions);
    }
}
