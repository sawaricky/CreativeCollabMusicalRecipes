using CreativeCollabMusicalRecipes.Models;
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
        // GET: Lesson
        private JavaScriptSerializer jss = new JavaScriptSerializer();

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
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/LessonData/ListLesson";
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
            // Objective: Communicate with our instructor data API to retrieve one Instrument lesson
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/LessonData/FindLesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            LessonDto selectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
            Debug.WriteLine("InstrumentLesson received ");

            return View(selectedLesson);
        }

        // GET: Instructor/New
        public ActionResult New()
        {
            return View();
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
        public ActionResult Create(Lesson lesson)
        {
            Debug.WriteLine("the json payload is :");

            //objective: add a new Lesson into our system using the API
            //curl -H "Content-Type:application/json" -d @InstrumentLesson.json https://localhost:44363/api/Lessondata/AddLesson 
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/LessonData/AddLesson";

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

        public ActionResult Edit(int id)
        {
            // Objective: Communicate with our Lesson data API to Edit one lesson
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/LessonData/FindLesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            LessonDto selectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
            Debug.WriteLine("Instructor received ");

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
        public ActionResult Update(Lesson Lesson)
        {
            try
            {
                Debug.WriteLine("The new lesson info is:");
                Debug.WriteLine(Lesson.LessonName);
                Debug.WriteLine(Lesson.StartDate);
                Debug.WriteLine(Lesson.EndDate);
                Debug.WriteLine(Lesson.InstructorId);

                // Serialize into JSON and send the request to the API
                HttpClient client = new HttpClient();
                string url = "https://localhost:44363/api/LessonData/UpdateLesson/" + Lesson.LessonID;

                string jsonpayload = jss.Serialize(Lesson);
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
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/LessonData/DeleteLesson/" + id;
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
        public ActionResult DeleteConfirm(int id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/LessonData/FindLesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LessonDto selectedlesson = response.Content.ReadAsAsync<LessonDto>().Result;
            return View(selectedlesson);
        }
    }
}