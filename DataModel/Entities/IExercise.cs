using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace DataModel.Entities
{
    public interface IExercise
    {
        int ExerciseId { get; set; }
        string ExerciseName { get; set; }
        string ExerciseType { get; set; }
        string Description { get; set; }
        int SetsNum { get; set; }
        int Repetitions { get; set; }
        string Note { get; set; }
        string MuscleName { get; set; }
        ExerciseStatus Status { get; set; }  
    }
}
