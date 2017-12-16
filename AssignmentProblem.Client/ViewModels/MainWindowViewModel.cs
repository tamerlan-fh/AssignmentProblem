using AssignmentProblem.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssignmentProblem.Client.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            CreateAgentCommand = new RelayCommand(p => CreateAgent(), p => CanCreateAgent());
            ConnectCommand = new RelayCommand(p => Connect(), p => CanConnect());

            Name = Environment.UserName;
        }

        private AgentClient agent;

        #region commands

        public RelayCommand CreateAgentCommand { get; private set; }
        public RelayCommand ConnectCommand { get; private set; }

        #endregion

        #region fields

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string name;

        public double Ram
        {
            get { return ram; }
            set { ram = value; OnPropertyChanged("Ram"); }
        }
        private double ram;

        public double CpuFrequency
        {
            get { return cpuFrequency; }
            set { cpuFrequency = value; OnPropertyChanged("CpuFrequency"); }
        }
        private double cpuFrequency;

        public string Host
        {
            get { return host; }
            set { host = value; OnPropertyChanged("Host"); }
        }
        private string host;

        public int Port
        {
            get { return port; }
            set { port = value; OnPropertyChanged("Port"); }
        }
        private int port;

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        private string status;

        public bool AgentNotCreated
        {
            get { return agent is null; }
        }

        public bool AgentNotConnected
        {
            get { return AgentNotCreated || !agent.IsConnected; }
        }

        #endregion

        #region methods

        private void CreateAgent()
        {
            agent = new AgentClient(new Agent(Name)
            {
                CpuFrequency = CpuFrequency,
                Ram = Ram
            });
            OnPropertyChanged("AgentNotCreated");
        }

        private bool CanCreateAgent()
        {
            return AgentNotCreated
                && !string.IsNullOrEmpty(Name)
                && Ram > 0
                && CpuFrequency > 0;
        }

        private void Connect()
        {
            agent.Connect(Host, port);
            OnPropertyChanged("AgentNotConnected");
        }

        private bool CanConnect()
        {
            return AgentNotConnected
                && !AgentNotCreated
                && Port > 0 &&
                !string.IsNullOrEmpty(Host);
        }

        #endregion
    }
}
