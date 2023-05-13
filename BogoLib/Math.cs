using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogoLib
{
    public static class BogoMath
    {
        /// <summary>
        /// Randomly calculate the root
        /// </summary>
        /// <param name="x">Number whose root you want to know</param>
        /// <returns>Returns the root if it is exact or -1 otherwise</returns>
        public static double Sqrt(double x)
        {
            if (x < 0) return double.NaN;

            int
                min = 0,
                max = (int)x,
                sqrt = Random.Shared.Next(max),
                exp = sqrt * sqrt;

            while (exp != x)
            {
                if (max - min == 1)
                    return -1;
                else if (exp < x)
                    min = sqrt;
                else
                    max = sqrt;

                sqrt = Random.Shared.Next(min, max);
                exp = sqrt * sqrt;
            }

            return sqrt;
        }
    }
}
