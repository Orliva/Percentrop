using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public interface IActivateFunc
    {
        double Activate(double x);
        double ActivateDx(double x);
    }
}
