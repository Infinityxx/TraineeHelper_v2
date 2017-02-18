using System;
using DataModelNew.Entitiess;
using Newtonsoft.Json;


namespace DataModelNew.Entities
{
    [Serializable, JsonObject]
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(Exercise))]
    public class Exercise : IExercise
    {
        [BsonIgnoreIfDefault]
        public int ExerciseId { get; set; }

        [BsonIgnoreIfDefault]
        public int PlanId { get; set; }

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
        public int SetsNum { get; set; }

        [BsonDefaultValue("")]
        public int Repetitions { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string Note { get; set; }

        [BsonDefaultValue("")]
        [BsonIgnoreIfDefault]
        public string MuscleName { get; set; }

        [BsonDefaultValue(null)]
        public ExerciseStatus Status { get; set; }

        
        public enum ExerciseStatus
        {
            Unstarted = 0,
            InProgress = 1,
            Completed = 2
        }
    }
}
