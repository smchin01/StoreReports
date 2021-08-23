using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Models;

namespace StoreReports.Interfaces
{
    public interface IReportSearchService
    {
        public List<ReportSearchModel> SubmitSearch(DateTime date);
    }
}




