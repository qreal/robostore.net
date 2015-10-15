namespace Robots.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAttributesForRobot : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Robots", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Robots", "SSID", c => c.String(nullable: false));
            AlterColumn("dbo.Robots", "ModelConfig", c => c.String(nullable: false));
            AlterColumn("dbo.Robots", "SystemConfig", c => c.String(nullable: false));
            AlterColumn("dbo.Robots", "Program", c => c.String(nullable: false));
            AlterColumn("dbo.Robots", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Robots", "Status", c => c.String());
            AlterColumn("dbo.Robots", "Program", c => c.String());
            AlterColumn("dbo.Robots", "SystemConfig", c => c.String());
            AlterColumn("dbo.Robots", "ModelConfig", c => c.String());
            AlterColumn("dbo.Robots", "SSID", c => c.String());
            AlterColumn("dbo.Robots", "Name", c => c.String());
        }
    }
}
