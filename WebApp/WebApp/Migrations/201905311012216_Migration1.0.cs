namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration10 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.People", newName: "Passengers");
            AddColumn("dbo.Passengers", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Passengers", "IsValidated", c => c.Int(nullable: false));
            AlterColumn("dbo.Passengers", "PassengerType", c => c.Int(nullable: false));
            DropColumn("dbo.Passengers", "Password");
            DropColumn("dbo.Passengers", "Email");
            DropColumn("dbo.Passengers", "BirthDate");
            DropColumn("dbo.Passengers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Passengers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Passengers", "Email", c => c.String());
            AddColumn("dbo.Passengers", "Password", c => c.String());
            AlterColumn("dbo.Passengers", "PassengerType", c => c.Int());
            AlterColumn("dbo.Passengers", "IsValidated", c => c.Boolean());
            DropColumn("dbo.Passengers", "Birthday");
            RenameTable(name: "dbo.Passengers", newName: "People");
        }
    }
}
