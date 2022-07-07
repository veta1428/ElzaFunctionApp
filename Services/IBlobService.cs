using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElzaFunctionApp
{
    internal interface IBlobService
    {
        Task<string> GetDataAsync(string name);
    }
}
