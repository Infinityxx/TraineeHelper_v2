using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class UserProfileDemoContext
    {
        public string Id { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
    }
}
