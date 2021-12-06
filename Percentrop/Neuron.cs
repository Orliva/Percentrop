using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyNeuralNetwork2
{
    public class Neuron : INeuron
    {
        /// <summary>
        /// Выход нейрона
        /// </summary>
        public double Output { get; set; }
        
        /// <summary>
        /// Невязка нейрона
        /// </summary>
        public double Delta { get; set; }

        /// <summary>
        /// Число входящих связей нейрона
        /// </summary>
        public int InputBondsCount { get; }

        /// <summary>
        /// Взвешенная сумма нейрона
        /// </summary>
        public double Sum { get; private set; }

        /// <summary>
        /// Список приходящих к нейрону связей
        /// </summary>
        public List<Bond> InputBonds { get; set; }


        /// <summary>
        /// Функция активации нейрона
        /// </summary>
        public SigmoidActiveFunc? ActivateFunc { get; set; }


        [JsonConstructorAttribute]
        public Neuron(double output, double delta, int inputBondsCount,
            double sum, List<Bond> inputBonds, SigmoidActiveFunc activateFunc)
        {
            Output = output;
            Delta = delta;
            InputBondsCount = inputBondsCount;
            Sum = sum;
            ActivateFunc = activateFunc;
        }

        public Neuron(int inputCount, SigmoidActiveFunc? active, List<Neuron>? previousNeuron = null)
        {
            Sum = 0;
            InputBondsCount = inputCount;
            InputBonds = new List<Bond>(inputCount);
            ActivateFunc = active;

            if (previousNeuron != null)
                InitWeightsRandomValue(previousNeuron);
            else
                InitWeightsRandomValue(previousNeuron, 1);
        }

        public void SetWeight(double[] weights)
        {
            if (InputBonds.Count != weights.Length)
                throw new Exception("Неверно переданы количество весов для инициализации");

            for (int i = 0; i < InputBonds.Count; i++)
                InputBonds[i].Weight = weights[i];
        }

        /// <summary>
        /// Инициализируем начальные веса случайными значениями
        /// </summary>
        private void InitWeightsRandomValue(List<Neuron>? previousNeuron, double? defaultValue = null)
        {
            Random rnd = new Random();

            //Задаем веса для входных нейронов
            if (defaultValue != null)
            {
                for (int i = 0; i < InputBondsCount; i++)
                    InputBonds.Add(new Bond(defaultValue.Value, null));
                return;
            }

            for (int i = 0; i < InputBondsCount; i++)
                InputBonds.Add(new Bond(rnd.NextDouble(), previousNeuron?[i]));
        }

        /// <summary>
        /// Считаем взвешенную сумму текущего нейрона
        /// и применяем функцию активации
        /// </summary>
        /// <returns>Выходное значение нейрона</returns>
        public double FeedForward()
        {
            //Что то лишнее считается
            //Проверить расчеты
            Sum = 0.0;
            foreach (var i in InputBonds)
                Sum += (i?.Neuron_in?.Output ?? 1) * (i?.Weight ?? 0);

            Output = ActivateFunc?.Activate(Sum) ?? 1;

            //Output = ActiveteFunc?.Activate(0.534) ?? 1;

            return Output;
        }

        #region CalcDeltas

        /// <summary>
        /// Расчет невязки для нейронов выходного слоя
        /// </summary>
        /// <param name="expected"></param>
        /// <returns></returns>
        public double CalcDeltasOutputNeuron(double expected)
        {
            double delta = expected - Output;
            Delta = delta * ActivateFunc?.ActivateDx(Sum) ?? 1;
            return Delta;
        }

        /// <summary>
        /// Расчет невязки для нейронов скрытого слоя
        /// </summary>
        /// <param name="previousNeurons">Список нейронов предыдущего слоя</param>
        /// <param name="indexBond">Индекс входящей связи</param>
        /// <returns>Невязка текущего нейрона</returns>
        public double CalcDeltasHiddenNeuron(List<Neuron> previousNeurons, int indexBond)
        {
            double deltas = 0.0;
            for(int i = 0; i < previousNeurons.Count; i++)
            {
                deltas += previousNeurons[i].Delta * previousNeurons[i].InputBonds[indexBond].Weight;
            }

            //   double delta = expected - Output;
            var t = ActivateFunc?.ActivateDx(Sum) ?? 1;
            var tmp = deltas * ActivateFunc?.ActivateDx(Sum) ?? 1;

            return (Delta = deltas * (ActivateFunc?.ActivateDx(Sum) ?? 1));
        }

        #endregion

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
