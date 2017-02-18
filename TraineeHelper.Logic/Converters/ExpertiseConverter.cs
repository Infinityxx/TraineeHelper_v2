using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class ExpertiseConverter
    {
        public static List<ExpertiseContext> ConvertToExpertisesContext(this IEnumerable<IExpertise> expertises)
        {
            List<ExpertiseContext> expertisesContext = new List<ExpertiseContext>();
            if (null == expertises)
                return expertisesContext;
            foreach (Expertise exp in expertises)
            {
                expertisesContext.Add(exp.ConvertToExpertiseContext());
            }

            return expertisesContext;
        }

        public static List<Expertise> ConvertToExpertises(this IEnumerable<ExpertiseContext> expertisesContext)
        {
            List<Expertise> expertises = new List<Expertise>();
            if (null == expertisesContext)
                return expertises;
            foreach(ExpertiseContext ctx in expertisesContext)
            {
                expertises.Add(ctx.ConvertToExpertise());
            }

            return expertises;
        }

        public static Expertise ConvertToExpertise(this ExpertiseContext context, bool generateId = false)
        {
            Expertise expertise = new Expertise();
            if (null == context)
                return expertise;
            expertise.Id = generateId ? ObjectId.GenerateNewId() : ObjectId.Parse(context.Id);
            expertise.ExpertiseName = context.ExpertiseName;

            return expertise;
        }

        public static ExpertiseContext ConvertToExpertiseContext(this IExpertise expertise)
        {
            ExpertiseContext expertiseContext = new ExpertiseContext();
            if (null == expertise)
                return expertiseContext;

            expertiseContext.ExpertiseName = expertise.ExpertiseName;
            expertiseContext.Id = expertise.Id.ToString();

            return expertiseContext;
        }
    }
}
