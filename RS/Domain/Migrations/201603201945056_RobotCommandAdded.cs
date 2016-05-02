namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RobotCommandAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RobotCommands",
                c => new
                    {
                        RobotCommandID = c.Int(nullable: false, identity: true),
                        RobotID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Argument = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RobotCommandID)
                .ForeignKey("dbo.Robots", t => t.RobotID, cascadeDelete: true)
                .Index(t => t.RobotID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RobotCommands", "RobotID", "dbo.Robots");
            DropIndex("dbo.RobotCommands", new[] { "RobotID" });
            DropTable("dbo.RobotCommands");
        }
    }
}
