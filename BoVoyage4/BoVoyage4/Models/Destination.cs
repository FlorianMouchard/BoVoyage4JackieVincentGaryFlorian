using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class Destination : BaseModel
    {
        [Display(Name = "Continent")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Continent { get; set; }

        [Display(Name = "Pays")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Pays { get; set; }

        [Display(Name = "Région")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Region { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Description { get; set; }

        public ICollection<DestinationFile> Files { get; set; }
    }
}