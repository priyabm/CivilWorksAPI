using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CivilWorks.BOModel
{
    public class ProjectQuantity
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public int ItemID { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public decimal Quantity { get; set; }
        public string WorkDescription { get; set; }
        public string ImageNote { get; set; }
        public string ImageFilePath { get; set; }

    }
}