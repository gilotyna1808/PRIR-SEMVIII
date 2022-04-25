using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Kliencka
{
    class Fibonacci_Task:Tasks
    {
        public string Calculate(List<string> param)
        {
            if(param.Count != 2)
            {
                throw new Exception("Nieprawidlowa ilosc parametrow");
            }
            int l = int.Parse(param[1]);
            int a = 0;
            int b = 1;
            int c = 0;
            for (int i = 2; i < l; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }
            if (l == 1) return "1";
            return c.ToString();
        }
    }
}
