using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem
{
    class Repository
    {
        public static Repository Instance
        {
            get
            {
                if(instance == null)
                    instance = new Repository();
                return instance;
            }
        }
        private static Repository instance;
        private Repository() { }
    }
}
