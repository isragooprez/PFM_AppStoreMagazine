using API._50.Domain.Core;
using API._80.Infraestructure.Data.Core;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API._80.Infraestructure.DB.Data.Core
{
    public class RepositoryUser : IRepositoryUser
    {
        private Repository<User> _repository = new Repository<User>(new MagazinesDBEntities());

        public void Add(User entidad)
        {
            _repository.Add(entidad);
            _repository.Save();
        }


        public IQueryable<User> AsQueryable()
        {
            return _repository.FindAll().AsQueryable();
        }

        public User Delete(User entidad)
        {
            User User = _repository.Delete(entidad);
            _repository.Save();
            return User;
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicado)
        {
            return _repository.Find(predicado);
        }


        public IEnumerable<User> FindAll()
        {
            return _repository.FindAll().ToList();
        }

        public User FindById(Expression<Func<User, bool>> predicado)
        {
            return _repository.FindById(predicado);
        }


        public User FindOne(Expression<Func<User, bool>> predicado)
        {
            return _repository.FindOne(predicado);
        }


        public void GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(User entidad)
        {
            _repository.Update(entidad);
            _repository.Save();
        }
    }
}
