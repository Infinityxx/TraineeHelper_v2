using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TraineeHelper.Logic;

namespace TraineeHelper.Web.Controllers
{
    public class TrainerApiController : ApiController
    {
        private UserManager userManager;
        private TrainerManager trainerManager;

        public TrainerApiController()
        {
            userManager = new UserManager();
            trainerManager = new TrainerManager();
        }

        [HttpPost]
        [Route("UpdateTrainerReputation")]
        public async Task<HttpResponseMessage> UpdateTrainerReputation(string trainerId, int reputation)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadGateway);
            var result = await trainerManager.UpdateTrainerReputation(trainerId, reputation);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}

