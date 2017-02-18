using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class ExpertiseManager
    {
        private ExpertiseEntityService ExpertiseEntityService;

        public ExpertiseManager()
        {
            ExpertiseEntityService = new ExpertiseEntityService();
        }

        public async Task<bool> CreateExpertise(ExpertiseContext expertiseCtx)
        {
            if (null == expertiseCtx)
                return false;
            Expertise expertise = expertiseCtx.ConvertToExpertise(true);
            var result = await ExpertiseEntityService.CreateExpertise(expertise);
            return result;
        }

        public async Task<ExpertiseContext> UpdateExpertise(ExpertiseContext expetriseCtx)
        {
            if (null == expetriseCtx)
                return null;
            Expertise expertise = expetriseCtx.ConvertToExpertise(false);
            var result = await ExpertiseEntityService.Update(expertise);
            return result.ConvertToExpertiseContext();
        }

        public async Task<Expertise> UpdateExpertise(Expertise expertise)
        {
            var result = await ExpertiseEntityService.Update(expertise);
            return result;
        }

        public async Task<bool> DeleteExpertise(ExpertiseContext expertisectx)
        {
            if (null == expertisectx)
                return false;
            Expertise expertise = expertisectx.ConvertToExpertise(false);
            var result = await ExpertiseEntityService.Delete(expertisectx.Id.ToString());
            return result;
        }

        public async Task<List<ExpertiseContext>> FindExpertisesByIds(List<string> Ids)
        {
            if (Ids.Count == 0)
                return null;
            List<ObjectId> ExpertiseIds = new List<ObjectId>();
            foreach(string Id in Ids)
            {
                ExpertiseIds.Add(ObjectId.Parse(Id));
            }
            var result = await ExpertiseEntityService.GetExpertisesByIds(ExpertiseIds);

            return result.ConvertToExpertisesContext(); 
        }

        public async Task<ExpertiseContext> FindExpertiseById(string Id)
        {
            if (Id == null)
                return null;

            var result = await ExpertiseEntityService.GetExpertiseById(Id);
            return result.ConvertToExpertiseContext();
        }

        public List<ExpertiseContext> GetAllExpertises()
        {
            var result = ExpertiseEntityService.GetAllExpertises();
            return result.ConvertToExpertisesContext();
        }
    }
}
