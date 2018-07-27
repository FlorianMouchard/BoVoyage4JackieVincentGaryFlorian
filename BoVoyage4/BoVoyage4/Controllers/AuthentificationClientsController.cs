﻿using BoVoyage4.Areas.BackOffice.Controllers;
using BoVoyage4.Data;
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
    public class AuthentificationClientsController : Controller
    {
        protected BoVoyage4DbContext db = new BoVoyage4DbContext();
        /// <summary>
        /// Permet l'affichage de la page de connexion côté client
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Permet de se connecter en tant que client à l'application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                    Session.Add("CLIENT_NAME", client.Prenom);
                    DisplayMessage("Login complété", MessageType.SUCCESS);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);

        }
      /// <summary>
      /// Permet de se déconnecter de l'application en tant que client et de revenir à l'accueil
      /// </summary>
      /// <returns></returns>
        [AuthentificationFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            TempData["Message"] = "Vous vous êtes déconnecté";
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