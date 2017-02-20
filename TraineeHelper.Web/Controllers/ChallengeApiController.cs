using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Web.Controllers
{
    public class ChallengeApiController : ApiController
    {
        private ChallengeManager challengeManager;

        public ChallengeApiController()
        {
            challengeManager = new ChallengeManager();
        }

        


        [HttpPost]
        [Route("api/CreateChallenges")]
        public async Task<HttpResponseMessage> CreateChallenges([FromBody]ChallengesPackage challengpkg)
        {
            if(!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            
            var result = await challengeManager.CreateChallenge(challengpkg);
            if(!result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");
            return Request.CreateResponse(HttpStatusCode.Created, "Request successfully sent");
        }

        [HttpPost]
        [Route("api/ChallengeInformation")]
        public async Task<HttpResponseMessage> ChallengeInformation(string id)
        {
            var result = await challengeManager.FindChallengeById(id);
            if (null != result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
        }

        [HttpPost]
        [Route("api/UpdateChallenge")]
        public async Task<HttpResponseMessage> UpdateChallenge([FromBody]ChallengeContext challengectx)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            var result = await challengeManager.UpdateChallenge(challengectx);
            if (null == result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");
            return Request.CreateResponse(HttpStatusCode.Created, "Request successfully sent");
        }

        [HttpPost]
        [Route("api/DeleteChallenge")]
        public async Task<HttpResponseMessage> DeleteChallenge([FromBody]ChallengeContext challengectx)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            var result = await challengeManager.DeleteChallenge(challengectx);
            if (!result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");
            return Request.CreateResponse(HttpStatusCode.Created, "Request successfully sent");
        }

        [HttpPost]
        [Route("api/FindUserChallenges")]
        public async Task<HttpResponseMessage> FindUserChallenges(string userId)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            var result = await challengeManager.FindUserChallenges(userId);
            if(null == result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong or no achievements found");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
