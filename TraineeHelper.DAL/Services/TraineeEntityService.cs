//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using TraineeHelper.Models;
//using DataModel.Entities;

//namespace TraineeHelper.DAL.Services
//{
//    public class TraineeEntityService : IEntityService<Trainee>
//    {
//        protected readonly MongoConnectionHandler<ITrainee> Trainees;
//        protected readonly MongoConnectionHandler<IUser> Users;
//        protected readonly EntityService<ITrainee, Trainee> entityServices = new EntityService<ITrainee, Trainee>();
//        protected readonly EntityService<IUser, User> usersServices = new EntityService<IUser, User>();
        
//        public TraineeEntityService()
//        {
//            Trainees = new MongoConnectionHandler<ITrainee>();
//            Users = new MongoConnectionHandler<IUser>();
//        }

        
//        //public async Task<IEnumerable<Trainee>> GetAllAsync()
//        //{
//        //    var task = (Task)((dynamic)entityServices.GetAllAsync(Trainees.MongoCollection));
//        //
//        //    await task;
//        //    var trainees = (IEnumerable<Trainee>)((dynamic)task).Result;
//        //    return trainees;
//        //}

//        public async Task<IEnumerable<Trainee>> GetAllAsync()
//        {
//            var builder = Builders<IUser>.Filter;
//            var filter = builder.Eq("_t", "Trainee");

//            var task =  (Task)((dynamic)Users.MongoCollection.Find(filter).ToListAsync());

//            await task;
//            var trainees = (IEnumerable<Trainee>)((dynamic)task).Result;
//            return trainees;
//        }

//        public Task<Trainee> GetOneAsync(Trainee context)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainee> GetOneAsync(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainee> GetManyAsync(IEnumerable<Trainee> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainee> GetManyAsync(IEnumerable<string> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Trainee> SaveManyAsync(IEnumerable<Trainee> contexts)
//        {
//            throw new NotImplementedException();
//        }
//        public Task<bool> RemoveOneAsync(Trainee context)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveManyAsync(IEnumerable<Trainee> contexts)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> RemoveManyAsync(IEnumerable<string> ids)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<Trainee> SaveOneAsync(Trainee trainee)
//        {
//            if (null != trainee)
//            {
//                //var trainee = context.AsNewTrainee();
//                await Trainees.MongoCollection.InsertOneAsync(trainee);
//                User traineeUser = new Trainee();
//                //traineeUser.Email = "111111";
//                //MongoConnectionHandler<IUser> Users = new MongoConnectionHandler<IUser>();
//                //await Users.MongoCollection.InsertOneAsync(traineeUser);
//                //context.Id = trainee.Id.ToString();
//                return trainee;
//            }
//            return trainee;
//            //var update = context.ToTrainee();
//            //await Trainees.MongoCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(context.Id)),update);
   
//            //return context;
//        }

//        public async Task<Trainee> Update(Trainee trainee)
//        {
//            var result = await Trainees.MongoCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(trainee.Id.ToString())), trainee);
//            if (result.IsAcknowledged)
//                return trainee;
//            return trainee;
//        }

//        public async Task<bool> Delete(string id)
//        {
//            var trainee = await entityServices.Delete(Trainees.MongoCollection, id);
//            return trainee;
//        }

//        public async Task<Trainee> GetById(string id)
//        {
//            //var trainee = await entityServices.GetOneAsync(Trainees.MongoCollection, id);
//            //return trainee.ToTraineeContext();
//            //var builder = Builders<IUser>.Filter;
//            //var filter = builder.Eq("_t", "Trainee");
//            var task = (Task)((dynamic)usersServices.GetOneAsync(Users.MongoCollection, id));
//            await task;
//            //var user = await entityServices.GetOneAsync(Users.MongoCollection, id);
//            var trainee = (Trainee)((dynamic)task).Result;
//            return trainee;
//            //return user.ToUserContext();  
//        }

        
//    }
//}
