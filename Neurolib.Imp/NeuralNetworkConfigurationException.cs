using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neurolib.Imp
{
    [Serializable]
    public class NeuralNetworkConfigurationException : ApplicationException
    {
        public NeuralNetworkConfigurationException() { }
        public NeuralNetworkConfigurationException(string message) : base(message) { }
        public NeuralNetworkConfigurationException(string message, Exception inner) : base(message, inner) { }
        
        protected NeuralNetworkConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) 
        {
        }
    }
}
