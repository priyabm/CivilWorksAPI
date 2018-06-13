using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CivilWorks.Models;
using BO = CivilWorks.BOModel;

namespace CivilWorks.BOModel
{
    public class ProjectReport
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string ReportNumber { get; set; }
        public System.DateTime ReportDate { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public string MorningWeather { get; set; }
        public string EveningWeather { get; set; }
        public decimal MorningTemprature { get; set; }
        public decimal EveningTemperature { get; set; }
        public int InspectedBy { get; set; }
        public System.DateTime InspectionDate { get; set; }
        public int CheckedBy { get; set; }
        public System.DateTime CheckedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public System.DateTime DateCreated { get; set; }

       public ICollection<BO.ProjectLabourEquipment> ProjectLabourIquipments { get; set; }
        public ICollection<BO.ProjectQuantity> ProjectQuantity { get; set; }
    }
}