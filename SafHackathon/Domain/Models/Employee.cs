using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Domain.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public string employee_salary { get; set; }
        public string employee_age { get; set; }

        public string profile_image { get; set; }
    }
}
