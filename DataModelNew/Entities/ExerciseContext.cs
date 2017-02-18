using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelNew.Entities
{
    [Serializable]
    public class ExerciseContext
    {
        public int ExerciseId { get; set; }
        public int PlanId { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseType { get; set; }
        public string Description { get; set; }
        public int SetsNum { get; set; }
        public int Repetitions { get; set; }
        public string Note { get; set; }
        public string MuscleName { get; set; }
        public ExerciseStatus Status { get; set; }

        public enum ExerciseStatus
        {
            Unstarted = 0,
            InProgress = 1,
            Completed = 2
        }
    }
}
