using AssignmentProblem.Managers;
using AssignmentProblem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            AgentsEditCommand = new RelayCommand(p => AgentsEdit());
            OperationsEditCommand = new RelayCommand(p => OperationsEdit());
            AssignmentCommand = new RelayCommand(p => Assignment(), p => CanAssignment());
        }

        public RelayCommand AgentsEditCommand { get; private set; }
        public RelayCommand OperationsEditCommand { get; private set; }
        public RelayCommand AssignmentCommand { get; private set; }

        public ObservableCollection<AgentViewModel> Agents { get { return AgentManager.Instance.Agents; } }
        public ObservableCollection<OperationViewModel> Operations { get { return OperationManager.Instance.Operations; } }

        private void AgentsEdit()
        {
            var window = new AgentСonfigurator();
            window.Show();
        }

        private void OperationsEdit()
        {
            var window = new OperationConfigurator();
            window.Show();
        }

        private void Assignment()
        {
            var result = AssignmentManager.Instance.DoAssignment(AgentManager.Instance.GetAgents(), OperationManager.Instance.GetOperations());

            foreach(var agent in Agents)
                foreach(var operation in result[agent.Agent])
                    agent.AddOperation(Operations.FirstOrDefault(x => x.ID == operation.ID));
        }

        private bool CanAssignment()
        {
            return Agents.Any() && Operations.Any();
        }
    }
}
