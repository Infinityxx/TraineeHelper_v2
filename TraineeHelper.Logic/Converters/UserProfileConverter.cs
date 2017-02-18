using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class UserProfileConverter
    {
        public static IEnumerable<UserProfileDemoContext> ConvertToUserDemoProfileContext(this IEnumerable<UserContext> userContextList)
        {
            if (null == userContextList)
                return null;
            var result = userContextList.Select(user => new UserProfileDemoContext
            {
                Id = user.Id,
                UserName = user.UserName,
                UserType = user.UserType
            });
            return result;
        }

        public static UserProfileDemoContext ConvertToUserDemoProfileContext(this UserContext userContext)
        {
            UserProfileDemoContext profileDemoContext = new UserProfileDemoContext();
            if (null == userContext)
                return null;
            profileDemoContext.Id = userContext.Id;
            profileDemoContext.UserName = userContext.UserName;
            profileDemoContext.UserType = userContext.UserType;

            return profileDemoContext;
        }
    }
}
