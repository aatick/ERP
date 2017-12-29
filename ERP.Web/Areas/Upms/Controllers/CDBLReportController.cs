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
    public class CDBLReportController : BaseController
    {
        private readonly ISPService spService;
        private readonly ICompanyInfoService companyInfoService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService;

        public CDBLReportController(ISPService spService, IMarketInfoService marketInfoService, ICompanyInfoService companyInfoService, IBrokerBranchService brokerBranchService
               , IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService)
        {
            this.spService = spService;
            this.companyInfoService = companyInfoService;
            this.brokerBranchService = brokerBranchService;
            this.marketInfoService = marketInfoService;
            this.brokerDepositoryParticipatoryService = brokerDepositoryParticipatoryService;
        }

        private class CDBLFileMode
        {
            public int Id { get; set; }
            public string FileNameMsg { get; set; }
            public string CDBLFileName { get; set; }
        }
        public JsonResult GetCDBLFileNames()
        {
            try
            {
                List<CDBLFileMode> CDBLFileList = new List<CDBLFileMode>();
                var empList = spService.GetDataWithoutParameter("GetCDBLFileNames");

                CDBLFileList = empList.Tables[0].AsEnumerable()
                .Select(row => new CDBLFileMode
                {
                    CDBLFileName = row.Field<string>("CDBLFileName"),
                    FileNameMsg = row.Field<string>("FileNameMsg"),
                }).ToList();
                return Json(CDBLFileList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult CdblGnlRpt()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["CDBLReportList"] = items;


            var moduleid = spService.GetSecurityModuleByControllerAction("CDBLReport", "CdblGnlRpt");
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
        public ActionResult Index()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("CDBLReport", "Index");
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

        public void ShowClosingStockReport(string fromDate, string toDate, string type)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = ReportHelper.FormatDateToString(fromDate),
                        TRANS_DATE_TO = ReportHelper.FormatDateToString(toDate)
                    },
                    "SP_RPT_CDBL_CLOSING_STOCK").Tables[0];
            ReportHelper.ShowReport(data, type, "rptCDBLClosingStock.rpt", "CDBLClosingStock_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }

        public void ShowStockMismatchReport(int companyId, string type)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        COMPANY_ID = companyId
                    },
                    "SP_RPT_STOCK_MISMATCH_WITH_CDBL").Tables[0];
            ReportHelper.ShowReport(data, type, "rptStockMismatchWithCDBL.rpt", "StockMismatchWithCDBL_" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }
        public void ShowClientWiseCashDividend(string fromDate, string toDate, string type, string code)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        Ismarged = 0,
                        FromDate = ReportHelper.FormatDateToString(fromDate),
                        ToDate = ReportHelper.FormatDateToString(toDate),
                        Code = code
                    },
                    "GetInvestorCashDevidend").Tables[0];
            ReportHelper.ShowReport(data, type, "rptCashDividendInvestorWise.rpt", "Cash_Devidend");
        }

        public void ShowBranchWiseBOOpening(string fromDate, string toDate, string type, int branch)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        FromDate = ReportHelper.FormatDateToString(fromDate),
                        ToDate = ReportHelper.FormatDateToString(toDate),
                        QType = "All",
                        BRANCH_ID = branch
                    },
                    "GetInvestorAccountOpeningFromCDBLInfo").Tables[0];
            ReportHelper.ShowReport(data, type, "rpt_GetInvestorAccountOpeningFromCDBLInfo.rpt", "Account_Opening");
        }

        public void ShowBranchWiseBOClosing(string fromDate, string toDate, string type, int branch)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(fromDate),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(toDate),
                        TYPE = -1,
                        BRANCH_ID = branch
                    },
                    "SP_RPT_INVESTOR_BO_CLOSING_INFO").Tables[0];
            ReportHelper.ShowReport(data, type, "rptInvestorBOClosing.rpt", "Account_Closing");
        }

        public void Get_General_CDBLReport(int rptslnx, string FileName, string QType, string FromDate, string ToDate, int IsPending, string exportType)
        {

            var param = new { FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate), QType = QType };

            if (rptslnx == 1) // "08DP01UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorAccountOpeningFromCDBLInfo").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorAccountOpeningFromCDBLInfo.rpt", "rpt_GetInvestorAccountOpeningFromCDBLInfo");
            }
            else if (rptslnx == 2)//"38DP78UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetTradePayIn").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetTradePayIn.rpt", "rpt_GetTradePayIn");
            }
            else if (rptslnx == 3)// "38DP79UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetTradePayOut").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetTradePayOut.rpt", "rpt_GetTradePayOut");
            }
            else if (rptslnx == 4)//17DP64UX.TXT"
            {
                var param2 = new { FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate), QType = QType, IsPending = IsPending };
                var data = spService.GetDataWithParameter(param2, "GetCompanyBonusRightShareDeclaration").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetCompanyBonusRightShareDeclaration.rpt", "rpt_GetCompanyBonusRightShareDeclaration");
            }
            else if (rptslnx == 5)//2 //"17DP70UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorBonusRightShareCreditInfo").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorBonusRightShareCreditInfo.rpt", "rpt_GetInvestorBonusRightShareCreditInfo");
            }
            else if (rptslnx == 6)//"11DPA6UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorCompanyWiseClosingStock").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorCompanyWiseClosingStock.rpt", "rpt_InvestorCompanyWiseClosingStock");
            }
            else if (rptslnx == 7)//"16DP61UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorDematShare").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorDematShare.rpt", "rpt_GetInvestorDematShare");
            }
            else if (rptslnx == 8)//"15DP62UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorRematShare").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorRematShare.rpt", "rpt_GetInvestorRematShare");
            }
            else if (rptslnx == 9)//"40DP31UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorPledgeShare").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorPledgeShare.rpt", "rpt_GetInvestorPledgeShare");
            }
            else if (rptslnx == 10)//"40DP68UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorUnpledgeShare").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorUnpledgeShare.rpt", "rpt_InvestorUnpledgeShare");
            }
            else if (rptslnx == 11)//16//"11DP98UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorShareTransferIn").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorShareTransferIn.rpt", "rpt_GetInvestorShareTransferIn");
            }
            else if (rptslnx == 12)//"11DP40UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorShareTransferOut").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorShareTransferOut.rpt", "rpt_InvestorShareTransferOut");
            }
            else if (rptslnx == 13)//"11DP81UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorShareTransmissionIn").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorShareTransmissionIn.rpt", "rpt_InvestorShareTransmissionIn");
            }
            else if (rptslnx == 14)//"11DP38UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorShareTransmissionOut").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorShareTransmissionOut.rpt", "rpt_InvestorShareTransmissionOut");
            }

            else if (rptslnx == 15)//"11DP41UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorChangeOwnershipIn").Tables[0];
                //var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorChangeOwnershipIn.rpt", "rpt_InvestorChangeOwnershipIn");
            }
            else if (rptslnx == 16)//"11DP39UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorChangeOwnershipOut").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_InvestorChangeOwnershipOut.rpt", "rpt_InvestorChangeOwnershipOut");
            }
            else if (rptslnx == 17)//"38DP77UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorPayInReconcilation").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorPayInReconcilation.rpt", "rpt_GetInvestorPayInReconcilation");
            }
            else if (rptslnx == 18)//"38DP80UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorPayOutReconcilation").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorPayOutReconcilation.rpt", "rpt_GetInvestorPayOutReconcilation");
            }
            else if (rptslnx == 19)//"43DP52UX.TXT"
            {
                var data = spService.GetDataWithParameter(param, "GetInvestorCDBLCharges").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rpt_GetInvestorCDBLCharges.rpt", "rpt_GetInvestorCDBLCharges");
            }

            else if (rptslnx == 20)//"17DP65UX.TXT"
            {
                var Ismarged = 0;
                if (IsPending == 1)
                {
                    Ismarged = 0;
                }
                else
                {
                    Ismarged = 1;
                }
                var paramfrac = new { Ismarged = Ismarged, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };
                var data = spService.GetDataWithParameter(paramfrac, "GetInvestorCashFraction").Tables[0];
                ReportHelper.ShowReport(data, exportType, "Rpt_Investor_CashFraction.rpt", "Rpt_Investor_CashFraction");
            }

            else if (rptslnx == 21)//"16DP95UX.TXT"
            {
                var parameter =
                    new
                    {
                        TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(FromDate),
                        TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(ToDate)
                    };
                var data = spService.GetDataWithParameter(parameter, "SP_RPT_IPO_SHARE_CREDIT").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rptIPOShareCredit.rpt", "IPOShareCredit");
            }

            else if (rptslnx == 22)//"17DP48UX.TXT"
            {
                var Ismarged = 0;
                if (IsPending == 1)
                {
                    Ismarged = 0;
                }
                else
                {
                    Ismarged = 1;
                }
                var paramfrac = new { Ismarged = Ismarged, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };

                var data = spService.GetDataWithParameter(paramfrac, "GetInvestorCashDevidend").Tables[0];
                ReportHelper.ShowReport(data, exportType, "Rpt_InvestorCash_Devidend.rpt", "Rpt_InvestorCash_Devidend");
            }
            else if (rptslnx == 23)//Account Modify "08DP03UX.TXT"
            {
                var paramModify = new { FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };
                var data = spService.GetDataWithParameter(paramModify, "Get_Investor_Account_Modify_Info").Tables[0];
                ReportHelper.ShowReport(data, exportType, "Rpt_Investor_Account_Modify.rpt", "Rpt_Investor_Account_Modify");
            }
            else if (rptslnx == 24)//"08DP04UX.TXT"
            {
                var isProcessed = -1;
                if (QType == "OnlyProcess")
                {
                    isProcessed = 1;
                }
                else if (QType == "OnlyUpload")
                {
                    isProcessed = 0;
                }
                var paramModify = new { TRANSACTION_DATE_FROM = ReportHelper.FormatDateToString(FromDate), TRANSACTION_DATE_TO = ReportHelper.FormatDateToString(ToDate), TYPE = isProcessed };
                var data = spService.GetDataWithParameter(paramModify, "SP_RPT_INVESTOR_BO_CLOSING_INFO").Tables[0];
                ReportHelper.ShowReport(data, exportType, "rptInvestorBOClosing.rpt", "InvestorBOClosing");
            }
        }
        
    }
}