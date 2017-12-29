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

// ReSharper disable once CheckNamespace
namespace Upms.Controllers
{
    public class TradeReportController : BaseController
    {
        private readonly ISPService spService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService;
        private readonly IBrokerDPBranchService brokerDPBranchService;
        private readonly IBrokerTraderService brokerTraderService;
        public TradeReportController(ISPService spService, IMarketInfoService marketInfoService, IBrokerBranchService brokerBranchService, IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService, IBrokerDPBranchService brokerDPBranchService, IBrokerTraderService brokerTraderService)
        {
            this.spService = spService;
            this.marketInfoService = marketInfoService;
            this.brokerBranchService = brokerBranchService;
            this.brokerDepositoryParticipatoryService = brokerDepositoryParticipatoryService;
            this.brokerDPBranchService = brokerDPBranchService;
            this.brokerTraderService = brokerTraderService;
        }
        //
        // GET: /TradeReport/


        public ActionResult StockExchangeReport()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.Market = marketInfoService.GetAll().ToList();
            ViewBag.Branch = brokerBranchService.GetAll().ToList();
            var moduleid = spService.GetSecurityModuleByControllerAction("TradeReport", "StockExchangeReport");
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

        public ActionResult ClientReport()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.BrokerBranch = brokerBranchService.GetAll().ToList();
            var moduleid = spService.GetSecurityModuleByControllerAction("TradeReport", "ClientReport");
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

        public ActionResult DepositoryParticipantReport()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.DP = brokerDepositoryParticipatoryService.GetAll().ToList();
            var moduleid = spService.GetSecurityModuleByControllerAction("TradeReport", "DepositoryParticipantReport");
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
        public ActionResult TraderWiseReport()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.Market = marketInfoService.GetAll().ToList();
            ViewBag.Trader = spService.GetDataBySqlCommand("SELECT DISTINCT TraderCode FROM TraderCodes").Tables[0].AsEnumerable().Select(x => x.Field<string>(0)).ToList();
            var moduleid = spService.GetSecurityModuleByControllerAction("TradeReport", "TraderWiseReport");
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

