using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using challenge.Repositories;
using challenge.Services;

namespace challenge.Models
{
    public class ReportingStructure
    {
        [Key]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int NumberOfReports { get; set; }

    }
}
