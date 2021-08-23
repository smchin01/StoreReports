using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Models;

namespace StoreReports.Interfaces
{
    public interface IDeleteReportService
    {
        public void DeleteDailyReport(DateTime date);

    }
}
