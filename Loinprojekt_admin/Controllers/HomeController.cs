using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loinprojekt_admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
        //funkar
        public ActionResult ShowProfile(int id)
        {
            
            UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();

            return View(client.GetUserByUserId(id));
        }
        //funkar
        public ActionResult ActiveUsers()
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            
            return View(client.GetActiveUsers());
        }
        //funkar
        public ActionResult Moderators()
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            return View(client.GetModerators());
        }
        //funkar
        public ActionResult FlaggedErrands()
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            return View(client.GetFlaggedUsers());
        } 
        //funkar
        public ActionResult BlockedUsers()
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            return View(client.GetBlockedUsers());
        }
     
        //funkar
        public ActionResult Delete(int id)
        {
            UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
            return View(client.GetUserByUserId(id));
        }
        //funkar
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            client.DeleteUser(id);

            return RedirectToAction("ActiveUsers");
        }
        //funkar
        public ActionResult AddPermission(int id)
        {
           LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            client.AssignModeratorRole(id);

            return RedirectToAction("ActiveUsers");
        }
        //funkar
        public ActionResult DeletePermission(int id)
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            client.AssignUserRole(id);

            return RedirectToAction("Moderators");
        }
        //funkar
        public ActionResult Unflag(int id)
        {
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

            client.UnflagUser(id);
            return RedirectToAction("ActiveUsers");
        }


        //KVAR ATT GÖRA:

        /* public ActionResult Block(int id)
         {
             LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
             client.BlockUser(id);
             return RedirectToAction("ActiveUsers");
         }*/


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
    }
}