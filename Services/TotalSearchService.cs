using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Models;
using StoreReports.Interfaces;
using Microsoft.Extensions.Logging;

namespace StoreReports.Services
{
    public class TotalSearchService : ITotalSearchService
    {
        private readonly ILogger<TotalSearchService> _logger;

        public TotalSearchService(ILogger<TotalSearchService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<TotalSearchModel> TotalSearch(DateTime startDate, DateTime endDate)
        {
            if(startDate == null || endDate == null)
            {
                throw new ArgumentNullException();
            }

            List<TotalSearchModel> totalList = new List<TotalSearchModel>();


            using (StoreReportsContext context = new StoreReportsContext())
            {
                string dt1 = startDate.ToString("yyyy-MM-dd");
                string dt2 = endDate.ToString("yyyy-MM-dd");

                var data = (context.ReportData.Where
                    (s => s.Date >= startDate && s.Date <= endDate).ToList());

                var t = (context.TotalSales
                       .Where(s => s.Date >= startDate && s.Date <= endDate).ToList());

                var g = (context.Bills.Where(x => x.Date >= startDate && x.Date <= endDate).ToList());


                TotalSearchModel totalSearchModel = new TotalSearchModel
                {
                    ReportDatum = new ReportDatum
                    {
                        Taxable = data.Sum(x => x.Taxable),
                        NonTax = data.Sum(x => x.NonTax),
                        Gas = data.Sum(x => x.Gas),
                        OnlineLotto = data.Sum(x => x.OnlineLotto),
                        InstantLotto = data.Sum(x => x.InstantLotto),
                        PaidOutsLotto = data.Sum(x => x.PaidOutsLotto),
                        PaidOuts = data.Sum(x => x.PaidOuts),
                        CreditCard = data.Sum(x => x.CreditCard),
                        Checks = data.Sum(x => x.Checks),
                        Cash = data.Sum(x => x.Cash),
                        StateTax = data.Sum(x => x.StateTax),
                        Date = startDate
                    },

                    TotalSale = new TotalSale
                    {
                        Date = endDate,
                        TotalAmount = t.Sum(x => x.TotalAmount)
                    },

                    Bill = new Bill 
                    { 
                        Date = endDate,
                        Amount = g.Sum(x  => x.Amount),
                        Bill1 = g.First().Bill1
                
                    }
                };

                    totalList.Add(totalSearchModel);
            }

            return totalList;
        }
    }
}
