namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration81 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pricelists", "LastUpdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Pricelists", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pricelists", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.Pricelists", "LastUpdate");
        }
    }
}
