using System;
using System.Collections.Generic;

#nullable disable

namespace StoreReports.Models
{
    public partial class ReportDatum
    {
        public decimal? Taxable { get; set; }
        public decimal? NonTax { get; set; }
        public decimal? Gas { get; set; }
        public decimal? OnlineLotto { get; set; }
        public decimal? InstantLotto { get; set; }
        public decimal? PaidOutsLotto { get; set; }
        public decimal? PaidOuts { get; set; }
        public decimal? CreditCard { get; set; }
        public decimal? Checks { get; set; }
        public decimal? Cash { get; set; }
        public decimal? StateTax { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Bill> Bill { get; set; }
        public virtual PaidOut PaidOut { get; set; }
        public virtual TotalSale TotalSale { get; set; }
    }
}
