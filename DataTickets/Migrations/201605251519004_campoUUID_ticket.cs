namespace DataTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campoUUID_ticket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "UUID", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "UUID");
        }
    }
}
