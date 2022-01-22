namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataBaseUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_TutorId" });
            DropColumn("dbo.Subjects", "Tutor_TutorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Tutor_TutorId", c => c.Int());
            CreateIndex("dbo.Subjects", "Tutor_TutorId");
            AddForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors", "TutorId");
        }
    }
}
