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
    [RoutePrefix("api/projectReport")]
    public class ProjectReportController : ApiController
    {
        private IRequestHandler<BO.ProjectReport> RequestHandler;
        public ProjectReportController()
        {
            RequestHandler = new APIRequestHandler<BO.ProjectReport>();
        }

        public HttpResponseMessage Get()
        {
            return RequestHandler.Get(Request);
        }

        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage SaveProjectReport([FromBody] BO.ProjectReport report)
        {
            return RequestHandler.CreateGbObject(Request, report);
        }
    }
}
