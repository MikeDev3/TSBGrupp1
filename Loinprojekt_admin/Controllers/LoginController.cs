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

        [HttpPost]
        public ActionResult LoginPage(string username, string password)
        {
            LoginService.LoginServiceClient klient = new LoginService.LoginServiceClient();
            bool response = klient.AdminLogin(username, password);
            
                        if (response)
                        {
                           Models.AdminModel sessionObjekt = new Models.AdminModel();
                           sessionObjekt.email = klient.GetAdminByUsername(username).Email;
                             sessionObjekt.ID = klient.GetAdminByUsername(username).ID;
                             sessionObjekt.username = klient.GetAdminByUsername(username).Username;

                              Session["admin"] = sessionObjekt;
                            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(username, false);
                            return RedirectToAction("Index", "Home");
                        }
                       
                        else
                        {

                            ModelState.AddModelError("Felmeddelande", "Inloggningen misslyckades");
                            return View();

                        }
                  
                // Session["Username"] = "Inloggad som - " + data;
        }
        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("LoginPage");
        }
    }
}