        public void ShowNetBrokerageComissionReport(string transactionDateFrom, string transactionDateTo, string type, int marketId)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId
                    },
                    "SP_RPT_GET_NET_BROKERAGE_COMISSION").Tables[0];
            var param = new Dictionary<string, object> { { "isSummary", 0 } };
            ReportHelper.ShowReport(data, type, "rptNetBrokerageComissionMarketWise.rpt", "NetBrokerageComission_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }

        public void Rpt_Investor_Overtrading(string TransactionDate, string InvestorCode, string type)
        {

            var param = new { INVESTOR_CODE = InvestorCode, DATE = ReportHelper.FormatDateToString(TransactionDate) };

            var data = spService.GetDataWithParameter(param, "Rpt_Investor_Overtrading").Tables[0];

            ReportHelper.ShowReport(data, type, "RptInvestorOvertradingReport.rpt", "RptInvestorOvertradingReport");

        }
        public void GET_T1_T9_MATURED_BALANCE(string maturedDate, string InvestorCode, string type)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = ReportHelper.FormatDateToString(maturedDate),
                        INVESTOR_CODE = InvestorCode,
                    },
                    "SP_GET_T1_T9_MATURED_BALANCE").Tables[0];
            ReportHelper.ShowReport(data, type, "Rpt_T1_T9_MATURED_BALANCE.rpt", "Rpt_T1_T9_MATURED_BALANCE");
        }
        public void ShowMarginBuyAndNonmarginSale(string TransactionDate, string type)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        DATE = ReportHelper.FormatDateToString(TransactionDate),

                    },
                    "Rpt_MarginBuy_NonMarginSale_Report").Tables[0];
            ReportHelper.ShowReport(data, type, "Rpt_MarginBuyNonMarginSaleReport.rpt", "Rpt_MarginBuyNonMarginSaleReport");
        }
        public void ShowAverageTurnoverReport(string transactionDateFrom, string transactionDateTo, string type, int marketId, int reportType)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId,
                        REPORT_TYPE = reportType
                    },
                    "SP_RPT_MONTHLY_YEARLY_AVERAGE_TURNOVER").Tables[0];
            ReportHelper.ShowReport(data, type, "rptAverageTurnover.rpt", "AverageTurnover");
        }

        public void ShowNetBrokerageComissionSummary(string transactionDateFrom, string transactionDateTo, string type, int marketId)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId
                    },
                    "SP_RPT_GET_NET_BROKERAGE_COMISSION_SUMMARY").Tables[0];
            var param = new Dictionary<string, object> { { "isSummary", 1 } };
            ReportHelper.ShowReport(data, type, "rptNetBrokerageComissionMarketWise.rpt", "NetBrokerageComission_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }

        // ReSharper disable once InconsistentNaming
        public void ShowCNSReport(string transactionDateFrom, string transactionDateTo, string type, int marketId)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId
                    },
                    "SP_RPT_GET_DSE_CSE_CNS_REPORT").Tables[0];
            ReportHelper.ShowReport(data, type, "rptCNSReport.rpt", "CNSReport_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }

        public void ShowClientTradeSummaryReport(string transactionDateFrom, string transactionDateTo, string type, int brokerBranch, string investorCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        INVESTOR_CODE = investorCode,
                        BROKER_BRANCH_ID = brokerBranch
                    },
                    "SP_RPT_CLIENT_TRADE_SUMMARY").Tables[0];
            ReportHelper.ShowReport(data, type, "rptTradeSummary.rpt", "TradeSummary_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }
        public void ShowTradeConfirmationReport(string transactionDateFrom, string transactionDateTo, string type, int brokerBranch, string investorCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        INVESTOR_CODE = investorCode,
                        BROKER_BRANCH_ID = brokerBranch
                    },
                    "SP_RPT_CLIENT_TRADE_CONFIRMATION_STATEMENT").Tables[0];
            ReportHelper.ShowReport(data, type, "rptTradeConfirmationStatement.rpt", "TradeConfirmationStatement_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }

        public JsonResult EmailTradeConfirmationReport(string transactionDateFrom, string transactionDateTo, string type, int brokerBranch, string investorCode)
        {
            try
            {
                var data =
                    spService.GetDataWithParameter(
                        new
                        {
                            TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                            TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                            INVESTOR_CODE = investorCode,
                            BROKER_BRANCH_ID = brokerBranch
                        },
                        "SP_RPT_CLIENT_TRADE_CONFIRMATION_STATEMENT").Tables[0];
                if (data.Rows.Count == 0)
                {
                    return Json(new { Status = false, Message = "No data found." }, JsonRequestBehavior.AllowGet);
                }

                var investor = spService.GetDataWithParameter(new { INVESTOR_CODE = investorCode }, "SP_GET_INVESTOR_EMAIL_ADDRESS").Tables[0];
                var email = investor.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(email))
                {
                    return Json(new { Status = false, Message = "This investor has no valid email address to email trade confirmation." }, JsonRequestBehavior.AllowGet);
                }
                ReportHelper.EmailReport(email, "Trade confirmation of " + investor.Rows[0][1], "Dear valued client, Trade confirmation report from " + ReportHelper.FormatDateToString(transactionDateFrom) + " to " + ReportHelper.FormatDateToString(transactionDateTo) + ".Please check the attachment below.", data, type, "rptTradeConfirmationStatement.rpt", "TradeConfirmationStatement_" + DateTime.Today.ToString("dd_MMM_yyyy"));
                spService.ExecuteStoredProcedure(new
                {
                    INVESTOR_ID = int.Parse(investor.Rows[0][2].ToString()),
                    TYPE = "ETRADE",
                    USER_ID = SessionHelper.LoggedInUserId
                }, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                return Json(new { Status = true, Message = "Trade confirmation email successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ShowBrokerageCommissionReport(string transactionDateFrom, string transactionDateTo, string type, int reportType, string investorCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        INVESTOR_CODE = investorCode,
                        REPORT_TYPE = reportType
                    },
                    "SP_RPT_INVESTOR_BROKERAGE_COMMISSION_SUMMARY").Tables[0];
            ReportHelper.ShowReport(data, type, "rptInvestorBrokerageCommission.rpt", "BrokerageCommission_" + investorCode.Replace(",", "_"));
        }
        public void ShowBrokerageCommissionClientWiseReport(string transactionDateFrom, string transactionDateTo, string type, string investorCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        CODE = investorCode
                    },
                    "SP_RPT_GET_NET_BROKERAGE_COMMISSION_INVESTOR_WISE").Tables[0];
            var param = new Dictionary<string, object> { { "isSummary", 0 } };
            ReportHelper.ShowReport(data, type, "rptNetBrokerageComissionMarketWise.rpt", "BrokerageIncome_" + investorCode.Replace(",", "_"), param);
        }

        public void ShowBrokerageCommissionClientWiseSummary(string transactionDateFrom, string transactionDateTo, string type, string investorCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        CODE = investorCode
                    },
                    "SP_RPT_GET_NET_BROKERAGE_COMMISSION_INVESTOR_WISE_SUMMARY").Tables[0];
            ReportHelper.ShowReport(data, type, "rptNetBrokerageCommissionClientWise.rpt", "BrokerageIncome_" + investorCode.Replace(",", "_"));
        }

        public void ShowBrokerageCommissionSummary(string transactionDateFrom, string transactionDateTo, string type, string investorCode, string balanceFrom, string balanceTo)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        INVESTOR_CODE = investorCode,
                        COMMISSION_FROM = balanceFrom,
                        COMMISSION_TO = balanceTo
                    },
                    "SP_RPT_ALL_INVESTOR_BROKERAGE_COMMISSION_SUMMARY").Tables[0];
            ReportHelper.ShowReport(data, type, "rptInvestorBrokerageCommissionSummary.rpt", "BrokerageCommissionSummary_" + investorCode.Replace(",", "_"));
        }
        public void ShowMemberPaymentReport(string transactionDateFrom, string transactionDateTo, string type, int marketId)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId
                    },
                    "SP_RPT_MEMBERS_PAYMENT").Tables[0];
            ReportHelper.ShowReport(data, type, "rptMembersPayment.rpt", "MembersPayment_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }
        public void ShowCSEMOReport(string transactionDate, string type, int marketId)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE = ReportHelper.FormatDateToString(transactionDate),
                        MARKET_ID = marketId
                    },
                    "SP_RPT_CSE_MO").Tables[0];
            ReportHelper.ShowReport(data, type, "rptCSEMO.rpt", "MO_" + ReportHelper.FormatDate(transactionDate).ToString("dd_MMM_yyyy"));
        }
        public void ShowNettingReport(string transactionDateFrom, string transactionDateTo, string type, int marketId, int reportType)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId,
                        @IS_CLIENT_WISE = reportType
                    },
                    "SP_RPT_BUY_SALE_NETTING_CLIENT_INSTRUMENT_WISE").Tables[0];
            var param = new Dictionary<string, object> { { "isClientWise", reportType == 1 } };
            ReportHelper.ShowReport(data, type, "rptBuySaleNettingReport.rpt", "BuySaleNetting_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }

        public void ShowDpTradeReport(string transactionDateFrom, string transactionDateTo, string type, int dpId, int reportType, int branchId)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        DP_ID = dpId,
                        DP_BRANCH_ID = branchId,
                        IS_CLIENT_WISE = reportType
                    },
                    "SP_RPT_DP_TRADE_SUMMARY_CLIENT_INSTRUMENT_WISE").Tables[0];
            var param = new Dictionary<string, object> { { "isClientWise", reportType == 1 } };
            ReportHelper.ShowReport(data, type, "rptDPTradeSummary.rpt", "DPTradeSummary_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }

        public void ShowTraderWiseBuySaleDetails(string transactionDateFrom, string transactionDateTo, string type, int marketId, int isSummary, int isOrder, string traderCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId,
                        User_ID = SessionHelper.LoggedInUserId,
                        TRADER_CODE = traderCode
                    },
                    (isOrder == 1 ? "SP_RPT_BUY_SALE_ORDER" : (isSummary == 0 ? "SP_RPT_BUY_SALE_DETAILS_TRADER_WISE" : "SP_RPT_BUY_SALE_SUMMARY_TRADER_WISE"))).Tables[0];

            ReportHelper.ShowReport(data, type, (isOrder == 1 ? "rptBuySaleOrder.rpt" : (isSummary == 0 ? "rptBuySaleDetailsTraderWise.rpt" : "rptBuySaleSummaryTraderWise.rpt")), (isOrder == 1 ? "TraderWiseBuySaleOrder_" : (isSummary == 0 ? "TraderWiseBuySaleDetails_" : "TraderWiseBuySaleSummary_")) + DateTime.Today.ToString("dd_MMM_yyyy"));
        }

        public void ShowDateWiseIncomeStatement(string transactionDateFrom, string transactionDateTo, string type, int marketId, int isSummary = 0, int reportType = 0, int branchId = 0)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        MARKET_ID = marketId,
                        BRANCH_ID = branchId,
                        REPORT_TYPE = reportType
                    },
                    "SP_RPT_DAY_WISE_INCOME_STATEMENT").Tables[0];
            var param = new Dictionary<string, object> { { "isSummary", isSummary } };
            ReportHelper.ShowReport(data, type, "rptDayWiseInomeStatement.rpt", "INCOME_STATEMENT_" + DateTime.Today.ToString("dd_MMM_yyyy"), param);
        }



        public void ShowDpNettingReport(string transactionDateFrom, string transactionDateTo, string type, int dpId, int branchId)
        {
            var tradeDateFrom = ReportHelper.FormatDate(transactionDateFrom);
            var tradeDateTo = ReportHelper.FormatDate(transactionDateTo);
            var data =
            spService.GetDataWithParameter(
                new
                {
                    TRANS_DATE_FROM = tradeDateFrom.ToString("dd MMM yyyy"),
                    TRANS_DATE_TO = tradeDateTo.ToString("dd MMM yyyy"),
                    DP_ID = dpId,
                    DP_BRANCH_ID = branchId,
                },
                "SP_RPT_DP_NETTING").Tables[0];
            ReportHelper.ShowReport(data, type, "rptDPNetting.rpt", "DPNetting_" + tradeDateFrom.ToString("ddMMyyyy") + "_to_" + tradeDateTo.ToString("ddMMyyyy"));
        }
        public void SaveTradeConfirmationReportAndMail(string transactionDateFrom, string transactionDateTo, string type, int brokerBranch, string investorCode)
        {
            var investor =
                spService.GetDataWithoutParameter("SP_GET_TODAYS_INDIVIDUAL_INVESTOR_CODE").Tables[0];
            for (var i = 0; i < investor.Rows.Count; i++)
            {
                var data = spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(transactionDateFrom),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(transactionDateTo),
                        INVESTOR_CODE = investor.Rows[i].Field<string>(0),
                        BROKER_BRANCH_ID = brokerBranch
                    },
                    "SP_RPT_CLIENT_TRADE_CONFIRMATION_STATEMENT").Tables[0];
                ReportHelper.SaveReport("D://Generated Data/Trade Confirmation E-mailed to Client/" + DateTime.Today.Year + "/" + DateTime.Today.ToString("MMMM") + "/" + DateTime.Today.ToString("dd MMM yyyy"), data, type, "rptTradeConfirmationStatement.rpt", "Trade_Confirmation_" + investor.Rows[i].Field<string>(0));
            }
        }

        // ReSharper disable once InconsistentNaming
        public JsonResult GetDPBranchByDPId(int dpId)
        {
            var branches = brokerDPBranchService.GetAll().Where(x => x.DepositoryParticipantId == dpId).ToList();
            return Json(branches, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Analysis()//TransactionTypeName
        {
            //var rptCon = DependencyResolver.Current.GetService<IPOGroupController>();
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.TransactionList = spService.GetDataWithoutParameter("GetTransactionTypeList_CDBL").Tables[0].AsEnumerable().Select(row => new { Id = row.Field<int>("Id"), TransactionTypeName = row.Field<string>("TransactionTypeName") }); //rptCon.GetTransactionTypeList().Where(x=>x.ChargeType == "CDBL").ToList().OrderBy(x => x.TransactionTypeName);
            var moduleid = spService.GetSecurityModuleByControllerAction("TradeReport", "Analysis");
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
        public void ShowSameTimeBuySale(string transactionDate, string type)
        {
            var tradeDate = ReportHelper.FormatDate(transactionDate);
            var data =
            spService.GetDataWithParameter(
                new
                {
                    TRANSACTION_DATE = tradeDate.ToString("dd MMM yyyy")
                },
                "SP_RPT_SAME_INSTRUMENT_BUY_SALE_ON_SAME_TIME").Tables[0];
            ReportHelper.ShowReport(data, type, "rptShareBuySaleOnSameTime.rpt", "BuySaleOnSameTime_" + tradeDate.ToString("ddMMyyyy"));
        }
        public void ShowSufficient_In_Suff_Neg_Charge(string transactionDate, string InvestorCode, int TransTypeId, string type)
        {
            var Qtype = 1;
            if (TransTypeId == 0)
                Qtype = 2;

            var data =
            spService.GetDataWithParameter(
                new//Qtype	INVESTOR_CODE	DATE	ChargeTransactionTypeId	
                {
                    Qtype = Qtype,
                    INVESTOR_CODE = InvestorCode,
                    DATE = ReportHelper.FormatDateToString(transactionDate),
                    ChargeTransactionTypeId = TransTypeId
                },
                "Rpt_InvBal_Sufficient_Insuff_Neg_AfterChargeDeduct").Tables[0];
            ReportHelper.ShowReport(data, type, "RptInvestorOvertradingSufficientInSufficientNegative.rpt", "RptInvestorOvertradingSufficientInSufficientNegative");
        }
    }
}