using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class UserTraineeProfileDataContext
    {    
        public ExerciseContext FavouriteExercise { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public Gender Gender { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public LocationContext Location { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
    }
}
