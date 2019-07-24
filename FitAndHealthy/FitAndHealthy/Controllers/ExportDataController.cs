using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitAndHealthy.Data;
using FitAndHealthy.Models;
using System.Text;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using FitAndHealthy.Repository;
using FitAndHealthy.Service;

namespace FitAndHealthy.Controllers
{
    [Produces("application/json")]
    [Route("api/ExportData")]
    public class ExportDataController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly WorkoutDataContext _workoutDataContext;
        private readonly IExportToExcelRepo _repo;

        public ExportDataController(IExportToExcelRepo repo,IHostingEnvironment hostingEnvironment, WorkoutDataContext workoutDataContext)
        {
            _repo = repo;
            _hostingEnvironment = hostingEnvironment;
            _workoutDataContext = workoutDataContext;            
        }
        [LogExceptionService]
        [HttpGet]
        [Route("ExportException")]
        public string Get()
        {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ExportExceptions.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            try
            {
                _repo.exportDataToExcel(file);
            }
            catch(Exception e)
            {
                throw e;
            }

            return " Exception list has been exported successfully";
        }
    }    
}