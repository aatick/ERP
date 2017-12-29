using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class SMSController : BaseController
    {
        private readonly ISPService spService;
        private readonly IIPODeclarationService iPODeclarationService;
        private readonly IEmailSMSInvestorAccessService emailSMSInvestorAccessService;
        private readonly IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService;
        private readonly IEmailSMSDPAccessService emailSMSDPAccessService;

        public SMSController(ISPService spService, IIPODeclarationService iPODeclarationService,
            IEmailSMSInvestorAccessService emailSMSInvestorAccessService,
            IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService,
            IEmailSMSDPAccessService emailSMSDPAccessService)
        {
            this.spService = spService;
            this.iPODeclarationService = iPODeclarationService;
            this.emailSMSInvestorAccessService = emailSMSInvestorAccessService;
            this.brokerDepositoryParticipatoryService = brokerDepositoryParticipatoryService;
            this.emailSMSDPAccessService = emailSMSDPAccessService;
        }
        public ActionResult Trade()
        {
            ViewBag.BusinessDate = ReportHelper.FormatDate(SessionHelper.BusinessDate);
            return View();
        }
        public ActionResult EmailSMSAccess()
        {
            return View();
        }
        public ActionResult EmailSMSDPAccess()
        {
            ViewBag.DpList = brokerDepositoryParticipatoryService.GetAll();
            return View();
        }
        //

        public JsonResult GetDpAccessInfo(int DpId)
        {
            try
            {
                var DpAccessList = spService.GetDataBySqlCommand("SELECT EmailSMSTypeId FROM EmailSMSDPAccess WHERE DepositoryParticipantId = " + DpId + " AND IsActive = 1").Tables[0]
                .AsEnumerable()
                .Select(row => new
                {
                    EmailSMSTypeId = row.Field<int>("EmailSMSTypeId"),
                  
                }).ToList();
                return Json(DpAccessList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EmaulSMS_Dp_AccessSave(int DpId, List<int> AllTypeId)
        {
            var Result = "";
            try
            {
                spService.GetDataBySqlCommand("UPDATE EmailSMSDPAccess SET IsActive = 0 WHERE DepositoryParticipantId = " + DpId + "");

                if (AllTypeId != null)
                {
                    foreach (var app in AllTypeId)
                    {
                        var accs = emailSMSDPAccessService.GetAll().Where(x => x.DepositoryParticipantId == DpId && x.EmailSMSTypeId == app);
                        if (accs.Count() == 1)
                        {
                            accs.FirstOrDefault().IsActive = true;
                            emailSMSDPAccessService.Update(accs.FirstOrDefault());
                        }
                        else
                        {
                            var access = new EmailSMSDPAccess();

                            access.DepositoryParticipantId = DpId;
                            access.EmailSMSTypeId = app;
                            access.CreateDate = DateTime.Now;
                            access.CreatedUserId = SessionHelper.LoggedInUserId;
                            access.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                            access.IsActive = true;
                            emailSMSDPAccessService.Create(access);
                        }

                    }
                }

                var dpdelaccess = emailSMSDPAccessService.GetAll().Where(x => x.DepositoryParticipantId == DpId && x.IsActive == false);

                if (dpdelaccess.Count() != 0)
                {
                    foreach (var p in dpdelaccess)
                    {
                        var param = new { DepositoryParticipantId = DpId, EmailSMSTypeId  = p.EmailSMSTypeId };
                        spService.GetDataWithParameter(param, "UpdateSMSEmailAccess");
                    }
                }

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult EmaulSMSAccessSave(int InvestorId, List<int> AllTypeId)
        {
            var Result = "";
            try
            {
                spService.GetDataBySqlCommand("UPDATE EmailSMSInvestorAccess SET IsActive = 0 WHERE InvestorId= " + InvestorId + "");

                if (AllTypeId != null)
                {
                    foreach (var app in AllTypeId)
                    {
                        var accs = emailSMSInvestorAccessService.GetAll().Where(x => x.InvestorId == InvestorId && x.EmailSMSTypeId == app);

                        if (accs.Count() == 1)
                        {
                            accs.FirstOrDefault().IsActive = true;
                            emailSMSInvestorAccessService.Update(accs.FirstOrDefault());
                        }
                        else
                        {
                            var access = new EmailSMSInvestorAccess();

                            access.InvestorId = InvestorId;
                            access.EmailSMSTypeId = app;
                            access.CreateDate = DateTime.Now;
                            access.CreatedUserId = SessionHelper.LoggedInUserId;
                            access.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                            access.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                            access.IsActive = true;
                            emailSMSInvestorAccessService.Create(access);
                        }

                    }
                }
              
                return Json(Result,JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(Result = ex.InnerException.Message,JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetInvestorByCode(string InvestorCode)
        {
            try
            {
                var param = new { INVESTOR_CODE = InvestorCode };
                var InvestorList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE_For_EmailSMSAccess").Tables[0]
                .AsEnumerable()
                .Select(row => new 
                {
                    Id = row.Field<int>("Id"),
                    InvestorCode = row.Field<string>("InvestorCode"),//
                    InvestorName = row.Field<string>("InvestorName"),
                    EmailSMSTypeAccessId = row.Field<string>("EmailSMSTypeAccessId"),
                    EmailSMSTypeAccessId_Dp = row.Field<string>("EmailSMSTypeAccessId_Dp"),
                    EmailSMSType_NOT_AccessId_Dp = row.Field<string>("EmailSMSType_NOT_AccessId_Dp"),
                    InvestorBranchName = row.Field<string>("InvestorBranchName")
                }).ToList();
                return Json(InvestorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetEmailSMSType()
        {
            var Result = "";
            try
            {
                var emailsmsType = spService.GetDataBySqlCommand("SELECT E.Id,E.EmailSMSTypeName,E.EmailSMSTypeShortName FROM EmailSMSType AS E WHERE E.IsActive = 1").Tables[0].AsEnumerable().Select(row => new { 
                           Id = row.Field<int>("Id"),
                           EmailSMSTypeName = row.Field<string>("EmailSMSTypeName"),
                           EmailSMSTypeShortName = row.Field<string>("EmailSMSTypeShortName"),
                
                }).ToList();
                return Json(emailsmsType, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(Result = ex.Message,JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetTradeMessage(string trxDate, int type)
        {
            var date = ReportHelper.FormatDateToString(trxDate);
            var data = spService.GetDataWithParameter(new
            {
                TRANSACTION_DATE = date,
                IS_VALID = type
            }, "SP_GET_TRADE_CONFIRNATION_MESSAGE").Tables[0].AsEnumerable().Select(x => new { InvestorCode = x.Field<string>(0), InvestorName = x.Field<string>(1), MobileNo = x.Field<string>(2), Message = x.Field<string>(3), InvestorId = x.Field<int>(4) }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public void ShowSMSTradeReport(string trxDate, string type, int valid)
        {
            var date = ReportHelper.FormatDateToString(trxDate);
            var data = spService.GetDataWithParameter(new
            {
                TRANSACTION_DATE = date,
                IS_VALID = valid
            }, "SP_GET_TRADE_CONFIRNATION_MESSAGE").Tables[0];
            var param = new Dictionary<string, object> { { "isIPO", 0 } };
            ReportHelper.ShowReport(data, type, "rptSMSTradeConfirmation.rpt", "SMSTradeConfirmation_" + trxDate + (valid == 1 ? "_VALID" : "_INVALID"), param);
        }

        public ActionResult IPOResult()
        {
            ViewBag.BusinessDate = ReportHelper.FormatDate(SessionHelper.BusinessDate);
            ViewBag.IPO = iPODeclarationService.GetAll().ToList();
            return View();
        }

        public JsonResult GetIPOMessage(int ipo, int type)
        {
            var data = spService.GetDataWithParameter(new
            {
                IPO_DECLARATION_ID = ipo,
                IS_VALID = type
            }, "SP_GET_IPO_RESULT_MESSAGE").Tables[0].AsEnumerable().Select(x => new { InvestorCode = x.Field<string>(0), InvestorName = x.Field<string>(1), MobileNo = x.Field<string>(2), Message = x.Field<string>(3), InvestorId = x.Field<int>(4) }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public void ShowSMSIPOReport(int ipo, string type, int valid)
        {
            var ipoDec = iPODeclarationService.GetById(ipo);
            var data = spService.GetDataWithParameter(new
            {
                IPO_DECLARATION_ID = ipo,
                IS_VALID = valid
            }, "SP_GET_IPO_RESULT_MESSAGE").Tables[0];
            var param = new Dictionary<string, object> { { "isIPO", 1 } };
            ReportHelper.ShowReport(data, type, "rptSMSTradeConfirmation.rpt", "SMS_IPO_" + ipoDec.TradingCode + (valid == 1 ? "_VALID" : "_INVALID"), param);
        }


        public JsonResult SendSMS(List<SMS> messages, string type, int ipo = 0)
        {
            try
            {
                var userId = SessionHelper.LoggedInUserId;
                var shortName = SessionHelper.OrganizationShortName;
                foreach (var x in messages)
                {
                    var message = x.Message + "\n\nFrom " + shortName;
                    var sms = ReportHelper.SendSMS(x.MobileNo, message);
                    if (sms.ErrorCode == 0)
                    {
                        spService.ExecuteStoredProcedure(new
                        {
                            INVESTOR_ID = x.InvestorId,
                            TYPE = type,
                            USER_ID = userId,
                            MESSAGE = sms.Message,
                            MESSAGE_ID = sms.MessageId,
                            MESSAGE_COUNT = sms.SMSCount,
                            IPO_DECLARATION_ID = ipo
                        }, "SP_INSERT_SMS_EMAIL_CONFIRMATION");
                    }
                }
                return Json(new { Status = "SUCCESS", Message = "Sent SMS successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = "ERROR", Message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message) }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult General()
        {
            ViewBag.BusinessDate = ReportHelper.FormatDate(SessionHelper.BusinessDate);
            return View();
        }
        public JsonResult GetInvestorInfo(string code)
        {
            var data = spService.GetDataWithParameter(new { CODE = code }, "SP_GET_INVESTOR_INFO_FOR_GENERAL_SMS").Tables[0].AsEnumerable().Select(x => new { InvestorCode = x.Field<string>(0), InvestorName = x.Field<string>(1), MobileNo = x.Field<string>(2), InvestorId = x.Field<int>(3) }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SentReport()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("SMS", "SentReport");
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
            ViewBag.BusinessDate = ReportHelper.FormatDate(SessionHelper.BusinessDate);
            ViewBag.SMSType =
                spService.GetDataBySqlCommand(
                    "SELECT Id,EmailSMSTypeName FROM EmailSMSType WHERE IsEmail=0 AND IsActive=1").Tables[0];
            return View();
        }

        public void ShowSMSSentSummaryReport(string transactionDateFrom, string transactionDateTo, string type, int smsType)
        {
            var tradeDateFrom = ReportHelper.FormatDate(transactionDateFrom);
            var tradeDateTo = ReportHelper.FormatDate(transactionDateTo);
            var data =
            spService.GetDataWithParameter(
                new
                {
                    TRANSACTION_DATE_FROM = tradeDateFrom.ToString("dd MMM yyyy"),
                    TRANSACTION_DATE_TO = tradeDateTo.ToString("dd MMM yyyy"),
                    SMS_TYPE = smsType
                },
                "SP_RPT_SENT_SMS_SUMMARY").Tables[0];
            ReportHelper.ShowReport(data, type, "rptSentSMSSummary.rpt", "SentSMSSummary_" + tradeDateFrom.ToString("dd MMM yyyy") + "_" + tradeDateTo.ToString("dd MMM yyyy"));
        }

        public void ShowSMSSentDetailsReport(string transactionDateFrom, string transactionDateTo, string type, int smsType)
        {
            var tradeDateFrom = ReportHelper.FormatDate(transactionDateFrom);
            var tradeDateTo = ReportHelper.FormatDate(transactionDateTo);
            var data =
            spService.GetDataWithParameter(
                new
                {
                    TRANSACTION_DATE_FROM = tradeDateFrom.ToString("dd MMM yyyy"),
                    TRANSACTION_DATE_TO = tradeDateTo.ToString("dd MMM yyyy"),
                    SMS_TYPE = smsType
                },
                "SP_RPT_SENT_SMS_DETAILS").Tables[0];
            ReportHelper.ShowReport(data, type, "rptSentSMSDetails.rpt", "SentSMSDetails_" + tradeDateFrom.ToString("dd MMM yyyy") + "_" + tradeDateTo.ToString("dd MMM yyyy"));
        }

        public JsonResult GetPassword(string password)
        {
            return Json(Encription.GetDecryptedText(password), JsonRequestBehavior.AllowGet);
        }
    }
}