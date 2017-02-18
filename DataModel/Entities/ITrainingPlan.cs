using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;
using TraineeHelper.Models;

namespace DataModel.Entities
{
    public interface ITrainingPlan : IMongoCommon
    {
        string TrainerId { get; set; }
        string TraineeId { get; set; }
        string PlanName { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime CompletionDate { get; set; }
        List<Exercise> Exercises { get; set; }
        TrainingPlanStatus TrainingPlanStatus { get; set; }
    }
}
