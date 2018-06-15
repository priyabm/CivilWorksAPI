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
    [RoutePrefix("api/project")]
    public class ProjectController: ApiController
    {
        private IRequestHandler<BO.Project> RequestHandler;

        public ProjectController()
        {
            RequestHandler = new APIRequestHandler<BO.Project>();           
        }

        public HttpResponseMessage Get()
        {
            //return new string[] { "value1", "value2" };
            return RequestHandler.Get(Request);

        }

    }
}