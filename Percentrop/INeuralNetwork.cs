using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public interface INeuralNetwork
    {
        Topology Topology { get; set; }
        List<ILayer> Layers { get; }
        ILearn? Learner { get; }
        List<double>? Predict(List<double> inputSignals);
    }
}