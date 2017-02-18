using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class UserContext 
    {
        public string Id { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public LocationContext Location { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneVisibility { get; set; }
        public bool UserVisibility { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public List<String> imagesList;


        public UserContext()
        {
            IsActive = true;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
            UserName = Email;
            UserVisibility = true;
            
        }


        public UserContext(string email, string password)
        {
            this.Email = email;
            this.Password = password;
            IsActive = true;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
            UserName = Email;
            UserVisibility = true;
            List<String> imagesList = new List<string>();
        }

    }
}
