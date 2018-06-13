using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;


namespace CivilWorks.Controllers
{

    [RoutePrefix("project")]
    public class ProjectController : ApiController
    {

        CivilWorksEntities2 _context = null;


        [HttpGet]
        [Route("GetProject")]
        public HttpResponseMessage GetProject(int id)
        {
            try
            {
                _context = new CivilWorksEntities2();
                
                var UserId = _context.Projects.Where(u => u.ID == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, UserId);
              
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

                

                var userid = _context.Projects.ToList();

                return Request.CreateResponse(HttpStatusCode.OK, userid);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

    }
}
