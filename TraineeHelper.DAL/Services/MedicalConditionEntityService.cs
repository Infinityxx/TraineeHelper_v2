using DataModel.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models;

namespace TraineeHelper.DAL.Services
{
    public class MedicalConditionEntityService //: IEntityService<MedicalConditionContext>
    {
        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        /*public Task<IEnumerable<MedicalConditionContext>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> GetManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> GetManyAsync(IEnumerable<MedicalConditionContext> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> GetOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> GetOneAsync(MedicalConditionContext context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveManyAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveManyAsync(IEnumerable<MedicalConditionContext> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveOneAsync(MedicalConditionContext context)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> SaveManyAsync(IEnumerable<MedicalConditionContext> contexts)
        {
            throw new NotImplementedException();
        }

        public Task<MedicalConditionContext> SaveOneAsync(MedicalConditionContext entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(MedicalConditionContext enitity)
        {
            throw new NotImplementedException();
        }

       /* public override void Update(MedicalCondition medicalCondition)
        {
            var filter = Builders<MedicalCondition>.Filter.Eq(s => s.Id, medicalCondition.Id);
            var update = Builders<MedicalCondition>.Update
                .Set("FatPercent", medicalCondition.FatPercent)
                .Set("Age", medicalCondition.Age)
                .Set("Height", medicalCondition.Height)
                .Set("MuscleMass", medicalCondition.MuscleMass)
                .Set("Weight", medicalCondition.Weight)
                .CurrentDate("lastModified");

            var result = MongoConnectionHandler.MongoCollection.UpdateOneAsync(filter, update);
        }
        */
    }
}
