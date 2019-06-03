namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Passengers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.PricelistItems", "Coefficients_Id", "dbo.Coefficients");
            DropForeignKey("dbo.PricelistItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.PricelistItems", "Pricelist_Id", "dbo.Pricelists");
            DropForeignKey("dbo.Stations", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.StationRoutes", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.StationRoutes", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Tickets", "Passenger_Id", "dbo.Passengers");
            DropForeignKey("dbo.Tickets", "PricelistItem_Id", "dbo.PricelistItems");
            DropForeignKey("dbo.Routes", "Timetable_Id", "dbo.Timetables");
            DropIndex("dbo.Passengers", new[] { "Address_Id" });
            DropIndex("dbo.PricelistItems", new[] { "Coefficients_Id" });
            DropIndex("dbo.PricelistItems", new[] { "Item_Id" });
            DropIndex("dbo.PricelistItems", new[] { "Pricelist_Id" });
            DropIndex("dbo.Routes", new[] { "Timetable_Id" });
            DropIndex("dbo.Stations", new[] { "Address_Id" });
            DropIndex("dbo.Tickets", new[] { "Passenger_Id" });
            DropIndex("dbo.Tickets", new[] { "PricelistItem_Id" });
            DropIndex("dbo.StationRoutes", new[] { "Station_Id" });
            DropIndex("dbo.StationRoutes", new[] { "Route_Id" });
            AlterColumn("dbo.Passengers", "Address_id", c => c.Int(nullable: false));
            AlterColumn("dbo.PricelistItems", "Coefficients_id", c => c.Int(nullable: false));
            AlterColumn("dbo.PricelistItems", "Item_id", c => c.Int(nullable: false));
            AlterColumn("dbo.PricelistItems", "Pricelist_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Stations", "Address_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "Passenger_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "PricelistItem_id", c => c.Int(nullable: false));
            DropColumn("dbo.Routes", "Timetable_Id");
            DropTable("dbo.StationRoutes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StationRoutes",
                c => new
                    {
                        Station_Id = c.Int(nullable: false),
                        Route_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Station_Id, t.Route_Id });
            
            AddColumn("dbo.Routes", "Timetable_Id", c => c.Int());
            AlterColumn("dbo.Tickets", "PricelistItem_id", c => c.Int());
            AlterColumn("dbo.Tickets", "Passenger_id", c => c.Int());
            AlterColumn("dbo.Stations", "Address_id", c => c.Int());
            AlterColumn("dbo.PricelistItems", "Pricelist_id", c => c.Int());
            AlterColumn("dbo.PricelistItems", "Item_id", c => c.Int());
            AlterColumn("dbo.PricelistItems", "Coefficients_id", c => c.Int());
            AlterColumn("dbo.Passengers", "Address_id", c => c.Int());
            CreateIndex("dbo.StationRoutes", "Route_Id");
            CreateIndex("dbo.StationRoutes", "Station_Id");
            CreateIndex("dbo.Tickets", "PricelistItem_Id");
            CreateIndex("dbo.Tickets", "Passenger_Id");
            CreateIndex("dbo.Stations", "Address_Id");
            CreateIndex("dbo.Routes", "Timetable_Id");
            CreateIndex("dbo.PricelistItems", "Pricelist_Id");
            CreateIndex("dbo.PricelistItems", "Item_Id");
            CreateIndex("dbo.PricelistItems", "Coefficients_Id");
            CreateIndex("dbo.Passengers", "Address_Id");
            AddForeignKey("dbo.Routes", "Timetable_Id", "dbo.Timetables", "Id");
            AddForeignKey("dbo.Tickets", "PricelistItem_Id", "dbo.PricelistItems", "Id");
            AddForeignKey("dbo.Tickets", "Passenger_Id", "dbo.Passengers", "Id");
            AddForeignKey("dbo.StationRoutes", "Route_Id", "dbo.Routes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StationRoutes", "Station_Id", "dbo.Stations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Stations", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.PricelistItems", "Pricelist_Id", "dbo.Pricelists", "Id");
            AddForeignKey("dbo.PricelistItems", "Item_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.PricelistItems", "Coefficients_Id", "dbo.Coefficients", "Id");
            AddForeignKey("dbo.Passengers", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
