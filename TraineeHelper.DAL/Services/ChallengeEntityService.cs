using DataModel.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;

namespace TraineeHelper.DAL.Services
{
    public class ChallengeEntityService
    {
        protected readonly MongoConnectionHandler<IChallenge> Challenges;
        protected readonly EntityService<IChallenge, Challenge> entityServices;

        public ChallengeEntityService()
        {
            Challenges = new MongoConnectionHandler<IChallenge>();
            entityServices = new EntityService<IChallenge, Challenge>();
        }

        public async Task<bool> CreateChallenge(List<Challenge> challenges)
        {
            Task result;
            foreach (Challenge c in challenges)
            {
                c.Created = DateTime.Now;
                result = Challenges.MongoCollection.InsertOneAsync(c);
                await result;
                if (result.IsFaulted)
                    return false;
            }
            //var result = Challenges.MongoCollection.InsertOneAsync(challenge);
            //await result;
            return true;
        }

        public async Task<Challenge> Update(Challenge challenge)
        {
            challenge.Modified = DateTime.Now;
            await Challenges.MongoCollection.ReplaceOneAsync(
                new BsonDocument("_id",
                new ObjectId(challenge.Id.ToString())), challenge);

            return challenge;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await entityServices.Delete(Challenges.MongoCollection, id);
            return result;
        }

        public async Task<IEnumerable<IChallenge>> GetAllChallengesRelatedToUser(string value, string id)
        {
            var builder = Builders<IChallenge>.Filter;
            var filter = builder.Eq(value, id);
            var result = await Challenges.MongoCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<Challenge> GetById(string id)
        {
            var task = (Task)((dynamic)entityServices.GetOneAsync(Challenges.MongoCollection, id));
            await task;

            var challenges = ((dynamic)task).Result;
            return challenges;
        }

        public async Task<IEnumerable<IChallenge>> GetChallengesByUsers(string senderId, string recieverId)
        {
            var builder = Builders<IChallenge>.Filter;

            var filter = (builder.Eq("TrainerId", senderId) & builder.Eq("TraineeId", recieverId));
            var result = await Challenges.MongoCollection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<IChallenge>> GetByUserId(string id)
        {
            var task = (Task)((dynamic)entityServices.GetManyByColumnAsync<string>(Challenges.MongoCollection, "TraineeId", id));
            await task;

            var challenges = ((dynamic)task).Result;
            return challenges;
        }

    }
}
