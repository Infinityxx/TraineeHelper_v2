using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;
using TraineeHelper.Models;
using TraineeHelper.Models.Entities;

namespace TraineeHelper.Models
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Trainer))]
    public class Trainer : User, ITrainer
    {
        
        public int Reputation { get; set; }
        public double Price { get; set; }
        
        [BsonIgnoreIfNull]
        public string[] Expertise { get; set; }
        public int ExperienceYears { get; set; }

        [BsonIgnoreIfNull]
        public Gender Gender { get; set; }
    }
}
