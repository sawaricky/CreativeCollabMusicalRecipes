namespace CreativeCollabMusicalRecipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeRecipeIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Lessons", new[] { "RecipeId" });
            AlterColumn("dbo.Lessons", "RecipeId", c => c.Int());
            CreateIndex("dbo.Lessons", "RecipeId");
            AddForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes", "RecipeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Lessons", new[] { "RecipeId" });
            AlterColumn("dbo.Lessons", "RecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "RecipeId");
            AddForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
        }
    }
}
