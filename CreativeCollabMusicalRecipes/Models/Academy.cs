using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCollabMusicalRecipes.Models
{
    public class Academy
    {
        [Key]
        public int AcademyId { get; set; }

        public string AcademyName { get; set; }

        public string AcademyAddress { get; set; }

    }
    public class AcademyDto
    {
        public int AcademyId { get; set; }

        public string AcademyName { get; set; }

        public string AcademyAddress { get; set; }

    }
}