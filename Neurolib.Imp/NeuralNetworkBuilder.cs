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

        public NeuralNetworkBuilder AddInputLayer(int numberOfInputs, ActivationFunction activationFunction, bool addBias)
        {
            if (this.currentNetwork.InputLayer != null)
                throw new NeuralNetworkConfigurationException("You can't have more than one input layers!");

            NeuralNetworkLayer inputLayer = new NeuralNetworkLayer();
            inputLayer.Inputs = new double[numberOfInputs + (addBias ? 1 : 0)];
            inputLayer.Errors = new double[numberOfInputs + (addBias ? 1 : 0)];
            inputLayer.ActivationFunction = activationFunction;
            // postpone initialization of weights
            this.currentNetwork.Layers.Add(inputLayer);
            this.currentNetwork.InputLayer = inputLayer;
            
            return this;
        }

        public NeuralNetworkBuilder AddHiddenLayer(int numberOfNodes, ActivationFunction activationFunction, bool addBias)
        {
            if (this.currentNetwork.InputLayer == null)
                throw new NeuralNetworkConfigurationException("You can't add a hidden layer begore adding an input one!");

            if (this.currentNetwork.OutputLayer != null)
                throw new NeuralNetworkConfigurationException("You can't add a hidden layer after the output one was defined!");

            var previousLayer = this.currentNetwork.Layers.Last();
            previousLayer.Weights = new double[previousLayer.Inputs.Length, numberOfNodes + (addBias ? 1 : 0)];
            previousLayer.LastWeightDelta = new double[previousLayer.Inputs.Length, numberOfNodes + (addBias ? 1 : 0)];

            NeuralNetworkLayer hiddenLayer = new NeuralNetworkLayer();
            hiddenLayer.Inputs = new double[numberOfNodes + (addBias ? 1 : 0)];
            hiddenLayer.Errors = new double[numberOfNodes + (addBias ? 1 : 0)];
            hiddenLayer.ActivationFunction = activationFunction;
            this.currentNetwork.Layers.Add(hiddenLayer);

            return this;
        }

        public NeuralNetworkBuilder AddOutputLayer(int numberOfOutputs, ActivationFunction activationFunction)
        {
            if (this.currentNetwork.InputLayer == null)
                throw new NeuralNetworkConfigurationException("You can't add an output layer before adding and input one");

            if (this.currentNetwork.OutputLayer != null)
                throw new NeuralNetworkConfigurationException("You can't add more than one output layers");

            var previousLayer = this.currentNetwork.Layers.Last();
            previousLayer.Weights = new double[previousLayer.Inputs.Length, numberOfOutputs];
            previousLayer.LastWeightDelta = new double[previousLayer.Inputs.Length, numberOfOutputs];
            
            NeuralNetworkLayer outputLayer = new NeuralNetworkLayer();
            outputLayer.Inputs = new double[numberOfOutputs];
            outputLayer.Errors = new double[numberOfOutputs];
            outputLayer.ActivationFunction = activationFunction;
            outputLayer.Weights = null;
            this.currentNetwork.Layers.Add(outputLayer);
            this.currentNetwork.OutputLayer = outputLayer;

            return this;
        }

        public NeuralNetwork GetNetwork()
        {
            if (this.currentNetwork.InputLayer == null)
                throw new NeuralNetworkConfigurationException("You should add an input and an output layer to your neural network before trying to get it from the builder!");
            if (this.currentNetwork.OutputLayer == null)
                throw new NeuralNetworkConfigurationException("You should add an output layer to your neural network before trying to get it from the builder!");

            this.currentNetwork.Initialize();

            return this.currentNetwork;
        }
    }
}