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
    public static class GymConverter
    {
        public static Gym ConvertToNewGym(this GymContext context)
        {
            return new Gym
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
                ExperienceYears = context.ExperienceYears,
                Expertise = null,
                ParkingLot = context.ParkingLot,
                Showers = context.Showers,
                Price = context.Price,
                Reputation = context.Reputation,
                Birthday = context.Birthday,
                Name = context.Name
            };
        }

        public static GymContext ConvertToGymContext(this IGym gym)
        {
            GymContext context = new GymContext();
            context.Id = gym.Id.ToString();
            context.UserName = gym.UserName;
            context.UserType = gym.UserType;
            context.Created = gym.Created;
            context.IsActive = gym.IsActive;
            context.Email = gym.Email;
            context.Description = gym.Description;
            context.Location = gym.Location.ConvertToLocationContext();
            context.Modified = gym.Modified;
            context.Password = gym.Password;
            context.PhoneNumber = gym.PhoneNumber;
            context.PhoneVisibility = gym.PhoneVisibility;
            context.UserVisibility = gym.UserVisibility;
            context.ExperienceYears = gym.ExperienceYears;
            context.Expertise = gym.Expertise;
            context.ParkingLot = gym.ParkingLot;
            context.Price = gym.Price;
            context.Showers = gym.Showers;
            context.Reputation = gym.Reputation;
            context.Name = gym.Name;
            context.Birthday = gym.Birthday;

            return context;
        }

        public static IEnumerable<GymContext> ConvertToGymContextList(this IEnumerable<IGym> contexts)
        {
            return contexts.Select(ConvertToGymContext);
        }

        public static Gym ConvertToGym(this GymContext context)
        {
            return new Gym
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
                ExperienceYears = context.ExperienceYears,
                Expertise = context.Expertise,
                ParkingLot = context.ParkingLot,
                Price = context.Price,
                Showers = context.Showers,
                Reputation = context.Reputation,
                Birthday = context.Birthday,
                Name = context.Name
            };
        }
        public static UserGymProfileDataContext ConvertToUserProfileContext(this GymContext gymContext)
        {
            return new UserGymProfileDataContext
            {
                UserId = gymContext.Id,
                Email = gymContext.Email,
                UserName = gymContext.UserName,
                UserType = gymContext.UserType,
                Name = gymContext.Name,
                ExperienceYears = gymContext.ExperienceYears,
                Expertise = gymContext.Expertise,
                IsActive = gymContext.IsActive,
                Location = gymContext.Location,
                PhoneNumber = gymContext.PhoneNumber,
                Price = gymContext.Price,
                Reputation = gymContext.Reputation,
                Birthday = gymContext.Birthday,
                ParkingLot = gymContext.ParkingLot,
                Showers = gymContext.Showers
            };
        }

      
    }
}
