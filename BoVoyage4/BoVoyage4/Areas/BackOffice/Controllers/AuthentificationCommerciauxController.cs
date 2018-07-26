using BoVoyage4.Areas.BackOffice.Models;
using BoVoyage4.Data;
using BoVoyage4.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class AuthentificationCommerciauxController : Controller
    {
        private BoVoyage4DbContext db = new BoVoyage4DbContext();
        // GET: BackOffice/Authentication
        public ActionResult Login()
        {
            return View();
        }

        // POST: BackOffice/Authentication/Login
        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult Login(AuthentificationCommercial model)

        {
            if (ModelState.IsValid)
            {
                var passwordHash = model.Password.HashMD5();
                var commercial = db.Commerciaux.SingleOrDefault(x => x.Email == model.Login && x.Password == passwordHash);
                if (commercial == null)
                {

                    ModelState.AddModelError("", "Utilisateur ou mot de passe incorrect");
                    return View(model);
                }
                else
                {
                    Session.Add("COMMERCIAL_BO", commercial);
                    Session.Add("COMMERCIAL_NAME", commercial.Prenom);
                    TempData["Message"] = "Login complété";
                    return RedirectToAction("Index", "Dashboard", new { area = "BackOffice" });
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