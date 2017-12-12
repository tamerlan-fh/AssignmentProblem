using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.ViewModels
{
    class OperationViewModel : ViewModelBase
    {
        private Operation model;
        public OperationViewModel(Operation operation)
        {
            this.model = operation;
        }
    }
}
