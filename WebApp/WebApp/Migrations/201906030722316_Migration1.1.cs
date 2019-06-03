namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "AppUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "AppUserId");
        }
    }
}
