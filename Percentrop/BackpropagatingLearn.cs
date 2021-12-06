using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public class BackpropagatingLearn : ILearn
    {
        public BackpropagatingLearn() { }

        /// <summary>
        /// Первое значение в inputs должно быть равно 1
        /// для нейрона смещения
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="inputs"></param>
        /// <param name="epoch"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public double Learn(List<double[]> expected, List<double[]> inputs,
            INeuralNetwork neuralNetwork, int epoch = 1000)
        {
            double error = 1.0;
            double errortmp = 0;
            int countEpochMinus = 0;
            int countEpochPlus = 0;
            int cnt = 0;

            if (expected.Count != inputs.Count)
                throw new Exception("Неверное число входных выборок к числу ожидаемых результатов");

            List<double[]> normInp = Normalization.Normalize(inputs);
            List<double[]> normExpected = Normalization.Normalize(expected);

            //while (error > 0.0001)
            //{
                for (int i = 0; i < epoch; i++)
                {
                    error = 0.0;
                    //Console.WriteLine("Learn epoch: " + i.ToString());
                    MixedList(normExpected, normInp);
                    for (int j = 0; j < normExpected.Count; j++)
                    {
                        double[] expectedLine = normExpected[j];
                        double[] normInputLine = normInp[j];

                        //List<double>? actual = neuralNetwork.Predict(new List<double> { 1, 0.32, 0.88, 0, 0 });

                        List<double>? actual = neuralNetwork.Predict(normInputLine.ToList());

                        double tmpError = Backpropagation(expectedLine, actual.ToArray(), neuralNetwork);
                        //Console.WriteLine(tmpError); //Ошибка на текущей итерации
                        error += tmpError;
                        
                    }

                    //cnt++;
                    error /= 2;
                    //if (cnt == 10000)
                    //{
                      //  cnt = 0;
                        Console.WriteLine(error);

                //neuralNetwork.Topology.LearningRate = 0.1;
                //}
                //if (error > errortmp)
                //{
                //    countEpochPlus = 0;
                //    countEpochMinus++;
                //    if (countEpochMinus == 100)
                //    {
                //        neuralNetwork.Topology.LearningRate -= 0.0001;
                //        Console.WriteLine("neuralNetwork.Topology.LearningRate = " +
                //            neuralNetwork.Topology.LearningRate);
                //        countEpochMinus = 0;
                //        // Thread.Sleep(200);
                //    }
                //}
                //else
                //{
                //    countEpochPlus++;
                //    countEpochMinus = 0;
                //    if (countEpochPlus == 100000)
                //    {
                //        neuralNetwork.Topology.LearningRate += 0.01;
                //        Console.WriteLine("neuralNetwork.Topology.LearningRate = " +
                //            neuralNetwork.Topology.LearningRate);
                //        countEpochPlus = 0;
                //        //Thread.Sleep(1);
                //    }
                //}
                errortmp = error;
                }
                    //Console.WriteLine(error + "  " + countEpochMinus + "  " + (error - errortmp));
                    //Console.WriteLine(error / expected.Count); //Средняя ошибка за эпоху
                    //break;
                    //Console.WriteLine((error / epoch));
                
            //}

            // var result = error / epoch;
            return error;
        }

        /// <summary>
        /// Метод обратного распространения ошибки
        /// </summary>
        /// <param name="expected">Ожидаемый результат</param>
        /// <param name="inputs">Входные значения</param>
        /// <returns></returns>
        private double Backpropagation(double[] expected, double[] actual, INeuralNetwork network)
        {

            List<double> difference = new List<double>(expected.Length);

            int tmp = 0;
            foreach (var i in actual)
            {
                difference.Add(expected[tmp] - i);
                tmp++;
            }

            for (int j = 0; j < network.Layers[^1].NeuronsCount; j++)
                network.Layers[^1].NeuronsList[j].CalcDeltasOutputNeuron(expected[j]);

            for (int i = network.Layers.Count - 2; i >= 1; i--)
            {
                for (int j = 0; j < network.Layers[i].NeuronsCount; j++)
                    network.Layers[i].NeuronsList[j].CalcDeltasHiddenNeuron(network.Layers[i + 1].NeuronsList, j);
            }

            for (int i = 1; i < network.Layers.Count; i++)
            {
                for (int j = 0; j < network.Layers[i].NeuronsCount; j++)
                {
                    //network.Layers[i].NeuronsList[j].CalcDeltasHidden(network.Layers[i + 1].NeuronsList, j);

                    for (int k = 0; k < network.Layers[i].NeuronsList[j].InputBonds.Count; k++)
                        network.Layers[i].NeuronsList[j].InputBonds[k].Weight +=
                            network.Layers[i].NeuronsList[j].InputBonds[k].Neuron_in.Output *
                                //network.Layers[i - 1].NeuronsList[j].Output *
                                network.Layers[i].NeuronsList[j].Delta *
                                network.Topology.LearningRate;
                }
            }

            var differenceSum = difference.Aggregate((x, y) => x + y);
            var result = differenceSum * differenceSum;
            return result;
        }

        private void MixedList(List<double[]> expectList, List<double[]> inputList)
        {
            Random rnd = new Random();
            double[] temp;

            for (int i = expectList.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);

                temp = expectList[j];
                expectList[j] = expectList[i];
                expectList[i] = temp;

                temp = inputList[j];
                inputList[j] = inputList[i];
                inputList[i] = temp;
            }
        }

    }
}
