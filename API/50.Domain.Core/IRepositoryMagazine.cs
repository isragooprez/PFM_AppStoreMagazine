using API._50.Domain.Core;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace API._50.Domain.Core
{
    public interface IRepositoryMagazine:IRepository<Magazine>
    {

        // Definimos metodos que sean unicos para un repositorio en especifico.
        IEnumerable<Magazine> GetByUrl(string url, string idUser);
        IEnumerable<Magazine> GetByIdUser(string idUser);

    }
}