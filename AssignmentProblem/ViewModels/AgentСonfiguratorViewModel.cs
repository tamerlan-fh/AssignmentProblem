using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.ViewModels
{
    class AgentСonfiguratorViewModel : ViewModelBase
    {
        public AgentСonfiguratorViewModel()
        {
            EnableAddModeCommand = new RelayCommand(p => IsAddMode = true, p => !IsAddMode);
            AddAgentCommand = new RelayCommand(p => AddAgent(), p => CanAddAgent());
            RemoveAgentCommand = new RelayCommand(p => RemoveAgent(p), p => CanRemoveAgent(p));
        }

        public RelayCommand EnableAddModeCommand { get; private set; }
        public RelayCommand AddAgentCommand { get; private set; }
        public RelayCommand RemoveAgentCommand { get; private set; }


        public ObservableCollection<Agent> Agents { get { return null; } }

        private void AddAgent()
        {

        }

        private bool CanAddAgent()
        {
            return IsAddMode;
        }

        private void RemoveAgent(object agent)
        {

        }

        private bool CanRemoveAgent(object agent)
        {
            return agent is Agent;
        }


        public bool IsAddMode
        {
            get { return isAddMode; }
            set { isAddMode = value; OnPropertyChanged("IsAddMode"); }
        }
        private bool isAddMode;
    }
}
