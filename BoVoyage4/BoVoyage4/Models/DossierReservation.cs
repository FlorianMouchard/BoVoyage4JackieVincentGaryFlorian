﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class DossierReservation : BaseModel
    {
        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        [RegularExpression("^\\d{16}$", ErrorMessage = "Entrer un numéro au format xxxxyyyyxxxxyyyy")]
        public string NumeroCarteBancaire { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public decimal PrixTotal { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public EtatDossierReservation EtatDossier { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public int VoyageID { get; set; }

        [ForeignKey("VoyageID")]
        public Voyage Voyage { get; set; }
    }
}