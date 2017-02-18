using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using TraineeHelper.Models;
using MongoDB.Driver.GridFS;
using MongoDB.Bson.Serialization;
using DataModel.DataModel;
using DataModel.Entities;
using TraineeHelper.Models.Entities;

namespace TraineeHelper.DAL
{
    public class MongoConnectionHandler<T> where T: IMongoEntity
    {
        
        public MongoClient _client;
        public IMongoDatabase _database { get; private set; }
        public IMongoCollection<T> MongoCollection { get; private set; }
        

        public MongoConnectionHandler()
        {
            const string connectionString = "mongodb://localhost:27017";
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("TraineeHelper");
            MongoCollection = _database.GetCollection<T>(typeof(T).Name.ToLower() + "s");

            RegisterMapIfNeeded<User>();
            RegisterMapIfNeeded<Trainee>();
            RegisterMapIfNeeded<Exercise>();
            RegisterMapIfNeeded<TrainingPlan>();
            RegisterMapIfNeeded<MedicalCondition>();
            RegisterMapIfNeeded<Challenge>();
            RegisterMapIfNeeded<TokenEntity>();
            RegisterMapIfNeeded<Location>();
            RegisterMapIfNeeded<Trainer>();
            RegisterMapIfNeeded<Gym>();
            RegisterMapIfNeeded<Connection>();
            RegisterMapIfNeeded<Expertise>();
            RegisterMapIfNeeded<Challenge>();

            var emailOptions = new CreateIndexOptions() { Unique = true };
            var userOptions = new CreateIndexOptions() { Unique = true , Sparse = true };

            var fieldEmail = new StringFieldDefinition<IUser>("Email");
            var fieldUserName = new StringFieldDefinition<IUser>("UserName");

            var emailIndexDefinition = new IndexKeysDefinitionBuilder<IUser>().Ascending(fieldEmail);
            _database.GetCollection<IUser>("iusers").Indexes.CreateOneAsync(emailIndexDefinition, emailOptions);

            var userNameindexDefinition = new IndexKeysDefinitionBuilder<IUser>().Ascending(fieldUserName);
            _database.GetCollection<IUser>("iusers").Indexes.CreateOneAsync(userNameindexDefinition, userOptions);
        }

        // Check to see if map is registered before registering class map
        // This is for the sake of the polymorphic types that we are using so Mongo knows how to deserialize
        public void RegisterMapIfNeeded<TClass>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TClass)))
                BsonClassMap.RegisterClassMap<TClass>();
        }
    }
}
