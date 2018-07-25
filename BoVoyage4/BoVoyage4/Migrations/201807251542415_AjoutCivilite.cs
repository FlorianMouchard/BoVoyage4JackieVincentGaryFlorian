namespace BoVoyage4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutCivilite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Civilites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Clients", "CiviliteID", c => c.Int(nullable: false));
            AddColumn("dbo.Commercials", "CiviliteID", c => c.Int(nullable: false));
            AddColumn("dbo.Participants", "CiviliteID", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "CiviliteID");
            CreateIndex("dbo.Commercials", "CiviliteID");
            CreateIndex("dbo.Participants", "CiviliteID");
            AddForeignKey("dbo.Clients", "CiviliteID", "dbo.Civilites", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Commercials", "CiviliteID", "dbo.Civilites", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Participants", "CiviliteID", "dbo.Civilites", "ID", cascadeDelete: false);
            DropColumn("dbo.Clients", "Civilite");
            DropColumn("dbo.Commercials", "Civilite");
            DropColumn("dbo.Participants", "Civilite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Civilite", c => c.String(nullable: false));
            AddColumn("dbo.Commercials", "Civilite", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "Civilite", c => c.String(nullable: false));
            DropForeignKey("dbo.Participants", "CiviliteID", "dbo.Civilites");
            DropForeignKey("dbo.Commercials", "CiviliteID", "dbo.Civilites");
            DropForeignKey("dbo.Clients", "CiviliteID", "dbo.Civilites");
            DropIndex("dbo.Participants", new[] { "CiviliteID" });
            DropIndex("dbo.Commercials", new[] { "CiviliteID" });
            DropIndex("dbo.Clients", new[] { "CiviliteID" });
            DropColumn("dbo.Participants", "CiviliteID");
            DropColumn("dbo.Commercials", "CiviliteID");
            DropColumn("dbo.Clients", "CiviliteID");
            DropTable("dbo.Civilites");
        }
    }
}
