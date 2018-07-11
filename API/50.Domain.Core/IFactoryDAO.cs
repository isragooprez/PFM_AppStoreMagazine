using API._50.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API._50.Domain.Core
{
    public abstract class  IFactoryDAO
    {
        public static IFactoryDAO factory = null;

        public static void SetFactory(IFactoryDAO factory)
        {
            IFactoryDAO.factory = factory;
        }

        public static IFactoryDAO GetFactory()
        {
            return factory;
        }
        public abstract IRepositoryUser GetRepositoryUser();
        public abstract IRepositoryMagazine GetRepositoryMagazine();
    }
}
