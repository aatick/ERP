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
    public class AccTrialBalanceController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
        //// private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
        //// private readonly IOfficeService officeService;

        public AccTrialBalanceController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccLastVoucherService accLastVoucherService, IAccReportService accReportService)
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;
            this.accLastVoucherService = accLastVoucherService;
            ////this.processInfoService = processInfoService;
            this.accReportService = accReportService;
            ////this.officeService = officeService;
        }
        #endregion

        #region Methods
        ////public JsonResult GetHOList()
        ////{
        ////   //// var First_Level = officeService.GetByOfficeOrgID(Convert.ToInt32(SessionHelper.LoginUserOfficeID), Convert.ToInt32(LoggedInOrganizationID));
        ////   //// var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 1 && c.FirstLevel == First_Level.FirstLevel);
        ////   // //var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        ////   // //{
        ////   // //    Value = x.OfficeID.ToString(),
        ////   // //    Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        ////   // //});
        ////   // //var office_items = new List<SelectListItem>();
        ////   // //if (viewOffice.ToList().Count > 0)
        ////   // //{
        ////   // //    office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        ////   // //}
        ////   // //office_items.AddRange(viewOffice);
        ////    return Json(office_items, JsonRequestBehavior.AllowGet);
        ////}
        ////public JsonResult GetZoneList(string HO_val)
        ////{
        ////    var ofcId = officeService.GetById(Convert.ToInt32(HO_val)).OfficeCode;
        ////    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 2 && c.FirstLevel == ofcId && c.OrgID == Convert.ToInt32(LoggedInOrganizationID));
        ////    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        ////    {
        ////        Value = x.OfficeID.ToString(),
        ////        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        ////    });
        ////    var office_items = new List<SelectListItem>();
        ////    if (viewOffice.ToList().Count > 0)
        ////    {
        ////        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        ////    }
        ////    office_items.AddRange(viewOffice);
        ////    return Json(office_items, JsonRequestBehavior.AllowGet);
        ////}
        ////public JsonResult GetAreaList(string HO_val,string zone_val)
        ////{
        ////    var ho_code = officeService.GetById(Convert.ToInt32(HO_val)).OfficeCode;
        ////    var zone_code = officeService.GetById(Convert.ToInt32(zone_val)).OfficeCode;
        ////    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 3 && c.FirstLevel == ho_code && c.SecondLevel == zone_code && c.OrgID == Convert.ToInt32(LoggedInOrganizationID));
        ////    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        ////    {
        ////        Value = x.OfficeID.ToString(),
        ////        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        ////    });
        ////    var office_items = new List<SelectListItem>();
        ////    if (viewOffice.ToList().Count > 0)
        ////    {
        ////        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        ////    }
        ////    office_items.AddRange(viewOffice);
        ////    return Json(office_items, JsonRequestBehavior.AllowGet);
        ////}
        ////public JsonResult GetOfficeList(string HO_val, string zone_val, string area_val)
        ////{
        ////    var ho_code = officeService.GetById(Convert.ToInt32(HO_val)).OfficeCode;
        ////    var zone_code = officeService.GetById(Convert.ToInt32(zone_val)).OfficeCode;
        ////    var area_code = officeService.GetById(Convert.ToInt32(area_val)).OfficeCode;
        ////    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 4 && c.FirstLevel == ho_code && c.SecondLevel == zone_code && c.ThirdLevel == area_code && c.OrgID == Convert.ToInt32(LoggedInOrganizationID));
        ////    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        ////    {
        ////        Value = x.OfficeID.ToString(),
        ////        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        ////    });
        ////    var office_items = new List<SelectListItem>();
        ////    if (viewOffice.ToList().Count > 0)
        ////    {
        ////        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        ////    }
        ////    office_items.AddRange(viewOffice);
        ////    return Json(office_items, JsonRequestBehavior.AllowGet);
        ////}

        public ActionResult GenerateTrialBalanceReport(string rptslnx, string from_date, string to_date)
        {
            try
            {

                var param = new { office_id = SessionHelper.LoggedInOfficeId, from_date = ReportHelper.FormatDateToString(from_date), to_date = ReportHelper.FormatDateToString(to_date) };
                var allVouchers = accReportService.GetDataTrialBalanceNew(param);
                ////reportParam.Add("param_orgName", ApplicationSettings.OrganiztionName);
                ////reportParam.Add("from_date", from_date);
                ////reportParam.Add("to_date", to_date);


                ReportHelper.ShowReport(allVouchers.Tables[0], "pdf", "rpt_acc_trial_balance_New.rpt", "trial_balance");
                return Content(string.Empty);

                // return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        //public ActionResult GenerateTrialBalanceReport(string office_id, string from_date, string to_date, string acc_level)
        //{
        //    try
        //    {

        //        var param = new { from_date = ReportHelper.FormatDateToString(from_date), to_date = ReportHelper.FormatDateToString(to_date), AccLevel = acc_level };
        //        var allVouchers = accReportService.GetDataTrialBalance(param);
        //        var reportParam = new Dictionary<string, object>();
        //        reportParam.Add("param_orgName", ApplicationSettings.OrganiztionName);
        //        reportParam.Add("from_date", from_date);
        //        reportParam.Add("to_date", to_date);

        //        //ReportHelper.PrintReport("rptLoanLedger.rpt", allproducts.Tables[0], reportParam);
        //        //return Content(string.Empty); ;

        //        ReportHelper.PrintReport("rpt_acc_trial_balance.rpt", allVouchers.Tables[0], reportParam);
        //        return Content(string.Empty);

        //        // return Json(new { Result = "OK" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

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
            ////var offcdetail = officeService.GetById(Convert.ToInt32(SessionHelper.LoginUserOfficeID));
            ////ViewData["OfficeLevel"] = offcdetail.OfficeLevel;
            ////ViewData["FirstLevel"] = officeService.GetByOfficeCode(offcdetail.FirstLevel).OfficeID;
            ////ViewData["SecondLevel"] = officeService.GetByOfficeCode(offcdetail.SecondLevel).OfficeID;
            ////ViewData["ThirdLevel"] = officeService.GetByOfficeCode(offcdetail.ThirdLevel).OfficeID;
            ////ViewData["FourthLevel"] = officeService.GetByOfficeCode(offcdetail.FourthLevel).OfficeID;


            // ViewData["TrxDate"] = SessionHelper.BusinessDate;
            ////var param = new { @OfficeID = SessionHelper.LoginUserOfficeID };
            ////var allProducts = accReportService.GetLastInitialDate(param);

            ////var detail = allProducts.ToString();

            //if (!IsDayInitiated)
            //{

            //    //ViewData["TrxDate"] = Convert.ToDateTime(allProducts.Tables[0].Rows[0][1].ToString());
            //    ViewData["TrxDate"] = Convert.ToString("dd-MMM-yyyy");
            //}
            //else
            //{
            //    ViewData["TrxDate"] = TransactionDate;
            //}

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
