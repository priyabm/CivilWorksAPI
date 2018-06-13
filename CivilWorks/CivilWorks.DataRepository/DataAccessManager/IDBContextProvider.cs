using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;

namespace CivilWorks.DataRepository.DataAccessManager
{
    interface IDBContextProvider
    {
        EO.CivilWorksEntities GetDBContext();
    }
}
