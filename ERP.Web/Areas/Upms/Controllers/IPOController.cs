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
    public class IPOController : BaseController
    {
        #region Variables

        private readonly ICompanyInfoService companyInfoService;
        private readonly ISPService spService;
        private readonly IIPOIssueMethodService ipoIssueMethodService;
        private readonly ILookupCurrencyService lookupCurrencyService;
        private readonly IIPODeclarationService ipoDeclarationService;
        private readonly IIPOCurrencyMappingService ipoCurrencyMappingService;
        private readonly IInvestorTypeService investorTypeService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IIPOApplicationService iPOApplicationService;
        private readonly IInvestorWiseTransactionChargeService investorWiseTransactionChargeService;
        private readonly IInvestorTransactionDailyService investorTransactionDailyService;
        private readonly ITradeTransactionChargeService tradeTransactionChargeService;
        private readonly IInvestorBalanceDailyService investorBalanceDailyService;
        private readonly IIPODraftService iPODraftService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerInfoService brokerInfoService;
        private readonly IIPOGroupMasterService iPOGroupMasterService;
        private readonly IInvestorAccountService investorAccountService;

        public IPOController(ICompanyInfoService companyInfoService, ISPService spService, IIPOIssueMethodService ipoIssueMethodService, ILookupCurrencyService lookupCurrencyService
                , IIPODeclarationService ipoDeclarationService, IIPOCurrencyMappingService ipoCurrencyMappingService
                , IInvestorTypeService investorTypeService, IBrokerBranchService brokerBranchService,
                IIPOApplicationService iPOApplicationService, IInvestorWiseTransactionChargeService investorWiseTransactionChargeService,
                IInvestorTransactionDailyService investorTransactionDailyService, ITradeTransactionChargeService tradeTransactionChargeService,
                IInvestorBalanceDailyService investorBalanceDailyService, IIPODraftService iPODraftService, IMarketInfoService marketInfoService
            , IBrokerInfoService brokerInfoService, IIPOGroupMasterService iPOGroupMasterService, IInvestorAccountService investorAccountService)
        {
            this.companyInfoService = companyInfoService;
            this.spService = spService;
            this.ipoIssueMethodService = ipoIssueMethodService;
            this.lookupCurrencyService = lookupCurrencyService;
            this.ipoDeclarationService = ipoDeclarationService;
            this.ipoCurrencyMappingService = ipoCurrencyMappingService;
            this.investorTypeService = investorTypeService;
            this.brokerBranchService = brokerBranchService;
            this.iPOApplicationService = iPOApplicationService;
            this.investorWiseTransactionChargeService = investorWiseTransactionChargeService;
            this.investorTransactionDailyService = investorTransactionDailyService;
            this.tradeTransactionChargeService = tradeTransactionChargeService;
            this.investorBalanceDailyService = investorBalanceDailyService;
            this.iPODraftService = iPODraftService;
            this.marketInfoService = marketInfoService;
            this.brokerInfoService = brokerInfoService;
            this.iPOGroupMasterService = iPOGroupMasterService;
            this.investorAccountService = investorAccountService;


        }

        #endregion


        #region Methods

        public JsonResult EditShareQuantity(string IpoApplicationId, string ShareQty, string AppAmount, string InvestorType)
        {
            var Result = string.Empty;
            try
            {
                var IPOApp = iPOApplicationService.GetById(Convert.ToInt32(IpoApplicationId));

                IPOApp.ShareQuantity = Convert.ToInt32(ShareQty);
                IPOApp.AppliedAmount = Convert.ToDecimal(AppAmount);
                IPOApp.UpdateDate = DateTime.Now;
                IPOApp.UpdateUserId = SessionHelper.LoggedInUserId;
                var DraftId = IPOApp.DraftId;
                var InvestorID = IPOApp.InvestorId;
                iPOApplicationService.Update(IPOApp);

                if (InvestorType == "NRB" && DraftId != null)
                {
                    var Draft = iPODraftService.GetById(Convert.ToInt32(DraftId));
                    if (Draft.InvestorId == InvestorID)
                    {
                        Draft.InvestorShareQty = Convert.ToInt32(ShareQty);
                    }
                    else
                    {
                        Draft.JoinShareQty = Convert.ToInt32(ShareQty);
                    }
                    iPODraftService.Update(Draft);
                }

                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeletelIPOOrder(string IpoApplicationId)
        {
            var Result = string.Empty;
            try
            {
                var IPOApp = iPOApplicationService.GetById(Convert.ToInt32(IpoApplicationId));

                IPOApp.IsActive = false;
                IPOApp.UpdateDate = DateTime.Now;
                IPOApp.UpdateUserId = SessionHelper.LoggedInUserId;
                iPOApplicationService.Update(IPOApp);

                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CancelIPOOrder(string IpoApplicationId)
        {
            var Result = string.Empty;
            try
            {
                var IPOApp = iPOApplicationService.GetById(Convert.ToInt32(IpoApplicationId));

                IPOApp.OrderStatus = false;
                IPOApp.OrderCancelDate = DateTime.Now;
                IPOApp.OrderCancelUserId = SessionHelper.LoggedInUserId;
                iPOApplicationService.Update(IPOApp);

                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GETIPOOrderInvestorWithCurrentBalance(string BranchId, string InvestorTypeId, string Investor_CODE, string IpoDeclarationId, int GroupId)
        {
            try
            {
                var param = new { BRANCH_ID = Convert.ToInt32(BranchId), INVESTOR_TYPE_ID = Convert.ToInt32(InvestorTypeId), INVESTOR_CODE = Investor_CODE, IpoDeclarationId = Convert.ToInt32(IpoDeclarationId), CreateUserId = SessionHelper.LoggedInUserId, RoleId = SessionHelper.LoggedIn_RoleId, GroupId = GroupId };
                var InvList = spService.GetDataWithParameter(param, "SP_GET_IPO_Order_Investor_With_Current_Balance_Modified_IPO_Immature_Bal");
                var IpoInvestorList = InvList.Tables[0].AsEnumerable()
                     .Select(
                         x =>
                             new 
                             {
                                 IpoApplicationId = x.Field<int>("IpoApplicationId"),
                                 InvestorId = x.Field<int>("InvestorId"),
                                 InvestorCode = x.Field<string>("InvestorCode"),
                                 InvestorName = x.Field<string>("InvestorName"),
                                 InvestorType = x.Field<string>("InvestorTypeShortName"),
                                 AccountType = x.Field<string>("AccountTypeName"),
                                 Balance = x.Field<decimal>("CurrentBalance"),
                                 ShareQuantity = x.Field<int>("ShareQuantity"),
                                 AppliedAmount = x.Field<decimal>("AppliedAmount"),
                                 CurrencyId = x.Field<int>("CurrencyId"),
                                 UserName = x.Field<string>("UserName"),
                                 UserCode = x.Field<string>("UserCode"),
                             }).ToList();
                return Json(IpoInvestorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IPOApplicationSave(string idList, int companyId, int marketId)
        {
            try
            {
                spService.ExecuteStoredProcedure(
                    new { APPLICATION_ID = idList, USER_ID = SessionHelper.LoggedInUserId, COMPANY_ID = companyId, MARKET_ID = marketId, TRANSACTION_DATE = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), BrokerBranchId = SessionHelper.LoggedInOfficeId },
                    "SP_PROCESS_IPO_APPLICATION");
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult IPOOrderSave(List<string> AllInvestorIds, List<int> InvestorTypeIds, string DeclarationId, string ShareQuantity, string totAmount,int GroupId)
        {
            var Result = string.Empty;
            try
            {
                var Declaration = ipoDeclarationService.GetById(Convert.ToInt32(DeclarationId));

                decimal? unitPrice = (Declaration.FaceValue + Declaration.Premium) - Declaration.Discount;
                var Index = 0;
                foreach (var app in AllInvestorIds)
                {
                    var Ipo = new IPOApplication();
                    Ipo.IPODeclarationId = Convert.ToInt32(DeclarationId);
                    Ipo.InvestorId = Convert.ToInt32(app);
                    Ipo.ShareQuantity = Convert.ToInt32(ShareQuantity);
                    Ipo.Lot = 1;
                    Ipo.UnitPrice = Convert.ToDecimal(unitPrice);
                    Ipo.AppliedAmount = Convert.ToDecimal(totAmount);
                    Ipo.OrderStatus = true;
                    Ipo.CurrencyId = 1;
                    Ipo.InvestorTypeId = Convert.ToInt32(InvestorTypeIds[Index]);
                    if (GroupId != 0)
                    {
                        Ipo.IpoGroupId = GroupId;
                    }
                    else
                    {
                        var Group_Id = spService.GetDataBySqlCommand("SELECT  M.Id FROM IPOGroupMaster AS M INNER JOIN IPOGroupDetails AS D ON D.IPOGroupMasterId = M.Id WHERE D.InvestorId = " + app + " AND M.IsActive = 1 AND D.IsActive = 1").Tables[0];

                        if (Group_Id.Rows.Count != 0)
                        {
                            Ipo.IpoGroupId = Convert.ToInt32(Group_Id.Rows[0][0]);
                        }
                        
                        //SELECT TOP 1 M.Id FROM IPOGroupMaster AS M INNER JOIN IPOGroupDetails AS D ON D.IPOGroupMasterId = M.Id WHERE D.InvestorId = 555 AND M.IsActive = 1 AND D.IsActive = 1
                    }
                  
                    Ipo.OrderDate = SessionHelper.TransactionDate;
                    Ipo.OrderUserId = SessionHelper.LoggedInUserId;
                    Ipo.CreateDate = DateTime.Now;
                    Ipo.CreatedUserId = SessionHelper.LoggedInUserId;
                    Ipo.IsActive = true;
                    Ipo.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    Ipo.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + app + ")").Tables[0].Rows[0][0]);
                    iPOApplicationService.Create(Ipo);
                    Index = Index + 1;
                }
                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public void Rpt_IPO_OrderApplication(string reportNo, int IpoDeclarationId, int BrokerBranch, int UserId, string FromDate, string ToDate, string exportType, int InvestorType, int IsGroupWise, int GroupId)
        {

            if (reportNo == "1") //IPO Declaration Information
            {
                var param = new { IpoDeclarationId = IpoDeclarationId };//Exec Rpt_IPODeclaration 33
                var data = spService.GetDataWithParameter(param, "Rpt_IPODeclaration").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_IPODeclaration.rpt", "rpt_IPODeclaration");
            }
            else if (reportNo == "2" || reportNo == "3") // IPO Order ,IPO Application
            {
                if (IsGroupWise == 1) // no groupwise
                {
                    var param = new
                    {
                        Qtype = reportNo,
                        IpoDeclarationId = IpoDeclarationId,
                        BrokerBranch = BrokerBranch,
                        UserId = UserId,
                        FirstDate = ReportHelper.FormatDateToString(FromDate),
                        EndDate = ReportHelper.FormatDateToString(ToDate),
                        InvestorType = InvestorType
                    };
                    if (BrokerBranch == 0 && UserId == 0 && FromDate == "" && ToDate == "") //All
                    {
                        var data = spService.GetDataWithParameter(param, "Rpt_IPO_OrderApplication").Tables[0];
                        ReportHelper.ShowReport(data, exportType, "RptIPOOrderApplicationAll.rpt", "RptIPOOrderApplicationAll");
                    }
                    else
                    {
                        var data = spService.GetDataWithParameter(param, "Rpt_IPO_OrderApplication").Tables[0];
                        ReportHelper.ShowReport(data, exportType, "RptIPOOrderApplication.rpt", "RptIPOOrderApplication");
                    }
                }
                else  ////  groupwise
                {
                    var param = new
                    {
                        Qtype = reportNo,
                        IpoDeclarationId = IpoDeclarationId,
                        BrokerBranch = BrokerBranch,
                        UserId = UserId,
                        FirstDate = ReportHelper.FormatDateToString(FromDate),
                        EndDate = ReportHelper.FormatDateToString(ToDate),
                        InvestorType = InvestorType,
                        GroupId = GroupId
                    };
                    var data = spService.GetDataWithParameter(param, "Rpt_IPO_OrderApplication_Groupwise").Tables[0];
                    ReportHelper.ShowReport(data, exportType, "RptIPOOrderApplication_Groupwise.rpt", "RptIPOOrderApplication_Groupwise");
                    
                }
               
            }
            else if (reportNo == "4")//IPO Order Summary (PDF)
            {
                var rptCon = DependencyResolver.Current.GetService<ReportController>();
                rptCon.ShowIPOApplicationReport(IpoDeclarationId, 1, 1);
            }
            else if (reportNo == "5") //IPO Summary (Allotment & Refund)
            {
                var param = new { IpoDeclarationId = IpoDeclarationId };
                var data = spService.GetDataWithParameter(param, "Rpt_IPO_SummaryReport").Tables[0];
                ReportHelper.ShowReport(data, exportType, "RptIPOSummaryReport.rpt", "RptIPOSummaryReport");
            }
        }
        public void Rpt_Ipo_ApplicationForm(string reportNo, int IpoDeclarationId, int BranchId, int InvestorTypeType, string InvestorCode, int ReportType, int GroupId)
        { //BranchId InvestorTypeType InvestorCode IpoDeclarationId
            var exportType = "pdf";
            if (ReportType == 1)
            {
                var param = new { IpoDeclarationId = IpoDeclarationId, BranchId = BranchId, InvestorTypeId = InvestorTypeType, InvestorCode = InvestorCode };
                var data = spService.GetDataWithParameter(param, "GetIpoApplicationInfo").Tables[0];
                ReportHelper.ShowReport(data, exportType, "Rpt_Ipo_ApplicationForm.rpt", "Rpt_Ipo_ApplicationForm");
            }
             else
            {
                var param = new { GroupId = GroupId ,IpoDeclarationId = IpoDeclarationId};
                var data = spService.GetDataWithParameter(param, "Get_Groupwise_IpoApplicationInfo").Tables[0];
                ReportHelper.ShowReport(data, exportType, "Rpt_Groupwise_IpoApplicationInfo.rpt", "Rpt_Groupwise_IpoApplicationInfo");
            }
        }
        public List<CompanyInformation> CompanyNameForIpoDeclaration()
        {
            var empList = spService.GetDataWithoutParameter("GetCompanyNameForIpoDeclaration");

            var companies = empList.Tables[0].AsEnumerable()
            .Select(row => new CompanyInformation
                {
                    Id = row.Field<int>("Id"),
                    CompanyName = row.Field<string>("CompanyName"),
                }).ToList();


            return companies;
        }


        public List<IPODeclaration> IPODeclarationCompany()
        {
            var IpoCompany = ipoDeclarationService.GetAll().Where(x => x.Status != "C").ToList();
            if (IpoCompany.Count > 0)
            {
                foreach (var app in IpoCompany)
                {
                    app.CompanyName = companyInfoService.GetById(app.CompanyId).CompanyName;
                }
            }
            return IpoCompany;
        }
        public List<IPODeclaration> IPODeclarationCompanyAll()
        {
            var IpoCompany = ipoDeclarationService.GetAll().OrderByDescending(x=>x.Id).ToList();
            if (IpoCompany.Count > 0)
            {
                foreach (var app in IpoCompany)
                {
                    app.CompanyName = companyInfoService.GetById(app.CompanyId).CompanyName;
                }
            }
            return IpoCompany;
        }



        public JsonResult GetInvestorType(int IssueMethodId)
        {
            try
            {
                // List<GLViewMode> GLViewModeList = new List<GLViewMode>();
                var param = new { IssueMethodId = IssueMethodId };
                var InvestorType = spService.GetDataWithParameter(param, "GetIPOIssueMethodWiseInvestorType");

                var IpoInvestorType = InvestorType.Tables[0].AsEnumerable()
                .Select(x =>
                            new
                            {
                                Id = x.Field<int>("Id"),
                                InvestorTypeShortName = x.Field<string>("InvestorTypeShortName"),
                                InvestorTypeName = x.Field<string>("InvestorTypeName"),
                            });
                return Json(IpoInvestorType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetDeclarationDetails(int declarationId)
        {
            var declaration = ipoDeclarationService.GetById(declarationId);
            declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
            var issueId = declaration.IssueMethodId ?? (declaration.Premium > 0 ? 2 : 1);
            declaration.IssueMethod = ipoIssueMethodService.GetById(issueId).IssueMethodName;
            var currency = ipoCurrencyMappingService.GetAllByDeclarionId(declarationId).ToList();
            return Json(new { Declaration = declaration, Currency = currency }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeclarationInfoForNrb(int DeclarationId)
        {
            var declaration = ipoDeclarationService.GetById(DeclarationId);
            var currency = ipoCurrencyMappingService.GetAllByDeclarionId(DeclarationId).ToList();
            return Json(new { Declaration = declaration, Currency = currency }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeclarationByCompany(int DeclarationId)
        {
            var declaration = ipoDeclarationService.GetById(DeclarationId);//GetByCompanyId(companyId);
            return Json(declaration, JsonRequestBehavior.AllowGet);
        }

        public JsonResult validGroupLeader( int GroupId)
        {
            try
            {
                var Status = "";
                var Leader = iPOGroupMasterService.GetAll().Where(x => x.Id == GroupId && x.IsActive == true).FirstOrDefault();
                if (Leader != null)
                {
                    var isCoe = investorAccountService.GetAll().Where(x => x.InvestorId == Leader.LeaderId).FirstOrDefault().StatusId;
                    if (isCoe == 3)
                    {
                        Status = "Closed";
                    }
                    else
                    {
                        Status = "ActiveGroup";
                    }
                    return Json(new { Result = "SUCCESS", Message = "", Status = Status }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Invalid Group", Status = "" }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message, Status = "" }, JsonRequestBehavior.AllowGet);
            }
        }





        public JsonResult GetInvestorInfoWithBalance(int branchId, int investorTypeId, string code,int IpoDeclarationId, int GroupId)
        {


            //var arrCode = code.Split('-');
            //var fromCode = arrCode[0];
            //var toCode = "";
            //if (arrCode.Length == 2)
            //    toCode = arrCode[1];
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        BRANCH_ID = branchId,
                        INVESTOR_TYPE_ID = investorTypeId,
                        INVESTOR_CODE = code,
                        IpoDeclarationId = IpoDeclarationId,
                        GroupId = GroupId

                    //}, "SP_GET_INVESTOR_WITH_CURRENT_BALANCE_EX").Tables[0]
                    }, "SP_GET_INVESTOR_WITH_CURRENT_BALANCE_EX_IPO_Immature_Bal").Tables[0]
                    

                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSl = x.Field<string>(0),
                                InvestorId = x.Field<int>(1),
                                InvestorCode = x.Field<string>(2),
                                BOID = x.Field<string>(3),
                                InvestorName = x.Field<string>(4),
                                InvestorTypeId = x.Field<int>(5),
                                InvestorType = x.Field<string>(6),
                                AccountType = x.Field<string>(7),
                                Balance = x.Field<decimal>(8),
                                Total = x.Field<int>(9),
                                AppliedStatus = x.Field<string>(10)
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public JsonResult IPODeclaration(IPODeclaration declaration, List<IPOCurrencyMapping> currency)
        {
            var message = "";
            if (currency == null)
                return Json(new { Status = false, Message = "Please enter all currency conversion rate." },
                    JsonRequestBehavior.AllowGet);
            if (currency.Count == 0)
                return Json(new { Status = false, Message = "Please enter all currency conversion rate." },
                    JsonRequestBehavior.AllowGet);

            if (declaration.ProspectusIssueDate.HasValue)
            {
                declaration.LockUptoThreeMonthDate = ((DateTime)declaration.ProspectusIssueDate).AddMonths(3);
                declaration.LockUptoSixMonthDate = ((DateTime)declaration.ProspectusIssueDate).AddMonths(6);
            }

            if (declaration.Id == 0)
            {
                var exist =
                    ipoDeclarationService.GetAll()
                        .FirstOrDefault(x => x.CompanyId == declaration.CompanyId && x.IsIPORPO == declaration.IsIPORPO);
                if (exist != null)
                    return Json(new { Status = false, Message = "This Company already in ipo." },
                        JsonRequestBehavior.AllowGet);

                declaration.Status = "P";
                declaration.TradingCode = companyInfoService.GetById(declaration.CompanyId).TradeIdDSE;
                declaration.CreateDate = DateTime.Now;
                declaration.CreatedUserId = SessionHelper.LoggedInUserId;
                declaration.IsActive = true;
                declaration.ClosingStatus = false;
                declaration.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                ipoDeclarationService.Create(declaration);
                message = "IPO Declaration Successfull.";
            }
            else
            {
                var dec = ipoDeclarationService.GetById(declaration.Id);
                dec.UpdateDate = DateTime.Now;
                dec.FaceValue = declaration.FaceValue;
                dec.UpdateUserId = SessionHelper.LoggedInUserId;
                dec.IsIPORPO = declaration.IsIPORPO;
                dec.LockUptoSixMonthDate = declaration.LockUptoSixMonthDate;
                dec.LockUptoThreeMonthDate = declaration.LockUptoThreeMonthDate;
                dec.Lot = declaration.Lot;
                dec.NRBApplicationLastDate = declaration.NRBApplicationLastDate;
                dec.Premium = declaration.Premium;
                dec.ProspectusIssueDate = declaration.ProspectusIssueDate;
                dec.ResultDownloadDate = declaration.ResultDownloadDate;
                dec.ApplicationEndDate = declaration.ApplicationEndDate;
                dec.ApplicationStartDate = declaration.ApplicationStartDate;
                dec.BankLastDate = declaration.BankLastDate;
                dec.DataUploadLastDate = declaration.DataUploadLastDate;
                dec.Discount = declaration.Discount;
                dec.MarketId = declaration.MarketId;
                dec.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                ipoDeclarationService.Update(dec);
                ipoCurrencyMappingService.DeleteByIPODeclarationId(declaration.Id);
                message = "IPO Declaration information updated successfully.";
            }
            foreach (var currencyMapping in currency)
            {
                currencyMapping.IPODeclarationId = declaration.Id;
                currencyMapping.CreateDate = DateTime.Now;
                currencyMapping.CreatedUserId = SessionHelper.LoggedInUserId;
                currencyMapping.IsActive = true;
            }
            ipoCurrencyMappingService.CreateRange(currency);

            return Json(new { Status = true, Message = message }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetBrokerEmoloyeeList(string EmployeeCode)
        {
            try
            {
                var param = new { EmployeeCode = EmployeeCode };

                var empList = spService.GetDataWithParameter(param, "GetBrokerEmoloyeeListForAutoComplete");

                var EmployeeList = empList.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     EmployeeName = row.Field<string>("EmployeeName"),
                 }).ToList();
                return Json(EmployeeList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        #endregion


        #region Actions

        public ActionResult Declaration()
        {
            ViewBag.Companies = CompanyNameForIpoDeclaration();//companyInfoService.GetAll().ToList();
            ViewBag.Currencies = lookupCurrencyService.GetAll().Where(x => x.CurrencyShortName.ToUpper() != "BDT").ToList();
            ViewBag.IssueMethods = ipoIssueMethodService.GetAll().ToList();
            ViewBag.MarketInformation = marketInfoService.GetAll().ToList();

            ViewData["IsDSEPrimary"] = "";
            if (SessionHelper.IsDSEPrimary == true)
            {
                ViewData["IsDSEPrimary"] = "Yes";
            }
            else
            {
                ViewData["IsDSEPrimary"] = "No";
            }
           
            var declarationList = ipoDeclarationService.GetAll().Where(x => x.ClosingStatus == false && x.Status != "C").ToList();
            foreach (var declaration in declarationList)
            {
                declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
                var issueId = declaration.IssueMethodId ?? (declaration.Premium > 0 ? 2 : 1);
                declaration.IssueMethod = ipoIssueMethodService.GetById(issueId).IssueMethodName;
            }
            return View(declarationList);
        }
        public ActionResult PlaceOrder()
        {

            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["InvestorTypeList"] = items;
            ViewBag.Branches =
                brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            // ViewBag.InvestorTypes = investorTypeService.GetAll().Where(x=>x.Id != 3).OrderBy(x => x.InvestorTypeName).ToList(); // 3 = NRB
            ViewBag.Companies = IPODeclarationCompany();
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            return View();
        }

        public ActionResult IpoApp()
        {
            ViewBag.Companies = IPODeclarationCompany();
            ViewBag.Branches =
               brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.InvestorTypes = investorTypeService.GetAll().OrderBy(x => x.InvestorTypeOrder).ToList();
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            return View();
        }
        public ActionResult IpoAppOperator()
        {
            ViewBag.Companies = IPODeclarationCompany();
            ViewBag.Branches =
               brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.InvestorTypes = investorTypeService.GetAll().OrderBy(x => x.InvestorTypeOrder).ToList();
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            return View();
        }
        public ActionResult IpoAppReport()
        {

            ViewBag.Companies = IPODeclarationCompanyAll();
            ViewBag.Branches =
               brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.InvestorTypes = investorTypeService.GetAll().OrderBy(x => x.InvestorTypeOrder).ToList();
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            return View();
        }
        public ActionResult IpoRpt()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("IPO", "IpoRpt");
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            ViewBag.Reports = spService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                ReportModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            {
                Id = x.Field<int>(0),
                ReportName = x.Field<string>(1),
                SerialNo = x.Field<int>(2)
            }).ToList();
            //ViewBag.Companies = IPODeclarationCompany();
            var declarations = ipoDeclarationService.GetAll().ToList();
            foreach (var declaration in declarations)
            {
                declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
            }
            ViewBag.Companies = declarations;
            ViewBag.Branches =
               brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.InvestorTypes = investorTypeService.GetAll().OrderBy(x => x.InvestorTypeOrder).ToList();
            return View();
        }
        public ActionResult IpoAllocation()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("IPO", "IpoAllocation");
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.Reports = spService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                ReportModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            {
                Id = x.Field<int>(0),
                ReportName = x.Field<string>(1),
                SerialNo = x.Field<int>(2)
            }).ToList();
            var declarations = ipoDeclarationService.GetAll().ToList();
            foreach (var declaration in declarations)
            {
                declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
            }
            ViewBag.Companies = declarations;
            ViewBag.Branches =
               brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.InvestorTypes = investorTypeService.GetAll().OrderBy(x => x.InvestorTypeOrder).ToList();
            return View();
        }
       
        #endregion

    }
}