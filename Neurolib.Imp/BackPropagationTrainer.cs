using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public class BackPropagationTrainer
    {
        private readonly NeuralNetwork network;

        public BackPropagationTrainer(NeuralNetwork network)
        {
            if (network == null)
                throw new ArgumentNullException("network", "Trainer needs actual neural network instance to train!");

            this.network = network;
        }

        public TrainingResult Train(TrainingData[] trainData, int numberOfIterations, double learningRate, double momentum, double maxAllowedError)
        {
            TrainingResult result = new TrainingResult();
            result.Errors = new double[numberOfIterations];
            double iterationErrorSum;
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                iterationErrorSum = 0.0;
                foreach (var data in trainData)
                {
                    double[] runResult = network.Run(data.InputValues);

                    double errorSum = 0.0;
                    for (int resultIndex = 0; resultIndex < runResult.Length; resultIndex++)
                    {
                        double actual = runResult[resultIndex];
                        double expected = data.OutputValues[resultIndex];

                        double diff = expected - actual;
                        errorSum += Math.Pow(diff, 2);

                        // set errors to output layer
                        network.OutputLayer.Errors[resultIndex] = network.OutputLayer.ActivationFunction.Reverse(actual) * diff;
                    }
                    iterationErrorSum += errorSum;

                    // calculate back layer errors and weight adjustments
                    for (int layerIndex = network.Layers.Count - 2; layerIndex >= 0; layerIndex--)
                    {
                        var upperLayer = network.Layers[layerIndex + 1];
                        var currentLayer = network.Layers[layerIndex];

                        for (int upperLayerNodeIndex = 0; upperLayerNodeIndex < upperLayer.Inputs.Length; upperLayerNodeIndex++)
                        {
                            double nodeError = upperLayer.Errors[upperLayerNodeIndex];
                            
                            for (int currentLayerNodeIndex = 0; currentLayerNodeIndex < currentLayer.Inputs.Length; currentLayerNodeIndex++)
                            {
                                //calculate errors
                                double accumulatedError = 0.0;
                                for(int upperLayerErrorIndex = 0; upperLayerErrorIndex < upperLayer.Inputs.Length; upperLayerErrorIndex++)
                                {
                                    accumulatedError += upperLayer.Errors[upperLayerErrorIndex] * currentLayer.Weights[currentLayerNodeIndex, upperLayerErrorIndex];
                                }
                                currentLayer.Errors[currentLayerNodeIndex] = currentLayer.ActivationFunction.Reverse(currentLayer.Inputs[currentLayerNodeIndex]) 
                                                                             * accumulatedError;
                                double weightDelta = nodeError * currentLayer.Inputs[currentLayerNodeIndex];
                                // adjust weights
                                currentLayer.Weights[currentLayerNodeIndex, upperLayerNodeIndex] = currentLayer.Weights[currentLayerNodeIndex, upperLayerNodeIndex] 
                                                                                                   + weightDelta * learningRate
                                                                                                   + currentLayer.LastWeightDelta[currentLayerNodeIndex, upperLayerNodeIndex] * momentum; // TODO: add momentum
                                currentLayer.LastWeightDelta[currentLayerNodeIndex, upperLayerNodeIndex] = weightDelta * learningRate;
                            }
                        }
                    }
                }

                result.Errors[iteration] = iterationErrorSum;

                double meanIterationError = iterationErrorSum / trainData.Length;

                result.Error = meanIterationError;

                if (meanIterationError < maxAllowedError)
                {
                    result.IsSuccessful = true;
                    result.TrainedNetwork = network;
                    result.TrainingIterationCount = iteration;
                    
                    break;
                }
            }

            return result;
        }
    }
}