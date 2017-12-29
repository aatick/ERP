//using UcasPortfolio.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class NRBController : BaseController
    {
        #region Variables

        private readonly IIPODraftService iPODraftService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly ISPService spService;
        private readonly ILookupCurrencyService lookupCurrencyService;
        private readonly IIPOApplicationService iPOApplicationService;
        private readonly IIPOCurrencyMappingService ipoCurrencyMappingService;
        private readonly IIPODeclarationService ipoDeclarationService;
        private readonly IInvestorTransactionDailyService investorTransactionDailyService;
        private readonly IInvestorBalanceDailyService investorBalanceDailyService;
        private readonly ITradeTransactionChargeService tradeTransactionChargeService;
        private readonly IInvestorWiseTransactionChargeService investorWiseTransactionChargeService;
        private readonly IInvestorAccountService investorAccountService;
        private readonly ICompanyInfoService companyInfoService;
       
        //private readonly IIPOIssueMethodService ipoIssueMethodService;
       
        
        //private readonly IInvestorTypeService investorTypeService;
        //private readonly IIPOApplicationService iPOApplicationService;
        public NRBController(IIPODraftService iPODraftService, IBrokerBranchService brokerBranchService, ISPService spService, 
            ILookupCurrencyService lookupCurrencyService, IIPOApplicationService iPOApplicationService,
            IIPOCurrencyMappingService ipoCurrencyMappingService, IIPODeclarationService ipoDeclarationService,
            IInvestorTransactionDailyService investorTransactionDailyService, IInvestorBalanceDailyService investorBalanceDailyService,
            ITradeTransactionChargeService tradeTransactionChargeService,IInvestorWiseTransactionChargeService investorWiseTransactionChargeService
              , ICompanyInfoService companyInfoService, IInvestorAccountService investorAccountService
                 
                //, IInvestorTypeService investorTypeService,
                // 
             
                //
            )
        {
            this.iPODraftService = iPODraftService;
            this.brokerBranchService = brokerBranchService;
            this.spService = spService;
            this.lookupCurrencyService = lookupCurrencyService;
            this.iPOApplicationService = iPOApplicationService;
            this.ipoCurrencyMappingService = ipoCurrencyMappingService;
            this.ipoDeclarationService = ipoDeclarationService;
            this.investorTransactionDailyService = investorTransactionDailyService;
            this.investorBalanceDailyService = investorBalanceDailyService;
            this.tradeTransactionChargeService = tradeTransactionChargeService;
            this.investorWiseTransactionChargeService = investorWiseTransactionChargeService;
            this.companyInfoService = companyInfoService;
            this.investorAccountService = investorAccountService;
            //this.ipoIssueMethodService = ipoIssueMethodService;
            
            
           
            //this.investorTypeService = investorTypeService;
           
            //this.iPODeclarationService = iPODeclarationService;         
        }

        #endregion

        #region Methods


        public JsonResult CheckFddNumber(int BankId, string FddNumber)
        {
            var Result = "";

            var param = new { FDDNumber = FddNumber, BankId = BankId };

            if (spService.GetDataWithParameter(param, "ChechFddNumber").Tables[0].Rows.Count >= 1)
               {
                   Result = "0";
               }
            else
               {
                   Result = "1";
               }
                    

            return Json(Result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteNRBDraft(int Id)
        {
            var Result = string.Empty;


            var Draft = iPODraftService.GetById(Convert.ToInt32(Id));
            Draft.IsActive = false;
            Draft.UpdateDate = DateTime.Now;
            Draft.UpdateUserId = SessionHelper.LoggedInUserId;
            iPODraftService.Update(Draft);


            var IpoJoin = iPOApplicationService.GetAll().Where(x => x.DraftId == Convert.ToInt32(Id));
            if (IpoJoin != null)
            {
                foreach( var app in IpoJoin)
                {

                    app.IsActive = false;
                    app.UpdateUserId = SessionHelper.LoggedInUserId;
                    app.UpdateDate = DateTime.Now;
                    iPOApplicationService.Update(app);

                }
               
            }
           return Json(Result = "1", JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetIPODraftInformation_Foreign(int DeclarationId)
        {
            var param = new { DeclarationId = DeclarationId };
            var data =
                spService.GetDataWithParameter(param, "Get_IPO_Draft_InformationForForeign").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSl = x.Field<string>("RowSl"),//InvestorName JoinHolder
                                Id = x.Field<int>("Id"),
                                IPODeclarationId = x.Field<int>("IPODeclarationId"),
                                InvestorId = x.Field<int>("InvestorId"), 
                                InvestorName = x.Field<string>("InvestorName"),
                                BranchName = x.Field<string>("BranchName"),
                                BankName = x.Field<string>("BankName"),
                                CompanyName = x.Field<string>("CompanyName"),
                                CurrencyId = x.Field<int>("CurrencyId"),
                                CurrencyName = x.Field<string>("CurrencyShortName"),
                                FDDNumber = x.Field<string>("FDDNumber"),
                                Amount = x.Field<decimal?>("Amount"),
                                DraftDateMsg = x.Field<string>("DraftDateMsg"),
                                ApplicationStatus = x.Field<int>("ApplicationStatus"),
                                IssuerBankBranchId = x.Field<int?>("IssuerBankBranchId"),
                                BankId = x.Field<int?>("BankId"),
                                InvestorShareQty = x.Field<int?>("InvestorShareQty"),
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult GetIPODraftInformation(int DeclarationId) 
        {
            var param = new { DeclarationId = DeclarationId, UserRoleId = SessionHelper.LoggedIn_RoleId, UserId = SessionHelper.LoggedInUserId, TransactionDate =Convert.ToDateTime(ReportHelper.FormatDateToString(SessionHelper.BusinessDate))};
            var data =
                spService.GetDataWithParameter( param,"Get_IPO_Draft_Information").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSl = x.Field<string>("RowSl"),//InvestorName JoinHolder
                                Id = x.Field<int>("Id"),
                                IPODeclarationId = x.Field<int>("IPODeclarationId"),
                                InvestorId = x.Field<int>("InvestorId"),
                                InvestorJointId = x.Field<int?>("InvestorJointId"),
                                InvestorName = x.Field<string>("InvestorName"),
                                JoinHolder = x.Field<string>("JoinHolder"),
                                BranchName = x.Field<string>("BranchName"),
                                BankName = x.Field<string>("BankName"),
                                CompanyName = x.Field<string>("CompanyName"),
                                CurrencyId = x.Field<int>("CurrencyId"),
                                CurrencyName = x.Field<string>("CurrencyShortName"),
                                FDDNumber = x.Field<string>("FDDNumber"),
                                Amount = x.Field<decimal?>("Amount"),
                                DraftDateMsg = x.Field<string>("DraftDateMsg"),
                                ApplicationStatus = x.Field<int>("ApplicationStatus"),
                                IssuerBankBranchId = x.Field<int?>("IssuerBankBranchId"),
                                BankId = x.Field<int?>("BankId"),
                                InvestorShareQty = x.Field<int?>("InvestorShareQty"),
                                JoinShareQty = x.Field<int?>("JoinShareQty"),
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public class AutoCompleteViewModel
        {
            public int Id { get; set; }
            public string InvestorName { get; set; }
            public string InvestorCode { get; set; }
        }

        public JsonResult SaveIPODraftForeign(string NRBDraftId, string IpoDeclarationId, string InvestorId, string BankBranchId, string CurrencyId, string FddNumber, string Amount, string DraftDate, string InvestorShareQty)
        {
            var Result = "0";
            try
            {
                if (NRBDraftId == "0")  //Save
                {
                    var IPODec = ipoDeclarationService.GetById(Convert.ToInt32(IpoDeclarationId));

                    var Draft = new IPODraft();
                    Draft.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                    Draft.CompanyId = Convert.ToInt32(IPODec.CompanyId);
                    Draft.InvestorId = Convert.ToInt32(InvestorId);
                    
                    if (BankBranchId != "")
                        Draft.IssuerBankBranchId = Convert.ToInt32(BankBranchId);
                    Draft.CurrencyId = Convert.ToInt32(CurrencyId);
                    Draft.FDDNumber = FddNumber;
                    if (Amount != null)
                        Draft.Amount = Convert.ToDecimal(Amount);
                    Draft.InvestorShareQty = Convert.ToInt32(InvestorShareQty);
                    Draft.DraftDate = Convert.ToDateTime(ReportHelper.FormatDateToString(DraftDate));
                    Draft.CreateDate = DateTime.Now;
                    Draft.CreatedUserId = SessionHelper.LoggedInUserId;
                    Draft.IsActive = true;
                    Draft.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    var DraftId = iPODraftService.Create(Draft).Id;


                    var Currency = ipoCurrencyMappingService.GetAll().Where(x => x.IPODeclarationId == Convert.ToInt32(IpoDeclarationId) && x.CurrencyId == Convert.ToInt32(CurrencyId)).FirstOrDefault();
                    decimal InvAmount = 0;
                    decimal JoinAmount = 0;

                    var InvReq = (Currency.LotAmount / IPODec.Lot) * Convert.ToInt32(InvestorShareQty);

                    if (Convert.ToDecimal(Amount) > InvReq)
                    {
                        JoinAmount = Convert.ToDecimal(Amount) - InvReq;
                        InvAmount = InvReq;
                    }
                    else
                    {
                        JoinAmount = 0;
                        InvAmount = InvReq;
                    }

                    //Investor

                    var Ipo = new IPOApplication();
                    Ipo.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                    Ipo.InvestorId = Convert.ToInt32(InvestorId);
                    Ipo.DraftId = DraftId;
                    if (InvestorShareQty != null && InvestorShareQty != "")
                        Ipo.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                    Ipo.Lot = 1;
                    Ipo.CurrencyId = Convert.ToInt32(CurrencyId);
                    Ipo.UnitPrice = Convert.ToDecimal(InvAmount);
                    Ipo.AppliedAmount = Convert.ToDecimal(InvAmount);
                    Ipo.OrderStatus = true;
                    Ipo.OrderDate = SessionHelper.TransactionDate;
                    Ipo.OrderUserId = SessionHelper.LoggedInUserId;
                    Ipo.CreateDate = DateTime.Now;
                    Ipo.CreatedUserId = SessionHelper.LoggedInUserId;
                    Ipo.IsActive = true;
                    Ipo.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                    iPOApplicationService.Create(Ipo);

                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                }
                else   //Edit
                {
                    var IPODec = ipoDeclarationService.GetById(Convert.ToInt32(IpoDeclarationId));

                    var Draft = iPODraftService.GetById(Convert.ToInt32(NRBDraftId));

                    Draft.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                    Draft.InvestorId = Convert.ToInt32(InvestorId);
                    if (BankBranchId != "")
                        Draft.IssuerBankBranchId = Convert.ToInt32(BankBranchId);
                    Draft.CurrencyId = Convert.ToInt32(CurrencyId);
                    Draft.FDDNumber = FddNumber;
                    if (Amount != null)
                        Draft.Amount = Convert.ToDecimal(Amount);
                    Draft.InvestorShareQty = Convert.ToInt32(InvestorShareQty);
                    Draft.UpdateDate = DateTime.Now;
                    Draft.UpdateUserId = SessionHelper.LoggedInUserId;
                    Draft.IsActive = true;
                    iPODraftService.Update(Draft);


                    var Currency = ipoCurrencyMappingService.GetAll().Where(x => x.IPODeclarationId == Convert.ToInt32(IpoDeclarationId) && x.CurrencyId == Convert.ToInt32(CurrencyId)).FirstOrDefault();
                    decimal InvAmount = 0;
                    decimal JoinAmount = 0;

                    var InvReq = (Currency.LotAmount / IPODec.Lot) * Convert.ToInt32(InvestorShareQty);

                    if (Convert.ToDecimal(Amount) > InvReq)
                    {
                        JoinAmount = Convert.ToDecimal(Amount) - InvReq;
                        InvAmount = InvReq;
                    }
                    else
                    {
                        JoinAmount = 0;
                        InvAmount = InvReq;
                    }

                    //  EDIT

                    var Ipo = iPOApplicationService.GetAll().Where(x => x.DraftId == Convert.ToInt32(NRBDraftId) && x.InvestorId == Convert.ToInt32(InvestorId)).FirstOrDefault();
                    if (Ipo != null)
                    {
                        if (InvestorShareQty != null && InvestorShareQty != "")
                            Ipo.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                        Ipo.Lot = 1;
                        Ipo.CurrencyId = Convert.ToInt32(CurrencyId);
                        Ipo.UnitPrice = Convert.ToDecimal(InvAmount);
                        Ipo.AppliedAmount = Convert.ToDecimal(InvAmount);
                        Ipo.UpdateDate = DateTime.Now;
                        Ipo.UpdateUserId = SessionHelper.LoggedInUserId;
                        iPOApplicationService.Update(Ipo);
                    }
                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveIPODraft(string NRBDraftId, string IpoDeclarationId, string InvestorId, string JoinHolderId, string BankBranchId, string CurrencyId, string FddNumber, string Amount, string DraftDate, string InvestorShareQty, string JoinHolderShareQty)
        {
            var Result = "0";
            try
            {
                if (NRBDraftId == "0")  //Save
                {
                    var IPODec = ipoDeclarationService.GetById(Convert.ToInt32(IpoDeclarationId));

                    var Draft = new IPODraft();
                    Draft.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                    Draft.CompanyId = Convert.ToInt32(IPODec.CompanyId);
                    Draft.InvestorId = Convert.ToInt32(InvestorId);
                    if (JoinHolderId != null && JoinHolderId != "0" && JoinHolderId != "")
                        Draft.InvestorJointId = Convert.ToInt32(JoinHolderId);
                    if (BankBranchId != "")
                        Draft.IssuerBankBranchId = Convert.ToInt32(BankBranchId);
                    Draft.CurrencyId = Convert.ToInt32(CurrencyId);
                    Draft.FDDNumber = FddNumber;
                    if (Amount != null)
                        Draft.Amount = Convert.ToDecimal(Amount);
                    Draft.InvestorShareQty = Convert.ToInt32(InvestorShareQty);
                    Draft.JoinShareQty = Convert.ToInt32(JoinHolderShareQty);
                    Draft.DraftDate = Convert.ToDateTime(ReportHelper.FormatDateToString(DraftDate));
                    Draft.CreateDate = DateTime.Now;
                    Draft.CreatedUserId = SessionHelper.LoggedInUserId;
                    Draft.IsActive = true;
                    Draft.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    var DraftId = iPODraftService.Create(Draft).Id;


                    var Currency = ipoCurrencyMappingService.GetAll().Where(x => x.IPODeclarationId == Convert.ToInt32(IpoDeclarationId) && x.CurrencyId == Convert.ToInt32(CurrencyId)).FirstOrDefault();
                    decimal InvAmount = 0;
                    decimal JoinAmount = 0;

                    var InvReq = (Currency.LotAmount / IPODec.Lot) * Convert.ToInt32(InvestorShareQty);

                    if (Convert.ToDecimal(Amount) > InvReq)
                    {
                        JoinAmount = Convert.ToDecimal(Amount) - InvReq;
                        InvAmount = InvReq;
                    }
                    else
                    {
                        JoinAmount = 0;
                        InvAmount = InvReq;
                    }

                    //Investor

                    var Ipo = new IPOApplication();
                    Ipo.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                    Ipo.InvestorId = Convert.ToInt32(InvestorId);
                    Ipo.DraftId = DraftId;
                    if (InvestorShareQty != null && InvestorShareQty != "")
                        Ipo.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                    Ipo.Lot = 1;
                    Ipo.CurrencyId = Convert.ToInt32(CurrencyId);
                    Ipo.UnitPrice = Convert.ToDecimal(InvAmount);
                    Ipo.AppliedAmount = Convert.ToDecimal(InvAmount);
                    Ipo.OrderStatus = true;
                    Ipo.InvestorTypeId = investorAccountService.GetByInvestorId(Convert.ToInt32(InvestorId)).InvestorTypeId;
                    Ipo.OrderDate = SessionHelper.TransactionDate;
                    Ipo.OrderUserId = SessionHelper.LoggedInUserId;
                    Ipo.CreateDate = DateTime.Now;
                    Ipo.CreatedUserId = SessionHelper.LoggedInUserId;
                    Ipo.IsActive = true;
                    Ipo.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    Ipo.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                    iPOApplicationService.Create(Ipo);


                    //IPO Application Fee

                    //decimal Deduct_Amount = 0;

                    //var ivnTrn = new InvestorTransactionDaily();
                    //ivnTrn.InvestorId = Convert.ToInt32(InvestorId);
                    //ivnTrn.TransactionTypeId = 14; //IPO Application Fee
                    //ivnTrn.TransactionDate = SessionHelper.TransactionDate;
                    //ivnTrn.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                    //ivnTrn.AverageUnitPrice = 0;//
                    //ivnTrn.DebitAmount = 0;
                    //Deduct_Amount = Deduct_Amount + 0; //DebitAmount
                    //ivnTrn.CreditAmount = 0;
                    //ivnTrn.CreateDate = DateTime.Now;
                    //ivnTrn.CreatedUserId = SessionHelper.LoggedInUserId;
                    //ivnTrn.IsActive = true;
                    //investorTransactionDailyService.Create(ivnTrn);




                    ////IPO Apply Charge

                    //var ivnTrnx = new InvestorTransactionDaily();
                    //ivnTrnx.InvestorId = Convert.ToInt32(InvestorId);
                    //ivnTrnx.TransactionTypeId = 15; //IPO Apply Charge
                    //ivnTrnx.TransactionDate = SessionHelper.TransactionDate;
                    //ivnTrnx.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                    //ivnTrnx.AverageUnitPrice = 0;// Ipo.UnitPrice;
                    //var chargex = investorWiseTransactionChargeService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(InvestorId) && x.TransactionTypeId == 15);
                    //if (chargex.Count() == 0)
                    //{
                    //    var ChargeAmount = tradeTransactionChargeService.GetAll().Where(x => x.TransactionTypeId == 15).FirstOrDefault().DSECharge;
                    //    ivnTrnx.DebitAmount = ChargeAmount;
                    //    Deduct_Amount = Deduct_Amount + ChargeAmount;
                    //}
                    //else
                    //{
                    //    ivnTrnx.DebitAmount = chargex.FirstOrDefault().ChargeAmount;
                    //    Deduct_Amount = Deduct_Amount + chargex.FirstOrDefault().ChargeAmount;
                    //}

                    //ivnTrnx.CreditAmount = 0;
                    //ivnTrnx.CreateDate = DateTime.Now;
                    //ivnTrnx.CreatedUserId = SessionHelper.LoggedInUserId;
                    //ivnTrnx.IsActive = true;
                    //investorTransactionDailyService.Create(ivnTrnx);

                    ////Investor Balance Daily

                    //var balanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(InvestorId)).FirstOrDefault();
                    //balanceDaily.CurrentBalance = balanceDaily.CurrentBalance - Deduct_Amount;
                    //investorBalanceDailyService.Update(balanceDaily);


                    /////////////////   /////////////////////////////////////////////////
                    /////////////////   /////////////////////////////////////////////////


                    //Join Holder

                    if (JoinHolderId != "0" && JoinHolderId != null && JoinHolderId != "")
                    {
                        var IpoJoin = new IPOApplication();
                        IpoJoin.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                        IpoJoin.InvestorId = Convert.ToInt32(JoinHolderId);
                        IpoJoin.DraftId = DraftId;
                        if (JoinHolderShareQty != null && JoinHolderShareQty != "")
                            IpoJoin.ShareQuantity = Convert.ToInt32(JoinHolderShareQty);
                        IpoJoin.Lot = 1;
                        IpoJoin.UnitPrice = Convert.ToDecimal(JoinAmount);
                        IpoJoin.AppliedAmount = Convert.ToDecimal(JoinAmount);
                        IpoJoin.CurrencyId = Convert.ToInt32(CurrencyId);
                        IpoJoin.OrderStatus = true;
                        IpoJoin.OrderDate = SessionHelper.TransactionDate;
                        IpoJoin.OrderUserId = SessionHelper.LoggedInUserId;
                        IpoJoin.CreateDate = DateTime.Now;
                        IpoJoin.CreatedUserId = SessionHelper.LoggedInUserId;
                        IpoJoin.IsActive = true;
                        IpoJoin.InvestorTypeId = investorAccountService.GetByInvestorId(Convert.ToInt32(JoinHolderId)).InvestorTypeId;
                        IpoJoin.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + JoinHolderId + ")").Tables[0].Rows[0][0]);
                        IpoJoin.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                        iPOApplicationService.Create(IpoJoin);



                        //    //IPO Application Fee

                        //    decimal JoinDeduct_Amount = 0;

                        //    var JoinivnTrn = new InvestorTransactionDaily();
                        //    JoinivnTrn.InvestorId = Convert.ToInt32(JoinHolderId);
                        //    JoinivnTrn.TransactionTypeId = 14; //IPO Application Fee
                        //    JoinivnTrn.TransactionDate = SessionHelper.TransactionDate;
                        //    JoinivnTrn.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                        //    JoinivnTrn.AverageUnitPrice = 0;//
                        //    JoinivnTrn.DebitAmount = 0;
                        //    JoinDeduct_Amount = JoinDeduct_Amount + 0; //DebitAmount
                        //    JoinivnTrn.CreditAmount = 0;
                        //    JoinivnTrn.CreateDate = DateTime.Now;
                        //    JoinivnTrn.CreatedUserId = SessionHelper.LoggedInUserId;
                        //    JoinivnTrn.IsActive = true;
                        //    investorTransactionDailyService.Create(JoinivnTrn);




                        //    //IPO Apply Charge

                        //    var JoinivnTrnx = new InvestorTransactionDaily();
                        //    JoinivnTrnx.InvestorId = Convert.ToInt32(JoinHolderId);
                        //    JoinivnTrnx.TransactionTypeId = 15; //IPO Apply Charge
                        //    JoinivnTrnx.TransactionDate = SessionHelper.TransactionDate;
                        //    JoinivnTrnx.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                        //    JoinivnTrnx.AverageUnitPrice = 0;// Ipo.UnitPrice;
                        //    var Joinchargex = investorWiseTransactionChargeService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(JoinHolderId) && x.TransactionTypeId == 15);
                        //    if (Joinchargex.Count() == 0)
                        //    {
                        //        var ChargeAmount = tradeTransactionChargeService.GetAll().Where(x => x.TransactionTypeId == 15).FirstOrDefault().DSECharge;
                        //        JoinivnTrnx.DebitAmount = ChargeAmount;
                        //        JoinDeduct_Amount = JoinDeduct_Amount + ChargeAmount;
                        //    }
                        //    else
                        //    {
                        //        JoinivnTrnx.DebitAmount = Joinchargex.FirstOrDefault().ChargeAmount;
                        //        JoinDeduct_Amount = JoinDeduct_Amount + Joinchargex.FirstOrDefault().ChargeAmount;
                        //    }

                        //    JoinivnTrnx.CreditAmount = 0;
                        //    JoinivnTrnx.CreateDate = DateTime.Now;
                        //    JoinivnTrnx.CreatedUserId = SessionHelper.LoggedInUserId;
                        //    JoinivnTrnx.IsActive = true;
                        //    investorTransactionDailyService.Create(JoinivnTrnx);

                        ////Investor Balance Daily

                        //    var JoinbalanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(JoinHolderId)).FirstOrDefault();
                        //    JoinbalanceDaily.CurrentBalance = JoinbalanceDaily.CurrentBalance - JoinDeduct_Amount;
                        //    investorBalanceDailyService.Update(JoinbalanceDaily);
                    }

                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                }
                else   //Edit
                {
                    var IPODec = ipoDeclarationService.GetById(Convert.ToInt32(IpoDeclarationId));

                    var Draft = iPODraftService.GetById(Convert.ToInt32(NRBDraftId));

                    Draft.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                   // Draft.CompanyId = Convert.ToInt32(IPODec.CompanyId);
                    Draft.InvestorId = Convert.ToInt32(InvestorId);
                    if (JoinHolderId != null && JoinHolderId != "0")
                        Draft.InvestorJointId = Convert.ToInt32(JoinHolderId);
                    if (BankBranchId != "")
                        Draft.IssuerBankBranchId = Convert.ToInt32(BankBranchId);
                    Draft.CurrencyId = Convert.ToInt32(CurrencyId);
                    Draft.FDDNumber = FddNumber;
                    if (Amount != null)
                        Draft.Amount = Convert.ToDecimal(Amount);
                    Draft.InvestorShareQty = Convert.ToInt32(InvestorShareQty);
                    Draft.JoinShareQty = Convert.ToInt32(JoinHolderShareQty);
                   // Draft.DraftDate = Convert.ToDateTime(DraftDate);
                    Draft.UpdateDate = DateTime.Now;
                    Draft.UpdateUserId  = SessionHelper.LoggedInUserId;
                    Draft.IsActive = true;
                    Draft.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                     iPODraftService.Update(Draft);


                    var Currency = ipoCurrencyMappingService.GetAll().Where(x => x.IPODeclarationId == Convert.ToInt32(IpoDeclarationId) && x.CurrencyId == Convert.ToInt32(CurrencyId)).FirstOrDefault();
                    decimal InvAmount = 0;
                    decimal JoinAmount = 0;

                    var InvReq = (Currency.LotAmount / IPODec.Lot) * Convert.ToInt32(InvestorShareQty);

                    if (Convert.ToDecimal(Amount) > InvReq)
                    {
                        JoinAmount = Convert.ToDecimal(Amount) - InvReq;
                        InvAmount = InvReq;
                    }
                    else
                    {
                        JoinAmount = 0;
                        InvAmount = InvReq;
                    }

                    //Investor  EDIT

                    var Ipo = iPOApplicationService.GetAll().Where(x => x.DraftId == Convert.ToInt32(NRBDraftId) && x.InvestorId == Convert.ToInt32(InvestorId)).FirstOrDefault();
                   // Ipo.IPODeclarationId = Convert.ToInt32(IpoDeclarationId);
                    if (Ipo != null)
                   {
                       if (InvestorShareQty != null && InvestorShareQty != "")
                           Ipo.ShareQuantity = Convert.ToInt32(InvestorShareQty);
                       Ipo.Lot = 1;
                       Ipo.CurrencyId = Convert.ToInt32(CurrencyId);
                       Ipo.UnitPrice = Convert.ToDecimal(InvAmount);
                       Ipo.AppliedAmount = Convert.ToDecimal(InvAmount);
                       Ipo.UpdateDate = DateTime.Now;
                       Ipo.UpdateUserId = SessionHelper.LoggedInUserId;
                       Ipo.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                       Ipo.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                       iPOApplicationService.Update(Ipo);
                   }
                    

                    //Join Holder EDIT

                    if (JoinHolderId != "0" && JoinHolderId != null && JoinHolderId != "")
                    {
                        var IpoJoin = iPOApplicationService.GetAll().Where(x => x.DraftId == Convert.ToInt32(NRBDraftId) && x.InvestorId == Convert.ToInt32(JoinHolderId)).FirstOrDefault();
                        if (IpoJoin != null)
                       {
                           if (JoinHolderShareQty != null && JoinHolderShareQty != "")
                               IpoJoin.ShareQuantity = Convert.ToInt32(JoinHolderShareQty);
                           IpoJoin.Lot = 1;
                           IpoJoin.UnitPrice = Convert.ToDecimal(JoinAmount);
                           IpoJoin.AppliedAmount = Convert.ToDecimal(JoinAmount);
                           IpoJoin.CurrencyId = Convert.ToInt32(CurrencyId);
                           IpoJoin.UpdateDate = DateTime.Now;
                           IpoJoin.UpdateUserId = SessionHelper.LoggedInUserId;
                           IpoJoin.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + JoinHolderId + ")").Tables[0].Rows[0][0]);
                           IpoJoin.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                           iPOApplicationService.Update(IpoJoin);
                       }
                    }
                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetInvestorListForeign(string InvestorCode, int IPODeclarationId)
        {
            try
            {
                List<AutoCompleteViewModel> InvestorList = new List<AutoCompleteViewModel>();

                var param = new { InvestorCode = InvestorCode, IPODeclarationId = IPODeclarationId };

                var empList = spService.GetDataWithParameter(param, "GetForeignInvestorListForAutoComplete");

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
        public JsonResult GetInvestorList(string InvestorCode,int IPODeclarationId)
        {
            try
            {
                List<AutoCompleteViewModel> InvestorList = new List<AutoCompleteViewModel>();

                var param = new { InvestorCode = InvestorCode, IPODeclarationId = IPODeclarationId };

                var empList = spService.GetDataWithParameter(param,"GetNRBInvestorListForAutoComplete");

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

        public void IPO_NRB_DraftReport(string reportNo, int IpoDeclarationId, int UserId, string FromDate, string ToDate, int BankId, int BankBranchId, int OrderBy, string exportType)
        {

            //-- IPODeclarationId ,FromDate ,ToDate ,CreatedUserId,BankBranchId,BankId,OrderBy
            var param = new { IPODeclarationId = IpoDeclarationId,
                              FromDate = ReportHelper.FormatDateToString(FromDate),
                              ToDate = ReportHelper.FormatDateToString(ToDate), 
                              CreatedUserId = UserId, 
                              BankBranchId = BankBranchId, 
                              BankId = BankId, 
                              OrderBy = OrderBy };
            var data = spService.GetDataWithParameter(param, "Get_IPO_DraftInformation").Tables[0];
            ReportHelper.ShowReport(data, exportType, "Rpt_Get_IPO_DraftInformation.rpt", "Rpt_Get_IPO_DraftInformation");


        }


        #endregion

        #region Action

        public ActionResult Index()
        {
            var IPOCon = DependencyResolver.Current.GetService<IPOController>();
            ViewBag.Companies = IPOCon.IPODeclarationCompany();
            ViewBag.Branches =
               brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.Currencies = lookupCurrencyService.GetAll().Where(x => x.CurrencyShortName.ToUpper() != "BDT").ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            return View();
        }
        public ActionResult DraftRpt()
        {
            //var IPOCon = DependencyResolver.Current.GetService<IPOController>();
            var declarations = ipoDeclarationService.GetAll().ToList();
            foreach (var declaration in declarations)
            {
                declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
            }
            ViewBag.Companies = declarations;
           // ViewBag.Companies = IPOCon.IPODeclarationCompany();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            return View();
        }
        public ActionResult Foreign()
        {
            var IPOCon = DependencyResolver.Current.GetService<IPOController>();
            ViewBag.Companies = IPOCon.IPODeclarationCompany();
            //ViewBag.Branches =
            //   brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.Currencies = lookupCurrencyService.GetAll().Where(x => x.CurrencyShortName.ToUpper() != "BDT").ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BankBranchlist"] = items;
            return View();
        }
        #endregion
      
       
    }
}
