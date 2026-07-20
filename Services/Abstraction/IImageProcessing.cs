using System.Collections.Generic;

namespace Services.Abstraction
{
    public interface IImageProcessing
    {
        string CropImage(string imageforCrop);
        IEnumerable<string> BulkCropImage(IEnumerable<string> imageforCrop);
    }
}
