//using System.Web.UI.WebControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class CompanyListedToDepositoryCompanyController : BaseController
    {

        #region Variables
        private readonly ICompanyListedToDepositoryCompanyService companyListedToDepositoryCompanyService;
        private readonly ICompanyInfoService companyInfoService;
        private readonly ICompanyDepositoryService companyDepositoryService;
        private readonly ISPService spService;

        public CompanyListedToDepositoryCompanyController(ICompanyListedToDepositoryCompanyService companyListedToDepositoryCompanyService, ICompanyInfoService companyInfoService, ICompanyDepositoryService companyDepositoryService, ISPService spService)
        {
            this.companyListedToDepositoryCompanyService = companyListedToDepositoryCompanyService;
            this.companyInfoService = companyInfoService;
            this.companyDepositoryService = companyDepositoryService;
            this.spService = spService;
        }

        #endregion

        #region Methods

        public JsonResult GetCompanyEnrollment()
        {
            var CompanyEnroll = spService.GetDataWithoutParameter("Get_CompanyListedToDepositoryCompany").Tables[0].AsEnumerable()
                  .Select(row => new
                  {
                      Id = row.Field<int>("Id"),
                      CompanyId = row.Field<int>("CompanyId"),
                      CompanyDepositoryId = row.Field<int>("CompanyDepositoryId"),
                      SpotStartDate = row.Field<string>("SpotStartDate"),
                      SpotEndDate = row.Field<string>("SpotEndDate"),
                      SuspendStartDate = row.Field<string>("SuspendStartDate"),
                      SuspendEndDate = row.Field<string>("SuspendEndDate"),
                      DematStartDate = row.Field<string>("DematStartDate"),
                      TradingStartDate = row.Field<string>("TradingStartDate"),
                      CompanyName = row.Field<string>("CompanyName"),
                      CompanyDepositoryName = row.Field<string>("DepositoryCompanyName")
                  }).ToList();

            return Json(CompanyEnroll, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyName()
        {
            var Company_NameList = companyInfoService.GetAll();

            var Company_Name = Company_NameList.OrderByDescending(o => o.Id).Select(s => new CompanyInfoViewModel()
            {
                Id = s.Id,
                CompanyName = s.CompanyName,
            });

            return Json(Company_Name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyDepository()
        {
            var Company_DepositoryList = companyDepositoryService.GetAll();

            var Company_DepositoryName = Company_DepositoryList.OrderByDescending(o => o.Id).Select(s => new CompanyDepositoryViewModel()
            {
                Id = s.Id,
                DepositoryCompanyName = s.DepositoryCompanyName,
            });

            return Json(Company_DepositoryName, JsonRequestBehavior.AllowGet);
        }



        public JsonResult CompanyEnrollDelete(string Id)
        {
            var Des = companyListedToDepositoryCompanyService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            companyListedToDepositoryCompanyService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveCompanyEnrollment(string hdnCompanyEnrollmentId, string TradingStartDate, string DematStartDate, string SuspendEndDate, string SuspendStartDate, string SpotStartDate, string SpotEndDate, string CompanyId, string CompanyDepositoryId)
        {
            var result = 0;
            try
            {

                if (hdnCompanyEnrollmentId == "0") //Save
                {
                    var Desig = new CompanyListedToDepositoryCompany()
                    {
                        CompanyId = Convert.ToInt32(CompanyId),
                        CompanyDepositoryId = Convert.ToInt32(CompanyDepositoryId),
                        SpotStartDate = ReportHelper.FormatDate(SpotStartDate), //Convert.ToDateTime(SpotStartDate),
                        SpotEndDate = ReportHelper.FormatDate(SpotEndDate),
                        SuspendStartDate = ReportHelper.FormatDate(SuspendStartDate),
                        SuspendEndDate = ReportHelper.FormatDate(SuspendEndDate),
                        DematStartDate = ReportHelper.FormatDate(DematStartDate),
                        TradingStartDate = ReportHelper.FormatDate(TradingStartDate),
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    companyListedToDepositoryCompanyService.Create(Desig);
                    result = 1;
                }
                else   // Edit
                {
                    var Des = companyListedToDepositoryCompanyService.GetById(Convert.ToInt32(hdnCompanyEnrollmentId));

                    Des.CompanyId = Convert.ToInt32(CompanyId);
                    Des.CompanyDepositoryId = Convert.ToInt32(CompanyDepositoryId);
                    Des.SpotStartDate = ReportHelper.FormatDate(SpotStartDate);
                    Des.SpotEndDate = ReportHelper.FormatDate(SpotEndDate);
                    Des.SuspendStartDate = ReportHelper.FormatDate(SuspendStartDate);
                    Des.SuspendEndDate = ReportHelper.FormatDate(SuspendEndDate);
                    Des.DematStartDate = ReportHelper.FormatDate(DematStartDate);
                    Des.TradingStartDate = ReportHelper.FormatDate(TradingStartDate);
                    Des.UpdateUserId = SessionHelper.LoggedInUserId;
                    Des.UpdateDate = DateTime.Now;
                    companyListedToDepositoryCompanyService.Update(Des);
                    result = 1;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Events

        public ActionResult Index()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["CompanyList"] = items;
            ViewData["CompanyDepositoryList"] = items;
            return View();
        }

        #endregion

       
    }
}
