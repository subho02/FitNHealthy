using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAndHealthy.Models;
using FitAndHealthy.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FitAndHealthy.Service
{
    public class LogExceptionService : ExceptionFilterAttribute, IExceptionFilter
    {        
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ExceptionDetails logger = new ExceptionDetails()
                {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    LogTime = DateTime.Now
                };
                var optionsBuilder = new DbContextOptionsBuilder<WorkoutDataContext>();
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-772HBIR\\SQLEXPRESS;Initial Catalog=WorkOut;Integrated Security=true");
                using (var context = new WorkoutDataContext(optionsBuilder.Options))
                {
                    context.Exception.Add(logger);
                    context.SaveChanges();
                }                                  

                filterContext.ExceptionHandled = true;
            }
        }
    }
}
