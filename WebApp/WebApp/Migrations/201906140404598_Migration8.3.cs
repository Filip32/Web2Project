namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration83 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "LastUpdate");
        }
    }
}
