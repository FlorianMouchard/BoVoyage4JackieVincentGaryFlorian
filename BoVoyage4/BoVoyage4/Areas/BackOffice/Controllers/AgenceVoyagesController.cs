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
      

        // GET: BackOffice/AgenceVoyages
        public ActionResult Index()
        {
            return View(db.AgenceVoyages.ToList());
        }

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

        // GET: BackOffice/AgenceVoyages/Create
        public ActionResult Create()
        {
            return View();
        }

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
                return RedirectToAction("Index");
            }

            return View(agenceVoyage);
        }

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
                return RedirectToAction("Index");
            }
            return View(agenceVoyage);
        }

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

        // POST: BackOffice/AgenceVoyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgenceVoyage agenceVoyage = db.AgenceVoyages.Find(id);
            db.AgenceVoyages.Remove(agenceVoyage);
            db.SaveChanges();
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
