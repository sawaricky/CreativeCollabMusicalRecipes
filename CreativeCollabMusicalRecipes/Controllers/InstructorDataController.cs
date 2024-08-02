using CreativeCollabMusicalRecipes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CreativeCollabMusicalRecipes.Controllers
{
    public class InstructorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Retrieves the details of an  instructors 
        /// </summary>
        /// <returns>an array of Instructor objects Dtos </returns>
        /// <example>
        /// Get: api/InstrumentLessonData/ListInstructors -> [{ "InstructorId": 1, "FirstName": "Akash"}{"LastName": "Sharma", "Wages": 12}] 
        /// </example>
        [HttpGet]
        [Route("api/InstructorData/ListInstructors")]
        public List<InstructorDto> ListInstructors()
        {
            // This is similar to Select * from Instructors
            List<Instructor> instructors = db.Instructor.ToList();
            List<InstructorDto> instructorDtos = new List<InstructorDto>();

            foreach (Instructor instructor in instructors)
            {
                InstructorDto dto = new InstructorDto
                {
                    HireDate = instructor.HireDate,
                    InstructorNumber = instructor.InstructorNumber,
                    LastName = instructor.LastName,
                    Wages = instructor.Wages,
                    FirstName = instructor.FirstName,
                    InstructorId = instructor.InstructorId,
                    AcademyId = instructor.AcademyId

                };

                instructorDtos.Add(dto);
            }

            return instructorDtos;
        }
        /// <summary>
        /// Retrieves the details of a specific instructor based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the instructor to retrieve.</param>
        /// <returns>An IHttpActionResult containing the instructor details if found, otherwise a NotFound result.</returns>
        /// <example>
        /// GET: /api/InstructorData/FindInstructor/5
        /// This will retrieve the instructor with ID 5 and return its details as an InstructorDto object.
        /// </example>
        [ResponseType(typeof(InstructorDto))]
        [HttpGet]
        [Route("api/InstructorData/FindInstructor/{id}")]

        public IHttpActionResult FindInstructor(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            InstructorDto instructorDto = new InstructorDto
            {
                InstructorId = instructor.InstructorId,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate,
                Wages = instructor.Wages,
                InstructorNumber = instructor.InstructorNumber,
                AcademyId = instructor.AcademyId,
                AcademyName = instructor.Academy.AcademyName
            };

            return Ok(instructorDto);

        }
        /// <summary>
        /// Updates the details of a specific Instructor by sending the provided data to the API.
        /// </summary>
        /// <param name="id">The ID of the Instructor to be updated.</param>
        /// <param name="InstructorDto">The Instructor DTO containing the updated details.</param>
        /// <returns>An IHttpActionResult indicating the result of the update operation.</returns>
        /// <example>
        /// POST: /api/InstrumentLessonData/UpdateInstructor/5
        /// This will update the instrument lesson with ID 5 using the provided details in the InstructorDto object.
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/InstructorData/UpdateInstructor/{id}")]
        [Authorize]
        public IHttpActionResult UpdateInstructor(int id, InstructorDto instructorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instructorDto.InstructorId)
            {
                return BadRequest();
            }

            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            instructor.FirstName = instructorDto.FirstName;
            instructor.LastName = instructorDto.LastName;
            instructor.HireDate = instructorDto.HireDate;
            instructor.Wages = instructorDto.Wages;
            instructor.InstructorNumber = instructorDto.InstructorNumber;
            instructor.AcademyId = instructorDto.AcademyId;
            db.Entry(instructor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Adds a new Instructor by sending the provided data to the API.
        /// </summary>
        /// <param name="Instructor">TheInstructor object containing the details to be added.</param>
        /// <returns>An IHttpActionResult indicating the result of the add operation. Returns Ok if successful, otherwise returns BadRequest with the model state.</returns>
        /// <example>
        /// POST: /api/InstrumentLessonData/AddInstructor
        /// This will add a new Instructor using the provided details in the Instructor object.
        /// </example>
        [ResponseType(typeof(Instructor))]
        [HttpPost]
        [Route("api/InstructorData/AddInstructor")]
        [Authorize]
        public IHttpActionResult AddInstructor(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Instructor.Add(instructor);
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Deletes a specific Instructor by sending a delete request to the API.
        /// </summary>
        /// <param name="id">The ID of the Instructor to be deleted.</param>
        /// <returns>An IHttpActionResult indicating the result of the delete operation. Returns Ok if successful, otherwise returns NotFound.</returns>
        /// <example>
        /// POST: /api/InstrumentLessonData/DeleteInstructor/2
        /// This will delete the Instructor with ID 2 from the database.
        /// </example>
        [ResponseType(typeof(Instructor))]
        [HttpPost]
        [Route("api/InstructorData/DeleteInstructor/{id}")]
        [Authorize]
        public IHttpActionResult DeleteInstructor(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            db.Instructor.Remove(instructor);
            db.SaveChanges();

            return Ok(instructor);
        }
        /// <summary>
        /// Checks if an Instructors with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the Instructors to check for existence.</param>
        /// <returns>True if an Instructors with the specified ID exists, otherwise false.</returns>
        /// <example>
        /// bool InstructorExists = InstructorExists(5);
        /// </example>
        private bool InstructorExists(int id)
        {
            return db.Instructor.Count(e => e.InstructorId == id) > 0;
        }

        [Route("api/InstructorData/CountInstructors")]
        [HttpGet]
        public IHttpActionResult CountInstructors()
        {
            int count = db.Instructor.Count();
            return Ok(count);
        }

    }
}