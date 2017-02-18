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
    public class GymApiController : ApiController
    {
        private GymManager gymManager;

        public GymApiController()
        {
            gymManager = new GymManager();
        }

        [HttpPost]
        [Route("UpdateGymReputation")]
        public async Task<HttpResponseMessage> UpdateGymReputation(string gymId, int reputation)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadGateway);
            var result = await gymManager.UpdateGymReputation(gymId, reputation);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
