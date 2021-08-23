using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Interfaces;
using StoreReports.Models;

namespace StoreReports.Services
{
    public class BillEntryService : IBillEntryService
    {
        public void SubmitBill(BillEntryModel billEntryModel)
        {
            if(billEntryModel == null)
            {
                throw new AccessViolationException(nameof(billEntryModel));
            }

            //SQL
            using (StoreReportsContext context = new StoreReportsContext())
            {
                //add bills
                var bill = new Bill
                {
                    Date = billEntryModel.Bill.Date,
                    Amount = billEntryModel.Bill.Amount,
                    Bill1 = billEntryModel.Bill.Bill1

                };
                context.Add(bill);
                context.SaveChanges();

            };


        }


    }
}
