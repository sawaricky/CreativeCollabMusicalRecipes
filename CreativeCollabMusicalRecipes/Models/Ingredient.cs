﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCollabMusicalRecipes.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientQuantity { get; set; }
        public string IngredientUnit { get; set; }

        [ForeignKey("Recipe")]

        // An Ingredient is part of one Recipe
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientQuantity { get; set; }
        public string IngredientUnit { get; set; }
        public List<RecipeDto> Recipes { get; set; }
    }
}