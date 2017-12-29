#region namespace

//using System.Transactions;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;
//using ERP.Web.Models;
//using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Hrms.Service;

//using HrmsWeb.App_Start;




#endregion



namespace Hrms.Controllers
{


    public class LeaveController : BaseController
    {

        #region Variable

        private readonly ISPService spService;
        private readonly IEmployeeProfileService employeeProfileService;
        private readonly IEmployeeDetailsService employeeDetailsService;
        private readonly ILeaveApplicationService leaveApplicationService;
        private readonly IBrokerDepartmentService brokerDepartmentService;
        public LeaveController(ISPService spService
            , IEmployeeProfileService employeeProfileService
            , IEmployeeDetailsService employeeDetailsService
            , ILeaveApplicationService leaveApplicationService
            , IBrokerDepartmentService brokerDepartmentService)
        {
            this.spService = spService;
            this.employeeProfileService = employeeProfileService;
            this.employeeDetailsService = employeeDetailsService;
            this.leaveApplicationService = leaveApplicationService;
            this.brokerDepartmentService = brokerDepartmentService;
        }

        #endregion


        #region Methods


        public ActionResult Leave_CancelApprover(int DeptId, string ApproverCode, int PermissionType)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE  EMP_LEAVE_APPROVAL_PERMISSION SET ENABLE_DISABLE_STATUS = 0 WHERE DEPT_ID = " + DeptId + " AND PERMISSION_TYPE = " + PermissionType + " AND EMP_ID = (SELECT TOP 1 ISNULL(E.emp_id,0)  FROM EMP_PROFILE E WHERE E.emp_office_code = '" + ApproverCode + "')");
                return Json(new { Result = "Success", Message = "Save successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Medical Document

        public ActionResult RetrievePrescription(int id)
        {
            byte[] cover = GetPrescription(id);
            if (cover != null)
            {
                MemoryStream ms = new MemoryStream(cover);
                return File(ms.ToArray(), "image/png");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/Images/BlankPage.png");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*");
            }
        }

        public byte[] GetPrescription(int Id)
        {
            var InvImg = leaveApplicationService.GetById(Id);
            if (InvImg != null)
            {
                var img = InvImg.medical_prescription;
                byte[] cover = img;
                return cover;
            }
            else
            {

                byte[] cover = null;
                return cover;
            }
        }


        public ActionResult RetrieveMedicalCertificate(int id)
        {
            byte[] cover = GetMedicalCertificate(id);
            if (cover != null)
            {
                MemoryStream ms = new MemoryStream(cover);
                return File(ms.ToArray(), "image/png");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/Images/BlankPage.png");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*");
            }
        }

        public byte[] GetMedicalCertificate(int Id)
        {
            var InvImg = leaveApplicationService.GetById(Id);
            if (InvImg != null)
            {
                var img = InvImg.medical_Certificate;
                byte[] cover = img;
                return cover;
            }
            else
            {

                byte[] cover = null;
                return cover;
            }
        }
        #endregion

