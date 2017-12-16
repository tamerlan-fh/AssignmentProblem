using AssignmentProblem.Managers;
using AssignmentProblem.Models;
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
            Operations = new ObservableCollection<OperationViewModel>();
      //      AgentManager.Instance.ServerStart();
        }

        public RelayCommand AgentsEditCommand { get; private set; }

        public ObservableCollection<AgentViewModel> Agents { get { return AgentManager.Instance.Agents; } }
        public ObservableCollection<OperationViewModel> Operations { get; private set; }

        private void AgentsEdit()
        {
            var window = new AgentСonfigurator();
            window.Show();
        }
    }
}
