﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyage4.Areas.BackOffice.Models;
using BoVoyage4.Data;
using BoVoyage4.Models;
using BoVoyage4.Utils;

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class ClientsController : BaseBoController
    {       
      
        //GET: BackOffice/Clients
        public ActionResult Index(RechercheClientViewModel model)
        {
            IEnumerable<Client> clients = db.Clients.Include(x => x.Civilite);
            if (!string.IsNullOrWhiteSpace(model.Nom))
                clients = db.Clients.Where(x => x.Nom.Contains(model.Nom));
            if (!string.IsNullOrWhiteSpace(model.Prenom))
                clients = db.Clients.Where(x => x.Nom.Contains(model.Prenom));
            if (model.NeAvantLe.HasValue)
                clients = db.Clients.Where(x => x.DateNaissance <= model.NeAvantLe);
            if (model.NeApresLe.HasValue)
                clients = db.Clients.Where(x => x.DateNaissance >= model.NeApresLe);

            model.Clients = clients.ToList();
            return View(model);
        }

        // GET: BackOffice/Clients/Details/5
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

        

        // GET: BackOffice/Clients/Edit/5
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
            ViewBag.CiviliteID = new SelectList(db.Civilites, "ID", "Libelle", client.CiviliteID);
            return View(client);
        }

        // POST: BackOffice/Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,CiviliteID,Nom,Prenom,Adresse,Telephone,DateNaissance")] Client client)
        {
            ModelState.Remove("Password");
            ModelState.Remove("PasswordConfirmation");
            ModelState.Remove("Email");
            var old = db.Clients.SingleOrDefault(x => x.ID == client.ID);
            client.Password = old.Password.HashMD5();
            client.PasswordConfirmation = old.Password.HashMD5();
            client.Email = old.Email;
            db.Entry(old).State = EntityState.Detached;
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                DisplayMessage($"Les données du client {client.Nom} {client.Prenom} ont été modifiées.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }
            ViewBag.CiviliteID = new SelectList(db.Civilites, "ID", "Libelle", client.CiviliteID);
            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            return View(client);
        }

        // GET: BackOffice/Clients/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: BackOffice/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            DisplayMessage($"Le client {client.Nom} {client.Prenom} a été supprimé.", MessageType.SUCCESS);
            return RedirectToAction("Index");
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
