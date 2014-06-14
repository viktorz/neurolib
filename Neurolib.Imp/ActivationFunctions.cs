using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public static class ActivationFunctions
    {
        private static readonly Func<double, double> sigmoidFunc = x => 1.0 / (1.0 + Math.Pow(Math.E, -x));
        public static readonly ActivationFunction Sigmoid = new ActivationFunction(sigmoidFunc,
                                                                                   x=> sigmoidFunc(x) * (1 - sigmoidFunc(x)));

        public static readonly ActivationFunction Identity = new ActivationFunction(x => x,
                                                                                    x => 1);
    }

    public class ActivationFunction
    {
        public readonly Func<double, double> Forward;
        public readonly Func<double, double> Reverse;

        public ActivationFunction(Func<double, double> forward, Func<double, double> reverse)
        {
            this.Forward = forward;
            this.Reverse = reverse;
        }
    }
}