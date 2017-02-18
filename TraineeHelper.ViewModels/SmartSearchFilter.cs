using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    public class SmartSearchFilter
    {
        public string UserType { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public int Reputation { get; set; }
        public Gender GenderFilter { get; set; }
        public string[] Expertise { get; set; }
        public bool Showers { get; set; }
        public bool Parking { get; set; }
        public string ExperienceYears { get; set; }
    }
}
