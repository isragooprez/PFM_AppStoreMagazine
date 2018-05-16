using API._50.Dominio.Core;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API._50.Domain.Core
{
    public interface IRepositoryMagazine:IRepository<Magazine>
    {

        // Definimos metodos que sean unicos para un repositorio en especifico.
        void GetByCountry(string country);
        void GetByPublisher(string publisher);

    }
}