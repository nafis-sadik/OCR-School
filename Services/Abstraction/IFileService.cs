using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface IFileService
    {
        bool SaveImageFile(string image, out string ResponseMsg);
    }
}
