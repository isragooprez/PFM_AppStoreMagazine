using API._50.Dominio.Core;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API._50.Domain.Core
{
    public interface IRepositoryUser: IRepository<User>
    {
        // Definimos metodos que sean unicos para un repositorio en especifico.
        void GetByEmail(string email);

    }
}
