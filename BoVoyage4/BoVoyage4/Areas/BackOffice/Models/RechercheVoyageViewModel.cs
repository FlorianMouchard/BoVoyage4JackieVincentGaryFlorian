using BoVoyage4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Areas.BackOffice.Models
{
    public class RechercheVoyageViewModel
    {
        //public IEnumerable<Destination> Destination { get; set; }


        [Display(Name ="Prix minimum")]
        [RegularExpression("\\d+", ErrorMessage = "N'entrez que des chiffres")]
        public decimal? PrixMin { get; set; }

        [Display(Name ="Prix maximum")]
        [RegularExpression("\\d+", ErrorMessage = "N'entrez que des chiffres")]
        public decimal? PrixMax { get; set; }

        [Display(Name ="Partir après le")]
        [DataType(DataType.Date)]
        public DateTime? DateMin { get; set; }

        [Display(Name = "Partir avant le")]
        [DataType(DataType.Date)]
        public DateTime? DateMax { get; set; }

        public IEnumerable<Voyage> Voyages { get; set; }
    }
}