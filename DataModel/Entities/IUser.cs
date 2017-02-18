using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;

namespace TraineeHelper.Models
{
    public interface IUser : IMongoCommon
    {
        string UserType { get; set; }
        string UserName { get; set; }
        [BsonIgnoreIfNull]
        string Name { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        Location Location { get; set; }
        [BsonIgnoreIfNull]
        string PhoneNumber { get; set; }
        bool UserVisibility { get; set; }
        bool PhoneVisibility { get; set; }
        [BsonIgnoreIfNull]
        DateTime Birthday { get; set; }
    }
}
