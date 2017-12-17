using AssignmentProblem.Library;

namespace AssignmentProblem.ViewModels
{
    class OperationViewModel : ViewModelBase
    {
        public Operation Model { get; private set; }
        public OperationViewModel(Operation operation)
        {
            this.Model = operation;
        }

        public int ID { get { return Model.ID; } }

        public double Complexity { get { return Model.Complexity; } }

        public string Filename { get { return Model.Filename; } }
    }
}
