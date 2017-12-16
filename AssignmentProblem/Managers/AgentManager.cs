using AssignmentProblem.Library;
using AssignmentProblem.Models;
using AssignmentProblem.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssignmentProblem.Managers
{
    /// <summary>
    /// менеджер исполнителей
    /// </summary>
    class AgentManager : ViewModelBase, IDisposable
    {
        public static AgentManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new AgentManager();
                return instance;
            }
        }
        private static AgentManager instance;

        private TcpListener server = null;
        private AgentManager()
        {
            Port = 7777;
            Host = GetCurrentIpAddress();

            Agents = new ObservableCollection<AgentViewModel>();
            ServerStart();
        }

        public string Host { get; private set; }

        public int Port { get; private set; }

        public string Address { get { return $"{Host}:{Port}"; } }

        public ObservableCollection<AgentViewModel> Agents { get; private set; }

        public int EnabledAgentsCount { get { return Agents.Where(x => x.IsEnabled).Count(); } }

        public async void ServerStart()
        {
            if(server != null) return;

            var address = IPAddress.Parse("127.0.0.1");

            try
            {
                server = new TcpListener(address, Port);
                server.Start();

                while(true)
                {
                    try
                    {
                        var client = await server.AcceptTcpClientAsync();
                        var response = new StringBuilder();
                        using(var stream = client.GetStream())
                        {
                            var data = new byte[256];
                            do
                            {
                                int length = await stream.ReadAsync(data, 0, data.Length);
                                response.Append(Encoding.UTF8.GetString(data, 0, length));
                            }
                            while(stream.DataAvailable); // пока данные есть в потоке
                        }

                        var agent = JsonConvert.DeserializeObject<Agent>(response.ToString());

                        AddAgent(agent, client);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            finally
            {
                server.Stop();
            }
        }

        public void ServerStop()
        {
            server.Stop();
        }

        private void AddAgent(Agent agent, TcpClient client)
        {
            Agents.Add(new AgentViewModel(agent, client));
        }

        public string GetCurrentIpAddress()
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var task = client.GetStringAsync(@"https://api.ipify.org");
                    task.Wait();
                    return task.Result;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
                return string.Empty;
            }
        }

        public void Dispose()
        {
            foreach(var agent in Agents)
                agent.Dispose();

            server.Stop();
        }
    }
}
