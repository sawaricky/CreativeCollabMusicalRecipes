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
        public ActionResult DeleteConfirm(int id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44363/api/AcademyData/FindAcademy/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AcademyDto selectedAcademy = response.Content.ReadAsAsync<AcademyDto>().Result;
            return View(selectedAcademy);
        }


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