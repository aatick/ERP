//using ERP.Web.Reports;

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Accounts.Service;
using Common.Service;
using Common.Service.ReportServies;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;

namespace Accounts.Controllers
{
    public class AccCashAtBankController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
        private readonly ISPService spService;
       // private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
       // private readonly IOfficeService officeService;

        public AccCashAtBankController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccLastVoucherService accLastVoucherService, IAccReportService accReportService, ISPService spService)
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;
            this.accLastVoucherService = accLastVoucherService;
           // this.processInfoService = processInfoService;
            this.accReportService = accReportService;
            this.spService = spService;
           // this.officeService = officeService;
        }
        #endregion

        #region Methods
       
        public ActionResult GenerateCashAtBankReport(int rptslnx, string from_date, string to_date, string ReportType)
        {
            try//Rpt_Acc_Voucher_BankBook 
            {
                var exportType = "pdf";
                var data =
              spService.GetDataWithParameter(
                  new { ReportType = ReportType,  from_date = ReportHelper.FormatDateToString(from_date), to_date = ReportHelper.FormatDateToString(to_date), office_id = SessionHelper.LoggedInOfficeId },
                  "Rpt_Acc_Voucher_BankBook").Tables[0];
                ReportHelper.ShowReport(data, exportType, "Rpt_Acc_Voucher_Bankbook.rpt", "Rpt_Acc_Voucher_Bankbook");
        
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion

        #region Events
        // GET: AccCashAtBank
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            //ViewData["HOList"] = items;
            //ViewData["ZoneList"] = items;
            //ViewData["AreaList"] = items;
            //ViewData["OfficeList"] = items;
            //var offcdetail = officeService.GetById(Convert.ToInt32(SessionHelper.LoginUserOfficeID));
            //ViewData["OfficeLevel"] = offcdetail.OfficeLevel;
            //ViewData["FirstLevel"] = officeService.GetByOfficeCode(offcdetail.FirstLevel).OfficeID;
            //ViewData["SecondLevel"] = officeService.GetByOfficeCode(offcdetail.SecondLevel).OfficeID;
            //ViewData["ThirdLevel"] = officeService.GetByOfficeCode(offcdetail.ThirdLevel).OfficeID;
            //ViewData["FourthLevel"] = officeService.GetByOfficeCode(offcdetail.FourthLevel).OfficeID;
            //ViewData["TrxDate"] = SessionHelper.BusinessDate;
            return View();
        }

        // GET: AccCashAtBank/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccCashAtBank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccCashAtBank/Create
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

        // GET: AccCashAtBank/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccCashAtBank/Edit/5
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

        // GET: AccCashAtBank/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccCashAtBank/Delete/5
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
