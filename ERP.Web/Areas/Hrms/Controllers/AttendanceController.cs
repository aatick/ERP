//using System.Transactions;
//using DotNetOpenAuth.AspNet;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;
//using ERP.Web.ViewModels;
//using AutoMapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;

//using UcasPortfolio.Service.StoredProcedure;


namespace Hrms.Controllers
{
    public class AttendanceController : BaseController
    {

        #region Variables

        private readonly ISPService spService;
        public AttendanceController(ISPService spService)
        {
            this.spService = spService;
        }
        #endregion

        #region Methods

        public JsonResult GetEmployeeAccountInfo(string EmployeeCode)
        {
            try
            {
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
        public JsonResult DeleteManualAttendanceVS(int Id)
        {
            try
            {
                spService.GetDataBySqlCommand("DELETE SAL_MONTHLY_ATTEN_MANUAL WHERE Id = "+Id+"");

                return Json(new { Result = "Success", Message = "DELETE successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RejectOSDAttendance(int Id)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE SAL_MONTHLY_ATTEN_MANUAL SET approval_status = 3,approv_date = GETDATE() WHERE Id = " + Id + "");

                return Json(new { Result = "Success", Message = "OSD Attendance reject successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ApproveOSDAttendance(int Id)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE SAL_MONTHLY_ATTEN_MANUAL SET approval_status = 1,approv_date = GETDATE() WHERE Id = " + Id + "");

                return Json(new { Result = "Success", Message = "OSD Attendance approve successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetManualAttendanceForApprove()
        {
            try
            {
                var param = new { trans_type = "VS", approval_status = 0, Approver = SessionHelper.LoggedInUserId_Hrm, AddDate = "", EmployeeId = 0 };
                var manuAttennfo = spService.GetDataWithParameter(param, "GetManualAttendance").Tables[0];
                var manuAttennfoList = manuAttennfo.AsEnumerable()
                .Select(
                    x =>
                        new
                        {
                            Id = x.Field<int>("Id"),
                            EMP_ID = x.Field<int>("employee_id"),
                            emp_name = x.Field<string>("emp_name"),
                            atten_date = x.Field<string>("atten_date"),
                            atten_type = x.Field<string>("atten_type"),
                            purpose = x.Field<string>("purpose"),
                            approval_statusMsg = x.Field<string>("approval_statusMsg"),
                            approv_emp_id = x.Field<int?>("approv_emp_id"),
                            Approver = x.Field<string>("Approver"),
                        }).ToList();

                return Json(manuAttennfoList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEmployeeInfo()
        {
            try
            {
                var param = new { EmployeeCode = SessionHelper.EmployeeCode };//SessionHelper.UserCode

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
                                emp_dept_id = x.Field<int?>("emp_dept_id"),
                                bank_id = x.Field<int?>("emp_salary_acc_bank_id"),//
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

        public JsonResult GetManualAttendance(string trans_type, int approval_status, int Approver, string AddDate, string EmployeeId)
        {
            try
            {
                if (EmployeeId == "")
                {
                    EmployeeId = SessionHelper.LoggedInUserId_Hrm.ToString();
                }
                var param = new { trans_type = trans_type, approval_status = approval_status, Approver = Approver, AddDate = DateTime.Now, EmployeeId = EmployeeId };
                //,V.atten_type,V.trans_type,V.purpose,V.approval_status,V.approv_emp_id,EA.emp_office_code+' - '+EA.emp_name AS Approver,CONVERT(VARCHAR,V.approv_date,103) AS approv_date
                var manuAttennfo = spService.GetDataWithParameter(param, "GetManualAttendance").Tables[0];

                var manuAttennfoList = manuAttennfo.AsEnumerable()
                .Select(
                    x =>
                        new
                        {
                            Id = x.Field<int>("Id"),
                            EMP_ID = x.Field<int>("employee_id"),
                            emp_name = x.Field<string>("emp_name"),
                            atten_date = x.Field<string>("atten_date"),
                            atten_type = x.Field<string>("atten_type"),
                            purpose = x.Field<string>("purpose"),
                            approval_statusMsg = x.Field<string>("approval_statusMsg"),
                            approv_emp_id = x.Field<int?>("approv_emp_id"),
                            Approver = x.Field<string>("Approver"),
                        }).ToList();

                return Json(manuAttennfoList, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                  return Json(new {Result = "Error",Message =ex.Message },JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveManualAttendanceVS(int AttendanceId,int EmployeeId, string MnlAttenTypeList, string TRANS_TYPE, string AttendanceDateFrom, string AttendanceDateTo, string Remarks, int Approval_status, int ApproverId)
        {
            try
            {
                if (AttendanceId == 0)
                {
                    var param = new { PEMPLOYEE_ID = EmployeeId, PATTEN_DATE_FROM = ReportHelper.FormatDateToString(AttendanceDateFrom), PATTEN_DATE_TO = ReportHelper.FormatDateToString(AttendanceDateTo), PATTEN_TYPE = "PR", TRANS_TYPE = TRANS_TYPE, PBRANCH_ID = SessionHelper.LoggedInOfficeId, PPURPOSE = Remarks, PREC_STATUS = 1, PUSER_ID = SessionHelper.LoggedInUserId_Hrm, Approval_status = Approval_status, ApproverId = ApproverId };
                    spService.GetDataWithParameter(param, "FSP_ADD_UP_ATTEN_MANUAL_ENTRY");
                }
                else
                {
                    spService.GetDataBySqlCommand("UPDATE SAL_MONTHLY_ATTEN_MANUAL SET  ATTEN_DATE = '" + ReportHelper.FormatDateToString(AttendanceDateFrom) + "', PURPOSE = '" + Remarks + "', EDIT_DATE = GETDATE(), UPDATED_BY = " + SessionHelper.LoggedInUserId_Hrm + ", approv_emp_id = " + ApproverId + " WHERE Id = " + AttendanceId + "");
                }
               
                return Json(new { Result = "Success", Message = "Save successfull. " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
        
        
        
        public JsonResult SaveManualAttendance(int EmployeeId, string MnlAttenTypeList, string TRANS_TYPE, string AttendanceDateFrom, string AttendanceDateTo, string Remarks, int Approval_status, int ApproverId)
        {
            try
            {
                var param = new { PEMPLOYEE_ID = EmployeeId, PATTEN_DATE_FROM = ReportHelper.FormatDateToString(AttendanceDateFrom), PATTEN_DATE_TO = ReportHelper.FormatDateToString(AttendanceDateTo), PATTEN_TYPE = MnlAttenTypeList, TRANS_TYPE = TRANS_TYPE, PBRANCH_ID = SessionHelper.LoggedInOfficeId, PPURPOSE = Remarks, PREC_STATUS = 1, PUSER_ID = SessionHelper.LoggedInUserId_Hrm, Approval_status = Approval_status, ApproverId = ApproverId };
                spService.GetDataWithParameter(param,"FSP_ADD_UP_ATTEN_MANUAL_ENTRY");
                return Json(new { Result = "Success", Message = "Save successfull. " }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new {Result = "Error",Message =ex.Message },JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Events

        public ActionResult Index()
        {
            ViewBag.OfficeBranchList = spService.GetDataBySqlCommand("SELECT B.branch_id AS BranchId,B.branch_office_code,B.branch_name,B.bb_branch_code +' - '+B.branch_name AS BranchName FROM HR_BRANCH_INFORMATION AS B").Tables[0].AsEnumerable().Select(row => new { BranchId = row.Field<int>("BranchId"), BranchName = row.Field<string>("BranchName") }).ToList();
            return View();
        }

        public ActionResult ManualAtn()
        {
            ViewBag.MnlAttenTypeList = spService.GetDataBySqlCommand("SELECT A.manual_atten_type_id,A.atten_type,A.type_description FROM HR_MANUAL_ATTEN_TYPE AS A WHERE A.type_description <> 'Visit Permission'").Tables[0].AsEnumerable().Select(row => new { AttenTypeId = row.Field<int>("manual_atten_type_id"), AttenType = row.Field<string>("atten_type"), TypeDescription = row.Field<string>("type_description") }).ToList();
            return View();
        }

        public ActionResult ManualAtnVS()
        {
            ViewBag.MnlAttenTypeList = spService.GetDataBySqlCommand("SELECT A.manual_atten_type_id,A.atten_type,A.type_description FROM HR_MANUAL_ATTEN_TYPE AS A WHERE A.type_description = 'Visit Permission'").Tables[0].AsEnumerable().Select(row => new { AttenTypeId = row.Field<int>("manual_atten_type_id"), AttenType = row.Field<string>("atten_type"), TypeDescription = row.Field<string>("type_description") }).ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Approverlist"] = items;
            
            return View();
        }
        public ActionResult ManualAtnVSApprove()
        {

            return View();
        }

        #endregion
    }
}
