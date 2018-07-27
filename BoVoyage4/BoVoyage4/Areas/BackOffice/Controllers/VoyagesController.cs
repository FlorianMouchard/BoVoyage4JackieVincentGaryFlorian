using System;
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
        // GET: BackOffice/Voyages      
       
        public ActionResult Index(RechercheVoyageViewModel model)
        {          

            IEnumerable<Voyage> voyages = db.Voyages.Include(x => x.Destination).Include(x => x.AgenceVoyage);
            if (model.DateMin.HasValue)
                voyages = db.Voyages.Include(x=> x.Destination).Include(x => x.AgenceVoyage).Where(x => x.DateAller >= model.DateMin);
            if (model.DateMax.HasValue)
                voyages = db.Voyages.Include(x => x.Destination).Include(x => x.AgenceVoyage).Where(x => x.DateAller <= model.DateMax);
            if (model.PrixMin != null)
                voyages = db.Voyages.Include(x => x.Destination).Include(x => x.AgenceVoyage).Where(x => x.TarifToutCompris >= model.PrixMin);
            if (model.PrixMax != null)
                voyages = db.Voyages.Include(x => x.Destination).Include(x => x.AgenceVoyage).Where(x => x.TarifToutCompris <= model.PrixMax);

            model.Voyages = voyages.ToList();
            return View(model);
        }

        // GET: BackOffice/Voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // GET: BackOffice/Voyages/Create
        public ActionResult Create()
        {
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Region");
            ViewBag.AgenceVoyageID = new SelectList(db.AgenceVoyages, "ID", "Nom");
            return View();
        }

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

        // GET: BackOffice/Voyages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
            ViewBag.AgenceVoyageID = new SelectList(db.AgenceVoyages, "ID", "Nom", voyage.AgenceVoyageID);
            return View(voyage);
        }

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

        // GET: BackOffice/Voyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

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
