namespace BoVoyage4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifdbvoyage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voyages", "AgenceVoyageID", c => c.Int(nullable: false));
            CreateIndex("dbo.Voyages", "AgenceVoyageID");
            AddForeignKey("dbo.Voyages", "AgenceVoyageID", "dbo.AgenceVoyages", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voyages", "AgenceVoyageID", "dbo.AgenceVoyages");
            DropIndex("dbo.Voyages", new[] { "AgenceVoyageID" });
            DropColumn("dbo.Voyages", "AgenceVoyageID");
        }
    }
}
