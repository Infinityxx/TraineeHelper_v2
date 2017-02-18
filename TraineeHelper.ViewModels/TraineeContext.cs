using TraineeHelper.ViewModels;
using System;
using System.Collections.Generic;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class TraineeContext : UserContext
    {
        //public string Id { get; set; }
        public List<ChallengeContext> Achievements { get; set; }
        public ExerciseContext FavouriteExercise { get; set; }
        public MedicalConditionContext MedicalCondition { get; set; }
        public Gender Gender { get; set; }

        public TraineeContext(string email, string password, string userName)
        {
            Email = email;
            Password = password;
            UserName = userName;
        }

        public TraineeContext(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public TraineeContext()
        {
        }
    }
}
