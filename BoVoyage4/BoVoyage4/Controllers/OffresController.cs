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
    public class OffresController : Controller
    {
        private BoVoyage4DbContext db = new BoVoyage4DbContext();

        // GET: Offres
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
            IQueryable<Voyage> voyages = db.Voyages.Include(x => x.Destination);
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

        // GET: Offres/Details/5
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
