using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace FitAndHealthy.Models
{
    public class ExceptionDetails
    {
        [Key]
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime LogTime { get; set; }
    }
}
