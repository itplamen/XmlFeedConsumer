namespace XmlFeedConsumer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XmlId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        IsLive = c.Boolean(nullable: false),
                        MatchId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .Index(t => t.XmlId, unique: true)
                .Index(t => t.MatchId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XmlId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        MatchType = c.String(nullable: false),
                        EventId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.XmlId, unique: true)
                .Index(t => t.EventId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XmlId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        IsLive = c.Boolean(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        SportId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sports", t => t.SportId, cascadeDelete: true)
                .Index(t => t.XmlId, unique: true)
                .Index(t => t.SportId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Sports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XmlId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        XmlSportId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.XmlSports", t => t.XmlSportId, cascadeDelete: true)
                .Index(t => t.XmlId, unique: true)
                .Index(t => t.XmlSportId)
                .Index(t => t.IsDeleted);
            
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
            
            CreateTable(
                "dbo.Odds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XmlId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Value = c.Double(nullable: false),
                        SpecialBetValue = c.String(),
                        BetId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bets", t => t.BetId, cascadeDelete: true)
                .Index(t => t.XmlId, unique: true)
                .Index(t => t.BetId)
                .Index(t => t.IsDeleted);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Odds", "BetId", "dbo.Bets");
            DropForeignKey("dbo.Sports", "XmlSportId", "dbo.XmlSports");
            DropForeignKey("dbo.Events", "SportId", "dbo.Sports");
            DropForeignKey("dbo.Matches", "EventId", "dbo.Events");
            DropForeignKey("dbo.Bets", "MatchId", "dbo.Matches");
            DropIndex("dbo.Odds", new[] { "IsDeleted" });
            DropIndex("dbo.Odds", new[] { "BetId" });
            DropIndex("dbo.Odds", new[] { "XmlId" });
            DropIndex("dbo.XmlSports", new[] { "IsDeleted" });
            DropIndex("dbo.Sports", new[] { "IsDeleted" });
            DropIndex("dbo.Sports", new[] { "XmlSportId" });
            DropIndex("dbo.Sports", new[] { "XmlId" });
            DropIndex("dbo.Events", new[] { "IsDeleted" });
            DropIndex("dbo.Events", new[] { "SportId" });
            DropIndex("dbo.Events", new[] { "XmlId" });
            DropIndex("dbo.Matches", new[] { "IsDeleted" });
            DropIndex("dbo.Matches", new[] { "EventId" });
            DropIndex("dbo.Matches", new[] { "XmlId" });
            DropIndex("dbo.Bets", new[] { "IsDeleted" });
            DropIndex("dbo.Bets", new[] { "MatchId" });
            DropIndex("dbo.Bets", new[] { "XmlId" });
            DropTable("dbo.Odds");
            DropTable("dbo.XmlSports");
            DropTable("dbo.Sports");
            DropTable("dbo.Events");
            DropTable("dbo.Matches");
            DropTable("dbo.Bets");
        }
    }
}