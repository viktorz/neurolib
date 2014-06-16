using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.SimpleBackProp
{
    [Serializable]
    public class BackPropNeuralNetwork : ISerializable
    {
        private Random rng = new Random((int)DateTime.Now.Ticks);

        private readonly int numberOfInput;
        private readonly int numberOfHidden;
        private readonly int numberOfOutput;

        private double[] inputs;
        private double[,] ihWeights;
        private double[] hSums;
        private double[] hOutputs;
        private double[] hGradients;
        private double[,] ihPreviousWeightDeltas;
        private double[,] hoWeights;
        private double[] oSums;
        private double[] outputs;
        private double[] oGradients;
        private double[,] hoPreviousWeightDeltas;

        public BackPropNeuralNetwork(int numInput, int numHidden, int numOutput)
        {
            numberOfInput = numInput;
            numberOfHidden = numHidden;
            numberOfOutput = numOutput;

            this.InitializeInternalArrays();
        }

        private void InitializeInternalArrays()
        {
            inputs = new double[numberOfInput];
            ihWeights = new double[numberOfInput, numberOfHidden];
            hSums = new double[numberOfHidden];
            hOutputs = new double[numberOfHidden];
            hGradients = new double[numberOfHidden];
            ihPreviousWeightDeltas = new double[numberOfInput, numberOfHidden];
            hoWeights = new double[numberOfHidden, numberOfOutput];
            oSums = new double[numberOfOutput];
            outputs = new double[numberOfOutput];
            oGradients = new double[numberOfOutput];
            hoPreviousWeightDeltas = new double[numberOfHidden, numberOfOutput];
        }

        public double NumberOfInputs
        {
            get { return this.numberOfInput; }
        }

        public void Initialize()
        {
            for (int i = 0; i < numberOfInput; i++)
            {
                for (int j = 0; j < numberOfHidden; j++)
                {
                    ihWeights[i, j] = rng.NextDouble();
                }
            }

            for (int i = 0; i < numberOfHidden; i++)
            {
                for (int j = 0; j < numberOfOutput; j++)
                {
                    hoWeights[i, j] = rng.NextDouble();
                }
            }
        }

        public double[] Run(double[] inputValues)
        {
            if (inputValues.Length != numberOfInput)
            {
                string message = string.Format("Network expects {0} input values, but got {1}", numberOfInput, inputValues.Length);
                throw new ArgumentException(message, "inputValues");
            }

            // reset any previous state
            for (int i = 0; i < numberOfHidden; ++i)
            {
                hSums[i] = 0.0;
            }
            for (int i = 0; i < numberOfOutput; ++i)
            {
                oSums[i] = 0.0;
            }

            // copy input values to input layer
            for (int i = 0; i < inputValues.Length; ++i)
            {
                this.inputs[i] = inputValues[i];
            }

            // compute sums for hidden layer
            for (int j = 0; j < numberOfHidden; ++j)
            {
                for (int i = 0; i < numberOfInput; ++i)
                {
                    hSums[j] += this.inputs[i] * ihWeights[i, j];
                }
            }

            // apply the activation function
            for (int i = 0; i < numberOfHidden; ++i)
            {
                //hOutputs[i] = activationForward(hSums[i]);
                hOutputs[i] = Math.Tanh(hSums[i]);
            }

            // compute sums for output layer
            for (int j = 0; j < numberOfOutput; ++j)
            {
                for (int i = 0; i < numberOfHidden; ++i)
                {
                    oSums[j] += hOutputs[i] * hoWeights[i,j];
                }
            }

            // apply the activation function
            for (int i = 0; i < numberOfOutput; ++i)
            {
                //outputs[i] = activationForward(oSums[i]);
                outputs[i] = 1.0 / (1.0 + Math.Exp(-oSums[i]));
            }

            // make results unchange-able from outside world
            double[] result = new double[numberOfOutput];
            outputs.CopyTo(result, 0);
            return result;
        }

        public void UpdateWeights(double[] targetOutput, double learnRate, double momentum)
        {
            if (targetOutput.Length != numberOfOutput)
            {
                string message = string.Format("Network expects {0} target values, but got {1}", numberOfOutput, targetOutput.Length);
                throw new ArgumentException(message, "targetOutput");
            }

            // compute output gradients
            for (int i = 0; i < oGradients.Length; ++i)
            {
                //double derivative = activationReverse(outputs[i]);
                double derivative = (1 - outputs[i]) * outputs[i];
                oGradients[i] = derivative * (targetOutput[i] - outputs[i]); 
            }

            // compute hidden gradients
            for (int i = 0; i < hGradients.Length; ++i)
            {
                //double derivative = activationReverse(hOutputs[i]);
                double derivative = (1 - hOutputs[i]) * (1 + hOutputs[i]);
                double sum = 0.0;
                for (int j = 0; j < numberOfOutput; ++j)
                {
                    sum += oGradients[j] * hoWeights[i, j];
                }
                hGradients[i] = derivative * sum;
            }

            // update input to hidden weights
            for (int i = 0; i < numberOfInput; ++i)
            {
                for (int j = 0; j < numberOfHidden; ++j)
                {
                    double delta = learnRate * hGradients[j] * inputs[i];
                    ihWeights[i,j] += delta;
                    ihWeights[i,j] += momentum * ihPreviousWeightDeltas[i,j];
                    ihPreviousWeightDeltas[i,j] = delta;
                }
            }

            // update hidden to outpout weights
            for (int i = 0; i < numberOfHidden; ++i)
            {
                for (int j = 0; j < numberOfOutput; ++j)
                {
                    double delta = learnRate * oGradients[j] * hOutputs[i];
                    hoWeights[i,j] += delta;
                    hoWeights[i,j] += momentum * hoPreviousWeightDeltas[i,j];
                    hoPreviousWeightDeltas[i,j] = delta;
                }
            }
        }

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("numberOfInput", this.numberOfInput);
            info.AddValue("numberOfHidden", this.numberOfHidden);
            info.AddValue("numberOfOutput", this.numberOfOutput);

            info.AddValue("ihWeights", this.ihWeights, typeof(double[,]));
            info.AddValue("hoWeights", this.hoWeights, typeof(double[,]));
        }

        private BackPropNeuralNetwork(SerializationInfo info, StreamingContext context)
        {
            this.numberOfInput = info.GetInt32("numberOfInput");
            this.numberOfHidden = info.GetInt32("numberOfHidden");
            this.numberOfOutput = info.GetInt32("numberOfOutput");

            this.InitializeInternalArrays();

            this.ihWeights = (double[,])info.GetValue("ihWeights", typeof(double[,]));
            this.hoWeights = (double[,])info.GetValue("hoWeights", typeof(double[,]));
        }

        #endregion
    }
}