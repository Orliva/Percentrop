using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public static class FactoryActiveFunc
    {
        public static IActivateFunc? Create(ActivateType type)
        {
            return type switch
            {
                ActivateType.None => null,
                ActivateType.Sigmoid => new SigmoidActiveFunc(),
                _ => null,
            };
        }
    }
}