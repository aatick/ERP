using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Web.bd.com.mobireach.user;
using ERP.Web.ViewModels;

namespace ERP.Web.Helpers
{
    public class ReportHelper
    {
        public static void PrintReport(string reportName, DataTable dataSource, Dictionary<string, object> parameters)
        {
            try
            {

                var crDocument = new ReportDocument();

                var crExportOptions = new ExportOptions();
                var crDiskFileDestination = new DiskFileDestinationOptions();
                string strFName;
                //All CR file assumed that it resides in the reports folder....
                var strReportPathAbsolute = HttpContext.Current.Server.MapPath("~/Reports/" + reportName);
                crDocument.Load(strReportPathAbsolute);
                crDocument.SetDataSource(dataSource);

                foreach (var kvp in parameters)
                {
                    crDocument.SetParameterValue(kvp.Key, kvp.Value);

                }
                strFName = HttpContext.Current.Server.MapPath("~/") + string.Format("{0}.pdf", Guid.NewGuid());
                crDiskFileDestination.DiskFileName = strFName;
                crExportOptions = crDocument.ExportOptions;
                crExportOptions.DestinationOptions = crDiskFileDestination;
                crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                crDocument.Export();
                crDocument.Dispose();
                crDocument.Close();
                //Response.ClearContent();
                // Response.ClearHeaders();
                // Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", strFName));
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.WriteFile(strFName);
                HttpContext.Current.Response.End();
                // Response.Close();
                System.IO.File.Delete(strFName);

            }
            catch (Exception ex)
            {


            }

        }

