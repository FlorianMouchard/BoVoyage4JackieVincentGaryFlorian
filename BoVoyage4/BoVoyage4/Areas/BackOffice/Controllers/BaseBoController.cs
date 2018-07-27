using BoVoyage4.Data;
using BoVoyage4.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Areas.BackOffice.Controllers
{

    //[AuthentificationFilter]

    public class BaseBoController : Controller
    {
        protected BoVoyage4DbContext db = new BoVoyage4DbContext();
        /// <summary>
        /// Il s'agit du modele type nous permettant l'affichage des DisplayMessage
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        protected void DisplayMessage(string message, MessageType type)
        {
            TempData["Message"] = message;
            switch (type)
            {
                case MessageType.SUCCESS:
                    TempData["MessageType"] = "success";
                    break;
                case MessageType.WARNING:
                    TempData["MessageType"] = "warning";
                    break;
                case MessageType.ERROR:
                    TempData["MessageType"] = "danger";
                    break;

            }
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

    public enum MessageType
    {
        SUCCESS,
        WARNING,
        ERROR
    }
}