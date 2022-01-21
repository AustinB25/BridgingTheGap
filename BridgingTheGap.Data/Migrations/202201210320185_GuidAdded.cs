namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Subjects", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Tutors", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tutors", "OwnerId");
            DropColumn("dbo.Subjects", "OwnerId");
            DropColumn("dbo.Students", "OwnerId");
        }
    }
}
