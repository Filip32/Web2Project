namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Passengers", "AppUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Passengers", "AppUserId", c => c.Int(nullable: false));
        }
    }
}
