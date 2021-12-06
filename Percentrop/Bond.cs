using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MyNeuralNetwork2
{
    public class Bond
    {
        public double Weight { get; set; }
        public double Delta { get; set; }
        public Neuron? Neuron_in;
        public Neuron? Neuron_out; //TODO: Удалить Out

        [JsonConstructorAttribute]
        public Bond(double weight = 0, Neuron? neuron_in = null, double delta = 0, Neuron? neuron_out = null)
        {
            Weight = weight;
            Delta = delta;
            Neuron_in = neuron_in;
            Neuron_out = neuron_out;
        }

        //public Bond(double weight, Neuron? inNeuron, double delta = 0, Neuron? outNeuron = null)
        //{
        //    Weight = weight;
        //    Delta = delta;
        //    Neuron_in = inNeuron;
        //}
    }
}
