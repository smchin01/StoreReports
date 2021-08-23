using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreReports.Interfaces;
using StoreReports.Models;
using Microsoft.Extensions.Logging;


namespace StoreReports.Services
{
    public class BillDeleteService :IDeleteBillService
    {
        private readonly ILogger<BillDeleteService> _logger;

        public BillDeleteService(ILogger<BillDeleteService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DeleteBill(DateTime date, decimal amount, String bill)
        {
            this._logger.LogInformation("Starting DeleteBill).");


            using (StoreReportsContext context = new StoreReportsContext())
            {
                string dt = date.ToString("yyyy-MM-dd");

                var data = (context.Bills.Where
                    (s => s.Date.ToString() == dt && s.Amount == amount && s.Bill1 == bill).FirstOrDefault());

                context.Bills.Remove(data);


                context.SaveChanges();
            }

        }




    }
}
