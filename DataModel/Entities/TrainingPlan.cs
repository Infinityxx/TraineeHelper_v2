using DataModel.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.Models
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(TrainingPlan))]
    public class TrainingPlan : ITrainingPlan
    {
        
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonIgnoreIfDefault]
        public string TrainerId { get; set; }

        [BsonIgnoreIfDefault]
        public string TraineeId { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string PlanName { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CompletionDate { get; set; }

        [BsonIgnoreIfDefault]
        public List<Exercise> Exercises { get; set; }

        [BsonIgnoreIfDefault]
        public bool IsActive { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Modified { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string TrainerName { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string TraineeName { get; set; }



        public TrainingPlanStatus TrainingPlanStatus { get; set; }
    }
}
