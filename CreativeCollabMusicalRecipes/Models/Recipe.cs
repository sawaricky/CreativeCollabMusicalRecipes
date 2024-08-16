using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCollabMusicalRecipes.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CookingTime { get; set; }

        // A Recipe can have many Ingredients
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        // A Recipe can have many Instructions
        public virtual ICollection<Instruction> Instructions { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

    }
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CookingTime { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public List<InstructionDto> Instructions { get; set; }
        public List<LessonDto> Lessons { get; set; }

    }
}