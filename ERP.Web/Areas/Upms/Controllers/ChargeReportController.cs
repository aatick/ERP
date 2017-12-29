using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;

namespace Upms.Controllers
{
    public class ChargeReportController : BaseController
    {
        private readonly ISPService spService;
        public ChargeReportController(ISPService spService)
        {
            this.spService = spService;
        }
        public ActionResult Index()
        {
            var moduleid = spService.GetSecurityModuleByControllerAction("ChargeReport", "Index");

            ViewBag.Reports = spService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                ReportModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            {
                Id = x.Field<int>(0),
                ReportName = x.Field<string>(1),
                SerialNo = x.Field<int>(2)
            }).ToList();
            return View();
        }

        public void ShowChargeInfoReport(string type)
        {
            var data =spService.GetDataWithoutParameter("SP_RPT_CHARGE_INFORMATION").Tables[0];
            ReportHelper.ShowReport(data, type, "rptChargeInfo.rpt", "ChargeInfo" + DateTime.Today.ToString("dd_MMM_yyyy"));
        }
    }
}