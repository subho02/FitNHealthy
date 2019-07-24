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
using System.Data;
using System.Reflection;

namespace FitAndHealthy.Controllers
{
    [LogExceptionService]
    [Produces("application/json")]
    [Route("api/FileDownload")]
    public class FileDownloadController : Controller
    {        
        [HttpGet("{filename}")]
        public async Task<IActionResult> Get(string filename)
        {
            try{
                string fileDetails = Path.Combine(Path.Combine(AssemblyDirectory, "wwroot"), filename);
                var memory = new MemoryStream();
                using (var stream = new FileStream(fileDetails, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return File(memory, GetMimeType(fileDetails), filename);
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private string GetMimeType(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }

    }
}