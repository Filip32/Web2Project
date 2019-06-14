namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration82 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "LastUpdate");
        }
    }
}
