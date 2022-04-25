using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Kliencka
{
    class IsPrimeTask : Tasks
    {
        public string Calculate(List<string> param)
        {
            if (param.Count != 2)
            {
                throw new Exception("Nieprawidlowa ilosc parametrow");
            }
            int l = int.Parse(param[1]);
            
            if (l < 2) return "false";
            for (int i = 2; i < l/2; i++)
            {
                if (l % i == 0) return "false";
            }
            return "true";
        }
    }
}
