using AssignmentProblem.Library;
using AssignmentProblem.Models;
using AssignmentProblem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.Managers
{
    /// <summary>
    /// менеджер операций
    /// </summary>
    class OperationManager
    {
        public static OperationManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new OperationManager();
                return instance;
            }
        }
        private static OperationManager instance;

        private OperationManager()
        {
            Operations = new ObservableCollection<OperationViewModel>();
        }

        public ObservableCollection<OperationViewModel> Operations { get; private set; }

        public void AddOperation(Operation operation)
        {

        }
    }
}
