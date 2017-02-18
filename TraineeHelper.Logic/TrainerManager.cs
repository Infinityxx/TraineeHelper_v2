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
    public class TrainerManager
    {
        private UserEntityService<Trainer> UserEntityService;

        public TrainerManager()
        {
            UserEntityService = new UserEntityService<Trainer>();
        }

        public async Task<IEnumerable<TrainerContext>> GetAllAsync()
        {
            var trainers = await UserEntityService.GetAllAsync();
            var trainersContext = TrainerConverter.ConvertToTrainerContextList(trainers);
            return trainersContext;
        }

        public async Task<TrainerContext> SaveOneAsync(TrainerContext trainerContext)
        {
            if(string.IsNullOrEmpty(trainerContext.Id))
            {
                var trainer = TrainerConverter.ConvertToNewTrainer(trainerContext);
                trainer = await UserEntityService.SaveOneAsync(trainer);
                trainerContext.Id = trainer.Id.ToString();
                return trainerContext;
            }

            var update = TrainerConverter.ConvertToTrainer(trainerContext);
            await UserEntityService.Update(update);
            return trainerContext;
        }

        public async Task<bool> DeleteTrainer(string id)
        {
            UserManager userManager = new UserManager();
            var trainer = await GetById(id);
            //string trainerUserId = trainer.TrainerUser;
            await userManager.DeleteUser(id);
            return await UserEntityService.Delete(id);
        }

        public async Task<TrainerContext> GetById(string id)
        {
            Trainer trainer = await UserEntityService.GetById(id);
            return trainer.ConvertToTrainerContext();
        }

        public async Task<UserTrainerProfileDataContext> ViewTrainerProfile(string userId)
        {
             TrainerContext trainerContext = await GetById(userId);
            
            UserTrainerProfileDataContext trainerProfileData = trainerContext.ConvertToUserProfileContext();
            return trainerProfileData;
        }

        //TODO fix avg reputation calc
        public async Task<bool> UpdateTrainerReputation(string userId, int ratingValue)
        {
            int avg = 0;
            Trainer trainer = await UserEntityService.GetById(userId);
            avg = trainer.Reputation;
            avg = (avg + ratingValue) / 2;
            trainer.Reputation = avg;

            var success = await UserEntityService.Update(trainer);
            if (null != success)
                return true;
            else
                return false;
        }

    }
}
