using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Web.Controllers
{
    //[ApiAuthenticationFilter]
    public class AuthenticateController : ApiController
    {
        #region Private variable.

        //private readonly ITokenServices _tokenServices;
        private readonly TokenManager TokenManager;
        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public AuthenticateController()
        {
            TokenManager = new TokenManager();
        }
        #endregion
       
        /// <summary>
        /// Authenticates user and returns token with expiry.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [Route("authenticate")]
        [Route("get/token")]
        public HttpResponseMessage Authenticate(UserContext user)
        {
            
            UserManager userManager = new UserManager();
            LoginManager loginManager = new LoginManager();
            //string userId = loginManager.Login(user.UserName, user.Password);
            string userId = loginManager.Login(user.Email, user.Password);
            
            if (userId != null)
            {
               
                return GetAuthToken(userId);
            }

            //if (!string.IsNullOrEmpty(user) && user == password)
            //{
            //    return GetAuthToken(1); // TODO use userId instead of 1
            //}

            //if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            //{
            //    var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
            //    if (basicAuthenticationIdentity != null)
            //    {
            //        var userId = basicAuthenticationIdentity.UserId;
            //        return GetAuthToken(userId);
            //    }
            //}
            return null;
        }

        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        private HttpResponseMessage GetAuthToken(string userId)
        {
            var token = TokenManager.GenerateToken(userId);
            var data = new
            {
                Token = token,
                TokenExpiry = ConfigurationManager.AppSettings["AuthTokenExpiry"],
                userId
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, data);
            //response.Headers.Add("Token", token.AuthToken);
            //response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
    }
}
