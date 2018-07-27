using BoVoyage4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Areas.BackOffice.Models
{
    public class RechercheClientViewModel
    {
        [Display(Name = "Nom")]        
        public string Nom { get; set; }

        [Display(Name = "Prénom")]        
        public string Prenom { get; set; }

        [Display(Name = "Né avant le")]       
        [DataType(DataType.Date)]
        public DateTime? NeAvantLe { get; set; }

        [Display(Name = "Né après le")]        
        [DataType(DataType.Date)]
        public DateTime? NeApresLe { get; set; }

        public IEnumerable<Client> Clients { get; set; }
    }
}