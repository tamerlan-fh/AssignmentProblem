using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.ViewModels
{
    class AgentViewModel : ViewModelBase
    {
        private Agent model;
        public AgentViewModel(Agent agent)
        {
            this.model = agent;
        }

        public ObservableCollection<OperationViewModel> Operations { get; private set; }
    }
}
