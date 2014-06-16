using MNISTParser;
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
            AlphabetRecognition();

            /* Unused spike methods - ignore them
            DemoTraining();
            MNISTRecognition();
            AlphabetRecognitionWithVariations();
            */

            Console.ReadLine();
        }

        private const double maxAllowedError = 0.0005;
        private const double value_A = 0.01;

        private static void AlphabetRecognition()
        {
            double[][] testInput = AlphabetData.Samples.Select(s => s.Input).ToArray();
            double[][] testOutput = AlphabetData.Samples.Select(s => new double[] { 2*s.Output }).ToArray();

            BackPropNeuralNetwork network = new BackPropNeuralNetwork(15, 20, 1);
            network.Initialize();
            BackPropNeuralNetworkTrainer trainer = new BackPropNeuralNetworkTrainer(100000, 0.3, 0.1, maxAllowedError);

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
            }
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

        private static void MNISTRecognition()
        {
            const string mnistDataPath = @"..\..\..\MNIST data";
            Parser parser = new Parser();

            var trainingData = parser.ParseTrainingData(mnistDataPath);

            double[][] testInput = new double[trainingData.Length][];
            double[][] testOutput = new double[trainingData.Length][];

            for (int i = 0; i < trainingData.Length; i++)
            {
                Image image = trainingData[i];
                testOutput[i] = new double[] { image.label };
                testInput[i] = image.pixelData.Select(v => v * 1.0).ToArray();
            }

            BackPropNeuralNetwork network = new BackPropNeuralNetwork(784, 300, 1); // 28 x 28
            network.Initialize();
            BackPropNeuralNetworkTrainer trainer = new BackPropNeuralNetworkTrainer(10000, 0.7, 0.2, 0.1);
            DateTime start = DateTime.Now;
            if (trainer.TrainNetwork(network, testInput, testOutput))
            {
                var duration = DateTime.Now - start;
                Console.WriteLine("Network is trained! Elapsed " +duration.ToString());

                string fileName = "MNISTRecognition_" + DateTime.Now.ToString("yyMMdd-HHmmss") + ".nnetwork";
                using (Stream stream = File.Create(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, network);
                }
            }
            else
            {
                var duration = DateTime.Now - start;
                Console.WriteLine("Network is NOT trained! Elapsed " + duration.ToString());
            }
        }

        private static void AlphabetRecognitionWithVariations()
        {
            string fileName = @"AlphabetRecognition_140615-024748.nnetwork";
            BackPropNeuralNetwork newNetwork;
            using (Stream stream = File.OpenRead(fileName))
            {
                BinaryFormatter formetter = new BinaryFormatter();
                newNetwork = (BackPropNeuralNetwork)formetter.Deserialize(stream);
            }

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
                    1, 1, 1, 
                    1, 0, 1, 
                    1, 0, 1
                }, // A
                new double[] 
                { 
                    0, 1, 0, 
                    1, 0, 1, 
                    1, 0, 1, 
                    1, 0, 1, 
                    1, 0, 1
                }, // A
                new double[] 
                { 
                    0, 1, 0, 
                    1, 0, 1, 
                    1, 1, 1, 
                    1, 0, 1, 
                    0, 0, 1
                }, // A
                new double[] 
                { 
                    0, 1, 0, 
                    1, 0, 1, 
                    1, 1, 1, 
                    1, 0, 1, 
                    0, 0, 0
                }, // A
                new double[] 
                { 
                    0, 1, 0, 
                    1, 0, 1, 
                    1, 1, 1, 
                    0, 0, 0, 
                    1, 0, 1
                }, // A
            };

            var etalon = GetMappedCharacter(newNetwork.Run(testInput[0]));
            var result2 = GetMappedCharacter(newNetwork.Run(testInput[1]));
            var result3 = GetMappedCharacter(newNetwork.Run(testInput[2]));
            var result4 = GetMappedCharacter(newNetwork.Run(testInput[3]));
            var result5 = GetMappedCharacter(newNetwork.Run(testInput[4]));
            var result6 = GetMappedCharacter(newNetwork.Run(testInput[5]));
        }

        private static char GetMappedCharacter(double[] raw)
        {
            double v = raw[0];
            double error = 0.005;
            
            var c = (from e in AlphabetData.Samples
                       let min = e.Output - error
                       let max = e.Output + error
                       where v >= min && v <= max
                       select e).FirstOrDefault();

            if (c == null)
                return '?';

            return c.Letter;
        }
    }
}
