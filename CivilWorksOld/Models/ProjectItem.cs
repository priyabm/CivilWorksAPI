//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CivilWorks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectItem
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Project Project { get; set; }
    }
}
