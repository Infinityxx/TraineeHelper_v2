using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models.Entities
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Expertise))]
    public class Expertise : IExpertise
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string ExpertiseName { get; set; }

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
        [BsonIgnoreIfNull]
        public string Description { get; set; }
    }
}
