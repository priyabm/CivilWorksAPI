using BO = CivilWorks.BusinessObjects;
using CivilWorks.WebAPI.RequestHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.DataRepository.Model;

namespace CivilWorks.WebAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IRequestHandler<BO.User> RequestHandler;
        private IRequestHandler<BO.UserPasswordActivation> InvitationRequestHandler;
        

        public UserController()
        {
            RequestHandler = new APIRequestHandler<BO.User>();
            InvitationRequestHandler = new APIRequestHandler<BO.UserPasswordActivation>();
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
        [Route("Save")]
        public HttpResponseMessage Post([FromBody]BO.User data)
        {            
            return RequestHandler.CreateGbObject(Request, data);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("Signin")]
        public HttpResponseMessage Signin([FromBody]BO.User user)
        {
           return RequestHandler.Login(Request, user);
        }

        [HttpPost]
        [Route("ValidateInvitation")]        
        public HttpResponseMessage ValidateInvitation([FromBody]BO.UserPasswordActivation data)
        {
            if (data != null)
                return InvitationRequestHandler.ValidateInvitation(Request, data);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, new BO.ErrorObject { ErrorMessage = "Invalid data", errorObject = "", ErrorLevel = BO.ErrorLevel.Critical });
        }


        [HttpPost]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword([FromBody]BO.User user)
        {
            return RequestHandler.ResetPassword(Request, user);
        }
    }
}
