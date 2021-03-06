﻿using System;
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
    public class GestionParticipantsController : BaseBoController
    {
        /// <summary>
        /// Permet de retourner la liste des participants
        /// </summary>
        /// <returns></returns>
        // GET: BackOffice/GestionParticipants
        public ActionResult Index()
        {
            var participants = db.Participants.Include(p => p.Civilite);
            return View(participants.ToList());
        }
        /// <summary>
        /// Permet d'afficher les informations liées à un participant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BackOffice/GestionParticipants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }


    }
      
}
