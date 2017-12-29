using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounts.Data.AccountsDataModel;
using Accounts.Service;
using AutoMapper;
using Common.Service;
using Common.Service.ReportServies;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;

namespace Accounts.Controllers
{
    public class BudgetController : BaseController
    {
        #region Variables
        private readonly IAccChartService accChartService;
        private readonly IBudgetService budgetService;
       // private readonly IOfficeService officeService;
        private readonly IAccReportService accReportService;
        public BudgetController(IAccChartService accChartService, IBudgetService budgetService, IAccReportService accReportService)//, IOfficeService officeService
        {
            this.accChartService = accChartService;
            this.budgetService = budgetService;
           // this.officeService = officeService;
            this.accReportService = accReportService;
            //this.purposeService = purposeService;
        }
        #endregion

        #region Methods
        public JsonResult GetBudgetList(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                var budgetDetail = budgetService.GetAll().Where(m => m.IsActive == true && m.OrgID == 1 && m.OfficeID == 1);                
                var viewDetail = Mapper.Map<IEnumerable<Budget>, IEnumerable<BudgetViewModel>>(budgetDetail);
                List<BudgetViewModel> detail = new List<BudgetViewModel>();
                int row_indx = 1;
                foreach (var vd in viewDetail)
                {
                    var accName = accChartService.GetById(vd.AccID);
                    var details = new BudgetViewModel() { SlNo = row_indx, BudgetID = vd.BudgetID, TrxDate = vd.TrxDate, BudgetYear = vd.BudgetYear, AccName = accName.AccCode + ", " + accName.AccName, BudgetAmount = vd.BudgetAmount };
                    detail.Add(details);
                    row_indx++;
                }
                var currentPageCodes = detail.ToList().Skip(jtStartIndex).Take(jtPageSize);

                return Json(new { Result = "OK", Records = currentPageCodes, TotalRecordCount = row_indx - 1 });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public ActionResult DeleteBudget(string BudgetId)
        {
            var bud = budgetService.GetById(Convert.ToInt32(BudgetId));
            bud.IsActive = false;
            bud.InActiveDate = DateTime.Now;
            budgetService.Update(bud);
            return RedirectToAction("Index");
        }
        public ActionResult GenerateBudgetReport(string office_id, string from_date, string to_date)
        {
            try
            {

                var param = new {from_date=ReportHelper.FormatDateToString(from_date),to_date=ReportHelper.FormatDateToString(to_date), office_id = office_id };
                var allVouchers = accReportService.GetBudgetReport(param);
                var reportParam = new Dictionary<string, object>();
                reportParam.Add("OrgName", ApplicationSettings.OrganiztionName);
                reportParam.Add("DateFrom", from_date);
                reportParam.Add("DateTo", to_date);
                ReportHelper.PrintReport("rpt_acc_budget.rpt", allVouchers.Tables[0], reportParam);

                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion

        #region Events
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new BudgetViewModel();
            model.TrxDate = TransactionDate;
            model.OfficeID = 1;// Convert.ToInt32(LoginUserOfficeID);
            model.OfficeLevel = 1;// officeService.GetById(model.OfficeID).OfficeLevel;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BudgetViewModel model)
        {
            try
            {
                var entity = Mapper.Map<BudgetViewModel, Budget>(model);
                entity.IsActive = true;
                entity.OrgID = 1;//Convert.ToInt32(LoggedInOrganizationID);
                entity.OfficeID = SessionHelper.LoggedInOfficeId;
                budgetService.Create(entity);
                return GetSuccessMessageResult();
            }
            catch(Exception ex)
            {
                return GetErrorMessageResult(ex);
            }
        }

        public ActionResult Edit(int id)
        {
            var budget = budgetService.GetById(id);
            var budgetModel = Mapper.Map<Budget, BudgetViewModel>(budget);
            var acc = accChartService.GetById(budget.AccID);
            budgetModel.AccName = string.Format("{0} - {1}", acc.AccCode, acc.AccName);
            budgetModel.OfficeLevel = 1;// officeService.GetById(budget.OfficeID).OfficeLevel;
            return View(budgetModel);
        }

        [HttpPost]
        public ActionResult Edit(BudgetViewModel model)
        {
            try
            {
                var entity = budgetService.GetById(Convert.ToInt32(model.BudgetID));
                if (ModelState.IsValid)
                {
                    entity.TrxDate = model.TrxDate;
                    entity.BudgetYear = model.BudgetYear;
                    entity.AccID = model.AccID;
                    entity.BudgetAmount = model.BudgetAmount;
                    entity.OfficeID = SessionHelper.LoggedInOfficeId;
                    budgetService.Update(entity);
                    return GetSuccessMessageResult();
                }
                else
                    return GetErrorMessageResult();
            }
            catch (Exception ex)
            {
                return GetErrorMessageResult(ex);
            }
        }
        public ActionResult BudgetReport()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            //ViewData["HOList"] = items;
            //ViewData["ZoneList"] = items;
            //ViewData["AreaList"] = items;
            //ViewData["OfficeList"] = items;
            //var offcdetail = officeService.GetById(Convert.ToInt32(LoginUserOfficeID));
            //ViewData["OfficeLevel"] = offcdetail.OfficeLevel;
            //ViewData["FirstLevel"] = officeService.GetByOfficeCode(offcdetail.FirstLevel).OfficeID;
            //ViewData["SecondLevel"] = officeService.GetByOfficeCode(offcdetail.SecondLevel).OfficeID;
            //ViewData["ThirdLevel"] = officeService.GetByOfficeCode(offcdetail.ThirdLevel).OfficeID;
            //ViewData["FourthLevel"] = officeService.GetByOfficeCode(offcdetail.FourthLevel).OfficeID;
            //ViewData["TrxDate"] = TransactionDate.ToString("dd-MMM-yyyy");
            return View();
        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Budget/Delete/5
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
        #endregion
    }
}
