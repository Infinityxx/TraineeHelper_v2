using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TraineeHelper.Logic;

namespace TraineeHelper.Web.Controllers
{
    public class GymController : Controller
    {
        private GymManager gymManager;

        public GymController()
        {
            gymManager = new GymManager();
        }
        // GET: Gym
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ViewGym(string gymId)
        {
            ViewBag.Title = "ViewGym";

            ExpertiseManager expertisesManager = new ExpertiseManager();

            ViewBag.AllowFriendshipRequest = false;
            var gymProfile = await gymManager.ViewGymProfile(gymId);

            List<string> expertisesIDs = new List<string>();
            foreach (string s in gymProfile.Expertise)
            {
                expertisesIDs.Add(s);
            }
            var expertises = await expertisesManager.FindExpertisesByIds(expertisesIDs);
            ConnectionManager connectionManager = new ConnectionManager();
            bool connectionExists = await connectionManager.CheckConnectionExistence(Request.Cookies["userId"].Value, gymId);
            ViewBag.AllowFriendshipRequest = !connectionExists;
            for (int i = 0; i < gymProfile.Expertise.Length; i++)
            {
                gymProfile.Expertise[i] = expertises[i].ExpertiseName;
            }

            return View(gymProfile);
            //return View(new UserTrainerProfileDataContext { UserName = "xxx" });
        }

    }
}