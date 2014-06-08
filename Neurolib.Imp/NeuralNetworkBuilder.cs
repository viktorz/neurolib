using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public class NeuralNetworkBuilder
    {
        protected NeuralNetwork currentNetwork = null;

        public NeuralNetworkBuilder()
        {
        }

        public NeuralNetworkBuilder CreateNew()
        {
            this.currentNetwork = new NeuralNetwork();

            return this;
        }

        public NeuralNetworkBuilder AddInputLayer(int numberOfInputs, Func<double, double> activationFunction, bool addBias)
        {
            NeuralNetworkLayer inputLayer = new NeuralNetworkLayer();
            inputLayer.Inputs = new double[numberOfInputs + (addBias ? 1 : 0)];
            inputLayer.ActivationFunction = activationFunction;
            // postpone initialization of weights
            this.currentNetwork.Layers.Add(inputLayer);
            this.currentNetwork.InputLayer = inputLayer;
            
            return this;
        }

        public NeuralNetworkBuilder AddHiddenLayer(int numberOfNodes, Func<double, double> activationFunction, bool addBias)
        {
            var previousLayer = this.currentNetwork.Layers.Last();
            previousLayer.Weights = new double[previousLayer.Inputs.Length, numberOfNodes + (addBias ? 1 : 0)];

            NeuralNetworkLayer hiddenLayer = new NeuralNetworkLayer();
            hiddenLayer.Inputs = new double[numberOfNodes + (addBias ? 1 : 0)];
            hiddenLayer.ActivationFunction = activationFunction;
            this.currentNetwork.Layers.Add(hiddenLayer);

            return this;
        }

        public NeuralNetworkBuilder AddOutputLayer(int numberOfOutputs, Func<double, double> activationFunction)
        {
            var previousLayer = this.currentNetwork.Layers.Last();
            previousLayer.Weights = new double[previousLayer.Inputs.Length, numberOfOutputs];

            NeuralNetworkLayer outputLayer = new NeuralNetworkLayer();
            outputLayer.Inputs = new double[numberOfOutputs];
            outputLayer.ActivationFunction = activationFunction;
            outputLayer.Weights = null;
            this.currentNetwork.Layers.Add(outputLayer);
            this.currentNetwork.OutputLayer = outputLayer;

            return this;
        }

        public NeuralNetwork GetNetwork()
        {
            this.currentNetwork.Initialize();

            return this.currentNetwork;
        }
    }
}