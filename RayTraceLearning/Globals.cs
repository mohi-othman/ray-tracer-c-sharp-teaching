//This class contains global values used by the whole library
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemDown.RayTracer
{
    public class Globals
    {
        //Value to represent infinite depth
        public static double Infinity = double.PositiveInfinity;

        //Value to reprsent a very small value used in some calculation to avoid graphic 
        //artifacts due to number precision errors.
        public static double Epsilon = 0.0001d;
    }
}
