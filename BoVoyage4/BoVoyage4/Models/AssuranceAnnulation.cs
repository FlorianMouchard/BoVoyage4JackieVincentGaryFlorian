using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class AssuranceAnnulation : Assurance
    {
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Nom { get; set; }

        [Display(Name = "Prix")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }
    }
}