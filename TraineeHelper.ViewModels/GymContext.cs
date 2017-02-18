using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class GymContext : UserContext
    {
        public int Reputation { get; set; }
        public double Price { get; set; }
        public bool ParkingLot { get; set; }
        public bool Showers { get; set; }
        public string[] Expertise { get; set; }
        public int ExperienceYears { get; set; }
        public List<ExpertiseContext> ExpertisesOptions { get; set; }

        public GymContext()
        {
            ExpertisesOptions = new List<ExpertiseContext>();
        }

        public GymContext(string email, string password)
        {
            Email = email;
            Password = password;
            ExpertisesOptions = new List<ExpertiseContext>();
        }

        public GymContext(string email, string password, string userName)
        {
            Email = email;
            Password = password;
            UserName = userName;
            ExpertisesOptions = new List<ExpertiseContext>();
        }
    }
}
