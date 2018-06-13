using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;

namespace CivilWorks.Controllers
{
    [RoutePrefix("get")]
    public class ReportPopupController : ApiController
    {
        CivilWorksEntities2 _context = null;

        [HttpGet]
        [Route("Getproject")]
        public HttpResponseMessage GetUser(int id)
        {

            try
            {
                _context = new CivilWorksEntities2();
                var user = _context.ProjectReports.Where(u => u.ID == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllProject")]
        public HttpResponseMessage GetAllProject()
        {
            try
            {
                _context = new CivilWorksEntities2();
                var users = (from x in _context.ProjectReports
                             join y in _context.Projects
                             on x.ProjectID equals y.ID
                             join z in _context.ProjectTeams
                             on x.ProjectID equals z.ProjectID
                             select new { x.ID, x.ProjectID, y.ProjectName, x.ReportNumber, x.ReportDate,z.InspectorName }
                           ).ToList();
                //var a = _context.ProjectReports.Where(y => y.ID ==).ToList();
                return Request.CreateResponse(HttpStatusCode.OK,users);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
