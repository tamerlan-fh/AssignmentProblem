using Newtonsoft.Json;
using System;

namespace AssignmentProblem.Library
{
    /// <summary>
    /// задача
    /// </summary>
    class Operation
    {
        /// <summary>
        /// уникальный идентификатор задачи
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// сложность задачи
        /// </summary>
        [JsonProperty("complexity")]
        public double Complexity { get; set; }

        /// <summary>
        /// метод расчета времени, затраченного на выполнение задачи
        /// </summary>
        /// <param name="complexity">сложность задачи</param>
        /// <param name="frequency">тактовая частота процессора</param>
        /// <returns>время выполнения</returns>
        public static TimeSpan GetTiming(double complexity, double frequency)
        {
            return TimeSpan.FromSeconds(frequency * complexity);
        }
    }
}
