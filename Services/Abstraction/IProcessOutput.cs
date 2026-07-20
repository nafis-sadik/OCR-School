using Entities.Application;
using System.Collections.Generic;



namespace Services.Abstraction
{
    public interface IProcessOutput
    {
        Marksheet OutputProcessing(IReadOnlyList<dynamic> _response);
    }
}
