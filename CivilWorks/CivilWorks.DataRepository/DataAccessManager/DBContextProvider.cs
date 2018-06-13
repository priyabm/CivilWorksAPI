using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;

namespace CivilWorks.DataRepository.DataAccessManager
{
    public class DBContextProvider : IDBContextProvider
    {
        EO.CivilWorksEntities IDBContextProvider.GetDBContext()
        {
            return new EO.CivilWorksEntities();
        }
    }
}
