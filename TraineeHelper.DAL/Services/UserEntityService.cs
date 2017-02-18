using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TraineeHelper.Models;
using DataModel.Entities;
using MongoDB.Bson;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.DAL.Services
{
    public class UserEntityService<T> : IEntityService<T> where T : User
    {
        protected readonly MongoConnectionHandler<IUser> Users;
        protected readonly EntityService<IUser, User> entityServices;

        public UserEntityService()
        {
            Users = new MongoConnectionHandler<IUser>();
            entityServices = new EntityService<IUser, User>();
        }

        public string Authenticate(string email, string password)
        {
            var builder = Builders<IUser>.Filter;
            var filter = builder.Eq("Email", email) & builder.Eq("Password", password);
            var user = Users.MongoCollection.Find(filter);
            if (user.Count() > 0 && user.First().Id.ToString() != null)
            {
                return user.First().Id.ToString();
            }
            return null;
        }
        /*
        public string Authenticate(string userName, string password)
        {
            //var user = Users.MongoCollection.FindAsync(u => u.UserName == userName &&
           //                                                                  u.Password == password);
            var builder = Builders<IUser>.Filter;
            var filter = builder.Eq("UserName", userName) & builder.Eq("Password", password);
            var user = Users.MongoCollection.Find(filter);
            if (user.Count() > 0 && user.First().Id.ToString() != null)
            {
                return user.First().Id.ToString();
            }
            return null;
            //var result = MongoConnectionHandler.MongoCollection.Find(_ => _.Id == ObjectId.Parse(id)).FirstOrDefault();
        }
        */
        public string AuthenticateByEmail(string email, string password)
        {
            var builder = Builders<IUser>.Filter;
            var filter = builder.Eq("Email", email) & builder.Eq("Password", password);
            var user = Users.MongoCollection.Find(filter);
            if (user.Count() > 0 && user.First().Id.ToString() != null)
            {
                return user.First().Id.ToString();
            }
            return null;
        }

        public async Task<T> SaveOneAsync(T user)
        {
            if (null != user)
            {
                await Users.MongoCollection.InsertOneAsync(user);
                return user;
            }

            return user;
        }



        /*
        Task<bool> IEntityService<User>.Update(User entity)
        {
            throw new NotImplementedException();
        }
        */
        /*public async Task<T> Update(T user)
        {
            var filter = Builders<T>.Filter.Eq("_id", user.Id.ToString());

            var result = await Users.MongoCollection.UpdateOneAsync(filter, update);
        }
        */
        public async Task<T> Update(T user)
        {
            user.Modified = DateTime.Now;
            await Users.MongoCollection.ReplaceOneAsync(
                new BsonDocument("_id",
                new ObjectId(user.Id.ToString())), user);
            return user;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await entityServices.Delete(Users.MongoCollection, id);
            return user;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var task = (Task)((dynamic)entityServices.GetAllAsync(Users.MongoCollection));
            await task;
            var users = (IEnumerable<T>)((dynamic)task).Result;
            return users;
        }

        public async Task<T> GetById(string id)
        {
            var task = (Task)((dynamic)entityServices.GetOneAsync(Users.MongoCollection, id));
            await task;
            //var user = await entityServices.GetOneAsync(Users.MongoCollection, id);
            var user = (T)((dynamic)task).Result;
            return user;
            //return user.ToUserContext();  
        }

        public async Task<IUser> GetByUserName(string userName)
        {
            if (null != userName)
            {
                var builder = Builders<IUser>.Filter;
                var filter = builder.Eq("UserName", userName);
                var user = await Users.MongoCollection.FindAsync(filter);

                return user.FirstOrDefault();
            }

            return null;
        }
        /// <summary>
        /// returns the User Id that matches the email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> GetByEmailEntityIdAsync(string email)
        {
            if (null != email)
            {
                var builder = Builders<IUser>.Filter;
                var filter = builder.Eq("Email", email);
                var user = await Users.MongoCollection.FindAsync(filter);
                return user.FirstOrDefault().Id.ToString();
            }
            return null;
        }

        public async Task<string> GetByColumnAsync<K>(string column, K value)
        {


            if (null != column)
            {
                var builder = Builders<IUser>.Filter;
                var filter = builder.Eq(column, value);
                var user = await Users.MongoCollection.FindAsync(filter);
                return user.FirstOrDefault().Id.ToString();
            }
            return null;
        }

        public async Task<IEnumerable<User>> GetManyByMultipleFilters(List<SearchFilter> filters)
        {
            var builder = Builders<IUser>.Filter;

            //var filter = builder.Regex(filters[0].Item1, "/" + filters[0].Item2 + "/i");
            if (null == filters) return null;
            
            string currentUserId;
            FilterDefinition<IUser> filter = null;
            if (filters[0] is SearchFilter)
            {
                switch (filters[0].FieldName)
                {
                    case "_id":
                        filter = builder.Ne("_id", ((ValueSearchFilter<string>)filters[0]).Value);
                        break;
                    case "UserType":
                        filter = builder.Eq(filters[0].FieldName, ((ValueSearchFilter<string>)filters[0]).Value);
                        break;
                    case "Gender":
                        if(((ValueSearchFilter<int>)filters[0]).Value != 2)
                            filter = builder.Eq(filters[0].FieldName, ((ValueSearchFilter<int>)filters[0]).Value);
                        break;
                }
            }
            else
            {
                filter = builder.Lte(filters[0].FieldName, ((RangeSearchFilter)filters[0]).ToValue) &
                    builder.Gt(filters[0].FieldName, ((RangeSearchFilter)filters[0]).FromValue);
            }  
            for (int i = 1; i < filters.Count; i++)
            {
                filter = ConcatenateFilter(filters, builder, filter, i);
                /*if (filters[i] is RangeSearchFilter)
                {
                    // mongo.from to....
                    filter = filter & builder.Lte(filters[i].FieldName, ((RangeSearchFilter)filters[i]).ToValue) &
                    builder.Gt(filters[i].FieldName, ((RangeSearchFilter)filters[i]).FromValue);
                }
                else
                {
                    //ValueSearchFilter valueFilter = (ValueSearchFilter)filters[i];
                    if (filters[i].FieldName == "_id")
                    {
                        currentUserId = ((ValueSearchFilter<string>)filters[i]).Value;

                        filter = filter & (builder.Ne("_id", currentUserId));
                    }
                    else
                    {
                        filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<T>)filters[i]).Value);
                    }                          
                }
                */
            }
           
            var list = await Users.MongoCollection.Find(filter).ToListAsync();
            if (list.Count > 0)
            {
                return list.Select(user => (User)user);
            }
            return null;
        }

        public FilterDefinition<IUser> ConcatenateFilter(List<SearchFilter> filters, FilterDefinitionBuilder<IUser> builder, FilterDefinition<IUser> filter, int i)
        {
            if (filters[i] is SearchFilter)
            {
                switch (filters[i].FieldName)
                {
                    case "_id":
                        filter = filter & builder.Ne("_id", ((ValueSearchFilter<string>)filters[i]).Value);
                        break;
                    case "UserType":
                        filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<string>)filters[i]).Value);
                        break;
                    case "Gender":
                        if (((ValueSearchFilter<int>)filters[i]).Value != 2)
                            filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<int>)filters[i]).Value);
                        break;
                    case "Location":
                        break;
                    case "Expertise":
                        //ExpertiseEntityService expertiseService = new ExpertiseEntityService();
                        //IExpertise expertiseVal = await expertiseService.GetExpertiseById(((ValueSearchFilter<string>)filters[i]).Value);
                        //expertiseVal.ExpertiseName
                        filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<string>)filters[i]).Value);
                        break;
                    case "Reputation":
                        filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<int>)filters[i]).Value);
                        break;
                    case "Showers":
                        filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<bool>)filters[i]).Value);
                        break;
                    case "ParkingLot":
                        filter = filter & builder.Eq(filters[i].FieldName, ((ValueSearchFilter<bool>)filters[i]).Value);
                        break;
                }
            }
            else
            {
                filter = filter & builder.Lte(filters[i].FieldName, ((RangeSearchFilter)filters[i]).ToValue) &
                    builder.Gt(filters[i].FieldName, ((RangeSearchFilter)filters[i]).FromValue);
            }

            return filter;
        }

        public async Task<IEnumerable<User>> GetManyByUserName(string userName, string currentUserId)
        {
            if (null != userName)
            {
                var builder = Builders<IUser>.Filter;
                var filter = builder.Regex("UserName", "/" + userName + "/i") & builder.Ne("_id", ObjectId.Parse(currentUserId)); // ignore case
                var list = await Users.MongoCollection.Find(filter).ToListAsync();
                if (list.Count > 0)
                {
                    return list.Select(user => (User)user);
                }
                return null;
            }
            return null;
        }

        public async Task<bool> CheckUserNameExistence(string userName)
        {
            if (userName != null)
            {
                var exists = await entityServices.CheckValueExistence(Users.MongoCollection, "UserName", userName);
                return exists;
            }
            return false;
        }

        public async Task<bool> CheckEmailExistence(string email)
        {
            if (email != null)
            {
                var exists = await entityServices.CheckValueExistence(Users.MongoCollection, "Email", email);
                return exists;
            }
            return false;
        }

        public Task<T> GetOneAsync(T context)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetManyAsync(IEnumerable<T> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<T> SaveManyAsync(IEnumerable<T> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveOneAsync(T context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveManyAsync(IEnumerable<T> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<T>> IEntityService<T>.GetAllAsync()
        {
            throw new NotImplementedException();
        }



        Task<T> IEntityService<T>.GetOneAsync(string id)
        {
            throw new NotImplementedException();
        }



        Task<T> IEntityService<T>.GetManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }




        Task<T> IEntityService<T>.GetById(string id)
        {
            throw new NotImplementedException();
        }

        //Task<bool> IEntityService<User>.Update(User entity)
        //{
        //    throw new NotImplementedException();
        //}



        //public void UploadImage(HttpPostedFile file)
        //{
        //    GridFSBucket fs = new GridFSBucket(MongoConnectionHandler._database);
        //    UploadFile(fs);      
        //}

        //private static ObjectId UploadFile(GridFSBucket fs)
        //{
        //    using (var s = File.OpenRead(@"c:\temp\test.txt"))
        //    {
        //        var t = Task.Run<ObjectId>(() =>
        //        {
        //            return fs.UploadFromStream("test.txt", s);
        //        });
        //        return t.Result;
        //    }
        //}
    }
}
