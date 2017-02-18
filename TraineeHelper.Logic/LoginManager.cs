using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL;
using TraineeHelper.DAL.Services;
using TraineeHelper.Models;

namespace TraineeHelper.Logic
{
    public class LoginManager
    {
        private UserManager userManager;
        //private EntityService<IMongoEntity, IMongoEntity > entityService;

        public LoginManager()
        {
            userManager = new UserManager();
        }

        /*public string Login(string userName, string password)
        {
            return userManager.authenticate(userName, password);
        }
        */
        public string Login(string email, string password)
        {
            return userManager.authenticate(email, password);
        }
    }
}
