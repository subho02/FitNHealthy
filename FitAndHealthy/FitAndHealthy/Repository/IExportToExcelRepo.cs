using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAndHealthy.Data;
using FitAndHealthy.Models;
using System.Text;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FitAndHealthy.Repository
{
    public interface IExportToExcelRepo
    {
        void exportDataToExcel(FileInfo file);
    }
}
