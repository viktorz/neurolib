using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    public class TrainingResult
    {
        internal TrainingResult()
        {
        }

        public double Error { get; internal set; }
        public int TrainingIterationCount { get; internal set; }
        public bool IsSuccessful { get; internal set; }
        public NeuralNetwork TrainedNetwork { get; internal set; }

        public double[] Errors { get; internal set; }
    }
}
