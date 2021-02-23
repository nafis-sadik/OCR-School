using Entities.Models;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Services.Abstraction
{
    public interface IProcessOutput
    {
        dynamic OutputProcessing(IReadOnlyList<dynamic> _response);
    }
}
