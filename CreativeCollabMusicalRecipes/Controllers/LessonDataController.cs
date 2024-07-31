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
    public class LessonDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves the details of a InstrumentLessons 
        /// </summary>
        /// <returns>an array of  lesson objects Dtos </returns>
        /// <example>
        /// Get: api/InstrumentLessonData/ListInstrumentLesson -> [{ "LessonId": 1, "LessonName": "Guitar"}{"LessonId": 2, "LessonName": "Piano"}] 
        /// </example>
        [HttpGet]
        [Route("api/LessonData/ListLesson")]
        public List<LessonDto> ListLessons()
        {
            // this is similar to Select * from InstrumentLesson
            List<Lesson> Lessons = db.Lesson.ToList();

            List<LessonDto> LessonDtos = new List<LessonDto>();

            foreach (Lesson Lesson in Lessons)
            {
                LessonDto Dto = new LessonDto();

                Dto.LessonName = Lesson.LessonName;
                Dto.StartDate = Lesson.StartDate;
                Dto.EndDate = Lesson.EndDate;
                Dto.LessonID = Lesson.LessonID;
                Dto.FirstName = Lesson.Instructor.FirstName;
                Dto.InstructorId = Lesson.Instructor.InstructorId;
                LessonDtos.Add(Dto);
            }

            return LessonDtos;
        }
       
        /// <summary>
        /// Retrieves the details of a specific instrument lesson based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to retrieve.</param>
        /// <returns>An IHttpActionResult containing the instrument lesson details if found, otherwise a NotFound result.</returns>
        /// <example>
        /// GET: /api/InstrumentLessonData/FindInstrumentLesson/5
        /// This will retrieve the instrument lesson with ID 5 and return its details as an InstrumentLessonDto object.
        /// </example>
        [ResponseType(typeof(LessonDto))]
        [HttpGet]
        [Route("api/LessonData/FindLesson/{id}")]
        public IHttpActionResult FindLesson(int id)
        {
            Lesson Lesson = db.Lesson.Find(id);
            if (Lesson == null)
            {
                return NotFound();
            }

            LessonDto LessonDto = new LessonDto
            {
                LessonID = Lesson.LessonID,
                LessonName = Lesson.LessonName,
                StartDate = Lesson.StartDate,
                EndDate = Lesson.EndDate,
                InstructorId = Lesson.InstructorId,
                FirstName = Lesson.Instructor?.FirstName,
                LastName = Lesson.Instructor?.LastName
            };

            return Ok(LessonDto);

        }
        /// <summary>
        /// Updates the details of a specific instrument lesson by sending the provided data to the API.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to be updated.</param>
        /// <param name="LessonDto">The instrument lesson DTO containing the updated details.</param>
        /// <returns>An IHttpActionResult indicating the result of the update operation.</returns>
        /// <example>
        /// POST: /api/InstrumentLessonData/UpdateInstrumentLesson/5
        /// This will update the instrument lesson with ID 5 using the provided details in the InstrumentLessonDto object.
        /// </example>
        //curl -d @instructor.json -H "Content-type:application/json" https://localhost:44300/api/LessData/UpdateLesson/1

        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/LessonData/UpdateLesson/{id}")]
        public IHttpActionResult UpdateLesson(int id, LessonDto LessonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != LessonDto.LessonID)
            {
                return BadRequest();
            }

            Lesson Lesson = db.Lesson.Find(id);
            if (Lesson == null)
            {
                return NotFound();
            }

            Lesson.LessonName = LessonDto.LessonName;
            Lesson.StartDate = LessonDto.StartDate;
            Lesson.EndDate = LessonDto.EndDate;
            Lesson.InstructorId = LessonDto.InstructorId;

            db.Entry(Lesson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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
        /// Adds a new instrument lesson by sending the provided data to the API.
        /// </summary>
        /// <param name="Lesson">The instrument lesson object containing the details to be added.</param>
        /// <returns>An IHttpActionResult indicating the result of the add operation. Returns Ok if successful, otherwise returns BadRequest with the model state.</returns>
        /// <example>
        /// POST: /api/InstrumentLessonData/AddInstrumentLesson
        /// This will add a new instrument lesson using the provided details in the InstrumentLesson object.
        /// </example>
        [ResponseType(typeof(Lesson))]
        [HttpPost]
        [Route("api/LessonData/AddLesson")]
        public IHttpActionResult AddLesson(Lesson Lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lesson.Add(Lesson);
            db.SaveChanges();

            return Ok();
        }
       
        /// <summary>
        /// Deletes a specific instrument lesson by sending a delete request to the API.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to be deleted.</param>
        /// <returns>An IHttpActionResult indicating the result of the delete operation. Returns Ok if successful, otherwise returns NotFound.</returns>
        /// <example>
        /// POST: /api/InstrumentLessonData/DeleteInstrumentLesson/2
        /// This will delete the instrument lesson with ID 2 from the database.
        /// </example>
        // POST: api/InstrumentLessonData/DeleteInstructor/2
        [ResponseType(typeof(Lesson))]
        [HttpPost]
        [Route("api/LessonData/DeleteLesson/{id}")]
        public IHttpActionResult DeleteLesson(int id)
        {
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }

            db.Lesson.Remove(lesson);
            db.SaveChanges();

            return Ok(lesson);
        }
        /// <summary>
        /// Checks if an instrument lesson with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the instrument lesson to check for existence.</param>
        /// <returns>True if an instrument lesson with the specified ID exists, otherwise false.</returns>
        /// <example>
        /// bool lessonExists = InstrumentLessonExists(5);
        /// </example>
        private bool LessonExists(int id)
        {
            return db.Lesson.Count(e => e.LessonID == id) > 0;
        }
    }
}