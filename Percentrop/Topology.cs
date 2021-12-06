using System.Collections.Generic;
using System.Text.Json;

namespace MyNeuralNetwork2
{
    public class Topology
    {
        /// <summary>
        /// Количество входных нейронов
        /// </summary>
        public int InputCount { get; }

        /// <summary>
        /// Количество выходных нейронов
        /// </summary>
        public int OutputCount { get; }

        /// <summary>
        /// Массив значей, где каждое значение из массива
        /// обозначает количество нейронов на соответствующем скрытом слое
        /// </summary>
        public List<int> HiddenLayers { get; }

        /// <summary>
        /// Скорость обучения
        /// </summary>
        public double LearningRate { get; set; }

        /// <summary>
        /// Топология полносвязного перцентрона
        /// </summary>
        /// <param name="inputCount">Количество входных нейронов</param>
        /// <param name="outputCount">Количество выходных нейронов</param>
        /// <param name="layers">Массив значей, где каждое значение из массива
        /// обозначает количество нейронов на соответствующем скрытом слое</param>
        /// <param name="learningRate">Скорость обучения (0 < learningRate < 1)</param>
        public Topology(int inputCount, int outputCount, List<int> hiddenLayers, double learningRate = 0.5)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(hiddenLayers);

            LearningRate = learningRate;
        }

        public static void Serialization(string path, Topology topology)
        {
            string topologyStr = JsonSerializer.Serialize(topology);

            File.WriteAllText(path, topologyStr);
        }

        public static Topology RestoreFromFile(string path)
        {
            return JsonSerializer.Deserialize<Topology>(File.ReadAllText(path));
        }
    }
}
