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
            //var result = AssignmentManager.Instance.DoAssignment(AgentManager.Instance.GetAgents(), OperationManager.Instance.GetOperations());
            var agents = AgentManager.Instance.Agents.ToArray();
            OperationViewModel[] operations = null;
            operations = OperationManager.Instance.Operations.Where(x => x.Agent is null).ToArray();

            while(operations.Any())
            {
                var result = AssignmentManager.Instance.DoAssignment(agents, operations);

                foreach(var agent in agents)
                    foreach(var operation in result[agent])
                        agent.AddOperation(operation);

                operations = OperationManager.Instance.Operations.Where(x => x.Agent is null).ToArray();
            };
        }

        private bool CanAssignment()
        {
            return Agents.Any() && Operations.Any();
        }
    }
}
