using System;
using System.Collections.Generic;
using System.Data;
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
    public class AccCollectionController : BaseController
    {
        #region Variables

        private readonly IAccCollectionService accCollectionService;
        private readonly IAccPaymentService accPaymentService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly ISPService spService;
        private readonly ILookupBankService lookupBankService;
        private readonly IAccBankBranchGLService accBankBranchGLService;
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccTransactionModeService accTransactionModeService;
        private readonly IAccReceiptPaymentMappingService accReceiptPaymentMappingService;
        private readonly IInvestorBalanceDailyService investorBalanceDailyService;
        private readonly ICheckDishonourCauseService checkDishonourCauseService;
        private readonly IInvestorCheckDishonourInfoService investorCheckDishonourInfoService;
        private readonly IIPODeclarationService iPODeclarationService;
        private readonly ICompanyInfoService companyInfoService;
        private readonly IAccChartService accChartService;
        public AccCollectionController(IAccCollectionService accCollectionService, IAccPaymentService accPaymentService, IBrokerBranchService brokerBranchService, ISPService spService, ILookupBankService lookupBankService, IAccBankBranchGLService accBankBranchGLService,
            IAccTrxMasterService accTrxMasterService, IAccTrxDetailService accTrxDetailService, IAccTransactionModeService accTransactionModeService, IAccReceiptPaymentMappingService accReceiptPaymentMappingService, IInvestorBalanceDailyService investorBalanceDailyService,
            ICheckDishonourCauseService checkDishonourCauseService, IInvestorCheckDishonourInfoService investorCheckDishonourInfoService, IIPODeclarationService iPODeclarationService, ICompanyInfoService companyInfoService, IAccChartService accChartService)
        {
            this.accCollectionService = accCollectionService;
            this.accPaymentService = accPaymentService;
            this.brokerBranchService = brokerBranchService;
            this.spService = spService;
            this.lookupBankService = lookupBankService;
            this.accBankBranchGLService = accBankBranchGLService;
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accTransactionModeService = accTransactionModeService;
            this.accReceiptPaymentMappingService = accReceiptPaymentMappingService;
            this.investorBalanceDailyService = investorBalanceDailyService;
            this.checkDishonourCauseService = checkDishonourCauseService;
            this.investorCheckDishonourInfoService = investorCheckDishonourInfoService;
            this.iPODeclarationService = iPODeclarationService;
            this.companyInfoService = companyInfoService;
            this.accChartService = accChartService;
        }
        #endregion

        #region Methods


        public JsonResult GetBankBranchByRoutingNo(string RouNo)
        {
            try
            {
                var BankBranch = spService.GetDataBySqlCommand("SELECT TOP 1 B.Id AS BankBranchId,B.BankId FROM LookupBankBranch AS B WHERE B.IsActive = 1 AND B.RoutingNo = '0'+'" + RouNo + "' OR B.RoutingNo = '" + RouNo + "'").Tables[0];
                if (BankBranch.Rows.Count != 0)
               {
                   return Json(new { Result = "Success", Message = "", BankBranchId = BankBranch.Rows[0][0], BankId = BankBranch.Rows[0][1] }, JsonRequestBehavior.AllowGet);
               }
                else
                {
                    return Json(new { Result = "NoFound", Message = "Invalid routing no", BankBranchId = 0, BankId = 0 }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch(Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, BankBranchId = 0, BankId = 0 },JsonRequestBehavior.AllowGet);
            }
        }
        public void MoneyReceiptPrint(int CollectionId)
        {
            var exportType = "pdf";
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        CollectionId = CollectionId,
                    },
                    "GetCollectionMoneyReceipt").Tables[0];
            ReportHelper.ShowReport(data, exportType, "RptCollectionMoneyreceipt.rpt", "RptCollectionMoneyreceipt");

        }

        public class IpoCompanyModel
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
        }

        public JsonResult GetIPOCompanyList()
        {
            var Result = string.Empty;
            try
            {
                var ComanyList = new List<IpoCompanyModel>();
                var IpoCompany = iPODeclarationService.GetAll().Where(x => x.ClosingStatus == false);
                if (IpoCompany.Count() != 0)
                {
                    foreach (var app in IpoCompany)
                    {
                        var Id = app.Id;
                        var CompanyName = companyInfoService.GetById(app.CompanyId).CompanyName;

                        var Comp = new IpoCompanyModel() { Id = Id, CompanyName = CompanyName };
                        ComanyList.Add(Comp);
                    }
                    return Json(ComanyList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Comp = new IpoCompanyModel() { Id = 0, CompanyName = "" };
                    ComanyList.Add(Comp);
                    return Json(ComanyList, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveDepositToBank(List<int> AllCheckIds, string BankId, string BranchId, string GLAccount, string SlipNo)
        {
            var result = "";
            try
            {
                if (AllCheckIds != null)
                {

                    foreach (var app in AllCheckIds)
                    {
                        var acc = accCollectionService.GetById(Convert.ToInt32(app));

                        acc.DepositStatus = "Y";
                        acc.DepositBankId = Convert.ToInt32(BankId);
                        acc.DepositBankBranchId = Convert.ToInt32(BranchId);
                        acc.AccId = Convert.ToInt32(GLAccount);
                        var GlAcc = accBankBranchGLService.GetAll().Where(x => x.AccId == Convert.ToInt32(GLAccount)).FirstOrDefault();

                        acc.AccountNo = GlAcc.AccountNo;
                        acc.BranchGLAccount = GlAcc.BranchGLAccountNo;

                        acc.DepositSlipNo = SlipNo;
                        acc.DepositDate = SessionHelper.TransactionDate;
                        acc.ClearanceStatus = null;
                        acc.ChequeStatusId = 3;
                        acc.DepositBy = SessionHelper.LoggedInUserId;
                        acc.UpdateUserId = SessionHelper.LoggedInUserId;
                        acc.UpdateDate = DateTime.Now;
                        if (acc.TransactionModeId == 2)
                        {
                            acc.ClearanceStatus = true;
                            acc.ClearanceDate = SessionHelper.TransactionDate;
                            acc.ClearDisHonourBy = SessionHelper.LoggedInUserId;
                        }

                        accCollectionService.Update(acc);



                        //Investor Balance Daily
                        if (acc.TransactionModeId == 2) //Cash
                        {
                            var IvnbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == acc.InvestorId);//.FirstOrDefault();
                            var balanceDaily = IvnbalanceDaily.FirstOrDefault();
                            //if (IvnbalanceDaily.Count() == 0)
                            //{
                            //    var param = new { InvestorId = acc.InvestorId, TransactionDate = SessionHelper.BusinessDate, CreatedUserId = SessionHelper.LoggedInUserId };
                            //    spService.GetDataWithParameter(param, "Insert_InvestorBalanceDaily");
                            //}

                            balanceDaily.ClearingChequeBalance = balanceDaily.ClearingChequeBalance - acc.Amount;
                            balanceDaily.UpdateDate = DateTime.Now;
                            balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;
                            balanceDaily.ActionStatus = true;
                            investorBalanceDailyService.Update(balanceDaily);

                        }

                        // Accounts


                        var acco = new AccTrxMaster();

                        acco.OfficeID = SessionHelper.LoggedInOfficeId;
                        acco.VoucherYear = DateTime.Now.Year;
                        acco.TrxDate = SessionHelper.TransactionDate;
                        acco.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                        acco.VoucherType = "Dr";
                        acco.VoucherTypeId = 1;
                        acco.IsPosted = false;
                        acco.IsYearlyClosing = false;
                        acco.IsAutoVoucher = true;
                        acco.IsRectify = false;
                        acco.IsActive = true;
                        acco.CreateDate = DateTime.Now;
                        acco.CreateUser = SessionHelper.LoggedInUserId;

                        var accMasterId = accTrxMasterService.Create(acco).TrxMasterID;

                        //var AccIdDr = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == acc.TransactionModeId && x.FormEntryType == false).FirstOrDefault().AccIdDr;
                        var AccIdCrd = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == acc.TransactionModeId && x.FormEntryType == false).FirstOrDefault();

                        var AccIdCr = AccIdCrd.AccIdCr;
                        var AccCrCode = AccIdCrd.AccCodeCr;

                        //Credit

                        var accDe = new AccTrxDetail();

                        accDe.TrxMasterID = accMasterId;
                        //accDe.MarketId = 0;//Session
                        //accDe.CompanyId = 0;//Session
                        accDe.AccID = AccIdCr; //Accid;
                        accDe.AccCode = AccCrCode;
                        accDe.Debit = 0;
                        accDe.Credit = acc.Amount;
                        accDe.InvestorId = acc.InvestorId;
                        accDe.TransactionTypeId = 38;
                        accDe.CreateDate = DateTime.Now;
                        accDe.IsActive = true;
                        accDe.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + acc.InvestorId + ")").Tables[0].Rows[0][0]);
                        accDe.CreateUser = SessionHelper.LoggedInUserId;
                        accTrxDetailService.Create(accDe);

                        //Debit

                        var accDetails = new AccTrxDetail();
                        accDetails.TrxMasterID = accMasterId;
                        //accDetails.MarketId = 0;//Session
                        //accDetails.CompanyId = 0;//Session
                        accDetails.AccID = Convert.ToInt32(GLAccount);
                        accDetails.AccCode = accChartService.GetById(Convert.ToInt32(GLAccount)).AccCode;
                        accDetails.Debit = acc.Amount;
                        accDetails.Credit = 0;
                        accDetails.InvestorId = acc.InvestorId;
                        accDetails.TransactionTypeId = 38;
                        accDetails.CreateDate = DateTime.Now;
                        accDetails.IsActive = true;
                        accDetails.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + acc.InvestorId + ")").Tables[0].Rows[0][0]);
                        accDetails.CreateUser = SessionHelper.LoggedInUserId;
                        accTrxDetailService.Create(accDetails);



                    }

                    return Json(result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result = "2", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(result = ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCheckDishonourCauseList()
        {
            try
            {
                var CauseList = checkDishonourCauseService.GetAll().Where(x => x.IsActive == true);


                return Json(CauseList.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RejectCollection(string Id)
        {
            var collec = accCollectionService.GetById(Convert.ToInt32(Id));
            collec.ApproveStatus = "R";
            collec.ChequeStatusId = 6;
            collec.ApproveDate = SessionHelper.TransactionDate;
            collec.ApproveBy = SessionHelper.LoggedInUserId;
            accCollectionService.Update(collec);

            return Json(new { result = "1" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DishonourCollection(string Id, string DishonourCauseId)
        {
            var result = string.Empty;
            try
            {
                if (Id != null && Id != "")
                {
                    var collec = accCollectionService.GetById(Convert.ToInt32(Id));
                    collec.ClearanceStatus = false;
                    collec.ChequeStatusId = 5;
                    collec.ClearanceDate = SessionHelper.TransactionDate;
                    collec.ClearDisHonourBy = SessionHelper.LoggedInUserId;
                    collec.UpdateUserId = SessionHelper.LoggedInUserId;
                    accCollectionService.Update(collec);


                    //Cause
                    // 
                    var cause = new InvestorCheckDishonourInformation();

                    cause.AccCollectionId = Convert.ToInt32(Id);
                    cause.DishonourCauseId = Convert.ToInt32(DishonourCauseId);
                    cause.IsRedeposit = false;
                    cause.IsActive = true;
                    cause.CreateDate = DateTime.Now;
                    cause.CreatedUserId = LoggedInUserId;
                    investorCheckDishonourInfoService.Create(cause);

                    ////// Payment


                    var acc = new AccPayment();
                    var Voucher_No = decimal.Parse(spService.GetDataBySqlCommand("DECLARE @MAX VARCHAR(20)=''; SELECT @MAX=MAX(VoucherNo) FROM AccPayment; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString());
                    acc.InvestorId = Convert.ToInt32(collec.InvestorId);
                    acc.VoucherNo = Voucher_No + 1;
                    acc.PostDate = SessionHelper.TransactionDate;
                    acc.TransactionModeId = Convert.ToInt32(collec.TransactionModeId);
                    acc.ChequeNo = collec.ChequeNo;

                    acc.ChequeDate = collec.CheckDate;

                    acc.BankId = collec.DepositBankId;

                    acc.BankBranchId = collec.DepositBankBranchId;
                    acc.Amount = collec.Amount;
                    acc.DocRefNo = collec.DocRefNo;

                    acc.TransactionDate = SessionHelper.TransactionDate;
                    acc.Valuedate = collec.Valuedate; ;
                    acc.Remarks = collec.Remarks + ".[Dishonour Collection]";
                    acc.AccId = collec.AccId;
                    acc.AccountNo = collec.AccountNo;
                    acc.BranchGLAccount = collec.BranchGLAccount;
                    acc.IsAccountPayee = true;
                    acc.ApproveStatus = "Y";
                    acc.ApproveDate = SessionHelper.TransactionDate;
                    acc.GLProcessStatus = "Y";
                    acc.PrintStatus = false;
                    acc.IsNegativeOrInsufficientBalanceWithdrawal = false;
                    acc.CreateDate = DateTime.Now;
                    acc.CreateBy = SessionHelper.LoggedInUserId;
                    acc.IsActive = true;
                    acc.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    acc.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    acc.ReferenceAccCollectionId = collec.Id;
                    acc.AutoEntryForCheckDishonour = true;
                    var PaymentId = accPaymentService.Create(acc).Id;


                    /////
                    //Accounts

                    var acco = new AccTrxMaster();

                    acco.OfficeID = SessionHelper.LoggedInOfficeId;
                    acco.VoucherYear = DateTime.Now.Year;
                    acco.TrxDate = SessionHelper.TransactionDate;
                    acco.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                    acco.VoucherType = "Cr";
                    acco.VoucherTypeId = 2;
                    acco.IsPosted = false;
                    acco.IsYearlyClosing = false;
                    acco.IsAutoVoucher = true;
                    acco.IsRectify = false;
                    acco.IsActive = true;
                    acco.CreateDate = DateTime.Now;
                    acco.CreateUser = SessionHelper.LoggedInUserId;

                    var accMasterId = accTrxMasterService.Create(acco).TrxMasterID;



                    var AccIdDrd = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 39 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == true).FirstOrDefault();
                    var AccIdCrd = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 39 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == true).FirstOrDefault();

                    var AccIdDr = AccIdDrd.AccIdDr;
                    var AccDrCode = AccIdDrd.AccCodeDr;

                    var AccIdCr = AccIdCrd.AccIdCr;
                    var AccCrCode = AccIdCrd.AccCodeCr;


                    //Credit

                    var accDe = new AccTrxDetail();

                    accDe.TrxMasterID = accMasterId;
                    // accDe.MarketId = 0;//Session
                    // accDe.CompanyId = 0;//Session
                    accDe.AccID = AccIdCr; //Accid;
                    accDe.AccCode = AccCrCode;
                    accDe.Debit = 0;
                    accDe.Credit = collec.Amount;
                    accDe.InvestorId = collec.InvestorId;
                    accDe.TransactionTypeId = 39; //Payment
                    accDe.CreateDate = DateTime.Now;
                    accDe.IsActive = true;
                    accDe.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    accDe.CreateUser = SessionHelper.LoggedInUserId;
                    accTrxDetailService.Create(accDe);

                    //Debit

                    var accDetails = new AccTrxDetail();
                    accDetails.TrxMasterID = accMasterId;
                    //accDetails.MarketId = 0;//Session
                    // accDetails.CompanyId = 0;//Session
                    accDetails.AccID = AccIdDr;//Convert.ToInt32(GLAccount); ;
                    accDetails.AccCode = AccDrCode;
                    accDetails.Debit = collec.Amount;
                    accDetails.Credit = 0;
                    accDetails.InvestorId = collec.InvestorId;
                    accDetails.TransactionTypeId = 39; //Payment
                    accDetails.CreateDate = DateTime.Now;
                    accDetails.IsActive = true;
                    accDetails.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    accDetails.CreateUser = SessionHelper.LoggedInUserId;
                    accTrxDetailService.Create(accDetails);




                    // Investor Balance Daily **************

                    var IvnbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == collec.InvestorId);
                    var balanceDaily = IvnbalanceDaily.FirstOrDefault();
                    //if (IvnbalanceDaily.Count() == 0)
                    //{
                    //    var param = new { InvestorId = collec.InvestorId, TransactionDate = SessionHelper.BusinessDate, CreatedUserId = SessionHelper.LoggedInUserId };
                    //    spService.GetDataWithParameter(param, "Insert_InvestorBalanceDaily");
                    //}
                    balanceDaily.CurrentBalance = balanceDaily.CurrentBalance - collec.Amount;
                    balanceDaily.ClearingChequeBalance = balanceDaily.ClearingChequeBalance - collec.Amount;
                    balanceDaily.UpdateDate = DateTime.Now;
                    balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;

                    balanceDaily.ActionStatus = true;
                    investorBalanceDailyService.Update(balanceDaily);

                    var param2 = new { @QType = "Payment", Coll_Pay_transfer_ID = PaymentId.ToString(), lcl_BusinessDate = SessionHelper.TransactionDate, CreateUser = SessionHelper.LoggedInUserId, lcl_OfficeID = SessionHelper.LoggedInOfficeId };
                    //spService.GetDataWithParameter(param2, "SP_Insert_Inv_Trans_Daily_Coll_Pay_Approve");
                    spService.GetDataWithParameter(param2, "SP_Insert_Inv_Trans_Daily_Coll_Pay_Approve_forDishonour");


                    return Json(result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result = "2", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ClearanceCollection(int Id)
        {
            var result = string.Empty;
            try
            {
                if (Id != 0)
                {

                    var collec = accCollectionService.GetById(Convert.ToInt32(Id));
                    collec.ClearanceStatus = true;
                    collec.ChequeStatusId = 4;
                    collec.ClearanceDate = SessionHelper.TransactionDate;
                    collec.ClearDisHonourBy = SessionHelper.LoggedInUserId;
                    collec.UpdateUserId = SessionHelper.LoggedInUserId;
                    accCollectionService.Update(collec);

                    //Investor Balance Daily

                    var IvnbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == collec.InvestorId);

                    var balanceDaily = IvnbalanceDaily.FirstOrDefault();
                    //if (IvnbalanceDaily.Count() == 0)
                    //{
                    //    var param = new { InvestorId = collec.InvestorId, TransactionDate = SessionHelper.BusinessDate, CreatedUserId = SessionHelper.LoggedInUserId };
                    //    spService.GetDataWithParameter(param, "Insert_InvestorBalanceDaily");
                    //}

                    //balanceDaily.CurrentBalance = balanceDaily.CurrentBalance + collec.Amount;
                    balanceDaily.ClearingChequeBalance = balanceDaily.ClearingChequeBalance - collec.Amount;
                    balanceDaily.UpdateDate = DateTime.Now;
                    balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;
                    balanceDaily.ActionStatus = true;
                    investorBalanceDailyService.Update(balanceDaily);
                    


                    return Json(result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result = "2", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(result = ex.Message, JsonRequestBehavior.AllowGet);
            }

        } //not use


        public JsonResult ReDepositCollection(string Id, string AccCollectionId)
        {
            var result = string.Empty;
            try
            {
                if (Id != null && Id != "")
                {

                    // Update  InvestorCheckDishonourInfo
                    var cause = investorCheckDishonourInfoService.GetById(Convert.ToInt32(Id));

                    cause.IsRedeposit = true;
                    cause.IsActive = true;
                    cause.UpdateDate = DateTime.Now;
                    cause.UpdateUserId = LoggedInUserId;
                    investorCheckDishonourInfoService.Update(cause);


                    // Update  AccCollection

                    var collec = accCollectionService.GetById(Convert.ToInt32(AccCollectionId));
                    collec.ClearanceStatus = false;
                    collec.ChequeStatusId = 6;
                    collec.ClearDisHonourBy = SessionHelper.LoggedInUserId;
                    collec.CreateDate = DateTime.Now;
                    collec.CreatedUserId = SessionHelper.LoggedInUserId;
                    accCollectionService.Update(collec);

                    //re create Acccollection


                    var accCo = new AccCollection();
                    var Voucher_No = decimal.Parse(spService.GetDataBySqlCommand("DECLARE @MAX VARCHAR(20)=''; SELECT @MAX=MAX(VoucherNo) FROM AccCollection; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString());
                    accCo.InvestorId = collec.InvestorId;
                    accCo.VoucherNo = Voucher_No + 1;
                    accCo.VoucherYear = DateTime.Now.Year;
                    accCo.PostDate = SessionHelper.TransactionDate;
                    accCo.TransactionModeId = collec.TransactionModeId;
                    accCo.ChequeNo = collec.ChequeNo;

                    accCo.ChequeDate = collec.ChequeDate;
                    accCo.AccId = collec.AccId;
                    accCo.AccountNo = collec.AccountNo;
                    accCo.BranchGLAccount = collec.BranchGLAccount;
                    accCo.BankId = collec.BankId;

                    accCo.BankBranchId = collec.BankBranchId;
                    accCo.Amount = collec.Amount;

                    accCo.TransactionDate = SessionHelper.TransactionDate;

                    accCo.Valuedate = collec.Valuedate;
                    accCo.Remarks = collec.Remarks;
                    accCo.ApproveStatus = "Y";
                    accCo.ApproveDate = SessionHelper.TransactionDate;
                    accCo.ApproveBy = SessionHelper.LoggedInUserId;
                    accCo.DepositStatus = "Y";
                    accCo.DepositBy = SessionHelper.LoggedInUserId;
                    accCo.DepositDate = SessionHelper.TransactionDate;

                    //acc.IPODeclarationId = Convert.ToInt32(IPODeclarationId);
                    accCo.ChequeStatusId = 6;
                    accCo.CreateDate = DateTime.Now;
                    accCo.CreatedUserId = SessionHelper.LoggedInUserId;
                    accCo.IsActive = true;
                    accCo.IsClosedInvestor = false;
                    accCo.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    accCo.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]); //Convert.ToInt32(spService.GetDataBySqlCommand("SELECT A.BrokerBranchId FROM InvestorAccount AS A WHERE A.InvestorId = " + Convert.ToInt32(InvestorId) + "").Tables[0].Rows[0][0]);
                    accCo.ReferenceAccCollectionId = collec.Id;
                    accCollectionService.Create(accCo);






                    //    Save Acc Master  //////////////// this code used when approve 

                    var acc = new AccTrxMaster();

                    acc.VoucherYear = DateTime.Now.Year;
                    acc.OfficeID = SessionHelper.LoggedInOfficeId;
                    acc.TrxDate = SessionHelper.TransactionDate;
                    acc.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                    acc.VoucherType = "Dr";
                    acc.VoucherTypeId = 1;
                    acc.IsPosted = false;
                    acc.IsYearlyClosing = false;
                    acc.IsAutoVoucher = true;
                    acc.IsRectify = false;
                    acc.IsActive = true;
                    acc.CreateDate = DateTime.Now;
                    acc.CreateUser = SessionHelper.LoggedInUserId;

                    var accMasterId = accTrxMasterService.Create(acc).TrxMasterID;

                    var AccIdDrd = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == true).FirstOrDefault();
                    var AccIdCrd = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == true).FirstOrDefault();


                    var AccIdDr = AccIdDrd.AccIdDr;
                    var AccDrCode = AccIdDrd.AccCodeDr;

                    var AccIdCr = AccIdCrd.AccIdCr;
                    var AccCrCode = AccIdCrd.AccCodeCr;

                    ////     //Debit


                    //  AccId = 38 For receive

                    var accDetails = new AccTrxDetail();
                    accDetails.TrxMasterID = accMasterId;
                    //accDetails.MarketId = 0;//Session
                    //accDetails.CompanyId = 0;//Session
                    accDetails.AccID = AccIdDr;
                    accDetails.AccCode = AccDrCode;
                    accDetails.Debit = collec.Amount;
                    accDetails.Credit = 0;
                    accDetails.InvestorId = collec.InvestorId;
                    accDetails.TransactionTypeId = 38;
                    accDetails.CreateDate = DateTime.Now;
                    accDetails.IsActive = true;
                    accDetails.CreateUser = SessionHelper.LoggedInUserId;
                    accDetails.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    accTrxDetailService.Create(accDetails);


                    //Credit

                    var accDe = new AccTrxDetail();

                    accDe.TrxMasterID = accMasterId;
                    // accDe.MarketId = 0;//Session
                    //accDe.CompanyId = 0;//Session
                    accDe.AccID = AccIdCr;
                    accDe.AccCode = AccCrCode;
                    accDe.Debit = 0;
                    accDe.Credit = collec.Amount;
                    accDe.InvestorId = collec.InvestorId;
                    accDe.TransactionTypeId = 38;
                    accDe.CreateDate = DateTime.Now;
                    accDe.IsActive = true;
                    accDe.CreateUser = SessionHelper.LoggedInUserId;
                    accDe.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    accTrxDetailService.Create(accDe);



                    ////////////////// this code used when deposit to bank 


                    var acco = new AccTrxMaster();

                    acco.OfficeID = SessionHelper.LoggedInOfficeId;
                    acco.VoucherYear = DateTime.Now.Year;
                    acco.TrxDate = SessionHelper.TransactionDate;
                    acco.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                    acco.VoucherType = "Dr";
                    acco.VoucherTypeId = 1;
                    acco.IsPosted = false;
                    acco.IsYearlyClosing = false;
                    acco.IsAutoVoucher = true;
                    acco.VoucherDesc = "Anything";
                    acco.IsRectify = false;
                    acco.IsActive = true;
                    acco.CreateDate = DateTime.Now;
                    acco.CreateUser = SessionHelper.LoggedInUserId;

                    var accoMasterId = accTrxMasterService.Create(acco).TrxMasterID;

                    //var AccIdDr = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == acc.TransactionModeId && x.FormEntryType == false).FirstOrDefault().AccIdDr;
                    var AccoIdCrd = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == false).FirstOrDefault();

                    var AccoIdCr = AccoIdCrd.AccIdCr;
                    var AccoDrCode = AccoIdCrd.AccCodeCr;
                    //Credit

                    var accDet = new AccTrxDetail();

                    accDet.TrxMasterID = accoMasterId;
                    // accDet.MarketId = 0;//Session
                    //accDet.CompanyId = 0;//Session
                    accDet.AccID = AccoIdCr; //Accid;
                    accDet.AccCode = AccoDrCode;
                    accDet.Debit = 0;
                    accDet.Credit = collec.Amount;
                    accDet.InvestorId = collec.InvestorId;
                    accDet.TransactionTypeId = 38;
                    accDet.CreateDate = DateTime.Now;
                    accDet.IsActive = true;
                    accDet.CreateUser = SessionHelper.LoggedInUserId;
                    accDet.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    accTrxDetailService.Create(accDet);

                    //Debit

                    var accDetail = new AccTrxDetail();
                    accDetail.TrxMasterID = accoMasterId;
                    //accDetail.MarketId = 0;//Session
                    //accDetail.CompanyId = 0;//Session
                    if (collec.AccId != null)
                        accDetail.AccID = Convert.ToInt32(collec.AccId);// GL Account No
                    accDetail.AccCode = accChartService.GetById(Convert.ToInt32(collec.AccId)).AccCode; ;
                    accDetail.Debit = collec.Amount;
                    accDetail.Credit = 0;
                    accDetail.InvestorId = collec.InvestorId;
                    accDetail.TransactionTypeId = 38;
                    accDetail.CreateDate = DateTime.Now;
                    accDetail.IsActive = true;
                    accDetail.CreateUser = SessionHelper.LoggedInUserId;
                    accDetail.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                    accTrxDetailService.Create(accDetail);


                    // Investor Balance Daily **************

                    var IvnbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == collec.InvestorId);

                    var balanceDaily = IvnbalanceDaily.FirstOrDefault();
                    //if (IvnbalanceDaily.Count() == 0)
                    //{
                    //    var param = new { InvestorId = collec.InvestorId, TransactionDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), CreatedUserId = SessionHelper.LoggedInUserId };
                    //    spService.GetDataWithParameter(param, "Insert_InvestorBalanceDaily");
                    //}



                    balanceDaily.CurrentBalance = balanceDaily.CurrentBalance + collec.Amount;
                    balanceDaily.ClearingChequeBalance = balanceDaily.ClearingChequeBalance + collec.Amount; //04 Oct 2017
                    balanceDaily.UpdateDate = DateTime.Now;
                    balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;

                    balanceDaily.ActionStatus = true;
                    investorBalanceDailyService.Update(balanceDaily);


                    //Insert Investor Transaction Daily 
                    //04 Oct 2017 Start

                    var param2 = new { @QType = "Collection", Coll_Pay_transfer_ID = collec.Id.ToString(), lcl_BusinessDate = SessionHelper.TransactionDate, CreateUser = SessionHelper.LoggedInUserId, lcl_OfficeID = SessionHelper.LoggedInOfficeId };
                    spService.GetDataWithParameter(param2, "SP_Insert_Inv_Trans_Daily_Coll_Pay_Approve");
                    //04 Oct 2017 End


                    return Json(result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result = "2", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RejectCollectionEx(string strList)
        {
            var Result = "";
            try
            {
                spService.GetDataBySqlCommand("UPDATE AccCollection SET IsActive = 0 WHERE Id IN (SELECT CODE FROM dbo.Split('" + strList + "',','))");
                return Json(Result = "Ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApproveCollection(List<int> AllCheckIds, string strList)
        {
            var result = string.Empty;
            try
            {
                if (AllCheckIds != null)
                {
                    foreach (var app in AllCheckIds)
                    {
                        var collec = accCollectionService.GetById(Convert.ToInt32(app));
                        collec.ApproveStatus = "Y";
                        collec.ApproveDate = SessionHelper.TransactionDate;
                        collec.ApproveBy = SessionHelper.LoggedInUserId;
                        collec.ClearanceStatus = null;
                        collec.ChequeStatusId = 2;
                        collec.ClearanceDate = SessionHelper.TransactionDate;
                        accCollectionService.Update(collec);

                        


                        // Save Acc Master

                        var acc = new AccTrxMaster();

                        acc.VoucherYear = DateTime.Now.Year;
                        acc.OfficeID = SessionHelper.LoggedInOfficeId;
                        acc.TrxDate = SessionHelper.TransactionDate;
                        acc.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                        acc.VoucherType = "Dr";
                        acc.VoucherTypeId = 1;
                        acc.IsPosted = false;
                        acc.IsYearlyClosing = false;
                        acc.IsAutoVoucher = true;
                        acc.IsRectify = false;
                        acc.IsActive = true;
                        acc.CreateDate = DateTime.Now;
                        acc.CreateUser = SessionHelper.LoggedInUserId;

                        var accMasterId = accTrxMasterService.Create(acc).TrxMasterID;

                        var AccIdDe = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == true).FirstOrDefault();
                        var AccIdCre = accReceiptPaymentMappingService.GetAll().Where(x => x.TransactionTypeId == 38 && x.AccTransactionModeId == collec.TransactionModeId && x.FormEntryType == true).FirstOrDefault();

                        var AccIdDr = Convert.ToInt32(AccIdDe.AccIdDr);
                        var AccIdDrCode = AccIdDe.AccCodeDr;

                        var AccIdCr = Convert.ToInt32(AccIdCre.AccIdCr);
                        var AAccIdCreCode = AccIdCre.AccCodeCr;


                        ////if (collec.TransactionModeId == 2 || collec.TransactionModeId == 6)//Cash
                        ////{
                        ////     //Debit


                        //  AccId = 38 For receive

                        var accDetails = new AccTrxDetail();
                        accDetails.TrxMasterID = accMasterId;
                        //accDetails.MarketId = 0;//Session
                        //accDetails.CompanyId = 0;//Session
                        accDetails.AccID = AccIdDr;
                        accDetails.AccCode = AccIdDrCode;
                        accDetails.Debit = collec.Amount;
                        accDetails.InvestorId = collec.InvestorId;
                        accDetails.TransactionTypeId = 38;
                        accDetails.Credit = 0;
                        accDetails.CreateDate = DateTime.Now;
                        accDetails.IsActive = true;
                        accDetails.CreateUser = SessionHelper.LoggedInUserId;
                        accDetails.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                        accTrxDetailService.Create(accDetails);


                        //Credit

                        var accDe = new AccTrxDetail();

                        accDe.TrxMasterID = accMasterId;
                        //accDe.MarketId = 0;//Session
                        //accDe.CompanyId = 0;//Session
                        accDe.AccID = AccIdCr;
                        accDe.AccCode = AAccIdCreCode;
                        accDe.Debit = 0;
                        accDe.Credit = collec.Amount;
                        accDe.InvestorId = collec.InvestorId;
                        accDe.TransactionTypeId = 38;
                        accDe.CreateDate = DateTime.Now;
                        accDe.IsActive = true;
                        accDe.CreateUser = SessionHelper.LoggedInUserId;
                        accDe.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + collec.InvestorId + ")").Tables[0].Rows[0][0]);
                        accTrxDetailService.Create(accDe);

                        // Investor Balance Daily **************

                        var IvnbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == collec.InvestorId);

                        var balanceDaily = IvnbalanceDaily.FirstOrDefault();
                        if (IvnbalanceDaily.Count() == 0)
                        {
                            var param = new { InvestorId = collec.InvestorId, Amount = collec.Amount, TransactionDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), CreatedUserId = SessionHelper.LoggedInUserId };
                            spService.GetDataWithParameter(param, "Insert_InvestorBalanceDaily");
                        }
                        else
                        {
                            balanceDaily.ClearingChequeBalance = balanceDaily.ClearingChequeBalance + collec.Amount;
                            balanceDaily.CurrentBalance = balanceDaily.CurrentBalance + collec.Amount;
                            balanceDaily.UpdateDate = DateTime.Now;
                            balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;
                            balanceDaily.ActionStatus = true;
                            investorBalanceDailyService.Update(balanceDaily);
                        }

                    }

                    var param2 = new { @QType = "Collection", Coll_Pay_transfer_ID = strList, lcl_BusinessDate = SessionHelper.TransactionDate, CreateUser = SessionHelper.LoggedInUserId, lcl_OfficeID = SessionHelper.LoggedInOfficeId };
                    spService.GetDataWithParameter(param2, "SP_Insert_Inv_Trans_Daily_Coll_Pay_Approve");

                    return Json(result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result = "2", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveChequeCash(string AccCollectionId, string InvestorId, string Date, string TransactionMode, string ChequeNo, string ChequeDate, string BankId, string BankBranchId, string Amount, string ReceiveDate, string ValueDate, string BranchId, string Remarks, string IPODeclarationId, bool IsCloseInv, string RoutingNo = null)
        {
            var Result = "";
            try
            {

                if (AccCollectionId == "0")
                {
                    //var Voucher_No = accCollectionService.GetAll().Max(x=>x.VoucherNo);// (x => x.VoucherNo);
                    var Voucher_No = decimal.Parse(spService.GetDataBySqlCommand("DECLARE @MAX VARCHAR(20)=''; SELECT @MAX=MAX(VoucherNo) FROM AccCollection; SELECT CASE WHEN @MAX IS NULL THEN 0 ELSE @MAX END").Tables[0].Rows[0][0].ToString());
                    var acc = new AccCollection();

                    acc.InvestorId = Convert.ToInt32(InvestorId);
                    acc.VoucherNo = Voucher_No + 1;
                    acc.VoucherYear = DateTime.Now.Year;
                    acc.PostDate = Convert.ToDateTime(ReportHelper.FormatDateToString(Date));
                    acc.TransactionModeId = Convert.ToInt32(TransactionMode);
                    acc.ChequeNo = ChequeNo;
                    if (!string.IsNullOrEmpty(ChequeDate))
                        acc.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(ChequeDate));
                    acc.RoutingNo = RoutingNo;
                    if (!string.IsNullOrEmpty(BankId))
                        acc.BankId = Convert.ToInt32(BankId);
                    if (!string.IsNullOrEmpty(BankBranchId))
                        acc.BankBranchId = Convert.ToInt32(BankBranchId);
                    acc.Amount = Convert.ToDecimal(Amount);

                    acc.TransactionDate = SessionHelper.TransactionDate;
                    if (!string.IsNullOrEmpty(ValueDate))
                        acc.Valuedate = Convert.ToDateTime(ReportHelper.FormatDateToString(ValueDate));
                    //if (!string.IsNullOrEmpty(BranchId))
                    //    acc.BranchId = Convert.ToInt32(BranchId);
                    acc.Remarks = Remarks;
                    acc.ApproveStatus = "N";
                    acc.DepositStatus = "N";
                    acc.IPODeclarationId = Convert.ToInt32(IPODeclarationId);
                    acc.ChequeStatusId = 1;
                    acc.CreateDate = DateTime.Now;
                    acc.CreatedUserId = SessionHelper.LoggedInUserId;
                    acc.IsActive = true;
                    acc.IsClosedInvestor = IsCloseInv;
                    acc.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    acc.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]); //Convert.ToInt32(spService.GetDataBySqlCommand("SELECT A.BrokerBranchId FROM InvestorAccount AS A WHERE A.InvestorId = " + Convert.ToInt32(InvestorId) + "").Tables[0].Rows[0][0]);
                    accCollectionService.Create(acc);
                }
                else
                {

                    var acc = accCollectionService.GetById(Convert.ToInt32(AccCollectionId));

                    acc.InvestorId = Convert.ToInt32(InvestorId);
                    acc.PostDate = Convert.ToDateTime(ReportHelper.FormatDateToString(Date));
                    acc.TransactionModeId = Convert.ToInt32(TransactionMode);
                    acc.ChequeNo = ChequeNo;
                    acc.RoutingNo = RoutingNo;
                    if (!string.IsNullOrEmpty(ChequeDate))
                        acc.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(ChequeDate));
                    if (!string.IsNullOrEmpty(BankId))
                        acc.BankId = Convert.ToInt32(BankId);
                    if (!string.IsNullOrEmpty(BankBranchId))
                        acc.BankBranchId = Convert.ToInt32(BankBranchId);
                    acc.Amount = Convert.ToDecimal(Amount);

                    
                    //acc.TransactionDate = SessionHelper.TransactionDate;
                    if (!string.IsNullOrEmpty(ValueDate))
                        acc.Valuedate = Convert.ToDateTime(ReportHelper.FormatDateToString(ValueDate));
                    if (!string.IsNullOrEmpty(BranchId))
                        acc.BranchId = Convert.ToInt32(BranchId);
                    acc.Remarks = Remarks;
                    acc.ApproveStatus = "N";
                    acc.DepositStatus = "N";
                    acc.UpdateDate = DateTime.Now;
                    acc.UpdateUserId = SessionHelper.LoggedInUserId;

                    accCollectionService.Update(acc);
                }

                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public class AutoCompleteViewModel
        {
            public int Id { get; set; }
            public string InvestorName { get; set; }
            public string InvestorCode { get; set; }
            public decimal? CurrentBalance { get; set; }
            public string BrokerBranchName { get; set; }
            public string InvestorBranchName { get; set; }
        }
        //


        public JsonResult GetClosedInvestorByCode(string InvestorCode)
        {
            try
            {
                List<AutoCompleteViewModel> InvestorList = new List<AutoCompleteViewModel>();
                var param = new { INVESTOR_CODE = InvestorCode };
                var empList = spService.GetDataWithParameter(param, "SP_GET_Closed_INVESTOR_NAME_BY_CODE");

                InvestorList = empList.Tables[0].AsEnumerable()
                .Select(row => new AutoCompleteViewModel
                {
                    Id = row.Field<int>("Id"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    InvestorBranchName = row.Field<string>("InvestorBranchName")
                }).ToList();
                return Json(InvestorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetInvestorByCode(string InvestorCode)
        {
            try
            {
                List<AutoCompleteViewModel> InvestorList = new List<AutoCompleteViewModel>();
                var param = new { INVESTOR_CODE = InvestorCode };
                var empList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE");

                InvestorList = empList.Tables[0].AsEnumerable()
                .Select(row => new AutoCompleteViewModel
                {
                    Id = row.Field<int>("Id"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    CurrentBalance = row.Field<decimal?>("CurrentBalance"),
                    InvestorBranchName = row.Field<string>("InvestorBranchName")
                }).ToList();
                return Json(InvestorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetInvestorList(string QType)
        {
            try
            {
                List<AutoCompleteViewModel> InvestorList = new List<AutoCompleteViewModel>();
                var param = new { QType = QType };
                var empList = spService.GetDataWithParameter(param, "GetInvestorListForAutoComplete");

                InvestorList = empList.Tables[0].AsEnumerable()
                .Select(row => new AutoCompleteViewModel
                {
                    Id = row.Field<int>("Id"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                }).ToList();
                return Json(InvestorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public class AccCollectionViewModel
        {
            //Id,VoucherNo,InvestorCode,InvestorName,TransactionMode,ChequeNo, ChequeDateMsg,C.BankName,C.BankBranchName,C.Amount,TransactionDateMsg,PostDateMsg,C.DocRefNo,C.BranchId AS BrokerBranch,C.Remarks,ValuedateMsg 
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
            public int AccCollectionId { get; set; }
            public string Remarks { get; set; }
            public string ValuedateMsg { get; set; }
            public int DishonourCauseId { get; set; }
            public string DishonourCause { get; set; }
            public int? IPODeclarationId { get; set; }
            public string InvestorBranchName { get; set; }
        }


        public void CollectionApprovePrint()
        {
            var exportType = "Pdf";
            var data = spService.GetDataWithoutParameter("GetCollectionInfoBeforeApprove").Tables[0];
            ReportHelper.ShowReport(data, exportType, "rptCollectionForApprove.rpt", "rptCollectionForApprove");

        }



        public JsonResult GetCollectionInfoBeforeApprove()
        {
            try
            {
                List<AccCollectionViewModel> CollecList = new List<AccCollectionViewModel>();
                var param = new { BranchId = SessionHelper.LoggedInOfficeId };
                var empList = spService.GetDataWithParameter(param, "GetCollectionInfoBeforeApproveBranchwise");

                CollecList = empList.Tables[0].AsEnumerable()
                .Select(row => new AccCollectionViewModel
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
                    BrokerBranchId = row.Field<int>("BrokerBranchId"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName"),
                    Remarks = row.Field<string>("Remarks"),
                    ValuedateMsg = row.Field<string>("ValuedateMsg"),
                    // IPODeclarationId = row.Field<int>("IPODeclarationId"),
                    InvestorBranchName = row.Field<string>("InvestorBranchName")

                }).ToList();
                return Json(CollecList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }//


        public JsonResult GetCollectionInfoOfClosedInvestorBeforeApprove()
        {
            try
            {
                List<AccCollectionViewModel> CollecList = new List<AccCollectionViewModel>();
                var param = new { BrokerBranchId = SessionHelper.LoggedInOfficeId };
                var empList = spService.GetDataWithParameter(param, "GetCollectionInfoOf_ClosedInvestor_BeforeApprove");

                CollecList = empList.Tables[0].AsEnumerable()
                .Select(row => new AccCollectionViewModel
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
                    BrokerBranchId = row.Field<int>("InvestorBranchId"),
                    InvestorBranchName = row.Field<string>("InvestorBranchName"),
                    Remarks = row.Field<string>("Remarks"),
                    ValuedateMsg = row.Field<string>("ValuedateMsg"),
                    //IPODeclarationId = row.Field<int>("IPODeclarationId")

                }).ToList();
                var json = Json(CollecList, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        //var json = Json(List_Investor, JsonRequestBehavior.AllowGet);
        //      json.MaxJsonLength = int.MaxValue;
        //      return json;






        public JsonResult GetCollectionInfoToDepositBank()
        {
            try
            {
              
                var empList = spService.GetDataWithoutParameter("GetCollectionInfoToDepositBank");

                var CollecList = empList.Tables[0].AsEnumerable()
                .Select(row => new 
                {
                    Id = row.Field<int>("Id"),
                    VoucherNo = row.Field<decimal>("VoucherNo"),
                    InvestorId = row.Field<int?>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    TransactionModeName = row.Field<string>("TransactionModeName"),
                    ChequeNo = row.Field<string>("ChequeNo"),
                    ChequeDateMsg = row.Field<string>("ChequeDateMsg"),
                    BankName = row.Field<string>("BankName"),
                    BankBranchName = row.Field<string>("BankBranchName"),
                    Amount = row.Field<decimal>("Amount"),
                    TransactionDateMsg = row.Field<string>("TransactionDateMsg"),
                    PostDateMsg = row.Field<string>("PostDateMsg"),
                    DocRefNo = row.Field<string>("DocRefNo"),
                    BrokerBranchId = row.Field<int>("BrokerBranchId"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName"),
                    Remarks = row.Field<string>("Remarks"),
                    ValuedateMsg = row.Field<string>("ValuedateMsg"),
                    InvestorTypeShortName = row.Field<string>("InvestorTypeShortName")

                }).ToList();
                var json = Json(CollecList, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;

                // return Json(CollecList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCollectionInfoDishonour(string BankId, string ChequeNo, string InvestorCode)
        {
            try
            {
                //List<AccCollectionViewModel> CollecList = new List<AccCollectionViewModel>();
                var param = new { BankId = Convert.ToInt32(BankId), ChequeNo = ChequeNo, InvestorCode = InvestorCode };
                var empList = spService.GetDataWithParameter(param, "GetCollectionInfoForClearanceDishonour");

                var CollecList = empList.Tables[0].AsEnumerable()
                 .Select(row => new //AccCollectionViewModel
                 {
                     Id = row.Field<int>("Id"),
                     VoucherNo = row.Field<decimal>("VoucherNo"),
                     InvestorId = row.Field<int?>("InvestorId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
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
                     Remarks = row.Field<string>("Remarks"),
                     ValuedateMsg = row.Field<string>("ValuedateMsg"),

                 }).ToList();
                var json = Json(CollecList, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
                // return Json(CollecList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCollectionInfoForRedeposit(string BankId, string ChequeNo, string InvestorCode)
        {
            try
            {
                List<AccCollectionViewModel> CollecList = new List<AccCollectionViewModel>();
                var param = new { BankId = Convert.ToInt32(BankId), ChequeNo = ChequeNo, InvestorCode = InvestorCode };
                var empList = spService.GetDataWithParameter(param, "GetCollectionInfoForRedeposit");

                CollecList = empList.Tables[0].AsEnumerable()
                .Select(row => new AccCollectionViewModel
                {
                    Id = row.Field<int>("Id"),
                    AccCollectionId = row.Field<int>("AccCollectionId"),
                    DishonourCauseId = row.Field<int>("DishonourCauseId"),
                    DishonourCause = row.Field<string>("DishonourCause"),
                    VoucherNo = row.Field<decimal>("VoucherNo"),
                    InvestorId = row.Field<int?>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
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
                    BrokerBranchId = row.Field<int>("BrokerBranchId"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName"),
                    Remarks = row.Field<string>("Remarks"),
                    ValuedateMsg = row.Field<string>("ValuedateMsg"),

                }).ToList();
                return Json(CollecList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public class GLViewMode
        {
            public int Id { get; set; }
            public string BankName { get; set; }
            public string BranchName { get; set; }
            public int IsDLRAccount { get; set; }
        }

        public JsonResult GetGLBankBranchList(string BankId)
        {
            try
            {
                List<GLViewMode> BankBrach = new List<GLViewMode>();
                var param = new { BankId = Convert.ToInt32(BankId) };
                var emList = spService.GetDataWithParameter(param, "GetGLBankBranch");

                BankBrach = emList.Tables[0].AsEnumerable()
                .Select(row => new GLViewMode
                {
                    Id = row.Field<int>("Id"),
                    BranchName = row.Field<string>("BranchName"),
                }).ToList();
                return Json(BankBrach, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetGLBank()
        {
            try
            {
                List<GLViewMode> GLViewModeList = new List<GLViewMode>();
                var emList = spService.GetDataWithoutParameter("GetGLBank");

                GLViewModeList = emList.Tables[0].AsEnumerable()
                .Select(row => new GLViewMode
                {
                    Id = row.Field<int>("Id"),
                    BankName = row.Field<string>("BankName"),
                }).ToList();
                return Json(GLViewModeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetGLAccountNo(string BranchId, int IsDlr)
        {
            try
            {
                //List<GLViewMode> emList = new List<GLViewMode>();
                var emList = accBankBranchGLService.GetAll().Where(x => x.BranchId == Convert.ToInt32(BranchId) && x.IsDLRAccount == IsDlr);

                //GLViewModeList = emList.sele
                //.Select(row => new GLViewMode
                //{
                //    Id = row.Field<int>("Id"),
                //    BankName = row.Field<string>("BankName"),
                //}).ToList();
                return Json(emList.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public void GetReceivePaymentReport(string Qtype, string FromDate, string ToDate, string BrokerBranchId, string TransMode, int Status, string exportType, string investorBranch)
        {
            var param = new { Qtype = Qtype, FirstDate = ReportHelper.FormatDateToString(FromDate), EndDate = ReportHelper.FormatDateToString(ToDate), BrokerBranch = BrokerBranchId, TransactionMode = TransMode, Status = Status, InvestorBranch = investorBranch };
            
            var data = spService.GetDataWithParameter(param, "Rpt_ChecCashkReceivePayment").Tables[0];

            if (TransMode != "2")
            {
                if (Qtype == "1")
                {

                    ReportHelper.ShowReport(data, exportType, "rptChequeReceiveInfo.rpt", "rptChequeReceiveInfo");
                }
                else //Qtype == "3"
                {
                    ReportHelper.ShowReport(data, exportType, "rptChequePaymentInfo.rpt", "rptChequePaymentInfo");
                }
            }
            else //Cash Receive
            {
                ReportHelper.ShowReport(data, exportType, "rptCashReceiveInfo.rpt", "rptCashReceiveInfo");
            }


        }


        public JsonResult ChequeNoValidation(string ChequeNo, int BankId)
        {
            var Result = string.Empty;

            var Coll = spService.GetDataBySqlCommand("SELECT * FROM AccCollection WHERE TransactionModeId = 1 AND BankId = " + BankId + " AND ChequeNo = '" + ChequeNo + "'").Tables[0].AsEnumerable();
            //.Select(x => x.Field<string>(0))
            //.ToList();

            if (Coll.Count() == 0)
            {
                Result = "1"; //Ok
            }
            else
            {
                Result = "2";
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Actions

        public ActionResult Index()
        {
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().Where(x => x.TransactionModeShortName != "FT").ToList();
            ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            ViewData["IPOCompanyList"] = items;
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;//.ToShortDateString("dd/mm/yy").ToString();
            return View();
        }

        public ActionResult IndexEx() // Closed Investor
        {
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().Where(x => x.TransactionModeShortName != "FT").ToList();
            ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            ViewData["IPOCompanyList"] = items;
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;//.ToShortDateString("dd/mm/yy").ToString();
            return View();
        }
        public ActionResult AccCollApprove()
        {
            return View();
        }
        public ActionResult CheckClDI()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["DishonourCauseList"] = items;
            return View();
        }
        public ActionResult ChequeDepositBank()
        {

            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Banklist"] = items;
            ViewData["BankBranchlist"] = items;
            ViewData["GLAccountList"] = items;
            return View();
        }
        public ActionResult ReDeposit()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            return View();
        }

        public ActionResult RcvPymtReport()
        {
            ViewBag.Branches = brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().ToList();
            return View();
        }

        #endregion
    }
}
