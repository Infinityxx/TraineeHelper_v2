using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;
using TraineeHelper.Models;

namespace TraineeHelper.Models
{
    public interface ITrainee : IUser
    {
        List<Challenge> Challenges { get; set; }
        Exercise FavouriteExercise { get; set; }     
        MedicalCondition MedicalCondition { get; set; }
        Gender Gender { get; set; }
    }
}
