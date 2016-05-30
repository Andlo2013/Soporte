namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campoDescargaCAT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketsCategorias", "isDescarga", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketsCategorias", "isDescarga");
        }
    }
}
