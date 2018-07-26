using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyage4.Data;
using BoVoyage4.Models;

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class DestinationsController : BaseBoController
    {
        private BoVoyage4DbContext db = new BoVoyage4DbContext();

        // GET: BackOffice/Destinations
        public ActionResult Index()
        {
            return View(db.Destinations.ToList());
        }

        // GET: BackOffice/Destinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // GET: BackOffice/Destinations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Continent,Pays,Region,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: BackOffice/Destinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Room room = db.Rooms.Include(x => x.Files).SingleOrDefault(x => x.ID == id); ou les deux lignes suivantes
            var rooms = db.Destinations.Include(r => r.Files).SingleOrDefault(x => x.ID == id);
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: BackOffice/Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Continent,Pays,Region,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }
        [HttpPost]
        public ActionResult AddFile(int id, HttpPostedFileBase upload)
        {
            if (upload.ContentLength > 0)
            {

                var model = new DestinationFile();

                model.DestinationID = id;
                model.Nom = upload.FileName;
                model.TypeContenu = upload.ContentType;

                using (var reader = new BinaryReader(upload.InputStream))
                {
                    model.Contenu = reader.ReadBytes(upload.ContentLength);
                }

                db.DestinationFiles.Add(model);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = model.DestinationID });
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult DeleteFile(int id)
        {
            var file = db.DestinationFiles.Find(id);
            if (file == null)
            {
                return HttpNotFound("Le fichier demandé n'existe pas.");
            }
            db.DestinationFiles.Remove(file);
            db.SaveChanges();
            return Json("OK");

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
