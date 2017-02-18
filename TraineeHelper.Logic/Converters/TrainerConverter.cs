using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class TrainerConverter
    {
        public static Trainer ConvertToNewTrainer(this TrainerContext context)
        {
            return new Trainer
            {
                Id = ObjectId.GenerateNewId(),
                UserName = context.UserName,
                Name = context.Name,
                UserType = context.UserType,
                Created = context.Created,
                IsActive = context.IsActive,
                Email = context.Email,
                Description = context.Description,
                Gender = context.Gender,
                Location = context.Location.ConvertToLocation(),
                Modified = context.Modified,
                Password = context.Password,
                PhoneNumber = context.PhoneNumber,
                PhoneVisibility = context.PhoneVisibility,
                UserVisibility = context.UserVisibility,
                ExperienceYears = context.ExperienceYears,
                Expertise = null,
                Price = context.Price,
                Reputation = context.Reputation,
                Birthday = context.Birthday
            };
        }

        public static TrainerContext ConvertToTrainerContext(this ITrainer trainer)
        {
            TrainerContext context = new TrainerContext();
            if (null == trainer)
                return context;
            context.Id = trainer.Id.ToString();
            context.UserName = trainer.UserName;
            context.UserType = trainer.UserType;
            context.Name = trainer.Name;
            context.Created = trainer.Created;
            context.IsActive = trainer.IsActive;
            context.Email = trainer.Email;
            context.Description = trainer.Description;
            context.Location = trainer.Location.ConvertToLocationContext();
            context.Modified = trainer.Modified;
            context.Password = trainer.Password;
            context.PhoneNumber = trainer.PhoneNumber;
            context.PhoneVisibility = trainer.PhoneVisibility;
            context.UserVisibility = trainer.UserVisibility;
            context.Gender = trainer.Gender;
            context.ExperienceYears = trainer.ExperienceYears;
            context.Expertise = trainer.Expertise;
            context.Price = trainer.Price;
            context.Reputation = trainer.Reputation;
            context.Birthday = trainer.Birthday;

            return context;
        }

        public static IEnumerable<TrainerContext> ConvertToTrainerContextList(this IEnumerable<ITrainer> contexts)
        {
            return contexts.Select(ConvertToTrainerContext);
        }

        public static Trainer ConvertToTrainer(this TrainerContext context)
        {
            return new Trainer
            {
                Id = ObjectId.Parse(context.Id.ToString()),
                UserName = context.UserName,
                UserType = context.UserType,
                Name = context.Name,
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
                Gender = context.Gender,
                ExperienceYears = context.ExperienceYears,
                Expertise = context.Expertise,
                Price = context.Price,
                Reputation = context.Reputation,
                Birthday = context.Birthday
            };
        }

        public static UserTrainerProfileDataContext ConvertToUserProfileContext(this TrainerContext trainerContext)
        {
            return new UserTrainerProfileDataContext
            {
                UserId = trainerContext.Id,
                Email = trainerContext.Email,
                UserName = trainerContext.UserName,
                UserType = trainerContext.UserType,
                Gender = trainerContext.Gender,
                Name = trainerContext.Name,
                ExperienceYears = trainerContext.ExperienceYears,
                Expertise = trainerContext.Expertise,
                IsActive = trainerContext.IsActive,
                Location = trainerContext.Location,
                PhoneNumber = trainerContext.PhoneNumber,
                Price = trainerContext.Price,
                Reputation = trainerContext.Reputation,
                Birthday = trainerContext.Birthday
            };
        }
    }
}
