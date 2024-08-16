namespace CreativeCollabMusicalRecipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relationalupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instructions", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            DropIndex("dbo.Instructions", new[] { "RecipeId" });
            DropIndex("dbo.Lessons", new[] { "RecipeId" });
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        Recipe_RecipeId = c.Int(nullable: false),
                        Ingredient_IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_RecipeId, t.Ingredient_IngredientId })
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientId, cascadeDelete: true)
                .Index(t => t.Recipe_RecipeId)
                .Index(t => t.Ingredient_IngredientId);
            
            CreateTable(
                "dbo.InstructionRecipes",
                c => new
                    {
                        Instruction_InstructionId = c.Int(nullable: false),
                        Recipe_RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instruction_InstructionId, t.Recipe_RecipeId })
                .ForeignKey("dbo.Instructions", t => t.Instruction_InstructionId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId, cascadeDelete: true)
                .Index(t => t.Instruction_InstructionId)
                .Index(t => t.Recipe_RecipeId);
            
            CreateTable(
                "dbo.LessonRecipes",
                c => new
                    {
                        Lesson_LessonID = c.Int(nullable: false),
                        Recipe_RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lesson_LessonID, t.Recipe_RecipeId })
                .ForeignKey("dbo.Lessons", t => t.Lesson_LessonID, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeId, cascadeDelete: true)
                .Index(t => t.Lesson_LessonID)
                .Index(t => t.Recipe_RecipeId);
            
            DropColumn("dbo.Ingredients", "RecipeId");
            DropColumn("dbo.Instructions", "RecipeId");
            DropColumn("dbo.Lessons", "RecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lessons", "RecipeId", c => c.Int());
            AddColumn("dbo.Instructions", "RecipeId", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredients", "RecipeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LessonRecipes", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.LessonRecipes", "Lesson_LessonID", "dbo.Lessons");
            DropForeignKey("dbo.InstructionRecipes", "Recipe_RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.InstructionRecipes", "Instruction_InstructionId", "dbo.Instructions");
            DropForeignKey("dbo.RecipeIngredients", "Ingredient_IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.RecipeIngredients", "Recipe_RecipeId", "dbo.Recipes");
            DropIndex("dbo.LessonRecipes", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.LessonRecipes", new[] { "Lesson_LessonID" });
            DropIndex("dbo.InstructionRecipes", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.InstructionRecipes", new[] { "Instruction_InstructionId" });
            DropIndex("dbo.RecipeIngredients", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.RecipeIngredients", new[] { "Recipe_RecipeId" });
            DropTable("dbo.LessonRecipes");
            DropTable("dbo.InstructionRecipes");
            DropTable("dbo.RecipeIngredients");
            CreateIndex("dbo.Lessons", "RecipeId");
            CreateIndex("dbo.Instructions", "RecipeId");
            CreateIndex("dbo.Ingredients", "RecipeId");
            AddForeignKey("dbo.Lessons", "RecipeId", "dbo.Recipes", "RecipeId");
            AddForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
            AddForeignKey("dbo.Instructions", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
        }
    }
}
