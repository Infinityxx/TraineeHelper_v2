using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TraineeHelper.Logic;

namespace TraineeHelper.Web.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        //private TokenManager TokenManager;

        public AuthorizationRequiredAttribute()
        {
            //TokenManager = new TokenManager();
            //TokenManager.GetTokenEntityServiceType();//GetType(ITokenService)
        }

        private const string Token = "Token";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //Get API key provider //was ITokenService
            var provider = filterContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(TokenManager)) as TokenManager;
            
            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();

                //Validate Token
                if (provider != null && !provider.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                    filterContext.Response = responseMessage;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}