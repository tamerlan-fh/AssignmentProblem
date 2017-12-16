using AssignmentProblem.Library;
using System;

namespace AssignmentProblem.Client.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            CreateAgentCommand = new RelayCommand(p => CreateAgent(), p => CanCreateAgent());
            ConnectCommand = new RelayCommand(p => Connect(), p => CanConnect());

            Name = Environment.UserName;
            CpuFrequency = 3.0;
            Ram = 8;
            Host = "127.0.0.1";
            Port = 7777;
        }

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
            get { return AgentClient.Instance.AgentNotCreated; }
        }

        public bool AgentNotConnected
        {
            get { return !AgentClient.Instance.IsConnected; }
        }

        #endregion

        #region methods

        private void CreateAgent()
        {
            AgentClient.Instance.CreateAgent(new Agent()
            {
                Name = Name,
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
            AgentClient.Instance.Connect(Host, port);
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
