namespace XmlFeedConsumer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class XmlSport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.XmlSports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            AddColumn("dbo.Sports", "XmlSportId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sports", "XmlSportId");
            AddForeignKey("dbo.Sports", "XmlSportId", "dbo.XmlSports", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sports", "XmlSportId", "dbo.XmlSports");
            DropIndex("dbo.XmlSports", new[] { "IsDeleted" });
            DropIndex("dbo.Sports", new[] { "XmlSportId" });
            DropColumn("dbo.Sports", "XmlSportId");
            DropTable("dbo.XmlSports");
        }
    }
}
