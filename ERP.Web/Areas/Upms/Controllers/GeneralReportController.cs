//using ERP.Web.Reports;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Service;
using Common.Data.CommonDataModel;

namespace Upms.Controllers
{
    public class GeneralReportController : BaseController
    {
        #region Variables

        private readonly ISPService sPService;
        private readonly IUltimateReportService ultimateReportService;
        private readonly IMarketGroupService marketGroupService;
        private readonly IMarketSectorService marketSectorService;
        private readonly IBrokerInfoService brokerInfoService;
        private readonly IBrokerBranchService brokerBranchService;
        public GeneralReportController(ISPService sPService, IUltimateReportService ultimateReportService
            , IMarketGroupService marketGroupService, IMarketSectorService marketSectorService
            , IBrokerInfoService brokerInfoService, IBrokerBranchService brokerBranchService)
        {
            this.sPService = sPService;
            this.ultimateReportService = ultimateReportService;
            this.marketGroupService = marketGroupService;
            this.marketSectorService = marketSectorService;
            this.brokerInfoService = brokerInfoService;
            this.brokerBranchService = brokerBranchService;
        }

        #endregion

        #region Methods
        public JsonResult GetSectorList()
        {
            var getSector = marketSectorService.GetAll().Where(s => s.IsActive == true).OrderBy(e => e.SectorName);
            var viewSector = getSector.Select(x => x).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.SectorName.ToString()
            });
            var sector_items = new List<SelectListItem>();

            sector_items.AddRange(viewSector);
            return Json(sector_items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroupList()
        {
            var getGroup = marketGroupService.GetAll().Where(s => s.IsActive == true).OrderBy(e => e.GroupName);
            var viewGroup = getGroup.Select(x => x).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.GroupName.ToString()
            });
            var group_items = new List<SelectListItem>();