        public static void PrintWithSubReport(string reportName, DataTable dataSource, Dictionary<string, object> parameters, Dictionary<string, DataTable> subReportDatasources, ReportClass reportClass)
        {
            try
            {


                // ReportDocument crDocument = new ReportDocument();

                var crExportOptions = new ExportOptions();
                var crDiskFileDestination = new DiskFileDestinationOptions();
                string strFName;
                //All CR file assumed that it resides in the reports folder....
                var strReportPathAbsolute = HttpContext.Current.Server.MapPath("~/Reports/" + reportName);
                reportClass.Load(strReportPathAbsolute);
                reportClass.SetDataSource(dataSource);

                foreach (var kvp in parameters)
                {
                    reportClass.SetParameterValue(kvp.Key, kvp.Value);

                }
                if (subReportDatasources != null)
                {
                    foreach (var kvp in subReportDatasources)
                    {
                        //string strSReportPathAbsolute = HttpContext.Current.Server.MapPath("~/Reports/" + kvp.Key);
                        //crDocument.OpenSubreport(strSReportPathAbsolute).SetDataSource(kvp.Value);
                        reportClass.OpenSubreport(kvp.Key).SetDataSource(kvp.Value);
                    }
                }

                strFName = HttpContext.Current.Server.MapPath("~/") + string.Format("{0}.pdf", Guid.NewGuid());
                crDiskFileDestination.DiskFileName = strFName;
                crExportOptions = reportClass.ExportOptions;
                crExportOptions.DestinationOptions = crDiskFileDestination;
                crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                reportClass.Export();
                reportClass.Dispose();
                reportClass.Close();
                //Response.ClearContent();
                // Response.ClearHeaders();
                // Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", strFName));
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.WriteFile(strFName);
                HttpContext.Current.Response.End();
                // Response.Close();
                System.IO.File.Delete(strFName);

            }
            catch (Exception ex)
            {


            }

        }
        //"<script type='text/javascript'> $(document).ready(function () {$.alert.open('info', 'No data found.'); window.close();});</script>");//"<script type='text/javascript'>$.alert.open('info','No data found.'); window.close();</script>")
        public static void ShowReport(DataTable data, string exportType
            , string reportName, string exportFileName
            , Dictionary<string, object> reportParameterCollection = null
            , Dictionary<int, DataTable> reportSubreportCollection = null
            , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
            )
        {
            if (HttpContext.Current.Response.IsRequestBeingRedirected)
                return;

            if (data.Rows.Count == 0)
            {
                //HttpContext.Current.Response.Write("<script type='text/javascript'>jquery.alert.open('info','No data found.'); window.close();</script>");
                HttpContext.Current.Response.Write("<script type='text/javascript'>alert('No data found.'); window.close();</script>");
                HttpContext.Current.Response.End();
                return;
            }
            var len = sourceFilePath.LastIndexOf(@"\Controllers", System.StringComparison.Ordinal);
            var reportPath = sourceFilePath.Substring(0, len + 1) + @"Reports\" + reportName;
            var report = new ReportDocument();
            report.Load(reportPath);
            report.SetDataSource(data);
            if (reportSubreportCollection != null)
            {
                foreach (var subReportPair in reportSubreportCollection)
                {
                    report.Subreports[subReportPair.Key].SetDataSource(subReportPair.Value);
                }
            }
            if (reportParameterCollection != null)
            {
                foreach (var parameterPair in reportParameterCollection)
                {
                    report.SetParameterValue(parameterPair.Key, parameterPair.Value);
                }
            }
            var exportOption = ExportFormatType.PortableDocFormat;
            switch (exportType.ToLower())
            {
                case "excel":
                    exportOption = ExportFormatType.Excel;
                    break;
                case "exceldata":
                    exportOption = ExportFormatType.ExcelRecord;
                    break;
                case "excelbook":
                    exportOption = ExportFormatType.ExcelWorkbook;
                    break;
                case "word":
                    exportOption = ExportFormatType.WordForWindows;
                    break;
                case "xml":
                    exportOption = ExportFormatType.Xml;
                    break;
            }

            report = SetReportHeader(report, reportName);

            var mode = ConfigurationManager.AppSettings["Mode"];
            HttpContext.Current.Response.BufferOutput = true;
            if (mode == "0")
            {
                SessionHelper.Report = report;
                SessionHelper.ReportFormat = exportOption;
                SessionHelper.ReportExportName = exportFileName;
                HttpContext.Current.Response.Redirect("~/Reports/");
            }
            else if (mode == "1")
            {
                report.ExportToHttpResponse(exportOption, HttpContext.Current.Response, false, exportFileName);
            }
        }
        public static ReportDocument SetReportHeader(ReportDocument report, string reportName)
        {
            report.SummaryInfo.ReportAuthor = HttpContext.Current.Server.MapPath(reportName == "rptBuySaleOrder.rpt" ? SessionHelper.OrganizationBuySaleOrderLogo : SessionHelper.OrganizationLogo);
            report.SummaryInfo.ReportTitle = SessionHelper.OrganizationName;
            report.SummaryInfo.ReportComments = SessionHelper.OrganizationAddress;

            report.PrintOptions.PaperSize = PaperSize.PaperA4;
            report.PrintOptions.ApplyPageMargins(new PageMargins(200, 200, 0, 200));

            if (IsFixedHeaderReport(reportName))
                return report;
            var header = new ReportHeader();
            if (IsLandscapeReport(reportName))
                header =
                    SessionHelper.ReportHeader.Where(x => x.ReportType.ToLower() == "landscape")
                        .ToList()
                        .FirstOrDefault();
            else
                header =
                    SessionHelper.ReportHeader.Where(x => x.ReportType.ToLower() == "portrait")
                        .ToList()
                        .FirstOrDefault();


            var companyName = (FieldObject)report.ReportDefinition.ReportObjects["CompanyName"];
            var companyNameFont = new Font("Arial", header.CompanyNameFontSize, header.CompanyNameBold ? FontStyle.Bold : FontStyle.Regular);
            companyName.ApplyFont(companyNameFont);

            companyName.ObjectFormat.EnableCanGrow = true;
            companyName.Color = Color.Black;
            companyName.Left = header.CompanyNameLeft;
            companyName.Top = header.CompanyNameTop;
            companyName.Height = header.CompanyNameHeight;
            companyName.Width = header.CompanyNameWidth;
            if (header.CompanyNameAlign.ToLower() == "center")
                companyName.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign;
            if (header.CompanyNameAlign.ToLower() == "left")
                companyName.ObjectFormat.HorizontalAlignment = Alignment.LeftAlign;
            if (header.CompanyNameAlign.ToLower() == "right")
                companyName.ObjectFormat.HorizontalAlignment = Alignment.RightAlign;
            if (header.CompanyNameAlign.ToLower() == "justified")
                companyName.ObjectFormat.HorizontalAlignment = Alignment.Justified;

            var companyAddress = (FieldObject)report.ReportDefinition.ReportObjects["CompanyAddress"];
            var companyAddressFont = new Font("Arial", header.CompanyAddressFontSize, header.CompanyAddressBold ? FontStyle.Bold : FontStyle.Regular);
            companyAddress.ApplyFont(companyAddressFont);
            companyAddress.ObjectFormat.EnableCanGrow = true;
            companyAddress.Color = Color.Black;
            companyAddress.Left = header.CompanyAddressLeft;
            companyAddress.Top = header.CompanyAddressTop;
            companyAddress.Height = header.CompanyAddressHeight;
            companyAddress.Width = header.CompanyAddressWidth;
            if (header.CompanyAddressAlign.ToLower() == "center")
                companyAddress.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign;
            if (header.CompanyAddressAlign.ToLower() == "left")
                companyAddress.ObjectFormat.HorizontalAlignment = Alignment.LeftAlign;
            if (header.CompanyAddressAlign.ToLower() == "right")
                companyAddress.ObjectFormat.HorizontalAlignment = Alignment.RightAlign;
            if (header.CompanyAddressAlign.ToLower() == "justified")
                companyAddress.ObjectFormat.HorizontalAlignment = Alignment.Justified;

            var reportsName = (TextObject)report.ReportDefinition.ReportObjects["ReportName"];
            var reportNameFont = new Font("Arial", header.ReportNameFontSize, header.ReportNameBold ? FontStyle.Bold : FontStyle.Regular);
            reportsName.ApplyFont(reportNameFont);
            reportsName.ObjectFormat.EnableCanGrow = true;
            reportsName.Color = Color.Black;
            reportsName.Left = header.ReportNameLeft;
            reportsName.Top = header.ReportNameTop;
            reportsName.Height = header.ReportNameHeight;
            reportsName.Width = header.ReportNameWidth;
            if (header.ReportNameAlign.ToLower() == "center")
                reportsName.ObjectFormat.HorizontalAlignment = Alignment.HorizontalCenterAlign;
            if (header.ReportNameAlign.ToLower() == "left")
                reportsName.ObjectFormat.HorizontalAlignment = Alignment.LeftAlign;
            if (header.ReportNameAlign.ToLower() == "right")
                reportsName.ObjectFormat.HorizontalAlignment = Alignment.RightAlign;
            if (header.ReportNameAlign.ToLower() == "justified")
                reportsName.ObjectFormat.HorizontalAlignment = Alignment.Justified;

            var firstLine = (TextObject)report.ReportDefinition.ReportObjects["FirstLine"];

            firstLine.Left = header.FirstLineLeft;
            firstLine.Top = header.FirstLineTop;
            firstLine.Width = header.FirstLineWidth;
            firstLine.ObjectFormat.EnableSuppress = header.FirstLineSuppress;

            var secondLine = (TextObject)report.ReportDefinition.ReportObjects["SecondLine"];
            secondLine.Left = header.SecondLineLeft;
            secondLine.Top = header.SecondLineTop;
            secondLine.Width = header.SecondLineWidth;
            secondLine.ObjectFormat.EnableSuppress = header.SecondLineSuppress;

            var companyLogo = (PictureObject)report.ReportDefinition.ReportObjects["CompanyLogo"];

            companyLogo.Left = header.CompanyLogoLeft;
            companyLogo.Top = header.CompanyLogoTop;
            companyLogo.Height = header.CompanyLogoHeight;
            companyLogo.Width = header.CompanyLogoWidth;



            return report;
        }

