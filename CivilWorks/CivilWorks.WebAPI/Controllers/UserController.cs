using BO = CivilWorks.BusinessObjects;
using CivilWorks.WebAPI.RequestHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CivilWorks.WebAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IRequestHandler<BO.User> RequestHandler;

        public UserController()
        {
            RequestHandler = new APIRequestHandler<BO.User>();
        }

        // GET: api/User
        public HttpResponseMessage Get()
        {
            //return new string[] { "value1", "value2" };
            return RequestHandler.Get(Request);
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
