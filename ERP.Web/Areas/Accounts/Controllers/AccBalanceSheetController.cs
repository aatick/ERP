﻿//using ERP.Web.Reports;

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
    public class AccBalanceSheetController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
       // private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
       // private readonly IOfficeService officeService;

        public AccBalanceSheetController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccLastVoucherService accLastVoucherService, IAccReportService accReportService)
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;
            this.accLastVoucherService = accLastVoucherService;
           // this.processInfoService = processInfoService;
            this.accReportService = accReportService;
           // this.officeService = officeService;
        }
        #endregion

        #region Methods
        //public JsonResult GetHOList()
        //{
        //    var First_Level = officeService.GetByOfficeOrgID(Convert.ToInt32(SessionHelper.LoginUserOfficeID), Convert.ToInt32(LoggedInOrganizationID));
        //    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 1 && c.FirstLevel == First_Level.FirstLevel);
        //    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        //    {
        //        Value = x.OfficeID.ToString(),
        //        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        //    });
        //    var office_items = new List<SelectListItem>();
        //    if (viewOffice.ToList().Count > 0)
        //    {
        //        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        //    }
        //    office_items.AddRange(viewOffice);
        //    return Json(office_items, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetZoneList(string HO_val)
        //{
        //    var ofcId = officeService.GetById(Convert.ToInt32(HO_val)).OfficeCode;
        //    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 2 && c.FirstLevel == ofcId && c.OrgID == Convert.ToInt32(LoggedInOrganizationID));
        //    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        //    {
        //        Value = x.OfficeID.ToString(),
        //        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        //    });
        //    var office_items = new List<SelectListItem>();
        //    if (viewOffice.ToList().Count > 0)
        //    {
        //        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        //    }
        //    office_items.AddRange(viewOffice);
        //    return Json(office_items, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetAreaList(string HO_val,string zone_val)
        //{
        //    var ho_code = officeService.GetById(Convert.ToInt32(HO_val)).OfficeCode;
        //    var zone_code = officeService.GetById(Convert.ToInt32(zone_val)).OfficeCode;
        //    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 3 && c.FirstLevel == ho_code && c.SecondLevel == zone_code && c.OrgID == Convert.ToInt32(LoggedInOrganizationID));
        //    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        //    {
        //        Value = x.OfficeID.ToString(),
        //        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        //    });
        //    var office_items = new List<SelectListItem>();
        //    if (viewOffice.ToList().Count > 0)
        //    {
        //        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        //    }
        //    office_items.AddRange(viewOffice);
        //    return Json(office_items, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetOfficeList(string HO_val, string zone_val, string area_val)
        //{
        //    var ho_code = officeService.GetById(Convert.ToInt32(HO_val)).OfficeCode;
        //    var zone_code = officeService.GetById(Convert.ToInt32(zone_val)).OfficeCode;
        //    var area_code = officeService.GetById(Convert.ToInt32(area_val)).OfficeCode;
        //    var OfficeList = officeService.GetAll().Where(c => c.OfficeLevel == 4 && c.FirstLevel == ho_code && c.SecondLevel == zone_code && c.ThirdLevel == area_code && c.OrgID == Convert.ToInt32(LoggedInOrganizationID));
        //    var viewOffice = OfficeList.Select(x => x).ToList().Select(x => new SelectListItem
        //    {
        //        Value = x.OfficeID.ToString(),
        //        Text = x.OfficeCode.ToString() + " " + x.OfficeName.ToString()
        //    });
        //    var office_items = new List<SelectListItem>();
        //    if (viewOffice.ToList().Count > 0)
        //    {
        //        office_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
        //    }
        //    office_items.AddRange(viewOffice);
        //    return Json(office_items, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GenerateBalanceSheetReport(string rptslnx, string to_date)
        {
            try
            {
                var param = new { office_id = SessionHelper.LoggedInOfficeId, to_date =ReportHelper.FormatDateToString(to_date) };
                var data = accReportService.GetDataBalanceSheetReport(param);

                ReportHelper.ShowReport(data.Tables[0], "pdf", "Rpt_Acc_BalanceSheet_New.rpt", "BalanceSheet");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

            //try
            //{
            //    ////var main_param = new { office_id = office_id, orgName = ApplicationSettings.OrganiztionName, FromDate = ReportHelper.FormatDateToString(to_date), ToDate = ReportHelper.FormatDateToString(to_date) };
                ////var liabilities_param = new { to_date = ReportHelper.FormatDateToString(to_date), office_id = office_id, AccLevel = acc_level, And_Condition = " AND t.AccType='L'" };
                ////var assets_param = new { to_date = ReportHelper.FormatDateToString(to_date), office_id = office_id, AccLevel = acc_level, And_Condition = " AND t.AccType='A'" };
                ////var allVouchers = accReportService.GetDataIncExpReport(main_param);

                ////var Liabilities_DB = accReportService.GetNewAccDataForReport(liabilities_param, "Proc_Rpt_Acc_BalanceSheet");
                ////var Assets_DB = accReportService.GetNewAccDataForReport(assets_param, "Proc_Rpt_Acc_BalanceSheet");

                ////var subReportDB = new Dictionary<string, DataTable>();
                ////subReportDB.Add("rpt_acc_sub_liabilities", Liabilities_DB.Tables[0]);
                ////subReportDB.Add("rpt_acc_sub_asset", Assets_DB.Tables[0]);

                //////var reportParam = new Dictionary<string, object>();
                //////reportParam.Add("Parameter_param_orgName", ApplicationSettings.OrganiztionName);
                //////reportParam.Add("FromDate", from_date);
                //////reportParam.Add("ToDate", to_date);

                ////ReportHelper.PrintWithSubReport("rpt_acc_balance_sheet.rpt", allVouchers.Tables[0], new Dictionary<string, object>(), subReportDB, new rpt_acc_balance_sheet());

                //////ReportHelper.PrintReport("rpt_acc_inc_exp.rpt", allVouchers.Tables[0], reportParam);
                ////return Content(string.Empty);

                //Incase of subreport.
                //var subReportDataSources = new Dictionary<string, DataTable>();
                //subReportDataSources.Add("Subreport1", allproducts.Tables[0]);
                //subReportDataSources.Add("Subreport2", allproducts.Tables[0]);
                //ReportHelper.PrintWithSubReport("", allproducts.Tables[0], new Dictionary<string, object>(), subReportDataSources);
                // return Content(string.Empty); 
           // }
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }

        #endregion

        #region Events
        // GET: AccBalanceSheet
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
            
            ////ViewData["TrxDate"] = TransactionDate.ToString("dd-MMM-yyyy");
            //var param = new { @OfficeID = SessionHelper.LoginUserOfficeID };
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

        // GET: AccBalanceSheet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccBalanceSheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccBalanceSheet/Create
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

        // GET: AccBalanceSheet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccBalanceSheet/Edit/5
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

        // GET: AccBalanceSheet/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccBalanceSheet/Delete/5
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
