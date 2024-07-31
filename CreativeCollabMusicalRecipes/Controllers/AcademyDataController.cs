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

        [ResponseType(typeof(Academy))]
        [HttpPost]
        [Route("api/AcademyData/AddAcademy")]
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

        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/AcademyData/UpdateAcademy/{id}")]
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
        private bool AcademyExists(int id)
        {
            return db.academys.Count(e => e.AcademyId == id) > 0;
        }

        [ResponseType(typeof(Instructor))]
        [HttpPost]
        [Route("api/AcademyData/DeleteAcademy/{id}")]
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
