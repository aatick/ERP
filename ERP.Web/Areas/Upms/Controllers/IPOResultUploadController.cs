//using UcasPortfolio.Data;

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;
using Utility;
using Common.Service;

namespace Upms.Controllers
{
    public class IPOResultUploadController : BaseController
    {

        private readonly ISPService spService;
        private readonly IIPODeclarationService ipoDeclarationService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerInfoService brokerInfoService;
        private readonly ITradeUploadFileInformationService tradeUploadFileInformationService;
        private static string sqlcon = ConfigurationManager.ConnectionStrings["ERPDbContext"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(sqlcon);
        public IPOResultUploadController(ISPService spService, IIPODeclarationService ipoDeclarationService
                , IMarketInfoService marketInfoService, IBrokerInfoService brokerInfoService
                , ITradeUploadFileInformationService tradeUploadFileInformationService)
        {
            this.spService = spService;
            this.ipoDeclarationService = ipoDeclarationService;
            this.marketInfoService = marketInfoService;
            this.brokerInfoService = brokerInfoService;
            this.tradeUploadFileInformationService = tradeUploadFileInformationService;
        }

        //public JsonResult FileNameValidation(string TradingCode, string MarketName, string BrokerCode, int IPODeclarationId)
        //{
        //    var result = false;

        //    var param = new { TradingCode = TradingCode, MarketName = MarketName, BrokerCode = BrokerCode, DeclarationId = IPODeclarationId };
        //    var abc = spService.GetDataWithParameter(param, "IPOResultUploadFileValidation");
        //    if (abc.Tables[0].Rows.Count >= 1)
        //    {
        //        if (abc.Tables[0].Rows[0][0].ToString() == "1" && abc.Tables[0].Rows[0][1].ToString() == "1")
        //        {
        //            result = abc.Tables[0].Rows[0][2].ToString() == TradingCode ? true : false;
        //        }
        //        else
        //        {
        //            result = "0";
        //        }
        //    }
        //    else
        //    {
        //        Result = "0";
        //    }

        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}

        public void InsertMonthlyReportData()
        {
            var param = new { UploadUserId = SessionHelper.LoggedInUserId };
            spService.GetDataWithParameter(param, "InsertMonthlyReportData");
        }

        //public void CopyDataFromFileToDatabase(DataTable table, string tableName)
        //{
        //    sqlConnection.Open();
        //    new SqlCommand("TRUNCATE TABLE " + tableName, sqlConnection).ExecuteNonQuery();
        //    var sqlBulkCopy = new SqlBulkCopy(sqlConnection)
        //    {
        //        BatchSize = table.Rows.Count,
        //        DestinationTableName = tableName
        //    };
        //    sqlBulkCopy.WriteToServer(table);
        //    sqlConnection.Close();
        //}

        //private List<Message> CopyTradeFileData(DataTable table, string databaseTableName)
        //{
        //    var messageList = new List<Message>();
        //    CopyDataFromFileToDatabase(table, databaseTableName);
        //    return messageList;
        //}

        public JsonResult UploadIPOFile(HttpPostedFileBase uploadedFile, int id, int fileId)
        {
            var tradeHelper = DependencyResolver.Current.GetService<TradeHelperController>();
            var name = uploadedFile.FileName.Split('.')[0].Split('_');
            if (name.Length != 3)
                return Json(new Message { Code = 0, Text = "Invalid IPO result file name." }, JsonRequestBehavior.AllowGet);
            var tradingCode = name[0];
            var marketName = name[1];
            var brokerCode = name[2];
            var declaration = ipoDeclarationService.GetById(id);
            var market = marketInfoService.GetById(declaration.MarketId);
            var broker = brokerInfoService.GetAll().FirstOrDefault(x => x.IsActive);
            var bCode = market.MarketShortName == "DSE" ? broker.BrokerCode.PadLeft(3, '0') : broker.BrokerCodeCSE.PadLeft(3, '0');
            bCode = bCode.Substring(bCode.Length - 3);
            if (declaration.TradingCode != tradingCode && market.MarketShortName != marketName || brokerCode != bCode)
                return Json(new Message { Code = 0, Text = "Invalid IPO result file name." }, JsonRequestBehavior.AllowGet);

            var fileName = uploadedFile.FileName;
            var file = tradeUploadFileInformationService.GetById(fileId);
            var extension = "." + file.Extension;
            if (!fileName.ToLower().EndsWith(extension))
            {
                return Json(new Message() { Code = 0, Text = "Wrong file extension." }, JsonRequestBehavior.AllowGet);
            }
            if (uploadedFile.ContentType.ToLower() != "text/plain")
                return Json(new Message() { Code = 0, Text = "Wrong file format." }, JsonRequestBehavior.AllowGet);

            var line = "";
            var splitChar = file.Delimeter;
            var stream = new StreamReader(uploadedFile.InputStream);
            var dataTable = new DataTable(file.MappingTableName);
            var arrColumnName = spService.GetDataWithParameter(new { table_name = file.MappingTableName }, "SP_COLUMNS").Tables[0].AsEnumerable().Select(item => new { ColumnName = item.Field<string>("COLUMN_NAME") });
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

            var messageList = tradeHelper.CopyTradeFileData(dataTable, file.MappingTableName, file.Id, spService.GetBusinessDay().ToString("dd MMM yyyy"), false, fileName);

            if (messageList[0].Code > -1)
                return Json(messageList[0], JsonRequestBehavior.AllowGet);

            var messages = tradeHelper.RunProcessProcedures(fileId,
                new
                {
                    FILE_ID = file.Id,
                    USER_ID = SessionHelper.LoggedInUserId,
                    TRANSACTION_DATE = spService.GetBusinessDay().ToString("dd MMM yyyy"),
                    DECLARATION_ID = id
                });

            return Json(messages[0], JsonRequestBehavior.AllowGet);

        }





        //
        // GET: /IPOResultUpload/
        public ActionResult Index()
        {
            var ipoCon = DependencyResolver.Current.GetService<IPOController>();
            ViewBag.Companies = ipoCon.IPODeclarationCompany();
            var moduleid = spService.GetSecurityModuleByControllerAction("IPOResultUpload", "Index");
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
                    IsActive = x.Field<bool>(12)
                }).ToList();
            ViewBag.FileList = accessedFileList;
            return View();
        }

        //
        // GET: /IPOResultUpload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /IPOResultUpload/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /IPOResultUpload/Create
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

        //
        // GET: /IPOResultUpload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /IPOResultUpload/Edit/5
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

        //
        // GET: /IPOResultUpload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /IPOResultUpload/Delete/5
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
    }
}
