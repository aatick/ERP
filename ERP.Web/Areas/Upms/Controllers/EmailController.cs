using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Service;

namespace Upms.Controllers
{
    public class EmailController : BaseController
    {
        private readonly ISPService spService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IInvestorTypeService investorTypeService;
        public EmailController(
            ISPService spService
            , IBrokerBranchService brokerBranchService
            , IInvestorTypeService investorTypeService
            )
        {
            this.spService = spService;
            this.brokerBranchService = brokerBranchService;
            this.investorTypeService = investorTypeService;
        }


        #region Method


        public JsonResult SendMailOfTradeConfirmationReport(string AllInvestorId, int EmailSMSTypeId)
        {

            if (EmailSMSTypeId != 3)
            {
                var param = new { AllInvestorId = AllInvestorId, EmailSMSTypeId = EmailSMSTypeId };

                var SendEmail = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_CODE_For_MailSending").Tables[0];

                var SenderMail = SessionHelper.OrgEmail;
                var Password = SessionHelper.OrgEmailPassword;
                var TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(SessionHelper.BusinessDate));
                var UserId = SessionHelper.LoggedInUserId;
                var orgShortName = SessionHelper.OrganizationShortName;
                Parallel.ForEach(SendEmail.AsEnumerable(), dr =>
                {
                    var FileLocation = ConfigurationManager.AppSettings["GeneratedDailyTradeFile"] + TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" + TransactionDate.ToString("dd MMM yyyy");
                    var FileLocationPortfolio = ConfigurationManager.AppSettings["GeneratedDailyPortfolioFile"] + TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" + TransactionDate.ToString("dd MMM yyyy");
                    var InvestorEmail = dr["SystemEmail"].ToString();
                    var InvestorId = dr["InvestorId"].ToString();

                    if (InvestorEmail != "" && InvestorEmail != null)
                    {
                        var EmailType = "";
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(SenderMail, orgShortName);
                        mail.To.Add(InvestorEmail);

                        System.Net.Mail.Attachment attachment;
                        if (EmailSMSTypeId == 1) //Tradefile
                        // if (EmailSMSTypeId == "ETRADE") //Tradefile
                        {
                            EmailType = "ETRADE";
                            mail.Subject = "Trade confirmation of " + dr["InvestorCode"].ToString();

                            mail.Body = "Trade confirmation report.Please check the attachment";

                            attachment = new System.Net.Mail.Attachment(FileLocation + "/Trade_Confirmation_" + dr["InvestorCode"].ToString() + ".pdf");
                            mail.Attachments.Add(attachment);
                        }
                        else //Portfolio
                        {
                            EmailType = "EPORTFOLIO";
                            mail.Subject = "Portfolio of " + dr["InvestorCode"].ToString();

                            mail.Body = "Portfolio report.Please check the attachment.";
                            attachment = new System.Net.Mail.Attachment(FileLocationPortfolio + "/Portfolio_" + dr["InvestorCode"].ToString() + ".pdf");
                            mail.Attachments.Add(attachment);
                        }


                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com"; // the host name
                        smtp.Port = 587; //port number
                        smtp.EnableSsl = true; //whether your smtp server requires SSL
                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new System.Net.NetworkCredential(SenderMail, Password);
                        smtp.Timeout = 20000;
                        smtp.Send(mail);

                        var Eparam = new { INVESTOR_ID = InvestorId, TYPE = EmailType, USER_ID = UserId, MESSAGE = "", MESSAGE_ID = 0, MESSAGE_COUNT = 0 };
                        //var Eparam = new { INVESTOR_ID = dr["InvestorId"], TYPE = EmailType, USER_ID = UserId, MESSAGE = "", MESSAGE_ID = 0, MESSAGE_COUNT = 0 };
                        //var Eparam = new { INVESTOR_ID = 561, TYPE = "ETRADE", USER_ID = 63, MESSAGE = "", MESSAGE_ID = 0, MESSAGE_COUNT = 0 };
                        //spService.GetDataWithParameter(Eparam, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                        spService.ExecuteStoredProcedure(Eparam, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                    }

                    System.Threading.Thread.Sleep(1000);


                });



                return Json(new { Result = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            else  // BO Opening 
            {
                var param1 = new { TransactionDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), AllInvestorId = AllInvestorId };
                var investor = spService.GetDataWithParameter(param1, "GetBO_Opening_Investor_Info").Tables[0];
                var TotInvestor = investor.Rows.Count;
                var FileLocationOpening = "";
                for (var i = 0; i < investor.Rows.Count; i++)
                {
                    var data = spService.GetDataWithParameter(
                        new
                        {
                            InvestorId = investor.Rows[i].Field<int>("InvestorId"),
                        },
                        "AccountSetupConfirmationMesage").Tables[0];
                    if (data.Rows.Count > 0)
                    {
                        FileLocationOpening = ConfigurationManager.AppSettings["GeneratedDailyBOOpeningFile"] +
                                          TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" +
                                          TransactionDate.ToString("dd MMM yyyy");

                        string file = @"" + FileLocationOpening + "/BO_Opening_Confirmation_" + investor.Rows[i].Field<string>("InvestorCode") +
                                      ".pdf";
                        if (Directory.Exists(Path.GetDirectoryName(file)))
                        {
                            System.IO.File.Delete(file);
                        }

                        var type = "pdf";
                        ReportHelper.SaveReport(FileLocationOpening, data, type, "Rpt_AccountSetupConfirmation.rpt",
                            "BO_Opening_Confirmation_" + investor.Rows[i].Field<string>("InvestorCode"));
                    }

                    var SenderMail = SessionHelper.OrgEmail;
                    var Password = SessionHelper.OrgEmailPassword;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(SenderMail, SessionHelper.OrganizationName);
                    mail.To.Add(investor.Rows[i].Field<string>("SystemEmail"));
                    System.Net.Mail.Attachment attachment;
                    mail.Subject = "Account Setup Acknowledgement";

                    mail.Body = "Account Setup Acknowledgement.Please check the attachment";
                    attachment = new System.Net.Mail.Attachment(FileLocationOpening + "/BO_Opening_Confirmation_" + investor.Rows[i].Field<string>("InvestorCode") + ".pdf");
                    mail.Attachments.Add(attachment);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; // the host name
                    smtp.Port = 587; //port number
                    smtp.EnableSsl = true; //whether your smtp server requires SSL
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential(SenderMail, Password);
                    smtp.Timeout = 20000;
                    smtp.Send(mail);

                    //var Eparam = new { INVESTOR_ID = InvestorId, TYPE = EmailType, USER_ID = UserId, MESSAGE = "", MESSAGE_ID = 0, MESSAGE_COUNT = 0 };
                    ////var Eparam = new { INVESTOR_ID = dr["InvestorId"], TYPE = EmailType, USER_ID = UserId, MESSAGE = "", MESSAGE_ID = 0, MESSAGE_COUNT = 0 };
                    ////var Eparam = new { INVESTOR_ID = 561, TYPE = "ETRADE", USER_ID = 63, MESSAGE = "", MESSAGE_ID = 0, MESSAGE_COUNT = 0 };
                    ////spService.GetDataWithParameter(Eparam, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                    //spService.ExecuteStoredProcedure(Eparam, "SP_INSERT_SMS_EMAIL_CONFIRMATION");


                }
                return Json(new { Result = "Ok" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult SaveTradeConfirmationReport(int EmailSMSTypeId, int InvestorGroup)
        {
            var Result = string.Empty;
            var TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(SessionHelper.BusinessDate));
            //var investor = spService.GetDataWithoutParameter("SP_GET_TODAYS_INDIVIDUAL_INVESTOR_CODE").Tables[0];



            if (EmailSMSTypeId == 1)
            {
                var investor = spService.GetDataWithoutParameter("SP_GET_TODAYS_INDIVIDUAL_INVESTOR_CODE_For_TradeConfimation_Generate").Tables[0];
                var TotInvestor = investor.Rows.Count;
                if (TotInvestor > 0)
                {
                    var TradeDate = investor.Rows[0]["TradeDate"].ToString();
                    if (TransactionDate == Convert.ToDateTime(TradeDate))
                    {
                        for (var i = 0; i < investor.Rows.Count; i++)
                        {
                            var data = spService.GetDataWithParameter(
                                new
                                {
                                    TRANS_DATE_FROM = TransactionDate,
                                    TRANS_DATE_TO = TransactionDate,
                                    INVESTOR_CODE = investor.Rows[i].Field<string>(0),
                                    BROKER_BRANCH_ID = 0
                                },
                                "SP_RPT_CLIENT_TRADE_CONFIRMATION_STATEMENT").Tables[0];
                            if (data.Rows.Count > 0)
                            {
                                var FileLocation = ConfigurationManager.AppSettings["GeneratedDailyTradeFile"] +
                                                   TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" +
                                                   TransactionDate.ToString("dd MMM yyyy");

                                string file = @"" + FileLocation + "/Trade_Confirmation_" + investor.Rows[i].Field<string>(0) +
                                              ".pdf";
                                if (Directory.Exists(Path.GetDirectoryName(file)))
                                {
                                    System.IO.File.Delete(file);
                                }

                                var type = "pdf";
                                ReportHelper.SaveReport(FileLocation, data, type, "rptTradeConfirmationStatement.rpt",
                                    "Trade_Confirmation_" + investor.Rows[i].Field<string>(0));
                            }
                        }
                    }
                    else
                    {
                        return Json(Result = "No Trade Found.", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json(Result = "No Trade Found.", JsonRequestBehavior.AllowGet);
                }


            }
            else if (EmailSMSTypeId == 2)
            {
                var paramg = new { InvestorGroup = InvestorGroup };
                var investorPortFolio = spService.GetDataWithParameter(paramg, "SP_GET_TODAYS_INDIVIDUAL_INVESTOR_CODE_For_PortfolioFile_Generate").Tables[0];

                for (var i = 0; i < investorPortFolio.Rows.Count; i++)
                {
                    var data =
                   spService.GetDataWithParameter(
                       new
                       {
                           TRANSACTION_DATE = TransactionDate,
                           INVESTOR_CODE = investorPortFolio.Rows[i].Field<string>(0)
                       },
                       "SP_RPT_INVESTOR_PORTFOLIO").Tables[0];
                    var subData = spService.GetDataWithParameter(
                            new
                            {
                                TRANSACTION_DATE = TransactionDate,
                                INVESTOR_CODE = investorPortFolio.Rows[i].Field<string>(0)
                            },
                            "SP_RPT_INVESTOR_RECEIVABLE_SHARE").Tables[0];


                    var FileLocation = ConfigurationManager.AppSettings["GeneratedDailyPortfolioFile"] + TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" + TransactionDate.ToString("dd MMM yyyy");

                    string file = @"" + FileLocation + "/Portfolio_" + investorPortFolio.Rows[i].Field<string>(0) + ".pdf";
                    //if (Directory.Exists(Path.GetDirectoryName(file)))
                    //{
                    //    System.IO.File.Delete(file);
                    //}

                    var type = "Pdf";

                    var subReport = new Dictionary<int, DataTable> { { 0, subData } };
                    ReportHelper.SaveReport(FileLocation, data, type, "rptInvestorPortfolio.rpt", "Portfolio_" + investorPortFolio.Rows[i].Field<string>(0), null, subReport);

                    //ReportHelper.SaveReport(FileLocation, data, type, "rptTradeConfirmationStatement.rpt", "Trade_Confirmation_" + investor.Rows[i].Field<string>(0), param);

                    //var param = new Dictionary<string, object> { { "logoPath", Server.MapPath("~/images/") } };
                    //var subReport = new Dictionary<int, DataTable> { { 0, subData } };
                    //ReportHelper.ShowReport(data, type, "rptInvestorPortfolio.rpt", "Portfolio_" + asOnDate.ToString("dd_MMM_yyyy") + "_" + code.Replace(",", "_"), param, subReport);
                }

            }
            //else //BO Account Opening
            //{
            //    var param1 = new { TransactionDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate) };
            //    var investor = spService.GetDataWithParameter(param1,"GetBO_Opening_Investor_Info").Tables[0];
            //    var TotInvestor = investor.Rows.Count;

            //            for (var i = 0; i < investor.Rows.Count; i++)
            //            {
            //                var data = spService.GetDataWithParameter(
            //                    new
            //                    {
            //                        InvestorId = investor.Rows[i].Field<int>("Id"),
            //                    }, //GeneratedDailyBOOpeningFile
            //                    "AccountSetupConfirmationMesage").Tables[0];
            //                if (data.Rows.Count > 0)
            //                {
            //                    var FileLocationOpening = ConfigurationManager.AppSettings["GeneratedDailyBOOpeningFile"] +
            //                                       TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" +
            //                                       TransactionDate.ToString("dd MMM yyyy");

            //                    string file = @"" + FileLocationOpening + "/BO_Opening_Confirmation_" + investor.Rows[i].Field<string>("InvestorCode") +
            //                                  ".pdf";
            //                    if (Directory.Exists(Path.GetDirectoryName(file)))
            //                    {
            //                        System.IO.File.Delete(file);
            //                    }

            //                    var type = "pdf";
            //                    var param = new Dictionary<string, object> { { "logoPath", Server.MapPath("~/images/") } };
            //                    ReportHelper.SaveReport(FileLocationOpening, data, type, "Rpt_AccountSetupConfirmation.rpt",
            //                        "BO_Opening_Confirmation_" + investor.Rows[i].Field<string>("InvestorCode"), param);
            //                }
            //            }
            //}
            return Json(Result = "Ok", JsonRequestBehavior.AllowGet);

        }
        // (@TransactionDate DATE)
        public JsonResult GetTradeConfirmationInvestor(int EmailSMSTypeId)
        {
            if (EmailSMSTypeId != 3)
            {
                var param = new { AllInvestorId = "", EmailSMSTypeId = EmailSMSTypeId };
                var InvList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_CODE_For_MailSending").Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     RowSL = row.Field<long>("RowSL"),
                     InvestorId = row.Field<int>("InvestorId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                     SystemEmail = row.Field<string>("SystemEmail")
                 }).ToList();
                return Json(InvList, JsonRequestBehavior.AllowGet);
            }
            else  //BO Opening
            {
                var param = new { TransactionDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), AllInvestorId = "0" };
                var InvList = spService.GetDataWithParameter(param, "GetBO_Opening_Investor_Info").Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     RowSL = row.Field<long>("RowSL"),
                     InvestorId = row.Field<int>("InvestorId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                     SystemEmail = row.Field<string>("SystemEmail")
                 }).ToList();

                return Json(InvList, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FileCount(int EmailSMSTypeId)
        {
            int Count = 0;
            var TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(SessionHelper.BusinessDate));
            var FileLocation = "";
            if (EmailSMSTypeId == 1) //Trade
            {
                FileLocation = ConfigurationManager.AppSettings["GeneratedDailyTradeFile"] + TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" + TransactionDate.ToString("dd MMM yyyy");
            }
            else  //Portfolio
            {
                FileLocation = ConfigurationManager.AppSettings["GeneratedDailyPortfolioFile"] + TransactionDate.Year + "/" + TransactionDate.ToString("MMMM") + "/" + TransactionDate.ToString("dd MMM yyyy");
            }

            string file = @"" + FileLocation + "";//if (!Directory.Exists(@"D:\Abdulla_UploadedFile"))
            if (Directory.Exists(file))//(Directory.Exists(Path.GetDirectoryName(file)))
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"" + FileLocation + "");
                Count = dir.GetFiles().Length;
            }

            return Json(Count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvestorInfo(string code)
        {
            var data = spService.GetDataWithParameter(new { CODE = code }, "SP_GET_INVESTOR_INFO_FOR_GENERAL_EMAIL").Tables[0].AsEnumerable().Select(x => new { InvestorCode = x.Field<string>(0), InvestorName = x.Field<string>(1), Email = x.Field<string>(2), InvestorId = x.Field<int>(3) }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendGeneralEmail(List<Email> emails, string type)
        {
            try
            {
                var userId = SessionHelper.LoggedInUserId;
                foreach (var email in emails)
                {
                    ReportHelper.SendEmail(email.EmailAddress, email.Subject, email.Body);
                    spService.ExecuteStoredProcedure(new
                    {
                        INVESTOR_ID = email.InvestorId,
                        TYPE = type,
                        USER_ID = userId
                    }, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                }
                return Json(new { Status = true, Message = "Email send successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public ActionResult Index()
        {
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            ViewBag.FileLocation = ConfigurationManager.AppSettings["GeneratedDailyTradeFile"];
            return View();
        }
        public ActionResult General()
        {
            ViewBag.BusinessDate = SessionHelper.BusinessDate;
            return View();
        }

    }
}
