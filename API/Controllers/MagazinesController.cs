using API.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Net;
using API._80.Infraestructure.Data.Core;
using API._50.Domain.Core;
using System.Collections;
using API.Content.Utils;
using System.Configuration;

namespace API.Controllers
{
    public class MagazinesController : ApiController
    {

        IFactoryDAO factoryDAO = new FactoryDAO();
        IRepositoryMagazine repositoryMagazine;

        //IoC Inyection
        //IFactoryDAO factoryDAO;
        //public MagazinesController(IFactoryDAO _factoryDAO)
        //{
        //    factoryDAO = _factoryDAO;
        //}

        //private readonly IRepositoryMagazine repositoryMagazine;
        public MagazinesController()
        {
        }
        public MagazinesController(IRepositoryMagazine _repositoryMagazine)
        {
            repositoryMagazine = _repositoryMagazine;
        }

        /// <summary>
        /// Collect information stored by the user.
        /// </summary>
        /// <returns>Get all journals stored by the user.</returns>
        // GET: api/Magazines
        public IQueryable<Magazine> GetMagazines()
        {
            return factoryDAO.GetRepositoryMagazine().AsQueryable();
        }
        /// <summary>
        /// Remove magazine.
        /// </summary>
        /// <param name="id">Magazine identifier</param>
        /// <returns>Rewrites the elminated magazine</returns>
        // GET: api/Magazines/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult GetMagazine(int id)
        {
            Magazine magazine = factoryDAO.GetRepositoryMagazine().FindById(magaz => magaz.Id == id);
            if (magazine == null)
            {
                return NotFound();
            }

            return Ok(magazine);
        }




        /// <summary>
        /// Search magazine by user ID. 
        /// </summary>
        /// <param name="idUser">User Identifier</param>
        /// <returns>Returns the user's magazine</returns>
        // GET: api/Magazines/User/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult GetMagazinesByIdUser(string idUser)
        {
            IEnumerable magazine = factoryDAO.GetRepositoryMagazine().GetByIdUser(idUser);
            if (magazine == null)
            {
                return NotFound();
            }

            return Ok(magazine);
        }
        /// <summary>
        /// Get the review given the user url in session.
        /// </summary>
        /// <param name="url">Magazine Url</param>
        /// <param name="idUser">User Identifier</param>
        /// <returns></returns>
        // GET: api/Magazines/Magazine/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult GetMagazinesByUrl(string url, string idUser)
        {
            IEnumerable magazine = factoryDAO.GetRepositoryMagazine().GetByUrl(ConfigurationManager.AppSettings.Get("BASE_URL") + Utils.UrlDecode(url), idUser);
            if (magazine == null)
            {
                return NotFound();
            }
            return Ok(magazine);
        }
        /// <summary>
        /// Partial update of a magazine.
        /// </summary>
        /// <param name="id">Magazine identifier</param>
        /// <param name="magazine">Object magazine </param>
        /// <returns></returns>
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
                factoryDAO.GetRepositoryMagazine().Update(magazine);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagazineExists(id))
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
        /// Create a magazine
        /// </summary>
        /// <param name="magazine">Object magazine </param>
        /// <returns>Object magazine</returns>
        // POST: api/Magazines
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult PostMagazine(Magazine magazine)
        {
            factoryDAO.GetRepositoryMagazine().Add(magazine);

            return CreatedAtRoute("DefaultApi", new { id = magazine.Id }, magazine);
        }
        /// <summary>
        /// Delete magazine
        /// </summary>
        /// <param name="id">Magazine identifier</param>
        /// <returns>Returns the elminated magazine</returns>
        // DELETE: api/Magazines/5
        [ResponseType(typeof(Magazine))]
        public IHttpActionResult DeleteMagazine(int id)
        {
            Magazine magazine = factoryDAO.GetRepositoryMagazine().FindById(magaz => magaz.Id == id);
            if (magazine == null)
            {
                return NotFound();
            }

            factoryDAO.GetRepositoryMagazine().Delete(magazine);
            if (magazine == null)
            {
                return NotFound();
            }

            return Ok(magazine);
        }

        private bool MagazineExists(int id)
        {
            if (factoryDAO.GetRepositoryMagazine().FindById(magaz => magaz.Id == id).Id > 0)
                return true;
            return false;
        }

    }
}