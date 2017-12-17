using AssignmentProblem.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssignmentProblem.ViewModels
{
    class AgentViewModel : ViewModelBase, IDisposable
    {
        public Agent Agent { get; private set; }

        private readonly TcpClient client;
        private readonly NetworkStream stream;
        public AgentViewModel(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();

            Task.Run(async () =>
            {
                Agent = await GetAgent();
            }).Wait();

            IsEnabled = true;

            Operations = new ObservableCollection<OperationViewModel>();
            TimeSpan t0 = TimeSpan.FromSeconds(1);
            TimeSpan t1 = TimeSpan.FromSeconds(1);
            TimeSpan t2 = TimeSpan.FromSeconds(1);

            TimeSpan t = t0 + t1 + t2;
        }

        private async Task<Agent> GetAgent()
        {
            var response = new StringBuilder();

            var data = new byte[256];
            do
            {
                int length = await stream.ReadAsync(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, length));
            }
            while(stream.DataAvailable); // пока данные есть в потоке

            return JsonConvert.DeserializeObject<Agent>(response.ToString());
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; OnPropertyChanged("IsEnabled"); }
        }
        private bool isEnabled;

        public string Name { get { return Agent.Name; } }

        public double CpuFrequency { get { return Agent.CpuFrequency; } }

        public ObservableCollection<OperationViewModel> Operations { get; private set; }

        public TimeSpan OperationsTime
        {
            get { return operationsTime; }
            private set { operationsTime = value; OnPropertyChanged("OperationsTime"); }
        }
        private TimeSpan operationsTime;

        public int OperationsComplectedCount
        {
            get { return Operations.Count(x => x.IsComplected); }
        }

        public void AddOperation(OperationViewModel operation)
        {
            Operations.Add(operation);

            var ticks = Operations.Select(x => Operation.GetTiming(x.Complexity, Agent.CpuFrequency)).Sum(x => x.Ticks);
            OperationsTime = TimeSpan.FromTicks(ticks);
        }

        public async void SendOperations()
        {
            try
            {
                var operations = Operations.Select(x => x.Model).ToArray();
                var message = JsonConvert.SerializeObject(operations);
                var data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                var product = await WaitProduct();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<Product> WaitProduct()
        {
            var response = new StringBuilder();
            try
            {
                var data = new byte[256];
                do
                {
                    int bytes = await stream.ReadAsync(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while(stream.DataAvailable); // пока данные есть в потоке

                return JsonConvert.DeserializeObject<Product>(response.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void Dispose()
        {
            stream.Close();
            stream.Dispose();
            client.Close();
            client.Dispose();
        }
    }
}
