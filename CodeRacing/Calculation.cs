using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRacing
{
    public static class Calculation
    {
        public static double LimitChange(double a, double max)
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
        public static double Limit(double v, double limit)
        {
            return Math.Max(-limit, Math.Min(v, limit));
        }
        public static double NormalizeAngle(double angle)
        {
            while (angle > Math.PI)
            {
                angle -= 2.0D * Math.PI;
            }

            while (angle < -Math.PI)
            {
                angle += 2.0D * Math.PI;
            }

            return angle;
        }
    }
}
