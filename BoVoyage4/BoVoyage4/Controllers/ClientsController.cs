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

namespace BoVoyage4.Controllers
{
    public class ClientsController : Controller
    {
        private BoVoyage4DbContext db = new BoVoyage4DbContext();

        // GET: Clients
        public ActionResult Index()
        {
            ViewBag.Civilites = db.Civilites.ToList();
            return View(db.Clients.ToList());
        }

        // GET: Clients/Search?
        [HttpGet]
        public ActionResult Index(string nom, string prenom, DateTime? neAvantLe, DateTime? neApresLe)
        {
            ViewBag.neAvantLe = neAvantLe;
            ViewBag.neApresLe = neApresLe;

            IQueryable<Client> clients = db.Clients;

            if (nom != null)
                clients = clients.Where(x => x.Nom.Contains(nom));
            if (prenom != null)
                clients = clients.Where(x => x.Nom.Contains(prenom));
            if (neAvantLe.HasValue)
                clients = clients.Where(x => x.DateNaissance <= neAvantLe.Value);
            if (neApresLe.HasValue)
                clients = clients.Where(x => x.DateNaissance >= neApresLe.Value);

            return View(db.Clients.ToList());
        }

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

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.Civilites = db.Civilites.ToList();
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Historique,Password,Civilite,Nom,Prenom,Adresse,Telephone,DateNaissance")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Civilites = db.Civilites.ToList();
            return View(client);
        }

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

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Historique,Password,Civilite,Nom,Prenom,Adresse,Telephone,DateNaissance")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
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

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
