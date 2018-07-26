using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage4.Filters
{
    public class AuthentificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["COMMERCIAL_BO"] == null)
            {
                filterContext.Result = new RedirectResult("\\BackOffice\\Authentication\\Login");
            }
        }
    }
}