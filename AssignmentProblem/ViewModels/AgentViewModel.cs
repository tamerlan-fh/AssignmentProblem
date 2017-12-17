using AssignmentProblem.Library;
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
        public AgentViewModel(Agent agent, TcpClient client)
        {
            this.Agent = agent;
            this.client = client;
            IsEnabled = true;

            Operations = new ObservableCollection<OperationViewModel>();
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

        public void SendOperations()
        {

        }

        private async void WaitProducts()
        {
            var response = new StringBuilder();
            try
            {
                using(var stream = client.GetStream())
                {
                    var data = new byte[256];
                    do
                    {
                        int bytes = await stream.ReadAsync(data, 0, data.Length);
                        response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while(stream.DataAvailable); // пока данные есть в потоке
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Dispose()
        {
            client.Close();
            client.Dispose();
        }
    }
}
