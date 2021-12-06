using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public class SigmoidActiveFunc : IActivateFunc
    {
        public SigmoidActiveFunc() { }

        public double Activate(double x)
        {
            return (1.0 / (1.0 + Math.Exp(-x)));
        }

        public double ActivateDx(double x)
        {
            var sigmoid = Activate(x);
            return sigmoid * (1 - sigmoid);
        }
    }
}
