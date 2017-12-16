using Newtonsoft.Json;

namespace AssignmentProblem.Library
{
    /// <summary>
    /// исполнитель
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// наименование агента
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// частота процессора, GHz
        /// </summary>
        [JsonProperty("cpu_frequency")]
        public double CpuFrequency { get; set; }

        /// <summary>
        /// объем оперативной памяти, GB
        /// </summary>
        [JsonProperty("ram")]
        public double Ram { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
