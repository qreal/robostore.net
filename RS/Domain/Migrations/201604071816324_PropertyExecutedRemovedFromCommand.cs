namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyExecutedRemovedFromCommand : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RobotCommands", "Executed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RobotCommands", "Executed", c => c.Boolean(nullable: false));
        }
    }
}
