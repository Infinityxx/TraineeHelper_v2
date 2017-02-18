using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;
using TraineeHelper.Models.Entities;

namespace TraineeHelper.DAL.Services
{
    public class ConnectionEntityService
    {
        protected readonly MongoConnectionHandler<IConnection> Connections;
        protected readonly EntityService<IConnection, Connection> entityServices;

        public ConnectionEntityService()
        {
            Connections = new MongoConnectionHandler<IConnection>();
            entityServices = new EntityService<IConnection, Connection>();
        }

        public async Task<Connection> CreateConnection(Connection connection)
        {
            if(null != connection)
            {
                connection.Created = DateTime.Now;
                var result = Connections.MongoCollection.InsertOneAsync(connection);
                await result;
                if (result.IsCompleted)
                    return connection;
                return null;
            }
            return null;         
        }

        public async Task<Connection> Update(Connection connection)
        {
            connection.Modified = DateTime.Now;
            await Connections.MongoCollection.ReplaceOneAsync(
                new BsonDocument("_id",
                new ObjectId(connection.Id.ToString())), connection);
            return connection;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await entityServices.Delete(Connections.MongoCollection, id);
            return result;
        }

        public async Task<Connection> GetById(string id)
        {
            var task = (Task)((dynamic)entityServices.GetOneAsync(Connections.MongoCollection, id));
            await task;
            var connection = (Connection)((dynamic)task).Result;
            return connection;  
        }

        public async Task<IEnumerable<IConnection>> GetAllActiveConnectionRelatedToUser(string id)
        {
            var builder = Builders<IConnection>.Filter;
            var filter = ((builder.Eq("Sender", id) | builder.Eq("Reciever", id)) & builder.Eq("ConnectionStatus", ConnectionStatus.ACCEPTED));
            var result = await Connections.MongoCollection.Find(filter).ToListAsync();
  
            return result;          
        }

        public async Task<IEnumerable<IConnection>> GetUserConnectionRequests(string id)
        {

            var builder = Builders<IConnection>.Filter;
            var filter = builder.Eq("Reciever", id) & builder.Eq("ConnectionStatus", ConnectionStatus.REQUEST);
            var result = await Connections.MongoCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<IConnection>> GetByUserId(string id)
        {
            var task = (Task)((dynamic)entityServices.GetManyByColumnAsync<string>(Connections.MongoCollection, "Reciever", id));
            await task;

            var connections = ((dynamic)task).Result;
            return connections;
        }

        public async Task<IEnumerable<IConnection>> GetConnectivityByUsers(string senderId, string recieverId)
        {
            var builder = Builders<IConnection>.Filter;

            var filter = (builder.Eq("Sender", senderId) & builder.Eq("Reciever", recieverId))
                | (builder.Eq("Sender", recieverId) & builder.Eq("Reciever", senderId));
            var result = await Connections.MongoCollection.Find(filter).ToListAsync();
            return result;
        }

    }
}
