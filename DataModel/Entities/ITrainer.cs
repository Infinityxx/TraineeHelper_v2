using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.Models
{
    public interface ITrainer : IUser
    {
        int Reputation { get; set; }
        double Price { get; set; }
        string[] Expertise { get; set; }
        int ExperienceYears { get; set; }
        Gender Gender { get; set; }
    }
}
