using AssignmentProblem.Managers;
using System.Collections.ObjectModel;

namespace AssignmentProblem.ViewModels
{
    class OperationConfiguratorViewModel : ViewModelBase
    {
        public ObservableCollection<OperationViewModel> Operations { get { return OperationManager.Instance.Operations; } }
    }
}
