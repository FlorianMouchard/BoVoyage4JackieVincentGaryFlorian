namespace BoVoyage4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DestinationFile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VoyageFiles", "VoyageID", "dbo.Voyages");
            DropIndex("dbo.VoyageFiles", new[] { "VoyageID" });
            CreateTable(
                "dbo.DestinationFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 254),
                        TypeContenu = c.String(nullable: false, maxLength: 100),
                        Contenu = c.Binary(nullable: false),
                        DestinationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: true)
                .Index(t => t.DestinationID);
            
            DropTable("dbo.VoyageFiles");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.DestinationFiles", "DestinationID", "dbo.Destinations");
            DropIndex("dbo.DestinationFiles", new[] { "DestinationID" });
            DropTable("dbo.DestinationFiles");
            CreateIndex("dbo.VoyageFiles", "VoyageID");
            AddForeignKey("dbo.VoyageFiles", "VoyageID", "dbo.Voyages", "ID", cascadeDelete: true);
        }
    }
}
