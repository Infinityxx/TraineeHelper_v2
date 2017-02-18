using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TraineeHelper.Models;
using DataModel.Entities;
using MongoDB.Bson;

namespace TraineeHelper.DAL.Services
{
    public class TrainingPlanEntityService /*: EntityService<ITrainingPlan>*/
    {
        protected readonly MongoConnectionHandler<ITrainingPlan> TrainingPlans;
        protected readonly EntityService<ITrainingPlan, TrainingPlan> entityServices;

        public TrainingPlanEntityService()
        {
            TrainingPlans = new MongoConnectionHandler<ITrainingPlan>();
            entityServices = new EntityService<ITrainingPlan, TrainingPlan>();
        }

        public async Task<bool> CreateTrainingPlan(TrainingPlan trainingPlan)
        {
            trainingPlan.Created = DateTime.Now;
            var result = TrainingPlans.MongoCollection.InsertOneAsync(trainingPlan);
            await result;
            if (result.IsCompleted)
                return true;
            return false;
        }

        public async Task<TrainingPlan> Update(TrainingPlan trainingPlan)
        {
            trainingPlan.Modified = DateTime.Now;
            await TrainingPlans.MongoCollection.ReplaceOneAsync(
                new BsonDocument("_id",
                new ObjectId(trainingPlan.Id.ToString())), trainingPlan);
            
            return trainingPlan;


        }

        public async Task<bool> Delete(string id)
        {
            var result = await entityServices.Delete(TrainingPlans.MongoCollection, id);
            return result;
        }
        
        public async Task<IEnumerable<ITrainingPlan>> GetAllTrainingPlansRelatedToUser(string value ,string id)
        {
            var builder = Builders<ITrainingPlan>.Filter;
            var filter = builder.Eq(value, id);
            var result = await TrainingPlans.MongoCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<TrainingPlan> GetById(string id)
        {
            var task = (Task)((dynamic)entityServices.GetOneAsync(TrainingPlans.MongoCollection, id));
            await task;
            var trainingPlan = (TrainingPlan)((dynamic)task).Result;
            return trainingPlan;
        }

        public async Task<IEnumerable<ITrainingPlan>> GetByUserId(string id)
        {
            var task = (Task)((dynamic)entityServices.GetManyByColumnAsync<string>(TrainingPlans.MongoCollection, "TraineeId", id));
            await task;

            var trainingPlans = ((dynamic)task).Result;
            return trainingPlans;
        }
    }
}
