using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;

namespace Upms.Controllers
{
    public class DayProcessController : BaseController
    {
        private readonly ISPService spService;
        private readonly IAspNetUserService aspNetUserService;

        public DayProcessController(ISPService spService, IAspNetUserService aspNetUserService)
        {
            this.spService = spService;
            this.aspNetUserService = aspNetUserService;
        }
        //SELECT CONVERT(VARCHAR,C.CalendarDate,105) AS CalendarDate  FROM CalendarDay AS C WHERE C.IsActive = 1 AND C.IsWorkingDay = 0

        public JsonResult GetHolidays()
        {
            var HoliDays = spService.GetDataBySqlCommand("SELECT C.Id, CONVERT(VARCHAR, DATEPART(DAY,C.CalendarDate)) +'-'+  CONVERT(VARCHAR,DATEPART(MONTH,C.CalendarDate))+'-'+  CONVERT(VARCHAR,DATEPART(YEAR,C.CalendarDate)) AS CalendarDate FROM CalendarDay AS C WHERE C.IsActive = 1 AND C.IsWorkingDay = 0").Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     CalendarDate = row.Field<string>("CalendarDate")
                 }).ToList();
            return Json(HoliDays, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWorkingDays()
        {
            var WorkingDay = spService.GetDataBySqlCommand("SELECT C.Id, CONVERT(VARCHAR, DATEPART(DAY,C.CalendarDate)) +'-'+  CONVERT(VARCHAR,DATEPART(MONTH,C.CalendarDate))+'-'+  CONVERT(VARCHAR,DATEPART(YEAR,C.CalendarDate)) AS CalendarDate FROM CalendarDay AS C WHERE C.IsActive = 1 AND C.IsWorkingDay = 1").Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     CalendarDate = row.Field<string>("CalendarDate")
                 }).ToList();
            return Json(WorkingDay, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDayClosing()
        {
            if (!string.IsNullOrEmpty(SessionHelper.NotProcessedFile))
                return Json(new { Result = "ERROR", Message = SessionHelper.NotProcessedFile + " has been uploaded but not processed !!!" }, JsonRequestBehavior.AllowGet);
            var jgh = SessionHelper.LoggedInOfficeId;
            try
            {
                var param = new { OfficeId = SessionHelper.LoggedInOfficeId, BusinessDate = ReportHelper.FormatDateToString(SessionHelper.BusinessDate), CreateUser = SessionHelper.LoggedInUserId, CreateDate = DateTime.Now };
                var dt = spService.GetDataWithParameter(param, "Prcs_DayEnd");
                var user = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId);
                return Json(new { Result = "SUCCESS", Message = "Day closing process successfull.", UserName = user.UserName, Password = user.Hashing }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //SessionHelper.IsDayInitiated = false;
                return Json(new { Result = "ERROR", Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveWorkingDay(string Description, string FromDate, string ToDate)
        {
            var param = new { DeclarationStartDateA = ReportHelper.FormatDateToString(FromDate), DeclarationeNDDateA = ReportHelper.FormatDateToString(ToDate), DayTypeIdA = 1, DescrittionA = Description, USER_ID = SessionHelper.LoggedInUserId };
            spService.GetDataWithParameter(param, "SpecialWorkingDayDeclaration");

            var Result = "Ok";
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveHoliday(int DaytypeId, string Description, string FromDate, string ToDate)
        {
            var param = new { DeclarationStartDateA = ReportHelper.FormatDateToString(FromDate), DeclarationeNDDateA = ReportHelper.FormatDateToString(ToDate), DayTypeIdA = DaytypeId, DescrittionA = Description, USER_ID = SessionHelper.LoggedInUserId };
            spService.GetDataWithParameter(param, "SpecialHolidayDeclaration");
            var Result = "Ok";
            return Json(Result, JsonRequestBehavior.AllowGet);
            //return Json(new {Result = "Ok",JsonRequestBehavior.AllowGet });
        }

        public ActionResult Index()
        {
            ViewBag.HohidayTypeList = spService.GetDataBySqlCommand("SELECT C.Id,C.DayTypeName FROM CalendarDayType C WHERE C.DayTypeName<>'Working Day'").Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     DayTypeName = row.Field<string>("DayTypeName")
                 }).ToList();
            var TransactionDay = ReportHelper.FormatDateToString(SessionHelper.BusinessDate);
            var dates = spService.GetDataWithoutParameter("SP_GET_DAY_INITIAL_END_DATE");
            ViewBag.DayInitialDate = "";
            ViewBag.DayEndDate = "";
            if (dates.Tables.Count > 0)
            {
                var initialEndDate = dates.Tables[0].AsEnumerable().Select(X => new { DayInitialDate = X.Field<string>(0), DayEndDate = X.Field<string>(1) }).ToList().FirstOrDefault();
                ViewBag.DayInitialDate = initialEndDate.DayInitialDate;
                ViewBag.DayEndDate = initialEndDate.DayEndDate;
            }
            return View();
        }
        public ActionResult NotDayInitial()
        {
            return View();
        }

    }
}
