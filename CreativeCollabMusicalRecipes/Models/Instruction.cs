using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCollabMusicalRecipes.Models
{
    public class Instruction
    {
        [Key]
        public int InstructionId { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }

        [ForeignKey("Recipe")]

        // An Instruction is part of one Recipe
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
    public class InstructionDto
    {
        public int InstructionId { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; }
    }
}