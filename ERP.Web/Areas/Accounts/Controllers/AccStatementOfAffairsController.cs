using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Accounts.Service;
using Common.Service;
using Common.Service.ReportServies;
using ERP.Web.Controllers;
using ERP.Web.Helpers;

namespace Accounts.Controllers
{
    public class AccStatementOfAffairsController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
        ////private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
       //// private readonly IOfficeService officeService;

        public AccStatementOfAffairsController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccLastVoucherService accLastVoucherService , IAccReportService accReportService)
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;
            this.accLastVoucherService = accLastVoucherService;
           //// this.processInfoService = processInfoService;
            this.accReportService = accReportService;
            ////this.officeService = officeService;
        }
        #endregion

        #region Methods
        public ActionResult GenerateStatementOfAffairsReport(string rptslnx, string office_id, string from_date, string to_date, string acc_level, string report_name, string reportNo)
        {
            try
            {
                var param = new { office_id = office_id, from_date =ReportHelper.FormatDateToString(from_date), to_date =ReportHelper.FormatDateToString(to_date), AccLevel = acc_level };
                if (reportNo == "1")
                {
                    var allVouchers = accReportService.GetStatementOfAffairsReport(param);
                    var reportParam = new Dictionary<string, object>();
                    reportParam.Add("param_orgName", ApplicationSettings.OrganiztionName);
                    reportParam.Add("DateFrom", from_date);
                    reportParam.Add("DateTo", to_date);
                    reportParam.Add("Report_Name", report_name);
                    ReportHelper.PrintReport("rpt_acc_statement_of_affairs.rpt", allVouchers.Tables[0], reportParam);
                }
                else
                {
                    var allVouchers = accReportService.GetStatementOfClosingAffairsReport(param);
                    var reportParam = new Dictionary<string, object>();
                    reportParam.Add("param_orgName", ApplicationSettings.OrganiztionName);
                    reportParam.Add("DateFrom", from_date);
                    reportParam.Add("DateTo", to_date);
                    reportParam.Add("Report_Name", report_name);
                    ReportHelper.PrintReport("rpt_acc_statement_of_affairs.rpt", allVouchers.Tables[0], reportParam);
                }
                
                return Content(string.Empty);                
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        #region Events
        // GET: AccTrialBalance
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            //ViewData["HOList"] = items;
            //ViewData["ZoneList"] = items;
            //ViewData["AreaList"] = items;
            //ViewData["OfficeList"] = items;
            //ViewData["AccCodeList"] = items;
            //var offcdetail = officeService.GetById(Convert.ToInt32(SessionHelper.LoginUserOfficeID));
            //ViewData["OfficeLevel"] = offcdetail.OfficeLevel;
            //ViewData["FirstLevel"] = officeService.GetByOfficeCode(offcdetail.FirstLevel).OfficeID;
            //ViewData["SecondLevel"] = officeService.GetByOfficeCode(offcdetail.SecondLevel).OfficeID;
            //ViewData["ThirdLevel"] = officeService.GetByOfficeCode(offcdetail.ThirdLevel).OfficeID;
            //ViewData["FourthLevel"] = officeService.GetByOfficeCode(offcdetail.FourthLevel).OfficeID;
            //ViewData["TrxDate"] = SessionHelper.BusinessDate;
            return View();
        }
        public ActionResult ClosingAffairs()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["HOList"] = items;
            ViewData["ZoneList"] = items;
            ViewData["AreaList"] = items;
            ViewData["OfficeList"] = items;
            ViewData["AccCodeList"] = items;
            //var offcdetail = officeService.GetById(Convert.ToInt32(SessionHelper.LoginUserOfficeID));
            //ViewData["OfficeLevel"] = offcdetail.OfficeLevel;
            //ViewData["FirstLevel"] = officeService.GetByOfficeCode(offcdetail.FirstLevel).OfficeID;
            //ViewData["SecondLevel"] = officeService.GetByOfficeCode(offcdetail.SecondLevel).OfficeID;
            //ViewData["ThirdLevel"] = officeService.GetByOfficeCode(offcdetail.ThirdLevel).OfficeID;
            //ViewData["FourthLevel"] = officeService.GetByOfficeCode(offcdetail.FourthLevel).OfficeID;
            //ViewData["TrxDate"] = TransactionDate.ToString("dd-MMM-yyyy");
            return View();
        }

        // GET: AccTrialBalance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccTrialBalance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccTrialBalance/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AccTrialBalance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccTrialBalance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AccTrialBalance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccTrialBalance/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
