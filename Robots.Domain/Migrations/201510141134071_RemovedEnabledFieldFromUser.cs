namespace Robots.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEnabledFieldFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Enabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Enabled", c => c.Boolean(nullable: false));
        }
    }
}
