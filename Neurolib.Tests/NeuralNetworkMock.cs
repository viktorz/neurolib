using Neurolib.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Tests
{
    internal class NeuralNetworkMock : NeuralNetwork
    {
        internal override void Initialize()
        {
            this.InitilizeIsCalled = true;
        }

        public bool InitilizeIsCalled { get; set; }
    }
}
