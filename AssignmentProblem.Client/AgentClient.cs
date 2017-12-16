using AssignmentProblem.Library;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssignmentProblem.Client
{
    class AgentClient : IDisposable
    {

        public static AgentClient Instance
        {
            get
            {
                if(instance == null)
                    instance = new AgentClient();
                return instance;
            }
        }
        private static AgentClient instance;

        private AgentClient()
        {
            client = new TcpClient();
        }

        private readonly TcpClient client;
        private NetworkStream stream;

        private Agent agent = null;

        public void CreateAgent(Agent agent)
        {

            this.agent = agent;
        }

        /// <summary>
        /// исполнитель подключен
        /// </summary>
        public bool IsConnected
        {
            get { return agent != null && client.Connected; }
        }

        public bool AgentNotCreated
        {
            get { return agent is null; }
        }

        /// <summary>
        /// подключение по указанному адресу
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public async void Connect(string host, int port)
        {
            if(IsConnected)
                return;

            try
            {
                client.Connect(host, port);
                stream = client.GetStream();
                await SendAgent();
                WaitOperations();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// ожидание назначения задач для исполнителя
        /// </summary>
        private async void WaitOperations()
        {
            if(!IsConnected)
                return;

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
                ;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// оправляет информация об исполнителе
        /// </summary>
        private async Task SendAgent()
        {
            try
            {
                var message = JsonConvert.SerializeObject(agent);
                var data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// разорвать соединение
        /// </summary>
        public void Disconnect()
        {
            if(!IsConnected)
                return;

            client.Close();
        }

        public void Dispose()
        {
            if(IsConnected)
            {
                stream.Dispose();
                client.Close();
            }

            client.Dispose();
        }
    }
}
