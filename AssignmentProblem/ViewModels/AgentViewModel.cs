using AssignmentProblem.Library;
using AssignmentProblem.Models;
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
        private readonly Agent agent;
        private readonly TcpClient client;
        public AgentViewModel(Agent agent, TcpClient client)
        {
            this.agent = agent;
            this.client = client;
            IsEnabled = true;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; OnPropertyChanged("IsEnabled"); }
        }
        private bool isEnabled;

        public string Name { get { return agent.Name; } }

        public double CpuFrequency { get { return agent.CpuFrequency; } }
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

        public void SendOperations()
        {

        }

        public void Dispose()
        {
            client.Close();
            client.Dispose();
        }

        public ObservableCollection<OperationViewModel> Operations { get; private set; }
    }
}
