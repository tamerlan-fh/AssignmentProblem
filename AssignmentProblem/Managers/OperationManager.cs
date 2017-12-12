using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
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
        private OperationManager() { }

        public Operation[] GetOperations()
        {
            return null;
        }

        public void AddOperation(Operation operation)
        {

        }

        public void RemoveOperation(Operation operation)
        {

        }
    }
}
