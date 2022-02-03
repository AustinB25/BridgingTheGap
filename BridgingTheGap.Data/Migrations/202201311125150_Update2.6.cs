namespace BridgingTheGap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Student_StudentId", c => c.Int());
            CreateIndex("dbo.Subjects", "Student_StudentId");
            AddForeignKey("dbo.Subjects", "Student_StudentId", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Subjects", new[] { "Student_StudentId" });
            DropColumn("dbo.Subjects", "Student_StudentId");
        }
    }
}
