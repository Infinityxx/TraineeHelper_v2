using System.Web.Http.Controllers;
using TraineeHelper.Logic;

namespace TraineeHelper.Web.Filters
{
    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        private TokenManager TokenManager;

        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthenticationFilter()
        {
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string token, HttpActionContext actionContext)
        {
            // TODO - check token against DB and return true if found ***************************
            TokenManager = new TokenManager();

            return TokenManager.ValidateToken(token); 
        }
    }
}