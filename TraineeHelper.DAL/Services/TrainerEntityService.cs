//using MongoDB.Bson;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TraineeHelper.Models;

//namespace TraineeHelper.DAL.Services
//{
//    public class TrainerEntityService : IEntityService<Trainer>
//    {
//        protected readonly MongoConnectionHandler<ITrainer> Trainers;
//        protected readonly EntityService<ITrainer, Trainer> entityServices = new EntityService<ITrainer, Trainer>();

//        public TrainerEntityService()
//        {
//            Trainers = new MongoConnectionHandler<ITrainer>();
//        }

//        public async Task<IEnumerable<Trainer>> GetAllAsync()
//        {
//            var task = (Task)((dynamic)entityServices.GetAllAsync(Trainers.MongoCollection));
//            await task;
//            var trainers = (IEnumerable<Trainer>)((dynamic)task).Result;
//            return trainers;
//        }

//        public Task<Trainer> GetOneAsync(Trainer context)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainer> GetOneAsync(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainer> GetManyAsync(IEnumerable<Trainer> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainer> GetManyAsync(IEnumerable<string> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainer> SaveManyAsync(IEnumerable<Trainer> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveOneAsync(Trainer context)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveManyAsync(IEnumerable<Trainer> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveManyAsync(IEnumerable<string> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<Trainer> SaveOneAsync(Trainer trainer)
//        {
//           if(null != trainer)
//            {
//                await Trainers.MongoCollection.InsertOneAsync(trainer);
//                return trainer;
//            }
//            return trainer;
//        }

//        public async Task<bool> Update(Trainer trainer)
//        {
//            var result = await Trainers.MongoCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(trainer.Id.ToString())), trainer);
//            if (result.IsAcknowledged)
//                return true;
//            return false;
//        }

//        public async Task<bool> Delete(string id)
//        {
//            var result = await entityServices.Delete(Trainers.MongoCollection, id);
//            return result;
//        }

//        public async Task<Trainer> GetById(string id)
//        {
//            var task = (Task)((dynamic)entityServices.GetOneAsync(Trainers.MongoCollection, id));
//            await task;
//            //var user = await entityServices.GetOneAsync(Users.MongoCollection, id);
//            var trainers = (Trainer)((dynamic)task).Result;
//            return trainers;
//        }
//    }
//}
