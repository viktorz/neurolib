using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.SimpleBackProp
{
    public static class Program
    {
        public static void Main()
        {
            //DemoTraining();
            AlphabetRecognition();

            Console.ReadLine();
        }

        private static void DemoTraining()
        {
            double[][] testInput = new double[1][] { new double[] { 1.0, -2.0, 3.0 } };
            double[][] testOutput = new double[1][] { new double[] { 0.1234, 0.8765 } };

            BackPropNeuralNetwork network = new BackPropNeuralNetwork(3, 4, 2);
            network.Initialize();
            BackPropNeuralNetworkTrainer trainer = new BackPropNeuralNetworkTrainer(1000000, 0.3, 0.1, 0.00001);

            trainer.TrainNetwork(network, testInput, testOutput);
        }

        private static void AlphabetRecognition()
        {
            #region date definition
            double[][] testInput = new double[][]
            {
                new double[] 
                { 
                    0, 1, 0, 
                    1, 0, 1, 
                    1, 1, 1, 
                    1, 0, 1, 
                    1, 0, 1
                }, // A
                new double[] 
                {
                    1, 1, 0,
                    1, 0, 1,
                    1, 1, 0,
                    1, 0, 1,
                    1, 1, 0
                }, // B
                new double[] 
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 0,
                    1, 0, 0,
                    1, 1, 1
                }, // C
                new double[] 
                { 
                    1, 1, 0,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 0
                }, // D
                new double[] 
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1
                }, // E
                new double[]
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 0
                }, // F
                new double[]
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1
                }, // G
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1
                }, // H
                new double[]
                {
                    1, 1, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0,
                    1, 1, 1
                }, // I
                new double[]
                {
                    1, 1, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0,
                    1, 1, 0
                }, // J
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 0,
                    1, 0, 1,
                    1, 0, 1
                }, // K
                new double[]
                {
                    1, 0, 0,
                    1, 0, 0,
                    1, 0, 0,
                    1, 0, 0,
                    1, 1, 1
                }, // L
                new double[]
                {
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1
                }, // M
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 1, 1,
                    1, 0, 1
                }, // N
                new double[]
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                }, // O
                new double[]
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 0,
                    1, 0, 0
                }, //  P
                new double[]
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    0, 0, 1
                }, // Q
                new double[]
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 1, 0,
                    1, 0, 1
                }, // R
                new double[]
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1
                }, // S
                new double[]
                {
                    1, 1, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0
                }, // T
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1
                }, // U
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    0, 1, 0,
                    0, 1, 0
                }, // V
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                }, // W
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    0, 1, 0,
                    1, 0, 1,
                    1, 0, 1
                }, // X
                new double[]
                {
                    1, 0, 1,
                    1, 0, 1,
                    0, 1, 0,
                    0, 1, 0,
                    0, 1, 0
                }, // Y
                new double[]
                {
                    1, 1, 1,
                    0, 0, 1,
                    0, 1, 0,
                    1, 0, 0,
                    1, 1, 1
                }, // Z
                /*
                new double[]
                {
                }, // 1
                new double[]
                {
                }, // 2 
                new double[]
                {
                }, // 3
                new double[]
                {
                }, // 4
                new double[]
                {
                }, // 5
                new double[]
                {
                }, // 6
                new double[]
                {
                }, // 7
                new double[]
                {
                }, // 8
                new double[]
                {
                }, // 9
                // Zero is covered by "o"
                */
            };
            double[][] testOutput = new double[][]
            {
                new double[] { 0.01 }, // A
                new double[] { 0.02 }, // B
                new double[] { 0.03 }, // C
                new double[] { 0.04 }, // D
                new double[] { 0.05 }, // E
                new double[] { 0.06 }, // F
                new double[] { 0.07 }, // G
                new double[] { 0.08 }, // I
                new double[] { 0.09 }, // J
                new double[] { 0.10 }, // K
                new double[] { 0.11 }, // L
                new double[] { 0.12 }, // M
                new double[] { 0.13 }, // N
                new double[] { 0.14 }, // O
                new double[] { 0.15 }, // P
                new double[] { 0.16 }, // Q
                new double[] { 0.17 }, // R
                new double[] { 0.18 }, // S
                new double[] { 0.19 }, // T
                new double[] { 0.20 }, // U
                new double[] { 0.21 }, // V
                new double[] { 0.22 }, // W
                new double[] { 0.23 }, // X 
                new double[] { 0.24 }, // Y
                new double[] { 0.25 }, // Z 
                new double[] { 0.26 }, // H
                /*
                new double[] { 0. }, // 1
                new double[] { 0. }, // 2 
                new double[] { 0. }, // 3
                new double[] { 0. }, // 4
                new double[] { 0. }, // 5
                new double[] { 0. }, // 6
                new double[] { 0. }, // 7
                new double[] { 0. }, // 8
                new double[] { 0. }, // 9
                 */
            };
            #endregion

            BackPropNeuralNetwork network = new BackPropNeuralNetwork(15, 20, 1);
            network.Initialize();
            BackPropNeuralNetworkTrainer trainer = new BackPropNeuralNetworkTrainer(1000000, 0.3, 0.1, 0.004);

            if (trainer.TrainNetwork(network, testInput, testOutput))
            {
                string fileName = "AlphabetRecognition_" + DateTime.Now.ToString("yyMMdd-HHmmss") + ".nnetwork";
                using (Stream stream = File.Create(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, network);
                }

                BackPropNeuralNetwork newNetwork;
                using (Stream stream = File.OpenRead(fileName))
                {
                    BinaryFormatter formetter = new BinaryFormatter();
                    newNetwork = (BackPropNeuralNetwork)formetter.Deserialize(stream);
                }

                double[] result1 = newNetwork.Run(testInput[1]);  // B - 2
                double[] result2 = newNetwork.Run(testInput[10]); // K - 10
                double[] result3 = newNetwork.Run(testInput[20]); // U - 20
                double[] result4 = newNetwork.Run(testInput[25]); // H - 26
            }
        }
    }
}
