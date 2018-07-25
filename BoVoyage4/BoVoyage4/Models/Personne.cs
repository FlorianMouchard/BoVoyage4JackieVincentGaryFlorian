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
        [Required(ErrorMessage = "La civilité est obligatoire")]
        public string Civilite { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'adresse est obligatoire")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Le téléphone est obligatoire")]
        [RegularExpression("(0|\\+33|0033)[1-9][0-9]{8}", ErrorMessage = "Entrer un numéro au format 0xxxxxxxxx")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire")]
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