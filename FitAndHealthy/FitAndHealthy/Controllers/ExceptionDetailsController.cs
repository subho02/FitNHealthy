using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitAndHealthy.Models;
using FitAndHealthy.Data;


namespace FitAndHealthy.Controllers
{
    [Produces("application/json")]
    [Route("api/ExceptionDetails")]
    public class ExceptionDetailsController : Controller
    {
        private readonly WorkoutDataContext _context;

        public ExceptionDetailsController(WorkoutDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ExceptionDetails> GetException()
        {
            return _context.Exception;
        }
    }
}