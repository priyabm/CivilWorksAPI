using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilWorks.BusinessObjects
{
   public class ProjectTeam
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string InspectorName { get; set; }

        public virtual Project Project { get; set; }
    }
}
