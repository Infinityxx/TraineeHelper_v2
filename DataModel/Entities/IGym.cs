using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models.Entities
{
    public interface IGym : IUser
    {
        int Reputation { get; set; }
        double Price { get; set; }
        bool ParkingLot { get; set; }
        bool Showers { get; set; }
        string[] Expertise { get; set; }
        int ExperienceYears { get; set; }
    }
}
