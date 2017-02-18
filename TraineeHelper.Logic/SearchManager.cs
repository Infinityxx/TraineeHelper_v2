using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class SearchManager
    {
        private UserEntityService<User> userEntityService;

        public SearchManager()
        {
            userEntityService = new UserEntityService<User>();
        }


        public async Task<IEnumerable<UserProfileDemoContext>> FindMatches(SmartSearchFilter searchFilter)
        {
            List<SearchFilter> filters = searchFilter.ConvertToSearchFilters();

            IEnumerable<User> usersSearchResult = await userEntityService.GetManyByMultipleFilters(filters);

            if (usersSearchResult != null)
            {
                IEnumerable<UserContext> userContextList = usersSearchResult.ConvertToUserContextList();

                return userContextList.ConvertToUserDemoProfileContext();
            }

            return null;

        }

        public async Task<IEnumerable<UserProfileDemoContext>> SearchProfile(string userName, string currentUserId)
        {
            IEnumerable<User> usersSearchResult = await userEntityService.GetManyByUserName(userName, currentUserId);
       
            //IEnumerable<User> usersSearchResult = await userEntityService.GetManyByMultipleFilters(filters, currentUserId);
            if (usersSearchResult != null)
            {
                IEnumerable<UserContext> userContextList = usersSearchResult.ConvertToUserContextList();

                return userContextList.ConvertToUserDemoProfileContext();
            }

            return null;
        }

        public async Task<UserProfileDemoContext> SearchProfile(string userName)
        {
            var userSearchResult = await userEntityService.GetByUserName(userName);
            if (null != userSearchResult)
            {
               var userSearchResultContext = userSearchResult.ConvertToUserContext().ConvertToUserDemoProfileContext();
                return userSearchResultContext;
            }
                
            return null;
        }

    }
}
