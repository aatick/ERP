using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;
using Utility;
using ReportHelper = ERP.Web.Helpers.ReportHelper;

namespace Upms.Controllers
{
    public class TradeController : BaseController
    {
        #region Variables
        private readonly ISPService spService;
        private readonly ITradeUploadFileInformationService tradeUploadFileInformationService;
        private readonly ICompanyInfoService companyInfoService;
        private readonly IMarketGroupService marketGroupService;
        private readonly IMarketSectorService marketSectorService;
        private readonly ITradeTransactionTypeService tradeTransactionTypeService;
        private static string sqlcon = ConfigurationManager.ConnectionStrings["UcasPortfolioDBContext"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(sqlcon);
        private TradeHelperController tradeHelper = DependencyResolver.Current.GetService<TradeHelperController>();
        public TradeController(ISPService spService, ICompanyInfoService companyInfoService, ITradeUploadFileInformationService tradeUploadFileInformationService
               , IMarketGroupService marketGroupService, IMarketSectorService marketSectorService
            , ITradeTransactionTypeService tradeTransactionTypeService)
        {
            this.spService = spService;
            this.tradeUploadFileInformationService = tradeUploadFileInformationService;
            this.companyInfoService = companyInfoService;
            this.marketGroupService = marketGroupService;
            this.marketSectorService = marketSectorService;
            this.tradeTransactionTypeService = tradeTransactionTypeService;
        }
        #endregion

        #region UploadTradeFiles
        //start upload trade files
        public ActionResult Upload()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("Trade", "Upload");
            var fileList = spService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                FileModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_FILE").Tables[0];
            var accessedFileList = new List<TradeUploadFileInformation>();
            if (fileList.Rows.Count > 0)
                accessedFileList = fileList.AsEnumerable().Select(x => new TradeUploadFileInformation()
                {
                    Id = x.Field<int>(0),
                    AspNetSecurityModuleId = x.Field<int>(1),
                    FileName = x.Field<string>(2),
                    FileDescription = x.Field<string>(3),
                    Delimeter = x.Field<string>(4),
                    Prefix = x.Field<string>(5),
                    Extension = x.Field<string>(6),
                    ContainingText = x.Field<string>(7),
                    SerialNo = x.Field<int>(8),
                    DependentOnFileId = x.Field<int?>(9),
                    MappingTableName = x.Field<string>(10),
                    CDBLFileName = x.Field<string>(11),
                    IsActive = x.Field<bool>(12),
                    HasProcess = x.Field<bool>(13),
                    FileType = x.Field<string>(14)
                }).Where(x => x.FileType == "TRADE" || x.FileType == "PRICE").ToList();
            ViewBag.FileList = accessedFileList;

            ViewBag.BusinessDate = spService.GetBusinessDay();
            return View();
        }

