using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CivilWorks.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public int EmpName { get; set; }
        public List<Address> AddressList { get; set; }
        public Employee()
        {
            AddressList = new List<Address>();
        }
    }
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}