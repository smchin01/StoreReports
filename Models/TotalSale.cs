using System;
using System.Collections.Generic;

#nullable disable

namespace StoreReports.Models
{
    public partial class TotalSale
    {
        public DateTime Date { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual ReportDatum DateNavigation { get; set; }
    }
}
