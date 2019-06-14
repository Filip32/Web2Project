namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration80 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pricelists", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pricelists", "RowVersion");
        }
    }
}
