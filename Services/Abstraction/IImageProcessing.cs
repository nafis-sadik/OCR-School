using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface IImageProcessing
    {
        string CropImage(string imageforCrop);
    }
}
