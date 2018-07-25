using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public abstract class Personne : BaseModel
    {
        [Display(Name = "Civilité")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Civilite { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Nom { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Prenom { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        public string Adresse { get; set; }

        [Display(Name = "Téléphone")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [RegularExpression("(0|\\+33|0033)[1-9][0-9]{8}", ErrorMessage = "Entrer un numéro au format 0xxxxxxxxx")]
        public string Telephone { get; set; }

        [Display(Name = "Date de naissance")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")] 
        public DateTime DateNaissance { get; set; }

        [NotMapped]
        public int Age
        {

            get
            {
                   return DateTime.Now.Year - DateNaissance.Year -
                         (DateTime.Now.Month < DateNaissance.Month ? 1 :
                         (DateTime.Now.Month == DateNaissance.Month && DateTime.Now.Day < DateNaissance.Day) ? 1 : 0);
            }
        }
    }
}