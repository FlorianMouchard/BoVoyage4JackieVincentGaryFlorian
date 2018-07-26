using BoVoyage4.Areas.BackOffice.Controllers;
using BoVoyage4.Filters;
using BoVoyage4.Models;
using BoVoyage4.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Controllers
{
    public class AuthentificationClientsController : BaseController
    {
      
        public ActionResult Login()
        {
            return View();
        }

        
        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult Login(AuthentificationClientViewModels model)

        {
            if (ModelState.IsValid)
            {
                var passwordHash = model.Password.HashMD5();
                var client = db.Clients.SingleOrDefault(x => x.Email == model.Login && x.Password == passwordHash);
                if (client == null)
                {

                    ModelState.AddModelError("", "Utilisateur ou mot de passe incorrect");
                    return View(model);
                }
                else
                {
                    Session.Add("CLIENT_BO", client);
                    Session.Add("CLIENT_NAME", client.Prenom);
                    TempData["Message"] = "Login complété";
                    return RedirectToAction("Index", "Dashboard", new { area = "" });
                }
            }
            return View(model);

        }

        // Get: BackOffice/Authentication/logout
        [AuthentificationFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["Message"] = "Vous vous êtes déconnecté";
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}