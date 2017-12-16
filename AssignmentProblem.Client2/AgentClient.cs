using AssignmentProblem.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace AssignmentProblem.Client
{
    class AgentClient : IDisposable
    {

        private TcpClient tcpClient;

        private Agent agent;

        public AgentClient()
        {
            tcpClient = new TcpClient();
            agent = GetAgent();

            Program.AddMessage($"создан агент {agent}");
        }

        private Agent GetAgent()
        {
            var agent = new Agent(Environment.UserName);
            return agent;
        }

        public bool Connect(string host, int port)
        {
            if(tcpClient.Connected)
            {
                Program.AddMessage($"агент {agent} уже соединен с {tcpClient.Client.RemoteEndPoint}");
                return false;
            }

            try
            {
                tcpClient.Connect(host, port);
                Program.AddMessage($"агент {agent} успешно подключился к {host}:{port}");
                return true;
            }
            catch(Exception ex)
            {
                Program.AddMessage($"агенту {agent} не удалось подключился к {host}:{port}. Ошибка {ex.Message}");
                return false;
            }
        }

        public async void WaitOperations()
        {
            if(tcpClient is null || !tcpClient.Connected)
            {
                Program.AddMessage($"агент {agent} не подключен");
                return;
            }

            Program.AddMessage($"агент {agent} ожидает получение задач");

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
                Program.AddMessage($"агенту {agent} не удалось получить задачи. Ошибка {ex.Message}");
            }
        }

        public void Disconnect()
        {
            if(tcpClient is null || !tcpClient.Connected)
            {
                Program.AddMessage($"агент {agent} не подключен");
                return;
            }

            tcpClient.Close();

            Program.AddMessage($"агент {agent} успешно разорвал подключение");
        }

        public void Dispose()
        {
            if(tcpClient != null && tcpClient.Connected)
            {
                tcpClient.Close();
                tcpClient.Dispose();
            }
        }
    }
}
