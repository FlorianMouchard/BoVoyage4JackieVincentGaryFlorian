using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Permet d'afficher la page accueil de l'application
        /// </summary>
        /// <returns></returns>
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Permet d'afficher la page "A propos" contenant des informations sur Bovoyage
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
    }
}