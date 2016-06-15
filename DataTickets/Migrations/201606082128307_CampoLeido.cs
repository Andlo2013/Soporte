namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoLeido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketsDetalles", "isReaded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketsDetalles", "isReaded");
        }
    }
}
