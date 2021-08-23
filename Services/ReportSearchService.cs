using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Models;
using StoreReports.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace StoreReports.Services
{
    public class ReportSearchService : IReportSearchService
    {
        private readonly ILogger<ReportSearchService> _logger;

        public ReportSearchService(ILogger<ReportSearchService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<ReportSearchModel> SubmitSearch(DateTime date)
        {
            if (date == null)
            {
                throw new ArgumentNullException();
            }

            List<ReportSearchModel> reportList = new List<ReportSearchModel>();

            using (StoreReportsContext context = new StoreReportsContext())
            {
                string dt = date.ToString("yyyy-MM-dd");

                var data = (context.ReportData
                          .Where(s => s.Date.ToString() == dt).FirstOrDefault());

                var t = (context.TotalSales
                          .Where(s => s.Date.ToString() == dt).FirstOrDefault());

                ReportSearchModel reportSearchModel = new ReportSearchModel
                {
                    ReportDatum = new ReportDatum
                    {
                        Taxable = data.Taxable,
                        NonTax = data.NonTax,
                        Gas = data.Gas,
                        OnlineLotto = data.OnlineLotto,
                        InstantLotto = data.InstantLotto,
                        PaidOutsLotto = data.PaidOutsLotto,
                        PaidOuts = data.PaidOuts,
                        CreditCard = data.CreditCard,
                        Checks = data.Checks,
                        Cash = data.Cash,
                        StateTax = data.StateTax,
                        Date = data.Date

                    },

                    TotalSale = new TotalSale
                    {
                        Date = data.Date.GetValueOrDefault(),
                        TotalAmount = t.TotalAmount
                    }

                };

                reportList.Add(reportSearchModel);

            }

            return reportList;

        }
             

    }
}
