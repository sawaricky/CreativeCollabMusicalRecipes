namespace CreativeCollabMusicalRecipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstructorandLesson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        InstructorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        InstructorNumber = c.String(),
                        HireDate = c.DateTime(nullable: false),
                        Wages = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AcademyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstructorId)
                .ForeignKey("dbo.Academies", t => t.AcademyId, cascadeDelete: true)
                .Index(t => t.AcademyId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        LessonName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Instructors", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.InstructorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Instructors", "AcademyId", "dbo.Academies");
            DropIndex("dbo.Lessons", new[] { "InstructorId" });
            DropIndex("dbo.Instructors", new[] { "AcademyId" });
            DropTable("dbo.Lessons");
            DropTable("dbo.Instructors");
        }
    }
}
