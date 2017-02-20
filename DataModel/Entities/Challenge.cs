using DataModel.Entities;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using TraineeHelper.Common;

namespace TraineeHelper.Models
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Challenge))]
    public class Challenge : IChallenge
    {
        public DateTime Created{ get; set; }

        public string Description { get; set; }
       
        [BsonId]
        public ObjectId Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime Modified { get; set; }

        public string TraineeId { get; set; }

        public string TrainerId { get; set; }

        public string TrainerName { get; set; }

        public string TraineeName { get; set; }

        public string ChallengeType { get; set; }

        public string ChallengeValue { get; set; }

        public string ChallengeTime { get; set; }

        public bool IsCompleted { get; set; }

        public ChallengeStatus ChallengeStatus { get; set; }
    }
}
