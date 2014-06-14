using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public class NeuralNetwork
    {
        private readonly List<NeuralNetworkLayer> layers = new List<NeuralNetworkLayer>();
        private NeuralNetworkLayer inputLayer;
        private NeuralNetworkLayer outputLayer;

        internal NeuralNetwork()
        {
        }

        internal List<NeuralNetworkLayer> Layers
        {
            get { return layers; }
        }

        internal NeuralNetworkLayer InputLayer
        {
            get { return this.inputLayer; }
            set { this.inputLayer = value; }
        }

        internal NeuralNetworkLayer OutputLayer
        {
            get { return this.outputLayer; }
            set { this.outputLayer = value; }
        }

        public double[] Run(double[] inputValues)
        {
            for (int i = 0; i < inputValues.Length; i++)
            {
                this.inputLayer.Inputs[i] = inputValues[i];
            }

            for (int li = 0; li < this.layers.Count - 1; li++)
            {
                NeuralNetworkLayer currentLayer = this.layers[li];
                NeuralNetworkLayer nextLayer = this.layers[li + 1];

                for (int i = 0; i < nextLayer.Inputs.Length; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < currentLayer.Inputs.Length; j++)
                    {
                        sum += currentLayer.Inputs[j] * currentLayer.Weights[j, i];
                    }
                    nextLayer.Inputs[i] = currentLayer.ActivationFunction.Forward(sum);
                }
            }
            return outputLayer.Inputs;
        }

        internal virtual void Initialize()
        {
            Random random;
            unchecked
            {
                random = new Random((int)DateTime.UtcNow.Ticks);
            }
            // TODO: add support for bias values
            for(int li = 0; li < this.layers.Count -1; li++)
            {
                NeuralNetworkLayer currentLayer = this.layers[li];
                NeuralNetworkLayer nextLayer = this.layers[li + 1];

                for (int i = 0; i < currentLayer.Inputs.Length; i++)
                {
                    if (li < this.layers.Count - 1)
                    {
                        for (int j = 0; j < nextLayer.Inputs.Length; j++)
                        {
                            //currentLayer.Weights[i, j] = random.NextDouble() * 4 - 2;
                            currentLayer.Weights[i, j] = random.NextDouble();
                            currentLayer.LastWeightDelta[i, j] = 0.0;
                        }
                    }

                    currentLayer.Errors[i] = double.MinValue;
                }
            }
        }
    }
}