            group_items.AddRange(viewGroup);
            return Json(group_items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenerateCompanyInfoReport(string reportNo,string SectorId, string GroupId, string Qtype, int LoanMargin, int OrderBy, string exportType)
        {
            try
            {

                var param = new { SectorId = Convert.ToInt32(SectorId), GroupId = Convert.ToInt32(GroupId), LoanMargin = LoanMargin, OrderBy = OrderBy };
                var alldata = ultimateReportService.GetReportDataWithParameter(param, "Rpt_CompanyInfoEX");
                if (Qtype == "1")
                {
                    ReportHelper.ShowReport(alldata.Tables[0], exportType, "Rpt_CompanyInfo.rpt", "Rpt_CompanyInfo");
                }
                else if (Qtype == "2")
                {
                    ReportHelper.ShowReport(alldata.Tables[0], exportType, "Rpt_CompanyInfoSectorwise.rpt", "Rpt_CompanyInfoSectorwise");
                }
                else
                {
                    ReportHelper.ShowReport(alldata.Tables[0], exportType, "Rpt_CompanyInfoGroupwise.rpt", "Rpt_CompanyInfoGroupwise");

                }
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        //public ActionResult GenerateCompanyInfoReport(string SectorId,string GroupId,string Qtype)
        //{
        //    try
        //    {
        //        var SectorTo = "0";
        //        var GroupTo = "0";

        //        if (SectorId == "0")
        //        {
        //            SectorId = "0000";
        //            SectorTo = "99999999";
        //        }
        //        else if (SectorId == "")
        //        {
        //            SectorId = "0";
        //            SectorTo = "0";
        //        }
        //        else
        //        {
        //            SectorTo = SectorId;
        //        }

        //        if ( GroupId == "0")
        //        {
        //            GroupId = "0000";
        //            GroupTo = "99999999";
        //        }
        //        else if (GroupId == "")
        //        {
        //            GroupId = "0";
        //            GroupTo = "0";
        //        }
        //        else
        //        {
        //            GroupTo = GroupId;
        //        }

        //        //@SectorId int,
        //        //            @SectorIdTo int,
        //        //            @GroupId int,
        //        //            @GroupIdTo int,
        //        //            @Qtype int
        //        var param = new { SectorId = Convert.ToInt32(SectorId), SectorIdTo = Convert.ToInt32(SectorTo), GroupId = Convert.ToInt32(GroupId), GroupIdTo = Convert.ToInt32(GroupTo), Qtype = Convert.ToInt32(Qtype) };
        //        var alldata = ultimateReportService.GetReportDataWithParameter(param, "Rpt_CompanyInfo");
        //        var reportParam = new Dictionary<string, object>();
        //        reportParam.Add("param_orgName", "UCas");
        //        //ReportHelper.PrintReport("Rpt_CompanyInfo.rpt", alldata.Tables[0], reportParam);
        //        if (Qtype == "1" || Qtype == "2")
        //        {

        //            ReportHelper.PrintReport("Rpt_CompanyInfoSectorwise.rpt", alldata.Tables[0], reportParam);
        //        }
        //        else if (Qtype == "3" || Qtype == "4")
        //        {
        //            ReportHelper.PrintReport("Rpt_CompanyInfoGroupwise.rpt", alldata.Tables[0], reportParam);
        //        }
        //        else
        //        {                                    
        //            ReportHelper.PrintReport("Rpt_CompanyInfo.rpt", alldata.Tables[0], reportParam);
        //        }
        //        return Content(string.Empty);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }
        //}

        public JsonResult GetSearchItem(int searchId)
        {
            var searchItem =
                sPService.GetDataWithParameter(new { SEARCH_BY_ID = searchId }, "SP_GET_INVESTOR_REPORT_SERCH_ITEM")
                    .Tables[0].AsEnumerable()
                    .Select(x => new { Id = x.Field<int>(0), Text = x.Field<string>(1) })
                    .ToList();
            var json = Json(searchItem, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public void GenerateInvestorInfoReport(string reportNo,int groupById, int searchById, int searchItemId, int orderById, string exportType)
        {
            var data =
                sPService.GetDataWithParameter(
                    new { SEARCH_BY_ID = searchById, SEARCH_ITEM_ID = searchItemId, ORDER_BY_ID = orderById },
                    "SP_RPT_GET_INVESTOR_INFO_REPORT").Tables[0];
            var reportParam = new Dictionary<string, object> { { "groupById", groupById } };
            ReportHelper.ShowReport(data, exportType, "rptInvestorGeneralReport.rpt", "rptInvestorGeneralReport", reportParam);
        }

        public void GenerateTraderInfoReport(int BrokerId, string exportType)
        {
            var data =
                sPService.GetDataWithParameter(
                    new { BrokerId = BrokerId },
                    "SP_RPT_BrokerTraderInfo").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "Rpt_Broker_TraderInfo.rpt", "Rpt_Broker_TraderInfo", reportParam);
        }

        public void BankwiseBranchList(int BankId, string exportType)
        {
            var data =
                sPService.GetDataWithParameter(
                    new { BankId = BankId },
                    "SP_RPT_BankwiseBranchList").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_BankwiseBranchList.rpt", "rpt_BankwiseBranchList", reportParam);
        }
        public void BrokerwiseEmployeeList(int BrokerId, string exportType)
        {
            var data =
                sPService.GetDataWithParameter(
                    new { BrokerId = BrokerId },
                    "Rpt_BrokerEmployeeInfo").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_BrokerEmployeeInfo.rpt", "rpt_BrokerEmployeeInfo", reportParam);
        }

        public void CompanyGroupChangingHistory(string reportNo, int CompanyId, string exportType)
        {
            var data =
                sPService.GetDataWithParameter(
                    new { CompanyId = CompanyId },
                    "rpt_CompanyGroupChangingHistory").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            if (CompanyId != 0)
            {
                ReportHelper.ShowReport(data, exportType, "rpt_CompanyGroupChangeHistory.rpt", "rpt_CompanyGroupChangeHistory", reportParam);
            }
            else
            {
                ReportHelper.ShowReport(data, exportType, "rpt_AllCompanyGroupChangeHistory.rpt", "rpt_AllCompanyGroupChangeHistory", reportParam);
            }

        }
        public void CompanyDepository(string rptslnx, string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter(
                    "Rpt_CompanyDepositoryInfo").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_CompanyDepository.rpt", "rpt_CompanyDepository", reportParam);
        }

        public void CompanySharePriceHistory(string reportNo,string CompanyId, string DateFrom, string DateTo, string exportType)
        {
            var data =
                sPService.GetDataWithParameter(
                 new { CompanyId = Convert.ToInt32(CompanyId), DateFrom = Convert.ToDateTime(DateFrom), DateTo = Convert.ToDateTime(DateTo) },
                    "rpt_CompanySharePriceHistory").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            if (CompanyId != "0")
            {
                ReportHelper.ShowReport(data, exportType, "rpt_CompanySharePriceHistory.rpt", "rpt_CompanySharePriceHistory", reportParam);
            }
            else
            {
                ReportHelper.ShowReport(data, exportType, "rpt_AllCompanySharePriceHistory.rpt", "rpt_AllCompanySharePriceHistory", reportParam);
            }

        }

        public void AccChequeCollectionReport(string reportNo,string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter("Proc_Rpt_Acc_CollectionInfo").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };

            ReportHelper.ShowReport(data, exportType, "RptCheckCollectionReport.rpt", "RptCheckCollectionReport", reportParam);


        }
        public void CompanySharePriceDaily(string rptslnx, string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter(
                    "rpt_CompanySharePriceDaily").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_CompanySharePriceDaily.rpt", "rpt_CompanySharePriceDaily", reportParam);
        }////MarketType MarketSector  MarketInstrumentType  MarketGroup

        public void Get_MarketGeneralReport(string rptslnx, string Qtype, string exportType)
        {

            var data =
            sPService.GetDataWithParameter(
                new { Qtype = Qtype },
                "rpt_MarketGroupSectorTypeInstrument").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            if (Qtype == "MarketType")
            {
                ReportHelper.ShowReport(data, exportType, "rpt_MarketTypeList.rpt", "rpt_MarketTypeList", reportParam);
            }//
            else if (Qtype == "MarketSector")
            {
                ReportHelper.ShowReport(data, exportType, "rpt_MarketSectorList.rpt", "rpt_MarketSectorList", reportParam);
            }
            else if (Qtype == "MarketInstrumentType")
            {
                ReportHelper.ShowReport(data, exportType, "rpt_MarketInstrumentType.rpt", "MarketInstrumentType", reportParam);
            }
            else if (Qtype == "MarketGroup")
            {
                ReportHelper.ShowReport(data, exportType, "rpt_MarketGroup.rpt", "rpt_MarketGroup", reportParam);
            }
        }


        public void BrokerDepositoryParticipantInfo(string rptslnx, string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter(
                    "rpt_BrokerDepositoryParticipantInfo").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_BrokerDepository.rpt", "rpt_BrokerDepository", reportParam);
        }
        public void DPBranchInfo(string rptslnx, string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter(
                    "Rpt_BrokerDepositoryBranch").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_BrokerDepositoryBranch.rpt", "rpt_BrokerDepositoryBranch", reportParam);
        }
      
        public void BrokerInformationReport(string rptslnx,string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter(
                    "rpt_BrokerInfoList").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_BrokerInfo.rpt", "rpt_BrokerInfo", reportParam);
        }

        public void Get_InvestorInformation(string InvestorCode, string exportType, string bo = "")
        {
            var data =
            sPService.GetDataWithParameter(
                new { InvestorCode = InvestorCode, BO = bo },
                "Rpt_InvestorInformationDetailAndAccount").Tables[0];
            ReportHelper.ShowReport(data, exportType, "Rpt_InvestorInformationDetailAndAccount.rpt", "Rpt_InvestorInformationDetailAndAccount");
        }
        

        #endregion

        #region Events

        //
        // GET: /GeneralReport/  
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BrokerList"] = items;
            ViewData["CompanyList"] = items;
            var moduleid = sPService.GetSecurityModuleByControllerAction("GeneralReport", "Index");
            ViewBag.Reports = sPService.GetDataWithParameter(new
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
        public ActionResult Indexrpt()
        {
            var moduleid = sPService.GetSecurityModuleByControllerAction("GeneralReport", "Indexrpt");
            ViewBag.Reports = sPService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                ReportModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            {
                Id = x.Field<int>(0),
                ReportName = x.Field<string>(1),
                SerialNo = x.Field<int>(2)
            }).ToList();


            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BrokerList"] = items;
            ViewData["CompanyList"] = items;
            return View();
        }
        public ActionResult Indexsgh()
        {
            
            var moduleid = sPService.GetSecurityModuleByControllerAction("GeneralReport", "Indexsgh");
            ViewBag.Reports = sPService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                ReportModuleId = moduleid
            }, "SP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            {
                Id = x.Field<int>(0),
                ReportName = x.Field<string>(1),
                SerialNo = x.Field<int>(2)
            }).ToList();

            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankList"] = items;
            ViewData["BrokerList"] = items;
            ViewData["CompanyList"] = items;
            return View();
        }
        public ActionResult TraderInfo()
        {
            ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
            return View();
        }
        public ActionResult CompanyInfoReport()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["SectorList"] = items;
            ViewData["GroupList"] = items;
            return View();
        }
        public ActionResult InvestorInfoReport()
        {

            return View();
        }
        public ActionResult InvestorBankBranchReport()
        {
            ViewBag.Branch = brokerBranchService.GetAll().ToList();
            return View();
        }
        public ActionResult InvestorDetailsReport()
        {
            return View();
        }
        //
        // GET: /GeneralReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /GeneralReport/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GeneralReport/Create
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

        //
        // GET: /GeneralReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /GeneralReport/Edit/5
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

        //
        // GET: /GeneralReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /GeneralReport/Delete/5
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
