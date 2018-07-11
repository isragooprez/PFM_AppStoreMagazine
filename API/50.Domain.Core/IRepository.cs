using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace API._50.Domain.Core
{
    public interface IRepository<T> where T: class
    {

        void Add(T entidad);
        void Update(T entidad);
        T Delete(T entidad);
        void Save();

        IQueryable<T> AsQueryable();
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(Expression<Func<T,bool>>predicado);
        T FindOne(Expression<Func<T, bool>> predicado);
        T FindById(Expression<Func<T, bool>> predicado);



    }
}