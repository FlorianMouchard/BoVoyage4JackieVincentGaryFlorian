namespace BoVoyage4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgenceVoyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Assurances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prix = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Historique = c.String(),
                        Password = c.String(nullable: false),
                        Civilite = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Commercials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Civilite = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Continent = c.String(nullable: false),
                        Pays = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DossierReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(nullable: false),
                        PrixTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EtatDossier = c.Int(nullable: false),
                        VoyageID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        ParticipantID = c.Int(nullable: false),
                        AssuranceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assurances", t => t.AssuranceID, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.ParticipantID, cascadeDelete: true)
                .ForeignKey("dbo.Voyages", t => t.VoyageID, cascadeDelete: true)
                .Index(t => t.VoyageID)
                .Index(t => t.ClientID)
                .Index(t => t.ParticipantID)
                .Index(t => t.AssuranceID);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reduction = c.Single(nullable: false),
                        Civilite = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateAller = c.DateTime(nullable: false),
                        DateRetour = c.DateTime(nullable: false),
                        PlacesDisponibles = c.Int(nullable: false),
                        TarifToutCompris = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DestinationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: true)
                .Index(t => t.DestinationID);
            
            CreateTable(
                "dbo.VoyageFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 254),
                        TypeContenu = c.String(nullable: false, maxLength: 100),
                        Contenu = c.Binary(nullable: false),
                        VoyageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Voyages", t => t.VoyageID, cascadeDelete: true)
                .Index(t => t.VoyageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoyageFiles", "VoyageID", "dbo.Voyages");
            DropForeignKey("dbo.DossierReservations", "VoyageID", "dbo.Voyages");
            DropForeignKey("dbo.Voyages", "DestinationID", "dbo.Destinations");
            DropForeignKey("dbo.DossierReservations", "ParticipantID", "dbo.Participants");
            DropForeignKey("dbo.DossierReservations", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.DossierReservations", "AssuranceID", "dbo.Assurances");
            DropIndex("dbo.VoyageFiles", new[] { "VoyageID" });
            DropIndex("dbo.Voyages", new[] { "DestinationID" });
            DropIndex("dbo.DossierReservations", new[] { "AssuranceID" });
            DropIndex("dbo.DossierReservations", new[] { "ParticipantID" });
            DropIndex("dbo.DossierReservations", new[] { "ClientID" });
            DropIndex("dbo.DossierReservations", new[] { "VoyageID" });
            DropTable("dbo.VoyageFiles");
            DropTable("dbo.Voyages");
            DropTable("dbo.Participants");
            DropTable("dbo.DossierReservations");
            DropTable("dbo.Destinations");
            DropTable("dbo.Commercials");
            DropTable("dbo.Clients");
            DropTable("dbo.Assurances");
            DropTable("dbo.AgenceVoyages");
        }
    }
}
