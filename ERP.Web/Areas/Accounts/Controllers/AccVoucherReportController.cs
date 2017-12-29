using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Accounts.Data.AccountsDataModel;
using Accounts.Service;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.ReportServies;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;

namespace Accounts.Controllers
{
    public class AccVoucherReportController : BaseController
    {
        // GET: AccVoucherReport
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
       // private readonly IProcessInfoService processInfoService;
        private readonly IAccReportService accReportService;
        private readonly ISPService spService;
        private readonly IAccVoucherTypeService accVoucherTypeService;

        public AccVoucherReportController(IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccChartService accChartService, IAccLastVoucherService accLastVoucherService, IAccReportService accReportService, ISPService spService, IAccVoucherTypeService accVoucherTypeService)//IProcessInfoService processInfoService,
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;
            this.accLastVoucherService = accLastVoucherService;
            this.accReportService = accReportService;
            this.spService = spService;
            this.accVoucherTypeService = accVoucherTypeService;
        }
        #endregion



        public void Acc_Voucher_AccountWise(string rptslnx, string FromDate, string DateTo, string AccID)
        {
            var exportType = "pdf";
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        AccID = AccID,
                        from_date = ReportHelper.FormatDateToString(FromDate),
                        to_date = ReportHelper.FormatDateToString(DateTo),
                        office_id = SessionHelper.LoggedInOfficeId
                    },
                    "Proc_Rpt_Acc_Voucher_AccountWise").Tables[0];
            ReportHelper.ShowReport(data, exportType, "Rpt_Acc_Voucher_AccountWise.rpt", "Rpt_Acc_Voucher_AccountWise");

        }



        public void Auto_Voucher_Info(string rptslnx, string FromDate, string ToDate)
        {
            var exportType = "pdf";
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TrxDateFrom = ReportHelper.FormatDateToString(FromDate),
                        TrxDateTo = ReportHelper.FormatDateToString(ToDate),
                    },
                    "GetAutoVoucherInformation").Tables[0];
            ReportHelper.ShowReport(data, exportType, "Rpt_Auto_Voucher_Info.rpt", "Rpt_Auto_Voucher_Info");
        }


        #region Methods
        private void MapDropDownList(AccVoucherEntryViewModel model)
        {
            var allVoucher = accVoucherTypeService.GetAll().Where(x=>x.VoucherTypeShortName != "AV");
            var viewVou = allVoucher.Select(x => x).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.VoucherType.ToString()
            });
            var Voucher_items = new List<SelectListItem>();
            Voucher_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            Voucher_items.AddRange(viewVou);
            model.VoucherTypeList = Voucher_items;

            //var type_item = new List<SelectListItem>();
            //type_item.Add(new SelectListItem() { Text = "Please Select", Value = "0" });
            //type_item.Add(new SelectListItem() { Text = "Cash (Debit)", Value = "CAD" });
            //type_item.Add(new SelectListItem() { Text = "Cash (Credit)", Value = "CAC" });
            ////type_item.Add(new SelectListItem() { Text = "Bank", Value = "Ba" });
            //type_item.Add(new SelectListItem() { Text = "Bank(Debit)", Value = "BDr" });
            //type_item.Add(new SelectListItem() { Text = "Bank(Credit)", Value = "BCr" });
            ////type_item.Add(new SelectListItem() { Text = "Bank(Cheque)", Value = "Ba" });
            ////type_item.Add(new SelectListItem() { Text = "Bank(Cash)", Value = "Bc" });
            //type_item.Add(new SelectListItem() { Text = "Journal", Value = "Jr" });
            //model.VoucherTypeList = type_item;

            var blnk_items = new List<SelectListItem>();
            //blnk_items.Add(new SelectListItem() { Text = "", Value = "0", Selected = true });            
            model.VoucherNoList = blnk_items;
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetVoucherList(string voucherTypeId, string trxDt, string trxDateTo)
        {
            

            try
            {
                List<AccTrxMaster> List_AccTrxMaster = new List<AccTrxMaster>();
                var param = new { voucherTypeId = voucherTypeId, TrxDate = ReportHelper.FormatDateToString(trxDt), trxDateTo = ReportHelper.FormatDateToString(trxDateTo), OfficeID = SessionHelper.LoggedInOfficeId };
                var getVoucher = accReportService.GetAccVoucherByVoucherType(param);

                List_AccTrxMaster = getVoucher.Tables[0].AsEnumerable()
                .Select(row => new AccTrxMaster
                {
                    TrxMasterID = row.Field<long>("TrxMasterID"),
                    VoucherNo = row.Field<long?>("VoucherNo")
                    
                }).ToList();

                return Json(List_AccTrxMaster, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


            //var getVoucher = accTrxMasterService.GetByTrxDt_VType(voucherType, Convert.ToDateTime(trxDt), Convert.ToInt32(offcId));
            //var viewVoucher = getVoucher.Select(x => x).ToList().Select(x => new SelectListItem
            //{
            //    Value = x.TrxMasterID.ToString(),
            //    Text = x.VoucherNo.ToString()
            //});
            //var voucher_items = new List<SelectListItem>();
            //if (viewVoucher.ToList().Count > 0)
            //{
            //    voucher_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            //}
            //voucher_items.AddRange(viewVoucher);
            //return Json(voucher_items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenerateVoucherReport(string rptslnx, string FromDate, string DateTo, string VoucherTypeId, string voucher_id)
        {
            try
            {
                if (voucher_id == "0" || voucher_id == "null")
                {
                    var param = new { voucherTypeId = VoucherTypeId, from_date =ReportHelper.FormatDateToString(FromDate), to_date =ReportHelper.FormatDateToString(DateTo), office_id = SessionHelper.LoggedInOfficeId};
                    var allVouchers = spService.GetDataWithParameter(param, "Proc_Rpt_Acc_Voucher_All");

                    ReportHelper.ShowReport(allVouchers.Tables[0],"pdf","rpt_acc_Allvoucher.rpt","Allvoucher");
                    return Content(string.Empty);

                }
                else
                {
                    var param = new { voucher_id = voucher_id };
                    var allVouchers = accReportService.GetDataVoucher(param);
                    ReportHelper.ShowReport(allVouchers.Tables[0], "pdf", "rpt_acc_voucher.rpt", "voucher");
                    return Content(string.Empty);
                }
                

                // return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult GenerateMasterWiseVoucherReport(string voucher_id)
        {
            try
            {
                    var param = new { voucher_id = voucher_id };
                    var allVouchers = accReportService.GetDataVoucher(param);
                    ReportHelper.ShowReport(allVouchers.Tables[0], "pdf", "rpt_acc_voucher.rpt", "voucher");
                    return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult GenerateAllVoucherReport(string voucher_typeId, string trxDt, string trxDateTo)
        {
            try
            {

                var param = new { voucher_typeId = voucher_typeId, office_id = SessionHelper.LoggedInOfficeId, trx_date =ReportHelper.FormatDateToString(trxDt), trxDateTo = ReportHelper.FormatDateToString(trxDateTo) };
                var allVouchers = accReportService.GetDataVoucherAll(param);
                var reportParam = new Dictionary<string, object>();
                reportParam.Add("param_OrgName", ApplicationSettings.OrganiztionName);
                //ReportHelper.PrintReport("rptLoanLedger.rpt", allproducts.Tables[0], reportParam);
                //return Content(string.Empty); ;
                //////if (voucher_type == "CAC")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all_Amount.rpt", allVouchers.Tables[0], reportParam);
                //////else if (voucher_type == "CAD")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all_Amount.rpt", allVouchers.Tables[0], reportParam);
                //////else if (voucher_type == "BDr")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all_Amount.rpt", allVouchers.Tables[0], reportParam);
                //////else if (voucher_type == "BCr")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all_Amount.rpt", allVouchers.Tables[0], reportParam);
                //////else if (voucher_type == "Ba")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all.rpt", allVouchers.Tables[0], reportParam);
                //////else if (voucher_type == "Bc")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all.rpt", allVouchers.Tables[0], reportParam);
                //////else if (voucher_type == "Jr")
                //////    ReportHelper.PrintReport("rpt_acc_voucher_all.rpt", allVouchers.Tables[0], reportParam);


                //ReportHelper.PrintReport("rpt_acc_voucher_all.rpt", allVouchers.Tables[0], new Dictionary<string, object>());
                //ReportHelper.PrintReport("rpt_acc_voucher_all.rpt", allVouchers.Tables[0], reportParam);
                
                return Content(string.Empty);

                // return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion

        #region Events

        public ActionResult AccTrans()
        {
            return View();
        }
        public ActionResult Index()
        {
            var model = new AccVoucherEntryViewModel();
            MapDropDownList(model);
            //model.TrxDate = processInfoService.GetByOfficeId(Convert.ToInt32(SessionHelper.LoginUserOfficeID)).BusinessDate;
            //model.TrxDate = processInfoService.GetInitialDtByOfficeId(Convert.ToInt32(SessionHelper.LoginUserOfficeID));
            //model.TrxDate = SessionHelper.TransactionDate;
            //if (IsDayInitiated)
            //    model.TrxDate = TransactionDate;
            
            //ViewData["TransactionDate"] = model.TrxDate.ToString("dd-MMM-yyyy");
            model.VoucherType = "0";
            //model.OfficeID = 6;
            model.OfficeID = Convert.ToInt32(SessionHelper.LoggedInOfficeId);
            //Session[sessionName] = CreateTable();

            //DateTime VDate;
            //VDate = System.DateTime.Now;
            //if (IsDayInitiated)
            //{
            //    ViewData["Trxdate"] = SessionHelper.BusinessDate;
                
            //}
            //else
            //{
            //    ViewData["Trxdate"] = DateTime.Now;
                
            //}
            
            return View(model);
        }

       // GET: AccVoucherReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccVoucherReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccVoucherReport/Create
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

        // GET: AccVoucherReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccVoucherReport/Edit/5
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

        // GET: AccVoucherReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccVoucherReport/Delete/5
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
