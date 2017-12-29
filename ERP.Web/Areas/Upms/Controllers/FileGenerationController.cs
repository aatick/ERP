using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
    public class FileGenerationController : BaseController
    {
        private readonly ISPService spService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService;
        private readonly IBrokerDPBranchService brokerDPBranchService;
        private readonly IBrokerTraderService brokerTraderService;
        private readonly IIPODeclarationService ipoDeclarationService;
        private readonly ICompanyInfoService companyInfoService;
        private readonly IBrokerInfoService brokerInfoService;
        private TradeHelperController tradeHelper = DependencyResolver.Current.GetService<TradeHelperController>();
        public FileGenerationController(ISPService spService, IMarketInfoService marketInfoService
            , IBrokerBranchService brokerBranchService, IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService
            , IBrokerDPBranchService brokerDPBranchService, IBrokerTraderService brokerTraderService
            , IIPODeclarationService ipoDeclarationService, ICompanyInfoService companyInfoService, IBrokerInfoService brokerInfoService)
        {
            this.spService = spService;
            this.marketInfoService = marketInfoService;
            this.brokerBranchService = brokerBranchService;
            this.brokerDepositoryParticipatoryService = brokerDepositoryParticipatoryService;
            this.brokerDPBranchService = brokerDPBranchService;
            this.brokerTraderService = brokerTraderService;
            this.ipoDeclarationService = ipoDeclarationService;
            this.companyInfoService = companyInfoService;
            this.brokerInfoService = brokerInfoService;
        }
        public ActionResult GenerateDPTradeFiles()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.DP = brokerDepositoryParticipatoryService.GetAll().ToList();
            ViewBag.FileLocation = ConfigurationManager.AppSettings["GeneratedDPFile"];
            var moduleid = spService.GetSecurityModuleByControllerAction("FileGeneration", "GenerateDPTradeFiles");
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

        public JsonResult GenerateDpBill(string transactionDate, int dpId)
        {
            var location = ConfigurationManager.AppSettings["GeneratedDPFile"];
            var tradeDate = ReportHelper.FormatDate(transactionDate);
            var dp = brokerDepositoryParticipatoryService.GetAll().ToList();
            if (dpId > 0)
                dp = dp.Where(x => x.Id == dpId).ToList();
            foreach (var depositoryPariticipant in dp)
            {
                var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = tradeDate.ToString("dd MMM yyyy"),
                        TRANS_DATE_TO = tradeDate.ToString("dd MMM yyyy"),
                        DP_ID = depositoryPariticipant.Id,
                        DP_BRANCH_ID = 0,
                        IS_CLIENT_WISE = 0
                    },
                    "SP_RPT_DP_TRADE_SUMMARY_CLIENT_INSTRUMENT_WISE").Tables[0];
                var path = location + tradeDate.Year + "/" + tradeDate.ToString("MMMM") +
                           "/" + tradeDate.ToString("ddMMyyyy") + "/" + depositoryPariticipant.DPShortName + " " +
                           tradeDate.ToString("ddMMyyyy");
                if (data.Rows.Count > 0)
                {
                    var param = new Dictionary<string, object> { { "isClientWise", false } };
                    ReportHelper.SaveReport(path, data, "Pdf", "rptDPTradeSummary.rpt", depositoryPariticipant.DPShortName + " Bill " + tradeDate.ToString("ddMMyyyy"), param);
                }
            }
            return Json(new { Status = true, Message = "Dp wise bill generate successfully." }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GenerateDpNetting(string transactionDate, int dpId)
        {
            var location = ConfigurationManager.AppSettings["GeneratedDPFile"];
            var tradeDate = ReportHelper.FormatDate(transactionDate);
            var dp = brokerDepositoryParticipatoryService.GetAll().ToList();
            if (dpId > 0)
                dp = dp.Where(x => x.Id == dpId).ToList();
            foreach (var depositoryPariticipant in dp)
            {
                var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE_FROM = tradeDate.ToString("dd MMM yyyy"),
                        TRANS_DATE_TO = tradeDate.ToString("dd MMM yyyy"),
                        DP_ID = depositoryPariticipant.Id,
                        DP_BRANCH_ID = 0,
                    },
                    "SP_RPT_DP_NETTING").Tables[0];
                var path = location + tradeDate.Year + "/" + tradeDate.ToString("MMMM") +
                           "/" + tradeDate.ToString("ddMMyyyy") + "/" + depositoryPariticipant.DPShortName + " " +
                           tradeDate.ToString("ddMMyyyy");
                if (data.Rows.Count > 0)
                {
                    ReportHelper.SaveReport(path, data, "Pdf", "rptDPNetting.rpt", depositoryPariticipant.DPShortName + " Netting " + tradeDate.ToString("ddMMyyyy"));
                }
            }
            return Json(new { Status = true, Message = "Dp wise netting generate successfully." }, JsonRequestBehavior.AllowGet);
        }
        // ReSharper disable once InconsistentNaming
        public JsonResult GenerateDpDSETradeFile(string transactionDate, int dpId)
        {
            var location = ConfigurationManager.AppSettings["GeneratedDPFile"];
            var tradeDate = ReportHelper.FormatDate(transactionDate);
            var dp = brokerDepositoryParticipatoryService.GetAll().ToList();
            if (dpId > 0)
                dp = dp.Where(x => x.Id == dpId).ToList();
            foreach (var depositoryPariticipant in dp)
            {
                var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE = tradeDate.ToString("dd MMM yyyy"),
                        DP_ID = depositoryPariticipant.Id,
                        DP_BRANCH_ID = 0,
                        MARKET = "DSE"
                    },
                    "SP_DP_TRADE_DATA_TEXT_FORMAT").Tables[0];
                var path = location + tradeDate.Year + "/" + tradeDate.ToString("MMMM") +
                           "/" + tradeDate.ToString("ddMMyyyy") + "/" + depositoryPariticipant.DPShortName + " " +
                           tradeDate.ToString("ddMMyyyy");
                if (data.Rows.Count > 0)
                {
                    ReportHelper.SaveDatatableInTextFormat(path, data, tradeDate.ToString("yyyyMMdd") + "_" + depositoryPariticipant.DPShortName + "_Data", "~");
                }
            }
            return Json(new { Status = true, Message = "Dp wise DSE trade file generate successfully." }, JsonRequestBehavior.AllowGet);
        }

        // ReSharper disable once InconsistentNaming
        public JsonResult GenerateDpCSETradeFile(string transactionDate, int dpId)
        {
            var location = ConfigurationManager.AppSettings["GeneratedDPFile"];
            var tradeDate = ReportHelper.FormatDate(transactionDate);
            var dp = brokerDepositoryParticipatoryService.GetAll().ToList();
            if (dpId > 0)
                dp = dp.Where(x => x.Id == dpId).ToList();
            foreach (var depositoryPariticipant in dp)
            {
                var data =
                spService.GetDataWithParameter(
                    new
                    {
                        TRANS_DATE = tradeDate.ToString("dd MMM yyyy"),
                        DP_ID = depositoryPariticipant.Id,
                        DP_BRANCH_ID = 0,
                        MARKET = "CSE"
                    },
                    "SP_DP_TRADE_DATA_TEXT_FORMAT").Tables[0];
                var path = location + tradeDate.Year + "/" + tradeDate.ToString("MMMM") +
                           "/" + tradeDate.ToString("ddMMyyyy") + "/" + depositoryPariticipant.DPShortName + " " +
                           tradeDate.ToString("ddMMyyyy");
                if (data.Rows.Count > 0)
                {
                    ReportHelper.SaveDatatableInTextFormat(path, data, "BT_WITH_TRADE_FLAG", "|");
                    data.Columns.RemoveAt(data.Columns.Count - 1);
                    ReportHelper.SaveDatatableInTextFormat(path, data, "bt", "|");
                }
            }
            return Json(new { Status = true, Message = "Dp wise CSE bt file generate successfully." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePayInPayOutFile()
        {
            ViewBag.FileLocation = ConfigurationManager.AppSettings["GeneratedPayInOutFile"];
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("FileGeneration", "GeneratePayInPayOutFile");
            ViewBag.Markets = marketInfoService.GetAll().Where(x => x.IsActive).ToList();
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

        public JsonResult GeneratePayInOut(string transactionDate, int marketId, int ispayIn)
        {
            var location = ConfigurationManager.AppSettings["GeneratedPayInOutFile"];
            var tradeDate = ReportHelper.FormatDate(transactionDate);
            var market = marketInfoService.GetById(marketId);
            var dpCode = brokerDepositoryParticipatoryService.GetAll().FirstOrDefault(x => x.PayInOut).DPCode;
            var data =
            spService.GetDataWithParameter(
                new
                {
                    TRANSACTION_DATE = tradeDate.ToString("dd MMM yyyy"),
                    MARKET_ID = marketId
                },
                (ispayIn == 1 ? "SP_EXPORT_PAY_IN" : "SP_EXPORT_PAY_OUT")).Tables[0];
            var path = location + "CDBL Data " + tradeDate.ToString("ddMMyyyy") + "/Broker/" +
                       tradeDate.ToString("ddMMMyyyy") + "/" + (ispayIn == 1 ? "PayIn" : "PayOut");
            var count = 0;
            if (Directory.Exists(path))
                count = Directory.GetFiles(path).Select(Path.GetFileName).Count();
            count++;
            if (data.Rows.Count > 0)
            {
                ReportHelper.SaveDatatableInTextFormat(path, data, (ispayIn == 1 ? "01" : "02") + dpCode.PadLeft(6, '0') + tradeDate.ToString("ddMMyyyy"), "", count.ToString().PadLeft(2, '0'));
            }
            return Json(new { Status = true, Message = "CDBL Pay " + (ispayIn == 1 ? "In" : "Out") + " (" + market.MarketShortName + ") file generate successfully." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateIPOFile()
        {
            ViewBag.FileLocation = ConfigurationManager.AppSettings["GeneratedIPOFile"];
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("FileGeneration", "GenerateIPOFile");
            var declarations = ipoDeclarationService.GetAll().ToList();
            foreach (var declaration in declarations)
            {
                declaration.CompanyName = companyInfoService.GetById(declaration.CompanyId).CompanyName;
            }
            ViewBag.Declarations = declarations;
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

        public JsonResult ExportIPOFile(int declarationId, int isSummary, int isNRB, string format, int isAll)
        {
            var message = "";
            if (isAll == 1)
            {
                GenerateIPO(declarationId, 0, 0, "pdf");
                GenerateIPO(declarationId, 0, 0, "txt");
                GenerateIPO(declarationId, 0, 1, "txt");
                GenerateIPO(declarationId, 1, 0, "pdf");
                message = "IPO file generate successfully.";
            }
            else
            {
                message = GenerateIPO(declarationId, isSummary, isNRB, format);

            }
            return Json(new { Status = true, Message = message }, JsonRequestBehavior.AllowGet);
        }

        public string GenerateIPO(int declarationId, int isSummary, int isNRB, string format)
        {
            var location = ConfigurationManager.AppSettings["GeneratedIPOFile"];
            var declaration = ipoDeclarationService.GetById(declarationId);
            var market = marketInfoService.GetById(declaration.MarketId);
            var trecCode = brokerInfoService.GetAll().FirstOrDefault(x => x.IsActive).BrokerCode.PadLeft(3, '0');
            trecCode = trecCode.Substring(trecCode.Length - 3);
            var companyCode = companyInfoService.GetById(declaration.CompanyId).TradeIdDSE;
            var data =
            spService.GetDataWithParameter(
                new
                {
                    IPO_DECLARATION_ID = declarationId,
                    IS_SUMMARY = isSummary,
                    IS_EXPORT = 1,
                    IS_NRB = isNRB,
                    FORMAT = format.ToLower(),
                    IPO_ORDER = 0
                },
                "SP_EXPORT_IPO_FILE").Tables[0];
            var exportFileName = "";
            var reportName = "";
            var param = new Dictionary<string, object>{};
            if (isSummary == 1)
            {
                exportFileName = companyCode + "_Summary_" + market.MarketShortName + "_" + trecCode;
                reportName = "rptIPOApplicationSummary.rpt";
                param.Add("isOrder", 0);
            }
            else if (isNRB == 1)
                exportFileName = companyCode + "_" + market.MarketShortName + "_NRB_" + trecCode;
            else
            {
                if (format.ToLower() == "txt")
                    exportFileName = companyCode + "_" + market.MarketShortName + "_" + trecCode;
                else
                {
                    exportFileName = companyCode + "_Details_" + market.MarketShortName + "_" + trecCode;
                    reportName = "rptIPOApplicationDetail.rpt";
                }
            }
            var path = location + companyCode;
            var message = "No data found.";
            if (data.Rows.Count > 0)
            {
                if (format.ToLower() == "txt")
                    ReportHelper.SaveDatatableInTextFormat(path, data, exportFileName, "~", format);
                else
                {
                    ReportHelper.SaveReport(path, data, "Pdf", reportName, exportFileName, param);
                }
                message = "IPO file generate successfully.";
            }
            return message;
        }
        public ActionResult GenerateLimitFile()
        {
            ViewBag.FileLocation = ConfigurationManager.AppSettings["GeneratedLimitFile"];
            ViewBag.ExcellLimitFileLocation = ConfigurationManager.AppSettings["ExcellLimitFile"];
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("FileGeneration", "GenerateLimitFile");
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
        public JsonResult ExportLimitFile(int type, int balanceType, int fromExcell)
        {
            var location = ConfigurationManager.AppSettings["GeneratedLimitFile"];
            var spName = "";
            var exportFileName = "";
            object param = new { };

            if (fromExcell == 1 && (type == 1 || type == 3))
            {
                var excellPath = ConfigurationManager.AppSettings["ExcellLimitFile"];
                if (Directory.Exists(excellPath))
                {
                    var aDirectory = new DirectoryInfo(excellPath);
                    if (aDirectory.GetFiles().ToList().Count == 0)
                    {
                        return Json(new { Status = "ERROR", Message = "There are no file in directory." },
                            JsonRequestBehavior.AllowGet);
                    }
                    if (aDirectory.GetFiles().ToList().Count > 1)
                    {
                        return Json(new { Status = "ERROR", Message = "There are more than one file in directory." },
                            JsonRequestBehavior.AllowGet);
                    }
                    var file = aDirectory.GetFiles()
                        .ToList()
                        .FirstOrDefault(
                            x =>
                                x.Name.Split('.')[1].ToLower() == "xls" ||
                                x.Name.Split('.')[1].ToLower() == "xlsx");
                    if (file == null)
                    {
                        return Json(new { Status = "ERROR", Message = "There are no excell file in directory." },
                            JsonRequestBehavior.AllowGet);
                    }
                    var excellData = ReportHelper.ConvertExcelltoDataTable(excellPath, file.Name);
                    if (excellData.Rows.Count == 0)
                    {
                        return
                            Json(
                                new
                                {
                                    Status = "ERROR",
                                    Message = "There are no data in <b>Sheet1</b> of the excell file."
                                },
                                JsonRequestBehavior.AllowGet);
                    }
                    tradeHelper.CopyDataFromFileToDatabase(excellData, "FileUserDefinedInvestorLimit");
                }
                else
                {
                    return Json(new { Status = "ERROR", Message = "Directory and file not found." }, JsonRequestBehavior.AllowGet);
                }
            }

            if (type == 1)
            {
                spName = "SP_EXPORT_CSE_CASH_LIMIT_FILE";
                param = new { IS_DEALER = 0, IS_MATURED_BALANCE = balanceType, IS_FROM_EXCELL = fromExcell };
                location += "CSE/";
                exportFileName = SessionHelper.OrganizationShortName + "#Cash_" + ReportHelper.FormatDate(SessionHelper.BusinessDate).ToString("ddMMyyyy");
            }
            else if (type == 2)
            {
                spName = "SP_EXPORT_CSE_STOCK_LIMIT_FILE";
                param = new { IS_DEALER = 0 };
                location += "CSE/";
                exportFileName = SessionHelper.OrganizationShortName + "#Stock_" + ReportHelper.FormatDate(SessionHelper.BusinessDate).ToString("ddMMyyyy");
            }
            else if (type == 3)
            {
                spName = "SP_EXPORT_CSE_CASH_LIMIT_FILE";
                param = new { IS_DEALER = 1, IS_MATURED_BALANCE = balanceType, IS_FROM_EXCELL = fromExcell };
                location += "CSE/";
                exportFileName = SessionHelper.OrganizationDealerCode + "#Cash_" + ReportHelper.FormatDate(SessionHelper.BusinessDate).ToString("ddMMyyyy") + "_Dealer";
            }
            else if (type == 4)
            {
                spName = "SP_EXPORT_CSE_STOCK_LIMIT_FILE";
                param = new { IS_DEALER = 1 };
                location += "CSE/";
                exportFileName = SessionHelper.OrganizationDealerCode + "#Stock_" + ReportHelper.FormatDate(SessionHelper.BusinessDate).ToString("ddMMyyyy") + "_Dealer";
            }
            var data = spService.GetDataWithParameter(param, spName).Tables[0];
            ReportHelper.SaveDatatableInTextFormat(location, data, exportFileName, "|");
            return Json(new { Status = "SUCCESS", Message = "File successfully generated." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenerateClientRegistration()
        {
            ViewBag.FileLocation = ConfigurationManager.AppSettings["GeneratedRegistrationFile"];
            ViewBag.BusinessDate = spService.GetBusinessDay();
            var moduleid = spService.GetSecurityModuleByControllerAction("FileGeneration", "GenerateClientRegistration");
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
        public JsonResult ExportClientRegistrationFile(int type, string dateFrom, string dateTo)
        {
            var trxDateFrom = ReportHelper.FormatDateToString(dateFrom);
            var trxDateTo = ReportHelper.FormatDateToString(dateTo);
            var location = ConfigurationManager.AppSettings["GeneratedRegistrationFile"];
            var spName = "";
            var exportFileName = "";
            object param = new { };
            if (type == 1)
            {
                spName = "SP_EXPORT_CSE_CLIENT_REGISTRATION";
                location += "CSE/";
                exportFileName = SessionHelper.OrganizationShortName + "#Client_Info_" + ReportHelper.FormatDate(dateTo).ToString("ddMMyyyy");
                param = new { TRANSACTION_DATE_FROM = trxDateFrom, TRANSACTION_DATE_TO = trxDateTo, IS_DEALER = 0 };
            }
            else if (type == 2)
            {
                spName = "SP_EXPORT_CSE_CLIENT_REGISTRATION";
                location += "CSE/";
                exportFileName = SessionHelper.OrganizationDealerCode + "#Client_Info_" + ReportHelper.FormatDate(dateTo).ToString("ddMMyyyy");
                param = new { TRANSACTION_DATE_FROM = trxDateFrom, TRANSACTION_DATE_TO = trxDateTo, IS_DEALER = 1 };
            }
            var data = spService.GetDataWithParameter(param, spName).Tables[0];
            if (data.Rows.Count == 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            ReportHelper.SaveDatatableInTextFormat(location, data, exportFileName, "~");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}