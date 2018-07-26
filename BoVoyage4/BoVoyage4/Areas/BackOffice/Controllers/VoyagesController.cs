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
    public class VoyagesController : BaseBoController
    {
        // GET: BackOffice/Voyages
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include(v => v.Destination);
            return View(voyages.ToList());
        }

        [HttpGet]
        public ActionResult Index(string destination, DateTime? dateMin, DateTime? dateMax, decimal prixMin, decimal prixMax)
        {
            ViewBag.dateMin = dateMin;
            ViewBag.dateMax = dateMax;
            ViewBag.prixMin = prixMin;
            ViewBag.prixMax = prixMax;

            IQueryable < Voyage > voyages = db.Voyages.Include(x => x.Destination);
            if (destination != null)
                voyages = voyages.Where(x => x.Destination.Region == destination);
            if (dateMin.HasValue)
                voyages = voyages.Where(x => x.DateAller >= dateMin.Value);
            if (dateMax.HasValue)
                voyages = voyages.Where(x => x.DateAller <= dateMax.Value);
            if (prixMin != 0)
                voyages = voyages.Where(x => x.TarifToutCompris >= prixMin);
            if (prixMax != 0)
                voyages = voyages.Where(x => x.TarifToutCompris <= prixMax);
            return View(voyages.ToList());
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
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent");
            return View();
        }

        // POST: BackOffice/Voyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateAller,DateRetour,PlacesDisponibles,TarifToutCompris,DestinationID")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Voyages.Add(voyage);
                db.SaveChanges();
                DisplayMessage($"Le voyage {voyage.Destination} à {voyage.TarifToutCompris} a été créé.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }

            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
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
            return View(voyage);
        }

        // POST: BackOffice/Voyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateAller,DateRetour,PlacesDisponibles,TarifToutCompris,DestinationID")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voyage).State = EntityState.Modified;
                db.SaveChanges();
                DisplayMessage($"Le voyage {voyage.Destination} a été modifié.", MessageType.SUCCESS);
                return RedirectToAction("Index");
            }
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
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
            DisplayMessage($"Le voyage {voyage.Destination} a été supprimé.", MessageType.SUCCESS);
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
