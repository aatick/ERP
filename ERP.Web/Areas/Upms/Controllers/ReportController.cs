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
using Upms.Service;

namespace Upms.Controllers
{
    public class ReportController : BaseController
    {
        #region Variables
        private readonly ISPService spService;
        private readonly ICompanyInfoService companyInfoService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService;
        private readonly IIPODeclarationService ipoDeclarationService;
        private readonly IBrokerInfoService brokerInfoService;
        private readonly IInvestorStatusService investorStatusService;
        private readonly IInvestorInfoService investorInfoService;
        private readonly IBrokerEmployeeService brokerEmployeeService;
        public ReportController(
                            ISPService spService, IMarketInfoService marketInfoService
                            , ICompanyInfoService companyInfoService, IBrokerBranchService brokerBranchService
                            , IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService
                            , IIPODeclarationService ipoDeclarationService, IBrokerInfoService brokerInfoService
                            , IInvestorStatusService investorStatusService, IInvestorInfoService investorInfoService
                            , IBrokerEmployeeService brokerEmployeeService
                    )
        {
            this.spService = spService;
            this.companyInfoService = companyInfoService;
            this.brokerBranchService = brokerBranchService;
            this.marketInfoService = marketInfoService;
            this.brokerDepositoryParticipatoryService = brokerDepositoryParticipatoryService;
            this.ipoDeclarationService = ipoDeclarationService;
            this.brokerInfoService = brokerInfoService;
            this.investorStatusService = investorStatusService;
            this.investorInfoService = investorInfoService;
            this.brokerEmployeeService = brokerEmployeeService;
        }
        #endregion

        public ActionResult PayInPayOut()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("Report", "PayInPayOut");
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
            ViewBag.Companies = companyInfoService.GetAll().OrderBy(x => x.CompanyName).ToList();
            ViewBag.Branch = brokerBranchService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Market = marketInfoService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.DP = brokerDepositoryParticipatoryService.GetAll().Where(x => x.IsActive).ToList();
            return View();
        }

