using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;

namespace CivilWorks.DataRepository.EntityRepository
{
    internal class BaseEntityRepo
    {
        internal EO.CivilWorksEntities _context;

        public BaseEntityRepo(EO.CivilWorksEntities context)
        {
            _context = context;
        }

        public virtual Object Get<T>()
        {
            throw new NotImplementedException();
        }
    }
}
