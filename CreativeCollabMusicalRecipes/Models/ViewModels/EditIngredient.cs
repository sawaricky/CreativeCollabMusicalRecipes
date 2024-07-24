using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollabMusicalRecipes.Models.ViewModels
{
    public class EditIngredient
    {
        public IngredientDto SelectedIngredient { get; set; }
        public IEnumerable<RecipeDto> RecipeOptions { get; set; }
    }
}