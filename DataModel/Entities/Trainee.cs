using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataModel.Entities;
using Newtonsoft.Json;
using TraineeHelper.Common;

namespace TraineeHelper.Models
{
   
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Trainee))]
    public class Trainee : User, ITrainee
    {
        [BsonIgnoreIfNull]
        public List<Challenge> Challenges { get; set; }
        [BsonIgnoreIfNull]
        public Exercise FavouriteExercise { get; set; }
        [BsonIgnoreIfNull]
        public MedicalCondition MedicalCondition { get; set; }
        public Gender Gender { get; set; }
    }


}
