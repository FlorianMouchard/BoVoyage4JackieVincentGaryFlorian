using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Areas.BackOffice.Controllers
{
    public class DashboardController : Controller
    {
        /// <summary>
        /// Permet l'affichage de l'accueil du backoffice
        /// </summary>
        /// <returns></returns>
        // GET: BackOffice/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}