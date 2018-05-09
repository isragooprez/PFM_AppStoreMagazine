using API.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using API.Services;
using System.Net;

namespace API.Controllers
{
    public class MagazinesController : ApiController
    {

        private MagazinesServices magazineService = new MagazinesServices();

        // GET: api/Magazines
        public IQueryable<Magazine> GetMagazines()
        {
            return magazineService.GetMagazines();
        }

        // GET: api/Magazines/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult GetMagazine(int id)
        {
            Magazine magazine = magazineService.GetMagazine(id);
            if (magazine == null)
            {
                return NotFound();
            }

            return Ok(magazine);
        }

        // PUT: api/Magazines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMagazine(int id, Magazine magazine)
        {
            if (id != magazine.Id)
            {
                return BadRequest();
            }

            try
            {
                magazineService.PutMagazine(id, magazine);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!magazineService.MagazineExists(id))
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

        // POST: api/Magazines
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult PostMagazine(Magazine magazine)
        {
            magazineService.PostMagazine(magazine);

            return CreatedAtRoute("DefaultApi", new { id = magazine.Id }, magazine);
        }

        // DELETE: api/Magazines/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult DeleteMagazine(int id)
        {

            Magazine magazine = magazineService.GetMagazine(id);
            if (magazine == null)
            {
                return NotFound();
            }

            var result = magazineService.DeleteMagazine(magazine);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(magazine);
        }


    }
}