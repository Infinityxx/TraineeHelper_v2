using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;
using TraineeHelper.Web.Filters;

namespace TraineeHelper.Web.Controllers
{
    public class SearchDataController : ApiController
    {
        private SearchManager searchManager;
        private TrainerManager trainerManager;
        private UserManager userManager;

        public SearchDataController()
        {
            searchManager = new SearchManager();
            trainerManager = new TrainerManager();
            userManager = new UserManager();
        }

        [HttpGet]
        [Route("FindUserByUserName")]
        public async Task<HttpResponseMessage> FindUserByUserName(string userName)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var result = await searchManager.SearchProfile(userName);
            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.BadGateway, "couldnt find the profile");
        }

        [HttpGet]
        [Route("FindUser")]
        [ApiAuthenticationFilter(true)]
        public async Task<HttpResponseMessage> FindUser(string userName, string userId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            //string userId = Request.Cookies["userId"].Value;
            var result = await searchManager.SearchProfile(userName,userId);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "couldnt find the profile");
        }

        [HttpPost]
        [Route("AdvancedSearch")]
        //[ApiAuthenticationFilter(true)]
        public async Task<HttpResponseMessage> AdvancedSearch(SmartSearchFilter searchFilter)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var result = await searchManager.FindMatches(searchFilter);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "couldnt find a match");
        }



        [HttpGet]
        [Route("FindUserById")]
        public async Task<HttpResponseMessage> FinduserById(string userId)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            var result = await userManager.GetById(userId);
            if (null != result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.BadGateway, "counldnt find the user");
        }
        //[Route("ViewUser")]
        //public async Task<HttpResponseMessage> ViewUser([FromBody]UserProfileDemoContext userProfileDemoContext)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //    var result = await userManager.GetById(userProfileDemoContext.Id);
        //    switch(result.UserType)
        //    {
        //        case "Trainer":
        //            var trainerProfile = await trainerManager.ViewTrainerProfile(userProfileDemoContext.Id);
        //            if(null == result)
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, "couldnt find the profile");
        //            return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.BadRequest, "couldnt find the profile");
        //}

    }
}
