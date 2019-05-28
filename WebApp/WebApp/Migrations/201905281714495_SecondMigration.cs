namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StationRoutes",
                c => new
                    {
                        Station_Id = c.Int(nullable: false),
                        Route_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Station_Id, t.Route_Id })
                .ForeignKey("dbo.Stations", t => t.Station_Id, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .Index(t => t.Station_Id)
                .Index(t => t.Route_Id);
            
            AddColumn("dbo.Routes", "Timetable_Id", c => c.Int());
            CreateIndex("dbo.Routes", "Timetable_Id");
            AddForeignKey("dbo.Routes", "Timetable_Id", "dbo.Timetables", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "Timetable_Id", "dbo.Timetables");
            DropForeignKey("dbo.StationRoutes", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.StationRoutes", "Station_Id", "dbo.Stations");
            DropIndex("dbo.StationRoutes", new[] { "Route_Id" });
            DropIndex("dbo.StationRoutes", new[] { "Station_Id" });
            DropIndex("dbo.Routes", new[] { "Timetable_Id" });
            DropColumn("dbo.Routes", "Timetable_Id");
            DropTable("dbo.StationRoutes");
        }
    }
}
