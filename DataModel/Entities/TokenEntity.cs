using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace DataModel.DataModel
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(TokenEntity))]
    public class TokenEntity : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public int TokenId { get; set; }
        public string UserId { get; set; }
        public string AuthToken { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime IssuedOn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ExpiresOn { get; set; }
    }
}
