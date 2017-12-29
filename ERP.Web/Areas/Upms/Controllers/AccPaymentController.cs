//using System.Web.UI.WebControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class AccPaymentController : BaseController
    {

        #region Variables

        private readonly IInvestorInfoService investorInfoService;
        private readonly IAccPaymentService accPaymentService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly ISPService spService;
        private readonly IAccBankBranchGLService accBankBranchGLService;
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccTransactionModeService accTransactionModeService;
        private readonly IAccReceiptPaymentMappingService accReceiptPaymentMappingService;
        private readonly IInvestorBalanceDailyService investorBalanceDailyService;
        private readonly IAccPaymentRequisitionService accPaymentRequisitionService;
        public AccPaymentController(
            IAccPaymentService accPaymentService
            , IBrokerBranchService brokerBranchService
            , ISPService spService
            , IAccBankBranchGLService accBankBranchGLService
            , IAccTrxMasterService accTrxMasterService
            , IAccTrxDetailService accTrxDetailService
            , IAccTransactionModeService accTransactionModeService
            , IAccReceiptPaymentMappingService accReceiptPaymentMappingService
            , IInvestorBalanceDailyService investorBalanceDailyService
            , IInvestorInfoService investorInfoService
            , IAccPaymentRequisitionService accPaymentRequisitionService)
        {
            this.accPaymentService = accPaymentService;
            this.brokerBranchService = brokerBranchService;
            this.spService = spService;
            this.accBankBranchGLService = accBankBranchGLService;
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accTransactionModeService = accTransactionModeService;
            this.accReceiptPaymentMappingService = accReceiptPaymentMappingService;
            this.investorBalanceDailyService = investorBalanceDailyService;
            this.investorInfoService = investorInfoService;
            this.accPaymentRequisitionService = accPaymentRequisitionService;
        }


        #endregion

        #region Methods

        //

        public JsonResult RejectRequisition(int Id)
        {
            var Result = "";
            var Req = accPaymentRequisitionService.GetById(Id);
            Req.IsActive = false;
            Req.UpdateDate = DateTime.Now;
            Req.UpdateUser = SessionHelper.LoggedInUserId;
            accPaymentRequisitionService.Update(Req);
            Result = "1";
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveRequisition(int Id)
        {
            var Result = "";
            var Req = accPaymentRequisitionService.GetById(Id);
            Req.IsApprove = true;
            Req.ApproveDate = SessionHelper.TransactionDate;
            Req.ApproveUserId = SessionHelper.LoggedInUserId;
            Req.UpdateDate = DateTime.Now;
            Req.UpdateUser = SessionHelper.LoggedInUserId;
            accPaymentRequisitionService.Update(Req);
            Result = "1";
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRequisition(int Id)
        {
            var Result = "";
            var Req = accPaymentRequisitionService.GetById(Id);
            Req.IsActive = false;
            Req.UpdateDate = DateTime.Now;
            Req.UpdateUser = SessionHelper.LoggedInUserId;
            accPaymentRequisitionService.Update(Req);
            Result = "1";
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SavePaymentRequisition(int RequisitionId, int InvestorId, decimal Amount, string RequisitionDate, string Remarks)
        {
            var Result = "";
            try
            {
                if (RequisitionId == 0)  //Save
                {
                    var Req = new AccPaymentRequisition();
                    Req.InvestorId = InvestorId;
                    Req.RequisitionNo = Convert.ToInt32(spService.GetDataBySqlCommand("DECLARE @MAX int =0; SELECT @MAX=MAX(RequisitionNo) FROM AccPaymentRequisition; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString()) + 1;
                    Req.RequisitionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(RequisitionDate)); //Convert.ToDateTime(SessionHelper.BusinessDate);
                    Req.Amount = Amount;
                    Req.IsPayment = false;
                    Req.Remarks = Remarks;
                    Req.IsActive = true;
                    Req.CreateUser = SessionHelper.LoggedInUserId;
                    Req.CreateDate = DateTime.Now;
                    Req.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    Req.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);

                    accPaymentRequisitionService.Create(Req);
                }
                else //Edit
                {
                    var Req = accPaymentRequisitionService.GetById(RequisitionId);
                    Req.InvestorId = InvestorId;
                    // Req.RequisitionNo = Convert.ToInt32(spService.GetDataBySqlCommand("DECLARE @MAX int =0; SELECT @MAX=MAX(RequisitionNo) FROM AccPaymentRequisition; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString()) + 1;
                    //Req.RequisitionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(RequisitionDate));
                    Req.Amount = Amount;
                    Req.Remarks = Remarks;
                    Req.IsPayment = false;
                    Req.IsActive = true;
                    Req.CreateUser = SessionHelper.LoggedInUserId;
                    Req.CreateDate = DateTime.Now;

                    accPaymentRequisitionService.Update(Req);
                }

                Result = "1";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public void GetRequisitionInfoApprove_Rpt(string IsApprove, string IsPayment, string DateFrom, string DateTo, string InvestorCode)
        {
            var exportType = "pdf";

            var param = new { IsApprove = IsApprove, IsPayment = IsPayment, DateFrom = ReportHelper.FormatDateToString(DateFrom), DateTo = ReportHelper.FormatDateToString(DateTo), InvestorCode = InvestorCode, BrokerBranchId = SessionHelper.LoggedInOfficeId };

            var data = spService.GetDataWithParameter(param, "GetRequisitionInfoApprove_Rpt").Tables[0];
            ReportHelper.ShowReport(data, exportType, "rptRequisitionInfoApprove.rpt", "rptRequisitionInfoApprove");
        }

        public JsonResult RequisitionInfoApprove(string IsApprove, string IsPayment, string DateFrom, string DateTo, string InvestorCode)
        {
            try
            {
                var param = new { IsApprove = IsApprove, IsPayment = IsPayment, DateFrom = ReportHelper.FormatDateToString(DateFrom), DateTo = ReportHelper.FormatDateToString(DateTo), InvestorCode = InvestorCode, BrokerBranchId = SessionHelper.LoggedInOfficeId };
                var ReqList = spService.GetDataWithParameter(param, "GetRequisitionInfoApprove").Tables[0].AsEnumerable() 

                .Select(row => new
                {
                    Id = row.Field<int>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorName = row.Field<string>("InvestorName"),
                    Amount = row.Field<decimal>("Amount"),
                    RequisitionDate = row.Field<string>("RequisitionDate"),
                    RequisitionNo = row.Field<int>("RequisitionNo"),
                    PaymentMsg = row.Field<string>("PaymentMsg"),
                    ApprovedMsg = row.Field<string>("ApprovedMsg")
                }).ToList();
                return Json(ReqList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RequisitionInfo()
        {
            try
            {

                var InvestorList = spService.GetDataBySqlCommand("SELECT R.Id,R.InvestorId,D.InvestorCode +' - '+ D.InvestorName AS InvestorName,R.RequisitionNo,REPLACE(CONVERT(VARCHAR,R.RequisitionDate,106),' ','-')AS RequisitionDate,R.Amount,R.IsPayment,R.Remarks FROM AccPaymentRequisition AS R INNER JOIN InvestorDetails AS D ON D.Id = R.InvestorId WHERE R.IsActive = 1 AND R.IsPayment = 0 AND R.IsApprove = 0 AND R.BrokerBranchId = " + SessionHelper.LoggedInOfficeId + " ORDER BY R.RequisitionNo DESC").Tables[0].AsEnumerable()

                .Select(row => new
                {
                    Id = row.Field<int>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorName = row.Field<string>("InvestorName"),
                    Amount = row.Field<decimal>("Amount"),
                    RequisitionDate = row.Field<string>("RequisitionDate"),
                    RequisitionNo = row.Field<int>("RequisitionNo"),
                    Remarks = row.Field<string>("Remarks")
                }).ToList();
                return Json(InvestorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetInvestorByCodeWithRequisitionInfo(string InvestorCode)
        {
            try
            {

                var param = new { INVESTOR_CODE = InvestorCode };
                //var empList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE_RequisitionInfo");
                var empList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE_RequisitionInfo_IPO_Immature_Bal").Tables[0];
                //IsReqPayment
                if (empList.Rows.Count == 0)
                {
                    return Json(new { Result = "Error", Message = "Investor not found", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else if (Convert.ToInt32(empList.Rows[0]["IsReqPayment"]) >= 1) //This requisition is already created a payment
                {
                    return Json(new { Result = "Error", Message = "Previous payment is not approve or reject yet.", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else if (Convert.ToInt32(empList.Rows[0]["RequiditionId"]) == 0)
                {
                    return Json(new { Result = "Error", Message = "No requisition create for this investor.", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var InvestorList = empList.AsEnumerable()
                .Select(row => new   //   RequiditionId RequisitionNo RequisitionDate Amount PreparedBy BrokerBranchName
                {
                    Id = row.Field<int>("Id"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    CurrentBalance = row.Field<decimal?>("CurrentBalance"),
                    RequiditionId = row.Field<int>("RequiditionId"),
                    RequisitionNo = row.Field<int>("RequisitionNo"),
                    RequisitionDate = row.Field<string>("RequisitionDate"),
                    Amount = row.Field<decimal>("Amount"),
                    PreparedBy = row.Field<string>("PreparedBy"),
                    RequisitionInfo = row.Field<string>("RequisitionInfo"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName")
                }).ToList();

                    return Json(new { Result = "Success", Message = "Success", InvestorList = InvestorList }, JsonRequestBehavior.AllowGet);
                    // return Json(InvestorList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", MEssage = ex.Message, InvestorList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetInvestorByCode(string InvestorCode)
        {
            try
            {
                List<AutoCompleteViewModel> InvestorList = new List<AutoCompleteViewModel>();
                var param = new { INVESTOR_CODE = InvestorCode, UserId = SessionHelper.LoggedInUserId };
                //var empList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE_PaymentRequisition");
                var empList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE_PaymentRequisition_IPO_Immature_Bal").Tables[0];

                if (empList.Rows.Count == 0)
                {
                    return Json(new { Result = "Error", Message = "No investor fount.", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else if (Convert.ToInt32(empList.Rows[0]["HavePayment"]) >= 1)
                {
                    return Json(new { Result = "Error", Message = "Previous Payment is not approve or reject.", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else if (Convert.ToInt32(empList.Rows[0]["HaveRequisition"]) >= 1)
                {
                    return Json(new { Result = "Error", Message = "Previous requisition is pending.", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    InvestorList = empList.AsEnumerable()
               .Select(row => new AutoCompleteViewModel
               {
                   Id = row.Field<int>("Id"),
                   InvestorCode = row.Field<string>("InvestorCode"),
                   InvestorName = row.Field<string>("InvestorName"),
                   HaveRequisition = row.Field<int>("HaveRequisition"),
                   CurrentBalance = row.Field<decimal>("CurrentBalance")
               }).ToList();
                    return Json(new { Result = "Success", Message = "Previous requisition is pending.", InvestorList = InvestorList }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, InvestorList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetCurrentBalance(string InvestorCode)
        {
            decimal CurrentBalance = 0;
            var currBln = spService.GetDataBySqlCommand("SELECT B.CurrentBalance FROM InvestorBalanceDaily AS B INNER JOIN InvestorDetails AS D ON D.Id = B.InvestorId WHERE D.InvestorCode = '" + InvestorCode + "'").Tables[0];//.Rows[0][0].ToString();
            if (currBln.Rows.Count != 0)
            {
                CurrentBalance = Convert.ToDecimal(currBln.Rows[0][0]);
            }
            return Json(CurrentBalance, JsonRequestBehavior.AllowGet);
        }

        public class AutoCompleteViewModel
        {
            public int Id { get; set; }
            public string InvestorName { get; set; }
            public string InvestorCode { get; set; }
            public decimal? CurrentBalance { get; set; }
            public int HaveRequisition { get; set; }
            public int RequiditionId { get; set; }
            public int RequisitionNo { get; set; }
            public string RequisitionDate { get; set; }
            public decimal? Amount { get; set; }
            public string PreparedBy { get; set; }
            public string RequisitionInfo { get; set; }
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            var InvImg = investorInfoService.GetById(Id);
            var img = InvImg.Photograph;
            byte[] cover = img;
            return cover;
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/*");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/images/blank-headshot.jpg");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*"); ;
            }
        }

        public byte[] GetSignFromDataBase(int Id)
        {
            var InvImg = investorInfoService.GetById(Id);
            var img = InvImg.Signature;
            byte[] cover = img;
            return cover;
        }
        public ActionResult RetrieveSign(int id)
        {
            byte[] cover = GetSignFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/*");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/Images/signature.png");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*"); ;
            }
        }
        public JsonResult RejectPayment(string Id)
        {
            var Pay = accPaymentService.GetById(Convert.ToInt32(Id));
            Pay.ApproveStatus = "R";
            Pay.IsActive = false;
            Pay.ApproveDate = SessionHelper.TransactionDate;
            Pay.UpdateBy = SessionHelper.LoggedInUserId;
            Pay.UpdateDate = DateTime.Now;
            accPaymentService.Update(Pay);
            return Json(new { result = "1" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FnPaymentClearance(string Id)
        {
            try
            {
                var Pay = accPaymentService.GetById(Convert.ToInt32(Id));
                Pay.ClearanceStatus = true;
                Pay.UpdateBy = SessionHelper.LoggedInUserId;
                Pay.UpdateDate = DateTime.Now;
                accPaymentService.Update(Pay);
                return Json(new { Result = "Success", Message = "Clearance successfull. " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApprovePayment(string Id)
        {
            var result = "";
            try
            {
                var Pay = accPaymentService.GetById(Convert.ToInt32(Id));
                Pay.ApproveStatus = "Y";
                Pay.ApproveDate = SessionHelper.TransactionDate;
                Pay.UpdateBy = SessionHelper.LoggedInUserId;
                Pay.UpdateDate = DateTime.Now;
                accPaymentService.Update(Pay);

                //Requisition

                if (Pay.RequisitionId != null && Pay.RequisitionId != 0) //09 Oct 2017
                {
                    var Req = accPaymentRequisitionService.GetAll().Where(x => x.Id == Pay.RequisitionId);
                    if (Req.Count() != 0)
                    {
                        var Requi = Req.FirstOrDefault();
                        Requi.IsPayment = true;
                        Requi.UpdateUser = SessionHelper.LoggedInUserId;
                        Requi.UpdateDate = DateTime.Now;
                        accPaymentRequisitionService.Update(Requi);
                    }

                }


                // Save Acc Master

                var acc = new AccTrxMaster();

                acc.OfficeID = SessionHelper.LoggedInOfficeId;
                acc.TrxDate = SessionHelper.TransactionDate;
                acc.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                acc.VoucherType = "Cr";
                acc.VoucherTypeId = 2;
                acc.IsPosted = false;
                acc.IsYearlyClosing = false;
                acc.IsAutoVoucher = true;
                acc.IsRectify = false;
                acc.IsActive = true;
                acc.CreateDate = DateTime.Now;
                acc.VoucherYear = ReportHelper.FormatDate(SessionHelper.BusinessDate).Year;
                acc.CreateUser = SessionHelper.LoggedInUserId;

                var accMasterId = accTrxMasterService.Create(acc).TrxMasterID;


                var AccId = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 39 && x.AccTransactionModeId == Pay.TransactionModeId && x.FormEntryType == true).FirstOrDefault();

                var AccIdDr = AccId.AccIdDr;
                var acccodedr = AccId.AccCodeDr;
                var AccIdC = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 39 && x.AccTransactionModeId == Pay.TransactionModeId && x.FormEntryType == true).FirstOrDefault();
                var AccIdCr = AccIdC.AccIdCr;
                var AccCodeCr = AccIdC.AccCodeCr;

                //Debit

                var accDetails = new AccTrxDetail();
                accDetails.TrxMasterID = accMasterId;
                //accDetails.MarketId = 0;//Session
                //accDetails.CompanyId = 0;//Session
                accDetails.AccID = AccIdDr;
                accDetails.AccCode = acccodedr;
                accDetails.InvestorId = Pay.InvestorId;
                accDetails.TransactionTypeId = 39;
                accDetails.Debit = Pay.Amount;
                accDetails.Credit = 0;
                accDetails.CreateDate = DateTime.Now;
                accDetails.IsActive = true;
                accDetails.CreateUser = SessionHelper.LoggedInUserId;
                accDetails.InvestorBranchId = Pay.InvestorBranchId;
                accTrxDetailService.Create(accDetails);



                //Credit

                var accDe = new AccTrxDetail();

                accDe.TrxMasterID = accMasterId;
                //accDe.MarketId = 0;//Session
                //accDe.CompanyId = 0;//Session
                accDe.AccID = AccIdCr;
                accDe.AccCode = AccCodeCr;
                accDe.InvestorId = Pay.InvestorId;
                accDe.TransactionTypeId = 39;
                accDe.Debit = 0;
                accDe.Credit = Pay.Amount;
                accDe.CreateDate = DateTime.Now;
                accDe.IsActive = true;
                accDe.CreateUser = SessionHelper.LoggedInUserId;
                accDe.InvestorBranchId = Pay.InvestorBranchId;
                accTrxDetailService.Create(accDe);


                var IvnbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Pay.InvestorId);

                var balanceDaily = IvnbalanceDaily.FirstOrDefault();
                //if (IvnbalanceDaily.Count() == 0)
                //{
                //    var param = new { InvestorId = Pay.InvestorId, TransactionDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), CreatedUserId = SessionHelper.LoggedInUserId };
                //    spService.GetDataWithParameter(param, "Insert_InvestorBalanceDaily");
                //}
                //balanceDaily.ClearingChequeBalance = balanceDaily.ClearingChequeBalance + Pay.Amount;
                balanceDaily.CurrentBalance = balanceDaily.CurrentBalance - Pay.Amount;
                balanceDaily.UpdateDate = DateTime.Now;
                balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;
                balanceDaily.ActionStatus = true;
                investorBalanceDailyService.Update(balanceDaily);



                var param2 = new { @QType = "Payment", Coll_Pay_transfer_ID = Id, lcl_BusinessDate = SessionHelper.TransactionDate, CreateUser = SessionHelper.LoggedInUserId, lcl_OfficeID = SessionHelper.LoggedInOfficeId };
                spService.GetDataWithParameter(param2, "SP_Insert_Inv_Trans_Daily_Coll_Pay_Approve");


                return Json(result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result = ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }



        public class GLViewMode
        {
            public int Id { get; set; }
            public string BankName { get; set; }
            public string BranchName { get; set; }
            public string AccBankBranchGL { get; set; }
            public string BranchGLAccountNoMsg { get; set; }
            public int AccId { get; set; }
        }
        public JsonResult GetGLAccountNo()
        {
            try
            {
                List<GLViewMode> GLViewModeList = new List<GLViewMode>();
                var emList = spService.GetDataWithoutParameter("GetBranchGLAccountNo");

                GLViewModeList = emList.Tables[0].AsEnumerable()
                .Select(row => new GLViewMode
                {
                    BranchGLAccountNoMsg = row.Field<string>("BranchGLAccountNoMsg"),
                    AccBankBranchGL = row.Field<string>("GLBankBranch"),
                    AccId = row.Field<int>("AccId")
                }).ToList();
                return Json(GLViewModeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        public class AccPaymentViewMode
        {
            public int Id { get; set; }
            public decimal VoucherNo { get; set; }
            public int? InvestorId { get; set; }
            public string InvestorCode { get; set; }
            public string InvestorName { get; set; }
            public int TransactionModeId { get; set; }
            public string TransactionModeName { get; set; }
            public string ChequeNo { get; set; }
            public string ChequeDateMsg { get; set; }
            public int? BankId { get; set; }
            public int? BankBranchId { get; set; }
            public string BankName { get; set; }
            public string BankBranchName { get; set; }
            public decimal Amount { get; set; }
            public string TransactionDateMsg { get; set; }
            public string PostDateMsg { get; set; }
            public string DocRefNo { get; set; }
            public int? BrokerBranchId { get; set; }
            public string BrokerBranchName { get; set; }

            public bool IsAccountPayee { get; set; }
            public string AccountNo { get; set; }
            public string BranchGLAccount { get; set; }
            public string InvestorGLAccount { get; set; }
            public string Remarks { get; set; }
            public string ValuedateMsg { get; set; }
            public decimal CurrentBalance { get; set; }
        }

        public JsonResult GetPaymentInfoBeforeApprove()
        {
            try
            {

                var param = new { BrokerBranchId = SessionHelper.LoggedInOfficeId };
                var accPayList = spService.GetDataWithParameter(param, "GetPaymentInfoBeforeApprove");

                var PayList = accPayList.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     VoucherNo = row.Field<decimal>("VoucherNo"),
                     InvestorId = row.Field<int?>("InvestorId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                     TransactionModeId = row.Field<int>("TransactionModeId"),
                     TransactionModeName = row.Field<string>("TransactionModeName"),
                     ChequeNo = row.Field<string>("ChequeNo"),
                     ChequeDateMsg = row.Field<string>("ChequeDateMsg"),
                     BankId = row.Field<int?>("BankId"),
                     BankBranchId = row.Field<int?>("BankBranchId"),
                     BankName = row.Field<string>("BankName"),
                     BankBranchName = row.Field<string>("BankBranchName"),
                     Amount = row.Field<decimal>("Amount"),
                     TransactionDateMsg = row.Field<string>("TransactionDateMsg"),
                     PostDateMsg = row.Field<string>("PostDateMsg"),
                     DocRefNo = row.Field<string>("DocRefNo"),
                     BrokerBranchId = row.Field<int?>("BrokerBranchId"),
                     BrokerBranchName = row.Field<string>("BrokerBranchName"),
                     InvestorBrokerBranchName = row.Field<string>("InvestorBrokerBranchName"),
                     Remarks = row.Field<string>("Remarks"),//  //C.,C.,C.,C.IsAccountPayee 
                     ValuedateMsg = row.Field<string>("ValuedateMsg"),
                     AccountNo = row.Field<string>("AccountNo"),
                     AccId = row.Field<int>("AccId"),
                     BranchGLAccount = row.Field<string>("BranchGLAccount"),
                     InvestorGLAccount = row.Field<string>("InvestorGLAccount"),
                     IsAccountPayee = row.Field<bool>("IsAccountPayee"),
                     CurrentBalance = row.Field<decimal>("CurrentBalance")

                 }).ToList();
                return Json(PayList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPaymentInfoBeforClearance()
        {
            try
            {

                var param = new { BrokerBranchId = SessionHelper.LoggedInOfficeId };
                var accPayList = spService.GetDataWithParameter(param, "GetPaymentInfoBeforClearance");

                var PayList = accPayList.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     VoucherNo = row.Field<decimal>("VoucherNo"),
                     InvestorId = row.Field<int?>("InvestorId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                     TransactionModeId = row.Field<int>("TransactionModeId"),
                     TransactionModeName = row.Field<string>("TransactionModeName"),
                     ChequeNo = row.Field<string>("ChequeNo"),
                     ChequeDateMsg = row.Field<string>("ChequeDateMsg"),
                     BankId = row.Field<int?>("BankId"),
                     BankBranchId = row.Field<int?>("BankBranchId"),
                     BankName = row.Field<string>("BankName"),
                     BankBranchName = row.Field<string>("BankBranchName"),
                     Amount = row.Field<decimal>("Amount"),
                     TransactionDateMsg = row.Field<string>("TransactionDateMsg"),
                     PostDateMsg = row.Field<string>("PostDateMsg"),
                     DocRefNo = row.Field<string>("DocRefNo"),
                     BrokerBranchId = row.Field<int?>("BrokerBranchId"),
                     BrokerBranchName = row.Field<string>("BrokerBranchName"),
                     InvestorBrokerBranchName = row.Field<string>("InvestorBrokerBranchName"),
                     Remarks = row.Field<string>("Remarks"),//  //C.,C.,C.,C.IsAccountPayee 
                     ValuedateMsg = row.Field<string>("ValuedateMsg"),
                     AccountNo = row.Field<string>("AccountNo"),
                     AccId = row.Field<int>("AccId"),
                     BranchGLAccount = row.Field<string>("BranchGLAccount"),
                     InvestorGLAccount = row.Field<string>("InvestorGLAccount"),
                     IsAccountPayee = row.Field<bool>("IsAccountPayee"),
                     CurrentBalance = row.Field<decimal>("CurrentBalance")

                 }).ToList();
                return Json(PayList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPaymentInfoOfNegativeBalanceBeforeApprove()
        {
            try
            {

                var param = new { BrokerBranchId = SessionHelper.LoggedInOfficeId };
                var accPayList = spService.GetDataWithParameter(param, "GetPaymentInfo_Of_NegativeBalance_BeforeApprove");

                var PayList = accPayList.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     VoucherNo = row.Field<decimal>("VoucherNo"),
                     InvestorId = row.Field<int?>("InvestorId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                     TransactionModeId = row.Field<int>("TransactionModeId"),
                     TransactionModeName = row.Field<string>("TransactionModeName"),
                     ChequeNo = row.Field<string>("ChequeNo"),
                     ChequeDateMsg = row.Field<string>("ChequeDateMsg"),
                     BankId = row.Field<int?>("BankId"),
                     BankBranchId = row.Field<int?>("BankBranchId"),
                     BankName = row.Field<string>("BankName"),
                     BankBranchName = row.Field<string>("BankBranchName"),
                     Amount = row.Field<decimal>("Amount"),
                     TransactionDateMsg = row.Field<string>("TransactionDateMsg"),
                     PostDateMsg = row.Field<string>("PostDateMsg"),
                     DocRefNo = row.Field<string>("DocRefNo"),
                     BrokerBranchId = row.Field<int?>("BrokerBranchId"),
                     BrokerBranchName = row.Field<string>("BrokerBranchName"),
                     InvestorBrokerBranchName = row.Field<string>("InvestorBrokerBranchName"),
                     Remarks = row.Field<string>("Remarks"),//  //C.,C.,C.,C.IsAccountPayee 
                     ValuedateMsg = row.Field<string>("ValuedateMsg"),
                     AccountNo = row.Field<string>("AccountNo"),
                     AccId = row.Field<int>("AccId"),
                     BranchGLAccount = row.Field<string>("BranchGLAccount"),
                     InvestorGLAccount = row.Field<string>("InvestorGLAccount"),
                     IsAccountPayee = row.Field<bool>("IsAccountPayee"),
                     CurrentBalance = row.Field<decimal>("CurrentBalance")

                 }).ToList();
                return Json(PayList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMoneyReceiptList(string ReceiptType, string InvestorCode, string FromDate, string ToDate)
        {
            try
            {
                var param = new { ReceiptType = ReceiptType, InvestorCode = InvestorCode, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };
                var ReceiptList = spService.GetDataWithParameter(param, "GetCollectionPaymentMoneyReceiptList")
                .Tables[0].AsEnumerable()
                .Select(row => new
                {//RowSL,Id,VoucherNo,InvestorId, InvestorName,TransactionModeId,TransactionModeShortName,ChequeNo,CheckDate,BankId,BankName,BankBranchId,BranchName,Amount,TransactionDate,CreatedUserId,EmployeeName 

                    RowSL = row.Field<string>("RowSL"),
                    Id = row.Field<int>("Id"),
                    VoucherNo = row.Field<string>("VoucherNo"),
                    InvestorName = row.Field<string>("InvestorName"),
                    TransactionModeShortName = row.Field<string>("TransactionModeShortName"),
                    ChequeNo = row.Field<string>("ChequeNo"),
                    CheckDate = row.Field<string>("CheckDate"),
                    BankName = row.Field<string>("BankName"),
                    BranchName = row.Field<string>("BranchName"),
                    Amount = row.Field<decimal>("Amount"),
                    TransactionDate = row.Field<string>("TransactionDate"),
                    EmployeeName = row.Field<string>("EmployeeName"),


                }).ToList();
                var json = Json(ReceiptList, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void MoneyReceiptPrint(int Id, string ReceiptType)
        {
            var exportType = "pdf";
            if (ReceiptType == "Received")
            {
                var data =
               spService.GetDataWithParameter(
                   new
                   {
                       CollectionId = Id,
                   },
                   "GetCollectionMoneyReceipt").Tables[0];
                ReportHelper.ShowReport(data, exportType, "RptCollectionMoneyreceipt.rpt", "RptCollectionMoneyreceipt");
            }
            else
            {
                var data =
               spService.GetDataWithParameter(
                   new
                   {
                       PaymentId = Id,
                   },
                   "GetPaymentMoneyReceipt").Tables[0];
                ReportHelper.ShowReport(data, exportType, "RptPaymentMoneyreceipt.rpt", "RptPaymentMoneyreceipt");
            }

        }


        public JsonResult SaveAccPayment(string AccPaymentId, string InvestorId, string TransactionDate, string TransactionMode, string ChequeNo, string ChequeDate, string BankId, string BankBranchId, string Amount, string DocRefNo, string PaymentDate, string ValueDate, string Remarks, string AccountNo, string AccId, string AccountPayee, string PaymentGLAccount, int RequiditionId)
        {
            var Result = "";
            try
            {

                if (AccPaymentId == "0")  //Save
                {
                    var Voucher_No = decimal.Parse(spService.GetDataBySqlCommand("DECLARE @MAX VARCHAR(20)=''; SELECT @MAX=MAX(VoucherNo) FROM AccPayment; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString());
                    var acc = new AccPayment();

                    acc.InvestorId = Convert.ToInt32(InvestorId);
                    acc.VoucherNo = Voucher_No + 1;
                    acc.PostDate = Convert.ToDateTime(ReportHelper.FormatDateToString(PaymentDate));
                    acc.TransactionModeId = Convert.ToInt32(TransactionMode);
                    acc.ChequeNo = ChequeNo;
                    if (ChequeDate != null && ChequeDate != "")
                        acc.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(ChequeDate));
                    if (BankId != "0" && BankId != null && BankId != "")
                        acc.BankId = Convert.ToInt32(BankId);
                    if (BankBranchId != "0" && BankBranchId != null && BankBranchId != "")
                        acc.BankBranchId = Convert.ToInt32(BankBranchId);
                    acc.Amount = Convert.ToDecimal(Amount);
                    acc.DocRefNo = DocRefNo;
                    if (TransactionDate != null && TransactionDate != "")
                        acc.TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(TransactionDate));
                    if (ValueDate != null && ValueDate != "")
                        acc.Valuedate = Convert.ToDateTime(ReportHelper.FormatDateToString(ValueDate));
                    acc.Remarks = Remarks;
                    if (AccId != "")
                        acc.AccId = Convert.ToInt32(AccId);
                    acc.AccountNo = AccountNo;
                    acc.BranchGLAccount = PaymentGLAccount;
                    acc.IsAccountPayee = Convert.ToBoolean(AccountPayee);
                    acc.ApproveStatus = "N";
                    acc.GLProcessStatus = "N";
                    acc.PrintStatus = false;
                    acc.IsNegativeOrInsufficientBalanceWithdrawal = false;
                    acc.CreateDate = DateTime.Now;
                    acc.CreateBy = SessionHelper.LoggedInUserId;
                    acc.IsActive = true;
                    acc.RequisitionId = RequiditionId;
                    acc.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    acc.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                    accPaymentService.Create(acc);
                    //if (RequiditionId != 0)
                    //{
                    //    var Req = accPaymentRequisitionService.GetById(RequiditionId);
                    //    Req.IsPayment = true;
                    //    Req.UpdateUser = SessionHelper.LoggedInUserId;
                    //    Req.UpdateDate = DateTime.Now;
                    //    accPaymentRequisitionService.Update(Req);
                    //}
                }
                else
                {

                    var acc = accPaymentService.GetById(Convert.ToInt32(AccPaymentId));

                    acc.InvestorId = Convert.ToInt32(InvestorId);
                    acc.PostDate = Convert.ToDateTime(ReportHelper.FormatDateToString(PaymentDate));
                    acc.TransactionModeId = Convert.ToInt32(TransactionMode);
                    acc.ChequeNo = ChequeNo;
                    if (ChequeDate != null && ChequeDate != "" && ChequeDate != "null")
                        acc.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(ChequeDate));
                    if (BankId != null && BankId != "" && BankId != "0")
                        acc.BankId = Convert.ToInt32(BankId);
                    if (BankBranchId != null && BankBranchId != "" && BankBranchId != "0")
                        acc.BankBranchId = Convert.ToInt32(BankBranchId);
                    acc.Amount = Convert.ToDecimal(Amount);
                    acc.DocRefNo = DocRefNo;
                    if (TransactionDate != null && TransactionDate != "" && ChequeDate != "null")
                        acc.TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(TransactionDate));
                    if (ValueDate != null && ValueDate != "" && ChequeDate != "null")
                        acc.Valuedate = Convert.ToDateTime(ReportHelper.FormatDateToString(ValueDate));
                    acc.Remarks = Remarks;
                    acc.AccountNo = AccountNo;
                    if (AccId != "")
                        acc.AccId = Convert.ToInt32(AccId);
                    acc.BranchGLAccount = PaymentGLAccount;
                    acc.IsAccountPayee = Convert.ToBoolean(AccountPayee);
                    acc.ApproveStatus = "N";
                    acc.GLProcessStatus = "N";
                    acc.PrintStatus = false;
                    acc.IsNegativeOrInsufficientBalanceWithdrawal = false;
                    acc.CreateDate = DateTime.Now;
                    acc.CreateBy = SessionHelper.LoggedInUserId;
                    acc.IsActive = true;
                    acc.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                    accPaymentService.Update(acc);
                }


                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveAccPaymentOfNegativeBalance(string AccPaymentId, string InvestorId, string TransactionDate, string TransactionMode, string ChequeNo, string ChequeDate, string BankId, string BankBranchId, string Amount, string DocRefNo, string PaymentDate, string ValueDate, string Remarks, string AccountNo, string AccId, string AccountPayee, string PaymentGLAccount, int RequiditionId)
        {
            var Result = "";
            try
            {

                if (AccPaymentId == "0")  //Save
                {
                    var Voucher_No = decimal.Parse(spService.GetDataBySqlCommand("DECLARE @MAX VARCHAR(20)=''; SELECT @MAX=MAX(VoucherNo) FROM AccPayment; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString());
                    var acc = new AccPayment();

                    acc.InvestorId = Convert.ToInt32(InvestorId);
                    acc.VoucherNo = Voucher_No + 1;
                    acc.PostDate = Convert.ToDateTime(ReportHelper.FormatDateToString(PaymentDate));
                    acc.TransactionModeId = Convert.ToInt32(TransactionMode);
                    acc.ChequeNo = ChequeNo;
                    if (ChequeDate != null && ChequeDate != "")
                        acc.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(ChequeDate));
                    if (BankId != "0" && BankId != null && BankId != "")
                        acc.BankId = Convert.ToInt32(BankId);
                    if (BankBranchId != "0" && BankBranchId != null && BankBranchId != "")
                        acc.BankBranchId = Convert.ToInt32(BankBranchId);
                    acc.Amount = Convert.ToDecimal(Amount);
                    acc.DocRefNo = DocRefNo;
                    if (TransactionDate != null && TransactionDate != "")
                        acc.TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(TransactionDate));
                    if (ValueDate != null && ValueDate != "")
                        acc.Valuedate = Convert.ToDateTime(ReportHelper.FormatDateToString(ValueDate));
                    acc.Remarks = Remarks;
                    acc.AccountNo = AccountNo;
                    if (AccId != "")
                        acc.AccId = Convert.ToInt32(AccId);
                    acc.BranchGLAccount = PaymentGLAccount;
                    acc.IsAccountPayee = Convert.ToBoolean(AccountPayee);
                    acc.ApproveStatus = "N";
                    acc.GLProcessStatus = "N";
                    var currBalance = investorBalanceDailyService.GetAll().Where(s => s.IsActive == true && s.InvestorId == Convert.ToInt32(InvestorId)).FirstOrDefault().CurrentBalance;

                    if (currBalance < 1) // Negative balance
                    {
                        acc.IsNegativeOrInsufficientBalanceWithdrawal = true;
                    }
                    else if (currBalance < Convert.ToDecimal(Amount))  // balance will be negative after payment
                    {
                        acc.IsNegativeOrInsufficientBalanceWithdrawal = true;
                    }
                    else // balance 1+ after payment
                    {
                        acc.IsNegativeOrInsufficientBalanceWithdrawal = false;
                    }

                    acc.PrintStatus = false;
                    acc.CreateDate = DateTime.Now;
                    acc.CreateBy = SessionHelper.LoggedInUserId;
                    acc.IsActive = true;
                    acc.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    acc.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);

                    accPaymentService.Create(acc);
                    if (RequiditionId != 0)
                    {
                        var Req = accPaymentRequisitionService.GetById(RequiditionId);
                        Req.IsPayment = true;
                        Req.UpdateUser = SessionHelper.LoggedInUserId;
                        Req.UpdateDate = DateTime.Now;
                        accPaymentRequisitionService.Update(Req);
                    }
                }
                else
                {

                    var acc = accPaymentService.GetById(Convert.ToInt32(AccPaymentId));

                    acc.InvestorId = Convert.ToInt32(InvestorId);
                    acc.PostDate = Convert.ToDateTime(ReportHelper.FormatDateToString(PaymentDate));
                    acc.TransactionModeId = Convert.ToInt32(TransactionMode);
                    acc.ChequeNo = ChequeNo;
                    if (ChequeDate != null && ChequeDate != "" && ChequeDate != "null")
                        acc.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(ChequeDate));
                    if (BankId != null && BankId != "" && BankId != "0")
                        acc.BankId = Convert.ToInt32(BankId);
                    if (BankBranchId != null && BankBranchId != "" && BankBranchId != "0")
                        acc.BankBranchId = Convert.ToInt32(BankBranchId);
                    acc.Amount = Convert.ToDecimal(Amount);
                    acc.DocRefNo = DocRefNo;
                    if (TransactionDate != null && TransactionDate != "" && ChequeDate != "null")
                        acc.TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(TransactionDate));
                    if (ValueDate != null && ValueDate != "" && ChequeDate != "null")
                        acc.Valuedate = Convert.ToDateTime(ReportHelper.FormatDateToString(ValueDate));
                    acc.Remarks = Remarks;
                    acc.AccountNo = AccountNo;
                    if (AccId != "")
                        acc.AccId = Convert.ToInt32(AccId);
                    acc.BranchGLAccount = PaymentGLAccount;
                    acc.IsAccountPayee = Convert.ToBoolean(AccountPayee);
                    acc.ApproveStatus = "N";
                    acc.GLProcessStatus = "N";
                    acc.PrintStatus = false;
                    var currBalance = investorBalanceDailyService.GetAll().Where(s => s.IsActive == true && s.InvestorId == Convert.ToInt32(InvestorId)).FirstOrDefault().CurrentBalance;

                    if (currBalance < 1) // Negative balance
                    {
                        acc.IsNegativeOrInsufficientBalanceWithdrawal = true;
                    }
                    else if (currBalance < Convert.ToDecimal(Amount))  // balance will be negative after payment
                    {
                        acc.IsNegativeOrInsufficientBalanceWithdrawal = true;
                    }
                    else // balance 1+ after payment
                    {
                        acc.IsNegativeOrInsufficientBalanceWithdrawal = false;
                    }
                    acc.CreateDate = DateTime.Now;
                    acc.CreateBy = SessionHelper.LoggedInUserId;
                    acc.IsActive = true;
                    acc.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);

                    accPaymentService.Update(acc);
                }


                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }









        #endregion

        #region Action
        public ActionResult Index()
        {
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().Where(x => x.TransactionModeShortName == "CH").ToList();
            //ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            ViewData["PaymentGLAccountList"] = items;
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            return View();
        }

        public ActionResult IndexEx()
        {
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().Where(x => x.TransactionModeShortName == "CH").ToList();
            // ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            ViewData["PaymentGLAccountList"] = items;
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            return View();
        }


        public ActionResult MoneyReceipt()
        {

            return View();
        }

        public ActionResult Requisition()
        {
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            return View();
        }

        public ActionResult ReqApprove()
        {
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            return View();
        }
        public ActionResult Approve()
        {

            return View();
        }
        public ActionResult PaymentClearance()
        {

            return View();
        }
        #endregion

    }
}
