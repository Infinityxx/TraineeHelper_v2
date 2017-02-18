using DataModel.Entities;
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
    public class UpdateManager
    {
        private UserEntityService<User> UserEntityService;

        public UpdateManager()
        {
            UserEntityService = new UserEntityService<User>();
        }

        public async Task<bool> UpdateAccountAsync(string Id, string email, bool phoneVisibility,
                                               string userName, bool userVisibility)
        {
            var user = await UserEntityService.GetById(Id);
            user.Email = email;
            user.PhoneVisibility = phoneVisibility;
            user.UserName = userName;
            user.UserVisibility = userVisibility;

            var result = await UserEntityService.Update(user);
            if (null != result)
            {
                return true;
            }
            return false;
        }
    }
}
