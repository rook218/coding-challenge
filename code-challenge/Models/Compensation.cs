using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        [Key]
        [JsonIgnore]
        public String CompensationId { get; set; }

        public String EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public int Salary { get; set; }

        // The challenge says that it should return the date, but it makes more sense to me for developers to handle the data the way they
        // want on the front-end. Plus this allows for multiple entries in one day to be tracked for timing to see which is most recent
        public DateTime EffectiveDate { get; set; }
    }
}
