using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyNeuralNetwork2
{
    public class NeuralNetwork : INeuralNetwork
    {
        /// <summary>
        /// Топология сети
        /// </summary>
        public Topology Topology { get; set; }

        /// <summary>
        /// Слои сети
        /// </summary>
        public List<ILayer> Layers { get; private set; }

        /// <summary>
        /// Класс ответственный за обучение сети
        /// </summary>
        public ILearn? Learner { get; }

        public NeuralNetwork(Topology topology, ILearn? learner = null)
        {
            Topology = topology;

            Layers = new List<ILayer>();
            Learner = learner;

            InitLayers();
        }

        #region InitLayers

        private void InitLayers()
        {
            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();
        }

        private void CreateInputLayer()
        {
            //+ 1 для нейрона смещения
            List<Neuron> inputNeurons = new List<Neuron>(Topology.InputCount);
            for (int i = 0; i < Topology.InputCount; i++)
            {
                Neuron neuron = new Neuron(
                    inputCount: 1,
                    (SigmoidActiveFunc)FactoryActiveFunc.Create(ActivateType.None)
                    );
                inputNeurons.Add(neuron);
            }
            ILayer? inputLayer = new Layer(inputNeurons, LayerType.Input);
            Layers.Add(inputLayer);
        }

        /// <summary>
        /// Создаем скрытые слои НС
        /// </summary>
        private void CreateHiddenLayers()
        {
            for (int i = 0; i < Topology.HiddenLayers.Count; i++)
            {
                List<Neuron> tmp = HelperCreateLayer(Topology.HiddenLayers[i], Layers[i].NeuronsList);
                //tmp.Insert(0, new Neuron(1, FactoryActiveFunc.Create(ActivateType.None)));
                Layers.Add(new Layer(tmp, LayerType.Hidden));
            }
        }

        private List<Neuron> HelperCreateLayer(int countNeuronsLayer, List<Neuron>? previousLayer)
        {
            //+ 1 для нейрона смещения
            List<Neuron> hiddenNeurons = new List<Neuron>(countNeuronsLayer);
            for (int j = 0; j < countNeuronsLayer; j++)
            {
                Neuron neuron = new Neuron(
                    inputCount: previousLayer?.Count ?? 1,
                    (SigmoidActiveFunc)FactoryActiveFunc.Create(ActivateType.Sigmoid),
                    previousLayer
                    );

                foreach (var item in Layers[^1].NeuronsList)
                {
                    foreach (var i in item.InputBonds)
                        i.Neuron_out = neuron;
                }
                hiddenNeurons.Add(neuron);
            }
            return hiddenNeurons;
        }

        /// <summary>
        /// Создаем выходной слой НС
        /// </summary>
        private void CreateOutputLayer()
        {
            for (int i = 0; i < Topology.OutputCount; i++)
            {
                List<Neuron> tmp = HelperCreateLayer(Topology.OutputCount, Layers[^1].NeuronsList);
                Layers.Add(new Layer(tmp, LayerType.Output));
            }
        }

        #endregion

        #region Learn

        public double Learn(List<double[]> expected, List<double[]> inputs, int epoch = 1000)
        {
            return Learner?.Learn(expected, inputs, this, epoch) ?? -1.0;
        }

        #endregion

        #region SaveAndRestoreLearnerResult
        public void SaveWeightToFile(string pathToLayers)
        {
            //string topologyStr = JsonSerializer.Serialize(Topology);
            //string str = JsonSerializer.Serialize(Layers);
            //string str;

            //File.WriteAllText(pathToTopology, topologyStr);



            List<double[]> weightSerial = new List<double[]>();
            for (int i = 1; i < Layers.Count; i++)
            //foreach (var i in Layers)
            {
                for (int item = 0; item < Layers[i].NeuronsCount; item++)
                //foreach (var item in i.NeuronsList)
                {
                    List<double> bondsWeight = new List<double>();
                    for (int j = 0; j < Layers[i].NeuronsList[item].InputBondsCount; j++)
                    //foreach (var j in item.InputBonds)
                    {
                        bondsWeight.Add(Layers[i].NeuronsList[item].InputBonds[j].Weight);
                        //str = JsonSerializer.Serialize(j.Weight);
                        //File.AppendAllText(path, j.Weight.ToString() + " ");
                    }
                    weightSerial.Add(bondsWeight.ToArray());
                    //File.AppendAllText(path, "\n");
                }
                //File.AppendAllText(path, "\n");
            }

            string str = JsonSerializer.Serialize(weightSerial);
            File.WriteAllText(pathToLayers, str);
        }


        //public Country(string id, string name, string phoneIndex, CountryStatusEnum status, DateTime? createdDate, DateTime? modifiedDate)
        //{
        //    Id = id;
        //    PhoneIndex = phoneIndex;
        //    Status = status;
        //    CreatedDate = createdDate;
        //    ModifiedDate = modifiedDate;
        //    Name = name;
        //}

        //public Country(string id, string name, string phoneIndex, CountryStatusEnum status, DateTime? createDate, DateTime? modifiedDate)
        //{
        //    Id = id;
        //    PhoneIndex = phoneIndex;
        //    Status = status;
        //    CreatedDate = createDate;
        //    ModifiedDate = modifiedDate;
        //    Name = name;
        //}
        public void RestoreWeight(string pathToLayers)
        {
            //Topology = JsonSerializer.Deserialize<Topology>(File.ReadAllText(pathToTopology));

            var tmp = JsonSerializer.Deserialize<List<double[]>>(File.ReadAllText(pathToLayers));
            //var tmp = JsonSerializer.Deserialize<List<Layer>>(File.ReadAllText(pathToLayers));
            InitAllWeight(tmp);
            //Layers = new List<ILayer>(tmp);

            if (Topology == null || Layers == null)
                throw new Exception("Ошибка десериализации");

            //string str = JsonSerializer.Serialize(Layers);

            //if (!File.Exists(path))
            //    return null;

            //List<double[]> restore = new List<double[]>();
            //using (var sr = new StreamReader(path))
            //{
            //    while (sr.EndOfStream)
            //    {
            //        string str = sr.ReadLine();


            //    }
            //}
        }

        #endregion
        public void InitAllWeight(List<double[]> weights)
        {
            int tmp = 0;
            for (int i = 1; i < Layers.Count; i++)
            {
                if (i + 1 != Layers.Count)
                {
                    //j = 1 если есть нейрон смещения
                    for (int j = 0; j < Layers[i].NeuronsList.Count; j++)
                    {
                        ((Neuron)Layers[i].NeuronsList[j]).SetWeight(weights[tmp++]);
                    }
                }
                else
                {
                    for (int j = 0; j < Layers[i].NeuronsList.Count; j++)
                    {
                        ((Neuron)Layers[i].NeuronsList[j]).SetWeight(weights[tmp++]);
                    }
                }

            }
        }

        #region Predict

        public List<double>? Predict(List<double> inputSignals)
        {
            //TODO: Вынести в отдельный класс метод Normalize, внедрить на него ссылку в Learner и сюда
            //Тестировать Predict на известных данных (по хорошему хотя бы 2 полных терации)
            
            //Нет смысла нормализовывать свои данные здесь, убрать
            //передавать нормализованные данные !!!
      //      if (!isNormalize)
        //        Learner?.Normalize(new List<double[]> { inputSignals.ToArray() });
            
            Layers[0].SetInputSignal(inputSignals);

            for (int i = 1; i < Layers.Count; i++)
            {
                for (int j = 0; j < Layers[i].NeuronsCount; j++)
                    Layers[i].NeuronsList[j].FeedForward();
            }

            return Layers[^1]?.NeuronsList?.Select((x) => x.Output)?.ToList();
        }

        #endregion
    }
}
