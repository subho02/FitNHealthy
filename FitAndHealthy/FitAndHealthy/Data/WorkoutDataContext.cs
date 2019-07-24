using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitAndHealthy.Models;


namespace FitAndHealthy.Data
{
    public class WorkoutDataContext : DbContext
    {
        public WorkoutDataContext()
        {
        }

        public WorkoutDataContext(DbContextOptions<WorkoutDataContext> options)
            : base(options)
        {
        }       
        public DbSet<Workout> Workout { get; set; }
        public DbSet<ExceptionDetails> Exception { get; set; }

    }
}
