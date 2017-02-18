using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataModel.Entities;
using Newtonsoft.Json;

namespace TraineeHelper.Models
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(MedicalCondition))]  
    public class MedicalCondition : IMedicalCondition
    {
        //[BsonId]
        //public string _id { get; set; }
        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public double MuscleMass { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public double FatPercent { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public double Weight { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public double Height { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public int Age { get; set; }
    }
}
