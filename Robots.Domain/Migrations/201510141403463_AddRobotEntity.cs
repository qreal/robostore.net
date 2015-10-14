namespace Robots.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRobotEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Robots",
                c => new
                    {
                        RobotID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SSID = c.String(),
                        UserID = c.Int(nullable: false),
                        ModelConfig = c.String(),
                        SystemConfig = c.String(),
                        Program = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.RobotID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Robots");
        }
    }
}
