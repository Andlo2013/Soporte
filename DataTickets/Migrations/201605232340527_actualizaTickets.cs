namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizaTickets : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TicketsDetalles", "tienePlan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketsDetalles", "tienePlan", c => c.Boolean(nullable: false));
        }
    }
}
