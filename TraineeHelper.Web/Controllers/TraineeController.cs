using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;
using TraineeHelper.Web.Filters;

namespace TraineeHelper.Web.Controllers
{
    //[ApiAuthenticationFilter(true)]
    //[RoutePrefix("v1/trainers/trainer")]
    public class TraineeController : ApiController
    {
        private readonly TraineeManager traineeManager;
 
        public TraineeController()
        {
            traineeManager = new TraineeManager();
        }


        // Get: /api/Trainee/514b46581cbfe31ad86ec630
        [HttpGet]
       // [ApiAuthenticationFilter(true)]       
        public async Task<HttpResponseMessage> Get(string id)
        {
            if(id == "test")
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Trainee count is 85");
            }

            var trainee = await traineeManager.GetById(id);
            if (trainee != null)
                return Request.CreateResponse(HttpStatusCode.OK, trainee);
            //string json = JsonConvert.SerializeObject(trainee);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainee not found for provided id");
        }
        //TODO complete controller
        // PUT: /api/Trainee/AddExercise/id
        [HttpPut]
        [Route("api/trainee/AddExercise/id")]
        public async Task<HttpResponseMessage> AddExerciseToTrainingPlan(string id, [FromBody]ViewModels.ExerciseContext exerciseContext)
        {
            var trainee = await traineeManager.GetById(id);
            if (trainee != null)
            {
                //await traineeManager.UpdateTrainingPlan(trainee, exerciseContext);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Adding exercise to training plan failed");
        }

        // PUT: /api/Trainee/TraineeMedicalCondition/id
        [HttpPut]
        [Route("api/trainee/MedicalCondition")]
        public async Task<HttpResponseMessage> TraineeMedicalCondition(string id, [FromBody]ViewModels.MedicalConditionContext medicalContext)
        {
                var result = await traineeManager.UpdateMedicalCondition(id, medicalContext);
                if(result)
                    return Request.CreateResponse(HttpStatusCode.OK);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Trainee or MedicalCondition not found or wrong update format");
        }

        [HttpDelete]
        [Route("api/trainee/delete")]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            try
            {
                await traineeManager.DeleteTrainee(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainee not found or delete failed");
            }
        }

        // PUT: /api/Trainee/Update
        [HttpPut]
        [Route("api/trainee/update")]
        public async Task<HttpResponseMessage> Update([FromBody] ViewModels.TraineeContext trainee)
        {
            try
            {
                await traineeManager.SaveOneAsync(trainee);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainee not found or wrong update format");
            }
        }


        [HttpGet]
        [Route("api/trainee/TrainingPlan")]
        public async Task<HttpResponseMessage> TraineeTrainingPlan(string id)
        {
            try
            {
                //var trainingPlan = await traineeManager.GetTraineeTrainingPlanAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK/*, trainingPlan*/);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainee not found or wrong format");
            }
        }

        [HttpGet]
        [Route("api/trainee/Achivements")]
        public HttpResponseMessage TraineeAchivements(string id)
        {
            try
            {
                var achievements = traineeManager.GetTraineeAchievements(id);
                return Request.CreateResponse(HttpStatusCode.OK, achievements);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainee not found or wrong format");
            }
        }
        
    }
}
