namespace KesMemorija.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricalProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        HistoricalValue_IDGeoPolozaja = c.String(),
                        HistoricalValue_Potrosnja = c.Double(nullable: false),
                        HistoricalValue_Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoricalProperties");
        }
    }
}