        public JsonResult UploadTradeFile(string transactionDate, int fileType, HttpPostedFileBase uploadedFile, int serial)
        {
            var messageList = new List<Message>();
            if (!ReportHelper.CheckSoftwareExpiration())
            {
                messageList.Add(new Message() { Code = -2, Text = "Software has been expired due to payment issue.<br/><br/> Please contact to your vendor." });
                return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var fileName = uploadedFile.FileName;
                var file = tradeUploadFileInformationService.GetById(fileType);
                var extension = "." + file.Extension;
                if (!fileName.ToLower().EndsWith(extension))
                {
                    messageList.Add(new Message() { Code = -2, Text = "Wrong file extension." });
                    return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                }
                var prefix = string.IsNullOrEmpty(file.Prefix) ? "" : file.Prefix.ToUpper();
                if (!string.IsNullOrEmpty(prefix))
                {
                    if (prefix == "DATE")
                    {
                        if (
                            !uploadedFile.FileName.ToUpper()
                                .StartsWith(ReportHelper.FormatDate(transactionDate).ToString("yyyyMMdd")))
                        {
                            messageList.Add(new Message() { Code = -2, Text = "Wrong File Name." });
                            return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
                var arrText = file.ContainingText.ToLower().Split(' ');
                if (!arrText.All(uploadedFile.FileName.ToLower().Contains))
                {
                    messageList.Add(new Message() { Code = -2, Text = "Wrong File Name." });
                    return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                }
                if (extension.ToLower() == ".xml" && (serial == 1 || serial == 3))
                {
                    var dataset = new DataSet();
                    dataset.ReadXml(uploadedFile.InputStream);
                    if (dataset.Tables.Count == 0)
                    {
                        messageList.Add(new Message() { Code = -2, Text = "Data not found in file." });
                        return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                    }
                    if (dataset.Tables[0].Rows.Count == 0)
                    {
                        messageList.Add(new Message() { Code = -2, Text = "Data not found in file." });
                        return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                    }
                    if (dataset.Tables[0].Columns.Contains("Date"))
                    {
                        if (dataset.Tables[0].Rows[0]["Date"].ToString() !=
                           ReportHelper.FormatDate(transactionDate).ToString("yyyyMMdd"))
                        {
                            messageList.Add(new Message() { Code = -2, Text = "Trade Date and File Data Trade Date Mismatch." });
                            return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if (dataset.Tables[0].Rows[0]["TradeDate"].ToString() !=
                            ReportHelper.FormatDate(transactionDate).ToString("yyyyMMdd"))
                        {
                            messageList.Add(new Message() { Code = -2, Text = "Trade Date and File Data Trade Date Mismatch." });
                            return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    messageList = tradeHelper.CopyTradeFileData(dataset.Tables[0], file.MappingTableName, fileType, ReportHelper.FormatDateToString(transactionDate), true, fileName);
                }
                else
                {
                    if (serial == 2)
                    {
                        var line = "";
                        var splitChar = file.Delimeter;
                        var stream = new StreamReader(uploadedFile.InputStream);
                        var dataTable = new DataTable(file.MappingTableName);
                        var arrColumnName =
                            spService.GetDataWithParameter(new { table_name = file.MappingTableName }, "SP_COLUMNS").Tables[0]
                                .AsEnumerable().Select(item => new { ColumnName = item.Field<string>("COLUMN_NAME") });
                        foreach (var column in arrColumnName)
                        {
                            dataTable.Columns.Add(column.ColumnName);
                        }

                        while ((line = stream.ReadLine()) != null)
                        {
                            var arrLine = line.Split(char.Parse(splitChar));
                            var dataRow = dataTable.NewRow();
                            dataRow.ItemArray = arrLine;
                            dataTable.Rows.Add(dataRow);
                        }
                        stream.Close();
                        var date = ReportHelper.FormatDate(transactionDate).ToString("dd/MM/yyyy");
                        if (dataTable.Rows[0]["TransactionDate"].ToString() != date)
                        {
                            messageList.Add(new Message()
                            {
                                Code = -2,
                                Text = "Trade Date and File Data Trade Date Mismatch."
                            });
                            return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                        }
                        messageList = tradeHelper.CopyTradeFileData(dataTable, file.MappingTableName, fileType,
                            ReportHelper.FormatDate(transactionDate).ToString("yyyy-MM-dd"), true, fileName);
                    }
                    else if (serial == 4)
                    {
                        var line = "";
                        var splitChar = file.Delimeter;
                        var stream = new StreamReader(uploadedFile.InputStream);
                        var dataTable = new DataTable(file.MappingTableName);
                        var arrColumnName =
                            spService.GetDataWithParameter(new { table_name = file.MappingTableName }, "SP_COLUMNS").Tables[0]
                                .AsEnumerable().Select(item => new { ColumnName = item.Field<string>("COLUMN_NAME") });
                        foreach (var column in arrColumnName)
                        {
                            dataTable.Columns.Add(column.ColumnName);
                        }
                        var market = "";
                        var category = "";
                        var start = false;
                        DateTime trxDate = DateTime.Today;
                        while ((line = stream.ReadLine()) != null)
                        {
                            var isValid = true;
                            if (line.ToUpper().Contains("TODAY'S SHARE MARKET :"))
                            {
                                isValid = false;
                                trxDate = DateTime.ParseExact(line.ToUpper().Replace("TODAY'S SHARE MARKET :", "").Trim(), "yyyy-MM-dd", null);
                            }
                            if (line.ToUpper().Contains("PRICES IN PUBLIC TRANSACTIONS"))
                            {
                                start = true;
                                market = "P";
                                isValid = false;
                            }
                            if (line.ToUpper().Contains("PRICES IN SPOT TRANSACTIONS (Treasury BONDs)"))
                            {
                                start = false;
                                market = "S";
                                isValid = false;
                            }
                            if (line.ToUpper().Contains("PRICES IN BLOCK TRANSACTIONS"))
                            {
                                start = false;
                                market = "B";
                                isValid = false;
                            }
                            if (line.ToUpper().Contains("TOP 10 GAINERS"))
                            {
                                start = false;
                                isValid = false;
                            }
                            if (start && line.ToUpper().StartsWith("A CATEGORY (EQUITY)"))
                            {
                                category = "A";
                                isValid = false;
                            }
                            if (start && line.ToUpper().StartsWith("B CATEGORY (EQUITY)"))
                            {
                                category = "B";
                                isValid = false;
                            }
                            if (start && line.ToUpper().StartsWith("N CATEGORY (EQUITY)"))
                            {
                                category = "N";
                                isValid = false;
                            }
                            if (start && line.ToUpper().StartsWith("Z CATEGORY (EQUITY)"))
                            {
                                category = "Z";
                                isValid = false;
                            }
                            if (line.StartsWith("Z Category (Equity) scrips traded in Public Market"))
                            {
                                category = "";
                                isValid = false;
                            }
                            if (line.StartsWith(" ") || line.ToUpper().StartsWith("ALL CATEGORY")
                                || line.ToUpper().StartsWith("A CATEGORY") || line.ToUpper().StartsWith("B CATEGORY")
                                || line.ToUpper().StartsWith("N CATEGORY") || line.ToUpper().StartsWith("Z CATEGORY")
                                || line.ToUpper().StartsWith("MUTUAL") || line.ToUpper().StartsWith("CORPORATE")
                                || line.ToUpper().StartsWith("TREASURY BOND") || line.ToUpper().StartsWith("TOTAL")
                                || line.ToUpper().StartsWith("MARKET") || line.ToUpper().StartsWith("INSTR CODE")
                                || line.ToUpper().StartsWith("*") || string.IsNullOrEmpty(line.Trim())
                                || line.ToUpper().StartsWith("-") || line.ToUpper().StartsWith("|") || line.ToUpper().StartsWith("="))
                            {
                                isValid = false;
                            }
                            if (isValid && start)
                            {
                                //var arrLine = line.Split(char.Parse(splitChar));
                                var code = line.Substring(0, 10).Trim();
                                var close = line.Substring(37, 9).Trim();
                                var open = line.Substring(10, 9).Trim();
                                var high = line.Substring(19, 9).Trim();
                                var low = line.Substring(28, 9).Trim();
                                var var = line.Substring(46, 7).Trim();
                                var company = companyInfoService.GetAll().FirstOrDefault(x => x.TradeIdDSE == code);
                                var cat = "";
                                var sec = "";
                                var isin = "";
                                if (company != null)
                                {
                                    var group = marketGroupService.GetAll().FirstOrDefault(x => x.Id == company.GroupId);
                                    if (group != null)
                                        cat = group.ShortName;
                                    var sector = marketSectorService.GetAll().FirstOrDefault(x => x.Id == company.SectorId);
                                    if (sector != null)
                                        sec = sector.ShortName;
                                    isin = company.ISINNo;
                                }

                                var strArr = new string[]
                                {
                                    code,isin,"",(market=="S"?"Y":"N"),trxDate.ToString("yyyyMMdd")
                                    ,close,open,high,low,"0",var
                                    ,(string.IsNullOrEmpty(category)?cat:category),sec
                                };

                                var dataRow = dataTable.NewRow();
                                dataRow.ItemArray = strArr;
                                dataTable.Rows.Add(dataRow);
                            }

                        }
                        stream.Close();
                        var date = ReportHelper.FormatDate(transactionDate).ToString("yyyyMMdd");
                        if (dataTable.Rows[0]["TradeDate"].ToString() != date)
                        {
                            messageList.Add(new Message()
                            {
                                Code = -2,
                                Text = "Trade Date and File Data Trade Date Mismatch."
                            });
                            return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
                            //return Json(messageList, JsonRequestBehavior.AllowGet);
                        }
                        messageList = tradeHelper.CopyTradeFileData(dataTable, file.MappingTableName, fileType,
                            ReportHelper.FormatDate(transactionDate).ToString("yyyy-MM-dd"), true, fileName);
                    }
                }
                tradeHelper.ReloadSession_NotProcessedFile();
                return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                messageList.Add(new Message()
                {
                    Code = -2,
                    Text = ex.InnerException != null ? ex.InnerException.Message : ex.Message
                });
                return Json(new { Messages = messageList, NotProcessedFile = SessionHelper.NotProcessedFile }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetErrorTrade(int errorId)
        {
            var data = spService.GetDataWithParameter(new { ERROR_TYPE = errorId },
                "SP_GET_ERROR_TRADE_FROM_UPLOADED_TRADE_DATA");
            return Json(data.Tables[0].AsEnumerable().Select(item => new { InvestorCode = item.Field<string>("InvestorCode"), BOID = item.Field<string>("BOID"), SecurityCode = item.Field<string>("SecurityCode"), ISIN = item.Field<string>("ISIN"), Quantity = item.Field<string>("Quantity"), Price = item.Field<string>("Price"), TraderCode = item.Field<string>("TraderCode") }).ToList(), JsonRequestBehavior.AllowGet);
        }
        //end upload trade files
        #endregion

        #region UploadCDBLFiles
        //start upload cdbl files
        public ActionResult UploadCDBLFile()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("Trade", "UploadCDBLFile");
            ViewBag.FileList = spService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                FileModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_FILE").Tables[0].AsEnumerable().Select(x => new TradeUploadFileInformation()
            {
                Id = x.Field<int>(0),
                AspNetSecurityModuleId = x.Field<int>(1),
                FileName = x.Field<string>(2),
                FileDescription = x.Field<string>(3),
                Delimeter = x.Field<string>(4),
                Prefix = x.Field<string>(5),
                Extension = x.Field<string>(6),
                ContainingText = x.Field<string>(7),
                SerialNo = x.Field<int>(8),
                DependentOnFileId = x.Field<int?>(9),
                MappingTableName = x.Field<string>(10),
                CDBLFileName = x.Field<string>(11),
                IsActive = x.Field<bool>(12),
                HasProcess = x.Field<bool>(13),
                FileType = x.Field<string>(14)
            }).Where(x => x.FileType == "CDBL").ToList();
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.CDBLFileLocation = ConfigurationManager.AppSettings["CDBLFileLocation"];
            return View();
        }
        public JsonResult UploadFile(string directory, List<int> files)
        {
            var messages = new List<Message>();
            try
            {
                var selectedFiles = tradeUploadFileInformationService.GetAll().Where(x => files.Contains(x.Id)).ToList();
                if (!Directory.Exists(directory))
                {
                    messages.Add(new Message() { Code = -2, Text = "Directory Not Found." });
                    return Json(messages, JsonRequestBehavior.AllowGet);
                }
                var aDirectory = new DirectoryInfo(directory);
                if (aDirectory.GetDirectories().ToList().Count == 0)
                {
                    messages.Add(new Message() { Code = -2, Text = "There are no folder in directory." });
                    return Json(messages, JsonRequestBehavior.AllowGet);
                }
                foreach (var aFolder in aDirectory.GetDirectories())
                {
                    DateTime transDate;
                    if (!DateTime.TryParseExact(aFolder.Name, "ddMMMyyyy", null, DateTimeStyles.None, out transDate))
                    {
                        messages.Add(new Message() { Code = -2, Text = "Invalid folders in directory." });
                        break;
                    }
                    if (aFolder.GetFiles().ToList().Count == 0) continue;
                    foreach (var aFile in aFolder.GetFiles())
                    {
                        var transactionDate = DateTime.ParseExact(aFolder.Name, "ddMMMyyyy", null);
                        var file = selectedFiles.FirstOrDefault(x => x.CDBLFileName.ToUpper() == aFile.Name.ToUpper());
                        if (file == null) continue;
                        var extension = "." + file.Extension.ToLower();
                        if (!aFile.Name.ToLower().EndsWith(extension)) continue;
                        var line = "";
                        var splitChar = file.Delimeter;
                        var stream = new StreamReader(aFile.FullName);
                        var dataTable = new DataTable(file.MappingTableName);
                        var arrColumnName = spService.GetDataWithParameter(new { table_name = file.MappingTableName }, "SP_COLUMNS").Tables[0].AsEnumerable().Select(item => new { ColumnName = item.Field<string>("COLUMN_NAME") });
                        foreach (var column in arrColumnName)
                        {
                            dataTable.Columns.Add(column.ColumnName);
                        }
                        while ((line = stream.ReadLine()) != null)
                        {
                            var arrLine = line.Split(char.Parse(splitChar));
                            var maxLenght = file.CDBLMaxColumnLength;
                            if (maxLenght > 0)
                            {
                                var arr = arrLine.ToList();
                                for (var i = 0; i < arr.Count; i++)
                                {
                                    if ((i + 1) > maxLenght)
                                        arr.RemoveAt(i);
                                }
                                arr.Add(transactionDate.ToString("dd-MMM-yyyy"));
                                arrLine = arr.ToArray();
                            }
                            var dataRow = dataTable.NewRow();
                            dataRow.ItemArray = arrLine;
                            dataTable.Rows.Add(dataRow);
                        }
                        stream.Close();
                        var date = transactionDate.ToString("dd-MMM-yyyy");
                        var checkdate = "";
                        switch (aFile.Name.ToUpper())
                        {
                            case "08DP01UX.TXT":
                                checkdate = dataTable.Rows[0]["OpeningDate"].ToString();
                                break;
                            default:
                                checkdate = dataTable.Rows[0]["TransactionDate"].ToString();
                                break;
                        }
                        if (checkdate != date) continue;
                        var messageList = tradeHelper.CopyTradeFileData(dataTable, file.MappingTableName, file.Id, transactionDate.ToString("dd MMM yyyy"), false, aFile.Name);
                        //var messageList = CopyTradeFileData(dataTable, file.MappingTableName, file.Id, transactionDate.ToString("dd MMM yyyy"), false, aFile.Name);
                        messages.AddRange(messageList);
                        if (messageList.FirstOrDefault(x => x.Code == 0 || x.Code == 1) != null) break;
                    }
                    if (messages.FirstOrDefault(x => x.Code == 0 || x.Code == 1) != null) break;
                }
                if (messages.Count == 0)
                    messages.Add(new Message() { Code = -2, Text = "No file found in folders." });
                tradeHelper.ReloadSession_NotProcessedFile();
                return Json(messages.OrderBy(x => x.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                messages.Add(new Message() { Code = -2, Text = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
                return Json(messages.OrderBy(x => x.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        //end upload cdbl files
        #endregion

        #region ProcessTradeFiles
        //start process Trade Files
        public ActionResult ProcessTradeFile()
        {
            ViewBag.BusinessDate = spService.GetBusinessDay();
            return View();
        }
        public JsonResult ProcessTradeFiles(string transDate)
        {
            var messageList =
                spService.GetDataWithParameter(new { TransactionDate = spService.GetBusinessDay().ToString("dd MMM yyyy") },
                    "SP_GET_DAY_PROCESS_STATUS").Tables[0].AsEnumerable()
                    .Select(item => new Message() { Code = item.Field<int>(0), Text = item.Field<string>(1) }).ToList();
            if (messageList[0].Code > -1)
                return Json(messageList, JsonRequestBehavior.AllowGet);
            var trxDate = ReportHelper.FormatDateToString(transDate);
            messageList = spService.GetDataWithParameter(
                new { USER_ID = SessionHelper.LoggedInUserId, TRANS_DATE = trxDate },
                "SP_PROCESS_TRADE_FILE").Tables[0].AsEnumerable()
                .Select(x => new Message() { Code = x.Field<int>(0), Text = x.Field<string>(1) })
                .ToList();
            tradeHelper.ReloadSession_NotProcessedFile();
            return Json(messageList, JsonRequestBehavior.AllowGet);
        }
        //end process Trade Files
        #endregion

        #region ProcessCDBLFiles
        //start process CDBL Files
        public ActionResult ProcessCDBLFiles()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("Trade", "ProcessCDBLFiles");
            ViewBag.FileList = spService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                FileModuleId = moduleid,
                ForProcess = 1
            }, "SP_GET_USER_ACCESSED_FILE").Tables[0].AsEnumerable().Select(x => new TradeUploadFileInformation()
            {
                Id = x.Field<int>(0),
                AspNetSecurityModuleId = x.Field<int>(1),
                FileName = x.Field<string>(2),
                FileDescription = x.Field<string>(3),
                Delimeter = x.Field<string>(4),
                Prefix = x.Field<string>(5),
                Extension = x.Field<string>(6),
                ContainingText = x.Field<string>(7),
                SerialNo = x.Field<int>(8),
                DependentOnFileId = x.Field<int?>(9),
                MappingTableName = x.Field<string>(10),
                CDBLFileName = x.Field<string>(11),
                IsActive = x.Field<bool>(12),
                HasProcess = x.Field<bool>(13),
                FileType = x.Field<string>(14)
            }).Where(x => x.HasProcess && x.FileType == "CDBL").ToList();
            ViewBag.BusinessDate = spService.GetBusinessDay();
            ViewBag.CDBLFileLocation = ConfigurationManager.AppSettings["CDBLFileLocation"];
            return View();
        }
        public JsonResult ProcessFile(List<int> files, string transDate)
        {
            var messageList =
                spService.GetDataWithParameter(new { TransactionDate = spService.GetBusinessDay().ToString("dd MMM yyyy") },
                    "SP_GET_DAY_PROCESS_STATUS").Tables[0].AsEnumerable()
                    .Select(item => new Message() { Code = item.Field<int>(0), Text = item.Field<string>(1) }).ToList();

            if (messageList[0].Code > -1)
                return Json(messageList, JsonRequestBehavior.AllowGet);

            var messages = new List<Message>();
            try
            {
                var trxDate = ReportHelper.FormatDateToString(transDate);
                var selectedFiles = tradeUploadFileInformationService.GetAll().Where(x => files.Contains(x.Id)).ToList();

                foreach (var file in selectedFiles)
                {
                    messageList = tradeHelper.RunProcessProcedures(file.Id,
                        new { FILE_ID = file.Id, USER_ID = SessionHelper.LoggedInUserId, TRANSACTION_DATE = trxDate });
                    messages.AddRange(messageList);
                }
                spService.ExecuteStoredProcedure(
                        new { USER_ID = SessionHelper.LoggedInUserId, TRANS_DATE = trxDate },
                        "SP_INSERT_TRANSACTION_DAILY_FROM_CDBL_CHARGES");
                tradeHelper.ReloadSession_NotProcessedFile();
            }
            catch (Exception ex)
            {
                messages.Add(new Message() { Code = -2, Text = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
        //end process CDBL Files
        #endregion

        #region ShareInRateSetup

        //start Share in rate set up
        public ActionResult ShareInRateSetup()
        {
            ViewBag.Files = tradeUploadFileInformationService.GetAll().Where(x => x.IsShareIn).ToList();
            ViewBag.Companies = companyInfoService.GetAll().ToList();
            return View();
        }

        public JsonResult GetShareInInformation(int fileId, string transactionDate, int companyId, string code, string bo)
        {
            var file = tradeUploadFileInformationService.GetById(fileId);
            var date = ReportHelper.FormatDateToString(transactionDate);
            var data =
                spService.GetDataWithParameter(
                    new { TRANSACTION_DATE = date, INVESTOR_CODE = code, BOID = bo, COMPANY_ID = companyId },
                    file.GetProcedureName).Tables[0].AsEnumerable().Select(x => new
                    {
                        Id = x.Field<int>(0),
                        Date = x.Field<string>(1),
                        InvestorCode = x.Field<string>(2),
                        Name = x.Field<string>(3),
                        Company = x.Field<string>(4),
                        Type = x.Field<string>(5),
                        Quantity = x.Field<int>(6),
                        Rate = x.Field<decimal>(7)
                    }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetShareInRate(int fileId, decimal rate, string id)
        {
            var file = tradeUploadFileInformationService.GetById(fileId);
            spService.ExecuteStoredProcedure(
                new { TABLE_NAME = file.RateSetupTableName, DEFINED_RATE = rate, DEFINED_ID = id },
                "SP_SET_CDBL_SHARE_IN_RATE");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //end Share in rate set up
        #endregion

        #region ReceiveCashDividend
        public ActionResult ReceiveCashDividend()
        {
            ViewBag.Companies = companyInfoService.GetAll().ToList();
            return View();
        }

        public JsonResult GetCashDividend(int companyId, int isCashDividend)
        {
            var data = spService.GetDataWithParameter(new
            {
                COMPANY_ID = companyId,
                IS_CASH_DIVIDEND = isCashDividend
            }, "SP_GET_CASH_DIVIDEND_FRACTION_AMOUNT").Tables[0].AsEnumerable().Select(x => new
            {
                InvestorCode = x.Field<string>(0),
                InvestorName = x.Field<string>(1),
                BOID = x.Field<string>(2),
                CompanyCode = x.Field<string>(3),
                Remarks = x.Field<string>(4),
                Amount = x.Field<decimal>(5),
                Rate = x.Field<string>(6),
                NetAmount = x.Field<decimal>(7),
                InvestorId = x.Field<int>(8),
                RecordDate = x.Field<DateTime>(9).ToString("dd-MMM-yyyy")
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ReceiveDividend(List<CashDividend> data, int cashtype)
        {
            try
            {
                foreach (var dividend in data)
                {
                    spService.ExecuteStoredProcedure(new
                    {
                        IS_CASH_DIVIDEND = cashtype,
                        INVESTOR_ID = dividend.InvestorId,
                        COMPANY_ID = dividend.CompanyId,
                        RECORD_DATE = dividend.RecordDate,
                        NET_AMOUNT = dividend.NetAmount,
                        RATE = dividend.Rate,
                        USER_ID = SessionHelper.LoggedInUserId,
                        TRANS_DATE = ReportHelper.FormatDateToString(SessionHelper.BusinessDate)
                    }, "SP_PROCESS_SINGLE_CASH_DIVIDEND_FRACTION");
                }

                spService.ExecuteStoredProcedure(new
                {
                    IS_CASH_DIVIDEND = cashtype,
                    COMPANY_ID = data[0].CompanyId,
                    RECORD_DATE = data[0].RecordDate,
                    USER_ID = SessionHelper.LoggedInUserId
                }, "SP_EXPIRED_CASH_DIVIDEND_FRACTION");
                return Json(new { Status = "SUCCESS", Message = "Successfully received." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = "ERROR", Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region RollBackUploadedFiles
        public ActionResult RollbackTradeFiles()
        {
            ViewBag.FileList = spService.GetDataWithoutParameter("SP_GET_UPLOADED_FILES_FOR_ROLLBACK")
                .Tables[0].AsEnumerable()
                .Select(x => new TradeUploadFileInformation()
                {
                    Id = x.Field<int>(0),
                    AspNetSecurityModuleId = x.Field<int>(1),
                    FileName = x.Field<string>(2),
                    FileDescription = x.Field<string>(3),
                    Delimeter = x.Field<string>(4),
                    Prefix = x.Field<string>(5),
                    Extension = x.Field<string>(6),
                    ContainingText = x.Field<string>(7),
                    SerialNo = x.Field<int>(8),
                    DependentOnFileId = x.Field<int?>(9),
                    MappingTableName = x.Field<string>(10),
                    CDBLFileName = x.Field<string>(11),
                    IsActive = x.Field<bool>(12),
                    HasProcess = x.Field<bool>(13),
                    FileType = x.Field<string>(14)
                }).Where(x => x.FileType == "TRADE").ToList();
            return View();
        }

        public ActionResult RollbackCDBLFiles()
        {
            ViewBag.FileList = spService.GetDataWithoutParameter("SP_GET_UPLOADED_FILES_FOR_ROLLBACK")
                 .Tables[0].AsEnumerable()
                 .Select(x => new TradeUploadFileInformation()
                 {
                     Id = x.Field<int>(0),
                     AspNetSecurityModuleId = x.Field<int>(1),
                     FileName = x.Field<string>(2),
                     FileDescription = x.Field<string>(3),
                     Delimeter = x.Field<string>(4),
                     Prefix = x.Field<string>(5),
                     Extension = x.Field<string>(6),
                     ContainingText = x.Field<string>(7),
                     SerialNo = x.Field<int>(8),
                     DependentOnFileId = x.Field<int?>(9),
                     MappingTableName = x.Field<string>(10),
                     CDBLFileName = x.Field<string>(11),
                     IsActive = x.Field<bool>(12),
                     HasProcess = x.Field<bool>(13),
                     FileType = x.Field<string>(14)
                 }).Where(x => x.FileType == "CDBL").ToList();
            return View();
        }

        public JsonResult RollbackFile(List<int> files, string transDate, int isTradeFile)
        {
            var messageList =
                spService.GetDataWithParameter(new { TransactionDate = spService.GetBusinessDay().ToString("dd MMM yyyy") },
                    "SP_GET_DAY_PROCESS_STATUS").Tables[0].AsEnumerable()
                    .Select(item => new Message() { Code = item.Field<int>(0), Text = item.Field<string>(1) }).ToList();

            if (messageList[0].Code > -1)
                return Json(messageList, JsonRequestBehavior.AllowGet);

            var messages = new List<Message>();
            var trxDate = ReportHelper.FormatDateToString(transDate);
            var selectedFiles = tradeUploadFileInformationService.GetAll().Where(x => files.Contains(x.Id)).ToList();

            foreach (var file in selectedFiles)
            {
                messageList =
                    spService.GetDataWithParameter(new { FILE_ID = file.Id, IS_TRADE_FILE = isTradeFile },
                        "SP_ROLLBACK_UPLOADED_FILE").Tables[0]
                        .AsEnumerable()
                        .Select(x => new Message { Code = x.Field<int>(0), Text = x.Field<string>(1) })
                        .ToList();
                messages.AddRange(messageList);
            }
            tradeHelper.ReloadSession_NotProcessedFile();
            return Json(messages, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}