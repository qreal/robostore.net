namespace Robots.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Enabled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Enalbled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Enalbled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Enabled");
        }
    }
}
