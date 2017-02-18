using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models.Entities;

namespace TraineeHelper.DAL.Services
{
    public class ExpertiseEntityService
    {
        protected readonly MongoConnectionHandler<IExpertise> Expertises;
        protected readonly EntityService<IExpertise, Expertise> entityServices;

        public ExpertiseEntityService()
        {
            Expertises = new MongoConnectionHandler<IExpertise>();
            entityServices = new EntityService<IExpertise, Expertise>();
        }

        public async Task<bool> CreateExpertise(Expertise expertise)
        {
            expertise.Created = DateTime.Now;
            var result = Expertises.MongoCollection.InsertOneAsync(expertise);
            await result;
            if (result.IsCompleted)
                return true;
            return false;
        }

        public async Task<Expertise> Update(Expertise expertise)
        {
            expertise.Modified = DateTime.Now;
            await Expertises.MongoCollection.ReplaceOneAsync(
                new BsonDocument("_id",
                new ObjectId(expertise.Id.ToString())), expertise);

            return expertise;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await entityServices.Delete(Expertises.MongoCollection, id);
            return result;
        }

        public IEnumerable<IExpertise> GetAllExpertises()
        {
            var expertisesList = entityServices.Get(Expertises.MongoCollection);
            return expertisesList;
        }

        public async Task<IExpertise> GetExpertiseById(string Id)
        {
            var result = await entityServices.GetOneAsync(Expertises.MongoCollection, Id);
            
            return result;
        }

        public async Task<IEnumerable<IExpertise>> GetExpertisesByIds(List<ObjectId> Ids)
        {
            var builder = Builders<IExpertise>.Filter;
            List<IExpertise> returnedExpertises = new List<IExpertise>();
            foreach (ObjectId id in Ids)
            {
                var filter = builder.Eq("_id", id);
                var result = await Expertises.MongoCollection.Find(filter).ToListAsync();
                returnedExpertises.Add(result.FirstOrDefault());
            }
            return returnedExpertises;
        } 
    }
}
