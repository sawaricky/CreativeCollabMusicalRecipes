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
    public class AcademyController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        // GET: Academy/List
        /// <summary>
        /// To list the data from the database for the academies
        /// </summary>
        /// <example>
        /// https://localhost:44300/api/AcademyData/ListAcademy
        /// </example>
        /// <returns>This will communicate with the AcademyData API and retrieve the academy data, then display its details in the view</returns>
        public ActionResult List()
        {

            //objective: communivate with out instructor data api to retrieve a list of INstructors
            //curl https://localhost:44363/api/AcademyData/ListAcademy
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/ListAcademy";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<AcademyDto> academys = response.Content.ReadAsAsync<IEnumerable<AcademyDto>>().Result;
            Debug.WriteLine("Number of instructors received ");
            Debug.WriteLine(academys.Count());

            return View(academys);
        }
        // GET: Instructor/NewInstructor
        public ActionResult New()
        {
            return View();
        }
        // POST: Academy/Create
        /// <summary>
        /// To display a form for adding a new academy
        /// </summary>
        /// <example>
        /// POST request to https://localhost:44300/api/AcademyData/AddAcademy with academy data.
        /// </example>
        /// <param name="academy">The academy object containing details to be added.</param>
        /// <returns>Redirects to the List view if successful, otherwise redirects to the Error view.</returns>
        [HttpPost]
        public ActionResult Create(Academy academy)
        {
            Debug.WriteLine("the json payload is :");
            //objective: add a new Instructor into our system using the API
            //curl -H "Content-Type:application/json" -d @instructor.json https://localhost:44363/api/InstructorData/AddInstructor
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/AddAcademy";

            string jsonpayload = jss.Serialize(academy);

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
        /// To retrieve the details of a specific academy by its ID
        /// </summary>
        /// <param name="id">The ID of the academy</param>
        /// <example>
        /// https://localhost:44300/api/AcademyData/FindAcademy/5
        /// </example>
        /// <returns>This will communicate with the AcademyData API and retrieve the details of the specified academy, then display its details in the view</returns>
        public ActionResult Details(int id)
        {
            // Objective: Communicate with our instructor data API to retrieve one instructor
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/FindAcademy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AcademyDto selectedAcademy = response.Content.ReadAsAsync<AcademyDto>().Result;
            Debug.WriteLine("Academy received ");

            return View(selectedAcademy);
        }
        // POST: Academy/Edit/5
        /// <summary>
        /// To update the details of an existing academy in the database
        /// </summary>
        /// <param name="id">The ID of the academy to update</param>
        /// <example>
        /// https://localhost:44363/api/AcademyData/UpdateAcademy/5
        /// </example>
        /// <returns>If successful, this will redirect to the index view. If an error occurs, it will return to the edit view.</returns>
        public ActionResult Edit(int id)
        {
            // Objective: Communicate with our instructor data API to edit one instructor
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/FindAcademy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            AcademyDto selectedAcademy = response.Content.ReadAsAsync<AcademyDto>().Result;
            Debug.WriteLine("Academy received ");

            return View(selectedAcademy);
        }
        // POST: Academy/Update
        /// <summary>
        /// updates an existing academy by submitting updated data 
        /// </summary>
        /// <param name="id">The ID of the academy to be updated.</param>
        /// <param name="academy">The academy object containing updated details.</param>
        /// <returns>Redirects to the List view if successful, otherwise redirects to the Error view.</returns>
        /// <example>
        /// POST request to https://localhost:44363/api/AcademyData/UpdateAcademy/{id} with updated academy data.
        /// </example>

        [HttpPost]
        public ActionResult Update(int id, Academy academy)
        {
            try
            {

                Debug.WriteLine("The new academy info is:");
                Debug.WriteLine(academy.AcademyName);
                Debug.WriteLine(academy.AcademyAddress);


                HttpClient client = new HttpClient();
                string url = "https://localhost:44363/api/AcademyData/UpdateAcademy/" + id;

                string jsonpayload = jss.Serialize(academy);
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
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return View();
            }

        }
        // GET: Academy/Delete/5
        /// <summary>
        /// To display a confirmation view for deleting an academy
        /// </summary>
        /// <param name="id">The ID of the academy to delete</param>
        /// <example>
        /// https://localhost:44300/api/AcademyData/DeleteAcademy/5
        /// </example>
        /// <returns>This will display a confirmation view to the user for deleting the specified academy</returns>
        public ActionResult DeleteConfirm(int id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/FindAcademy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AcademyDto selectedAcademy = response.Content.ReadAsAsync<AcademyDto>().Result;
            return View(selectedAcademy);
        }

        // POST: Academy/Delete/5
        /// <summary>
        /// To delete an existing academy from the database
        /// </summary>
        /// <param name="id">The ID of the academy to delete</param>
        /// <example>
        /// https://localhost:44300/api/AcademyData/DeleteAcademy/5
        /// </example>
        /// <returns>If successful, this will redirect to the index view. If an error occurs, it will return to the delete confirmation view.</returns>
        // POST: Academy/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/DeleteAcademy/" + id;
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
    }
}