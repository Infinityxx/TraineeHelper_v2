using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataModel.Entities;
using Newtonsoft.Json;
using TraineeHelper.Models.Entities;

namespace TraineeHelper.Models
{

    [Serializable, JsonObject]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Trainee), typeof(Trainer), typeof(Gym))]
    public class User : IUser
    {

        [BsonId]
        public ObjectId Id { get; set; }
        public string UserType { get; set; }


        //[BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string UserName { get; set; }


        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Name { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Password { get; set; }


        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Email  { get; set; }


        [BsonIgnoreIfNull]
        public Location Location { get; set; }


        //[BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonIgnoreIfNull]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Birthday { get; set; }

        //[BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string PhoneNumber { get; set; }
        public bool PhoneVisibility { get; set; }
        public bool UserVisibility { get; set; }

        public bool IsActive { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Modified { get; set; }
       

        public User()
        {
            IsActive = true;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
            UserVisibility = true;
        }


        public User(string email, string password)
        {
            this.Email = email;
            this.Password = password;
            IsActive = true;
            Created = DateTime.Now;
            Modified = DateTime.Now;
            UserVisibility = true;
        }


    }


}
