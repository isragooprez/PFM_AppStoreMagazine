using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazine.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> findAll();
        T find(int id);
        bool Create(T entidad);
        bool Edit(T entidad);
        bool Delete(int id);
    }
}
