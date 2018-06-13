using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CivilWorks.BOModel
{
    public class ProjectLabourEquipment
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public string ContractorName { get; set; }
        public string Labour_Name { get; set; }
        public string EquipmentName { get; set; }
        public string WorkDescription { get; set; }
        public Nullable<decimal> LabourHours { get; set; }
        public Nullable<decimal> EquipmentHours { get; set; }
    }
}