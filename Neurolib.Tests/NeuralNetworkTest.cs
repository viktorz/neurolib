using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neurolib.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Tests
{
    [TestClass]
    public class NeuralNetworkTest
    {
        [TestMethod]
        public void Run()
        {
            NeuralNetwork network = new NeuralNetwork();
            network.Layers.Add(new NeuralNetworkLayer()
            {
                ActivationFunction = ActivationFunctions.Sigmoid,
                Inputs = new double[] { 1, 1, 1 },
                Weights = new double[3, 4] 
                { 
                    {1,1,1,1},
                    {1,1,1,1},
                    {1,1,1,1}
                }
            });
            network.InputLayer = network.Layers[0];
            network.Layers.Add(new NeuralNetworkLayer()
            {
                ActivationFunction = ActivationFunctions.Sigmoid,
                Inputs = new double[] { 1, 1, 1, 1 },
                Weights = new double[4, 1] 
                { 
                    {1},
                    {1},
                    {1},
                    {1}
                }
            });
            network.Layers.Add(new NeuralNetworkLayer()
            {
                ActivationFunction = ActivationFunctions.Sigmoid,
                Inputs = new double[] { 1 },
                Weights = null
            });
            network.OutputLayer = network.Layers.Last();

            var result = network.Run(new double[] { 1, 1, 1 });
            Assert.AreEqual(1, result.Length, "unexpected output lenght");
            Assert.IsTrue(Math.Abs(result[0] - 0.978338018376308) < 0.0000002, "unexpected output");
        }

        [TestMethod]
        public void Initialize()
        {
            NeuralNetwork network = new NeuralNetwork();
            network.Layers.Add(new NeuralNetworkLayer()
            {
                ActivationFunction = ActivationFunctions.Sigmoid,
                Inputs = new double[] { 5, 5, 5 },
                Errors = new double[] { double.MinValue, double.MinValue, double.MinValue } ,
                Weights = new double[3, 4] 
                { 
                    {5,5,5,5},
                    {5,5,5,5},
                    {5,5,5,5}
                },
                LastWeightDelta = new double[3, 4]
                { 
                    {0,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0}
                }
            });
            network.InputLayer = network.Layers[0];
            network.Layers.Add(new NeuralNetworkLayer()
            {
                ActivationFunction = ActivationFunctions.Sigmoid,
                Inputs = new double[] { 1, 1, 1, 1 },
                Errors = new double[] { double.MinValue, double.MinValue, double.MinValue, double.MinValue },
                Weights = new double[4, 1] 
                { 
                    {5},
                    {5},
                    {5},
                    {5}
                },
                LastWeightDelta = new double[4,1]
                {
                    {0},
                    {0},
                    {0},
                    {0}
                }
            });
            network.Layers.Add(new NeuralNetworkLayer()
            {
                ActivationFunction = ActivationFunctions.Sigmoid,
                Inputs = new double[] { 1 },
                Errors = new double[] { double.MinValue },
                Weights = null,
                LastWeightDelta = null
            });
            network.OutputLayer = network.Layers.Last();

            network.Initialize();

            foreach (var layer in network.Layers)
            {
                if (layer == network.OutputLayer)
                    break;

                foreach (var w in layer.Weights)
                {
                    Assert.IsTrue(w >= -2 && w <= 2);
                }
            }
        }
    }
}
