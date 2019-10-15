using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Application
{
    public static class Maths
    {
        public static int GetNegativeValue(int Value)
        {
            int NegativeValue = Math.Abs(Value) * (-1);

            return NegativeValue;
        }
        public static decimal GetNegativeValue(decimal Value)
        {
            decimal NegativeValue = Math.Abs(Value) * (-1);

            return NegativeValue;
        }
        public static double GetNegativeValue(double Value)
        {
            double NegativeValue = Math.Abs(Value) * (-1);

            return NegativeValue;
        }

    }
}
