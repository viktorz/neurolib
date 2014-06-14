using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neurolib.Imp;

namespace Neurolib.Tests
{
    [TestClass]
    public class BackPropagationTrainerTests
    {
        [TestMethod]
        public void TrainXor()
        {
            NeuralNetworkBuilder builder = new NeuralNetworkBuilder();
            NeuralNetwork network = builder.CreateNew()
                                           .AddInputLayer(2, ActivationFunctions.Sigmoid, false)
                                           .AddHiddenLayer(2, ActivationFunctions.Sigmoid, false)
                                           .AddOutputLayer(1, ActivationFunctions.Sigmoid)
                                           .GetNetwork();
            BackPropagationTrainer trainer = new BackPropagationTrainer(network);

            TrainingData[] data = new TrainingData[]
            {
                new TrainingData(new double[] { 1, 1 }, new double[] { 0 }),
                new TrainingData(new double[] { 1, 0 }, new double[] { 1 }),
                new TrainingData(new double[] { 0, 1 }, new double[] { 1 }),
                new TrainingData(new double[] { 0, 0 }, new double[] { 0 }),
            };
            var result = trainer.Train(data, 100000, 0.7, 0.0, 0.005);

            string csv = result.Errors.ToCsvString();
            Assert.IsTrue(result.IsSuccessful, "could not traing against expected error");

            double[] result1 = network.Run(new double[] { 1.0, 1.0 });
            double[] result2 = network.Run(new double[] { 1.0, 0.0 });           
            double[] result3 = network.Run(new double[] { 0.0, 1.0 });
            double[] result4 = network.Run(new double[] { 0.0, 0.0 });

            Assert.IsTrue(result1[0].IsEqual(0, 0.0005));
            Assert.IsTrue(result2[0].IsEqual(1, 0.0005));
            Assert.IsTrue(result3[0].IsEqual(1, 0.0005));
            Assert.IsTrue(result4[0].IsEqual(0, 0.0005));
        }

        [TestMethod]
        public void Train1()
        {
            NeuralNetworkBuilder builder = new NeuralNetworkBuilder();
            NeuralNetwork network = builder.CreateNew()
                                           .AddInputLayer(2, ActivationFunctions.Sigmoid, false)
                                           .AddOutputLayer(1, ActivationFunctions.Sigmoid)
                                           .GetNetwork();
            BackPropagationTrainer trainer = new BackPropagationTrainer(network);

            TrainingData[] data = new TrainingData[]
            {
                new TrainingData(new double[] { 1, 1 }, new double[] { 2 }),
            };
            var result = trainer.Train(data, 1000, 0.3, 0.0, 0.005);

            string csv = result.Errors.ToCsvString();
            Assert.IsTrue(result.IsSuccessful, "could not traing against expected error");

            double[] result1 = network.Run(new double[] { 1.0, 1.0 });
            Assert.IsTrue(result1[0].IsEqual(0, 0.0005));
        }
    }


}