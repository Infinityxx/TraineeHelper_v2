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
    public class AccountController : Controller
    {
        private UserManager userManager;
        private UpdateManager updateManager;

        public AccountController()
        {
            userManager = new UserManager();
            updateManager = new UpdateManager();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Login Page";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Register Page";

            return View();
        }

        public ActionResult SetUserName()
        {
            ViewBag.Title = "Set UserName Page";

            return View();
        }


        public async Task<ActionResult> PersonalAccount()

        {
            ViewBag.Title = "UpdateAccountView";
            string userId = Request.Cookies["userId"].Value;
           
            UserContext user = await userManager.GetById(userId);

            return View(user);
        }

        [HttpPost]
        [Route("UpdateAccount")]
        public async Task<ActionResult> UpdateAccount(UserContext userContext)
        {
            // TODO save in DB
            
            string userId = Request.Cookies["userId"].Value;
            //var result = await updateManager.UpdateAccountAsync(userId, userContext.Email,
            //    userContext.PhoneVisibility, userContext.UserName, userContext.UserVisibility);
            var result = await updateManager.UpdateAccountAsync(userId, userContext.Email, userContext.PhoneVisibility,
                userContext.UserName, userContext.UserVisibility);

            return RedirectToAction("Index","Home", new { area = "" });
        }
    }

}
