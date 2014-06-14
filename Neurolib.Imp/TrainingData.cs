using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public class TrainingData
    {
        public readonly double[] InputValues;
        public readonly double[] OutputValues;

        public TrainingData()
        {
        }

        public TrainingData(double[] inputValues, double[] outputValues)
        {
            this.InputValues = inputValues;
            this.OutputValues = outputValues;
        }
    }
}
