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
using BoVoyage4.Utils;

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class CommerciauxController : BaseBoController
    {
        private BoVoyage4DbContext db = new BoVoyage4DbContext();

        // GET: BackOffice/Commerciaux
        public ActionResult Index()
        {
            ViewBag.Civilites = db.Civilites.ToList();
            return View(db.Commerciaux.ToList());
        }

        // GET: BackOffice/Commerciaux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commercial commercial = db.Commerciaux.Find(id);
            if (commercial == null)
            {
                return HttpNotFound();
            }
            return View(commercial);
        }

        // GET: BackOffice/Commerciaux/Create
        public ActionResult Create()
        {
            ViewBag.Civilites = db.Civilites.ToList();
            return View();
        }

        // POST: BackOffice/Commerciaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,PasswordConfirmation,CiviliteID,Nom,Prenom,Adresse,Telephone,DateNaissance")] Commercial commercial)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                commercial.Password = commercial.Password.HashMD5();
                db.Commerciaux.Add(commercial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Civilites = db.Civilites.ToList();
            return View(commercial);
        }

        // GET: BackOffice/Commerciaux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commercial commercial = db.Commerciaux.Include(x => x.Civilite).SingleOrDefault(x=> x.ID == id);
            if (commercial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Civilites = db.Civilites.ToList();
            return View(commercial);
        }

        // POST: BackOffice/Commerciaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,CiviliteID,Nom,Prenom,Adresse,Telephone,DateNaissance")] Commercial commercial)
        {
            ModelState.Remove("Password");
            ModelState.Remove("PasswordConfirmation");
            var old = db.Commerciaux.SingleOrDefault(x => x.ID == commercial.ID);
            commercial.Password = old.Password.HashMD5();
            commercial.PasswordConfirmation = old.Password.HashMD5();                      
            db.Entry(old).State = EntityState.Detached;
            if (ModelState.IsValid)
            {
                db.Entry(commercial).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Civilites = db.Civilites.ToList();
            return View(commercial);
        }

        // GET: BackOffice/Commerciaux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commercial commercial = db.Commerciaux.Find(id);
            if (commercial == null)
            {
                return HttpNotFound();
            }
            return View(commercial);
        }

        // POST: BackOffice/Commerciaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commercial commercial = db.Commerciaux.Find(id);
            db.Commerciaux.Remove(commercial);
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
