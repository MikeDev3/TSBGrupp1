using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loinprojekt_admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }
// Post-metod för login-sidan, tar användarnamn och lösenord som inparameter
        [HttpPost]
        public ActionResult LoginPage(string username, string password)
        {
// Anrop till webservicen
            LoginService.LoginServiceClient klient = new LoginService.LoginServiceClient();
/*
 Variabel för att kolla om inloggningen är godkänd.
 Variabelns värde är lika med ett anrop på Login-metoden i webservicen och vi skickar dessutom med användarnamn och lösenord vid anropet.
 */
            bool response = klient.AdminLogin(username, password);

            // Om inloggningen är godkänd, dvs att responce-variabeln är true
            if (response)

            {
                /*
                 Här skapas ett objekt som tilldelas värdena av en användares Id, Email och användarnamn.
                 Dessa data hämtas från webservicen.
                 */
                Models.AdminModel sessionObjekt = new Models.AdminModel();
                sessionObjekt.email = klient.GetAdminByUsername(username).Email;
                sessionObjekt.ID = klient.GetAdminByUsername(username).ID;
                sessionObjekt.username = klient.GetAdminByUsername(username).Username;
                             // De olika värdena lagras i en array
                Session["admin"] = sessionObjekt;

              //  ViewBag.Username = "Inloggad som: " + sessionObjekt.username;

                // Flytta användaren från login-sidan
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(username, false);
                // När inloggningen är klar hamnar användaren på förstasidan, nu som inloggad admin
                return RedirectToAction("Index", "Home");

            }
            else
            {

                // Om inloggningen inte gick så bra visas ett felmeddelande

                ModelState.AddModelError("Felmeddelande", "Inloggningen misslyckades");

                return View();
            }
                  
                // Session["Username"] = "Inloggad som - " + data;
        }
        // Metod för att logga ut en användare
    public ActionResult LogOut()
        {
        // Anrop på en färdig metod för att logga ut
    System.Web.Security.FormsAuthentication.SignOut();
    // När utloggningen är klar, skicka användaren till login-sidan
    return RedirectToAction("LoginPage");
        }
    }
}