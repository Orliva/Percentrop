using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyNeuralNetwork2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double[]> weights = new List<double[]>()
            {
                new double[] { 0.5, -0.1, 0.2 },
                new double[] { 0.3, -0.23, 0.43 },
                new double[] { 0, 0.33, -0.4 },
                new double[] { 0, -0.5, 0.2 },
                new double[] { 0, -0.49, 0.39 },


                //new double[] { -0.2, 0.1, 0.2, 0.3, 0 },
                //new double[] { -0.1, -0.3, 0.2, 0.1, -0.3 },
                //new double[] { 0.2, 0, -0.1 }
            };

            var expectedOutputs = new List<double[]>
            {
            //    new double[] { 1 },
            //    new double[] { 0.5},
            //    new double[] { 1 },
            //    new double[] { 0.5 },
                new double[] { 0 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 0 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 },
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 }
            };

            var inputs = new List<double[]>
            {
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                //T  A  S  F

                //new double[] { 2, 1, 3 },
                //new double[] { 1, 2, 1 },
                //new double[] { 3, 2, 1 },
                //new double[] { 1, 3, 2 },


                new double[] { 1, 0, 0, 0, 0 },
                new double[] { 1, 0, 0, 0, 1 },
                new double[] { 1, 0, 0, 1, 0 },
                new double[] { 1, 0, 0, 1, 1 },
                new double[] { 1, 0, 1, 0, 0 },
                new double[] { 1, 0, 1, 0, 1 },
                new double[] { 1, 0, 1, 1, 0 },
                new double[] { 1, 1, 1, 0, 0 },
                new double[] { 1, 1, 1, 0, 1 },
                new double[] { 1, 1, 1, 1, 0 },
                new double[] { 1, 1, 1, 1, 1 }

                //new double[] { 0, 0, 0, 0 },
                //new double[] { 0, 0, 0, 1 },
                //new double[] { 0, 0, 1, 0 },
                //new double[] { 0, 0, 1, 1 },
                //new double[] { 0, 1, 0, 0 },
                //new double[] { 0, 1, 0, 1 },
                //new double[] { 0, 1, 1, 0 },
                //new double[] { 0, 1, 1, 1 },
                //new double[] { 1, 0, 0, 0 },
                //new double[] { 1, 0, 0, 1 },
                //new double[] { 1, 0, 1, 0 },
                //new double[] { 1, 0, 1, 1 },
                //new double[] { 1, 1, 0, 0 },
                //new double[] { 1, 1, 0, 1 },
                //new double[] { 1, 1, 1, 0 },
                //new double[] { 1, 1, 1, 1 }
            };

            var testExpected = new List<double[]>
            {
                new double[] { 0 },
                    new double[] { 1 },
                    new double[] { 1 },
                    new double[] { 1 },
                    new double[] { 1 },
            };

            var testInput = new List<double[]>
            {
                new double[] { 1, 0, 1, 1, 1 },
                new double[] { 1, 1, 0, 0, 0 },
                new double[] { 1, 1, 0, 0, 1 },
                new double[] { 1, 1, 0, 1, 0 },
                new double[] { 1, 1, 0, 1, 1 },
            };


            FeedForwardToRealDatasetTest();

            //Topology topology = new Topology(4, 1, new List<int> { 2 }, 0.01);
            //var learner = new BackpropagatingLearn();
            //NeuralNetwork neuralNetwork = new NeuralNetwork(topology, learner);

            ////neuralNetwork.InitAllWeight(weights);

            //var difference = neuralNetwork.Learn(expectedOutputs, inputs, neuralNetwork, 10000);

            ////передать входные значения
            ////neuralNetwork.Predict(new List<double> { 1, 0.32, 0.88, 0, 0 });

            ////          List<double[]> expected, List< double[] > inputs,
            ////            INeuralNetwork neuralNetwork


            ////List<double[]> expected = new List<double[]>()
            ////{
            ////    new double[] { 0 }
            ////};

            ////List<double[]> inputs = new List<double[]>()
            ////{
            ////    new double[] { 1, 0.32, 0.88, 0, 0 },
            ////};


            //var results = new List<List<double>>();
            //for (int i = 0; i < testExpected.Count; i++)
            //{
            //    List<double> res = neuralNetwork.Predict(testInput[i].ToList());
            //    List<double> listDoule = new List<double>();
            //    foreach (var item in res)
            //        listDoule.Add(item);
            //    results.Add(listDoule);
            //}

            //for (int i = 0; i < results.Count; i++)
            //{
            //    for (int j = 0; j < results[i].Count; j++)
            //    {
            //        var expected = Math.Round(testExpected[i][j], 6);
            //        var actual = Math.Round(results[i][j], 6);
            //        if (!test(expected, actual))
            //            Console.WriteLine("TEST FAIL: " + "expected:" +
            //                expected.ToString() + " " + "actual:" +
            //                actual.ToString() + " " + (expected - actual).ToString());
            //        else
            //            Console.WriteLine("TEST SUCCESS!!!");
            //        //Assert.AreEqual(expected, actual);
            //    }
            //}

            //int k = 0;
            ////TODO: Задать конкретные значения нейронов,
            //конкретные значения весов,
            //отслеживать обучение






            //List<double[]> expectedOutputs = new List<double[]>();
            //List<double[]> inputs = new List<double[]>();


            //using (StreamReader sr = new StreamReader("heart.csv"))
            //{
            //    sr.ReadLine();

            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        string[] tmp = line.Split(";");
            //        expectedOutputs.Add(new double[] { Convert.ToDouble(tmp.Last()) });
            //        inputs.Add(tmp.Select(x => double.Parse(x)).SkipLast(1).ToArray());
            //    }
            //}

            //var topology = new Topology(inputs.FirstOrDefault().Length, 1,
            //    new List<int> { 6, 6 }, 0.3);
            //var neuralNetwork = new NeuralNetwork(topology);
            //var difference = neuralNetwork.Learn(expectedOutputs, inputs, 10000);



            //var results = new List<List<double>>();
            //for (int i = 0; i < expectedOutputs.Count; i++)
            //{
            //    var res = neuralNetwork.Predict(inputs[i]);
            //    List<double> listDoule = new List<double>();
            //    foreach (var item in res)
            //        listDoule.Add(item.Output);
            //    results.Add(listDoule);
            //}

            //for (int i = 0; i < results.Count; i++)
            //{
            //    for (int j = 0; j < results[i].Count; j++)
            //    {
            //        var expected = Math.Round(expectedOutputs[i][j], 2);
            //        var actual = Math.Round(results[i][j], 2);
            //        if (!test(expected, actual))
            //            Console.WriteLine("TEST FAIL: " + "expected:" +
            //                expected.ToString() + " " + "actual:" +
            //                actual.ToString() + " " + (expected - actual).ToString());
            //        else
            //            Console.WriteLine("TEST SUCCESS!!!");
            //    }
            //}
            ////var converter = new PictureConverter();
            ////var inputs = converter.Convert(@"C:\Users\ProgOtpadKor\source\repos\NeuralNetwork\NeuralNetworkTests\Images\Parasitized.png");
        }


        public static void FeedForwardToRealDatasetTest()
        {
            List<double[]> expectedOutputs = new List<double[]>(1001);
            List<double[]> inputs = new List<double[]>(1001);
            List<double[]> evalInputs = new List<double[]>(1000);
            List<double[]> evalExpectedOutputs = new List<double[]>(1001);


            using (StreamReader sr = new StreamReader("winequality-red.csv"))
            {
                sr.ReadLine();
                int count = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    count++;
                    string[] tmp = line.Split(";");
                    if (count <= 100)
                    {
                        expectedOutputs.Add(new double[] { Convert.ToDouble(tmp.Last()) });
                        //expectedOutputs.Add(new double[] { Convert.ToDouble(tmp.Last()) });
                        inputs.Add(tmp.Select(x => double.Parse(x)).SkipLast(1).ToArray());
                    }
                    else
                    {
                        evalExpectedOutputs.Add(new double[] { Convert.ToDouble(tmp.Last()) });
                        evalInputs.Add(tmp.Select(x => double.Parse(x)).SkipLast(1).ToArray());
                        if (count == 200)
                            break;
                    }
                }
            }

            //using (StreamReader sr = new StreamReader("winequality-white.csv"))
            //{
            //    sr.ReadLine();
            //    int count = 0;
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        count++;
            //        string[] tmp = line.Split(";");
            //        if (count <= 1000)
            //        {

            //            expectedOutputs.Add(new double[] { Convert.ToDouble(tmp.Last()) });
            //            //expectedOutputs.Add(new double[] { Convert.ToDouble(tmp.Last()) });
            //            inputs.Add(tmp.Select(x => double.Parse(x)).SkipLast(1).ToArray());
            //        }
            //        else
            //        {
            //            evalExpectedOutputs.Add(new double[] { 1 });
            //            evalInputs.Add(tmp.Select(x => double.Parse(x)).ToArray());
            //            if (count == 1600)
            //                break;
            //        }
            //    }
            //}


            Topology topology = new Topology(inputs.FirstOrDefault().Length, 1,
                new List<int> { 13 },
                learningRate: 0.1);
            var learner = new BackpropagatingLearn();
            NeuralNetwork neuralNetwork = new NeuralNetwork(topology, learner);

            var difference = neuralNetwork.Learn(expectedOutputs, inputs, 100);


            //Topology restorTopology = Topology.RestoreFromFile("topology.txt");
            //NeuralNetwork neuralNetwork = new NeuralNetwork(restorTopology);
            //neuralNetwork.RestoreWeight("layers.txt");


            //neuralNetwork.InitAllWeight(weights);

            // var difference = neuralNetwork.Learn(expectedOutputs, inputs, 100);

            // neuralNetwork.RestoreWeight("topology.txt", "layers.txt");


            //var topology = new Topology(inputs.FirstOrDefault().Length, 1,
            //    new List<int> { 10, 9, 6, 4, 3, 3 }, 0.01);
            //var neuralNetwork = new NeuralNetwork(topology);
            //var difference = neuralNetwork.Learn(expectedOutputs, inputs, 1000);

            //var tmpInp = Normalization.Normalize(evalInputs);
            //var tmpExp = Normalization.Normalize(evalExpectedOutputs);

            var tmpInp = Normalization.Normalize(inputs);
            var tmpExp = Normalization.Normalize(expectedOutputs);

            var results = new List<List<double>>();
            for (int i = 0; i < evalExpectedOutputs.Count; i++)
            {
                var res = neuralNetwork.Predict(tmpInp[i].ToList());
                List<double> listDoule = new List<double>();
                foreach (var item in res)
                    listDoule.Add(item);
                results.Add(listDoule);
            }

            int countFailt = 0;
            for (int i = 0; i < results.Count; i++)
            {
                for (int j = 0; j < results[i].Count; j++)
                {
                    var expected = tmpExp[i][j];
                    var actual = results[i][j];
                    if (!test(expected, actual))
                    {
                        countFailt++;
                        Console.WriteLine("TEST FAIL: " + "expected:" +
                            expected.ToString() + " " + "actual:" +
                            actual.ToString() + " " + (expected - actual).ToString());
                    }
                    else
                    {
                        Console.WriteLine("TEST SUCCESS!!!");
                    }
                }
            }
            Console.WriteLine("In total: " + results.Count + " tests");
            Console.WriteLine("Fail: " + countFailt);
            Console.WriteLine("Success: " + (results.Count - countFailt));
            //Topology.Serialization("topology.txt", topology);
            //neuralNetwork.SaveWeightToFile("layers.txt");
        }

        static bool test(double exp, double act)
        {
            if (Math.Round(exp, 2) != Math.Round(act, 2))
                return false;
            return true;
        }
    }
}