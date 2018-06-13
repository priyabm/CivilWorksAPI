using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;
using Bo=CivilWorks.BOModel;

namespace CivilWorks.Controllers
{
    [RoutePrefix("team")]
    public class ProjectTeamController : ApiController
    {

        CivilWorksEntities2 _context = null;

        [HttpGet]
        [Route("GetTeam")]
        public HttpResponseMessage GetTeam(int id)
        {

            try
            {
                _context = new CivilWorksEntities2();
                var user = _context.ProjectTeams.Where(u => u.ID == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllTeams")]
        public HttpResponseMessage GetAllTeams()
        {
            try
            {
                _context = new CivilWorksEntities2();
                var users = _context.ProjectTeams.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
