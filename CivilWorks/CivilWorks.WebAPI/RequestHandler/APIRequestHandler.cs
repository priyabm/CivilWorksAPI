using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CivilWorks.DataRepository.DataAccessManager;
using System.Net;
using  CivilWorks.BusinessObjects;

namespace CivilWorks.WebAPI.RequestHandler
{
    public class APIRequestHandler<T> : IRequestHandler<T>
    {
        private IDataAccessManager<T> dataAccessManager;

        public APIRequestHandler()
        {
            dataAccessManager = new APIDataAccessManager<T>();
        }

        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var objResult = dataAccessManager.Get();
            try
            {
                if (objResult != null)
                    return request.CreateResponse(HttpStatusCode.OK, objResult);
                else
                    return request.CreateResponse(HttpStatusCode.NotFound, objResult);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }
        }

        #region Login
        public HttpResponseMessage Login(HttpRequestMessage request, T gbObject)
        {
            User userBO = (User)(object)gbObject;
            if (userBO == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new ErrorObject { ErrorMessage = "User object can't be null", errorObject = "", ErrorLevel = ErrorLevel.Error });
            }
            var objResult = dataAccessManager.Login(gbObject);

            try
            {
                var res = objResult;
                return request.CreateResponse(HttpStatusCode.Created, objResult);

            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }
        }
        #endregion

        public HttpResponseMessage CreateGbObject(HttpRequestMessage request, T gbObject)
        {
            var objResult = dataAccessManager.Save(gbObject);

            try
            {
                var res = objResult;
                if (res != null)
                    return request.CreateResponse(HttpStatusCode.Created, res);
                else
                    return request.CreateResponse(HttpStatusCode.NotFound, res);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }
        }

        public HttpResponseMessage ValidateInvitation(HttpRequestMessage request, T gbObject)
        {
            UserPasswordActivation invitationBO = (UserPasswordActivation)(object)gbObject;
            if (invitationBO == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, new ErrorObject { ErrorMessage = "Invitation object can't be null", errorObject = "", ErrorLevel = ErrorLevel.Error });
            }
            var objResult = dataAccessManager.ValidateInvitation(gbObject);

            try
            {
                var res =objResult;
                if (res != null)
                    return request.CreateResponse(HttpStatusCode.Created, objResult);
                else
                    return request.CreateResponse(HttpStatusCode.NotFound, objResult);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }
        }


        public HttpResponseMessage ResetPassword(HttpRequestMessage request, T gbObject)
        {
            var objResult = dataAccessManager.ResetPassword(gbObject);

            try
            {
                var res = (object)objResult;
                if (res != null)
                    return request.CreateResponse(HttpStatusCode.Created, res);
                else
                    return request.CreateResponse(HttpStatusCode.NotFound, res);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, objResult);
            }
        }

    }
}