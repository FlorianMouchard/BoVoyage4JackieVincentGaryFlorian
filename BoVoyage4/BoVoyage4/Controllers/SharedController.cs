using BoVoyage4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Controllers
{
    public class SharedController : Controller
    {
        protected BoVoyage4DbContext db = new BoVoyage4DbContext();
        // GET: Shared
        [ChildActionOnly]
        public ActionResult TopFivePrice()
        {
            var voyages = db.Voyages.OrderByDescending(x => x.TarifToutCompris).Take(5);
            return View("_TopFivePrice", voyages);
        }

        [ChildActionOnly]
        public ActionResult TopFiveDate()
        {
            var voyages = db.Voyages.OrderByDescending(x => x.DateAller).Take(5);
            return View("_TopFiveDate", voyages);
        }
    }
}