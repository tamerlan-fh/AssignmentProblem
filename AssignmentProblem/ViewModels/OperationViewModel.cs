using System;
using AssignmentProblem.Library;

namespace AssignmentProblem.ViewModels
{
    public class OperationViewModel : ViewModelBase
    {
        public Operation Model { get; private set; }
        public OperationViewModel(Operation operation)
        {
            this.Model = operation;
        }

        public Agent Agent
        {
            get { return agent; }
            set
            {
                agent = value;
                OnPropertyChanged("Agent");
                Time = Operation.GetTiming(Complexity, Agent.CpuFrequency);
            }
        }
        private Agent agent;

        public TimeSpan Time { get; private set; }

        public int ID { get { return Model.ID; } }

        public double Complexity { get { return Model.Complexity; } }

        public string Filename { get { return Model.Filename; } }

        public bool IsComplected { get { return Result != null; } }

        public Product Result
        {
            get { return result; }
            set { result = value; OnPropertyChanged("Result"); OnPropertyChanged("IsComplected"); }
        }
        private Product result;
    }
}
