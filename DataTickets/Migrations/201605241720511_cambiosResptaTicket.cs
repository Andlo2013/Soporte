namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosResptaTicket : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketsDetalles", "File1", c => c.String());
            AlterColumn("dbo.TicketsDetalles", "File2", c => c.String());
            AlterColumn("dbo.TicketsDetalles", "File3", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketsDetalles", "File3", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.TicketsDetalles", "File2", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.TicketsDetalles", "File1", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
