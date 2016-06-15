namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketsDetalles", "whoSend", c => c.String(nullable: false, maxLength: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketsDetalles", "whoSend", c => c.String(nullable: false));
        }
    }
}
