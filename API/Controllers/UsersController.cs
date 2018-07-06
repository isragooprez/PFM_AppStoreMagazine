using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using API._50.Domain.Core;
using API._50.Dominio.Core;
using API._80.Infraestructure.Data.Core;
using API.Models;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        IFactoryDAO factoryDAO = new FactoryDAO();
        IRepositoryUser repositoryUser;
        public UsersController()
        {
        }
        public UsersController(IRepositoryUser _repositoryUser)
        {
            repositoryUser = _repositoryUser;
        }

        /// <summary>
        /// Get all application users
        /// </summary>
        /// <returns>List of user type objects</returns>
        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return factoryDAO.GetRepositoryUser().AsQueryable();
        }
        /// <summary>
        /// You get a user given your id
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>User type object</returns>
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = factoryDAO.GetRepositoryUser().FindById(users => users.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        /// <summary>
        /// Partial update of a User
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="user">User type object</param>
        /// <returns>Returns the updated user</returns>
        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != user.Id)
                {
                    return BadRequest();
                }

                User _user = factoryDAO.GetRepositoryUser().FindById(users => users.Id == id);

                factoryDAO.GetRepositoryUser().Update(_user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        /// Create a User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns the created user object</returns>
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            factoryDAO.GetRepositoryUser().Add(user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }
        /// <summary>
        /// Remove User by Id
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Returns the deleted user object</returns>
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = factoryDAO.GetRepositoryUser().FindById(users => users.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            factoryDAO.GetRepositoryUser().Delete(user);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            if (factoryDAO.GetRepositoryUser().FindById(users => users.Id == id) != null)
                return true;
            return false;
        }
    }
}