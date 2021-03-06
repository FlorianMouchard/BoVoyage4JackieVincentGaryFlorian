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

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class VoyagesController : BaseBoController
    {
        /// <summary>
        /// Permet de retourner la liste des voyages
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // GET: BackOffice/Voyages      
       
        public ActionResult Index(RechercheVoyageViewModel model)
        {          

            IEnumerable<Voyage> voyages = db.Voyages.Include(x => x.Destination).Include(x => x.AgenceVoyage);
            if (!string.IsNullOrWhiteSpace(model.Destination))
                voyages = voyages.Where(x => x.Destination.Pays.Contains(model.Destination));
            if (model.DateMin.HasValue)
                voyages = voyages.Where(x => x.DateAller >= model.DateMin);
            if (model.DateMax.HasValue)
                voyages = voyages.Where(x => x.DateAller <= model.DateMax);
            if (model.PrixMin != null)
                voyages = voyages.Where(x => x.TarifToutCompris >= model.PrixMin);
            if (model.PrixMax != null)
                voyages = voyages.Where(x => x.TarifToutCompris <= model.PrixMax);

            model.Voyages = voyages.ToList();
            return View(model);
        }
        /// <summary>
        /// Permet l'affichage des informations liées à un voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/Voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include(x => x.AgenceVoyage).Include(y => y.Destination).SingleOrDefault(x => x.ID == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }
        /// <summary>
        /// Permet d'afficher la page créer un voyage
        /// </summary>
        /// <returns></returns>
        // GET: BackOffice/Voyages/Create
        public ActionResult Create()
        {
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Region");
            ViewBag.AgenceVoyageID = new SelectList(db.AgenceVoyages, "ID", "Nom");
            return View();
        }
        /// <summary>
        /// Permet de créer un voyage dans la base de données
        /// </summary>
        /// <param name="voyage"></param>
        /// <returns></returns>
        // POST: BackOffice/Voyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateAller,DateRetour,PlacesDisponibles,TarifToutCompris,DestinationID,AgenceVoyageID")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Voyages.Add(voyage);
                var destination = db.Destinations.Find(voyage.DestinationID);
                db.SaveChanges();
                DisplayMessage($"Le voyage à destination de {destination.Region} à {voyage.TarifToutCompris}€ a été créé.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }

            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Region", voyage.DestinationID);
            ViewBag.AgenceVoyageID = new SelectList(db.AgenceVoyages, "ID", "Nom", voyage.AgenceVoyageID);
            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            return View(voyage);
        }
        /// <summary>
        /// Permet d'afficher la page modifier un voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/Voyages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include(x => x.AgenceVoyage).Include(y => y.Destination).SingleOrDefault(x => x.ID == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
            ViewBag.AgenceVoyageID = new SelectList(db.AgenceVoyages, "ID", "Nom", voyage.AgenceVoyageID);
            return View(voyage);
        }
        /// <summary>
        /// Permet de modifier un voyage en base de données
        /// </summary>
        /// <param name="voyage"></param>
        /// <returns></returns>
        // POST: BackOffice/Voyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateAller,DateRetour,PlacesDisponibles,TarifToutCompris,DestinationID,AgenceVoyageID")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voyage).State = EntityState.Modified;
                db.SaveChanges();
                var destination = db.Destinations.Find(voyage.DestinationID);

                DisplayMessage($"Le voyage à destination de {destination.Region} a été modifié.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
            ViewBag.AgenceVoyageID = new SelectList(db.AgenceVoyages, "ID", "Nom", voyage.AgenceVoyageID);

            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            return View(voyage);
        }
        /// <summary>
        /// Permet d'afficher la page supprimer un voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/Voyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            Voyage voyage = db.Voyages.Include(x => x.AgenceVoyage).Include(y=>y.Destination).SingleOrDefault(x => x.ID == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }
        /// <summary>
        /// Permet de supprimer un voyage de la base de données
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: BackOffice/Voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            db.Voyages.Remove(voyage);
            db.SaveChanges();
            DisplayMessage($"Le voyage à destination de {voyage.Destination.Region} a été supprimé.", MessageType.SUCCESS);
            return RedirectToAction("Index");
        }
       
      

    }
}
