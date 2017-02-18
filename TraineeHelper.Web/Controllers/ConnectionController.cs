using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;
using TraineeHelper.Web.Models;

namespace TraineeHelper.Web.Controllers
{
    public class ConnectionController : ApiController
    {
        private ConnectionManager ConnectionManager;
        

        public ConnectionController()
        {
            ConnectionManager = new ConnectionManager();

        }

        [HttpPost]
        [Route("RequestConnection")]
        public async Task<HttpResponseMessage> RequestConnection(ConnectionContext model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var IsConnectionExist = await ConnectionManager.CheckConnectionExistence(model.Sender, model.Reciever);
            
            if (!IsConnectionExist)
            {
                var result = await ConnectionManager.SaveConnection(model);
                if (null == result)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");
                //TODO send notification to the addressee
                return Request.CreateResponse(HttpStatusCode.OK, "Request successfully sent");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Connection Already Exists");
        }

        [HttpPost]
        [Route("DeclineRequest")]
        public async Task<HttpResponseMessage> DeclineRequest(string id)
        {
            var result = await ConnectionManager.UpdateConnectionStatus(id, Common.ConnectionStatus.REJECTED);
            if (!result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");

            return Request.CreateResponse(HttpStatusCode.OK, "Request successfully declined");
        }

        [HttpPost]
        [Route("ApproveRequest")]
        public async Task<HttpResponseMessage> ApproveRequest(string id)
        {
            var result = await ConnectionManager.UpdateConnectionStatus(id, Common.ConnectionStatus.ACCEPTED);
            if (!result)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something went wrong");

            return Request.CreateResponse(HttpStatusCode.OK, "Request successfully Approve");
        }

        [HttpGet]
        [Route("api/ConnectionInformation")]
        public async Task<HttpResponseMessage> ConnectionInformation(string id)
        {
            var result = await ConnectionManager.FindConnectionById(id);
            if (null != result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
        }
    }
}

