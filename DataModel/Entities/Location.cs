using DataModel.Entities;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Location))]
    public class Location : ILocation
    {

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string City { get; set; }

        //[BsonDefaultValue("")]
        //[BsonIgnoreIfDefault]


        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        //[BsonIgnoreIfDefault]
        //public double CoordX { get; set; }

        //[BsonIgnoreIfDefault]
        //public double CoordY { get; set; }
    }
}