        public void ShowPayInPayOutReport(string fromDate, string toDate, int companyId, int branchId, int marketId, int isPayIn, string type, int dpId, int isClientWise)
        {
            var from = ReportHelper.FormatDate(fromDate);
            var to = ReportHelper.FormatDate(toDate);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        COMPANY_ID = companyId,
                        BRANCH_ID = branchId,
                        MARKET_ID = marketId,
                        TRANS_DATE_FROM = from,
                        TRANS_DATE_TO = to,
                        DP_ID = dpId
                    },
                    (isPayIn == 1 ? "SP_RPT_CDBL_PAY_IN" : "SP_RPT_CDBL_PAY_OUT")).Tables[0];
            var param = new Dictionary<string, object> { { "isPayIn", isPayIn }, { "isClientWise", isClientWise } };
            ReportHelper.ShowReport(data, type, "rptPayInPayOut.rpt", (isPayIn == 1 ? "PayIn" : "PayOut") + "_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }

        public ActionResult Stock()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("Report", "Stock");
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
            ViewBag.Companies = companyInfoService.GetAll().OrderBy(x => x.CompanyShortName).ToList();
            ViewBag.Branch = brokerBranchService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.DP = brokerDepositoryParticipatoryService.GetAll().Where(x => x.IsActive).ToList();
            return View();
        }
        public void ShowInstrumentWiseStockReport(string date, int branchId, string type, int dpId, int companyId, string code)
        {
            var trxdate = ReportHelper.FormatDate(date);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = trxdate,
                        BRANCH_ID = branchId,
                        DP_ID = dpId,
                        COMPANY_ID = companyId,
                        INVESTOR_CODE = code
                    },
                    "SP_RPT_STOCK_INSTRUMENT_WISE").Tables[0];
            ReportHelper.ShowReport(data, type, "rptStockInstrumentWise.rpt", "StockInstrumentWise_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }

        public void Rpt_IPO_Allocation(string reportNo, int IpoDeclarationId, int BrokerBranch, string FromDate, string exportType, int InvestorType, string group = "")
        {
            var param = new { Qtype = reportNo, IpoDeclarationId = IpoDeclarationId, BrokerBranch = BrokerBranch, InvestorTypeId = InvestorType, FirstDate = ReportHelper.FormatDateToString(FromDate), GroupCode = group };

            var data = spService.GetDataWithParameter(param, "Rpt_IPO_AllocationRefundFine").Tables[0];
            if (reportNo == "1" || reportNo == "5")
            {
                ReportHelper.ShowReport(data, exportType, "RptIPOAllocationRefund.rpt", "RptIPOAllocationRefund");
            }
            else if (reportNo == "2")
            {
                ReportHelper.ShowReport(data, exportType, "RptIPOAllocation.rpt", "RptIPOAllocation");
            }
            else if (reportNo == "3")
            {
                ReportHelper.ShowReport(data, exportType, "RptIPORefund.rpt", "RptIPORefund");
            }
            else
            {
                ReportHelper.ShowReport(data, exportType, "RptIPOFineAmount.rpt", "RptIPOFineAmount");
            }
        }

        public void InvestorInterest(int Qtype, int InvestorId, string FirstDate, string EndDate, int Process)
        {
            var exportType = "pdf";

            if (Qtype == 1)
            {
                var param = new { Qtype = 1, InvestorId = InvestorId, FirstDate = ReportHelper.FormatDateToString(FirstDate), EndDate = ReportHelper.FormatDateToString(EndDate), IsProcessed = Process };

                var data = spService.GetDataWithParameter(param, "Rpt_Investor_Interest").Tables[0];
                ReportHelper.ShowReport(data, exportType, "RptInvestorInterestReport.rpt", "RptInvestorInterestReport");
            }
            else if (Qtype == 2)
            {
                var param = new { Qtype = 2, InvestorId = InvestorId, FirstDate = ReportHelper.FormatDateToString(FirstDate), EndDate = ReportHelper.FormatDateToString(EndDate), IsProcessed = Process };

                var data = spService.GetDataWithParameter(param, "Rpt_Investor_Interest").Tables[0];
                ReportHelper.ShowReport(data, exportType, "RptInvestorInterestReportAfterProcess.rpt", "RptInvestorInterestReportAfterProcess");
            }
            else
            {
                var param = new { Qtype = 1, InvestorId = InvestorId, FirstDate = ReportHelper.FormatDateToString(FirstDate), EndDate = ReportHelper.FormatDateToString(EndDate), IsProcessed = Process };

                var data = spService.GetDataWithParameter(param, "Rpt_Investor_Interest").Tables[0];
                ReportHelper.ShowReport(data, exportType, "RptInvestorInterestReportDatewiseSummary.rpt", "RptInvestorInterestReportDatewiseSummary");
            }

        }


        public void InvestorCompanywiseGain(int InvestorId, int CompanyId, string FirstDate, string EndDate, int ddlReportview)
        {
            var exportType = "pdf";

            var param = new { InvestorId = InvestorId, CompanyId = CompanyId, FirstDate = ReportHelper.FormatDateToString(FirstDate), EndDate = ReportHelper.FormatDateToString(EndDate) };

            var data = spService.GetDataWithParameter(param, "Rpt_Investor_CompanywiseGain").Tables[0];
            if (ddlReportview == 0)
            {
                ReportHelper.ShowReport(data, exportType, "RptInvestorCapitalGain.rpt", "RptInvestorCapitalGain");
            }
            else
            {
                ReportHelper.ShowReport(data, exportType, "RptInvestorCapitalGainSummary.rpt", "RptInvestorCapitalGainSummary");
            }
        }
        public void GetInvestorFundTransfer(int InvestorId, string FirstDate, string EndDate, int Status, int UserId, string RptGroupwise)
        {
            var exportType = "pdf";

            var param = new { TransDateFrom = ReportHelper.FormatDateToString(FirstDate), TransDateTo = ReportHelper.FormatDateToString(EndDate), GroupLeaderId = InvestorId, Status = Status, UserId = UserId };

            var data = spService.GetDataWithParameter(param, "GetInvestorFundTransferRPT").Tables[0];
            if (InvestorId == 0)
            {
                ReportHelper.ShowReport(data, exportType, "rptInvestorFundTrasferInformation.rpt", "rptInvestorFundTrasferInformation");
            }
            else
            {
                if (RptGroupwise == "LM")
                {
                    ReportHelper.ShowReport(data, exportType, "rptInvestorFundTrasferInformationLeaderwise.rpt", "rptInvestorFundTrasferInformationLeaderwise");
                }
                else
                {
                    ReportHelper.ShowReport(data, exportType, "rptInvestorFundTrasferInformationReceiver.rpt", "rptInvestorFundTrasferInformationReceiver");
                }
            }
        }


        public void ShowShareStockReport(string date, int branchId, string type, int dpId, int companyId, string code, int isClientWise)
        {
            var trxdate = ReportHelper.FormatDate(date);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = trxdate,
                        BRANCH_ID = branchId,
                        DP_ID = dpId,
                        COMPANY_ID = companyId,
                        INVESTOR_CODE = code
                    },
                    "SP_RPT_SHARE_STOCK").Tables[0];
            var param = new Dictionary<string, object> { { "isClientWise", isClientWise } };
            ReportHelper.ShowReport(data, type, "rptShareStock.rpt", "ShareStock_" + (isClientWise == 1 ? "Client_Wise" : "Instrument_Wise") + "_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }

        public ActionResult ClosingBalance()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.InvestorStatus = investorStatusService.GetAll();
            //var moduleid = spService.GetSecurityModuleByControllerAction("Report", "ClosingBalance");
            //ViewBag.Reports = spService.GetDataWithParameter(new
            //{
            //    USER_ID = SessionHelper.LoggedInUserId,
            //    ReportModuleId = moduleid
            //}, "SP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            //{
            //    Id = x.Field<int>(0),
            //    ReportName = x.Field<string>(1),
            //    SerialNo = x.Field<int>(2)
            //}).ToList();
            ViewBag.Branch = brokerBranchService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.DP = brokerDepositoryParticipatoryService.GetAll().Where(x => x.IsActive).ToList();
            return View();
        }

        public void ShowClosingBalanceReport(string date, int branchId, string type, int dpId, string code, int zeroShare, int condition, string balanceFrom, string balanceTo, int balanceType, int InvestorStatusId)
        {
            if (string.IsNullOrEmpty(balanceFrom))
            {
                balanceFrom = null;
            }
            if (string.IsNullOrEmpty(balanceTo))
            {
                balanceTo = null;
            }
            var trxdate = ReportHelper.FormatDate(date);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE = trxdate.ToString("dd MMM yyyy"),
                        BR_ID = branchId,
                        DP = dpId,
                        BL_TYPE = balanceType,
                        ZERO_SHARE = zeroShare,
                        CONDITION = condition,
                        BL_FROM = balanceFrom,
                        BL_TO = balanceTo,
                        INV_CODE = code,
                        InvestorStatusId = InvestorStatusId
                    },
                    "SP_RPT_CLOSING_BALANCE_INVESTOR_WISE").Tables[0];
            ReportHelper.ShowReport(data, type, "rptClosingBalance.rpt", "ClosingBalance_" + trxdate.ToString("dd_MMM_yyyy"));
        }

        public void ShowIPOApplicationReport(int declarationId, int isSummary, int isOrder = 0)
        {
            var data =
            spService.GetDataWithParameter(
                new
                {
                    IPO_DECLARATION_ID = declarationId,
                    IS_SUMMARY = isSummary,
                    IS_EXPORT = 0,
                    IS_NRB = 0,
                    FORMAT = "pdf",
                    IPO_ORDER = isOrder
                },
                "SP_EXPORT_IPO_FILE").Tables[0];
            var declaration = ipoDeclarationService.GetById(declarationId);
            var trecCode = brokerInfoService.GetAll().FirstOrDefault(x => x.IsActive).BrokerCode.PadLeft(3, '0');
            var companyCode = companyInfoService.GetById(declaration.CompanyId).TradeIdDSE;
            var exportFileName = "";
            var reportName = "";
            if (isSummary == 1)
            {
                exportFileName = companyCode + "_Summary_DSE_" + trecCode;
                reportName = "rptIPOApplicationSummary.rpt";
                var param = new Dictionary<string, object> { { "isOrder", isOrder } };
                ReportHelper.ShowReport(data, "Pdf", reportName, exportFileName, param);
            }
            else
            {
                exportFileName = companyCode + "_Detail_DSE_" + trecCode;
                reportName = "rptIPOApplicationDetail.rpt";
                ReportHelper.ShowReport(data, "Pdf", reportName, exportFileName);
            }

        }


        public ActionResult CapitalGain()
        {
            //var declarations = ipoDeclarationService.GetAll().ToList();
            //foreach (var declaration in declarations)
            //{
            //    declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
            //}
            ViewBag.Companies = companyInfoService.GetAll().Where(x => x.IsActive).OrderBy(x => x.CompanyName).ToList();
            return View();
        }

        public ActionResult Interest()
        {

            return View();
        }

        public ActionResult OverTrading()
        {

            return View();
        }
        public ActionResult TDayMatured()
        {

            return View();
        }
        public ActionResult ClientLedger()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("Report", "ClientLedger");
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
            return View();
        }
        public ActionResult InstrumentLedger()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("Report", "InstrumentLedger");
            ViewBag.Companies = companyInfoService.GetAll().Where(x => x.IsActive).OrderBy(x => x.TradeIdDSE).ToList();
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
            return View();
        }

        public void ShowClientLedger(string fromDate, string toDate, string type, string code, int isHawlaWise)
        {
            var trxdatefrom = ReportHelper.FormatDate(fromDate);
            var trxdateto = ReportHelper.FormatDate(toDate);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = trxdatefrom.ToString("dd MMM yyyy"),
                        TRANS_DATE_TO = trxdateto.ToString("dd MMM yyyy"),
                        INVESTOR_CODE = code,
                        IS_HAWLA_WISE = isHawlaWise
                    },
                    "SP_RPT_INVESTOR_LEDGER").Tables[0];
            ReportHelper.ShowReport(data, type, (isHawlaWise == 0 ? "rptInvestorLedger.rpt" : "rptInvestorLedgerHawlaWise.rpt"), (isHawlaWise == 0 ? "InvestorLedger_" : "InvestorLedgeHawlaWise_") + trxdatefrom.ToString("dd_MMM_yyyy") + "to_" + trxdateto.ToString("dd_MMM_yyyy") + "_" + code.Replace(",", "_"));
        }

        public JsonResult EmailClientLedger(string fromDate, string toDate, string type, string code, int isHawlaWise)
        {
            try
            {
                var trxdatefrom = ReportHelper.FormatDate(fromDate);
                var trxdateto = ReportHelper.FormatDate(toDate);
                var data =
                    spService.GetDataWithParameter(
                        new
                        {
                            TRANS_DATE_FROM = trxdatefrom.ToString("dd MMM yyyy"),
                            TRANS_DATE_TO = trxdateto.ToString("dd MMM yyyy"),
                            INVESTOR_CODE = code,
                            IS_HAWLA_WISE = isHawlaWise
                        },
                        "SP_RPT_INVESTOR_LEDGER").Tables[0];
                if (data.Rows.Count == 0)
                {
                    return Json(new { Status = false, Message = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                var investor = spService.GetDataWithParameter(new { INVESTOR_CODE = code }, "SP_GET_INVESTOR_EMAIL_ADDRESS").Tables[0];
                var email = investor.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { Status = false, Message = "This investor has no valid email address to email ledger." }, JsonRequestBehavior.AllowGet);
                }
                ReportHelper.EmailReport(email, "Ledger (" + (isHawlaWise == 0 ? "Summary" : "Details") + ") of " + investor.Rows[0][1], "Dear valued client, ledger report from " + trxdatefrom.ToString("dd MMM yyyy") + " to " + trxdateto.ToString("dd MMM yyyy") + ".Please check the attachment below."
                    , data, type,
                    (isHawlaWise == 0 ? "rptInvestorLedger.rpt" : "rptInvestorLedgerHawlaWise.rpt"),
                    (isHawlaWise == 0 ? "Ledger_" : "LedgeHawlaWise_") +
                    trxdatefrom.ToString("dd_MMM_yyyy") + "to_" + trxdateto.ToString("dd_MMM_yyyy") + "_" +
                    code.Replace(",", "_"));
                spService.ExecuteStoredProcedure(new
                {
                    INVESTOR_ID = int.Parse(investor.Rows[0][2].ToString()),
                    TYPE = "ELEDGER",
                    USER_ID = SessionHelper.LoggedInUserId
                }, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                return Json(new { Status = true, Message = "Ledger email successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ShowInstrumentLedger(string fromDate, string toDate, string type, string code, int isHawlaWise, int companyId, int isInvestorWise)
        {
            var trxdatefrom = ReportHelper.FormatDate(fromDate);
            var trxdateto = ReportHelper.FormatDate(toDate);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = trxdatefrom.ToString("dd MMM yyyy"),
                        TRANS_DATE_TO = trxdateto.ToString("dd MMM yyyy"),
                        INVESTOR_CODE = code,
                        IS_HAWLA_WISE = isHawlaWise,
                        COMPANY_ID = companyId,
                        IS_INVESTOR_WISE = isInvestorWise
                    },
                    "SP_RPT_INSTRUMENT_LEDGER").Tables[0];
            var reportName = "";
            var exportName = "";
            if (isHawlaWise == 1 && isInvestorWise == 1)
            {
                reportName = "rptInstrumentLedgerHawlaWiseInvestorWise.rpt";
                exportName = "InstrumentLedgerHawlaWise";
            }
            else if (isHawlaWise == 1 && isInvestorWise == 0)
            {
                reportName = "rptInstrumentLedgerHawlaWise.rpt";
                exportName = "InstrumentLedgerHawlaWise";
            }
            else if (isHawlaWise == 0 && isInvestorWise == 1)
            {
                reportName = "rptInstrumentLedgerInvestorWise.rpt";
                exportName = "InstrumentLedger";
            }
            else if (isHawlaWise == 0 && isInvestorWise == 0)
            {
                reportName = "rptInstrumentLedger.rpt";
                exportName = "InstrumentLedger";
            }
            ReportHelper.ShowReport(data, type, reportName, exportName + trxdatefrom.ToString("dd_MMM_yyyy") + "to_" + trxdateto.ToString("dd_MMM_yyyy") + "_" + code.Replace(",", "_"));
        }
        public ActionResult ClientPortfolio()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            return View();
        }
        public void ShowPortfolio(string trxDate, string type, string code)
        {
            if (!ReportHelper.CheckSoftwareExpiration())
                Response.Redirect(Url.Action("Login", "Account"));
            var asOnDate = ReportHelper.FormatDate(trxDate);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = asOnDate.ToString("dd MMM yyyy"),
                        INVESTOR_CODE = code
                    },
                    "SP_RPT_INVESTOR_PORTFOLIO").Tables[0];
            var subData = spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = asOnDate.ToString("dd MMM yyyy"),
                        INVESTOR_CODE = code
                    },
                    "SP_RPT_INVESTOR_RECEIVABLE_SHARE").Tables[0];
            var subReport = new Dictionary<int, DataTable> { { 0, subData } };
            ReportHelper.ShowReport(data, type, "rptInvestorPortfolio.rpt", "Portfolio_" + asOnDate.ToString("dd_MMM_yyyy") + "_" + code.Replace(",", "_"), null, subReport);
        }

        public JsonResult EmailPortfolio(string trxDate, string type, string code)
        {
            try
            {
                var asOnDate = ReportHelper.FormatDate(trxDate);
                var data =
                    spService.GetDataWithParameter(
                        new
                        {
                            TRANSACTION_DATE = asOnDate.ToString("dd MMM yyyy"),
                            INVESTOR_CODE = code
                        },
                        "SP_RPT_INVESTOR_PORTFOLIO").Tables[0];
                var subData = spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = asOnDate.ToString("dd MMM yyyy"),
                        INVESTOR_CODE = code
                    },
                    "SP_RPT_INVESTOR_RECEIVABLE_SHARE").Tables[0];
                var subReport = new Dictionary<int, DataTable> { { 0, subData } };
                var investor = spService.GetDataWithParameter(new { INVESTOR_CODE = code }, "SP_GET_INVESTOR_EMAIL_ADDRESS").Tables[0];
                var email = investor.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { Status = false, Message = "This investor has no valid email address to email portfolio." }, JsonRequestBehavior.AllowGet);
                }
                ReportHelper.EmailReport(email, "Portfolio of " + investor.Rows[0][1], "Dear valued client, portfolio report as on " + asOnDate.ToString("dd MMM yyyy") + ".Please check the attachment below.", data, type, "rptInvestorPortfolio.rpt",
                    "Portfolio_" + asOnDate.ToString("dd_MMM_yyyy") + "_" + code.Replace(",", "_"), null, subReport);
                spService.ExecuteStoredProcedure(new
                {
                    INVESTOR_ID = int.Parse(investor.Rows[0][2].ToString()),
                    TYPE = "EPORTFOLIO",
                    USER_ID = SessionHelper.LoggedInUserId
                }, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                return Json(new { Status = true, Message = "Portfolio email successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ShowTaxCertificate(string trxDateFrom, string trxDateTo, string type, string code, int isPrintOnPad)
        {
            var fromDate = ReportHelper.FormatDate(trxDateFrom);
            var toDate = ReportHelper.FormatDate(trxDateTo);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = fromDate.ToString("dd MMM yyyy"),
                        TRANSACTION_DATE_TO = toDate.ToString("dd MMM yyyy"),
                        INVESTOR_CODE = code,
                        USER_ID = SessionHelper.LoggedInUserId
                    },
                    "SP_RPT_INVESTOR_TAX_CERTIFICATE").Tables[0];
            var param = new Dictionary<string, object> { { "isPadprint", isPrintOnPad } };
            ReportHelper.ShowReport(data, type, "rptInvestorTAXCertificate.rpt", "TaxCertificate_" + code.Replace(",", "_"), param);
        }

        public JsonResult EmailTaxCertificate(string trxDateFrom, string trxDateTo, string type, string code, int isPrintOnPad)
        {
            try
            {
                var fromDate = ReportHelper.FormatDate(trxDateFrom);
                var toDate = ReportHelper.FormatDate(trxDateTo);
                var data =
                    spService.GetDataWithParameter(
                        new
                        {
                            TRANSACTION_DATE_FROM = fromDate.ToString("dd MMM yyyy"),
                            TRANSACTION_DATE_TO = toDate.ToString("dd MMM yyyy"),
                            INVESTOR_CODE = code,
                            USER_ID = SessionHelper.LoggedInUserId
                        },
                        "SP_RPT_INVESTOR_TAX_CERTIFICATE").Tables[0];
                var param = new Dictionary<string, object>
                {
                    {"isPadprint", isPrintOnPad}
                };
                var investor = spService.GetDataWithParameter(new { INVESTOR_CODE = code }, "SP_GET_INVESTOR_EMAIL_ADDRESS").Tables[0];
                var email = investor.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { Status = false, Message = "This investor has no valid email address to email tax certificate." }, JsonRequestBehavior.AllowGet);
                }
                ReportHelper.EmailReport(email, "TAX certificate of " + investor.Rows[0][1], "Dear valued client, TAX certificate report from " + fromDate.ToString("dd MMM yyyy") + " to " + toDate.ToString("dd MMM yyyy") + ".Please check the attachment below.", data, type, "rptInvestorTAXCertificate.rpt", "TaxCertificate_" + code.Replace(",", "_"), param);
                spService.ExecuteStoredProcedure(new
                {
                    INVESTOR_ID = int.Parse(investor.Rows[0][2].ToString()),
                    TYPE = "ETAX",
                    USER_ID = SessionHelper.LoggedInUserId
                }, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                return Json(new { Status = true, Message = "TAX certificate email successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ClientTAXCertificate()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            return View();
        }

        public JsonResult GetInvestorName(string code)
        {
            var data = spService.GetDataWithParameter(
            new
            {
                INVESTOR_CODE = code
            },
            "SP_GET_INVESTOR_NAME_BY_CODE").Tables[0].AsEnumerable().Select(x => new { InvestorCode = x.Field<string>(1), InvestorName = x.Field<string>(2), TaxRate = x.Field<decimal>(5) }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Introducer()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("Report", "Introducer");
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
            ViewBag.Employees = brokerEmployeeService.GetAll().ToList();
            return View();
        }
        public void ShowIntroducerWiseNetBrokerage(string fromDate, string toDate, string type, string employeeId, int isSummary)
        {
            var dateFrom = ReportHelper.FormatDate(fromDate);
            var dateTo = ReportHelper.FormatDate(toDate);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = dateFrom.ToString("dd MMM yyyy"),
                        TRANSACTION_DATE_TO = dateTo.ToString("dd MMM yyyy"),
                        EMPLOYEE_ID = employeeId
                    },
                    (isSummary == 0 ? "SP_RPT_GET_NET_BROKERAGE_COMMISSION_INTRODUCER_WISE" : "SP_RPT_GET_NET_BROKERAGE_COMMISSION_INTRODUCER_WISE_SUMMARY")).Tables[0];
            var param = new Dictionary<string, object> { };
            if (isSummary == 0)
            {
                param.Add("isSummary", 0);
            }
            ReportHelper.ShowReport(data, type, (isSummary == 0 ? "rptNetBrokerageComissionMarketWise.rpt" : "rptNetBrokerageCommissionIntroducerWise.rpt"), "IntroducerWiseTurnover_" + dateFrom.ToString("dd_MMM_yyyy") + "_to_" + dateTo.ToString("dd_MMM_yyyy"), param);
        }

        public void ShowIntroducerWiseCashChequeShareIn(string fromDate, string toDate, string type, string employeeId, int inOut)
        {
            var dateFrom = ReportHelper.FormatDate(fromDate);
            var dateTo = ReportHelper.FormatDate(toDate);
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = dateFrom.ToString("dd MMM yyyy"),
                        TRANSACTION_DATE_TO = dateTo.ToString("dd MMM yyyy"),
                        EMPLOYEE_ID = employeeId
                    },
                    (inOut == 0 ? "SP_RPT_INTRODUCER_WISE_CASH_CHEQUE_SHARE_IN" : "SP_RPT_INTRODUCER_WISE_CASH_CHEQUE_SHARE_OUT")).Tables[0];
            var param = new Dictionary<string, object> { { "inOut", inOut } };
            ReportHelper.ShowReport(data, type, "rptIntroducerWiseCashChequeShareInOut.rpt", "IntroducerWiseCashChequeShareInOut_" + dateFrom.ToString("dd_MMM_yyyy") + "_to_" + dateTo.ToString("dd_MMM_yyyy"), param);
        }

        public void ShowInvestorWithBankBranch(string reportNo,string bankAccount, string branchId, string bo, string code, string name, string bank, string branch, string account, string bankBranch, string type)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        BANK_AC_TYPE = bankAccount,
                        BRANCH_ID = branchId,
                        BO_ID = bo,
                        INVESTOR_CODE = code,
                        INVESTOR_NAME = name,
                        BANK = bank,
                        BRANCH = branch,
                        ACCOUNT_NO = account,
                        BANK_BRANCH_TYPE = bankBranch
                    },
                    "SP_RPT_INVESTOR_WITH_BANK_BRANCH_ACCOUNT").Tables[0];
            ReportHelper.ShowReport(data, type, "rptInvestorBankAccountList.rpt", "InvestorBankAccountList");
        }
    }
}