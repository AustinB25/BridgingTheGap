namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update28 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_TutorId" });
            CreateTable(
                "dbo.TutorSubjects",
                c => new
                    {
                        Tutor_TutorId = c.Int(nullable: false),
                        Subject_SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tutor_TutorId, t.Subject_SubjectId })
                .ForeignKey("dbo.Tutors", t => t.Tutor_TutorId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectId, cascadeDelete: true)
                .Index(t => t.Tutor_TutorId)
                .Index(t => t.Subject_SubjectId);
            
            DropColumn("dbo.Subjects", "Tutor_TutorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Tutor_TutorId", c => c.Int());
            DropForeignKey("dbo.TutorSubjects", "Subject_SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.TutorSubjects", "Tutor_TutorId", "dbo.Tutors");
            DropIndex("dbo.TutorSubjects", new[] { "Subject_SubjectId" });
            DropIndex("dbo.TutorSubjects", new[] { "Tutor_TutorId" });
            DropTable("dbo.TutorSubjects");
            CreateIndex("dbo.Subjects", "Tutor_TutorId");
            AddForeignKey("dbo.Subjects", "Tutor_TutorId", "dbo.Tutors", "TutorId");
        }
    }
}
