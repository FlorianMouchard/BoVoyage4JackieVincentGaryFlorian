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

namespace BoVoyage4.Controllers
{
    public class OffresController : BaseController
    {


        // GET: Offres
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
