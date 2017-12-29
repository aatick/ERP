//using ERP.Web.Reports;

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
    public class AccIncExpReportController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
       // private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
       // private readonly IOfficeService officeService;
        public AccIncExpReportController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccReportService accReportService)
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;            
            //this.processInfoService = processInfoService;
            this.accReportService = accReportService;
            //this.officeService = officeService;
        }
        #endregion

        #region Methods
        
        public ActionResult GenerateIncExpReport(int rptslnx, string from_date)
        {
            
                try
                {
                    var param = new { office_id = SessionHelper.LoggedInOfficeId, to_date = ReportHelper.FormatDateToString(from_date) };
                    var data = accReportService.GetDataIncExpReport(param);

                    ReportHelper.ShowReport(data.Tables[0], "pdf", "Rpt_Acc_IncomeExpense_New.rpt", "IncomeExpense");
                    return Content(string.Empty);
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }

        }
        #endregion

        #region Events
        // GET: AccIncExpReport
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
            
           ///////ViewData["TrxDate"] = TransactionDate.ToString("dd-MMM-yyyy");
           // var param = new { @OfficeID = SessionHelper.LoginUserOfficeID };
            //var allProducts = accReportService.GetLastInitialDate(param);

            //var detail = allProducts.ToString();

            //if (!IsDayInitiated)
            //{

            //    //ViewData["TrxDate"] = Convert.ToDateTime(allProducts.Tables[0].Rows[0][1].ToString());
            //    ViewData["TrxDate"] = DateTime.Now;
            //}
            //else
            //{
            //    ViewData["TrxDate"] = SessionHelper.BusinessDate;
            //}
            return View();
        }

        // GET: AccIncExpReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccIncExpReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccIncExpReport/Create
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

        // GET: AccIncExpReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccIncExpReport/Edit/5
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

        // GET: AccIncExpReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccIncExpReport/Delete/5
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
