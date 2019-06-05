namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "IsStation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "IsStation");
        }
    }
}
