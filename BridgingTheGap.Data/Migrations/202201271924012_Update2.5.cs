namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update25 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "TutorId" });
            RenameColumn(table: "dbo.Subjects", name: "TutorId", newName: "Tutor_TutorId");
            AlterColumn("dbo.Subjects", "Tutor_TutorId", c => c.Int());
            CreateIndex("dbo.Subjects", "Tutor_TutorId");
            AddForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors", "TutorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_TutorId" });
            AlterColumn("dbo.Subjects", "Tutor_TutorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Subjects", name: "Tutor_TutorId", newName: "TutorId");
            CreateIndex("dbo.Subjects", "TutorId");
            AddForeignKey("dbo.Subjects", "TutorId", "dbo.Tutors", "TutorId", cascadeDelete: true);
        }
    }
}
