using AssignmentProblem.Models;
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
            Agents = new ObservableCollection<AgentViewModel>();
            Operations = new ObservableCollection<OperationViewModel>();
        }

        #region commands

        public RelayCommand Command { get; private set; }

        #endregion

        public ObservableCollection<AgentViewModel> Agents { get; private set; }
        public ObservableCollection<OperationViewModel> Operations { get; private set; }
    }
}
