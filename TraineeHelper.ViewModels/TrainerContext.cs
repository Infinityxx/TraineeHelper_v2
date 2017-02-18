using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class TrainerContext : UserContext
    {
        public int Reputation { get; set; }
        public Gender Gender { get; set; }
        public double Price { get; set; }
        public string[] Expertise { get; set; }
        public int ExperienceYears { get; set; }
        public List<ExpertiseContext> ExpertisesOptions { get; set; }
        

        public TrainerContext()
        {
            ExpertisesOptions = new List<ExpertiseContext>();
        }


        public TrainerContext(string email, string password)
        {
            Email = email;
            Password = password;
            ExpertisesOptions = new List<ExpertiseContext>();
        }

        public TrainerContext(string email, string password, string userName)
        {
            UserName = userName;
            Email = email;
            Password = password;
            ExpertisesOptions = new List<ExpertiseContext>();
        }
    }
}
