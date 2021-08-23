using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoreReports.Interfaces;
using StoreReports.Models;

namespace StoreReports.Services
{
    public class DeleteReportService : IDeleteReportService
    {
        private readonly ILogger<DeleteReportService> _logger;

        public DeleteReportService(ILogger<DeleteReportService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DeleteDailyReport(DateTime date)
        {
            this._logger.LogInformation("Starting DeleteDailyReport).");


            using (StoreReportsContext context = new StoreReportsContext())
            {
                string dt = date.ToString("yyyy-MM-dd");

                var data = (context.ReportData.Where
                    (s => s.Date.ToString() == dt).FirstOrDefault());

                var total = context.TotalSales.Where(s => s.Date.ToString() == dt).FirstOrDefault();

                context.TotalSales.Remove(total);

                context.ReportData.Remove(data);
                

                context.SaveChanges();
            }

        }



    }
}
