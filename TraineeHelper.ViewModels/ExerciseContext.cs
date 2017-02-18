using TraineeHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class ExerciseContext
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseType { get; set; }
        public string Description { get; set; }
        public int SetsNum { get; set; }
        public int Repetitions { get; set; }
        public string Note { get; set; }
        public string MuscleName { get; set; }
        public ExerciseStatus Status { get; set; }


        public ExerciseContext()
        {
            this.Description = "";
            this.ExerciseId = 0;
            this.ExerciseName = "";
            this.MuscleName = "";
            this.Note = "";
            this.SetsNum = 0;
            this.Status = 0;
            this.Repetitions = 0;
            this.ExerciseType = "";
        }

    }

   
}
