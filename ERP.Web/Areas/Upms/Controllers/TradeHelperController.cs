using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Service;
using Utility;

namespace Upms.Controllers
{
    public class TradeHelperController : BaseController
    {
        private readonly ISPService spService;
        private readonly ITradeUploadFileInformationService tradeUploadFileInformationService;
        private readonly ICompanyInfoService companyInfoService;
        private static string sqlcon = ConfigurationManager.ConnectionStrings["ERPDbContext"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(sqlcon);
        public TradeHelperController(ISPService spService, ICompanyInfoService companyInfoService, ITradeUploadFileInformationService tradeUploadFileInformationService)
        {
            this.spService = spService;
            this.tradeUploadFileInformationService = tradeUploadFileInformationService;
            this.companyInfoService = companyInfoService;
        }
        public List<Message> RunUploadProcedures(string fileName, int fileId, string transactionDate)
        {
            var messageList = new List<Message>();
            var sqlUpload = tradeUploadFileInformationService.GetById(fileId).UploadProcedureName;
            if (string.IsNullOrEmpty(sqlUpload)) return messageList;
            messageList = spService.GetDataWithParameter(
                    new { FILE_ID = fileId, USER_ID = SessionHelper.LoggedInUserId, TRANS_DATE = transactionDate },
                    sqlUpload).Tables[0].AsEnumerable()
                    .Select(x => new Message() { Code = x.Field<int>(0), Text = x.Field<string>(1) })
                    .ToList();
            return messageList;
        }
        public void ReloadSession_NotProcessedFile()
        {
            var fileStatus = spService.GetDataWithParameter(new
            {
                TRANSACTION_DATE = spService.GetBusinessDay().ToString("dd MMM yyyy")
            }, "SP_GET_NOT_PROCESSED_FILE_NAME");
            SessionHelper.NotProcessedFile = "";
            if (fileStatus.Tables.Count > 0)
            {
                if (fileStatus.Tables[0].Rows.Count > 0)
                {
                    SessionHelper.NotProcessedFile = fileStatus.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        public List<Message> RunProcessProcedures(int fileId, object param)
        {
            var messageList = new List<Message>();
            var sqlProcess = tradeUploadFileInformationService.GetById(fileId).ProcessProcedureName;
             if (string.IsNullOrEmpty(sqlProcess)) return messageList;
            messageList = spService.GetDataWithParameter(param, sqlProcess).Tables[0].AsEnumerable()
                    .Select(x => new Message() { Code = x.Field<int>(0), Text = x.Field<string>(1) })
                    .ToList();
            return messageList;
        }

        public void CopyDataFromFileToDatabase(DataTable table, string tableName)
        {
            sqlConnection.Open();
            new SqlCommand("TRUNCATE TABLE " + tableName, sqlConnection).ExecuteNonQuery();
            var sqlBulkCopy = new SqlBulkCopy(sqlConnection)
            {
                BatchSize = table.Rows.Count,
                DestinationTableName = tableName
            };
            sqlBulkCopy.WriteToServer(table);
            sqlConnection.Close();
        }

        public List<Message> CopyTradeFileData(DataTable table, string databaseTableName, int fileId, string transactionDate, bool isTradeFile = false, string fileName = "")
        {
            var trans_date = DateTime.Parse(transactionDate);
            var businessDay = spService.GetBusinessDay();
            var messageList = new List<Message>();
            messageList =
                spService.GetDataWithParameter(new { TransactionDate = trans_date.ToString("dd MMM yyyy"), FileId = fileId, BUSINESS_DATE = businessDay.ToString("dd MMM yyyy") },
                    "SP_GET_FILE_UPLOAD_STATUS").Tables[0].AsEnumerable()
                    .Select(item => new Message() { Code = item.Field<int>(0), Text = item.Field<string>(1) }).ToList();

            if (messageList[0].Code > -1)
                return messageList;

            CopyDataFromFileToDatabase(table, databaseTableName);

            if (isTradeFile)
            {
                messageList =
                    spService.GetDataWithoutParameter("SP_CHECK_TRADE_DATA").Tables[0].AsEnumerable()
                        .Select(item => new Message() { Code = item.Field<int>(0), Text = item.Field<string>(1) })
                        .ToList();
                if (messageList.Count > 0)
                    return messageList;
            }
            messageList = RunUploadProcedures(fileName, fileId, transactionDate);
            return messageList;
        }
    }
}