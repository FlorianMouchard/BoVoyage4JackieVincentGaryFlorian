using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class Voyage : BaseModel
    {
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name ="Date aller")]
        public DateTime DateAller { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name =" Date retour")]
        public DateTime DateRetour { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name ="Places disponibles")]
        public int PlacesDisponibles { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name ="Tarif tout compris")]
        [RegularExpression("\\d+", ErrorMessage = "N'entrez que des chiffres")]
        public decimal TarifToutCompris { get; set; }

        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name ="Destination")]
        public int DestinationID { get; set; }

        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }
}