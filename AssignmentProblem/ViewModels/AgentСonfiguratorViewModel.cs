﻿using AssignmentProblem.Managers;
using System.Collections.ObjectModel;

namespace AssignmentProblem.ViewModels
{
    class AgentСonfiguratorViewModel : ViewModelBase
    {
        public ObservableCollection<AgentViewModel> Agents { get { return AgentManager.Instance.Agents; } }

        public string Address { get { return AgentManager.Instance.Address; } }
    }
}
