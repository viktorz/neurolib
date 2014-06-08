using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    internal class NeuralNetworkLayer
    {
        private double[] inputs;
        private double[,] weights;
        private Func<double, double> activationFunction;

        public NeuralNetworkLayer()
        {
        }

        internal double[] Inputs
        {
            get { return inputs; }
            set { inputs = value; }
        }

        internal double[,] Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        internal Func<double, double> ActivationFunction
        {
            get { return this.activationFunction; }
            set { this.activationFunction = value; }
        }
    }
}
