namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Routes", "DayType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "DayType");
            DropColumn("dbo.Routes", "Deleted");
        }
    }
}
