namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoWhoSend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketsDetalles", "whoSend", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketsDetalles", "whoSend");
        }
    }
}
