namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalInfoAddedToCommands : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RobotCommands", "Received", c => c.Boolean(nullable: false));
            AddColumn("dbo.RobotCommands", "Executed", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProgramRobots", "Received");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProgramRobots", "Received", c => c.Boolean(nullable: false));
            DropColumn("dbo.RobotCommands", "Executed");
            DropColumn("dbo.RobotCommands", "Received");
        }
    }
}
