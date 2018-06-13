using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CivilWorks.DataRepository.DataAccessManager;
using System.Net;

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
    }
}