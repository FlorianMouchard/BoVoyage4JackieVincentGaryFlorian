using BoVoyage4.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Utils.Validators
{
    public class ExistingClientMail : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            using (BoVoyage4DbContext db = new BoVoyage4DbContext())
                return !db.Clients.Any(x => x.Email == value.ToString());
        }
    }
}