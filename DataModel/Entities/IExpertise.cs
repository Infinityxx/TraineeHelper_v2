using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models.Entities
{
    public interface IExpertise : IMongoCommon
    {
        string ExpertiseName { get; set; }
    }
}
