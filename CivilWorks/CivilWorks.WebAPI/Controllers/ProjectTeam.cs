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
    [RoutePrefix("api/projectTeam")]
    public class ProjectTeamController : ApiController
    {
      
            private IRequestHandler<BO.ProjectTeam> RequestHandler;

            public ProjectTeamController()
            {
                RequestHandler = new APIRequestHandler<BO.ProjectTeam>();
            }

            public HttpResponseMessage Get()
            {
               return RequestHandler.Get(Request);

            }

        }
    
}