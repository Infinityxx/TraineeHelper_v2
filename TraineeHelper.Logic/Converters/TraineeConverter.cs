using DataModel.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class TraineeConverter
    {
       public static Trainee ConvertToNewTrainee(this TraineeContext context)
        {

            return new Trainee
            {
                Id = ObjectId.GenerateNewId(),
                UserName = context.UserName,
                UserType = context.UserType,
                Created = context.Created,
                IsActive = context.IsActive,
                Email = context.Email,
                Description = context.Description,
                Location = context.Location.ConvertToLocation(),
                Modified = context.Modified,
                Password = context.Password,
                PhoneNumber = context.PhoneNumber,
                PhoneVisibility = context.PhoneVisibility,
                UserVisibility = context.UserVisibility,
                Birthday = context.Birthday,
                FavouriteExercise = context.FavouriteExercise.ConvertToNewExercise(),
                Gender = context.Gender,
                Challenges = context.Achievements.ConvertToChallenges(),
                MedicalCondition = context.MedicalCondition.ToNewMedicalCondition(),
                Name = context.Name
            };
        }

        public static TraineeContext ConvertToTraineeContext(this ITrainee trainee)
        {
            TraineeContext context = new TraineeContext();
            context.Id = trainee.Id.ToString();
            context.UserName = trainee.UserName;
            context.UserType = trainee.UserType;
            context.Created = trainee.Created;
            context.IsActive = trainee.IsActive;
            context.Email = trainee.Email;
            context.Description = trainee.Description;
            context.Location = trainee.Location.ConvertToLocationContext();
            context.Modified = trainee.Modified;
            context.Password = trainee.Password;
            context.PhoneNumber = trainee.PhoneNumber;
            context.PhoneVisibility = trainee.PhoneVisibility;
            context.UserVisibility = trainee.UserVisibility;
            context.Birthday = trainee.Birthday;
            context.Gender = trainee.Gender;
            context.Achievements = trainee.Challenges.ConvertToChallengesContexts();
            context.MedicalCondition = trainee.MedicalCondition.ToMedicalConditionContext();
            context.Name = trainee.Name;
            context.FavouriteExercise = trainee.FavouriteExercise.ConvertToExerciseContext();

            return context;

        }

        public static IEnumerable<TraineeContext> ConvertToTraineeContextList(this IEnumerable<ITrainee> contexts)
        {
            return contexts.Select(ConvertToTraineeContext);
        }

        public static Trainee ConvertToTrainee(this TraineeContext context)
        {
            return new Trainee
            {
                Id = ObjectId.Parse(context.Id.ToString()),
                UserName = context.UserName,
                UserType = context.UserType,
                Created = context.Created,
                IsActive = context.IsActive,
                Email = context.Email,
                Description = context.Description,
                Location = context.Location.ConvertToLocation(),
                Modified = context.Modified,
                Password = context.Password,
                PhoneNumber = context.PhoneNumber,
                PhoneVisibility = context.PhoneVisibility,
                UserVisibility = context.UserVisibility,
                Birthday = context.Birthday,
                FavouriteExercise = context.FavouriteExercise.ConvertToExercise(),
                Gender = context.Gender,
                Challenges = context.Achievements.ConvertToChallenges(),
                MedicalCondition = context.MedicalCondition.toMedicalCondition(),
                Name = context.Name
            };
        }

        public static UserTraineeProfileDataContext ConvertToUserProfileContext(this TraineeContext traineeContext)
        {
            return new UserTraineeProfileDataContext
            {
                UserId = traineeContext.Id,
                Email = traineeContext.Email,
                UserName = traineeContext.UserName,
                UserType = traineeContext.UserType,
                Gender = traineeContext.Gender,
                Name = traineeContext.Name,
                IsActive = traineeContext.IsActive,
                Location = traineeContext.Location,
                PhoneNumber = traineeContext.PhoneNumber,
                Birthday = traineeContext.Birthday,
                FavouriteExercise = traineeContext.FavouriteExercise
            };
        }
    }
}
