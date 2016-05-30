namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campoSecRespta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketsDetalles", "SecRespta", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketsDetalles", "SecRespta");
        }
    }
}
