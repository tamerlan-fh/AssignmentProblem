using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private AgentManager()
        {
            Agents = new ObservableCollection<Agent>();
        }

        public ObservableCollection<Agent> Agents { get; private set; }

        public void AddAgent(Agent agent)
        {
            if(!Agents.Contains(agent))
                Agents.Add(agent);
        }

        public void RemoveAgent(Agent agent)
        {
            Agents.Remove(agent);
        }
    }
}
