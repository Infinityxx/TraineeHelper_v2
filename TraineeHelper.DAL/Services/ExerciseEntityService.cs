using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TraineeHelper.Models;
using DataModel.Entities;

namespace TraineeHelper.DAL.Services
{
    public class ExerciseEntityService //: IEntityService<ExerciseContext>
    {
        //protected readonly MongoConnectionHandler<IExercise> Exercises;
        //protected readonly EntityService<IExercise, Exercise> entityServices = new EntityService<IExercise, Exercise>();

/*
        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExerciseContext>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> GetManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> GetManyAsync(IEnumerable<ExerciseContext> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> GetOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> GetOneAsync(ExerciseContext context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveManyAsync(IEnumerable<ExerciseContext> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveOneAsync(ExerciseContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> SaveManyAsync(IEnumerable<ExerciseContext> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<ExerciseContext> SaveOneAsync(ExerciseContext entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ExerciseContext enitity)
        {
            throw new NotImplementedException();
        }

       /* public override  void Update(ExerciseContex exercise)
        {
     
            var filter = Builders<ExerciseContex>.Filter.Eq(s => s.ExerciseId, exercise.ExerciseId);
            var update = Builders<ExerciseContex>.Update
                .Set("Description", exercise.Description)
                .Set("ExerciseName", exercise.ExerciseName)
                .Set("PlanId", exercise.PlanId)
                .Set("SetsNum", exercise.SetsNum)
                .Set("Repetitions", exercise.Repetitions)
                .Set("Note", exercise.Note)
                .Set("MuscleName", exercise.MuscleName)
                .Set("Status", exercise.Status)
                .CurrentDate("lastModified");

            var result = MongoConnectionHandler.MongoCollection.UpdateOneAsync(filter, update);
        }
        */
    }
}
