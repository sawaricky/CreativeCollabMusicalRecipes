namespace CreativeCollabMusicalRecipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class academy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Academies",
                c => new
                    {
                        AcademyId = c.Int(nullable: false, identity: true),
                        AcademyName = c.String(),
                        AcademyAddress = c.String(),
                    })
                .PrimaryKey(t => t.AcademyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Academies");
        }
    }
}
