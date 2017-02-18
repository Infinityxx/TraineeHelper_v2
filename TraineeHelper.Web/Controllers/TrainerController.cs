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
    public class TrainerController : Controller
    {
        TrainerManager trainerManager;

        public TrainerController()
        {
            trainerManager = new TrainerManager();
        }
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ViewTrainer(string trainerId)
        {
            ViewBag.Title = "ViewTrainer";
            ExpertiseManager expertisesManager = new ExpertiseManager();
            
            var trainerProfile = await trainerManager.ViewTrainerProfile(trainerId);
            List<string> expertisesIDs = new List<string>();
            foreach(string s in trainerProfile.Expertise)
            {
                expertisesIDs.Add(s);
            }
            var expertises = await expertisesManager.FindExpertisesByIds(expertisesIDs);
            ConnectionManager connectionManager = new ConnectionManager();
            bool connectionExists = await connectionManager.CheckConnectionExistence(Request.Cookies["userId"].Value, trainerId);
            ViewBag.AllowFriendshipRequest = !connectionExists;
            TempData["ViewerRating"] = 0;
            for(int i = 0; i < trainerProfile.Expertise.Length; i++)
            {
                trainerProfile.Expertise[i] = expertises[i].ExpertiseName;
            }
                                                        
            return View(trainerProfile);
            //return View(new UserTrainerProfileDataContext { UserName = "xxx" });
        }

        public ActionResult CreateTrainingPlanView()
        {
            ViewBag.Title = "CreateTrainingPlanView";

            return View();
        }

        public ActionResult CreateExercisesView()
        {
            ViewBag.Title = "CreateExercisesView";

            ExerciseContext exercise = new ExerciseContext();
            return View(exercise);
        }

        public ActionResult CreateChallengesForTraineeView()
        {
            ViewBag.Title = "CreateAchievementsForTraineeView";

            ChallengeContext challenge = new ChallengeContext();

            return View(challenge);
        }
    }
}