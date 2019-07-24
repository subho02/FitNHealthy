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
using FitAndHealthy.Repository;

namespace FitAndHealthy.Service
{    
    public class ExportTransactionProcessorService : IExportToExcelRepo
    {
        private readonly WorkoutDataContext _workoutDataContext;
        public ExportTransactionProcessorService(WorkoutDataContext workoutDataContext)
        {
            _workoutDataContext = workoutDataContext;
        }

        public void exportDataToExcel(FileInfo file)
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {

                IList<ExceptionDetails> exceptionList = _workoutDataContext.Exception.ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exception");
                int totalRows = exceptionList.Count();

                worksheet.Cells[1, 1].Value = "Exception ID";
                worksheet.Cells[1, 2].Value = "Controller Name";
                worksheet.Cells[1, 3].Value = "Exception Message";
                worksheet.Cells[1, 4].Value = "Exception Stack";
                worksheet.Cells[1, 5].Value = "Exception Log Time";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = exceptionList[i].Id;
                    worksheet.Cells[row, 2].Value = exceptionList[i].ControllerName;
                    worksheet.Cells[row, 3].Value = exceptionList[i].ExceptionMessage;
                    worksheet.Cells[row, 4].Value = exceptionList[i].ExceptionStackTrace;
                    worksheet.Cells[row, 5].Value = exceptionList[i].LogTime;
                    i++;
                }

                package.Save();

            }
        }
    }
}
