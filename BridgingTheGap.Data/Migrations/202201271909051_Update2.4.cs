namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update24 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_TutorId" });
            RenameColumn(table: "dbo.Subjects", name: "Tutor_TutorId", newName: "TutorId");
            AlterColumn("dbo.Subjects", "TutorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "TutorId");
            AddForeignKey("dbo.Subjects", "TutorId", "dbo.Tutors", "TutorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "TutorId" });
            AlterColumn("dbo.Subjects", "TutorId", c => c.Int());
            RenameColumn(table: "dbo.Subjects", name: "TutorId", newName: "Tutor_TutorId");
            CreateIndex("dbo.Subjects", "Tutor_TutorId");
            AddForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors", "TutorId");
        }
    }
}
