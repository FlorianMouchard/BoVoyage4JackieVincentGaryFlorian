using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class AgenceVoyage : BaseModel
    {
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Nom { get; set; }

    }
}