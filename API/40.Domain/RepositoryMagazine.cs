using API._50.Domain.Core;
using API._80.Infraestructure.Data.Core;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace API._40.Domain
{
    public class RepositoryMagazine : IRepositoryMagazine
    {
        private Repository<Magazine> _repository = new Repository<Magazine>(new MagazinesDBEntities());

        public void Add(Magazine entidad)
        {
            _repository.Add(entidad);
            _repository.Save();
        }


        public IQueryable<Magazine> AsQueryable()
        {
            return _repository.AsQueryable();
        }

        public Magazine Delete(Magazine entidad)
        {
            Magazine Magazine = _repository.Delete(entidad);
            _repository.Save();
            return Magazine;
        }

        public IEnumerable<Magazine> Find(Expression<Func<Magazine, bool>> predicado)
        {
            return _repository.Find(predicado);
        }


        public IEnumerable<Magazine> FindAll()
        {
            return _repository.FindAll().ToList();
        }

        public Magazine FindById(Expression<Func<Magazine, bool>> predicado)
        {
            return _repository.FindById(predicado);
        }


        public Magazine FindOne(Expression<Func<Magazine, bool>> predicado)
        {
            return _repository.FindOne(predicado);
        }


        public IEnumerable<Magazine> GetByIdUser(string idUser)
        {
            return _repository.Find(magaz => magaz.UserId == idUser);
        }

        public IEnumerable<Magazine> GetByUrl(string url, string idUser)
        {
            return _repository.Find(magaz => magaz.Url == url && magaz.UserId== idUser);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Magazine entidad)
        {
            _repository.Update(entidad);
            _repository.Save();
        }
    }
}