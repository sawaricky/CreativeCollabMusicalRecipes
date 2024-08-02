using CreativeCollabMusicalRecipes.Models;
using CreativeCollabMusicalRecipes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CreativeCollabMusicalRecipes.Controllers
{
    public class LessonController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static LessonController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44363/api/");
        }

        // GET: Instructor/List
        /// <summary>
        /// To list the data from the database for the instrument lessons
        /// </summary>
        /// <example>
        /// https://localhost:44300/api/InstrumentLessonData/ListInstrumentLesson
        /// </example>
        /// <returns>This will communicate with the InstrumentLessonData API and retrieve the InstrumentLesson and then display its details in the view</returns>
        // GET: Instructor
        public ActionResult List()
        {

            //objective: communivate with out instructor data api to retrieve a list of instrumetn lessons 
            //curl https://localhost:44363/api/LessonData/ListLesson

            string url = "LessonData/ListLesson";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<LessonDto> lessons = response.Content.ReadAsAsync<IEnumerable<LessonDto>>().Result;
            Debug.WriteLine("number of lessons received ");
            Debug.WriteLine(lessons.Count());

            return View(lessons);
        }
        /// <summary>
        /// Retrieves the details of a specific instrument lesson based on the provided ID.
        /// </summary>
        /// /// <param name="id">The ID of the instrument lesson to retrieve.</param>
        /// <example>
        /// GET: /InstrumentLesson/Details/5
        /// This will communicate with the InstrumentLessonData API and retrieve the instrument lesson with ID 5, and then display its details in the view.
        /// </example>
        public ActionResult Details(int id)
        {
            string url = "LessonData/FindLesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DetailsLesson ViewModel = new DetailsLesson();

            LessonDto selectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
            Debug.WriteLine("Lesson received ");
            ViewModel.SelectedLesson = selectedLesson;

            // Fetch related recipes
            if (selectedLesson.RecipeId.HasValue)
            {
                url = "RecipeData/FindRecipe/" + selectedLesson.RecipeId.Value;
                response = client.GetAsync(url).Result;
                RecipeDto relatedRecipe = response.Content.ReadAsAsync<RecipeDto>().Result;
                Debug.WriteLine("Recipe received");

                selectedLesson.Recipe = relatedRecipe; // Assign the recipe to the lesson
            }

            return View(ViewModel);
        }

        /// <summary>
        /// Retrieves the authentication token from the application's cookie and sets it in the HTTP client's headers.
        /// This method ensures that the HTTP client is prepared for making authenticated requests to the WebAPI.
        /// </summary>
        /// <example>
        /// Usage:
        /// <code>
        /// GetApplicationCookie();
        /// // Now the client has the authentication token set in the headers and can make authenticated requests.
        /// </code>
        /// </example>
        private void GetApplicationCookie()
        {
            string token = "";

            // Remove any existing cookies from the HTTP client's headers to prevent caching issues.
            client.DefaultRequestHeaders.Remove("Cookie");

            // Check if the user is authenticated before proceeding.
            if (!User.Identity.IsAuthenticated) return;

            // Retrieve the authentication cookie from the current HTTP context.
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            // Log the token for debugging purposes.
            Debug.WriteLine("Token Submitted is : " + token);

            // If a token is found, add it to the HTTP client's headers.
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);
            }

            return;
        }

        // GET: Instructor/New
        [Authorize]
        public ActionResult New()
        {
            string url = "recipedata/listrecipes/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<RecipeDto> RecipeOptions = response.Content.ReadAsAsync<IEnumerable<RecipeDto>>().Result;
            return View(RecipeOptions);
        }
        /// <summary>
        /// Creates a new lesson by posting the provided data to the API.
        /// </summary>
        /// /// <param name="lesson">The lesson object containing the details to be created.</param>
        /// <returns>A redirection to the list view if the creation is successful, otherwise it will redirect to an error view.
        /// </returns>
        /// /// <example>
        /// POST: /lesson/Create
        /// This will send a JSON payload containing the new instrument lesson details to the InstrumentLessonData API and create the instrument lesson in the system.
        /// </example>
        // POST: Instructor/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Lesson lesson)
        {
            if (lesson.RecipeId == null || lesson.RecipeId == 0)
            {
                lesson.RecipeId = null;
            }
            Debug.WriteLine("the json payload is :");

            //objective: add a new Lesson into our system using the API
            //curl -H "Content-Type:application/json" -d @InstrumentLesson.json https://localhost:44363/api/Lessondata/AddLesson 

            string url = "LessonData/AddLesson";

            string jsonpayload = jss.Serialize(lesson);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        /// <summary>
        /// Retrieves the details of a specific instrument lesson for editing based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to retrieve for editing.</param>
        /// <returns>A View displaying the details of the selected instrument lesson for editing.</returns>
        /// /// GET: /InstrumentLesson/Edit/5
        /// This will communicate with the InstrumentLessonData API to retrieve the instrument lesson with ID 5,
        /// and then display its details in the view for editing.
        /// </example>

        [Authorize]
        public ActionResult Edit(int id)
        {
            string lessonUrl = "LessonData/FindLesson/" + id;
            HttpResponseMessage lessonResponse = client.GetAsync(lessonUrl).Result;
            LessonDto selectedLesson = lessonResponse.Content.ReadAsAsync<LessonDto>().Result;

            string recipeUrl = "recipedata/listrecipes/";
            HttpResponseMessage recipeResponse = client.GetAsync(recipeUrl).Result;
            IEnumerable<RecipeDto> recipeOptions = recipeResponse.Content.ReadAsAsync<IEnumerable<RecipeDto>>().Result;

            ViewBag.RecipeOptions = recipeOptions;

            return View(selectedLesson);
        }
        /// <summary>
        /// Updates the details of a specific instrument lesson by posting the provided data to the API.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to be updated.</param>
        //// <param name="instrumentLesson">The instrument lesson object containing the updated details.</param>
        /// <returns>It is redirecting to the list view if the update is successful, otherwise redirects to an error view.</returns>
        /// <example>
        /// POST: /Instructor/Update/5
        /// This will send a JSON payload containing the updated instrument lesson details to the InstrumentLessonData API and update the instrument lesson in the system.
        /// </example>
        [HttpPost]
        [Authorize]
        public ActionResult Update(Lesson Lesson)
        {
            try
            {
                Debug.WriteLine("The new lesson info is:");
                Debug.WriteLine(Lesson.LessonName);
                Debug.WriteLine(Lesson.StartDate);
                Debug.WriteLine(Lesson.EndDate);
                Debug.WriteLine(Lesson.InstructorId);
                Debug.WriteLine(Lesson.RecipeId);

                // Serialize into JSON and send the request to the API

                if (Lesson.RecipeId == 0)
                {
                    Lesson.RecipeId = null;
                }

                string url = "LessonData/UpdateLesson/" + Lesson.LessonID;
                string jsonpayload = jss.Serialize(Lesson);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }

        }
        //both summary blocks of instrument and instructor below
        /// <summary>
        /// Deletes a specific instrument lesson by sending a delete request to the API.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to be deleted.</param>
        /// <returns>A redirection to the list view if the deletion is successful, otherwise redirects to an error view.</returns>
        /// <example>
        /// POST: /LessonData/Delete/5
        /// This will send a delete request to the LessonData API to remove the instrument lesson with ID 5 from the system.
        /// </example>
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {

            string url = "LessonData/DeleteLesson/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        /// <summary>
        /// Retrieves the details of a specific instrument lesson for confirmation of deletion based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to retrieve for deletion confirmation.</param>
        /// <returns>A View displaying the details of the selected instrument lesson for confirmation of deletion.</returns>
        /// <example>
        /// GET: /InstrumentLesson/DeleteConfirm/5
        /// This will communicate with the InstrumentLessonData API to retrieve the instrument lesson with ID 5, and then display its details in the view for deletion confirmation.
        /// </example>

        // GET: Instructor/Delete/5
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {

            string url = "LessonData/FindLesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LessonDto selectedlesson = response.Content.ReadAsAsync<LessonDto>().Result;
            return View(selectedlesson);
        }
    }
}