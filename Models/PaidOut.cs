using System;
using System.Collections.Generic;

#nullable disable

namespace StoreReports.Models
{
    public partial class PaidOut
    {
        public DateTime Date { get; set; }
        public decimal? Amount { get; set; }
        public string Reason { get; set; }

        public virtual ReportDatum DateNavigation { get; set; }
    }
}