        public static void SaveReport(
            string directory, object data, string exportType
            , string reportName, string exportFileName
            , Dictionary<string, object> reportParameterCollection = null
            , Dictionary<int, DataTable> reportSubreportCollection = null
            , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
            )
        {
            var len = sourceFilePath.LastIndexOf(@"\Controllers", System.StringComparison.Ordinal);
            var report = new ReportDocument();
            report.Load(sourceFilePath.Substring(0, len + 1) + @"Reports\" + reportName);
            report.SetDataSource(data);

            if (reportSubreportCollection != null)
            {
                foreach (var subReportPair in reportSubreportCollection)
                {
                    report.Subreports[subReportPair.Key].SetDataSource(subReportPair.Value);
                }
            }
            if (reportParameterCollection != null)
            {
                foreach (var parameterPair in reportParameterCollection)
                {
                    report.SetParameterValue(parameterPair.Key, parameterPair.Value);
                }
            }
            var exportOption = ExportFormatType.PortableDocFormat;
            var ext = "pdf";
            switch (exportType.ToLower())
            {
                case "excel":
                    exportOption = ExportFormatType.Excel;
                    ext = "xls";
                    break;
                case "exceldata":
                    exportOption = ExportFormatType.ExcelRecord;
                    ext = "xls";
                    break;
                case "excelbook":
                    exportOption = ExportFormatType.ExcelWorkbook;
                    ext = "xlsx";
                    break;
                case "word":
                    exportOption = ExportFormatType.WordForWindows;
                    ext = "doc";
                    break;
                case "xml":
                    exportOption = ExportFormatType.Xml;
                    ext = "xml";
                    break;
            }

            report = SetReportHeader(report, reportName);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var filePath = directory + "/" + exportFileName + "." + ext;
            if (File.Exists(filePath))
                File.Delete(filePath);
            using (var stream = report.ExportToStream(exportOption))
            {
                var fileStream = File.Create(filePath, (int)stream.Length);
                var bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, bytesInStream.Length);
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                fileStream.Close();
            }
            report.Dispose();
        }

