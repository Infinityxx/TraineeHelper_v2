using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using TraineeHelper.Models;
using System.Threading.Tasks;
using DataModel.Entities;

namespace TraineeHelper.DAL.Services
{
    public class EntityService<TCollection, TContext>// : IEntityService<T> 
        where TCollection : IMongoCommon
        where TContext : IMongoCommon, new()
    {
//        public readonly MongoConnectionHandler<T> MongoConnectionHandler;

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="entity"></param>
            
        //public async Task Create(T entity)
        //{
        //    await MongoConnectionHandler.MongoCollection.InsertOneAsync(entity);
        //}
        /// <summary>
        /// Delete an entity by id
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> Delete(IMongoCollection<TCollection> collection, string id)
        {
           
            if (id == null || id == "") return false;
            var filter = new BsonDocument("_id", ObjectId.Parse(id));
            await collection.DeleteOneAsync(filter);
            //await collection.DeleteOneAsync(a => a.Id == ObjectId.Parse(id));
            

            return true;  
        }

        /// <summary>
        /// Remove one context
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> RemoveOneAsync(IMongoCollection<TCollection> collection, TContext context)
        {
            if (context == null) return false;

            await collection.UpdateOneAsync(
              new BsonDocument("_id", context.Id),
              new BsonDocument("$set", new BsonDocument
              {
                  {nameof(IMongoCommon.IsActive), false },
                  {nameof(IMongoCommon.Modified), DateTime.UtcNow }

              }));
            return true;
        }

        /// <summary>
        /// fetch many by context
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="contexts"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TCollection>> GetManyAsync(IMongoCollection<TCollection> collection,
                                                           IEnumerable<TContext> contexts)
        {
            var list = new List<TCollection>();
            foreach (var context in contexts)
            {
                var doc = await GetOneAsync(collection, context);
                if (doc == null) continue;
                list.Add(doc);
            }

            return list;
        }

        public async Task<bool> CheckValueExistence<K>(IMongoCollection<TCollection> collection, string column, K value)
        {
            if(null != column)
            {
                var builder = Builders<TCollection>.Filter;
                var filter = builder.Eq(column, value);
                var exists =  await collection.Find(filter).AnyAsync();
                return exists;
            }
            return false;
        }


        /// <summary>
        /// fetch many by context
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="contexts"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TCollection>> GetManyByColumnAsync<K>(IMongoCollection<TCollection> collection, string column, K value)
        {
            if (null != column)
            {
                var builder = Builders<TCollection>.Filter;
                var filter = builder.Eq(column, value);
                var result = await collection.FindAsync(filter);
                return result.ToList();
            }
            return null;
        }

        //public async Task<IEnumerable<TCollection>> GetManyByColumnsAsync<K>(IMongoCollection<TCollection> collection, List<Tuple<string, K>> filters)
        //{
        //    //var builder = Builders<TCollection>.Filter;
        //    //var key = "UserName";
        //    //var value = "111";
        //    //MongoDB.Driver.FilterDefinition<TCollection> filter = builder.Ne(key, value);

        //    //filters
        //    //  .Where(f => f.Item2.ToString() != string.Empty && f.Item2.ToString() != "0").ToList()
        //    //.ForEach(f => { filter = filter & builder.Eq(f.Item1, f.Item2); });

        //    var filter = Builders<TCollection>.Filter.Eq(x => x.UserName, "a");
        //    filter = filter & (Builders<TCollection>.Filter.Eq(x => x.Email, "koko"));


        //    var result = await collection.FindAsync<TCollection>(filter);
        //     return result.ToList();
        //}

        //var filter = Builders<ExerciseContex>.Filter.Eq(s => s.ExerciseId, exercise.ExerciseId);

        /// <summary>
        /// fetch many by context ids
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TCollection>> GetManyAsync(IMongoCollection<TCollection> collection,
                                                           IEnumerable<string> ids)
        {
            var list = new List<TCollection>();
            foreach (var id in ids)
            {
                //var doc = await GetOneAsync()
                var doc = await GetOneAsync(collection, id);
                if (doc == null) continue;
                list.Add(doc);
            }

            return list;
        }

        /// <summary>
        /// Fetch One entity by Id
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<TCollection> GetOneAsync(IMongoCollection<TCollection> collection, TContext context)
        {
            //return await collection.Find(new BsonDocument("_id", context.Id)).FirstOrDefaultAsync();
            var filter = new BsonDocument("_id", context.Id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Protected constructor
        /// </summary>
        //protected EntityService()
        //{
        //    MongoConnectionHandler = new MongoConnectionHandler<T>();
        //}

        /// <summary>
        /// Fetches Entity details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TCollection> GetOneAsync(IMongoCollection<TCollection> collection, string id)
        {

            // var result = MongoConnectionHandler.MongoCollection.Find(filter).FirstAsync();
            return await GetOneAsync(collection, new TContext { Id = new ObjectId(id) });
        }



        /*public virtual async Task<TCollection> GetOneAsync(IMongoCollection<TCollection> collection, FilterDefinition<> filter)
        {
            //return await GetOneAsync(collection, new TContext { Id = new ObjectId(id) });
            var result = await collection.Find(filter);
        }
        */
        /// <summary>
        /// Fetch all entities
        /// </summary>
        /// <returns></returns>
        public virtual List<TCollection> Get(IMongoCollection<TCollection> collection)
        {
            var documents = collection
                .Find(_ => true).ToList();
            return documents;   
        }

        /*public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            var documents = MongoConnectionHandler.MongoCollection;
            return documents;
        }
        */
        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="enitity"></param>
        //public virtual Task<bool> Update(T enitity);


        /// <summary>
        /// get all entities
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TCollection>> GetAllAsync(IMongoCollection<TCollection> collection)
        {
            var documents = await collection.FindAsync(t => true);
            return documents.ToList();
        }


        
    }
}
