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
    public class AcademyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/AcademyData/ListAcademy
        /// <summary>
        /// Retrieves a list of academies from the database in DTO format.
        /// </summary>
        /// <returns>An enumerable list of AcademyDto objects showing academies.</returns>
        // GET: api/AcademyData/ListAcademy
        [HttpGet]
        [Route("api/AcademyData/ListAcademy")]
        public IEnumerable<AcademyDto> ListAcademy()
        {
            List<Academy> Academys = db.academys.ToList();
            List<AcademyDto> AcademyDtos = new List<AcademyDto>();

            foreach (Academy academy in Academys)
            {
                AcademyDto dto = new AcademyDto
                {
                    AcademyId = academy.AcademyId,
                    AcademyAddress = academy.AcademyAddress,
                    AcademyName = academy.AcademyName

                };

                AcademyDtos.Add(dto);
            }

            return AcademyDtos;
        }
        // POST: api/AcademyData/AddAcademy
        /// <summary>
        /// Adds a new academy to the database.
        /// </summary>
        /// <param name="academy">The academy object to add.</param>
        /// <returns>Returns the newly added academy.</returns>
        // POST: api/AcademyData
        [ResponseType(typeof(Academy))]
        [HttpPost]
        [Route("api/AcademyData/AddAcademy")]
        [Authorize(Roles = "MusicAdmin")]
        public IHttpActionResult AddAcademy(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.academys.Add(academy);
            db.SaveChanges();

            return Ok();
        }
        // GET: api/AcademyData/FindAcademy
        /// <summary>
        /// Retrieves details of a specific academy by its ID.
        /// </summary>
        /// <param name="id">The ID of the academy to retrieve.</param>
        /// <returns>Returns the details of the academy by the given ID in DTO format.</returns>
        [ResponseType(typeof(AcademyDto))]
        [HttpGet]
        [Route("api/AcademyData/FindAcademy/{id}")]
        public IHttpActionResult FindAcademy(int id)
        {
            Academy academy = db.academys.Find(id);
            if (academy == null)
            {
                return NotFound();
            }

            AcademyDto academyDto = new AcademyDto
            {
                AcademyId = academy.AcademyId,
                AcademyName = academy.AcademyName,
                AcademyAddress = academy.AcademyAddress
            };

            return Ok(academyDto);

        }
        // POST: api/AcademyData/UpdateAcademy
        /// <summary>
        /// Updates the details of an existing academy in the database.
        /// </summary>
        /// <param name="id">The ID of the academy to update.</param>
        /// <param name="academy">The updated academy object containing new details.</param>
        /// <returns>Returns a status code indicating the success of the update operation.</returns>
        // Post: api/AcademyData/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/AcademyData/UpdateAcademy/{id}")]
        [Authorize(Roles = "MusicAdmin")]
        public IHttpActionResult UpdateAcademy(int id, AcademyDto academyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != academyDto.AcademyId)
            {
                return BadRequest();
            }

            Academy academy = db.academys.Find(id);
            if (academy == null)
            {
                return NotFound();
            }

            academy.AcademyName = academyDto.AcademyName;
            academy.AcademyAddress = academyDto.AcademyAddress;
            db.Entry(academy).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademyExists(id))
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
        /// Checks if an academy exists in the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the academy to check.</param>
        /// <returns>True if an academy with the specified ID exists; otherwise, false.</returns>
        private bool AcademyExists(int id)
        {
            return db.academys.Count(e => e.AcademyId == id) > 0;
        }
        // POST: api/AcademyData/DeleteAcademy/5
        /// <summary>
        /// Deletes an existing academy from the database.
        /// </summary>
        /// <param name="id">The ID of the academy to delete.</param>
        /// <returns>Returns the deleted academy if successful; otherwise, returns a NotFound response.</returns>
        // DELETE: api/AcademyData/5
        [ResponseType(typeof(Instructor))]
        [HttpPost]
        [Route("api/AcademyData/DeleteAcademy/{id}")]
        [Authorize(Roles = "MusicAdmin")]
        public IHttpActionResult DeleteAcademy(int id)
        {
            Academy academy = db.academys.Find(id);
            if (academy == null)
            {
                return NotFound();
            }

            db.academys.Remove(academy);
            db.SaveChanges();

            return Ok(academy);
        }
    }
}
