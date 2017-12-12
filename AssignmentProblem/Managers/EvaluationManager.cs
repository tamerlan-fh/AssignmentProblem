using AssignmentProblem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentProblem.Managers
{
    /// <summary>
    /// менеджер по оценке сложности задачи
    /// </summary>
    class EstimateManager
    {
        public static EstimateManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new EstimateManager();
                return instance;
            }
        }
        private static EstimateManager instance;
        private EstimateManager() { }

        /// <summary>
        /// получение оценки сложности разбора файла
        /// </summary>
        /// <param name="file">информация о файле</param>
        public Estimate GetEstimate(FileInfo file)
        {
            return null;
        }
    }
}
