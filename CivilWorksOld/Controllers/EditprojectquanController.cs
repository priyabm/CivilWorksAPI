using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;
using System.Data.Entity;

namespace CivilWorks.Controllers
{
    [RoutePrefix("edit")]
    public class EditprojectquanController : ApiController
    {
        CivilWorksEntities2 context = new CivilWorksEntities2();
        [HttpGet]
        [Route("EditReport/{id}")]
        public HttpResponseMessage EditReport(int id)
        {
            try
            {
                var a = context.ProjectReports.Where(u => u.ID == id)
                    .Include("ProjectLabourEquipments")
                    .Include("ProjectQuantities")
                    .FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, a);


                //var query = from ot in context.ProjectReports
                //            join v in context.ProjectLabourEquipments on ot.ID equals v.ReportID
                //            join c in context.ProjectQuantities on ot.ID equals c.ReportID
                //            where ot.ID == id
                //            select new { ot,v,c};
                //return Request.CreateResponse(HttpStatusCode.OK, query);

                //var query = context.ProjectReports
                //   .Include("ProjectLabourEquipments")
                //   .Include("ProjectQuantities")
                //   .FirstOrDefault();

                //return query.Select(x => new ProjectReport
                //{
                //    ID = x.ID,
                //    ProjectLabourEquipments = x.ProjectLabourEquipment.Select(y => new ProjectLabourEquipment
                //    {
                //        ID = y.ID,
                //        ReportID = y.ReportID
                //    }),
                //    ProjectQuantities = new ProjectQuantity(x.ProjectQuantities)
                //});
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        
    }
}
