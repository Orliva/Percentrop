using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public interface INeuron
    {
        int InputBondsCount { get; }
        double Output { get; set; }
        double Sum { get; }
        public double Delta { get; set; }
        List<Bond> InputBonds { get; }
        SigmoidActiveFunc? ActivateFunc { get; set; }
        double FeedForward();
        public double CalcDeltasOutputNeuron(double expected);
        public double CalcDeltasHiddenNeuron(List<Neuron> previousNeurons, int indexBond);
    }
}
