using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API._50.Dominio.Core;
using API._80.Infraestructure.Data.Core;
using API.Models;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        IFactoryDAO factoryDAO = new FactoryDAO();


        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return factoryDAO.GetRepositoryUser().AsQueryable();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = factoryDAO.GetRepositoryUser().FindById(users => users.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
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

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
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

        private bool UserExists(int id)
        {
            if (factoryDAO.GetRepositoryUser().FindById(users => users.Id == id).Id > 0)
                return true;
            return false;
        }
    }
}