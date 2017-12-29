using System;
using System.Web.Mvc;
using Common.Service;
using Common.Service.ReportServies;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Service;

namespace Upms.Controllers
{
    public class DayEndController : BaseController
    {
        private readonly IDayEndService dayEndService;
        // private readonly IOfficeService officeService;
        private readonly IDayInitialService dayInitialService;
        private readonly IWeeklyReportService weeklyReportService;
        public DayEndController(IDayEndService dayEndService, IDayInitialService dayInitialService, IWeeklyReportService weeklyReportService)
        {
            this.dayEndService = dayEndService;
            //this.officeService = officeService;
            this.dayInitialService = dayInitialService;
            this.weeklyReportService = weeklyReportService;
        }
        // GET: DayEnd
        public ActionResult Index()
        {
            DateTime vdate = TransactionDate;
            var model = new DayEndViewModel();
            if (IsDayInitiated)

                model.BusinessDate = vdate;
            //MapDropDownList(model);

            return View(model);
        }
        //private void MapDropDownList(DayEndViewModel model)
        //{

        //    var alloffice = officeService.GetAll().Where(d => d.OfficeID == LoginUserOfficeID && d.OrgID==LoggedInOrganizationID);

        //    var viewOffice = alloffice.Select(m => new SelectListItem() { Text = string.Format("{0} - {1}", m.OfficeCode, m.OfficeName), Value = m.OfficeID.ToString() });

        //    model.officeListItems = viewOffice;

        //}
        public JsonResult DayEndProcess(string officeId, string businessDate)
        {
            try
            {
                if (!IsDayInitiated)
                {
                    return GetErrorMessageResult("Please run the start work process");
                }
                //dayEndService.DayEndProcess(SessionHelper.LoginUserOfficeID.Value, TransactionDate,LoggedInOrganizationID);
                var param = new { OfficeId = 1, BusinessDate = businessDate, OrgID = 1 };
                weeklyReportService.DayEndProcess(param);

                try
                {
                    DateTime? transactionDate;
                    string OrginizationName;
                    string Processtype;
                    DateTime? LastDayEndDate;
                    var dayInitialStatus = dayInitialService.ValidateDayInitial(1, out transactionDate, out OrginizationName, out Processtype, out   LastDayEndDate, 1);

                    SessionHelper.TransactionDay = dayInitialStatus;
                    SessionHelper.TransactionDate = transactionDate.HasValue ? transactionDate.Value : default(DateTime);
                    SessionHelper.OrganizationName = OrganizationName;
                    SessionHelper.ProcessType = ProcessType;
                    SessionHelper.LastDayEndDate = LastDayEndDate;
                    SessionHelper.IsDayInitiated = !string.IsNullOrEmpty(dayInitialStatus);

                }
                catch (Exception ex)
                {
                    SessionHelper.TransactionDay = "";
                    SessionHelper.TransactionDate = default(DateTime);
                    SessionHelper.IsDayInitiated = false;
                    return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return GetErrorMessageResult(ex);
                // return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        // GET: DayEnd/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: DayEnd/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: DayEnd/Create
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
        // GET: DayEnd/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: DayEnd/Edit/5
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
        // GET: DayEnd/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: DayEnd/Delete/5
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
