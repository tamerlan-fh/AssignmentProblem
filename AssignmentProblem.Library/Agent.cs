namespace AssignmentProblem.Library
{
    /// <summary>
    /// исполнитель
    /// </summary>
    public class Agent
    {

        public Agent(string name)
        {

        }
        /// <summary>
        /// наименование агента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// число ядер
        /// </summary>
        public int ProcessorCount { get; set; }

        /// <summary>
        /// частота процессора, GHz
        /// </summary>
        public double CpuFrequency { get; set; }

        /// <summary>
        /// объем оперативной памяти, GB
        /// </summary>
        public double Ram { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
