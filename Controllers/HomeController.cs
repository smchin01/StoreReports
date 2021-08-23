using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreReports.Models;
using StoreReports.Interfaces;

namespace StoreReports.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportEntryService _reportEntryService;
        private readonly IReportSearchService _reportSearchService;
        private readonly ITotalSearchService _totalSearchService;
        private readonly IBillEntryService _billEntryService;
        private readonly IDeleteReportService _deleteReportService;
        private readonly IBillSearchService _billSearchService;
        private readonly IDeleteBillService _deleteBillService;

        public HomeController(ILogger<HomeController> logger, IReportEntryService reportEntryService, IReportSearchService reportSearchService, ITotalSearchService totalSearchService, IBillEntryService billEntryService, IDeleteReportService deleteReportService, IBillSearchService billSearchService, IDeleteBillService deleteBillService)
        {
            _logger = logger;
            this._reportEntryService = reportEntryService ?? throw new ArgumentNullException(nameof(reportEntryService));
            this._reportSearchService = reportSearchService ?? throw new ArgumentNullException(nameof(reportSearchService));
            this._totalSearchService = totalSearchService ?? throw new ArgumentNullException(nameof(totalSearchService));
            this._billEntryService = billEntryService ?? throw new ArgumentNullException(nameof(billEntryService));
            this._deleteReportService = deleteReportService ?? throw new ArgumentNullException(nameof(deleteReportService));
            this._billSearchService = billSearchService ?? throw new ArgumentNullException(nameof(billSearchService));
            this._deleteBillService = deleteBillService ?? throw new ArgumentNullException(nameof(deleteBillService));
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Handles initial form request
        /// </summary>
        /// <returns>Student entry form view</returns>
        public IActionResult ReportEntryForm()
        {
            this._logger.LogInformation("Starting ReportEntryForm().");

            if (!ModelState.IsValid)
            {
                this._logger.LogError("ModelState invalid.");
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            ReportEntryModel reportEntryModel = new ReportEntryModel();

            return View("ReportEntryForm", reportEntryModel);
        }

        /// <summary>
        /// Submits user input to database
        /// and redirects to a clean form
        /// </summary>
        [HttpPost]
        public IActionResult ReportEntryForm(ReportEntryModel reportEntryModel)
        {
            this._logger.LogInformation("Starting SubmitEntryForm(ReportEntryModel reportEntryModel).");

            if (reportEntryModel == null)
            {
                throw new ArgumentNullException(nameof(reportEntryModel));
            }

            this._reportEntryService.SubmitReport(reportEntryModel);

            return RedirectToAction("Index");
        }

        public IActionResult BillEntryForm()
        {
            this._logger.LogInformation("Starting BIllEntryForm().");

            if (!ModelState.IsValid)
            {
                this._logger.LogError("ModelState invalid.");
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            BillEntryModel billEntryModel = new BillEntryModel();

            return View("BillEntryForm", billEntryModel);
        }

        /// <summary>
        /// Submits user input to database
        /// and redirects to a clean form
        /// </summary>
        [HttpPost]
        public IActionResult BillEntryForm(BillEntryModel billEntryModel)
        {
            this._logger.LogInformation("Starting bill entry form).");

            if (billEntryModel == null)
            {
                throw new ArgumentNullException(nameof(billEntryModel));
            }

            this._billEntryService.SubmitBill(billEntryModel);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles initial form request
        /// </summary>
        /// <returns>Student entry form view</returns>
        public IActionResult ReportSearchForm()
        {
            this._logger.LogInformation("Starting ReportSearchForm().");

            if (!ModelState.IsValid)
            {
                this._logger.LogError("ModelState invalid.");
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            List<ReportSearchModel> reportSearchModel = new List<ReportSearchModel>();

            return View("ReportSearchForm", reportSearchModel);
        }

        /// <returns>Student info</returns>
        [HttpPost]
        public IActionResult SubmitSearchDate(DateTime date)
        {
            this._logger.LogInformation("Starting SubmitSearchStudent(StudentSearchModel studentSearchModel).");

            if (date == null)
            {
                throw new ArgumentNullException();
            }

            List<ReportSearchModel> st = _reportSearchService.SubmitSearch(date);

            return View("ReportSearchForm", st);
        }

        public IActionResult TotalSearchForm()
        {
            this._logger.LogInformation("Starting TotalSearchForm");

            if(!ModelState.IsValid)
            {
                this._logger.LogError("ModelState Invalid");
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            List<TotalSearchModel> totalSearchModel = new List<TotalSearchModel>();
            return View("TotalSearchForm", totalSearchModel);
        }

        [HttpPost]
        public IActionResult SubmitTotalDates(DateTime date1, DateTime date2)
        {
            this._logger.LogInformation("Starting SubmitTotalDates()");
            if (date1 == null || date2 == null)
            {
                throw new ArgumentNullException();
            }

            List<TotalSearchModel> ts = _totalSearchService.TotalSearch(date1, date2);

            return View("TotalSearchForm", ts);

        }
        public IActionResult BillSearchForm()
        {
            this._logger.LogInformation("Starting BillSearchForm");

            if (!ModelState.IsValid)
            {
                this._logger.LogError("ModelState Invalid");
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            List<BillSearchModel> bill = new List<BillSearchModel>();
            return View("BillSearchForm", bill);
        }

        [HttpPost]
        public IActionResult SubmitSearchBill(String month, int year)
        {
            this._logger.LogInformation("Starting SubmitTotalDates()");
            if (month == null)
            {
                throw new ArgumentNullException();
            }

            List<BillSearchModel> ts = _billSearchService.SubmitSearchBill(month, year);

            return View("BillSearchForm", ts);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DeleteDailyReport(DateTime date)
        {
             _deleteReportService.DeleteDailyReport(date);

            return View("Index");
        }

        public IActionResult DeleteBill(DateTime date, Decimal amount, String bill)
        {
            _deleteBillService.DeleteBill(date, amount, bill);

            return View("Index");
        }



    }
}
