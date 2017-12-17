using AssignmentProblem.Library;
using System.Collections.Generic;
using System.Linq;

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

        public Dictionary<Agent, List<Operation>> DoAssignment(Agent[] agents, Operation[] operations)
        {
            var assignment = new Dictionary<Agent, List<Operation>>();
            if(!agents.Any()) return assignment;

            foreach(var agent in agents)
                assignment.Add(agent, new List<Operation>());

            if(!operations.Any()) return assignment;

            var cicle = operations.Length / agents.Length;

            for(int i = 0; i < cicle; i++)
                for(int j = 0; j < agents.Length; j++)
                    assignment[agents[j]].Add(operations[i * agents.Length + j]);

            var mod = operations.Length % agents.Length;
            for(int i = 0; i < mod; i++)
                    assignment[agents[i]].Add(operations[cicle * agents.Length + i]);

            return assignment;
        }
    }
}
