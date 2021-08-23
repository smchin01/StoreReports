using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StoreReports.Models
{
    public class TotalSearchModel
    {
        public ReportDatum ReportDatum { get; set; }

        public TotalSale TotalSale { get; set; }

        public Bill Bill { get; set; }
    }
}
