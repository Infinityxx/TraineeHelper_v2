using DataModel.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class UserConverter
    {
        public static ObjectId ConvertToObjectId(this string id)
        {
            return ObjectId.Parse(id);
        }

        public static string ConvertoToId(this ObjectId Id)
        {
            return Id.ToString();
        }

        public static User ConvertToUser(this UserContext context, bool generateId = false)
        {
            User user = new User();
            user.Id = generateId ? ObjectId.GenerateNewId() : ObjectId.Parse(context.Id);
            user.UserName = context.UserName;
            user.Location.City = context.Location.City;
            user.Location.Country = context.Location.Country;
            user.UserType = context.UserType;
            user.Created = context.Created;
            user.IsActive = context.IsActive;
            user.Email = context.Email;
            user.Description = context.Description;
            user.Modified = context.Modified;
            user.Password = context.Password;
            user.PhoneNumber = context.PhoneNumber;
            user.PhoneVisibility = context.PhoneVisibility;
            user.UserVisibility = context.UserVisibility;
            user.Name = context.Name;
            user.Birthday = context.Birthday;
            return user;
        }

        public static UserContext ConvertToUserContext(this IUser user)
        {
            return new UserContext
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                UserType = user.UserType,
                Created = user.Created,
                IsActive = user.IsActive,
                Email = user.Email,
                Description = user.Description,
                Name = user.Name,
                Location = user.Location.ConvertToLocationContext(),
                Modified = user.Modified,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                PhoneVisibility = user.PhoneVisibility,
                UserVisibility = user.UserVisibility,
                Birthday = user.Birthday
            };
        }

        public static LocationContext ConvertToLocationContext(this Location Location)
        {
            LocationContext locationContext = new LocationContext();
            if(Location != null)
            {
                locationContext.Country = Location.Country;
                locationContext.City = Location.City;
                return locationContext;
            }
            return locationContext;
        }

        public static IEnumerable<UserContext> ConvertToUserContextList(this IEnumerable<IUser> contexts)
        {
            return contexts.Select(ConvertToUserContext);
        }

        public static Location ConvertToLocation(this LocationContext locationContext)
        {
            Location location = new Location();
            if(locationContext != null)
            {
                location.City = locationContext.City;
                location.Country = locationContext.Country;
                return location;
            }
            return location;
        }
    }
}
