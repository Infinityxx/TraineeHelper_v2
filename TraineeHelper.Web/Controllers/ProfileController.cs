using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TraineeHelper.Logic;
using TraineeHelper.Logic.Converters;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Web.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager userManager;
        private UpdateManager updateManager;
        private RegisterManager registerManager;

        public ProfileController()
        {
            userManager = new UserManager();
            updateManager = new UpdateManager();
            registerManager = new RegisterManager();
        }

        // GET: Profile
        public async Task<ActionResult> PersonalProfile()
        {
            //ViewBag.Title = "UpdateProfileView";
            string userId = Request.Cookies["userId"].Value;

            var user = await userManager.GetById(userId);

            switch (user.UserType)
            {
                case "Trainer":
                    TrainerManager trainerManager = new TrainerManager();
                    var trainer = await trainerManager.GetById(userId);
                    TempData["trainerProfileData"] = trainer;
                    return RedirectToAction("PersonalTrainerProfile");
                case "Trainee":
                    TraineeManager traineeManager = new TraineeManager();
                    var trainee = await traineeManager.GetById(userId);
                    TempData["traineeProfileData"] = trainee;
                    return RedirectToAction("PersonalTraineeProfile");
                case "Gym":
                    GymManager gymManager = new GymManager();
                    var gym = await gymManager.GetById(userId);
                    TempData["gymProfileData"] = gym;
                    return RedirectToAction("PersonalGymProfile");
            }

            return View();
        }

        public ActionResult PersonalTrainerProfile(/*TrainerContext trainer*/)
        {
            ViewBag.Title = "PersonalTrainerProfile";
            var trainerctx = (TrainerContext)TempData["trainerProfileData"];

            ExpertiseManager expertiseManager = new ExpertiseManager();
            List<String> images = userManager.GetUserImagesPath(trainerctx.Id);
            trainerctx.imagesList = images;
            trainerctx.ExpertisesOptions = expertiseManager.GetAllExpertises();
            //List<ExpertiseContext> expertisesOptions = await expertiseManager.GetAllExpertises();
            //trainerctx.ExpertisesOptions = expertisesOptions;

            return View(trainerctx);
        }

        public async Task<ActionResult> UpdateTrainerProfile(TrainerContext trainer, string[] selectedExpertises)
        {
            trainer.Expertise = selectedExpertises;
            var result = await registerManager.SaveTrainer(trainer);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult PersonalTraineeProfile(/*TraineeContext trainee*/)

        {
            ViewBag.Title = "PersonalTraineeProfile";
            var traineectx = (TraineeContext)TempData["traineeProfileData"];
            
            return View(traineectx);
        }

        public async Task<ActionResult> UpdateTraineeProfile(TraineeContext trainee)
        {
            var result = await registerManager.SaveTrainee(trainee);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult PersonalGymProfile(/*GymContext gym*/)
        {
            ViewBag.Title = "PersonalGymProfile";
            var gymctx = (GymContext)TempData["gymProfileData"];
            ExpertiseManager expertiseManager = new ExpertiseManager();

            gymctx.ExpertisesOptions = expertiseManager.GetAllExpertises();

            return View(gymctx);
        }

        public async Task<ActionResult> UpdateGymProfile(GymContext gym, string[] selectedExpertises)
        {
            gym.Expertise = selectedExpertises;
            var result = await registerManager.SaveGym(gym);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<ActionResult> MedicalCondition()
        {
            ViewBag.Title = "MedicalCondition";
            TraineeManager traineeManager = new TraineeManager();
            var trainee = await traineeManager.GetById(Request.Cookies["userId"].Value);
            
            return View(trainee.MedicalCondition);
        }

        public async Task<ActionResult> UpdateMedicalCondition(MedicalConditionContext medicalCondition)
        {
            string userId = Request.Cookies["userId"].Value;
            TraineeManager traineeManager = new TraineeManager();
            await traineeManager.UpdateMedicalCondition(userId, medicalCondition);
            return RedirectToAction("PersonalProfile", "Profile");
        }

        /// <summary>
        /// Returns active connections
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Connections()
        {
            ConnectionManager connectionManager = new ConnectionManager();
            
            var result = await connectionManager.FindUserActiveConnections(Request.Cookies["userId"].Value);
            
            return View(result);
        }

        public async Task<ActionResult> Requests()
        {
            ConnectionManager connectionManager = new ConnectionManager();
            var result = await connectionManager.FindUserRequests(Request.Cookies["userId"].Value);

            return View(result);
        }

        /// <summary>
        /// Returns TrainingPlan requests
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TrainingPlans()
        {
            TrainingPlanManager tpManager = new TrainingPlanManager();
            var trainingPlans = await tpManager.FindUserTrainingPlans(Request.Cookies["userId"].Value);
            return View(trainingPlans);
        }
    }
}