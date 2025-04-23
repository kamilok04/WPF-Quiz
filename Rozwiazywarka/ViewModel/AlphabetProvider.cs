using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Rozwiazywarka.ViewModel
{
    public class AlphabetProvider : IAlphabetProvider
    {
        public IEnumerable<char> GetAlphabet()
        { for (char c = 'A'; c <= 'Z'; c++) yield return c; }
            
        
    }
}
