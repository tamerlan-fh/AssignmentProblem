using AssignmentProblem.Library;
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

        public Agent[] GetAgents()
        {
            return Agents.Select(x => x.Agent).ToArray();
        }

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
                        AddAgent(client);
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

        private void AddAgent(TcpClient client)
        {
            Agents.Add(new AgentViewModel(client));
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
