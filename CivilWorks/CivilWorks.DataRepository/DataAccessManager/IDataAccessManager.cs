using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilWorks.DataRepository.DataAccessManager
{
    public interface IDataAccessManager<T>
    {
        object Get();
    }
}
