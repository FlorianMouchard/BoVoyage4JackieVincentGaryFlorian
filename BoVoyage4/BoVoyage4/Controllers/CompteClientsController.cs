using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyage4.Data;
using BoVoyage4.Models;
using BoVoyage4.Utils;

namespace BoVoyage4.Controllers
{
    public class CompteClientsController : BaseController
    {
        /// <summary>
        /// Permet l'affichage des informations liées aux comptes clients dans la base de données
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        /// <summary>
        /// Permet d'afficher la page de création d'un compte client
        /// </summary>
        /// <returns></returns>
        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.Civilites = db.Civilites.ToList();
            return View();
        }
        /// <summary>
        /// Permet de créer un compte client en base de données
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Historique,Password,PasswordConfirmation,CiviliteID,Nom,Prenom,Adresse,Telephone,DateNaissance")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                client.Password = client.Password.HashMD5();
                db.Clients.Add(client);
                db.SaveChanges();
                DisplayMessage($"Le client {client.Prenom} {client.Nom} s'est enregistré.", MessageType.SUCCESS);
                return RedirectToAction("Index", "Home");
            }
            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            ViewBag.Civilites = db.Civilites.ToList();
            return View(client);
        }
        /// <summary>
        /// Permet d'afficher la page modifier un compte client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        /// <summary>
        /// Permet de modifier un compte client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Historique,Password,PasswordConfirmation,CiviliteID,Nom,Prenom,Adresse,Telephone,DateNaissance")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                DisplayMessage($"Les données du client {client.Prenom} {client.Nom} ont été modifiées.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }
            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            return View(client);
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
