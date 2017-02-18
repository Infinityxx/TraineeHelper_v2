using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;

namespace DataModel.DataModel
{
    public class TraineeExercises
    {
        public Trainee Trainee { get; set; }
        public List<Exercise> AvailableExercise { get; set; }
    }
}
