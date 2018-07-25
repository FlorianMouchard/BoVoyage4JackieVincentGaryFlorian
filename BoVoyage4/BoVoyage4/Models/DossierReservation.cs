using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class DossierReservation : BaseModel
    {
        [Display(Name ="Numéro de carte bancaire")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [RegularExpression("^\\d{16}$", ErrorMessage = "Entrer un numéro au format xxxxyyyyxxxxyyyy")]
        public string NumeroCarteBancaire { get; set; }

        [Display(Name = "Prix total")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public decimal PrixTotal { get; set; }

        [Display(Name = "Etat du dossier")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public EtatDossierReservation EtatDossier { get; set; }

        [Display(Name = "Voyage")]
        public int VoyageID { get; set; }
        [ForeignKey("VoyageID")]
        public Voyage Voyage { get; set; }

        [Display(Name = "Client")]
        public int ClientID { get; set; }
        [ForeignKey("ClientID")]
        public Client Client { get; set; }

        [Display(Name = "Participant")]
        public int ParticipantID { get; set; }
        [ForeignKey("ParticipantID")]
        public Participant Participant { get; set; }

        [Display(Name = "Assurance")]
        public int AssuranceID { get; set; }
        [ForeignKey("AssuranceID")]
        public Assurance Assurance { get; set; }

    }
}