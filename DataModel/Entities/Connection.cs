using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using TraineeHelper.Common;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace TraineeHelper.Models.Entities
{

   [Serializable, JsonObject]
   [BsonDiscriminator(Required = true)]

    public class Connection : IConnection
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        [BsonIgnoreIfNull]
        public string Contact { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public ConnectionType ConnectionType { get; set; }

        [BsonIgnoreIfDefault]
        public bool IsActive { get; set;}

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public string Description { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Modified { get; set; }     

        public Connection()
        {
            Created = DateTime.Now;
        }

        public Connection(string sender, string reciever, ConnectionType connectionType)
        {
            Sender = sender;
            Reciever = reciever;
            ConnectionType = connectionType;
            Created = DateTime.Now;
            Modified = DateTime.Now;
            ConnectionStatus = ConnectionStatus.REQUEST;
        }


    }
}
