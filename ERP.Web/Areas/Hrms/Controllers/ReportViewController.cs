using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Hrms.CommonDropDownList;
using Hrms.HRMViewModel;

namespace Hrms.Controllers
{
    public class ReportViewController : BaseController
    {

        #region member
        private readonly ISPService spService;
        GetCommonDropDownList commonDropDownList;
        public ReportViewController(ISPService spService)
        {
            this.spService = spService;
            this.commonDropDownList = new GetCommonDropDownList();
        }
        #endregion

        // GET: ReportView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {

            return View();
        }

        #region Blood Group wise Employee list Report

        public ActionResult EmployeelistReport()
        {
            var model = new EmployeeReportViewModel();
            var itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            ViewBag.JobTypeList = spService.GetDataWithParameter(new { PJOB_ID = -1 }, "FSP_GET_HR_JOB_TYPE").Tables[0].AsEnumerable().Select(row => new { JOB_ID = row.Field<int>("JOB_ID"), JOB_NAME = row.Field<string>("JOB_NAME") }).ToList();
            ViewBag.BloodGroupList = spService.GetDataWithParameter(new { PBLOOD_GROUP_ID = -1 }, "FSP_GET_HR_BLOOD_GROUP").Tables[0].AsEnumerable().Select(row => new { BLOOD_GROUP_ID = row.Field<int>("BLOOD_GROUP_ID"), BLOOD_GROUP_NAME = row.Field<string>("BLOOD_GROUP_NAME") }).ToList();
            model.ReportNameList = itemList;
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["DesignationList"] = items;
            return View(model);
        }

