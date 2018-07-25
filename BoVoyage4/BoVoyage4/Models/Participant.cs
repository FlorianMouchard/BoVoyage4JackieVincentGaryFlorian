using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Models
{
    public class Participant : Personne
    {
        [Display(Name = "Réduction")]
        public float Reduction { get; set; }

    }
}