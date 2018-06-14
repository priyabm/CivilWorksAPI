using CivilWorks.Common;
using CivilWorks.DataRepository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = CivilWorks.BusinessObjects;
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

        public Object Login(T data, int? nestingLevels = default(int?), bool includeAllVersions = false, bool applySecurity = false)
        {
            try
            {
                if (data == null)
                    throw new GbException(string.Format("Null Object cannot be saved. ObjectType : {0}", typeof(T).Name));

                //Update CreatedBy and other tracking fields to child entities

                BaseEntityRepo baseRepo = RepoFactory.GetRepo<T>(dbContextProvider.GetDBContext());

                //List<MIDAS.GBX.BusinessObjects.BusinessValidation> validationResults = baseRepo.Validate(data);
                //if (validationResults.Count > 0)
                //{
                //    return new ErrorObject { ErrorMessage = "Please check error object for more details", errorObject = validationResults, ErrorLevel = ErrorLevel.Validation };
                //}
                //else
                //{
                    var gbdata = baseRepo.Login(data);
                    return gbdata;
               // }
            }

            catch (GbException gbe)
            {
                //LogManager.LogErrorMessage(gbe.Message, 0, (GbObject)(object)(entity));
                throw;
            }
            catch (Exception ex)
            {
                //LogManager.LogErrorMessage(ex.Message, 0, (MaestroObject)(object)(entity));
                throw new GbException(string.Format("An unknown Error occurred while saving {0} [{1}]","",  ex.Message));
            }
        }
        
        #region Save
        public Object Save(T data)
        {
            try
            {
                //var gbObject = (GbObject)(object)entity;
                if (data == null)
                    throw new GbException(string.Format("Null Object cannot be saved. ObjectType : {0}", typeof(T).Name));

                //Update CreatedBy and other tracking fields to child entities

                BaseEntityRepo baseRepo = RepoFactory.GetRepo<T>(dbContextProvider.GetDBContext());

               
                    var gbdata = baseRepo.Save(data);
                    return gbdata;
                
            }        
            catch (GbException gbe)
            {
                return gbe;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        #endregion

        public Object ValidateInvitation(T data, int? nestingLevels = default(int?), bool includeAllVersions = false, bool applySecurity = false)
        {
            try
            {
                if (data == null)
                    throw new GbException(string.Format("Null Object cannot be saved. ObjectType : {0}", typeof(T).Name));

                //Update CreatedBy and other tracking fields to child entities

                BaseEntityRepo baseRepo = RepoFactory.GetRepo<T>(dbContextProvider.GetDBContext());

                //List<MIDAS.GBX.BusinessObjects.BusinessValidation> validationResults = baseRepo.Validate(data);
                //if (validationResults.Count > 0)
                //{
                //    return new ErrorObject { ErrorMessage = "Please check error object for more details", errorObject = validationResults, ErrorLevel = ErrorLevel.Validation };
                //}
                //else
                //{
                    var gbSavedObject = baseRepo.ValidateInvitation(data);

                    return gbSavedObject;
               // }


            }          
            catch (GbException gbe)
            {
                return gbe;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        #region ResetPassword
        public Object ResetPassword(T data)
        {
            try
            {
                //var gbObject = (GbObject)(object)entity;
                if (data == null)
                    throw new GbException(string.Format("Null Object cannot be saved. ObjectType : {0}", typeof(T).Name));

                //Update CreatedBy and other tracking fields to child entities

                BaseEntityRepo baseRepo = RepoFactory.GetRepo<T>(dbContextProvider.GetDBContext());

                //List<MIDAS.GBX.BusinessObjects.BusinessValidation> validationResults = baseRepo.Validate(data);
                //if (validationResults.Count > 0)
                //{
                //    return new ErrorObject { ErrorMessage = "Please check error object for more details", errorObject = validationResults, ErrorLevel = ErrorLevel.Validation };
                //}
                //else
                //{
                    var gbdata = baseRepo.ResetPassword(data);
                    return gbdata;
                //}
            }

          
            catch (GbException gbe)
            {
                return gbe;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        #endregion

    }
}
