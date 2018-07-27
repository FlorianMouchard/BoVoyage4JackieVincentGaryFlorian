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
        /// <summary>
        /// Permet d'afficher la page de connexion pour un commercial
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Permet de se connecter au backoffice en tant que commercial
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                    DisplayMessage("Login complété", MessageType.SUCCESS);
                    //TempData["Message"] = ;
                    return RedirectToAction("Index", "Dashboard", new { area = "BackOffice" });
                }
            }
            return View(model);

        }

        /// <summary>
        /// Permet de se deconnecter du backoffice
        /// </summary>
        /// <returns></returns>
        [AuthentificationFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            DisplayMessage("Vous vous êtes déconnecté", MessageType.SUCCESS);            
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        protected void DisplayMessage(string message, MessageType type)
        {
            TempData["Message"] = message;
            switch (type)
            {
                case MessageType.SUCCESS:
                    TempData["MessageType"] = "success";
                    break;
                case MessageType.WARNING:
                    TempData["MessageType"] = "warning";
                    break;
                case MessageType.ERROR:
                    TempData["MessageType"] = "danger";
                    break;

            }
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