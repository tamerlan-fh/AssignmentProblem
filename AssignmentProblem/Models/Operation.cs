using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.Models
{
    /// <summary>
    /// Задача
    /// </summary>
    class Operation
    {
        /// <summary>
        /// информация об обрабатываемом файле
        /// </summary>
        public FileInfo File { get; set; }

        /// <summary>
        /// оченка сложности файла
        /// </summary>
        public Estimate Estimate { get; set; }

        /// <summary>
        /// затраченное время на обработку
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// обработка файла завершена
        /// </summary>
        public bool IsDone { get { return Result != null; } }

        /// <summary>
        /// результат обработки
        /// </summary>
        public object Result { get; set; }
    }
}
