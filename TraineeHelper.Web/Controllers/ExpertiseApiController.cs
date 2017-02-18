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
    public class ExpertiseApiController : ApiController
    {
        private ExpertiseManager ExpertiseManager;

        public ExpertiseApiController()
        {
            ExpertiseManager = new ExpertiseManager();
        }

        [HttpPost]
        [Route("api/CreateExpertise")]
        public async Task<HttpResponseMessage> CreateExpertise([FromBody]ExpertiseContext expertiseCtx)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var result = await ExpertiseManager.CreateExpertise(expertiseCtx);
            if (!result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");
            return Request.CreateResponse(HttpStatusCode.OK, "Expertise Created Successfully");
        }

        [HttpGet]
        [Route("api/Expertise")]
        public async Task<HttpResponseMessage> ExpertiseInfo(string id)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var result = await ExpertiseManager.FindExpertiseById(id);
            if (null != result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
        }

        [HttpPost]
        [Route("api/FindExpertisesList")]
        public async Task<HttpResponseMessage> FindExpertisesList([FromBody]List<string> Ids)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var result = await ExpertiseManager.FindExpertisesByIds(Ids);
            if (null != result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
        }
    }
}
