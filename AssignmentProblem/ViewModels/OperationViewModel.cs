using AssignmentProblem.Library;
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

        public int ID { get { return model.ID; } }

        public double Complexity { get { return model.Complexity; } }
    }
}
