using System;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Service;

namespace Upms.Controllers
{
    public class DayInititalController : BaseController
    {

        private readonly IDayInitialService dayInitialService;
        private readonly IAspNetUserService aspNetUserService;
        private readonly ISPService spService;
        // private readonly IOfficeService officeService;
        public DayInititalController(IDayInitialService dayInitialService, ISPService spService, IAspNetUserService aspNetUserService)
        {
            this.dayInitialService = dayInitialService;
            this.spService = spService;
            this.aspNetUserService = aspNetUserService;
        }
        // GET: DayInitital
        public ActionResult Index()
        {
            // DateTime vdate = TransactionDate;
            var model = new DayInitialViewModel();
            //if (IsDayInitiated)

            model.BusinessDate = DateTime.Now;
            // MapDropDownList(model);
            return View(model);
        }
        //private void MapDropDownList(DayInitialViewModel model)
        //{

        //    var alloffice = officeService.GetAll().Where(d => d.OfficeID == LoginUserOfficeID && d.OrgID == LoggedInOrganizationID);

        //    var viewOffice = alloffice.Select(m => new SelectListItem() { Text = string.Format("{0} - {1}", m.OfficeCode, m.OfficeName), Value = m.OfficeID.ToString() });

        //    model.officeListItems = viewOffice;

        //}
        // GET: DayInitital/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: DayInitital/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: DayInitital/Create
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
        // [HttpPost]
        public JsonResult DayInitialProcess(string businessDate)
        {
            try
            {
                var param = new { OfficeId = SessionHelper.LoggedInOfficeId, BusinessDate = ReportHelper.FormatDateToString(businessDate), CreateUser = SessionHelper.LoggedInUserId, CreateDate = DateTime.Now };
                spService.GetDataWithParameter(param, "Prcs_DayInitial");
                var user = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId);
                return Json(new { Result = "SUCCESS", UserName = user.UserName, Password = user.Hashing, Message = "Day initial process successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionHelper.IsDayInitiated = false;
                return Json(new { Result = "ERROR", Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: DayInitital/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: DayInitital/Edit/5
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
        // GET: DayInitital/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: DayInitital/Delete/5
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
