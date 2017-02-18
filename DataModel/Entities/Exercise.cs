using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using DataModel.Entities;
using Newtonsoft.Json;
using TraineeHelper.Common;

namespace TraineeHelper.Models
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = (true))]
    [BsonKnownTypes(typeof(Exercise))]
    public class Exercise : IExercise
    {
        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public int ExerciseId { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string ExerciseName { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string ExerciseType { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public int SetsNum { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public int Repetitions { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Note { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string MuscleName { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public ExerciseStatus Status { get; set; }


        public Exercise()
        {
            this.Description = "";
            this.ExerciseId = 0;
            this.ExerciseName = "";
            this.MuscleName = "";
            this.Note = "";
            this.SetsNum = 0;
            this.Status = 0;
            this.Repetitions = 0;
            this.ExerciseType = "";
        }
    }

}
