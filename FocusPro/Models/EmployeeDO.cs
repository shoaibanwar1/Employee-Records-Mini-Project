using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FocusPro.Models
{
    public class EmployeeDO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string Hobbies { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
}