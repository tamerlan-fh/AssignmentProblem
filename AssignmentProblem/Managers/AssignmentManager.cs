using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.Managers
{
    /// <summary>
    /// менеджер назначения
    /// </summary>
    class AssignmentManager
    {
        public static AssignmentManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new AssignmentManager();
                return instance;
            }
        }
        private static AssignmentManager instance;
        private AssignmentManager() { }
    }
}
