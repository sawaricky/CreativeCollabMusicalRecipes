using CreativeCollabMusicalRecipes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CreativeCollabMusicalRecipes.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            var totals = await GetTotalsAsync();
            return View(totals);
        }

        private async Task<TotalsViewModel> GetTotalsAsync()
        {
            var totalsViewModel = new TotalsViewModel();

            // Fetch total lessons
            var lessonResponse = await client.GetAsync("https://localhost:44363/api/LessonData/CountLessons");
            if (lessonResponse.IsSuccessStatusCode)
            {
                totalsViewModel.TotalLessons = await lessonResponse.Content.ReadAsAsync<int>();
            }

            // Fetch total recipes
            var recipeResponse = await client.GetAsync("https://localhost:44363/api/RecipeData/CountRecipes");
            if (recipeResponse.IsSuccessStatusCode)
            {
                totalsViewModel.TotalRecipes = await recipeResponse.Content.ReadAsAsync<int>();
            }

            // Fetch total instructors
            var instructorResponse = await client.GetAsync("https://localhost:44363/api/InstructorData/CountInstructors");
            if (instructorResponse.IsSuccessStatusCode)
            {
                totalsViewModel.TotalInstructors = await instructorResponse.Content.ReadAsAsync<int>();
            }

            return totalsViewModel;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}