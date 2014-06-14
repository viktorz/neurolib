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
        private double[] errors;
        private ActivationFunction activationFunction;

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

        internal double[] Errors
        {
            get { return this.errors; }
            set { this.errors = value; }
        }

        private double[,] lastWeightDelta;
        public double[,] LastWeightDelta
        {
            get { return lastWeightDelta; }
            set { lastWeightDelta = value; }
        }

        internal ActivationFunction ActivationFunction
        {
            get { return this.activationFunction; }
            set { this.activationFunction = value; }
        }
    }
}
