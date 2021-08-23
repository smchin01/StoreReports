using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Models;

namespace StoreReports.Interfaces
{
    public interface ITotalSearchService
    {
        public List<TotalSearchModel> TotalSearch(DateTime startDate, DateTime endDate);
    }
}
