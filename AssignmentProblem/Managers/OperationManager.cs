using AssignmentProblem.Library;
using AssignmentProblem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public Operation[] GetOperations()
        {
            return Operations.Select(x => x.Model).ToArray();
        }

        private static int index = 1;
        public void AddOperation(Operation operation)
        {
            if(Operations.Any(x => x.ID == operation.ID)) return;
            Operations.Add(new OperationViewModel(operation));
        }

        Random random = new Random(DateTime.Now.Millisecond);

        public void AddOperation(FileInfo file)
        {
            var operation = new Operation()
            {
                ID = index++,
                Content = Encoding.UTF8.GetBytes(index.ToString()),
                Complexity = 10 * random.NextDouble(),
                Filename = file.Name
            };
            AddOperation(operation);
        }

        public void RemoveOperation(Operation operation)
        {
            RemoveOperation(operation.ID);
        }
        public void RemoveOperation(int id)
        {
            var item = Operations.FirstOrDefault(x => x.ID == id);
            if(item is null) return;
            Operations.Remove(item);
        }
    }
}
