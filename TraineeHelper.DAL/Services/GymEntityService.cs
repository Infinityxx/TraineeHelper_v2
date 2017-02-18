//using MongoDB.Bson;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TraineeHelper.Models.Entities;

//namespace TraineeHelper.DAL.Services
//{
//    public class GymEntityService : IEntityService<Gym>
//    {
//        protected readonly MongoConnectionHandler<IGym> Gyms;
//        protected readonly EntityService<IGym, Gym> entityServices = new EntityService<IGym, Gym>();

//        public GymEntityService()
//        {
//            Gyms = new MongoConnectionHandler<IGym>();
//        }

//        public async Task<IEnumerable<Gym>> GetAllAsync()
//        {
//            var task = (Task)((dynamic)entityServices.GetAllAsync(Gyms.MongoCollection));
//            await task;
//            var gyms = (IEnumerable<Gym>)((dynamic)task).Result;
//            return gyms;
//        }

//        public Task<Gym> GetOneAsync(Gym context)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Gym> GetOneAsync(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Gym> GetManyAsync(IEnumerable<Gym> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Gym> GetManyAsync(IEnumerable<string> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Gym> SaveManyAsync(IEnumerable<Gym> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveOneAsync(Gym context)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveManyAsync(IEnumerable<Gym> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveManyAsync(IEnumerable<string> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<Gym> SaveOneAsync(Gym gym)
//        {
//            if (null != gym)
//            {
//               await Gyms.MongoCollection.InsertOneAsync(gym);
//               return gym;
//            }
//            return gym;
//         }

//        public async Task<Gym> GetById(string id)
//        {
//            var task = (Task)((dynamic)entityServices.GetOneAsync(Gyms.MongoCollection, id));
//            await task;
//            //var user = await entityServices.GetOneAsync(Users.MongoCollection, id);
//            var gym = (Gym)((dynamic)task).Result;
//            return gym;
//            //return user.ToUserContext();  
//        }

//        public async Task<Gym> Update(Gym gym)
//        {
//            var result = await Gyms.MongoCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(gym.Id.ToString())), gym);
//            if (result.IsAcknowledged)
//                return gym;
//            return gym;
//        }

//        public async Task<bool> Delete(string id)
//        {
//            var gym = await entityServices.Delete(Gyms.MongoCollection, id);
//            return gym;
//        }
//    }
//}
