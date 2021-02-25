using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IFileService
    {
        Task<IEnumerable<string>> SaveFormFiles(IFormFileCollection files);
    }
}
