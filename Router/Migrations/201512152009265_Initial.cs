namespace Router.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        ConfigurationID = c.Int(nullable: false, identity: true),
                        RobotID = c.Int(nullable: false),
                        Port = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConfigurationID)
                .ForeignKey("dbo.Robots", t => t.RobotID, cascadeDelete: true)
                .Index(t => t.RobotID);
            
            CreateTable(
                "dbo.Robots",
                c => new
                    {
                        RobotID = c.Int(nullable: false, identity: true),
                        isOnline = c.Boolean(nullable: false),
                        IP = c.String(),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.RobotID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        RobotID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Robots", t => t.RobotID, cascadeDelete: true)
                .Index(t => t.RobotID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "RobotID", "dbo.Robots");
            DropForeignKey("dbo.Configurations", "RobotID", "dbo.Robots");
            DropIndex("dbo.Messages", new[] { "RobotID" });
            DropIndex("dbo.Configurations", new[] { "RobotID" });
            DropTable("dbo.Messages");
            DropTable("dbo.Robots");
            DropTable("dbo.Configurations");
        }
    }
}
