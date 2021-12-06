using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public interface ILearn
    {
        double Learn(List<double[]> expected, List<double[]> inputs,
             INeuralNetwork neuralNetwork, int epoch = 1000);
    }
}
