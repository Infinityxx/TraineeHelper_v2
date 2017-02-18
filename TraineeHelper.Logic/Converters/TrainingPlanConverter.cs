using DataModel.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class TrainingPlanConverter
    {
        public static List<TrainingPlanContext> ConvertToTrainingPlansContexts(this IEnumerable<ITrainingPlan> trainingplans)
        {
            List<TrainingPlanContext> trainingPlansContext = new List<TrainingPlanContext>();
            if (trainingplans == null)
                return trainingPlansContext;
            foreach (TrainingPlan t in trainingplans)
            {
                trainingPlansContext.Add(t.ConvertToTrainingPlanContext());
            }

            return trainingPlansContext;
        }

        public static List<TrainingPlan> ConvertToTrainingPlans(this IEnumerable<TrainingPlanContext> trainingPlansContext)
        {
            List<TrainingPlan> trainingPlans = new List<TrainingPlan>();
            if (trainingPlansContext == null)
                return trainingPlans;
            foreach (TrainingPlanContext t in trainingPlansContext)
            {
                trainingPlans.Add(t.ConvertToTrainingPlan());
            }

            return trainingPlans;
        }

        public static TrainingPlan ToNewTrainingPlan(this TrainingPlanContext context)
        {
            TrainingPlan trainingPlan = new TrainingPlan();
            return trainingPlan;
        }

        public static TrainingPlanContext ConvertToTrainingPlanContext(this TrainingPlan trainingPlan)
        {
            TrainingPlanContext trainingPlanContext = new TrainingPlanContext();
            if (null == trainingPlan)
                return trainingPlanContext;
            trainingPlanContext.CompletionDate = trainingPlan.CompletionDate;
            trainingPlanContext.Created = trainingPlan.Created;
            trainingPlanContext.Description = trainingPlan.Description;
            trainingPlanContext.EndDate = trainingPlan.EndDate;
            trainingPlanContext.Id = trainingPlan.Id.ToString();
            trainingPlanContext.TrainerName = trainingPlan.TrainerName;
            trainingPlanContext.TraineeName = trainingPlan.TraineeName;
            trainingPlanContext.TrainerId = trainingPlan.TrainerId;
            trainingPlanContext.StartDate = trainingPlan.StartDate;
            trainingPlanContext.PlanName = trainingPlan.PlanName;
            trainingPlanContext.TraineeId = trainingPlan.TraineeId;
            trainingPlanContext.Exercises = trainingPlan.Exercises.ConvertToExerciseContexts();
            trainingPlanContext.TrainingPlanStatus = trainingPlan.TrainingPlanStatus;

            return trainingPlanContext;
        }

        public static TrainingPlan ConvertToTrainingPlan(this TrainingPlanContext context, bool generateId = false)
        {
            TrainingPlan trainingPlan = new TrainingPlan();
            if (null == context)
                return trainingPlan;
            trainingPlan.CompletionDate = context.CompletionDate;
            trainingPlan.Created = context.Created;
            trainingPlan.Description = context.Description;
            trainingPlan.EndDate = context.EndDate;
            trainingPlan.Id = generateId ? ObjectId.GenerateNewId() : ObjectId.Parse(context.Id);
            trainingPlan.TrainerId = context.TrainerId;
            trainingPlan.StartDate = context.StartDate;
            trainingPlan.PlanName = context.PlanName;
            trainingPlan.TraineeId = context.TraineeId;
            trainingPlan.Exercises = context.Exercises.ToExercises();
            trainingPlan.TrainingPlanStatus = context.TrainingPlanStatus;
            
            return trainingPlan;
        }
    }
}
