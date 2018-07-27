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

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class AgenceVoyagesController : BaseBoController
    {
        /// <summary>
        /// Retourne la liste des Agences
        /// </summary>
        /// <returns></returns>
        // GET: BackOffice/AgenceVoyages
        public ActionResult Index()
        {
            return View(db.AgenceVoyages.ToList());
        }


        /// <summary>
        /// Permet d'afficher les informations liées aux Agences de voyages
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/AgenceVoyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenceVoyage agenceVoyage = db.AgenceVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return HttpNotFound();
            }
            return View(agenceVoyage);
        }


        /// <summary>
        /// Permet d'afficher la page créer une Agence de voyage
        /// </summary>
        /// <returns></returns>
        // GET: BackOffice/AgenceVoyages/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Permet l'envoi de données en base avec un formulaire "création Agence de Voyages" (création d'une agence)
        /// </summary>
        /// <param name="agenceVoyage"></param>
        /// <returns></returns>
        // POST: BackOffice/AgenceVoyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nom")] AgenceVoyage agenceVoyage)
        {
            if (ModelState.IsValid)
            {
                db.AgenceVoyages.Add(agenceVoyage);
                db.SaveChanges();
                DisplayMessage($"Agence de voyage {agenceVoyage.Nom} enregistré.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }
            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            return View(agenceVoyage);
        }
        /// <summary>
        /// Permet d'afficher la page modifier une agence de voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/AgenceVoyages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenceVoyage agenceVoyage = db.AgenceVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return HttpNotFound();
            }
            return View(agenceVoyage);
        }
        /// <summary>
        /// permet l'envoi de données en base et de modifier une agence de voyage
        /// </summary>
        /// <param name="agenceVoyage"></param>
        /// <returns></returns>
        // POST: BackOffice/AgenceVoyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nom")] AgenceVoyage agenceVoyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agenceVoyage).State = EntityState.Modified;
                db.SaveChanges();
                DisplayMessage($"Les données de l'agence de voyage {agenceVoyage.Nom} ont été modifiées.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }
            DisplayMessage("Une erreur est apparue", MessageType.ERROR);
            return View(agenceVoyage);
        }
        /// <summary>
        /// Permet d'afficher la page supprimer une agence de voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/AgenceVoyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenceVoyage agenceVoyage = db.AgenceVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return HttpNotFound();
            }
            return View(agenceVoyage);
        }
        
        /// <summary>
        /// Permet l'envoi de données en base et de supprimer une agence de voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: BackOffice/AgenceVoyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {           
            AgenceVoyage agenceVoyage = db.AgenceVoyages.Find(id);
            db.AgenceVoyages.Remove(agenceVoyage);
            db.SaveChanges();
            DisplayMessage($"L'agence de voyage {agenceVoyage.Nom} a été supprimée.", MessageType.SUCCESS);
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
