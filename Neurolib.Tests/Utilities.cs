using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Tests
{
    public static class Utilities
    {
        public static bool IsEqual(this double value, double targetValue, double delta)
        {
            double diff = Math.Abs(value - targetValue);
            return diff < delta;
        }

        public static string ToCsvString(this double[] array)
        {
            StringBuilder builder = new StringBuilder(array.Length * 10);
            foreach (var value in array)
            {
                builder.Append(value);
                builder.AppendLine(";");
            }
            return builder.ToString();
        }
    }
}
