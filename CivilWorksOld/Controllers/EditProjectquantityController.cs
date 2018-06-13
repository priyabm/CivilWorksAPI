using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;

namespace CivilWorks.Controllers
{
    [RoutePrefix("edit")]
    public class EditProjectquantityController : ApiController
    {
        CivilWorksEntities2 context = new CivilWorksEntities2();
        ProjectQuantity pquant = new ProjectQuantity();

        [HttpGet]
        [Route("GetQuantity")]
        public HttpResponseMessage GetQuantity(int id)
        {
            try
            {
                context = new CivilWorksEntities2();
                var quan = context.ProjectQuantities.Where(u => u.ID == id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, quan);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            //    CivilWorksEntities2 context = new CivilWorksEntities2();
            //    List<ProjectQuantity> quan = new List<ProjectQuantity>();
            //    ProjectQuantity q = new ProjectQuantity();
            //    //List<BOModel.ProjectQuantity> quan = new List<BOModel.ProjectQuantity>();
            //    try
            //    {
            //        // quan = context.ProjectQuantities.Select(y => y.ID == quantity.ID).ToList();
            //        var a = context.ProjectQuantities.Where(y => y.ID == quantity.ID).ToList();

            //        //foreach(var item1 in a)
            //        //{
            //        // q.ID = item1.

            //        //}
            //        return Request.CreateResponse(HttpStatusCode.OK, a);
            //    }

        }
    }
}





//[HttpPut]
//[Route("GetQuantity")]
//public HttpResponseMessage Put([FromBody] ProjectQuantity quantity)
//{
//    CivilWorksEntities2 context = new CivilWorksEntities2();
//    ProjectQuantity pquant = new ProjectQuantity();
//    try
//    {
//        pquant.ID = quantity.ID;
//        pquant.ItemID = quantity.ItemID;
//        pquant.Location = quantity.Location;
//        pquant.Status = quantity.Status;
//        pquant.WorkDescription = quantity.WorkDescription;
//        pquant.ImageFilePath = quantity.ImageFilePath;
//        pquant.ImageNote = quantity.ImageNote;
//        pquant.Quantity = quantity.Quantity;
//        pquant.ReportID = quantity.ReportID;


//        //context.ProjectQuantities.Add(entity);
//        context.SaveChanges();
//        return Request.CreateResponse(HttpStatusCode.OK, quantity);
//    }
//    catch (Exception ex)
//    {
//        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//    }
//}
//CivilWorksEntities2 context = new CivilWorksEntities2();
//[HttpPost]
//[Route("SaveProjectReport")]
//public HttpResponseMessage editProjectQuantity([FromBody] Models.ProjectQuantity report)
//{

//    try
//    {
//        var a = context.ProjectQuantities.Where(y => y.ReportID == quantity.ReportID).ToList();
//    }

//catch(Exception ex)
//    {
//        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
//    }       


