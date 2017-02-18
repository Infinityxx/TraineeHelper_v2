using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;

namespace TraineeHelper.Models
{
    public interface IMongoCommon : IMongoEntity
    {
        bool IsActive { get; set; }
        string Description { get; set; }
        DateTime Created { get; set; }
        [BsonIgnoreIfNull]
        DateTime Modified { get; set; }
    }
}
