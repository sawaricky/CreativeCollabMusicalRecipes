﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollabMusicalRecipes.Models.ViewModels
{
    public class EditInstruction
    {
        public InstructionDto SelectedInstruction { get; set; }
        public IEnumerable<RecipeDto> RecipeOptions { get; set; }
    }
}