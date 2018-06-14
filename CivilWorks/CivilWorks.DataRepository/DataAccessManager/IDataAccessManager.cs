using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = CivilWorks.BusinessObjects;

namespace CivilWorks.DataRepository.DataAccessManager
{
    public interface IDataAccessManager<T>
    {
        object Get();
        Object Login(T gbObject, int? nestingLevels = null, bool includeAllVersions = false, bool applySecurity = false);
        Object Save(T gbObject);
        Object ValidateInvitation(T gbObject, int? nestingLevels = null, bool includeAllVersions = false, bool applySecurity = false);
        Object ResetPassword(T gbObject);
    }
}
