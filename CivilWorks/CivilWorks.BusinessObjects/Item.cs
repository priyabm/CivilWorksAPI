using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilWorks.BusinessObjects
{
    public class Item
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public int UnitTypeID { get; set; }
        public decimal Price { get; set; }

    }
}
