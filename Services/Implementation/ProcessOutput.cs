using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Abstraction;
using Entities.Application;

namespace Services.Implementation
{
    public class ProcessOutput : IProcessOutput
    {
        public Marksheet OutputProcessing(IReadOnlyList<dynamic> _response)
        {
            // Data Types
            Marksheet result = new Marksheet(new List<int>(), new List<int>());
            int value = 0;
            string description = "";
            IList<int> collection;

      
            // Processing output
            for (int i = 1; i < _response.Count; i++)
            {
                description = _response[i].Description.ToLower();
                if (description.Contains("question") || description.Contains("score"))
                {
                    collection = description.Contains("question") ? result.Question : result.Marks;
                    if (++i < _response.Count)
                        description = _response[i].Description;
                    else
                    {
                        collection.Add(0);
                        continue;
                    }
                    if (int.TryParse(description, out value))
                    {
                        if (++i >= _response.Count) { continue; }
                        while (int.TryParse(_response[i].Description, out int _value))
                        {
                            value *= 10;
                            value += _value;
                        }
                        collection.Add(value);
                    }
                    else
                    {
                        collection.Add(0);
                    }
                    i--;
                }
                else
                {
                    value = 0;
                    continue;
                }
            }
            return result;
        }
    }
}
