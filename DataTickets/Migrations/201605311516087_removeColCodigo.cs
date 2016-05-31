namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeColCodigo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tickets", "UniqueCOD");
            DropColumn("dbo.Tickets", "Codigo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "Codigo", c => c.String(nullable: false, maxLength: 13));
            CreateIndex("dbo.Tickets", "Codigo", unique: true, name: "UniqueCOD");
        }
    }
}
