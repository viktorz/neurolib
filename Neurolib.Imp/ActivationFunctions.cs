using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public static class ActivationFunctions
    {
        public static readonly Func<double, double> Sigmoid = x => 1.0 / (1.0 + Math.Pow(Math.E, -x));
    }
}
