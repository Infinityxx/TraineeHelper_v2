using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using TraineeHelper.Web.Models;
using TraineeHelper.Web.Providers;
using TraineeHelper.Web.Results;
using DataModel.Entities;
using System.Net;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;
using TraineeHelper.Web.Filters;
using System.Drawing;
using System.Drawing.Imaging;

namespace TraineeHelper.Web.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Account")]
    public class AccountApiController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private RegisterManager registerManager;
        private UserManager myManager;
        private UpdateManager updateManager;

        public AccountApiController()
        {
            myManager = new UserManager();
            updateManager = new UpdateManager();
        }

        public AccountApiController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }
        

        // POST api/Account/Logout
        [Route("Logout")]
        [ApiAuthenticationFilter(true)]
        public IHttpActionResult Logout()
        {
            TokenManager tokenManager = new TokenManager();
            tokenManager.KillToken(HttpContext.Current.Request.Headers["Token"]);

            return Ok();
        }

        [HttpGet]
        [Route("GetUserName")]
        public async Task<HttpResponseMessage> GetUserName(string id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var user = await myManager.GetById(id);
            string userName = user.UserName;
            return Request.CreateResponse(HttpStatusCode.OK, userName);
        }

        
        [Route("UploadImage")]
        [HttpPost]
        public HttpResponseMessage PostUserImage(string userId)
        {
            string filePath;

            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowFileExtensions = new List<string>
                        {
                            ".jpg", ".gif", ".png"
                        };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg, .gif, ");
                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");
                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            
                            var fileDirectory = HttpContext.Current.Server.MapPath("~/UserImage/" + userId + "/");
                            if (!System.IO.Directory.Exists(fileDirectory))
                            {
                                System.IO.Directory.CreateDirectory(fileDirectory);
                            }
                            filePath = HttpContext.Current.Server.MapPath("~/UserImage/" + userId + "/" + postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            postedFile.InputStream.Close();
                            postedFile.InputStream.Dispose();                           
                        }
                    }
                    
                    filePath = HttpContext.Current.Server.MapPath("~/UserImage/" + userId + "/" + postedFile.FileName);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
                    Bitmap b = new Bitmap(img);
                    img.Dispose();
                    ToolManager toolManager = new ToolManager();
                    //Size size = new Size(5, 5);
                    System.Drawing.Image resizedImg = toolManager.ResizeImage(b, new Size(100, 100));
                    //Bitmap image = new Bitmap(i);
                    resizedImg.Save(filePath);
                    //
                    //
                    resizedImg.Dispose();
                    //if (System.IO.File.Exists(filePath))
                    //{
                    //    System.GC.Collect();
                    //    System.GC.WaitForPendingFinalizers();
                    //    System.IO.File.Delete(filePath);
                    //}
                    //i.Save(filePath);


                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.OK, message1);
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = ex.ToString();
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }


        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var result = await myManager.ChangeUserPasswordAsync(model.Email, model.OldPassword, model.NewPassword);
            
            if (!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"wrong email or password");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "password changed successfully");
        }

        [HttpPost]
        //[Route("UpdateAccount")]
        public async Task<HttpResponseMessage> UpdateAccount(UpdateAccountBindingModel model, string userId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var result = await updateManager.UpdateAccountAsync(userId, model.Email, model.PhoneVisibility
                , model.UserName, model.UserVisibility);

            if(!result)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "update failed");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "updated successfully");
        }


        [Route("ChangeEmail")]
        public async Task<HttpResponseMessage> ChangeEmail(ChangeEmailBindingModel model, string userNameId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var result = await myManager.ChangeEmail(model.Email, userNameId);

            if (result == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "bad email or email doesnt exists");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Email changed successfully");

        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/SetUserName
        [Route("SetUserName")]
        public async Task<HttpResponseMessage> SetUserName([FromBody]SetUserNameBindingModel model, string userNameId)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            myManager = new UserManager();
            var result = await myManager.SetUserName(model.UserName, userNameId);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        
        [HttpGet]
        public async Task<HttpResponseMessage> CheckUserNameAndEmailExistence(string email, string userName)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            registerManager = new RegisterManager();

            var isEmailExist = await registerManager.CheckEmailExistence(email);
            var isuserNameExist = await registerManager.CheckUserNameExistence(userName);

            bool[] existence = { isEmailExist, isuserNameExist };
            return Request.CreateResponse(HttpStatusCode.OK, existence);            
        }

        [Route("SetPhoneVisibility")]
        public async Task<HttpResponseMessage> SetPhoneVisibility(SetPhoneVisibilityBindingModel model, string userNameId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            myManager = new UserManager();
            var result = await myManager.SetPhoneVisibility(model.PhoneVisibility, userNameId);
            if (result != false)
                return Request.CreateResponse(HttpStatusCode.OK, "Phone Visibility Changed Successfully");
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // TODO move to SearchController (api)

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> Register(RegisterBindingModel model, string userRole, string userName)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            registerManager = new RegisterManager();

            switch(userRole)
            {
                case "Trainee":                  
                    var traineeResult = await registerManager.RegisterTraineeUser(model.Email, model.Password, userName);
                    return Request.CreateResponse(HttpStatusCode.OK, traineeResult);
                case "Trainer":
                    var trainerResult = await registerManager.RegisterTrainerUser(model.Email, model.Password, userName);
                    return Request.CreateResponse(HttpStatusCode.OK, trainerResult);
                case "Gym":
                    var gymResult = await registerManager.RegisterGymUser(model.Email, model.Password, userName);
                    return Request.CreateResponse(HttpStatusCode.OK, gymResult);                 
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        /*[HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                //return View(ModelState)
            }

            //var userId = await UserManager.FindByNameAsync(model.Email);
            HttpResponseMessage res = new AuthenticateController().Authenticate(model.Username, model.Password);
            if (user != null)
            {
                if(!await UserManager.IsEmailConfirmed(user.Id))
                {
                    ViewBag.errorMessage = "You must have a confirmed email to log on";
                    //return view()
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager
            }
        }
        */

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
