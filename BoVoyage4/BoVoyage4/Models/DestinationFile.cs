using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class DestinationFile : BaseModel
    {
        [Display(Name="Nom du fichier")]
        [Required(ErrorMessage = "Champ {0} obligatoire")]
        [StringLength(254)]
        public string Nom { get; set; }

        [Display(Name ="Type du fichier")]
        [Required(ErrorMessage = "Champ {0} obligatoire")]
        [StringLength(100)]
        public string TypeContenu { get; set; }

        [Display(Name ="Contenu")]
        [Required(ErrorMessage = "Champ {0} obligatoire")]
        public byte[] Contenu { get; set; }
        
        [Display(Name = "Destination")]
        public int DestinationID { get; set; }

        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }
}