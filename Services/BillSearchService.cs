using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Models;
using StoreReports.Interfaces;
using Microsoft.Extensions.Logging;

namespace StoreReports.Services
{
    public class BillSearchService : IBillSearchService
    {
        private readonly ILogger<BillSearchService> _logger;

        public BillSearchService(ILogger<BillSearchService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<BillSearchModel> SubmitSearchBill(string month, int year)
        {
            if (month == null)
            {
                throw new ArgumentNullException();
            }


            int m = 1;

            switch (month)
            {
                case "January":
                    m = 1;
                    break;
                case "February":
                    m = 2;
                    break;
                case "March":
                    m = 3;
                    break;
                case "April":
                    m = 4;
                    break;
                case "May":
                    m = 5;
                    break;
                case "June":
                    m = 6;
                    break;
                case "July":
                    m = 7;
                    break;
                case "August":
                    m = 8;
                    break;
                case "September":
                    m = 9;
                    break;
                case "October":
                    m = 10;
                    break;
                case "November":
                    m = 11;
                    break;
                case "December":
                    m = 12;
                    break;
            }

            DateTime d2 = new DateTime(2000,01,10);
            DateTime d1 = new DateTime(year, m, 01);
            if(m == 2)
            {
               d2 = new DateTime(year, m, 28);
            }
            else if (m % 2 == 0)
            {
                 d2 = new DateTime(year, m, 30);
            }
            else
            {
                 d2 = new DateTime(year, m, 31);
            }

            List<BillSearchModel> billSearchModel = new List<BillSearchModel>();

            using (StoreReportsContext context = new StoreReportsContext())
            {


                var t = (context.Bills
                       .Where(s => s.Date >= d1 && s.Date <= d2).ToList());

                foreach (Bill b in t)
                {

                    Bill bill = new Bill { Bill1 = b.Bill1, Amount = b.Amount, Date = b.Date };

                    BillSearchModel billModel = new BillSearchModel
                    {
                        Bill = new Bill
                        {
                            Amount = bill.Amount,
                            Date = bill.Date,
                            Bill1 = bill.Bill1
                        }
                    };

                    billSearchModel.Add(billModel);
                 
                }
            }

            return billSearchModel;

        }
    }
}
