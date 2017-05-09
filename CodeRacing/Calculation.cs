using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRacing
{
    public static class Calculation
    {
        public static double Limit(double a, double max)
        {
            if(Math.Abs(a) > max)
            {
                return Math.Sign(a) * max;
            }
            else
            {
                return a;
            }
        }
    }
}
