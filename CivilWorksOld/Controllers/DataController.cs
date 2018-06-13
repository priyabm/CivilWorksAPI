using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;

namespace CivilWorks.Controllers
{
    [RoutePrefix("report")]
    public class DataController : ApiController
    {
        CivilWorksEntities2 _context = null;

        [HttpGet]
        [Route("Getdata")]
        public HttpResponseMessage GetData(int id)
        {

            try
            {
                _context = new CivilWorksEntities2();
                var user = _context.ProjectReports.Where(u => u.ProjectID == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllData")]
        public HttpResponseMessage GetAllIData()
        {
            try
            {
                _context = new CivilWorksEntities2();
                var users = _context.ProjectReports.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}