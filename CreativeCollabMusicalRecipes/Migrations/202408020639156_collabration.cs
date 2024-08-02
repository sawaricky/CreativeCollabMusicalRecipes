namespace CreativeCollabMusicalRecipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collabration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "RecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "RecipeId");
            AddForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Lessons", new[] { "RecipeId" });
            DropColumn("dbo.Lessons", "RecipeId");
        }
    }
}
