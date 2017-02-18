using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class TrainingPlanManager
    {
        private TrainingPlanEntityService TrainingPlanEntityService;

        public TrainingPlanManager()
        {
            TrainingPlanEntityService = new TrainingPlanEntityService();
        }

        public async Task<bool> CreateTrainingPlan(TrainingPlanContext trainingPlanctx)
        {
            if (null == trainingPlanctx)
                return false;
            TrainingPlan trainingPlan = trainingPlanctx.ConvertToTrainingPlan(true);
            var result = await TrainingPlanEntityService.CreateTrainingPlan(trainingPlan);
            return result;
        }

        public async Task<TrainingPlanContext> UpdateTrainingPlan(TrainingPlanContext trainingPlanctx)
        {
            if (null == trainingPlanctx)
                return null;
            TrainingPlan trainingPlan = trainingPlanctx.ConvertToTrainingPlan(false);
            var result = await TrainingPlanEntityService.Update(trainingPlan);
            return result.ConvertToTrainingPlanContext();
        }

        public async Task<TrainingPlan> UpdateTrainingPlan(TrainingPlan trainingPlan)
        {
            var result = await TrainingPlanEntityService.Update(trainingPlan);
            return result;
        }

        //TODO: Create Exercises/update exercises methods.

        public async Task<bool> DeleteTrainingPlan(TrainingPlanContext trainingPlanctx)
        {
            if (null == trainingPlanctx)
                return false;
            TrainingPlan trainingPlan = trainingPlanctx.ConvertToTrainingPlan(false);
            var result = await TrainingPlanEntityService.Delete(trainingPlan.Id.ToString());
            return result;
        }

        public async Task<TrainingPlanContext> FindTrainingPlanById(string TrainingPlanId)
        {
            if (TrainingPlanId == null)
                return null;

            var result = await TrainingPlanEntityService.GetById(TrainingPlanId);
            return result.ConvertToTrainingPlanContext();
        }

        public async Task<bool> UpdateExercise(string trainingplanId, Exercise exercise)
        {
            if (exercise == null || trainingplanId == null)
                return false;

            var result = await TrainingPlanEntityService.GetById(trainingplanId);
            if(result != null)
            {
                var exercisefromtp = result.Exercises.Find(e => e.ExerciseId == exercise.ExerciseId);
                result.Exercises.Remove(exercisefromtp);
                result.Exercises.Add(exercise);
                var updateResult = await TrainingPlanEntityService.Update(result);
                if (updateResult != null)
                    return true;
                else
                    return false;
            }

            return false;
                
        }

        public async Task<IEnumerable<TrainingPlanContext>> FindUserTrainingPlans(string userId)
        {
            if (userId == null)
                return null;
            UserManager userManager = new UserManager();
            var result = await TrainingPlanEntityService.GetByUserId(userId);
            foreach (TrainingPlan tp in result)
            {
                tp.TrainerName = await userManager.GetUserNameById(tp.TrainerId);
                tp.TraineeName = await userManager.GetUserNameById(tp.TraineeId);
            }

            return result.ConvertToTrainingPlansContexts();
        }


        public async Task<bool> AddExerciseToTrainingPlan(string TrainingPlanId, ExerciseContext exerciseCtx)
        {
            if (null == exerciseCtx || null == TrainingPlanId)
                return false;
            
            Exercise exercise = exerciseCtx.ConvertToExercise(); //TODO?? make ID for exercise?
            var trainingPlan = await TrainingPlanEntityService.GetById(TrainingPlanId);
            trainingPlan.Exercises.Add(exercise);
            var result = await UpdateTrainingPlan(trainingPlan);
            if (null != result)
                return true;
            else
                return false;
        }

        public async Task<bool> RemoveExerciseFromTrainingPlan(string TrainingPlanId, ExerciseContext exerciseCtx)
        {
            if (null == exerciseCtx || null == TrainingPlanId)
                return false;

            var trainingPlan = await TrainingPlanEntityService.GetById(TrainingPlanId);
            Exercise exercise = exerciseCtx.ConvertToExercise();
            trainingPlan.Exercises.Remove(exercise);
            var result = await UpdateTrainingPlan(trainingPlan);
            if (null != result)
                return true;
            else
                return false;
        }

    }
}
