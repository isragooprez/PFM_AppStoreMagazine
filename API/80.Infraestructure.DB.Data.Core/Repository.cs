using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using API._50.Dominio.Core;

namespace API._80.Infraestructure.Data.Core
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly MagazinesDBEntities _context;

        public Repository(MagazinesDBEntities context)
        {
            if (context == null)
                throw new ArgumentNullException("Contexto", "La unidad de trabajo no puede ser null.");
            _context = context;
        }

        public void Add(T entidad)
        {
            if (_context.Entry<T>(entidad).State != System.Data.Entity.EntityState.Detached)
                _context.Entry<T>(entidad).State = System.Data.Entity.EntityState.Added;
            else
                _context.Set<T>().Add(entidad);
        }

        public IQueryable<T> AsQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T Delete(T entidad)
        {
            if (_context.Entry<T>(entidad).State != System.Data.Entity.EntityState.Detached)
                _context.Set<T>().Attach(entidad);
            _context.Entry<T>(entidad).State = System.Data.Entity.EntityState.Deleted;
            return _context.Set<T>().FirstOrDefault();

        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicado)
        {
            return _context.Set<T>().Where(predicado);
        }
        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>();
        }

        public T FindById(Expression<Func<T, bool>> predicado)
        {
            return _context.Set<T>().Where(predicado).FirstOrDefault();
        }

        public T FindOne(Expression<Func<T, bool>> predicado)
        {
            throw new NotImplementedException();
        }

        public void Update(T entidad)
        {
            if (_context.Entry<T>(entidad).State != System.Data.Entity.EntityState.Detached)
                _context.Set<T>().Attach(entidad);

            _context.Entry<T>(entidad).State = System.Data.Entity.EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}