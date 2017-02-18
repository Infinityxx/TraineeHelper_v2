using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class GymManager
    {
        private UserEntityService<Gym> UserEntityService;

        public GymManager()
        {
            UserEntityService = new UserEntityService<Gym>();
        }

        public async Task<IEnumerable<GymContext>> GetAllAsync()
        {
            var gyms = await UserEntityService.GetAllAsync();
            var gymsContext = GymConverter.ConvertToGymContextList(gyms);
            return gymsContext;
        }

        public async Task<GymContext> SaveOneAsync(GymContext gymContext)
        {
            if (string.IsNullOrEmpty(gymContext.Id))
            {
                var gym = GymConverter.ConvertToNewGym(gymContext);
                gym = await UserEntityService.SaveOneAsync(gym);
                gymContext.Id = gym.Id.ToString();
                return gymContext;
            }

            var update = GymConverter.ConvertToGym(gymContext);
            await UserEntityService.Update(update);
            return gymContext;
        }

        public async Task<bool> DeleteGym(string id)
        {
            UserManager userManager = new UserManager();
            var gym = await GetById(id);
            return await UserEntityService.Delete(id);
        }

        public async Task<GymContext> GetById(string id)
        {
            Gym gym = await UserEntityService.GetById(id);
            return gym.ConvertToGymContext();
        }

        public async Task<UserGymProfileDataContext> ViewGymProfile(string userId)
        {
            GymContext gymContext = await GetById(userId);

            UserGymProfileDataContext gymProfileData = gymContext.ConvertToUserProfileContext();
            return gymProfileData;
        }

        public async Task<bool> UpdateGymReputation(string userId, int ratingValue)
        {
            int avg = 0;
            Gym gym = await UserEntityService.GetById(userId);
            avg = gym.Reputation;
            avg = (avg + ratingValue) / 2;
            gym.Reputation = avg;

            var success = await UserEntityService.Update(gym);
            if (null != success)
                return true;
            else
                return false;
        }

    }
}
