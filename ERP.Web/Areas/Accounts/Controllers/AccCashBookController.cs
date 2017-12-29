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
    public class AccCashBookController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
        //private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
        //private readonly IOfficeService officeService;

        public AccCashBookController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccLastVoucherService accLastVoucherService, IAccReportService accReportService)//IProcessInfoService processInfoService,, IOfficeService officeService
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
        public ActionResult GenerateCashBookReport(string rptslnx, string office_id, string from_date, string to_date, string acc_level)
        {
            try
            {
               // var param = new { Office = SessionHelper.LoginUserOfficeID, Date = Date };
                var param = new { office_id = office_id, from_date =ReportHelper.FormatDateToString(from_date), to_date = ReportHelper.FormatDateToString(to_date), AccLevel = acc_level };
                var allVouchers = accReportService.GetDataCashBookReport(param);
                                      
                var reportParam = new Dictionary<string, object>();
                reportParam.Add("Param_OrgName", ApplicationSettings.OrganiztionName);
                reportParam.Add("FromDate", from_date);
                reportParam.Add("ToDate", to_date);


                ReportHelper.PrintReport("rpt_acc_cashbook.rpt", allVouchers.Tables[0], reportParam);
             //   ReportHelper.PrintReport("rpt_MRA_ProvisionCalculation.rpt", Mras.Tables[0], new Dictionary<string, object>());
         
        

        //    public ActionResult GenerateMraMIS4BReport(string Date)
        //{
        //    try
        //    {
        //        var param = new { Office = SessionHelper.LoginUserOfficeID, Date = Date };

        //        var MraMIS4B1s = mraReportService.GetDataMraMISReport4B1(param);
        //        var MraMIS4B2s = mraReportService.GetDataMraMISReport4B2(param);

        //        var subReportDB = new Dictionary<string, DataTable>();
        //        subReportDB.Add("MRA-MIS-04B_Part02.rpt", MraMIS4B2s.Tables[0]);

        //        ReportHelper.PrintWithSubReport("MRA-MIS-04B.rpt", MraMIS4B1s.Tables[0], new Dictionary<string, object>(), subReportDB, new MRA_MIS_04B());


        //        return Content(string.Empty);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}
                //ReportHelper.MyPrintReport("rpt_acc_cashbook.rpt", allVouchers.Tables[0], allbanks.Tables[0], reportParam);
                //ReportHelper.PrintWithSubReport("rpt_acc_cashbook.rpt", allVouchers.Tables[0], reportParam, subReportDB);

                //ReportDocument crDocument = new ReportDocument();

                //ExportOptions crExportOptions = new ExportOptions();
                //DiskFileDestinationOptions crDiskFileDestination = new DiskFileDestinationOptions();
                //string strFName;
                //string strReportPathAbsolute = System.Web.HttpContext.Current.Server.MapPath("~/Reports/" + "rpt_acc_cashbook.rpt");
                //crDocument.Load(strReportPathAbsolute);
                ////crDocument.OpenSubreport("rpt_acc_cashbook_bank.rpt").SetDataSource(allbanks);
                //crDocument.SetDataSource(allVouchers);
                //crDocument.SetParameterValue("Param_OrgName", ApplicationSettings.OrganiztionName);
                //crDocument.SetParameterValue("FromDate", from_date);
                //crDocument.SetParameterValue("ToDate", to_date);


                //strFName = System.Web.HttpContext.Current.Server.MapPath("~/") + string.Format("{0}.pdf", Guid.NewGuid());
                //crDiskFileDestination.DiskFileName = strFName;
                //crExportOptions = crDocument.ExportOptions;
                //crExportOptions.DestinationOptions = crDiskFileDestination;
                //crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //crDocument.Export();
                //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                //System.Web.HttpContext.Current.Response.WriteFile(strFName);
                //System.Web.HttpContext.Current.Response.End();
                //// Response.Close();
                //System.IO.File.Delete(strFName);
                //*********************************************************

                





                return Content(string.Empty);

                //Incase of subreport.
                //var subReportDataSources = new Dictionary<string, DataTable>();
                //subReportDataSources.Add("Subreport1", allproducts.Tables[0]);
                //subReportDataSources.Add("Subreport2", allproducts.Tables[0]);
                //ReportHelper.PrintWithSubReport("", allproducts.Tables[0], new Dictionary<string, object>(), subReportDataSources);
                // return Content(string.Empty); 
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        #region Events
        // GET: AccCashBook
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

        // GET: AccCashBook/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccCashBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccCashBook/Create
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

        // GET: AccCashBook/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccCashBook/Edit/5
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

        // GET: AccCashBook/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccCashBook/Delete/5
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
