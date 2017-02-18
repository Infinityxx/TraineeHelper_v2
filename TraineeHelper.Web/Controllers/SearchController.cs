using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;
using TraineeHelper.Web.Models;

namespace TraineeHelper.Web.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Search";
            UpdateAccountBindingModel updateModel = new UpdateAccountBindingModel();
            return View(updateModel);
        }

        public ActionResult SearchMatches()
        {
            ExpertiseManager expertiseMng = new ExpertiseManager();
            List<ExpertiseContext> expertises = expertiseMng.GetAllExpertises();

            ViewBag.Title = "SearchMatches";
            return View(expertises);
        }
    }
}