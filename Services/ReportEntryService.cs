using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoreReports.Interfaces;
using StoreReports.Models;

namespace StoreReports.Services
{
    public class ReportEntryService : IReportEntryService
    {
        public void SubmitReport(ReportEntryModel reportEntryModel)
        {
            if(reportEntryModel == null)
            {
                throw new AccessViolationException(nameof(reportEntryModel));
            }

            //SQL
            using (StoreReportsContext context = new StoreReportsContext())
            {
                //add daily report data 

                var dailyReport = new ReportDatum
                {
                    Taxable = reportEntryModel.ReportData.Taxable,
                    NonTax = reportEntryModel.ReportData.NonTax,
                    Gas = reportEntryModel.ReportData.Gas,
                    OnlineLotto = reportEntryModel.ReportData.OnlineLotto,
                    InstantLotto = reportEntryModel.ReportData.InstantLotto,
                    PaidOutsLotto = reportEntryModel.ReportData.PaidOutsLotto,
                    PaidOuts = reportEntryModel.ReportData.PaidOuts,
                    CreditCard = reportEntryModel.ReportData.CreditCard,
                    Checks = reportEntryModel.ReportData.Checks,
                    Cash = reportEntryModel.ReportData.Cash,
                    StateTax = reportEntryModel.ReportData.StateTax,
                    Date = reportEntryModel.ReportData.Date
                };

                var totalSales = new TotalSale
                {
                    Date = reportEntryModel.ReportData.Date.GetValueOrDefault(),
                    TotalAmount = (reportEntryModel.ReportData.Taxable + reportEntryModel.ReportData.NonTax + reportEntryModel.ReportData.Gas + reportEntryModel.ReportData.OnlineLotto +
                        reportEntryModel.ReportData.InstantLotto)
                };

                context.Add(dailyReport);
                context.Add(totalSales);
                context.SaveChanges();
            };

        }

    }
}
