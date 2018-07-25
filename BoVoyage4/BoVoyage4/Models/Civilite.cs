using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class Civilite : BaseModel
    {
        [Required(ErrorMessage = "Nom obligatoire")]
        [StringLength(15, ErrorMessage = "Trop long")]
        public string Libelle { get; set; }
    }
}