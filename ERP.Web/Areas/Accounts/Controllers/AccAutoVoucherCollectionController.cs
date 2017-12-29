using System;
using System.Web.Mvc;
using Accounts.Service;
using Common.Service.ReportServies;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
namespace Accounts.Controllers
{
    public class AccAutoVoucherCollectionController :  BaseController
    {
        private readonly IWeeklyReportService weeklyReportService;
        private readonly IAccAutoVoucherCollectionService accAutoVoucherCollectionService;
        private readonly ISPService spService;
        //private readonly IOfficeService officeService;
        public AccAutoVoucherCollectionController(IAccAutoVoucherCollectionService accAutoVoucherCollectionService, IWeeklyReportService weeklyReportService, ISPService spService)
          {
              this.accAutoVoucherCollectionService = accAutoVoucherCollectionService;
              //this.officeService = officeService;
              this.weeklyReportService = weeklyReportService;
              this.spService = spService;
          }
        // GET: AccAutoVoucherCollection
        public ActionResult Index()
        {
            var model = new AccAutoVoucherCollectionViewModel() ;

            if (IsDayInitiated)
                model.BusinessDate =SessionHelper.BusinessDate;
            //MapDropDownList(model);

            return View(model);
        }
        public JsonResult AutoVoucherCollectionProcess(string businessDate)
        {
            var Result = "";
            try
            {
                if (!IsDayInitiated)
                {
                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                   // return GetErrorMessageResult("Please run the start work process");
                }
                else
                {
                    var param = new { OfficeID = SessionHelper.LoggedInOfficeId, TrxDate = businessDate, CreateUser = SessionHelper.LoggedInUserId };
                    spService.GetDataWithParameter(param, "AutoVoucherCollection");

                    return Json(Result = "OK", JsonRequestBehavior.AllowGet);
                }
        
            }
            catch (Exception ex)
            {
                return Json( Result = ex.Message , JsonRequestBehavior.AllowGet);
            }


        }
        private void MapDropDownList(AccAutoVoucherCollectionViewModel model)
        {

            //var alloffice = officeService.GetAll().Where(a => a.OfficeID == LoginUserOfficeID && a.OrgID==LoggedInOrganizationID);

            //var viewOffice = alloffice.Select(m => new SelectListItem() { Text = string.Format("{0} - {1}", m.OfficeCode, m.OfficeName), Value = m.OfficeID.ToString() });

            //model.officeListItems = viewOffice;

        }
        // GET: AccAutoVoucherCollection/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccAutoVoucherCollection/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccAutoVoucherCollection/Create
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

        // GET: AccAutoVoucherCollection/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccAutoVoucherCollection/Edit/5
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

        // GET: AccAutoVoucherCollection/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccAutoVoucherCollection/Delete/5
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