        //public void EmpListBloodGroupwise(string PEFFECTIVE_date, string PDECLARATION_date, int PBRANCH_ID, int PDEPT_ID, int PDESK_ID, int PDESG_ID_FROM, int PDESG_ID_TO, int PBLOOD_GROUP_ID, int PEMP_ACTIVE_STATUS)
        //{
        //    var exportType = "pdf";
        //    var param = new { PEFFECTIVE_date = PEFFECTIVE_date, PDECLARATION_date = PDECLARATION_date, PBRANCH_ID = PBRANCH_ID, PDEPT_ID = PDEPT_ID, PDESK_ID = PDESK_ID, PDESG_ID_FROM = PDESG_ID_FROM, PDESG_ID_TO = PDESG_ID_TO, PBLOOD_GROUP_ID = PBLOOD_GROUP_ID, PEMP_ACTIVE_STATUS = PEMP_ACTIVE_STATUS };
        //    //var param = new { PEFFECTIVE_date = " ", PDECLARATION_date = " ", PBRANCH_ID=0, PDEPT_ID =-1, PDESK_ID=0, PDESG_ID_FROM=-1, PDESG_ID_TO=-1, PBLOOD_GROUP_ID=-1, PEMP_ACTIVE_STATUS=0 };
        //    var reportParam = new Dictionary<string, object> { { "logoPath", Server.MapPath("~/images/") } };
        //    var data = spService.GetDataWithParameter(param, "RSP_GET_EMP_LIST_BG_WISE").Tables[0];
        //    ReportHelper.ShowReport(data, exportType, "rpt_Employee_List_Blood_Group_Wise.rpt", "rpt_Employee_List_Blood_Group_Wise", reportParam);
        //}
        public void ShowEmployeelistReport(string rptslnx, string StartDate, string EndDate, int BranchId, int DepartmentId, int PDESK_ID, int JobTypeId, int PDESG_ID_FROM, int PDESG_ID_TO, int PBLOOD_GROUP_ID, int PEMP_ACTIVE_STATUS, string ReportType)
        {
            try
            {
                var param = new
                {
                    PEFFECTIVE_date = ReportHelper.FormatDateToString(StartDate),
                    PDECLARATION_date = ReportHelper.FormatDateToString(EndDate),
                    PBRANCH_ID = BranchId,
                    PDEPT_ID = DepartmentId,
                    PDESK_ID = PDESK_ID,
                    JobTypeId = JobTypeId,
                    PDESG_ID_FROM = PDESG_ID_FROM,
                    PDESG_ID_TO = PDESG_ID_TO,
                    PBLOOD_GROUP_ID = PBLOOD_GROUP_ID,
                    PEMP_ACTIVE_STATUS = PEMP_ACTIVE_STATUS

                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_EMP_LIST_BG_WISE");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_List_Blood_Group_Wise.rpt", "rpt_Employee_List_Blood_Group_Wise.cs");

                // ReportHelper.PrintReport("rpt_Employee_List_Blood_Group_Wise.rpt", MainReport.Tables[0], reportParam);
                //return Content(string.Empty);
            }
            catch (Exception ex)
            {
                //return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        #region EmployeeWiseAttendanceReport
        public ActionResult EmployeeWiseAttendanceReport()
        {
            return View();
        }

        public ActionResult ShowEmployeeWiseAttendanceReport(string rptslnx,string StartDate, string EndDate, string EmployeeOfficeCode, string ReportType)
        {
            try
            {
                /*RSP_GET_ATTENDANCE_EMPLOYEE_WS](
													@PATTENDANCE_STARTdatetime nvarchar(12),
                                                    @PATTENDANCE_ENDdatetime   nvarchar(12),
                                                    @PEMPLOYEE_ID          NUMERIC(10),
                                                    @PEMPLYEE_OFFICE_ID    NVARCHAR(55)*/
                var param = new
                {
                    PATTENDANCE_STARTdatetime = ReportHelper.FormatDateToString(StartDate),
                    PATTENDANCE_ENDdatetime = ReportHelper.FormatDateToString(EndDate),
                    //PEMPLOYEE_ID = EmployeeId,
                    PEMPLYEE_OFFICE_ID = EmployeeOfficeCode
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_ATTENDANCE_EMPLOYEE_WS");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                // ReportHelper.PrintReport("rpt_Employee_Attendence_Full.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Attendence_Full.rpt", "rpt_Employee_Attendence_Full.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region TypeWiseAttendanceReport
        public ActionResult TypeWiseAttendanceReport()
        {
            return View();
        }
        public ActionResult ShowTypeWiseAttendanceReport(string rptslnx,string StartDate, string EndDate, int BranchId, int DepartmentId, string AttendanceType, string ReportType)
        {
            try
            {
                var param = new { PATTENDANCE_STARTdatetime = ReportHelper.FormatDateToString(StartDate), PATTENDANCE_ENDdatetime = ReportHelper.FormatDateToString(EndDate), PBRANCH_ID = BranchId, PDEPT_ID = DepartmentId, PTYPE = AttendanceType };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_ATTENDANCE_TYPEWS");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                //ReportHelper.PrintReport("rpt_Employee_Attendence_Type_Wise.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Attendence_Type_Wise.rpt", "rpt_Employee_Attendence_Type_Wise.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion TypeWiseAttendanceReport

        #region Attendance Summary Report

        public ActionResult AttendanceSummaryReport()
        {
            var model = new EmployeeReportViewModel();
            var itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "Please Select", Value = "" });

            model.ReportNameList = itemList;
            return View(model);
        }
        public ActionResult ShowAttendanceSummaryReport(string rptslnx, string ReportName, string StartDate, string EndDate, int BranchId, int DepartmentId, string ReportType)
        {
            try
            {
                /*[RSP_GET_ATTENDANCE_DTWS]
												(     
													@PATTENDANCE_STARTdatetime    nvarchar(12)
                                                     , @PATTENDANCE_ENDdatetime    nvarchar(12)
                                                     , @PBRANCH_ID             numeric(10,0)
                                                     , @PDEPT_ID               numeric(10,0)
                                                     
												) */
                var param = new
                {
                    PATTENDANCE_STARTdatetime = ReportHelper.FormatDateToString(StartDate),
                    PATTENDANCE_ENDdatetime = ReportHelper.FormatDateToString(EndDate),
                    PBRANCH_ID = BranchId,
                    PDEPT_ID = DepartmentId
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_ATTENDANCE_SUMMARY");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                //ReportHelper.PrintReport("rpt_Employee_Attendence_Summery.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Attendence_Summery.rpt", "rpt_Employee_Attendence_Summery.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region Monthly Attendance Register

        public ActionResult MonthlyAttendanceRegister()
        {
            var model = new EmployeeReportViewModel();
            var itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "Please Select", Value = "" });

            model.ReportNameList = itemList;
            return View(model);
        }
        public ActionResult ShowMonthlyAttendanceRegister(string rptslnx,string ReportName, string StartDate, string EndDate, int? BranchId, int? DepartmentId, string ReportType)
        {
            try
            {
                /*[RSP_GET_ATTENDANCE_DTWS]
												(     
													@PATTENDANCE_STARTdatetime    nvarchar(12)
                                                     , @PATTENDANCE_ENDdatetime    nvarchar(12)
                                                     , @PBRANCH_ID             numeric(10,0)
                                                     , @PDEPT_ID               numeric(10,0)
                                                     
												) */
                var param = new
                {
                    PATTENDANCE_STARTdatetime = ReportHelper.FormatDateToString(StartDate),
                    PATTENDANCE_ENDdatetime = ReportHelper.FormatDateToString(EndDate),
                    PBRANCH_ID = BranchId,
                    PDEPT_ID = DepartmentId
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_ATTENDANCE_DTWS");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                // ReportHelper.PrintReport("rpt_Employee_Attendance_Register.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Attendance_Register.rpt", "rpt_Employee_Attendance_Register.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Branch and Department Wise
        public ActionResult BranchAndDepartmentWiseReport()
        {
            var model = new EmployeeReportViewModel();
            model.ReportNameList = commonDropDownList.getAttendanceReportName();
            return View(model);
        }
        public ActionResult ShowBranchAndDepartmentWiseReport(string rptslnx,string StartDate, string EndDate, int? BranchId, int? DepartmentId, string ReportType)
        {
            try
            {
                /*[RSP_GET_ATTENDANCE_DTWS]
												(     
													@PATTENDANCE_STARTdatetime    nvarchar(12)
                                                     , @PATTENDANCE_ENDdatetime    nvarchar(12)
                                                     , @PBRANCH_ID             numeric(10,0)
                                                     , @PDEPT_ID               numeric(10,0)
                                                     
												) */
                var param = new
                {
                    //PATTENDANCE_STARTdatetime = Convert.ToDateTime(StartDate).ToString("dd-MMM-yyyy"),
                    //PATTENDANCE_ENDdatetime = Convert.ToDateTime(EndDate).ToString("dd-MMM-yyyy"),
                    PATTENDANCE_STARTdatetime = ReportHelper.FormatDateToString(StartDate),
                    PATTENDANCE_ENDdatetime = ReportHelper.FormatDateToString(EndDate),
                    PBRANCH_ID = BranchId,
                    PDEPT_ID = DepartmentId
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_ATTENDANCE_DTWS");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                // ReportHelper.PrintReport("rpt_Employee_Attendance_Branch_Wise.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Attendance_Branch_Wise.rpt", "rpt_Employee_Attendance_Branch_Wise.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Employee Leave Type
        public ActionResult EmployeeLeaveDetailsReport()
        {
            return View();
        }

        public ActionResult ShowEmployeeLeaveDetailsReport(string rptslnx,string StartDate, string EndDate, int? EmployeeId, string EmployeeOfficeCode, int LeaveTypeId, string ReportType)
        {
            try
            {
                /*
                [RSP_GET_HR_LEAVE_DETAILS_ALL]
                                        ( @PEMP_ID numeric(10,0), @PEMP_OFFICE_CODE nvarchar(12),
                                             @PBRANCH_ID numeric(10,0), @PDEPT_ID numeric(10,0),
                                             @PFROM_date nvarchar(12),   @PTO_date nvarchar(12),  @PLEAVE_TYPE_ID numeric(10,0)
                                        )
            */
                var param = new
                {
                    PEMP_ID = EmployeeId,
                    PEMP_OFFICE_CODE = EmployeeOfficeCode,
                    PBRANCH_ID = 0,
                    PDEPT_ID = 0,
                    PFROM_date = ReportHelper.FormatDateToString(StartDate),
                    PTO_date = ReportHelper.FormatDateToString(EndDate),
                    PLEAVE_TYPE_ID = LeaveTypeId
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_HR_LEAVE_DETAILS_ALL");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                //ReportHelper.PrintReport("rpt_Employee_Leave_Range_Wise.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Leave_Range_Wise.rpt", "rpt_Employee_Leave_Range_Wise.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region LeaveSummerReport
        public ActionResult LeaveSummerReport()
        {
            var model = new EmployeeReportViewModel();
            model.EmployeeActiveStatusList = commonDropDownList.getEmployeeActiveStatus();
            return View(model);
        }
        public ActionResult ShowLeaveSummerReport(string rptslnx,string EndDate, string ReportType)
        {
            try
            {
                var param = new
                {
                    TO_DATE = ReportHelper.FormatDateToString(EndDate)
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_EMPLOYEE_WISE_LEAVE_SUMMARY");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                // ReportHelper.PrintReport("rpt_Employee_Leave_Summary.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Employee_Leave_Summary.rpt", "rpt_Employee_Leave_Summary.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Office Calendar Report

        public ActionResult OfficeCalendarReport()
        {
            var model = new EmployeeReportViewModel();
            var itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "Please Select", Value = "" });

            model.ReportNameList = itemList;
            return View(model);
        }
        public ActionResult ShowOfficeCalendarReport(string rptslnx,string StartDate, string EndDate, int BranchId, string DayType, int PLATE_COUNT, int PABSENT_COUNT, string ReportType)
        {
            try
            {
                var param = new
                {
                    PSTART_datetime = ReportHelper.FormatDateToString(StartDate),
                    PEND_datetime = ReportHelper.FormatDateToString(EndDate),
                    PBRANCH_ID = BranchId,
                    PTRANSACTION_TYPE = DayType,
                    PLATE_COUNT = PLATE_COUNT,
                    PABSENT_COUNT = PABSENT_COUNT
                };
                var MainReport = spService.GetDataWithParameter(param, "RSP_GET_HR_HOLIDAY");

                var reportParam = new Dictionary<string, object>();

                //ReportHelper.PrintWithSubReport("rpt_NU_AppointmentLetter.rpt", MainReport.Tables[0], new Dictionary<string, object>(),subReportDB, new rpt_NU_AppointmentLetter());
                //ReportHelper.PrintReport("rpt_Employee_Attendence_Summery.rpt", MainReport.Tables[0], reportParam);
                ReportHelper.ShowReport(MainReport.Tables[0], ReportType, "rpt_Office_Calender.rpt", "rpt_Office_Calender.cs");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


    }
}