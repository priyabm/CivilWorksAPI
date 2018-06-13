using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;
using System.Linq;
using System.Data.Entity.Validation;
using BO = CivilWorks.BOModel;

namespace CivilWorks.Controllers
{
    [RoutePrefix("ProjectReport")]
    public class ProjectReportController : ApiController
    {
        [HttpPost]
        [Route("SaveEmployee")]
        public HttpResponseMessage SaveEmployee([FromBody] Models.Employee employee)
        {
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [HttpPost]
        [Route("SaveProjectReport")]
        public HttpResponseMessage SaveProjectReport([FromBody] Models.ProjectReport report)
        {

            try
            {

                CivilWorksEntities2 context = new CivilWorksEntities2();
              
                context.ProjectReports.Add(report);
                context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK,"Successfully Saved");

                //BO.ProjectReport rep = new BO.ProjectReport();
                //List<BO.ProjectLabourEquipment> lstProjectLabourEquipment = new List<BOModel.ProjectLabourEquipment>();
                //BO.ProjectLabourEquipment BOProjectLabourEquipment = new BOModel.ProjectLabourEquipment();



                //foreach(Models.ProjectLabourEquipment item in report.ProjectLabourEquipments)
                //{
                //    BOProjectLabourEquipment.ID = item.ID;
                //    BOProjectLabourEquipment.Labour_Name = item.Labour_Name;
                //    BOProjectLabourEquipment.ContractorName = item.ContractorName;
                //    BOProjectLabourEquipment.EquipmentHours = item.EquipmentHours;
                //    BOProjectLabourEquipment.WorkDescription = item.WorkDescription;
                //    BOProjectLabourEquipment.LabourHours = item.LabourHours;
                //    BOProjectLabourEquipment.EquipmentName = item.EquipmentName;

                //    lstProjectLabourEquipment.Add(BOProjectLabourEquipment);
                //}

                //rep.ProjectLabourIquipments = lstProjectLabourEquipment;

                //List<BO.ProjectQuantity> lstProjectquantity = new List<BOModel.ProjectQuantity>();
                //BO.ProjectQuantity BOProjectQuantity = new BOModel.ProjectQuantity();

                //foreach(Models.ProjectQuantity item in report.ProjectQuantities)
                //{
                //    BOProjectQuantity.ID = item.ID;
                //    BOProjectQuantity.ItemID = item.ItemID;
                //    BOProjectQuantity.Location = item.Location;
                //    BOProjectQuantity.Status = item.Status;
                //    BOProjectQuantity.Quantity = item.Quantity;
                //    BOProjectQuantity.WorkDescription = item.WorkDescription;
                //    BOProjectQuantity.ImageNote = item.ImageNote;
                //    BOProjectQuantity.ImageFilePath = item.ImageFilePath;

                //    lstProjectquantity.Add(BOProjectQuantity);
                //}

                //rep.ProjectQuantity = lstProjectquantity;


                //Models.ProjectReport rep = new Models.ProjectReport();
                //List<ProjectLabourEquipment> equ = new List<ProjectLabourEquipment>();
                //List<ProjectQuantity> equ1 = new List<ProjectQuantity>();
                //rep.ProjectLabourEquipments = report.ProjectLabourEquipments;
                //rep.ProjectQuantities = report.ProjectQuantities;
                //rep.ProjectID = report.ProjectID;
                //rep.CheckedBy = report.CheckedBy;
                //rep.ReportNumber = report.ReportNumber;
                //rep.ReportDate = report.ReportDate;
                //rep.MorningWeather = report.MorningWeather;
                //rep.MorningTemprature = report.MorningTemprature;
                //rep.InspectionDate = report.InspectionDate;
                //rep.InspectedBy = report.InspectedBy;
                //rep.EveningWeather = report.EveningWeather;
                //rep.EveningTemperature = report.EveningTemperature;
                //rep.CreatedByUserID = report.CreatedByUserID;
                //rep.StartTime = report.StartTime;
                //rep.EndTime = report.EndTime;
                //rep.DateCreated = report.DateCreated;
                //context.ProjectReports.Add(rep);

                //return Request.CreateResponse<T>(HttpStatusCode.OK, rep);

                // return Request.CreateResponse<BO.ProjectReport>(HttpStatusCode.OK, rep);

            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }


        [HttpPost]
        [Route("UpdateProjectReport")]
        public HttpResponseMessage UpdateProjectReport([FromBody] Models.ProjectReport report1)
        {

            try
            {
                CivilWorksEntities2 context = new CivilWorksEntities2();

                ProjectReport ProjRpt = new ProjectReport();
                ProjRpt = context.ProjectReports.Where(p => p.ID == report1.ID).FirstOrDefault();
                if (ProjRpt != null)
                {
                    ProjRpt.ProjectID = report1.ProjectID;
                    ProjRpt.ReportNumber = report1.ReportNumber;
                    ProjRpt.ReportDate = report1.ReportDate;
                    ProjRpt.StartTime = report1.StartTime;
                    ProjRpt.EndTime = report1.EndTime;
                    ProjRpt.MorningWeather = report1.MorningWeather;
                    ProjRpt.EveningWeather = report1.EveningWeather;
                    ProjRpt.MorningTemprature = report1.MorningTemprature;
                    ProjRpt.EveningTemperature = report1.EveningTemperature;
                    ProjRpt.InspectedBy = report1.InspectedBy;
                    ProjRpt.InspectionDate = report1.InspectionDate;
                    ProjRpt.CheckedBy = report1.CheckedBy;
                    ProjRpt.CheckedDate = report1.CheckedDate;

                    var ProjQuantityIDs = report1.ProjectQuantities.Select(p => p.ID).ToList();

                    var ProjQuantity_Remove = context.ProjectQuantities
                                                     .Where(p => p.ReportID == report1.ID && ProjQuantityIDs.Contains(p.ID) == false)
                                                     .ToList();

                    ProjQuantity_Remove.ForEach(p => context.ProjectQuantities.Remove(p));

                    var ProjQuantity_Update = context.ProjectQuantities
                                                     .Where(p => p.ReportID == report1.ID && ProjQuantityIDs.Contains(p.ID) == true)
                                                     .ToList();

                    foreach (var eachProjQuantity in ProjQuantity_Update)
                    {
                        var ProjQuantity = report1.ProjectQuantities.Where(p => p.ID == eachProjQuantity.ID).SingleOrDefault();
                        eachProjQuantity.ReportID = ProjQuantity.ReportID;
                        eachProjQuantity.ItemID = ProjQuantity.ItemID;
                        eachProjQuantity.Location = ProjQuantity.Location;
                        eachProjQuantity.Status = ProjQuantity.Status;
                        eachProjQuantity.Quantity = ProjQuantity.Quantity;
                        eachProjQuantity.WorkDescription = ProjQuantity.WorkDescription;
                        eachProjQuantity.ImageNote = ProjQuantity.ImageNote;
                        eachProjQuantity.ImageFilePath = ProjQuantity.ImageFilePath;
                    }


                    var ProjQuantityIDs_Add = context.ProjectQuantities.Where(p => p.ReportID == report1.ID).Select(p => p.ID).ToList();

                    var ProjQuantity_Add = report1.ProjectQuantities
                                                     .Where(p => p.ReportID == report1.ID && ProjQuantityIDs_Add.Contains(p.ID) == false)
                                                     .ToList();

                    ProjQuantity_Add.ForEach(p => context.ProjectQuantities.Add(p));

                 






                    var ProjLabourIDs = report1.ProjectLabourEquipments.Select(p => p.ID).ToList();

                    var ProjLabour_Remove = context.ProjectLabourEquipments
                                                     .Where(p => p.ReportID == report1.ID && ProjLabourIDs.Contains(p.ID) == false)
                                                     .ToList();

                    ProjLabour_Remove.ForEach(p => context.ProjectLabourEquipments.Remove(p));

                    var ProjLabour_Update = context.ProjectLabourEquipments
                                                     .Where(p => p.ReportID == report1.ID && ProjLabourIDs.Contains(p.ID) == true)
                                                     .ToList();

                    foreach (var eachProjLabour in ProjLabour_Update)
                    {
                        var ProjLabour = report1.ProjectLabourEquipments.Where(p => p.ID == eachProjLabour.ID).SingleOrDefault();
                        eachProjLabour.ReportID = ProjLabour.ReportID;
                        eachProjLabour.ContractorName = ProjLabour.ContractorName;
                        eachProjLabour.Labour_Name = ProjLabour.Labour_Name;
                        eachProjLabour.EquipmentName = ProjLabour.EquipmentName;
                        eachProjLabour.WorkDescription = ProjLabour.WorkDescription;
                        eachProjLabour.WorkDescription = ProjLabour.WorkDescription;
                        eachProjLabour.LabourHours = ProjLabour.LabourHours;
                        eachProjLabour.EquipmentHours = ProjLabour.EquipmentHours;
                    }


                    var ProjLabourIDs_Add = context.ProjectLabourEquipments.Where(p => p.ReportID == report1.ID).Select(p => p.ID).ToList();

                    var ProjLabour_Add = report1.ProjectLabourEquipments
                                                     .Where(p => p.ReportID == report1.ID && ProjLabourIDs_Add.Contains(p.ID) == false)
                                                     .ToList();

                    ProjLabour_Add.ForEach(p => context.ProjectLabourEquipments.Add(p));
                    context.SaveChanges();

                    var ProjReport = context.ProjectReports.Include("ProjectQuantities")
                                                    .Include("ProjectLabourEquipments")
                                                    .Where(p => p.ID == report1.ID)
                                                    .ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, ProjReport);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, report1);


            /*
            List<ProjectQuantity> quan = new List<ProjectQuantity>();
            List<ProjectLabourEquipment> lab = new List<ProjectLabourEquipment>();

            foreach (var item in report1.ProjectQuantities)
            {

                if(item.ID>0)
                {
                    var a = context.ProjectQuantities.Where(x => x.ID == item.ReportID).FirstOrDefault();
                    if(a != null)
                    {
                        context.ProjectQuantities.Remove(a);

                    }

                    context.ProjectQuantities.Add(item);
                    context.SaveChanges();
                }
                else
                {
                    context.ProjectQuantities.Add(item);
                    context.SaveChanges();
                }


            }

            foreach (var item in report1.ProjectLabourEquipments)
            {
                if (item.ID > 0)
                {
                    var a = context.ProjectLabourEquipments.Where(x => x.ID == item.ID).FirstOrDefault();
                    if (a != null)
                    {
                        context.ProjectLabourEquipments.Remove(a);
                    }
                    context.ProjectLabourEquipments.Add(item);
                    context.SaveChanges();
                }
                else
                {
                    context.ProjectLabourEquipments.Add(item);
                    context.SaveChanges();
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, report1);
        }

        catch (Exception ex)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
        }
        */

        }
    }
}
