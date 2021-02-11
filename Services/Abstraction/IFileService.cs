using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IFileService
    {
        bool SaveImageFile(List<string> image, out List<string> ResponseMsg);
        Task<List<string>> SaveFormFiles(IFormFileCollection files);
    }
}
