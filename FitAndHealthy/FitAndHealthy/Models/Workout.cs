using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitAndHealthy.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        public int DistanceInMeters { get; set; }

        [Required]
        public long TimeInSeconds { get; set; }
    }
}
