using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public class Normalization
    {
        /// <summary>
        /// Привести входные значения к диапозону [0..1]
        /// </summary>
        /// <param name="inputs">
        /// double[] - Одна строка входных данных
        /// List<double[]> - полный набор входных строк
        /// </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<double[]> Normalize(List<double[]> inputs)
        {
            List<double[]> result = new List<double[]>(inputs);

            foreach (var i in inputs)
            {
                if (i.Max() <= 1 && i.Min() >= 0)
                    return new List<double[]>(inputs);
            }

            int? countCol;

            if ((countCol = inputs.FirstOrDefault()?.Length) == null)
                throw new Exception("Неверные входные значения");

            for (int column = 0; column < countCol; column++)
            {
                double min = inputs[0][column];
                double max = inputs[0][column];

                for (int row = 1; row < inputs.Count; row++)
                {
                    double item = inputs[row][column];

                    if (item < min)
                        min = item;

                    if (item > max)
                        max = item;
                }

                double divider = max - min;
                for (int row = 0; row < inputs.Count; row++)
                    result[row][column] = (inputs[row][column] - min) / divider;
            }

            return result;
        }
    }
}
