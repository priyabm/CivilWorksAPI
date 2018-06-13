using CivilWorks.DataRepository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilWorks.DataRepository.DataAccessManager
{
    public class APIDataAccessManager<T> : IDataAccessManager<T>
    {
        IDBContextProvider dbContextProvider = null;

        public APIDataAccessManager()
        {
            dbContextProvider = new DBContextProvider();
        }

        //public APIDataAccessManager(IDBContextProvider dbContextProvider = null)
        //{
        //    this.dbContextProvider = dbContextProvider ?? new DBContextProvider();
        //}

        public object Get()
        {
            object result = null;
            try
            {
                BaseEntityRepo baseRepo = RepoFactory.GetRepo<T>(dbContextProvider.GetDBContext());
                result = baseRepo.Get<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
