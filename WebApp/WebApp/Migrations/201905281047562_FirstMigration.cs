namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetNumber = c.Int(nullable: false),
                        StreetName = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Coefficients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoefficientPensioner = c.Double(nullable: false),
                        CoefficientStudent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfTicket = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        Email = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        IsValidated = c.Boolean(),
                        Picture = c.String(),
                        PassengerType = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.PricelistItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Coefficients_Id = c.Int(),
                        Item_Id = c.Int(),
                        Pricelist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coefficients", t => t.Coefficients_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .ForeignKey("dbo.Pricelists", t => t.Pricelist_Id)
                .Index(t => t.Coefficients_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.Pricelist_Id);
            
            CreateTable(
                "dbo.Pricelists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Departures = c.String(),
                        RouteType = c.Int(nullable: false),
                        RouteNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Name = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Passenger_Id = c.Int(),
                        PricelistItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Passenger_Id)
                .ForeignKey("dbo.PricelistItems", t => t.PricelistItem_Id)
                .Index(t => t.Passenger_Id)
                .Index(t => t.PricelistItem_Id);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "PricelistItem_Id", "dbo.PricelistItems");
            DropForeignKey("dbo.Tickets", "Passenger_Id", "dbo.People");
            DropForeignKey("dbo.Stations", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.PricelistItems", "Pricelist_Id", "dbo.Pricelists");
            DropForeignKey("dbo.PricelistItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.PricelistItems", "Coefficients_Id", "dbo.Coefficients");
            DropForeignKey("dbo.People", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Tickets", new[] { "PricelistItem_Id" });
            DropIndex("dbo.Tickets", new[] { "Passenger_Id" });
            DropIndex("dbo.Stations", new[] { "Address_Id" });
            DropIndex("dbo.PricelistItems", new[] { "Pricelist_Id" });
            DropIndex("dbo.PricelistItems", new[] { "Item_Id" });
            DropIndex("dbo.PricelistItems", new[] { "Coefficients_Id" });
            DropIndex("dbo.People", new[] { "Address_Id" });
            DropTable("dbo.Timetables");
            DropTable("dbo.Tickets");
            DropTable("dbo.Stations");
            DropTable("dbo.Routes");
            DropTable("dbo.Pricelists");
            DropTable("dbo.PricelistItems");
            DropTable("dbo.People");
            DropTable("dbo.Locations");
            DropTable("dbo.Items");
            DropTable("dbo.Coefficients");
            DropTable("dbo.Addresses");
        }
    }
}