        public JsonResult SaveLeavePermissionAuth(int DepartmentId, int RecommenderId, int ApproverId, int Permission_Id_rec, int Permission_Id_App)
        {
            try
            {
                var Message = "";
                if (DepartmentId != 0)
                {
                    if (RecommenderId == 0 && ApproverId == 0)
                    {
                        return Json(new { Result = "Error", Message = "Select recommender or approver or both" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var IfRecomExist = spService.GetDataBySqlCommand("SELECT * FROM EMP_LEAVE_APPROVAL_PERMISSION AS P WHERE P.DEPT_ID = " + DepartmentId + " AND P.EMP_ID = " + RecommenderId + " AND P.ENABLE_DISABLE_STATUS = 1 AND P.PERMISSION_TYPE = 1").Tables[0];
                        if (IfRecomExist.Rows.Count == 0)
                        {
                            if (Permission_Id_rec == 0) //Save Recommender
                            {
                                if (RecommenderId != 0)
                                {
                                    spService.GetDataBySqlCommand("INSERT INTO EMP_LEAVE_APPROVAL_PERMISSION(EMP_ID,DEPT_ID,PERMISSION_TYPE,ENABLE_DISABLE_STATUS)VALUES(" + RecommenderId + "," + DepartmentId + ",1,1)");
                                }
                            }
                            else  //Update Recommender
                            {
                                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPROVAL_PERMISSION SET EMP_ID = " + RecommenderId + " WHERE PERMISSION_id = " + Permission_Id_rec + "");
                            }

                        }
                        //else
                        //{
                        //    Message = "Recommender";
                        //}
                        var IfApproExist = spService.GetDataBySqlCommand("SELECT * FROM EMP_LEAVE_APPROVAL_PERMISSION AS P WHERE P.DEPT_ID = " + DepartmentId + " AND P.EMP_ID = " + ApproverId + " AND P.ENABLE_DISABLE_STATUS = 1 AND P.PERMISSION_TYPE = 2").Tables[0];
                        if (IfApproExist.Rows.Count == 0)
                        {
                            if (Permission_Id_App == 0)  // Save Approver
                            {
                                if (ApproverId != 0)
                                {
                                    spService.GetDataBySqlCommand("INSERT INTO EMP_LEAVE_APPROVAL_PERMISSION(EMP_ID,DEPT_ID,PERMISSION_TYPE,ENABLE_DISABLE_STATUS)VALUES(" + ApproverId + "," + DepartmentId + ",2,1)");
                                }
                            }
                            else   // Update Approver
                            {
                                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPROVAL_PERMISSION SET EMP_ID = " + ApproverId + " WHERE PERMISSION_id = " + Permission_Id_App + "");
                            }
                        }
                        //else
                        //{
                        //    Message = Message + ", Approver";
                        //}

                        // if (Message == "")
                        //{
                        return Json(new { Result = "Success", Message = "Save successfull." }, JsonRequestBehavior.AllowGet);
                        // }
                        //else
                        //  {
                        //      return Json(new { Result = "Success", Message = "This " + Message + " already exists" }, JsonRequestBehavior.AllowGet);
                        //  }



                    }
                }
                else
                {
                    return Json(new { Result = "Error", Message = "Select department" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetApproverList(int DeptId, int PermissionType)
        {
            try
            {
                var param = new { DeptId = DeptId, PermissionType = PermissionType };

                var EmpAccInfo = spService.GetDataWithParameter(param, "GetDepartmentWiseApprover").Tables[0];

                var EmpAccInfoList = EmpAccInfo.AsEnumerable()
                .Select(
                    x =>
                        new
                        {
                            EMP_ID = x.Field<int>("EMP_ID"),
                            emp_name = x.Field<string>("emp_name"),
                            dept_name = x.Field<string>("dept_name"),
                            desg_name = x.Field<string>("desg_name"),
                        }).ToList();

                return Json(EmpAccInfoList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LeaveMailSend(int EmployeeId, int LeaveId, string LvStatus)
        {

            try
            {
                if (EmployeeId == 0)
                {
                    var AppPanel = spService.GetDataBySqlCommand("SELECT ISNULL(reliever_emp_id,0) AS reliever_emp_id,ISNULL(recommend_emp_id,0) AS recommend_emp_id,ISNULL(hrd_apprved_emp_id,0) AS hrd_apprved_emp_id FROM EMP_LEAVE_APPLICATION WHERE emp_leave_application_id = " + LeaveId + "").Tables[0];

                    if (LvStatus == "Reliever")
                    {
                        EmployeeId = Convert.ToInt32(AppPanel.Rows[0][0]); //Recommender
                    }
                    else if (LvStatus == "Recommender")
                    {
                        EmployeeId = Convert.ToInt32(AppPanel.Rows[0][1]); //Recommender
                    }
                    else if (LvStatus == "Approver")
                    {
                        EmployeeId = Convert.ToInt32(AppPanel.Rows[0][2]); //Approver
                    }

                }

                var ReceiveEmp = employeeProfileService.GetById(EmployeeId);

                var emailAddress = ReceiveEmp.OfficeEmail;
                if (emailAddress != "" && emailAddress != null)
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("momin@ucasbd.com", "Ucas HRMS");
                    mail.To.Add(emailAddress);
                    mail.Subject = "Leave Permission";
                    mail.Body = "A leave application is waiting for your permission.";
                    // mail.IsBodyHtml = true; //*This line is must


                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; // the host name
                    smtp.Port = 587; //port number
                    smtp.EnableSsl = true; //whether your smtp server requires SSL
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("momin@ucasbd.com", "Mins@124");
                    smtp.Timeout = 20000;
                    smtp.Send(mail);
                }
                return Json(new { Result = "Success", Message = "Email send successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Leave_Final_Approve(int LeaveAppId, string LeaveStartDate, string LeaveEndDate, string Remarks)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET leave_status = 2,remarks = '" + Remarks + "',approved_date = GETDATE(),approved_start_date =  '" + ReportHelper.FormatDateToString(LeaveStartDate) + "',approved_end_date = '" + ReportHelper.FormatDateToString(LeaveEndDate) + "',approved_no_of_days =0 WHERE emp_leave_application_id =  " + LeaveAppId + "");

                return Json(new { Result = "Success", Message = "Leave approve successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Leave_Final_ApproveEx(List<int> LeaveAppId, List<string> LeaveStartDate, List<string> LeaveEndDate, List<string> Remarks)
        {
            try
            {
                if (LeaveAppId.Count > 0)
                {
                    var Index = 0;
                    foreach (var r in LeaveAppId)
                    {

                        spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET leave_status = 2,remarks = '" + Remarks[Index] + "',approved_date = GETDATE(),approved_start_date =  '" + ReportHelper.FormatDateToString(LeaveStartDate[Index]) + "',approved_end_date = '" + ReportHelper.FormatDateToString(LeaveEndDate[Index]) + "',hrd_apprved_emp_id = " + SessionHelper.LoggedInUserId_Hrm + ",recommend_emp_id = " + SessionHelper.LoggedInUserId_Hrm + " WHERE emp_leave_application_id =  " + r + "");
                        Index = Index + 1;
                    }

                    return Json(new { Result = "Success", Message = "Leave approve successfull" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "Error", Message = "Select leave" }, JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Leave_Cancel_By_Approver(List<int> LeaveAppId)
        {
            try
            {
                if (LeaveAppId.Count > 0)
                {
                    var Index = 0;
                    foreach (var r in LeaveAppId)
                    {

                        spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET leave_status = 8, leave_cancel_date = '" + DateTime.Now + "',leave_cancel_emp_id =" + SessionHelper.LoggedInUserId + ",edit_date = '" + DateTime.Now + "',updated_by = " + SessionHelper.LoggedInUserId + " WHERE emp_leave_application_id =  " + r + "");
                        Index = Index + 1;
                    }

                    return Json(new { Result = "Success", Message = "Leave cancel successfull" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "Error", Message = "Select leave" }, JsonRequestBehavior.AllowGet);
                }



            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Leave_Recommendation_Approve(int LeaveAppId, string LeaveStartDate, string LeaveEndDate, string Reconmmad)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET leave_status = 5,recommend_emp_id=" + SessionHelper.LoggedInUserId_Hrm + ",recommend_date = GETDATE(),recommend_start_date = '" + ReportHelper.FormatDateToString(LeaveStartDate) + "',recommend_end_date = '" + ReportHelper.FormatDateToString(LeaveEndDate) + "',approved_start_date = '" + ReportHelper.FormatDateToString(LeaveStartDate) + "',approved_end_date = '" + ReportHelper.FormatDateToString(LeaveEndDate) + "',recommend_no_of_days =0,recommend_Remarks = '" + Reconmmad + "' WHERE emp_leave_application_id = " + LeaveAppId + "");

                //Email Send

                //var RecoEmpId = spService.GetDataBySqlCommand("SELECT ISNULL(E.reliever_emp_id,0) AS reliever_emp_id,ISNULL(E.recommend_emp_id,0) AS recommend_emp_id,ISNULL(E.hrd_apprved_emp_id,0) AS hrd_apprved_emp_id FROM EMP_LEAVE_APPLICATION AS E WHERE  E.emp_leave_application_id = " + LeaveAppId + "").Tables[0].Rows[0][2];
                //if (RecoEmpId != null) // Email send to approver
                //{
                //    //LeaveMailSend(Convert.ToInt32(RecoEmpId)); 

                //    var ReceiveEmp = employeeDetailsService.GetByEmpId(Convert.ToInt32(RecoEmpId));

                //    var emailAddress = ReceiveEmp.emp_office_mail_address;
                //    if (emailAddress != "" && emailAddress != null)
                //    {
                //        MailMessage mail = new MailMessage();
                //        mail.From = new MailAddress("momin@ucasbd.com", "Ucas HRMS");
                //        mail.To.Add(emailAddress);
                //        mail.Subject = "Leave Permission";
                //        mail.Body = "A leave application is waiting for your permission.";
                //        // mail.IsBodyHtml = true; //*This line is must


                //        SmtpClient smtp = new SmtpClient();
                //        smtp.Host = "smtp.gmail.com"; // the host name
                //        smtp.Port = 587; //port number
                //        smtp.EnableSsl = true; //whether your smtp server requires SSL
                //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //        smtp.Credentials = new System.Net.NetworkCredential("momin@ucasbd.com", "Mins@124");
                //        smtp.Timeout = 20000;
                //        smtp.Send(mail);
                //    }
                // }
                return Json(new { Result = "Success", Message = "Leave approve successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Leave_Cancel(int LeaveAppId, int LeaveStatus, string Remarks)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET leave_status = " + LeaveStatus + ",remarks = '" + Remarks + "',leave_cancel_emp_id = " + SessionHelper.LoggedInUserId + ",leave_cancel_date = GETDATE() WHERE emp_leave_application_id = " + LeaveAppId + "");

                return Json(new { Result = "Success", Message = "Leave cancel successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LeaveDelete(int LeaveAppId)
        {
            try
            {
                spService.GetDataBySqlCommand("DELETE EMP_LEAVE_APPLICATION WHERE emp_leave_application_id = " + LeaveAppId + "");

                return Json(new { Result = "Success", Message = "Leave delete successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Leave_Cancel_Reliever(int LeaveAppId, int LeaveStatus)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET reliever_emp_id =  NULL WHERE emp_leave_application_id = " + LeaveAppId + " ");

                return Json(new { Result = "Success", Message = "Leave cancel successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Leave_Reliever_Approve(int LeaveAppId)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION  SET leave_status = 4,reliever_emp_id = " + SessionHelper.LoggedInUserId_Hrm + " WHERE emp_leave_application_id = " + LeaveAppId + "");

                //Email Send

                // var RecoEmpId = spService.GetDataBySqlCommand("SELECT ISNULL(E.reliever_emp_id,0) AS reliever_emp_id,ISNULL(E.recommend_emp_id,0) AS recommend_emp_id,ISNULL(E.hrd_apprved_emp_id,0) AS hrd_apprved_emp_id FROM EMP_LEAVE_APPLICATION AS E WHERE  E.emp_leave_application_id = " + LeaveAppId + "").Tables[0].Rows[0][1];

                // if (RecoEmpId != null) // Email send to commender
                //{
                //    //LeaveMailSend(Convert.ToInt32(RecoEmpId)); 


                //    var ReceiveEmp = employeeDetailsService.GetByEmpId(Convert.ToInt32(RecoEmpId));

                //    var emailAddress = ReceiveEmp.emp_office_mail_address;
                //    if (emailAddress != "" && emailAddress != null)
                //    {
                //        MailMessage mail = new MailMessage();
                //        mail.From = new MailAddress("momin@ucasbd.com", "Ucas HRMS");
                //        mail.To.Add(emailAddress);
                //        mail.Subject = "Leave Permission";
                //        mail.Body = "A leave application is waiting for your permission.";
                //        // mail.IsBodyHtml = true; //*This line is must


                //        SmtpClient smtp = new SmtpClient();
                //        smtp.Host = "smtp.gmail.com"; // the host name
                //        smtp.Port = 587; //port number
                //        smtp.EnableSsl = true; //whether your smtp server requires SSL
                //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //        smtp.Credentials = new System.Net.NetworkCredential("momin@ucasbd.com", "Mins@124");
                //        smtp.Timeout = 20000;
                //        smtp.Send(mail);
                //    }
                //}

                return Json(new { Result = "Success", Message = "Save successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GET_EMP_LEAVE_APPLY_Info()
        {
            try
            {
                var param = new { PEMP_ID = SessionHelper.LoggedInUserId_Hrm, PEMP_OFFICE_CODE = SessionHelper.EmployeeCode, PTRANS_date = DateTime.Now, PEMP_LEAVE_APPLICATION_ID = -1 };
                var empList = spService.GetDataWithParameter(param, "FSP_GET_EMP_LEAVE_APPLY");

                var List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    EMP_ID = row.Field<int>("EMP_ID"),
                    EMP_NAME = row.Field<string>("EMP_NAME"),
                    EMP_OFFICE_CODE = row.Field<string>("EMP_OFFICE_CODE"),
                    EMP_DESG_NAME = row.Field<string>("EMP_DESG_NAME"),
                    EMP_BRANCH_NAME = row.Field<string>("EMP_BRANCH_NAME"),
                    EMP_DEPT_NAME = row.Field<string>("EMP_DEPT_NAME"),
                    EMP_DEPT_ID = row.Field<int?>("EMP_DEPT_ID"),

                    CL_BALANCE = row.Field<decimal?>("CL_BALANCE"),
                    PL_BALANCE = row.Field<decimal?>("PL_BALANCE"),
                    SL_BALANCE = row.Field<decimal?>("SL_BALANCE"),
                    ML_BALANCE = row.Field<decimal?>("ML_BALANCE"),
                    LWP_BALANCE = row.Field<decimal?>("LWP_BALANCE"),
                    LWP_CTS_BALANCE = row.Field<decimal?>("LWP_CTS_BALANCE"),

                    CL_AVAILED = row.Field<decimal?>("CL_AVAILED"),
                    PL_AVAILED = row.Field<decimal?>("PL_AVAILED"),
                    SL_AVAILED = row.Field<decimal?>("SL_AVAILED"),
                    ML_AVAILED = row.Field<decimal?>("ML_AVAILED"),
                    LWP_AVAILED = row.Field<decimal?>("LWP_AVAILED"),
                    LWP_CTS_AVAILED = row.Field<decimal?>("LWP_CTS_AVAILED"),


                    EMP_LEAVE_APPLICATION_ID = row.Field<int?>("EMP_LEAVE_APPLICATION_ID"),
                    LEAVE_TYPE_ID = row.Field<int?>("LEAVE_TYPE_ID"),
                    LEAVE_TYPE_NAME = row.Field<string>("LEAVE_TYPE_NAME"),
                    LEAVE_TYPE_SHORTNAME = row.Field<string>("LEAVE_TYPE_SHORTNAME"),
                    APPLIED_START_date = row.Field<string>("APPLIED_START_date"), //APPLIED_START_date APPLIED_END_date
                    APPLIED_END_date = row.Field<string>("APPLIED_END_date"),

                    APPLIED_NO_OF_DAYS = row.Field<int?>("APPLIED_NO_OF_DAYS"),
                    APPLIED_date = row.Field<string>("APPLIED_date"),

                    APPROVED_START_date = row.Field<string>("APPROVED_START_date"),
                    APPROVED_END_date = row.Field<string>("APPROVED_END_date"),
                    APPROVED_date = row.Field<string>("APPROVED_date"),
                    APPROVED_NO_OF_DAYS = row.Field<int?>("APPROVED_NO_OF_DAYS"),

                    recommend_date = row.Field<string>("recommend_date"),
                    recommend_start_date = row.Field<string>("recommend_start_date"),
                    recommend_end_date = row.Field<string>("recommend_end_date"),
                    recommend_no_of_days = row.Field<int?>("recommend_no_of_days"),



                    PURPOSE_OF_LEAVE = row.Field<string>("PURPOSE_OF_LEAVE"),
                    ADDRESS_DURING_LEAVE = row.Field<string>("ADDRESS_DURING_LEAVE"),
                    CONTACT_NO = row.Field<string>("CONTACT_NO"),
                    REMARKS = row.Field<string>("REMARKS"),
                    RELIEVER_EMP_ID = row.Field<int?>("RELIEVER_EMP_ID"),
                    recommend_emp_id = row.Field<int?>("recommend_emp_id"),
                    HRD_APPRVED_EMP_ID = row.Field<int?>("HRD_APPRVED_EMP_ID"),
                    LEAVE_STATUS = row.Field<int?>("LEAVE_STATUS"),
                    LEAVE_STATUSMsg = row.Field<string>("LEAVE_STATUSMsg"),
                    LeaveCancelEmpName = row.Field<string>("LeaveCancelEmpName")

                }).ToList();

                // return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet); 
                return Json(new { Result = "Success", List_EmployeeInfo = List_EmployeeInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Get_Employee_leave_Typewise(string LeaveShortName)
        {
            try
            {
                var param = new { EmployeeId = SessionHelper.LoggedInUserId_Hrm, LeaveShortName = LeaveShortName };
                var empList = spService.GetDataWithParameter(param, "Get_Employee_leave_Typewise");

                var List_LeaveInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    emp_leave_application_id = row.Field<int>("emp_leave_application_id"),
                    employee_id = row.Field<int>("employee_id"),
                    leave_type_id = row.Field<int>("leave_type_id"),
                    leave_type_name = row.Field<string>("leave_type_name"),
                    leave_type_shortname = row.Field<string>("leave_type_shortname"),
                    applied_date = row.Field<string>("applied_date"),
                    applied_start_date = row.Field<string>("applied_start_date"),
                    ASapplied_end_date = row.Field<string>("ASapplied_end_date"),
                    NoOfDays = row.Field<int>("NoOfDays"),

                }).ToList();
                return Json(new { Result = "Success", List_LeaveInfo = List_LeaveInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        public JsonResult EmployeeLeaveBalance(int EmployeeId, string EmployeeCode)
        {
            try
            {
                var param = new { V_EMP_ID = EmployeeId, PEMP_OFFICE_CODE = EmployeeCode };
                var empList = spService.GetDataWithParameter(param, "EmployeeLeaveBalance");

                var List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    emp_id = row.Field<int>("emp_id"),
                    CL_BALANCE = row.Field<decimal?>("CL_BALANCE"),
                    PL_BALANCE = row.Field<decimal?>("PL_BALANCE"),
                    SL_BALANCE = row.Field<decimal?>("SL_BALANCE"),
                    ML_BALANCE = row.Field<decimal?>("ML_BALANCE"),
                    LWP_BALANCE = row.Field<decimal?>("LWP_BALANCE"),
                    LWP_CTS_BALANCE = row.Field<decimal?>("LWP_CTS_BALANCE"),

                    CL_AVAILED = row.Field<decimal?>("CL_AVAILED"),
                    PL_AVAILED = row.Field<decimal?>("PL_AVAILED"),
                    SL_AVAILED = row.Field<decimal?>("SL_AVAILED"),
                    ML_AVAILED = row.Field<decimal?>("ML_AVAILED"),
                    LWP_AVAILED = row.Field<decimal?>("LWP_AVAILED"),
                    LWP_CTS_AVAILED = row.Field<decimal?>("LWP_CTS_AVAILED"),

                }).ToList();
                return Json(new { Result = "Success", List_EmployeeInfo = List_EmployeeInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public class EmployeeModelMedical
        {
            public int EmployeeId { get; set; }
            public HttpPostedFileBase medical_Certificate { get; set; }
            public HttpPostedFileBase medical_prescription { get; set; }
        }

        [HttpPost]
        public ActionResult SaveLeaveApplication(int LeaveApplicationId, int LeaveTypeId, string AppliedDate, string LeaveFromDate, string LeaveToDate, string PurposeOfleave, string LeaveAddress, string Telephone, string Remarks, string RelieverEmployeeId, int approved_emp_id, string recommend_emp_id, EmployeeModelMedical model)
        {
            try
            {

                var LEAVE_STATUS = 1;

                var EmpId = "0";

                if (RelieverEmployeeId == "0")
                {

                    LEAVE_STATUS = 4;
                    EmpId = recommend_emp_id;

                }
                else
                {
                    EmpId = RelieverEmployeeId;
                }

                var paramValid = new { EMPLOYEE_ID = SessionHelper.LoggedInUserId_Hrm, @LEAVE_TYPE_ID = LeaveTypeId, START_DATE = ReportHelper.FormatDateToString(LeaveFromDate), END_DATE = ReportHelper.FormatDateToString(LeaveToDate), LeaveAppId = LeaveApplicationId };

                var leaveVali = spService.GetDataWithParameter(paramValid, "FSP_LEAVE_VALIDATION").Tables[0];
                if (leaveVali.Rows[0][0].ToString() == "0")
                {
                    if (RelieverEmployeeId == "0" || RelieverEmployeeId == "") { RelieverEmployeeId = null; };
                    var param = new { PEMP_LEAVE_APPLICATION_ID = LeaveApplicationId, PEMPLOYEE_ID = SessionHelper.LoggedInUserId_Hrm, PLEAVE_TYPE_ID = LeaveTypeId, PAPPLIED_START_date = ReportHelper.FormatDateToString(LeaveFromDate), PAPPLIED_END_date = ReportHelper.FormatDateToString(LeaveToDate), PAPPLIED_date = ReportHelper.FormatDateToString(AppliedDate), PPURPOSE_OF_LEAVE = PurposeOfleave, PADDRESS_DURING_LEAVE = LeaveAddress, PCONTACT_NO = Telephone, approved_emp_id = approved_emp_id, recommend_emp_id = recommend_emp_id, PRELIEVER_EMP_ID = RelieverEmployeeId, PREMARKS = Remarks, PLEAVE_STATUS = LEAVE_STATUS, PUSER_ID = SessionHelper.LoggedInUserId_Hrm };//medical_Certificate  , medical_Certificate = model.medical_Certificate.InputStream, medical_prescription = model.medical_prescription.InputStream
                    var ApplicationId = spService.GetDataWithParameter(param, "FSP_ADD_UP_EMP_LEAVE_APPLY_EX").Tables[0].Rows[0][0];

                    if (LeaveTypeId == 3)
                    {
                        if (ApplicationId != null && ApplicationId != "")
                        {
                            var LeaveAtt = leaveApplicationService.GetById(Convert.ToInt32(ApplicationId));
                            if (model.medical_Certificate != null)
                            {

                                byte[] data = new byte[model.medical_Certificate.ContentLength];
                                model.medical_Certificate.InputStream.Read(data, 0, model.medical_Certificate.ContentLength);
                                LeaveAtt.medical_Certificate = data;

                            }

                            if (model.medical_prescription != null)
                            {
                                byte[] data = new byte[model.medical_prescription.ContentLength];
                                model.medical_prescription.InputStream.Read(data, 0, model.medical_prescription.ContentLength);
                                LeaveAtt.medical_prescription = data;
                            }
                            leaveApplicationService.Update(LeaveAtt);
                        }
                    }


                    //spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION SET medical_Certificate = '" + model.medical_Certificate + "',medical_prescription = '" + model.medical_prescription + "' WHERE emp_leave_application_id = " + ApplicationId + "");

                    return Json(new { Result = "Success", Message = "Save successfull. ", EmployeeId = EmpId, LeaveAppId = ApplicationId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "Error", Message = leaveVali.Rows[0][0].ToString(), RelieverEmployeeId = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public ActionResult SaveLeaveApplication(int LeaveApplicationId, int LeaveTypeId, string AppliedDate, string LeaveFromDate, string LeaveToDate, string PurposeOfleave, string LeaveAddress, string Telephone, string Remarks, string RelieverEmployeeId, int approved_emp_id, string recommend_emp_id, EmployeeModelMedical model)
        //{
        //    try
        //    {
        //        byte[] bytes;
        //        //using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //        //{
        //        //    bytes = br.ReadBytes(postedFile.ContentLength);
        //        //}
        //        //spService.GetDataBySqlCommand("UPDATE EMP_LEAVE_APPLICATION SET medical_Certificate = '" + bytes + "' WHERE emp_leave_application_id = 207");

        //        var paramValid = new { EMPLOYEE_ID = SessionHelper.LoggedInUserId, @LEAVE_TYPE_ID = LeaveTypeId, START_DATE = ReportHelper.FormatDateToString(LeaveFromDate), END_DATE = ReportHelper.FormatDateToString(LeaveToDate)};

        //        var leaveVali = spService.GetDataWithParameter(paramValid, "FSP_LEAVE_VALIDATION").Tables[0];
        //        if (leaveVali.Rows[0][0].ToString() == "0")
        //        {
        //            var param = new { PEMP_LEAVE_APPLICATION_ID = LeaveApplicationId, PEMPLOYEE_ID = SessionHelper.LoggedInUserId, PLEAVE_TYPE_ID = LeaveTypeId, PAPPLIED_START_date = ReportHelper.FormatDateToString(LeaveFromDate), PAPPLIED_END_date = ReportHelper.FormatDateToString(LeaveToDate), PAPPLIED_date = ReportHelper.FormatDateToString(AppliedDate), PPURPOSE_OF_LEAVE = PurposeOfleave, PADDRESS_DURING_LEAVE = LeaveAddress, PCONTACT_NO = Telephone, approved_emp_id = approved_emp_id,recommend_emp_id = recommend_emp_id, PRELIEVER_EMP_ID = RelieverEmployeeId, PREMARKS = Remarks, PLEAVE_STATUS = 1, PUSER_ID = SessionHelper.LoggedInUserId };
        //            spService.GetDataWithParameter(param, "FSP_ADD_UP_EMP_LEAVE_APPLY_EX");
        //           // LeaveMailSend(Convert.ToInt32(RelieverEmployeeId));
        //            //@approved_emp_id				INT,
        //            //@recommend_emp_id	

        //            //if (LeaveApplicationId == 0)
        //            //{
        //            //    //Email Send 
        //            var EmpId = "0";
        //            if (LeaveTypeId != 3)
        //            {
        //                EmpId = RelieverEmployeeId;

        //            }
        //            else
        //            {
        //                EmpId = recommend_emp_id;
        //            }
        //            //    var ReceiveEmp = employeeDetailsService.GetByEmpId(Convert.ToInt32(EmpId));

        //            //    var emailAddress = ReceiveEmp.emp_office_mail_address;
        //            //    if (emailAddress != "" && emailAddress != null)
        //            //    {
        //            //        MailMessage mail = new MailMessage();
        //            //        mail.From = new MailAddress("momin@ucasbd.com", "Ucas HRMS");
        //            //        mail.To.Add(emailAddress);
        //            //        mail.Subject = "Leave Permission";
        //            //        mail.Body = "A leave application is waiting for your permission.";
        //            //        // mail.IsBodyHtml = true; //*This line is must


        //            //        SmtpClient smtp = new SmtpClient();
        //            //        smtp.Host = "smtp.gmail.com"; // the host name
        //            //        smtp.Port = 587; //port number
        //            //        smtp.EnableSsl = true; //whether your smtp server requires SSL
        //            //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //            //        smtp.Credentials = new System.Net.NetworkCredential("momin@ucasbd.com", "Mins@124");
        //            //        smtp.Timeout = 20000;
        //            //        smtp.Send(mail);
        //            //    }
        //            //}

        //            return Json(new { Result = "Success", Message = "Save successfull. ", EmployeeId = EmpId }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new { Result = "Error", Message = leaveVali.Rows[0][0].ToString(), RelieverEmployeeId = 0 }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(new { Result = "Error",Message = ex.Message},JsonRequestBehavior.AllowGet);
        //    }
        //}


        public JsonResult GetDepartmentwiseLeaveRecommenderAndApprover()
        {
            try
            {
                var empList = spService.GetDataWithoutParameter("Get_Departmentwise_Leave_Recommender_And_Approver");

                var List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    RowSl = row.Field<string>("RowSl"),
                    dept_id = row.Field<int>("dept_id"),
                    dept_name = row.Field<string>("dept_name"),
                    dept_short_name = row.Field<string>("dept_short_name"),
                    Recommender = row.Field<string>("Recommender"),
                    Approver = row.Field<string>("Approver")

                }).ToList();

                return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //emp_leave_application_id,employee_id,emp_office_code,emp_name, emp_joining_datetime,department_id,dept_name,designation_id,desg_name,leave_type_id,leave_type_name,branch_id,branch_name		,applied_start_date,applied_end_date,applied_date,applied_no_of_days,recommend_start_date, recommend_end_date,recommend_no_of_days, recommend_date,purpose_of_leave,address_during_leave,contact_no,remarks,reliever_branch_id,reliever_designation_id

        public JsonResult Get_Employee_Leave_Info_For_Process(string Releiver, int LeaveStatusId)
        {
            try
            {
                var param = new { Qtype = Releiver, Relie_Recom_Approval_Id = SessionHelper.LoggedInUserId_Hrm, LeaveStatus = LeaveStatusId };

                var EmpAccInfo = spService.GetDataWithParameter(param, "Get_Employee_Leave_Info_For_Process").Tables[0];
                if (EmpAccInfo.Rows.Count == 0)
                {
                    return Json(new { Result = "NotFound", Message = "No Data Found", EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var EmpAccInfoList = EmpAccInfo.AsEnumerable()
                    .Select(
                        x =>
                            new  //contact_no purpose_of_leave emp_leave_application_id emp_office_code emp_name leave_type_name applied_start_date applied_end_date
                            {
                                RowSl = x.Field<string>("RowSl"),
                                emp_leave_application_id = x.Field<int>("emp_leave_application_id"),
                                employee_id = x.Field<int>("employee_id"),
                                emp_office_code = x.Field<string>("emp_office_code"),
                                emp_name = x.Field<string>("emp_name"),
                                emp_joining_datetime = x.Field<string>("emp_joining_datetime"),
                                dept_name = x.Field<string>("dept_name"),
                                desg_name = x.Field<string>("desg_name"),
                                leave_type_name = x.Field<string>("leave_type_name"),
                                branch_name = x.Field<string>("branch_name"),
                                applied_start_date = x.Field<string>("applied_start_date"),
                                applied_end_date = x.Field<string>("applied_end_date"),
                                applied_date = x.Field<string>("applied_date"),
                                applied_no_of_days = x.Field<decimal?>("applied_no_of_days"),
                                RelieverName = x.Field<string>("RelieverName"),
                                RecommandName = x.Field<string>("RecommandName"),
                                recommend_start_date = x.Field<string>("recommend_start_date"),//RelieverName RecommandName
                                recommend_end_date = x.Field<string>("recommend_end_date"),
                                recommend_no_of_days = x.Field<decimal?>("recommend_no_of_days"),
                                recommend_date = x.Field<string>("recommend_date"),
                                purpose_of_leave = x.Field<string>("purpose_of_leave"),
                                address_during_leave = x.Field<string>("address_during_leave"),
                                contact_no = x.Field<string>("contact_no"),
                                remarks = x.Field<string>("remarks"),
                            }).ToList();

                    return Json(new { Result = "Success", Message = "Success", EmpAccInfoList = EmpAccInfoList }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Get_Employee_Leave_Info_For_Approve()
        {
            try
            {
                var param = new { Approval_Id = SessionHelper.LoggedInUserId_Hrm };

                var EmpAccInfo = spService.GetDataWithParameter(param, "Get_Employee_Leave_Info_For_Approve").Tables[0];
                var EmpAccInfoList = EmpAccInfo.AsEnumerable()
                .Select(
                    x =>
                        new  //contact_no purpose_of_leave emp_leave_application_id emp_office_code emp_name leave_type_name applied_start_date applied_end_date
                        {
                            RowSl = x.Field<string>("RowSl"),
                            emp_leave_application_id = x.Field<int>("emp_leave_application_id"),
                            employee_id = x.Field<int>("employee_id"),
                            emp_office_code = x.Field<string>("emp_office_code"),
                            emp_name = x.Field<string>("emp_name"),
                            emp_joining_datetime = x.Field<string>("emp_joining_datetime"),
                            dept_name = x.Field<string>("dept_name"),
                            desg_name = x.Field<string>("desg_name"),
                            leave_type_name = x.Field<string>("leave_type_name"),
                            branch_name = x.Field<string>("branch_name"),
                            applied_start_date = x.Field<string>("applied_start_date"),
                            applied_end_date = x.Field<string>("applied_end_date"),
                            applied_date = x.Field<string>("applied_date"),
                            applied_no_of_days = x.Field<int?>("applied_no_of_days"),
                            RelieverName = x.Field<string>("RelieverName"),
                            RecommandName = x.Field<string>("RecommandName"),
                            recommend_start_date = x.Field<string>("recommend_start_date"),//RelieverName RecommandName
                            recommend_end_date = x.Field<string>("recommend_end_date"),
                            recommend_no_of_days = x.Field<int?>("recommend_no_of_days"),
                            recommend_date = x.Field<string>("recommend_date"),
                            purpose_of_leave = x.Field<string>("purpose_of_leave"),
                            address_during_leave = x.Field<string>("address_during_leave"),
                            contact_no = x.Field<string>("contact_no"),
                            remarks = x.Field<string>("remarks"),
                        }).ToList();

                return Json(new { Result = "Success", Message = "Success", EmpAccInfoList = EmpAccInfoList }, JsonRequestBehavior.AllowGet);
                //return Json( EmpAccInfoList , JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Get_Employee_Leave_Info_By_LeaveId(int LeaveAppId)
        {
            try
            {
                var param = new { LeaveAppId = LeaveAppId };

                var EmpAccInfo = spService.GetDataWithParameter(param, "Get_Employee_Leave_Info_By_LeaveId").Tables[0];
                var EmpAccInfoList = EmpAccInfo.AsEnumerable()
                .Select(
                    x =>
                        new
                        {
                            RowSl = x.Field<string>("RowSl"),
                            emp_leave_application_id = x.Field<int>("emp_leave_application_id"),
                            employee_id = x.Field<int>("employee_id"),
                            emp_office_code = x.Field<string>("emp_office_code"),
                            emp_name = x.Field<string>("emp_name"),
                            emp_joining_datetime = x.Field<string>("emp_joining_datetime"),
                            dept_name = x.Field<string>("dept_name"),
                            desg_name = x.Field<string>("desg_name"),
                            leave_type_name = x.Field<string>("leave_type_name"),
                            branch_name = x.Field<string>("branch_name"),
                            applied_start_date = x.Field<string>("applied_start_date"),
                            applied_end_date = x.Field<string>("applied_end_date"),
                            applied_date = x.Field<string>("applied_date"),
                            applied_no_of_days = x.Field<int?>("applied_no_of_days"),
                            RelieverName = x.Field<string>("RelieverName"),
                            RecommandName = x.Field<string>("RecommandName"),
                            recommend_start_date = x.Field<string>("recommend_start_date"),
                            recommend_end_date = x.Field<string>("recommend_end_date"),
                            recommend_no_of_days = x.Field<int?>("recommend_no_of_days"),
                            recommend_date = x.Field<string>("recommend_date"),
                            purpose_of_leave = x.Field<string>("purpose_of_leave"),
                            address_during_leave = x.Field<string>("address_during_leave"),
                            contact_no = x.Field<string>("contact_no"),
                            remarks = x.Field<string>("remarks"),
                        }).ToList();

                return Json(new { Result = "Success", Message = "Success", EmpAccInfoList = EmpAccInfoList }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetLeaveInfoForLeaveCancel(string EmployeeCode, string StartDate, string EndDate)
        {
            try
            {
                var param = new { EmployeeCode = EmployeeCode, StartDate = ReportHelper.FormatDateToString(StartDate), EndDate = ReportHelper.FormatDateToString(EndDate) };

                var EmpAccInfo = spService.GetDataWithParameter(param, "GetLeaveInfoForLeaveCancel").Tables[0];
                if (EmpAccInfo.Rows.Count == 0)
                {
                    return Json(new { Result = "NotFound", Message = "No Data Found", EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var EmpAccInfoList = EmpAccInfo.AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                emp_leave_application_id = x.Field<int>("emp_leave_application_id"),
                                employee_id = x.Field<int>("employee_id"),
                                emp_office_code = x.Field<string>("emp_office_code"),
                                emp_name = x.Field<string>("emp_name"),
                                emp_joining_datetime = x.Field<string>("emp_joining_datetime"),
                                dept_name = x.Field<string>("dept_name"),
                                desg_name = x.Field<string>("desg_name"),
                                leave_type_name = x.Field<string>("leave_type_name"),
                                branch_name = x.Field<string>("branch_name"),
                                applied_start_date = x.Field<string>("applied_start_date"),
                                applied_end_date = x.Field<string>("applied_end_date"),
                                applied_date = x.Field<string>("applied_date"),
                                applied_no_of_days = x.Field<int?>("applied_no_of_days"),
                                purpose_of_leave = x.Field<string>("purpose_of_leave"),
                                address_during_leave = x.Field<string>("address_during_leave"),
                                contact_no = x.Field<string>("contact_no"),
                                remarks = x.Field<string>("remarks"),
                                LeaveStatusMsg = x.Field<string>("LeaveStatusMsg")
                            }).ToList();

                    return Json(new { Result = "Success", Message = "Success", EmpAccInfoList = EmpAccInfoList }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetEmployeeSalaryAccountInfo()
        {
            try
            {
                string EmployeeCode = employeeProfileService.GetById(SessionHelper.LoggedInUserId_Hrm).emp_office_code;
                var param = new { EmployeeCode = EmployeeCode };

                var EmpAccInfo = spService.GetDataWithParameter(param, "GetEmployee_Salary_AccountInfo").Tables[0];
                if (EmpAccInfo.Rows.Count == 0)
                {
                    return Json(new { Result = "NotFound", Message = "Employee not found", EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var EmpAccInfoList = EmpAccInfo.AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                emp_id = x.Field<int>("emp_id"),
                                empcode = x.Field<string>("emp_office_code"),
                                empname = x.Field<string>("emp_name"),
                                DateOfBirth = x.Field<string>("DateOfBirth"),
                                JoiningDate = x.Field<string>("JoiningDate"),
                                ConfirmationDate = x.Field<string>("ConfirmationDate"),
                                desgname = x.Field<string>("desg_name"),
                                branchname = x.Field<string>("branch_name"),
                                deptname = x.Field<string>("dept_name"),
                                bank_id = x.Field<int?>("emp_salary_acc_bank_id"),
                                emp_salary_acc_branch_id = x.Field<int?>("emp_salary_acc_branch_id"),
                                emp_salary_acc_no = x.Field<string>("emp_salary_acc_no"),
                                issalarystop = x.Field<decimal>("is_salary_stop"),
                            }).ToList();

                    return Json(new { Result = "Success", Message = "Success", EmpAccInfoList = EmpAccInfoList }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message, EmpAccInfoList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Actions

        public ActionResult IndexTest()
        {
            ViewBag.LeaveTypeList = spService.GetDataWithParameter(new { Employee_Id = SessionHelper.LoggedInUserId_Hrm }, "FSP_GET_HR_LEAVE_TYPE").Tables[0].AsEnumerable().Select(row => new { LEAVE_TYPE_ID = row.Field<int>("LEAVE_TYPE_ID"), LEAVE_TYPE_NAME = row.Field<string>("LEAVE_TYPE_NAME") }).ToList();
            ViewBag.RELIEVERList = spService.GetDataWithParameter(new { PEMPLOYEE_ID = SessionHelper.LoggedInUserId_Hrm }, "FSP_GET_LEAVE_RELIEVER_INFO_Ex").Tables[0].AsEnumerable().Select(row => new { Reliever_EMP_ID = row.Field<int>("EMP_ID"), EMP_NAME = row.Field<string>("EMP_NAME") }).ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Approverlist"] = items;
            ViewData["Recommenderlist"] = items;

            return View();
        }

        public ActionResult leaveRecoApp()
        {
            ViewBag.DepartmentList = brokerDepartmentService.GetAll().Where(x => x.IsActive == true).ToList(); //spService.GetDataWithParameter(new { PDEPT_ID = -1 }, "FSP_GET_HR_DEPT").Tables[0].AsEnumerable().Select(row => new { DEPT_ID = row.Field<int>("DEPT_ID"), DEPT_NAME = row.Field<string>("DEPT_NAME") }).ToList();

            ViewBag.EmployeeList = spService.GetDataBySqlCommand("SELECT E.emp_id,E.emp_office_code +' - '+E.emp_name AS EmployeeName  FROM EMP_PROFILE AS E WHERE E.emp_status_id = 1").Tables[0].AsEnumerable().Select(row => new { emp_id = row.Field<int>("emp_id"), EmployeeName = row.Field<string>("EmployeeName") }).ToList();
            //
            return View();
        }

        public ActionResult frmReliever()
        {
            return View();
        }



        public ActionResult Index()
        {
            ViewBag.LeaveTypeList = spService.GetDataWithParameter(new { Employee_Id = SessionHelper.LoggedInUserId_Hrm }, "FSP_GET_HR_LEAVE_TYPE").Tables[0].AsEnumerable().Select(row => new { LEAVE_TYPE_ID = row.Field<int>("LEAVE_TYPE_ID"), LEAVE_TYPE_NAME = row.Field<string>("LEAVE_TYPE_NAME") }).ToList();
            ViewBag.RELIEVERList = spService.GetDataWithParameter(new { PEMPLOYEE_ID = SessionHelper.LoggedInUserId_Hrm }, "FSP_GET_LEAVE_RELIEVER_INFO_Ex").Tables[0].AsEnumerable().Select(row => new { Reliever_EMP_ID = row.Field<int>("EMP_ID"), EMP_NAME = row.Field<string>("EMP_NAME") }).ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Approverlist"] = items;
            ViewData["Recommenderlist"] = items;
            return View();
        }

        public ActionResult frmRecom()
        {
            return View();
        }
        public ActionResult frmApprove()
        {
            return View();
        }
        public ActionResult frmApproveEx()
        {
            return View();
        }
        public ActionResult frmLeaveCancel()
        {
            return View();
        }

        #endregion

    }
}
