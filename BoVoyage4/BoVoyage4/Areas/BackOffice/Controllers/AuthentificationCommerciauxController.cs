using BoVoyage4.Areas.BackOffice.Models;
using BoVoyage4.Data;
using BoVoyage4.Filters;
using BoVoyage4.Models;
using BoVoyage4.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class AuthentificationCommerciauxController : Controller
    {
        protected BoVoyage4DbContext db = new BoVoyage4DbContext();
        public ActionResult Login()
        {
            return View();
        }

        
        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult Login(AuthentificationCommercialViewModels model)

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

        
        [AuthentificationFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["Message"] = "Vous vous êtes déconnecté";
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}