namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration50 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteStations", "Station_num", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteStations", "Station_num");
        }
    }
}
