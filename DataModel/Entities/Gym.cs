using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace TraineeHelper.Models.Entities
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Gym))]
    public class Gym : User, IGym
    {
        public int Reputation { get; set; }
        public double Price { get; set; }
        public bool ParkingLot { get; set; }
        public bool Showers { get; set; }
        [BsonIgnoreIfNull]
        public string[] Expertise { get; set; }
        public int ExperienceYears { get; set; }
    }
}
