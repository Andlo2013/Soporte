namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class camposCmbChangeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "cmbPrioridadId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "cmbEstadoId", c => c.Int(nullable: false));
            DropColumn("dbo.Tickets", "PrioridadId");
            DropColumn("dbo.Tickets", "TicketEstadoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TicketEstadoId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "PrioridadId", c => c.Int(nullable: false));
            DropColumn("dbo.Tickets", "cmbEstadoId");
            DropColumn("dbo.Tickets", "cmbPrioridadId");
        }
    }
}
