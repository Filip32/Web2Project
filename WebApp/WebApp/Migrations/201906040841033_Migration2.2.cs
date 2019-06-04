namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "TypeOfTicket", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "Price", c => c.Single(nullable: false));
            DropColumn("dbo.Tickets", "PricelistItem_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "PricelistItem_id", c => c.Int(nullable: false));
            DropColumn("dbo.Tickets", "Price");
            DropColumn("dbo.Tickets", "TypeOfTicket");
        }
    }
}
