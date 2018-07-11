using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API._40.Domain;
using API._50.Domain.Core;
using API._80.Infraestructure.DB.Data.Core;

namespace API._80.Infraestructure.Data.Core
{
    public class FactoryDAO:IFactoryDAO
    {
        private IRepositoryUser repositoryUsers;
        private IRepositoryMagazine repositoryMagazine;


        public override IRepositoryUser GetRepositoryUser()
        {
            if (repositoryUsers == null)
            {
                repositoryUsers = new RepositoryUser();
            }
            return repositoryUsers;
        }

        public override IRepositoryMagazine GetRepositoryMagazine()
        {
            if (repositoryMagazine == null)
            {
                repositoryMagazine = new RepositoryMagazine();
            }
            return repositoryMagazine;
        }
    }
}