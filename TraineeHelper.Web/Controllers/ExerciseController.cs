using System.Web.Http;

namespace TraineeHelper.Web.Controllers
{
    //[ApiAuthenticationFilter(true)]
    public class ExerciseController : ApiController
    {
        //private readonly ExerciseService exerciseService;
        //
        //public ExerciseController()
        //{
        //    exerciseService = new ExerciseService();
        //}
        //
        //// Get: /api/exercise/514b46581cbfe31ad86ec630
        //public HttpResponseMessage Get(string id)
        //{
        //   
        //    //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(HttpResponseMessage));
        //    //HttpResponse response = Request.CreateResponse
        //    var exercise = exerciseService.GetById(id);
        //    if (exercise != null)
        //        //string json = JsonConvert.SerializeObject
        //        //object objResponse = jsonSerializer.ReadObject(exercise);
        //        return Request.CreateResponse(HttpStatusCode.OK, exercise, "application/json");
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Exercise not found for provided id");
        //}
        //
        //// POST: /api/Exercise/Create
        ///*public async Task<HttpResponseMessage> Post([FromBody] DataModel.ExerciseContex exercise)
        //{
        //    await exerciseService.Create(exercise);
        //    return Request.CreateResponse(HttpStatusCode.Created, exercise);              
        //}
        //*/
        //
        //
        //// POST: /api/Exercise/Delete/514b46581cbfe31ad86ec630
        ////[ApiAuthenticationFilter(false)]
        //public HttpResponseMessage Delete(string id)
        //{
        //    try
        //    {
        //        exerciseService.Delete(id);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Exercise not found or delete failed");
        //    }
        //}
        //
        //// PUT: /api/Exercise/Update
        ///*public HttpResponseMessage Update([FromBody] DataModel.ExerciseContex exercise)
        //{
        //    try
        //    {
        //        exerciseService.Update(exercise);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Exercise not found or wrong update format");
        //    }
        //}
        //*/

    }
}