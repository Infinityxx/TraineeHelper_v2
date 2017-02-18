using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class UserTrainerProfileDataContext
    {
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public LocationContext Location { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int Reputation { get; set; }
        public double Price { get; set; }
        public string[] Expertise { get; set; }
        public int ExperienceYears { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public List<int> ReputationOptions { get; set; }


        public UserTrainerProfileDataContext()
        {
            ReputationOptions = new List<int>();
            ReputationOptions.Add(1);
            ReputationOptions.Add(2);
            ReputationOptions.Add(3);
            ReputationOptions.Add(4);
            ReputationOptions.Add(5);
        }
    }
}
