using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.SimpleBackProp
{
    public class BackPropNeuralNetworkTrainer
    {
        private int maxIterations;
        private double learningRate;
        private double momentum;
        private double maxAllowedError;

        public BackPropNeuralNetworkTrainer(int maxIterations, double learningRate, double momentum, double maxAllowedError)
        {
            this.maxIterations = maxIterations;
            this.learningRate = learningRate;
            this.momentum = momentum;
            this.maxAllowedError = maxAllowedError;
        }

        public bool TrainNetwork(BackPropNeuralNetwork network, double[][] inputValues, double[][] targetValues)
        {
            // TODO: check array lenghts agains network params

            double[] currentResult;
            double iterationError;
            double[] currentInput;
            double[] targetOutput;
            double[] sampleErrors = new double[inputValues.Length];
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                if (iteration % 20 == 0)
                {
                    // TODO: broadcast progress
                    Console.WriteLine("Training iteration " + iteration);
                    Console.WriteLine("Errors: "+string.Join("  ", sampleErrors));
                }

                bool allClear = true;
                
                for (int sampleIndex = 0; sampleIndex < inputValues.Length; sampleIndex++)
                {
                    currentInput = inputValues[sampleIndex];
                    targetOutput = targetValues[sampleIndex];

                    currentResult = network.Run(currentInput);
                    iterationError = CalculateError(currentResult, targetOutput);
                    sampleErrors[sampleIndex] = iterationError;
                    if (iterationError >= this.maxAllowedError)
                    {
                        allClear = false;
                        network.UpdateWeights(targetOutput, this.learningRate, this.momentum);
                    }
                }

                if (allClear)
                {
                    // TODO: success!
                    Console.WriteLine("Training complete at iteration " + iteration);
                    return true;
                }
            }

            return false;
        }

        private double CalculateError(double[] actualValues, double[] expectedValues)
        {
            double sum = 0.0;
            for (int i = 0; i < expectedValues.Length; ++i)
            {
                sum += (expectedValues[i] - actualValues[i]) * (expectedValues[i] - actualValues[i]);
            }
            return Math.Sqrt(sum);
        }
    }
}
