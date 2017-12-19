using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AssignmentProblem.Library;
using AssignmentProblem.Views;
using Newtonsoft.Json;

namespace AssignmentProblem.ViewModels
{
    public class AgentViewModel : ViewModelBase, IDisposable
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

            ViewOperationsCommand = new RelayCommand(p => ViewOperations());
        }

        public RelayCommand ViewOperationsCommand { get; private set; }

        private void ViewOperations()
        {
            var window = new OperationForAgent(this);
            window.Show();
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
            operation.Agent = this.Agent;

            //var ticks = Operations.Select(x => Operation.GetTiming(x.Complexity, Agent.CpuFrequency)).Sum(x => x.Ticks);
            //OperationsTime = TimeSpan.FromTicks(ticks);

            OperationsTime = TimeSpan.FromTicks(Operations.Select(x => x.Time).Sum(x => x.Ticks));
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
