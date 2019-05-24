﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loinprojekt_admin.Controllers
{
     [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;

            return View();
        }
        //funkar
        // Metod för att visa en profil, tar en användares Id som inparameter för att kunna visa denna specifika profil
        public ActionResult ShowProfile(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();

                if (client.GetUserByUserId(id) != null)
                {
                    // Vy som använder sig av en metod från webservicen för att hitta en specifik användare
                    return View(client.GetUserByUserId(id));

                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "No profile to show here");
                    return View();

                }
            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }
        //funkar
        // Metod för att visa aktiva användare
        public ActionResult ActiveUsers()
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.GetActiveUsers() != null)
                {
                    // Anrop till servicens metod för att visa alla aktiva användare
                    return View(client.GetActiveUsers());

                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "It seems like there are no active users yet");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }

        //funkar
        // Metod för att visa alla moderatorer
        public ActionResult Moderators()
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.GetModerators() != null)
                {

                    // Anrop till webservicens metod för att visa alla moderatorer
                    return View(client.GetModerators());

                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "It seems like there are no assigned moderators yet");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }

        //funkar
        // Metod för att visa alla flaggade ärenden
        public ActionResult FlaggedErrands()
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.GetFlaggedUsers() != null)
                {
                    // Anrop till webservicens metod för att visa alla flaggade ärenden
                    return View(client.GetFlaggedUsers());

                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "No flagged users here, good for us!");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        } 

        //funkar
        // Metod för att visa alla blockade användare
        public ActionResult BlockedUsers()
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.GetBlockedUsers() != null)
                {
                    // Anrop till webservicens metod för att visa alla blockade användare
                    return View(client.GetBlockedUsers());
                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "No blocked users, good for us!");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
           
        }
     
        //funkar
        // Metod för att radera en användare, tar den specifika användarens Id som inparamteter
        public ActionResult Delete(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
                if (client.GetUserByUserId(id) != null)
                {
                    // Anrop till webservicens metod för att hitta en specifik användare och visa upp en vy utifrån detta
                    return View(client.GetUserByUserId(id));
                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "Cant find this user");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }

        }

        //funkar
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        /*
         Metod för att bekräfta radering av en användare, här utförs således den faktiska raderingen.
         Metoden tar den specifika användarens Id som inparamter.
         */
        public ActionResult ConfirmDelete(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();

                // Anrop till webservicen
                // Anrop till webservicens metod för att radera en användare, där vi skickar me med den specifika användarens Id
                client.DeleteUser(id);
                // När raderingen slutförs, återvänd till sidan som visar alla aktiva användare
                ModelState.AddModelError("Felmeddelande", "Konto raderat!");
                return RedirectToAction("ActiveUsers");

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }
        //funkar
        /*
        Metod för att lägga till moderatorsbehörigheter.
        Metodens inparameter är det Id som tillhör den specifika användare som ska tilldelas moderatorsbehörigheter.
        */
        public ActionResult AddPermission(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;

            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.AssignModeratorRole(id) == true)
                    // Anrop till webservicens metod för att tilldela moderatorsbehörigheter, där vi skickar med den specifika användarens Id
                    client.AssignModeratorRole(id);
                    // När behörigheten är tilldelad, återvänd till sidan som visar alla aktiva användare
                    return RedirectToAction("ActiveUsers");
               

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }


        }
        //funkar
        /*
        Metod för att radera moderatorsbehörigheter.
        Metodens inparameter är ID:t som tillhör den specifika användare vars behörighet ska raderas.
        */
        public ActionResult DeletePermission(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.AssignUserRole(id) == true)
                {
                    // Här ropas på webserivcens metod för att lägga till behörigheter, är det rätt?
                    client.AssignUserRole(id);
                    // När behörigheten är raderad, återvänd till sidan som visar alla moderatorer
                    return RedirectToAction("Moderators");
                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "The user is already a normal user");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }
        //funkar
        // Metod för att ta bort flaggan från en användare, tar den specifika användarens Id som inparameter
        public ActionResult Unflag(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.AssignUserRole(id) == true)
                {
                    // Anrop till webservicens metod för att ta bort flaggan från en användare, här skickar vi med Id:t för den specifika användaren
                    client.UnflagUser(id);
                    // När operationen är klar, återvänd till sidan som visar alla aktiva användare
                    return RedirectToAction("ActiveUsers");
                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "The user cant be unflagged");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }

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
              Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();
                if (client.GetAdmins() != null)
                {

                    // Anrop till webserivcens metod för att hämta alla admins
                    return View(client.GetAdmins());
                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "It seems like there are no admins yet");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }

        //funkar
        // Metod för att radera en användare, tar den specifika användarens Id som inparamteter
        public ActionResult BlockUser(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];
            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;

            try
            {
                // Anrop till webservicen
                UserService.UserProfileServiceClient client = new UserService.UserProfileServiceClient();
                if (client.GetUserByUserId(id) != null)
                {
                    Models.Userobjekt userobjekt = new Models.Userobjekt();
                    userobjekt.ID = client.GetUserByUserId(id).Id;
                    userobjekt.name = client.GetUserByUserId(id).Name;
                    userobjekt.surname = client.GetUserByUserId(id).Surname;
                    userobjekt.city = client.GetUserByUserId(id).City;
                    userobjekt.adress = client.GetUserByUserId(id).Address;
                    userobjekt.phonenumber = client.GetUserByUserId(id).Phonenumber;
                    userobjekt.username = client.GetUserByUserId(id).Username;
                    userobjekt.email = client.GetUserByUserId(id).Email;
                    userobjekt.picture = client.GetUserByUserId(id).Picture;

                    Models.ViewModel viewModel = new Models.ViewModel();
                    viewModel.userID = userobjekt.ID;
                    viewModel.userObjekt = userobjekt;

                    // Anrop till webservicens metod för att hitta en specifik användare och visa upp en vy utifrån detta
                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "Cant find this user");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }

        }

        //funkar
        [HttpPost, ActionName("BlockUser")]
        [ValidateAntiForgeryToken]
        /*
         Metod för att bekräfta radering av en användare, här utförs således den faktiska raderingen.
         Metoden tar den specifika användarens Id som inparamter.
         */
        public ActionResult ConfirmBlock(Models.ViewModel viewModel)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;

            try
            {
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

                // Anrop till webservicen
                // Anrop till webservicens metod för att radera en användare, där vi skickar me med den specifika användarens Id
               client.BlockUser(viewModel.userID, sessionObjekt.ID, viewModel.reason, viewModel.dateTo );
                // När raderingen slutförs, återvänd till sidan som visar alla aktiva användare
                ModelState.AddModelError("Felmeddelande", "Konto raderat!");
                return RedirectToAction("ActiveUsers");

            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }
        }

        public ActionResult AdminProfile(int id)
        {
            Models.AdminModel sessionObjekt = (Models.AdminModel)Session["admin"];

            ViewBag.Username = "Inloggad som: " + sessionObjekt.username;
            try
            {
                // Anrop till webservicen
                LoginService.LoginServiceClient client = new LoginService.LoginServiceClient();

                if (client.GetAdminById(id) != null)
                {
                    // Vy som använder sig av en metod från webservicen för att hitta en specifik användare
                    return View(client.GetAdminById(id));

                }
                else
                {
                    ModelState.AddModelError("Felmeddelande", "No profile to show here");
                    return View();

                }
            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "Home", "ActiveUsers"));
            }

            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }
    }
}