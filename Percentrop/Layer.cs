using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyNeuralNetwork2
{
    public class Layer : ILayer
    {
        /// <summary>
        /// Количество нейронов в слое
        /// </summary>
        public int NeuronsCount { get; }// => NeuronsList?.Count ?? 0;

        /// <summary>
        /// Тип слоя
        /// </summary>
        public LayerType LayerType { get; }

        /// <summary>
        /// Список нейронов слоя
        /// </summary>
        public List<Neuron> NeuronsList { get; }


        /// <summary>
        /// Конструктор для десериализации
        /// </summary>
        /// <param name="neuronsCount"></param>
        /// <param name="layerType"></param>
        /// <param name="neuronsList"></param>
        [JsonConstructorAttribute]
        public Layer(int neuronsCount, LayerType layerType, List<Neuron> neuronsList)
        {
            NeuronsCount = neuronsCount;
            LayerType = layerType;
            NeuronsList = new List<Neuron>(neuronsList);
        }

        public Layer(List<Neuron> neuronsList, LayerType layerType = LayerType.Hidden)
        {
            //TODO: проверить все входные нейроны на соответствие типу

            NeuronsList = neuronsList;
            LayerType = layerType;
            NeuronsCount = NeuronsList?.Count ?? 0;
        }

        /// <summary>
        /// Задать значения входного слоя
        /// </summary>
        /// <param name="inputSignals">Входящая строка входных сигналов</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetInputSignal(List<double> inputSignals)
        {
            if (inputSignals.Count != NeuronsCount)
                throw new ArgumentException();

            for (int i = 0; i < NeuronsCount; i++)
                NeuronsList[i].Output = inputSignals[i];
        }

        public override string ToString()
        {
            return this.LayerType.ToString();
        }
    }
}
