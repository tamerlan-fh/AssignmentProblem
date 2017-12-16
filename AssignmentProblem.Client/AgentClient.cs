using AssignmentProblem.Library;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace AssignmentProblem.Client
{
    class AgentClient : IDisposable
    {
        private TcpClient tcpClient;

        private readonly Agent agent;

        public AgentClient(Agent agent)
        {
            tcpClient = new TcpClient();
            this.agent = agent;
        }

        private Agent GetAgent()
        {
            var agent = new Agent(Environment.UserName);
            return agent;
        }

        public bool IsConnected
        {
            get { return tcpClient.Connected; }
        }

        public void Connect(string host, int port)
        {
            if(IsConnected)
                return;

            try
            {
                tcpClient.Connect(host, port);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void WaitOperations()
        {
            if(!IsConnected)
                return;

            var response = new StringBuilder();
            try
            {
                using(var stream = tcpClient.GetStream())
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

        public void Disconnect()
        {
            if(!IsConnected)
                return;

            tcpClient.Close();
        }

        public void Dispose()
        {
            if(IsConnected)
                tcpClient.Close();

            tcpClient.Dispose();
        }
    }
}
