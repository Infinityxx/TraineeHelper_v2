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
    public class TrainingPlanApiController : ApiController
    {
        private TrainingPlanManager trainingPlanManager;

        public TrainingPlanApiController()
        {
            trainingPlanManager = new TrainingPlanManager();
        }


        [HttpPost]
        [Route("api/CreateTrainingPlan")]
        public async Task<HttpResponseMessage> CreateTrainingPlan([FromBody]TrainingPlanContext trainingPlanctx)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var result = await trainingPlanManager.CreateTrainingPlan(trainingPlanctx);
            if(!result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");
            return Request.CreateResponse(HttpStatusCode.Created, "Request successfully sent");
        }

        [HttpPost]
        [Route("api/TrainingPlanInformation")]
        public async Task<HttpResponseMessage> TrainingPlanInformation(string id)
        {
            var result = await trainingPlanManager.FindTrainingPlanById(id);
            if (null != result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
        }
    }
}
