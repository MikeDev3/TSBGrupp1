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
        // Metod för att visa en profil, tar en användares Id som inparameter för att kunna visa denna specifika profil
        public ActionResult ShowProfile(int id)
        {
            // Anrop till webservicen
            UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
            // Vy som använder sig av en metod från webservicen för att hitta en specifik användare
            return View(client.GetUserByUserId(id));
        }
        //funkar
        // Metod för att visa aktiva användare
        public ActionResult ActiveUsers()
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till servicens metod för att visa alla aktiva användare
            return View(client.GetActiveUsers());
        }
        //funkar
        // Metod för att visa alla moderatorer
        public ActionResult Moderators()
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
// Anrop till webservicens metod för att visa alla moderatorer
            return View(client.GetModerators());
        }
        //funkar
        // Metod för att visa alla flaggade ärenden
        public ActionResult FlaggedErrands()
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till webservicens metod för att visa alla flaggade ärenden
            return View(client.GetFlaggedUsers());
        } 
        //funkar
        // Metod för att visa alla blockade användare
        public ActionResult BlockedUsers()
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till webservicens metod för att visa alla blockade användare
            return View(client.GetBlockedUsers());
        }
     
        //funkar
        // Metod för att radera en användare, tar den specifika användarens Id som inparamteter
        public ActionResult Delete(int id)
        {
            // Anrop till webservicen
            UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
            // Anrop till webservicens metod för att hitta en specifik användare och visa upp en vy utifrån detta
            return View(client.GetUserByUserId(id));
        }
        //funkar
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        /*
         Metod för att bekräfta radering av en användare, här utförs således den faktiska raderingen.
         Metoden tar den specifika användarens Id som inparamter.
         */
        public ActionResult ConfirmDelete(int id)
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till webservicens metod för att radera en användare, där vi skickar me med den specifika användarens Id
            client.DeleteUser(id);
            // När raderingen slutförs, återvänd till sidan som visar alla aktiva användare
            return RedirectToAction("ActiveUsers");
        }
        //funkar
        /*
        Metod för att lägga till moderatorsbehörigheter.
        Metodens inparameter är det Id som tillhör den specifika användare som ska tilldelas moderatorsbehörigheter.
        */
        public ActionResult AddPermission(int id)
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till webservicens metod för att tilldela moderatorsbehörigheter, där vi skickar med den specifika användarens Id
            client.AssignModeratorRole(id);

           // När behörigheten är tilldelad, återvänd till sidan som visar alla aktiva användare
            return RedirectToAction("ActiveUsers");
        }
        //funkar
        /*
        Metod för att radera moderatorsbehörigheter.
        Metodens inparameter är ID:t som tillhör den specifika användare vars behörighet ska raderas.
        */
        public ActionResult DeletePermission(int id)
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Här ropas på webserivcens metod för att lägga till behörigheter, är det rätt?
            client.AssignUserRole(id);
            // När behörigheten är raderad, återvänd till sidan som visar alla moderatorer
            return RedirectToAction("Moderators");
        }
        //funkar
        // Metod för att ta bort flaggan från en användare, tar den specifika användarens Id som inparameter
        public ActionResult Unflag(int id)
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till webservicens metod för att ta bort flaggan från en användare, här skickar vi med Id:t för den specifika användaren
            client.UnflagUser(id);
           // När operationen är klar, återvänd till sidan som visar alla aktiva användare
            return RedirectToAction("ActiveUsers");
        }


        //KVAR ATT GÖRA:
        // Denna metod är fortfarande bortkommenterad, men den ser ut som att den borde funka?
        /* public ActionResult Block(int id)
         {
             LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
             client.BlockUser(id);
             return RedirectToAction("ActiveUsers");
         }*/

// Metod för kontaktvyn, där man ska kunna kontakta andra admins
        public ActionResult Contact()
        {
            // Anrop till webservicen
            LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
            // Anrop till webserivcens metod för att hämta alla admins
            client.GetAdmins();
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
    }
}