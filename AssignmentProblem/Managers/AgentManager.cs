using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.Managers
{
    /// <summary>
    /// менеджер исполнителей
    /// </summary>
    class AgentManager
    {
        public static AgentManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new AgentManager();
                return instance;
            }
        }
        private static AgentManager instance;
        private AgentManager() { }

        public Agent[] GetAgents()
        {
            return null;
        }

        public void AddAgent(Agent agent)
        {

        }

        public void RemoveAgent(Agent agent)
        {

        }
    }
}
