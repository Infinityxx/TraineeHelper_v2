using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class RegisterManager//<TContext> where TContext : UserContext
    {
        private UserManager userManager;
        private UserEntityService<User> userEntityService;

        public RegisterManager()
        {
            userManager = new UserManager();
            userEntityService = new UserEntityService<User>();
        }

        public async Task<UserContext> RegisterTraineeUser(string email, string password, string userName)
        { 
            TraineeContext userTraineeContext = new TraineeContext(email, password, userName);
            userTraineeContext.UserType = "Trainee";
            userTraineeContext.Created = DateTime.Now;
            var result = await SaveTrainee(userTraineeContext);

            return result;
        } 

        public async Task<UserContext> RegisterGymUser(string email, string password, string userName)
        {
            GymContext userGymContext = new GymContext(email, password, userName);
            userGymContext.UserType = "Gym";
            userGymContext.Created = DateTime.Now;
            var result = await SaveGym(userGymContext);

            return result;
        }

        public async Task<UserContext> RegisterTrainerUser(string email, string password, string userName)
        {
            //TrainerContext userTrainerContext = new TrainerContext(email, password);
            TrainerContext userTrainerContext = new TrainerContext(email, password, userName);
            userTrainerContext.UserType = "Trainer";
            userTrainerContext.Created = DateTime.Now;
            var result = await SaveTrainer(userTrainerContext);

            return result;
        }

        public async Task<GymContext> SaveGym(GymContext userGymContext)
        {
            if (string.IsNullOrEmpty(userGymContext.Id))
            {
                Gym gymUser = new Gym();
                gymUser = userGymContext.ConvertToNewGym();

                var result = await userEntityService.SaveOneAsync(gymUser);
                return ((Gym)result).ConvertToGymContext();
            }
            //var update = UserConverter.ConvertToUser(userGymContext);
            var update = userGymContext.ConvertToGym();
            await userEntityService.Update(update);
            return userGymContext;
        }

        public async Task<TrainerContext> SaveTrainer(TrainerContext userTrainerContext)
        {
            if (string.IsNullOrEmpty(userTrainerContext.Id))
            {
                Trainer trainerUser = new Trainer();
                trainerUser = userTrainerContext.ConvertToNewTrainer();

                var result = await userEntityService.SaveOneAsync(trainerUser);
                return ((Trainer)result).ConvertToTrainerContext();
            }
            //var update = UserConverter.ConvertToUser(userTrainerContext);
            var update = userTrainerContext.ConvertToTrainer();
            await userEntityService.Update(update);
            return userTrainerContext;
        }
      
        public async Task<TraineeContext> SaveTrainee(TraineeContext userTraineeContext)
        {   
            if (string.IsNullOrEmpty(userTraineeContext.Id))
            {
                Trainee traineeUser = new Trainee();
                traineeUser = userTraineeContext.ConvertToNewTrainee();

                var result = await userEntityService.SaveOneAsync(traineeUser);
                return ((Trainee)result).ConvertToTraineeContext();      
            }
            var update = userTraineeContext.ConvertToTrainee();
            //var update = UserConverter.ConvertToTrainer(userTraineeContext);
            await userEntityService.Update(update);
            return userTraineeContext;
        }

        public async Task<bool> CheckEmailExistence(string email)
        {
            return await userEntityService.CheckEmailExistence(email);
        }

        public async Task<bool> CheckUserNameExistence(string userName)
        {
            return await userEntityService.CheckUserNameExistence(userName);
        }

        public async Task<GymContext> RegisterGym(string email, string password)
        {
            var gymManager = new GymManager();
            var gymContext = new GymContext();

            UserContext userGymContext = new UserContext(email, password);
            userGymContext.UserType = "Gym";
            userGymContext.Created = DateTime.Now;
            var result = await userManager.SaveUser(userGymContext);

            if(userGymContext.Id != null)
            { 
                InitializeNewUser(userGymContext, gymContext);
                gymContext.UserType = "Gym";
                await gymManager.SaveOneAsync(gymContext);
            }
            await gymManager.SaveOneAsync(gymContext);

            return gymContext;
        }


        public void InitializeNewUser(UserContext userEntityContext, UserContext entityContext)
        {
            entityContext.UserName = userEntityContext.UserName;
            entityContext.Email = userEntityContext.Email;
            entityContext.Location = userEntityContext.Location;
            entityContext.IsActive = userEntityContext.IsActive;
            entityContext.Modified = userEntityContext.Modified;
            entityContext.Password = userEntityContext.Password;
            entityContext.Created = userEntityContext.Created;
            
        }
    }
}
