using BoVoyage4.Areas.BackOffice.Models;
using BoVoyage4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BoVoyage4.Data
{
    public class BoVoyage4DbContext : DbContext
    {
        public BoVoyage4DbContext() : base("gjvf")
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Voyage> Voyages { get; set; }    

        public DbSet<VoyageFile> VoyageFiles { get; set; }

        public DbSet<AgenceVoyage> AgenceVoyages { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<DossierReservation> DossierReservations { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Commercial> Commerciaux { get; set; }

        public DbSet<AssuranceAnnulation> AssuranceAnnulations { get; set; }

    }
}