        public static void SaveDatatableInTextFormat(string directory, DataTable data, string exportFileName, string delimeter, string format = "txt")
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var filePath = directory + "/" + exportFileName + "." + format;
            if (File.Exists(filePath))
                File.Delete(filePath);
            using (var file = File.CreateText(filePath))
            {
                for (var row = 0; row < data.Rows.Count; row++)
                {
                    var stringToWrite = string.Join(delimeter, data.Rows[row].ItemArray);
                    file.WriteLine(stringToWrite);
                }
            }
        }

        public static DateTime FormatDate(string date)
        {
            return !string.IsNullOrEmpty(date) ? DateTime.ParseExact(date, "dd/MM/yyyy", null) : default(DateTime);
        }

        public static string FormatDateToString(string date)
        {
            return !string.IsNullOrEmpty(date) ? DateTime.ParseExact(date, "dd/MM/yyyy", null).ToString("dd MMM yyyy") : "";
        }

        public static DateTime? FormatNullableDate(string date)
        {
            return !string.IsNullOrEmpty(date) ? DateTime.ParseExact(date, "dd/MM/yyyy", null) : (DateTime?)null;
        }
        public static SMS SendSMS(string mobileNo, string message)
        {
            var service = new SmsControllerService();
            var returnValues = service.SendTextMessage(SessionHelper.SMSUserName, SessionHelper.SMSPassword, SessionHelper.SMSMobileNo, mobileNo, message);
            return new SMS() { Message = message, MobileNo = mobileNo, SMSCount = string.IsNullOrEmpty(returnValues.SMSCount) ? 0 : int.Parse(returnValues.SMSCount), MessageId = string.IsNullOrEmpty(returnValues.MessageID) ? "0" : returnValues.MessageID, ErrorCode = string.IsNullOrEmpty(returnValues.ErrorCode) ? 0 : int.Parse(returnValues.ErrorCode) };
        }
        public static DataTable ConvertExcelltoDataTable(string path, string fileName)
        {
            var connString = "";
            var filePath = path + fileName;
            var extension = fileName.ToLower().Split('.')[1];
            if (extension.Trim() == "xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (extension.Trim() == "xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            var oledbConn = new OleDbConnection(connString);
            var data = new DataTable();
            try
            {

                oledbConn.Open();
                using (var cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    var oleda = new OleDbDataAdapter { SelectCommand = cmd };
                    var ds = new DataSet();
                    oleda.Fill(ds);
                    data = ds.Tables[0];
                }
            }
            catch
            {
            }
            finally
            {
                oledbConn.Close();
            }

            return data;

        }

        public static bool CheckSoftwareExpiration()
        {
            var isExpired = 0;
            try
            {
                var sqlcon = ConfigurationManager.ConnectionStrings["ERPDbContext"].ConnectionString;
                var sqlConnection = new SqlConnection(sqlcon);
                sqlConnection.Open();
                var sql = "OPEN SYMMETRIC KEY ERP_SYMMETRIC_KEY DECRYPTION BY CERTIFICATE ERP_CERTIFICATE";
                sql +=
                    " IF CONVERT(DATE,GETDATE(),106)>CONVERT(DATE,CAST(DecryptByKey((SELECT TOP 1 SoftwareExpirationKey FROM Organization)) AS VARCHAR(MAX)),106)";
                sql += " SELECT 1";
                sql += " ELSE";
                sql += " SELECT 0";
                var sqlCommand = new SqlCommand(sql, sqlConnection);
                var dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    isExpired = int.Parse(dataReader[0].ToString());
                }
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                isExpired = 1;
            }
            return isExpired == 0;
        }
        public static void SendEmail(string email, string subject, string body, Stream file = null, string fileDescription = null)
        {
            var senderEmail = SessionHelper.OrgEmail;
            var emailPassword = SessionHelper.OrgEmailPassword;
            var organizationName = SessionHelper.OrganizationShortName;
            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail, organizationName),
                Subject = subject,
                Body = body
            };
            mail.To.Add(email);
            if (file != null && !string.IsNullOrEmpty(fileDescription))
            {
                var attachment = new Attachment(file, fileDescription);
                mail.Attachments.Add(attachment);
            }
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(senderEmail, emailPassword),
                Timeout = 20000
            };
            smtp.Send(mail);
        }

        public static void EmailReport(
            string email, string subject, string body, object data
            , string exportType, string reportName, string exportFileName
            , Dictionary<string, object> reportParameterCollection = null
            , Dictionary<int, DataTable> reportSubreportCollection = null
            , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
            )
        {
            var len = sourceFilePath.LastIndexOf(@"\Controllers", System.StringComparison.Ordinal);
            var report = new ReportDocument();
            report.Load(sourceFilePath.Substring(0, len + 1) + @"Reports\" + reportName);
            report.SetDataSource(data);

            if (reportSubreportCollection != null)
            {
                foreach (var subReportPair in reportSubreportCollection)
                {
                    report.Subreports[subReportPair.Key].SetDataSource(subReportPair.Value);
                }
            }
            if (reportParameterCollection != null)
            {
                foreach (var parameterPair in reportParameterCollection)
                {
                    report.SetParameterValue(parameterPair.Key, parameterPair.Value);
                }
            }
            var exportOption = ExportFormatType.PortableDocFormat;
            var ext = "pdf";
            switch (exportType.ToLower())
            {
                case "excel":
                    exportOption = ExportFormatType.Excel;
                    ext = "xls";
                    break;
                case "exceldata":
                    exportOption = ExportFormatType.ExcelRecord;
                    ext = "xls";
                    break;
                case "excelbook":
                    exportOption = ExportFormatType.ExcelWorkbook;
                    ext = "xlsx";
                    break;
                case "word":
                    exportOption = ExportFormatType.WordForWindows;
                    ext = "doc";
                    break;
                case "xml":
                    exportOption = ExportFormatType.Xml;
                    ext = "xml";
                    break;
            }

            report = SetReportHeader(report, reportName);

            using (var stream = report.ExportToStream(exportOption))
            {
                SendEmail(email, subject, body, stream, exportFileName + "." + ext);
            }
            report.Dispose();
        }

        public static bool IsLandscapeReport(string reportName)
        {
            var landscapeReports = new[]
            {
                "rpt_acc_trial_balance_New.rpt", "rpt_BrokerInfo.rpt", "Rpt_Get_IPO_DraftInformation.rpt",
                "Rpt_MarginBuyNonMarginSaleReport.rpt", "rptChargeInfo.rpt", "RptCheckCollectionReport.rpt",
                "rptChequeReceiveInfo.rpt", "rptDayWiseInomeStatement.rpt", "rptInvestorGeneralReport.rpt",
                "rptShareBuySaleOnSameTime.rpt","rptCashDividendInvestorWise.rpt"
            };
            return landscapeReports.Contains(reportName);
        }

        public static bool IsFixedHeaderReport(string reportName)
        {
            var fixedHeaderReports = new[]
            {
                "Rpt_Ipo_ApplicationForm.rpt", "rptBuySaleOrder.rpt", "RptCollectionMoneyreceipt.rpt",
                "RptIpoApplicationForm.rpt", "RptPaymentMoneyreceipt.rpt"
            };
            return fixedHeaderReports.Contains(reportName);
        }
    }
}