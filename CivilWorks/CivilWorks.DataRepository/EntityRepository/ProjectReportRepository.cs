using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;
using BO = CivilWorks.BusinessObjects;
using CivilWorks.Common;
using CivilWorks.DataRepository.EntityRepository.Helper;
using CivilWorks.BusinessObjects.MesssagingService;


namespace CivilWorks.DataRepository.EntityRepository
{
   internal class ProjectReportRepository:BaseEntityRepo
    {
        public ProjectReportRepository(EO.CivilWorksEntities context) : base(context)
        {

        }

        public override Object Save<T>(T entity)
        {
            BO.ProjectReport reportBO = (BO.ProjectReport)(object)entity;
            BO.ProjectLabourEquipment eqBO = (BO.ProjectLabourEquipment)(object)entity;
            BO.ProjectQuantity quantityBO = (BO.ProjectQuantity)(object)entity;

            if (reportBO == null) return new BO.ErrorObject { ErrorMessage = "report object can't be null", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
            EO.ProjectReport reportDB = new EO.ProjectReport();

            reportDB.ID = reportBO.ID;
            reportDB.ProjectID = reportBO.ProjectID;
            reportDB.ReportNumber = reportBO.ReportNumber;
            reportDB.ReportDate = reportBO.ReportDate;
            reportDB.StartTime = reportBO.StartTime;
            reportDB.EndTime = reportBO.EndTime;
            reportDB.MorningWeather = reportBO.MorningWeather;
            reportDB.EveningWeather = reportBO.EveningWeather;
            reportDB.MorningTemprature = reportBO.MorningTemprature;
            reportDB.EveningTemperature = reportBO.EveningTemperature;
            reportDB.InspectedBy = reportBO.InspectedBy;
            reportDB.InspectionDate = reportBO.InspectionDate;
            reportDB.CheckedBy = reportBO.CheckedBy;
            reportDB.CheckedDate = reportBO.CheckedDate;
            reportDB.CreatedByUserID = reportBO.CreatedByUserID;
            reportDB.DateCreated = reportBO.DateCreated;
            //reportDB.ProjectLabourEquipments = reportBO.ProjectLabourEquipments;
            //reportDB.ProjectQuantities = reportBO.ProjectQuantities;

            if(reportDB.ID>0)
            {

            }
            else
            {
                _context.ProjectReports.Add(reportDB);
            }
                        
            return (object)reportDB;
        }


                      
    }

}
