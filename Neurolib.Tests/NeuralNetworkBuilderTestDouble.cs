using Neurolib.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Tests
{
    internal class NeuralNetworkBuilderTestDouble : NeuralNetworkBuilder
    {
        public NeuralNetworkBuilderTestDouble() :
            base()
        {

        }

        public NeuralNetwork CurrentNeuralNetwork 
        {
            get { return base.currentNetwork; }
            set { base.currentNetwork = value; }
        }
    }
}
