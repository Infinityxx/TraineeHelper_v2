using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class TraineeManager
    {
        private UserEntityService<Trainee> UserEntityService;

        public TraineeManager()
        {
            UserEntityService = new UserEntityService<Trainee>();
        }

        public async Task<IEnumerable<TraineeContext>> GetAllAsync()
        {
            var trainees = await UserEntityService.GetAllAsync();
            var traineesContext = TraineeConverter.ConvertToTraineeContextList(trainees);
            return traineesContext;
        }

        public async Task<TraineeContext> SaveOneAsync(TraineeContext traineeContext)
        {
            if(string.IsNullOrEmpty(traineeContext.Id))
            {
                var trainee = TraineeConverter.ConvertToNewTrainee(traineeContext);
                trainee = await UserEntityService.SaveOneAsync(trainee);
                traineeContext.Id = trainee.Id.ToString();
                return traineeContext;
            }

            var update = TraineeConverter.ConvertToTrainee(traineeContext);
            await UserEntityService.Update(update);
            return traineeContext;
        }

        public async Task<bool> DeleteTrainee(string id)
        {
            UserManager userManager = new UserManager();
            return await userManager.DeleteUser(id);
        }

        public async Task<TraineeContext> GetById(string id)
        {
            Trainee trainee = await UserEntityService.GetById(id);
            return trainee.ConvertToTraineeContext();
        }

        public async Task<bool> UpdateMedicalCondition(string traineeId ,MedicalConditionContext medicalConditionctx)
        {
            var trainee = await GetById(traineeId);
            trainee.MedicalCondition.Age = medicalConditionctx.Age;
            trainee.MedicalCondition.FatPercent = medicalConditionctx.FatPercent;
            trainee.MedicalCondition.Height = medicalConditionctx.Height;
            trainee.MedicalCondition.Weight = medicalConditionctx.Weight;
            trainee.MedicalCondition.MuscleMass = medicalConditionctx.MuscleMass;
            var result = await SaveOneAsync(trainee);
            if (result != null)
                return true;
            return false;
        }

        //public async Task UpdateTrainingPlan(TraineeContext traineeContext, ExerciseContext exerciseContext)
        //{
        //    traineeContext.TrainingPlans.Exercises.Add(exerciseContext);
        //    await SaveOneAsync(traineeContext);
        //}

        //public async Task<TrainingPlanContext> GetTraineeTrainingPlanAsync(string id)
        //{
        //    var trainee = await GetById(id);
        //    return trainee.TrainingPlan;
        //}

        public async Task<List<ChallengeContext>> GetTraineeAchievements(string id)
        {
            var trainee = await GetById(id);
            return trainee.Achievements;
        }

        public async Task<UserTraineeProfileDataContext> ViewTraineeProfile(string userId)
        {
            TraineeContext traineeContext = await GetById(userId);

            UserTraineeProfileDataContext traineeProfileData = traineeContext.ConvertToUserProfileContext();
            return traineeProfileData;
        }
    }
}
