using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TraineeHelper.Web.Controllers
{
    public class MedicalConditionController : ApiController
    {
        //private readonly MedicalConditionService medicalConditionService;
        //
        //public MedicalConditionController()
        //{
        //    medicalConditionService = new MedicalConditionService();
        //}
        //
        //// Get: /api/MedicalCondition/514b46581cbfe31ad86ec630
        //public HttpResponseMessage Get(string id)
        //{
        //    var medicalCondition = medicalConditionService.GetById(id);
        //    if (medicalCondition != null)
        //        return Request.CreateResponse(HttpStatusCode.OK, medicalCondition);
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Medical Condition not found for provided id");
        //}
        //
        //// POST: /api/MedicalCondition/Create
        //public async Task<HttpResponseMessage> Post([FromBody] DataModel.MedicalCondition medicalCondition)
        //{
        //    //await medicalConditionService.Create(medicalCondition);
        //    return Request.CreateResponse(HttpStatusCode.Created, medicalCondition);
        //}
        //
        //
        //// POST: /api/MedicalCondition/Delete/514b46581cbfe31ad86ec630
        //public HttpResponseMessage Delete(string id)
        //{
        //    try
        //    {
        //        medicalConditionService.Delete(id);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Medical Condition not found or delete failed");
        //    }
        //}
        //
        //// PUT: /api/MedicalCondition/Update
        //public HttpResponseMessage Update([FromBody] DataModel.MedicalCondition medicalCondition)
        //{
        //    try
        //    {
        //        //medicalConditionService.Update(medicalCondition);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Exercise not found or wrong update format");
        //    }
        //}

    }
}
