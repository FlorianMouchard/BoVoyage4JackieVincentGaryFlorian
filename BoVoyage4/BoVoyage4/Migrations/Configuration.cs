using BoVoyage4.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace BoVoyage4.Migrations
{
    public class Configuration : DbMigrationsConfiguration<BoVoyage4DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}