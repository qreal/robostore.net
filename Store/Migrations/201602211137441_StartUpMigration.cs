namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartUpMigration : DbMigration
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
                    })
                .PrimaryKey(t => t.RobotID);
            
            CreateTable(
                "dbo.ProgramRobots",
                c => new
                    {
                        ProgramRobotID = c.Int(nullable: false, identity: true),
                        ProgramID = c.Int(nullable: false),
                        RobotID = c.Int(nullable: false),
                        CurrentVersion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProgramRobotID)
                .ForeignKey("dbo.Programs", t => t.ProgramID, cascadeDelete: true)
                .ForeignKey("dbo.Robots", t => t.RobotID, cascadeDelete: true)
                .Index(t => t.ProgramID)
                .Index(t => t.RobotID);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ProgramID = c.Int(nullable: false, identity: true),
                        ActualVersion = c.Int(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ProgramID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProgramRobots", "RobotID", "dbo.Robots");
            DropForeignKey("dbo.ProgramRobots", "ProgramID", "dbo.Programs");
            DropForeignKey("dbo.Configurations", "RobotID", "dbo.Robots");
            DropIndex("dbo.ProgramRobots", new[] { "RobotID" });
            DropIndex("dbo.ProgramRobots", new[] { "ProgramID" });
            DropIndex("dbo.Configurations", new[] { "RobotID" });
            DropTable("dbo.Programs");
            DropTable("dbo.ProgramRobots");
            DropTable("dbo.Robots");
            DropTable("dbo.Configurations");
        }
    }
}
