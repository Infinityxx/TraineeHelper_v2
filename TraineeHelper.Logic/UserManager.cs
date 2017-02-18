using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class UserManager
    {
        private UserEntityService<User> UserEntityService;

        public UserManager()
        {
            UserEntityService = new UserEntityService<User>();
        }

        public async Task<UserContext> SaveUser(UserContext userContext)
        {
            //User user = UserConverter.ToUser(userContext);
            if(string.IsNullOrEmpty(userContext.Id))
            {
                User user = UserConverter.ConvertToUser(userContext, true);
                await UserEntityService.SaveOneAsync(user);
                userContext.Id = user.Id.ToString();
                return userContext;
            }
            var update = UserConverter.ConvertToUser(userContext);
            await UserEntityService.Update(update);      
            return userContext;
        }
     
        public string authenticate(string email, string password)
        {
            var id = UserEntityService.Authenticate(email, password);
            return id;
        }

        public async Task<bool> DeleteUser(string id)
        {
            return await UserEntityService.Delete(id);
        }

        public async Task<IEnumerable<UserContext>> GetAllAsync()
        {
            var users = await UserEntityService.GetAllAsync();
            return users.Select(u => new UserContext
            {
                Id = u.Id.ToString(),
                UserType = u.UserType,
                Email = u.Email,
                UserName = u.UserName,
                IsActive = u.IsActive,
                Description = u.Description,
                Created = u.Created,
                Modified = u.Modified,
                PhoneNumber = u.PhoneNumber,
                Location = u.Location.ConvertToLocationContext(),
                Password = u.Password,
                PhoneVisibility = u.PhoneVisibility,
                UserVisibility = u.UserVisibility
            });
        }

        public async Task<UserContext> GetById(string id)
        {
            User user = await UserEntityService.GetById(id);
            return user.ConvertToUserContext();
        }

        public string authenticatePasswordChange(string email, string oldPassword)
        {
            return UserEntityService.AuthenticateByEmail(email, oldPassword);
        }

        public async Task<bool> ChangeUserPasswordAsync(string email, string oldPassword, string newPassword)
        {
            string id = authenticatePasswordChange(email, oldPassword);
            if (id != null)
            {
                var user = await UserEntityService.GetById(id);
                //user.Id = id.ConvertToObjectId();
                user.Password = newPassword;
                var result = await UserEntityService.Update(user);
                if (null != result)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<UserContext> SetUserName(string userName, string userNameId)
        {
            var user = await UserEntityService.GetById(userNameId);
            user.UserName = userName;
            await UserEntityService.Update(user);
            UserContext userContext = user.ConvertToUserContext();
            return userContext;
        }

        public async Task<string> GetUserNameById(string id)
        {
            var user = await UserEntityService.GetById(id);
            return user.UserName;
        }

        public async Task<string> GetUserIdByMail(string email)
        {
            return await UserEntityService.GetByEmailEntityIdAsync(email);
        }

        public async Task<string> ChangeEmail(string email, string userNameId)
        {
            var user = await UserEntityService.GetById(userNameId);
            user.Email = email;
            var result = await UserEntityService.Update(user);
            if(result != null)
            {
                return email;
            }
            return null;
        } 

        public async Task<bool> SetPhoneVisibility(bool phoneVisibility, string userNameId)
        {
            var user = await UserEntityService.GetById(userNameId);
            user.PhoneVisibility = phoneVisibility;
            var result = await UserEntityService.Update(user);
            if (result != null)
                return true;
            return false;
        }

        public List<String> GetUserImagesPath(String userId)
        {
            DirectoryInfo Folder;
            FileInfo[] Images;

            Folder = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/UserImage/" + userId + "/"));
            //var fileDirectory = HttpContext.Current.Server.MapPath("~/UserImage/" + userId + "/");
            Images = Folder.GetFiles();
            List<String> imagesList = new List<String>();
            var version = DateTime.Now;
            for (int i = 0; i < Images.Length; i++)
            {
                //File.GetLastWriteTime(HttpContext.Current.Server.MapPath(Folder.Parent + "\\" + Folder.Name + "\\" + Images[i].Name)).ToFileTime();
                imagesList.Add(String.Format(@"{0}\\{1}\\{2}?ver={3}",Folder.Parent ,Folder.Name, Images[i].Name, version));
            }

            return imagesList;
        }
    }
}
