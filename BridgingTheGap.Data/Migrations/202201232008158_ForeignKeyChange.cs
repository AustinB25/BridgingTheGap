namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Tutor_TutorId", c => c.Int());
            CreateIndex("dbo.Subjects", "Tutor_TutorId");
            AddForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors", "TutorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_TutorId" });
            DropColumn("dbo.Subjects", "Tutor_TutorId");
        }
    }
}
