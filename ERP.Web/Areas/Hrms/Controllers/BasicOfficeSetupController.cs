using System;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Hrms.CommonDropDownList;
using Hrms.HRMViewModel;

namespace Hrms.Controllers
{
    public class BasicOfficeSetupController : BaseController
    {

        #region Member
        // GET: BasicOfficeSetup
        public readonly GetCommonDropDownList getCommonDropDownList;
        private readonly ISPService spService;
        public BasicOfficeSetupController(ISPService spService)
        {
            getCommonDropDownList = new GetCommonDropDownList();
            this.spService = spService;
        }
        #endregion

        #region Action
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OfficeCalendar()
        {
            BasicOfficeSetupViewModel model = new BasicOfficeSetupViewModel();
            model.trans_yearList = getCommonDropDownList.getOfficeCalenderYear();
            return View(model);
        }

        public ActionResult GovernmentHoliday()
        {
            GovernmentHolidayViewModel model = new GovernmentHolidayViewModel();
            model.HolidayYearList = getCommonDropDownList.getOfficeCalenderYear();
            return View(model);
        }

        public ActionResult SpecialOfficeDay()
        {
            return View();
        }
        #endregion

        #region  method
        public JsonResult SaveOfficeCalendar(BasicOfficeSetupViewModel model)
        {
            try
            {

                string StartDate = "01-JAN-" + model.trans_year;
                string ENDDate = "31-DEC-" + model.trans_year;
                string OFFICEINTIME = model.office_intime;
                string OFFICEOUTTIME = model.office_outtime;
                string ABSENTTIME = model.absent_time;
                //string HALFOFFICEOUTTIME = model.half_OfficeTIme;
                string HALFOFFICEOUTTIME = model.half_office_outtime;
                string OFFICELATETIME = model.late_time;
                int PUSER_ID = SessionHelper.LoggedInUserId;
                int? noOfHoliDAY = model.noOfHoliDAY;



                var parm = new
                {
                    StartDate = StartDate,
                    ENDDate = ENDDate,
                    OFFICEINTIME = OFFICEINTIME,
                    OFFICEOUTTIME = OFFICEOUTTIME,
                    ABSENTTIME = ABSENTTIME,
                    HALFOFFICEOUTTIME = HALFOFFICEOUTTIME,
                    OFFICELATETIME = OFFICELATETIME,
                    PUSER_ID = PUSER_ID,
                    noOfHoliDAY = noOfHoliDAY
                };
                 spService.GetDataWithParameter(parm, "FSP_SET_OFFICE_TIME");

                return Json(new { Result = "Success", Message = "Office time setup successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
        }

        public JsonResult SaveGovernmentHoliday(GovernmentHolidayViewModel model)
        {
            try
            {

                string P_STARTdatetime = ReportHelper.FormatDateToString(model.StartDate);
                string P_ENDdatetime = ReportHelper.FormatDateToString(model.EndDate);
                int PUSER_ID = SessionHelper.LoggedInUserId; 
                string P_HOLIDAYPURPOSE = model.Purpose;
                string P_REMARK = model.Remarks;


                var parm = new
                {
                    P_STARTdatetime = P_STARTdatetime,
                    P_ENDdatetime = P_ENDdatetime,
                    PUSER_ID = PUSER_ID,
                    P_HOLIDAYPURPOSE = P_HOLIDAYPURPOSE,
                    P_REMARK = P_REMARK
                };
                 spService.GetDataWithParameter(parm, "FSP_SET_HOLIDAY");

                return Json(new { Result = "Success", Message = "Holiday setup successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
        }

        public JsonResult SaveSpecialOfficeDay(SpecialOfficeDayViewModel model)
        {
            try
            {

                string P_STARTdatetime = ReportHelper.FormatDateToString(model.StartDate);
                string P_ENDdatetime = ReportHelper.FormatDateToString(model.EndDate);
                string P_OFFICEINTIME = model.office_intime;
                string P_OFFICEOUTTIME = model.office_outtime;
                string P_ABSENTTIME = model.absent_time;
                string P_HALFOFFICEOUTTIME = model.half_OfficeTIme;
                string P_OFFICELATETIME = model.late_time;
                int PUSER_ID = SessionHelper.LoggedInUserId; 
                int P_LATE_COUNT = Convert.ToInt32(model.IsLateCount);
                string P_HOLIDAYPURPOSE = model.Purpose;
                int P_HOLIDAY_AS_OFFICEDAY = Convert.ToInt32(model.IsSetHoliDay);
                int P_ABSENT_COUNT = Convert.ToInt32(model.IsAbsentCount);
                int P_SATURDAY_AS_MANDATRYDAY = Convert.ToInt32(model.IsSetDayMandatory);
                string P_REMARK = model.Remarks;
                int noOfHoliDAY = model.noOfHoliDAY;


                /*
               [FSP_SET_SPECIAL_OFFICE_TIME]
                               (
                                @P_STARTdatetime  VARCHAR(12), @P_ENDdatetime  VARCHAR(12),
                                @P_OFFICEINTIME	VARCHAR(12),  @P_OFFICEOUTTIME	VARCHAR(12),
                                @P_ABSENTTIME	VARCHAR(12), @P_HALFOFFICEOUTTIME	 VARCHAR(12),
                                @P_OFFICELATETIME VARCHAR(12),  @PUSER_ID NUMERIC(10),
                                @P_LATE_COUNT NUMERIC(10),  @P_HOLIDAYPURPOSE		 VARCHAR(55),
                                @P_HOLIDAY_AS_OFFICEDAY  INT      @P_ABSENT_COUNT			 INT                                   ,
                                @P_SATURDAY_AS_MANDATRYDAY INT  @P_REMARK               VARCHAR(150),
								@noOfHoliDAY			INT

                                )
                 
                 */

                var parm = new
                {
                    P_STARTdatetime = P_STARTdatetime,
                    P_ENDdatetime = P_ENDdatetime,
                    P_OFFICEINTIME = P_OFFICEINTIME,
                    P_OFFICEOUTTIME = P_OFFICEOUTTIME,
                    P_ABSENTTIME = P_ABSENTTIME,
                    P_HALFOFFICEOUTTIME = P_HALFOFFICEOUTTIME,
                    P_OFFICELATETIME = P_OFFICELATETIME,
                    PUSER_ID = PUSER_ID,
                    P_LATE_COUNT = P_LATE_COUNT,
                    P_HOLIDAYPURPOSE = P_HOLIDAYPURPOSE,
                    P_HOLIDAY_AS_OFFICEDAY = P_HOLIDAY_AS_OFFICEDAY,
                    P_ABSENT_COUNT = P_ABSENT_COUNT,
                    P_SATURDAY_AS_MANDATRYDAY = P_SATURDAY_AS_MANDATRYDAY,
                    P_REMARK = P_REMARK,
                    noOfHoliDAY = noOfHoliDAY

                };
                var result = spService.GetDataWithParameter(parm, "FSP_SET_SPECIAL_OFFICE_TIME");

                return Json(new { Result = "Success", Message = "Special day setup successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
        }
        #endregion
    }
}