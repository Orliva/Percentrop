using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public interface ILayer
    {
        int NeuronsCount { get; }
        List<Neuron> NeuronsList { get; }
        LayerType LayerType { get; }
        void SetInputSignal(List<double> inputSignals);
    }
}
