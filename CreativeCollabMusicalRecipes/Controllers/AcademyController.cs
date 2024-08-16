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
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AcademyController()
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
            string url = "AcademyData/ListAcademy";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<AcademyDto> academys = response.Content.ReadAsAsync<IEnumerable<AcademyDto>>().Result;
            Debug.WriteLine("Number of instructors received ");
            Debug.WriteLine(academys.Count());

            return View(academys);
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

        // GET: Instructor/NewInstructor
        [Authorize(Roles = "MusicAdmin")]
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
        [Authorize(Roles = "MusicAdmin")]
        public ActionResult Create(Academy academy)
        {
            Debug.WriteLine("the json payload is :");
            //objective: add a new Instructor into our system using the API
            //curl -H "Content-Type:application/json" -d @instructor.json https://localhost:44363/api/InstructorData/AddInstructor
            string url = "AcademyData/AddAcademy";

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
            string url = "AcademyData/FindAcademy/" + id;
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
        [Authorize(Roles = "MusicAdmin")]
        public ActionResult Edit(int id)
        {
            // Objective: Communicate with our instructor data API to edit one instructor
            string url = "AcademyData/FindAcademy/" + id;
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
        [Authorize(Roles = "MusicAdmin")]
        public ActionResult Update(int id, Academy academy)
        {
            try
            {

                Debug.WriteLine("The new academy info is:");
                Debug.WriteLine(academy.AcademyName);
                Debug.WriteLine(academy.AcademyAddress);


                    string url = "AcademyData/UpdateAcademy/" + id;

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
        [Authorize(Roles = "MusicAdmin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "AcademyData/FindAcademy/" + id;
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
        [Authorize(Roles = "MusicAdmin")]
        public ActionResult Delete(int id)
        {
            string url = "AcademyData/DeleteAcademy/" + id;
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