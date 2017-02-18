using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TraineeHelper.Logic;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Web.Controllers
{
    public class TraineeViewController : Controller
    {
        private TraineeManager traineeManager;
        private UserManager userManager;

        public TraineeViewController()
        {
            traineeManager = new TraineeManager();
            userManager = new UserManager();
        }
        // GET: TraineeView
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ViewTrainee(string traineeId)
        {
            ViewBag.Title = "ViewTrainee";
            ViewBag.AllowFriendshipRequest = false;
            var traineeProfile = await traineeManager.ViewTraineeProfile(traineeId);
            var viewerUserType = (await userManager.GetById(Request.Cookies["userId"].Value)).UserType;
            HttpCookie watchedUserCookie = new HttpCookie("watchedUserId");
            watchedUserCookie.Value = traineeProfile.UserId;
            Response.Cookies.Add(watchedUserCookie);
            //Request.Cookies["watchedUserId"].Value = traineeProfile.UserId;
            ConnectionManager connectionManager = new ConnectionManager();
            bool connectionExists = await connectionManager.CheckConnectionExistence(Request.Cookies["userId"].Value, traineeId);
            ViewBag.AllowFriendshipRequest = !connectionExists;
            
            //var connection = await connectionManager.FindConnectionById(Request.Cookies["userId"].Value);
            //if(connection.ConnectionType != Common.ConnectionType.Mentor)
            //{
            ViewBag.ViewerUserType = viewerUserType;
            if (connectionExists && viewerUserType == "Trainer")
            {
                ViewBag.AllowMentorOptions = false;
                var connectionStatus = await connectionManager.GetConnectionStatus(Request.Cookies["userId"].Value, traineeId);
                if (connectionStatus == Common.ConnectionStatus.ACCEPTED)
                    ViewBag.AllowMentorOptions = true;
            }
            else
            {
                ViewBag.AllowMentorOptions = false;
            }
                
            //}
            //else
            //{
            //    ViewBag.AllowMentorOptions = connectionExists;
            //}
            

            return View(traineeProfile);
        }


        public async Task<ActionResult> PersonalProfile()
        {
            //ViewBag.Title = "PersonaProfileView";
            string userId = Request.Cookies["userId"].Value;

            TraineeContext trainee = await traineeManager.GetById(userId);

            return View(trainee);
        }

        public async Task<ActionResult> ViewTraineeMedicalCondition(string traineeId)
        {
            ViewBag.Title = "ViewTraineeMedicalCondition";
            //string userId = Request.Cookies["userId"].Value;
            TraineeContext trainee = await traineeManager.GetById(traineeId);
            ViewBag.traineeId = traineeId;

            return View(trainee.MedicalCondition);
        }

        public async Task<ActionResult> ViewTraineeTrainingPlans(string traineeId)
        {
            ViewBag.Title = "ViewTraineeTrainingPlans";
            TrainingPlanManager tpManager = new TrainingPlanManager();
            var trainingPlans = await tpManager.FindUserTrainingPlans(traineeId);
            return View(trainingPlans);
        }
        
        public async Task<ActionResult> ViewTrainingPlanDetails(string TrainingPlanId)
        {
            ViewBag.Title = "ViewTrainingPlanDetails";
            TrainingPlanManager tpManager = new TrainingPlanManager();
            var trainingPlan = await tpManager.FindTrainingPlanById(TrainingPlanId);

            return View(trainingPlan.Exercises);
        }
    }
}