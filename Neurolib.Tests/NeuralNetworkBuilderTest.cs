using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neurolib.Imp;

namespace Neurolib.Tests
{
    [TestClass]
    public class NeuralNetworkBuilderTest
    {
        [TestMethod]
        public void CreateNew_ShouldPrepareANewInstance()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            Assert.IsNotNull(network1);

            builder.CreateNew();
            NeuralNetwork network2 = builder.CurrentNeuralNetwork;
            Assert.IsNotNull(network2);

            Assert.AreNotSame(network1, network2);
        }

        [TestMethod]
        public void AddInputLayer_ShouldAddInputLayer()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, false);
            Assert.AreEqual(1, network1.Layers.Count);
            Assert.IsNotNull(network1.InputLayer);
            Assert.AreEqual(3, network1.InputLayer.Inputs.Length);
            Assert.AreEqual(func, network1.InputLayer.ActivationFunction);
            Assert.IsNull(network1.InputLayer.Weights);
        }

        [TestMethod]
        public void AddInputLayerWithBias_ShouldAddBiasNode()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, true);
            Assert.AreEqual(1, network1.Layers.Count);
            Assert.IsNotNull(network1.InputLayer);
            Assert.AreEqual(4, network1.InputLayer.Inputs.Length);
            Assert.AreEqual(func, network1.InputLayer.ActivationFunction);
            Assert.IsNull(network1.InputLayer.Weights);
        }

        [TestMethod]
        public void AddInputLayer_CalledTwice_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, true);
            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.AddInputLayer(2, func, true), "can't have 2 input layers");
        }

        [TestMethod]
        public void AddHiddenLayer_WithInputLayer_ShouldAddHiddenLayer()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, false)
                   .AddHiddenLayer(4, func, false);

            Assert.AreEqual(2, network1.Layers.Count);
            Assert.AreEqual(4, network1.Layers[1].Inputs.Length);
            Assert.AreEqual(func, network1.Layers[1].ActivationFunction);
            Assert.IsNull(network1.Layers[1].Weights);
            Assert.IsNotNull(network1.InputLayer.Weights);
            Assert.AreEqual(3, network1.InputLayer.Weights.GetLength(0));
            Assert.AreEqual(4, network1.InputLayer.Weights.GetLength(1));
        }

        [TestMethod]
        public void AddHiddenLayerWithBias_ShouldAddBiasNode()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, false)
                   .AddHiddenLayer(4, func, true);

            Assert.AreEqual(2, network1.Layers.Count);
            Assert.AreEqual(5, network1.Layers[1].Inputs.Length);
            Assert.AreEqual(func, network1.Layers[1].ActivationFunction);
            Assert.IsNull(network1.Layers[1].Weights);
            Assert.IsNotNull(network1.InputLayer.Weights);
            Assert.AreEqual(3, network1.InputLayer.Weights.GetLength(0));
            Assert.AreEqual(5, network1.InputLayer.Weights.GetLength(1));
        }

        [TestMethod]
        public void AddHiddenLayer_WithoutInputLayer_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.AddHiddenLayer(2, func, true), "can't have only hidden layer");

        }

        [TestMethod]
        public void AddHiddenLayer_ThreeTimes_ShouldAddThreeHiddenLayers()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func1 = new ActivationFunction(x => x, x => 1);
            ActivationFunction func2 = new ActivationFunction(x => 2*x, x => 1);
            ActivationFunction func3 = new ActivationFunction(x => 3*x, x => 1);
            ActivationFunction func4 = new ActivationFunction(x => 4 * x, x => 1);
            builder.AddInputLayer(2, func1, false)
                   .AddHiddenLayer(3, func2, false)
                   .AddHiddenLayer(4, func3, false)
                   .AddHiddenLayer(5, func4, false);

            Assert.AreEqual(4, network1.Layers.Count);
            Assert.IsNotNull(network1.InputLayer.Weights);
            Assert.AreEqual(2, network1.InputLayer.Weights.GetLength(0));
            Assert.AreEqual(3, network1.InputLayer.Weights.GetLength(1));

            NeuralNetworkLayer hidden1 = network1.Layers[1];
            Assert.AreEqual(3, hidden1.Inputs.Length);
            Assert.AreEqual(func2, hidden1.ActivationFunction);
            Assert.IsNotNull(hidden1.Weights);
            Assert.AreEqual(3, hidden1.Weights.GetLength(0));
            Assert.AreEqual(4, hidden1.Weights.GetLength(1));

            NeuralNetworkLayer hidden2 = network1.Layers[2];
            Assert.AreEqual(4, hidden2.Inputs.Length);
            Assert.AreEqual(func3, hidden2.ActivationFunction);
            Assert.IsNotNull(hidden2.Weights);
            Assert.AreEqual(4, hidden2.Weights.GetLength(0));
            Assert.AreEqual(5, hidden2.Weights.GetLength(1));

            NeuralNetworkLayer hidden3 = network1.Layers[3];
            Assert.AreEqual(5, hidden3.Inputs.Length);
            Assert.AreEqual(func4, hidden3.ActivationFunction);
            Assert.IsNull(hidden3.Weights);
        }

        [TestMethod]
        public void AddOutputLayer_ShouldAddOutputLayer()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func1 = new ActivationFunction(x => x, x => 1);
            ActivationFunction func2 = new ActivationFunction(x => 2 * x, x => 1);
            builder.AddInputLayer(3, func1, false)
                   .AddOutputLayer(2, func2);

            Assert.AreEqual(2, network1.Layers.Count);
            Assert.IsNotNull(network1.InputLayer);
            Assert.AreEqual(3, network1.InputLayer.Inputs.Length);
            Assert.AreEqual(func1, network1.InputLayer.ActivationFunction);
            Assert.IsNotNull(network1.InputLayer.Weights);
            Assert.AreEqual(3, network1.InputLayer.Weights.GetLength(0));
            Assert.AreEqual(2, network1.InputLayer.Weights.GetLength(1));

            Assert.IsNotNull(network1.OutputLayer);
            Assert.AreEqual(2, network1.OutputLayer.Inputs.Length);
            Assert.AreEqual(func2, network1.OutputLayer.ActivationFunction);
            Assert.IsNull(network1.OutputLayer.Weights);
        }

        [TestMethod]
        public void AddOutputLayer_WithoutInputLayer_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.AddOutputLayer(2, func), "can't have only output layer");
        }

        [TestMethod]
        public void AddOutputLayer_CalledTwice_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, false)
                   .AddOutputLayer(4, func);

            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.AddOutputLayer(5, func), "can't have 2 output layers");
        }

        [TestMethod]
        public void AddHiddenLayer_CalledAfterAddOutpout_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            NeuralNetwork network1 = builder.CurrentNeuralNetwork;
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, false)
                   .AddOutputLayer(4, func);

            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.AddHiddenLayer(5, func, false), "can't have hidden layer after the output layer");

        }

        [TestMethod]
        public void AddLayers_ShouldSetProperLayersAsInputAndOutput()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew()
                   .AddInputLayer(2, new ActivationFunction(x => x, x => 1), false)
                   .AddHiddenLayer(3, new ActivationFunction(x => 2 * x, x => 1), false)
                   .AddHiddenLayer(4, new ActivationFunction(x => 3 * x, x => 1), false)
                   .AddOutputLayer(1, new ActivationFunction(x => 4 * x, x => 1));
            NeuralNetwork network = builder.CurrentNeuralNetwork;

            Assert.IsNotNull(network);
            Assert.AreEqual(4, network.Layers.Count);
            Assert.AreEqual(network.Layers[0], network.InputLayer);
            Assert.AreEqual(network.Layers[3], network.OutputLayer);
        }

        [TestMethod]
        public void GetNetwork_WithSetUpNetwork()
        {
            NeuralNetwork neuralNetworkMock = new NeuralNetworkMock();

            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CurrentNeuralNetwork = neuralNetworkMock;
            var network = builder.GetNetwork();

            Assert.IsNotNull(network);
            Assert.AreSame(neuralNetworkMock, network);
            Assert.IsTrue(((NeuralNetworkMock)neuralNetworkMock).InitilizeIsCalled);
        }
        
        [TestMethod]
        public void GetNetwork_WithNetworkWithoutInputLayer_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();

            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.GetNetwork(), "can't get not set up neural network");
        }

        [TestMethod]
        public void GetNetwork_WithNetworkWithoutOutputLayer_ShouldThrowException()
        {
            NeuralNetworkBuilderTestDouble builder = new NeuralNetworkBuilderTestDouble();
            builder.CreateNew();
            ActivationFunction func = ActivationFunctions.Identity;
            builder.AddInputLayer(3, func, false);

            AssertEx.AssertThrows<NeuralNetworkConfigurationException>(() => builder.GetNetwork(), "can't get not set up neural network");
        }
    }
}