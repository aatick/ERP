//using ERP.Web.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class CompanyInfoController : BaseController
    {

        #region Variables

        private readonly ICompanyInfoService companyInfoService;
        private readonly IMarketGroupService marketGroupService;
        private readonly IMarketSectorService marketSectorService;
        private readonly IMarketTypeService marketTypeService;
        private readonly ICompanyDepositoryService companyDepositoryService;
        private readonly ILookupCountryService lookupCountryService;
        private readonly ICompanyGroupChangeHistoryService companyGroupChangeHistoryService;
        private readonly ISPService spService;
        private readonly IMarketInstrumentTypeService marketInstrumentTypeService;
        private readonly ICompanyLoanStatusHistoryService companyLoanStatusHistoryService;

        public CompanyInfoController(
                        ICompanyInfoService companyInfoService
                        , IMarketGroupService marketGroupService
                        , IMarketSectorService marketSectorService
                        , IMarketTypeService marketTypeService
                        , ICompanyDepositoryService companyDepositoryService
                        , ILookupCountryService lookupCountryService
                        , ICompanyGroupChangeHistoryService companyGroupChangeHistoryService
                        , ISPService spService
                        , IMarketInstrumentTypeService marketInstrumentTypeService
                        , ICompanyLoanStatusHistoryService companyLoanStatusHistoryService
                    )
        {
            this.companyInfoService = companyInfoService;
            this.marketGroupService = marketGroupService;
            this.marketSectorService = marketSectorService;
            this.marketTypeService = marketTypeService;
            this.companyDepositoryService = companyDepositoryService;
            this.lookupCountryService = lookupCountryService;
            this.companyGroupChangeHistoryService = companyGroupChangeHistoryService;
            this.spService = spService;
            this.marketInstrumentTypeService = marketInstrumentTypeService;
            this.companyLoanStatusHistoryService = companyLoanStatusHistoryService;
        }

        #endregion

        #region Methods

        public JsonResult EditMargin(int Id, string IsMarginLoanMsg)
        {


            var Comp = companyInfoService.GetById(Id);

            if (IsMarginLoanMsg == "Yes")
            {
                Comp.IsMarginLoan = false;
            }
            else
            {
                Comp.IsMarginLoan = true;
            }
            Comp.UpdateDate = DateTime.Now;
            Comp.UpdateUserId = SessionHelper.LoggedInUserId;
            companyInfoService.Update(Comp);

            //LoanMargin

            var MarginEx = companyLoanStatusHistoryService.GetAll().Where(x => x.CompanyId == Id).LastOrDefault();

            if (MarginEx != null)
            {
                MarginEx.ValidTo = DateTime.Now;
                companyLoanStatusHistoryService.Update(MarginEx);
            }

            var loanMar = new CompanyLoanStatusChangeHistory();


            loanMar.CompanyId = Id;
            if (IsMarginLoanMsg == "Yes")
            {
                loanMar.IsMargin = false;
            }
            else
            {
                loanMar.IsMargin = true;
            }
            loanMar.ValidFrom = DateTime.Now;

            loanMar.CreatedUserId = SessionHelper.LoggedInUserId;
            loanMar.CreateDate = DateTime.Now;

            companyLoanStatusHistoryService.Create(loanMar);


            var Result = "Ok";
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyInfo()
        {
            try
            {
                List<CompanyInfoViewModel> Companyinfo = new List<CompanyInfoViewModel>();

                var Master = spService.GetDataWithoutParameter("SP_GetCompanyInfo");
                Companyinfo = Master.Tables[0].AsEnumerable()
                .Select(row => new CompanyInfoViewModel
                {
                    Id = row.Field<int>("Id"),
                    RowSl = row.Field<long>("RowSl"),
                    Address = row.Field<string>("Address"),
                    AuthorizeCapital = row.Field<decimal?>("AuthorizeCapital"),
                    CompanyDepositoryId = row.Field<int?>("CompanyDepositoryId"),
                    CompanyDepositoryShortName = row.Field<string>("CompanyDepositoryShortName"),
                    DepositoryCompanyName = row.Field<string>("DepositoryCompanyName"),
                    CompanyName = row.Field<string>("CompanyName"),
                    CompanyShortName = row.Field<string>("CompanyShortName"),
                    CountryId = row.Field<int?>("CountryId"),
                    CountryName = row.Field<string>("CountryName"),
                    Email = row.Field<string>("Email"),
                    WebAddress = row.Field<string>("WebAddress"),
                    EPS = row.Field<decimal?>("EPS"),
                    FaceValue = row.Field<decimal?>("FaceValue"),
                    Fax = row.Field<string>("Fax"),
                    GroupId = row.Field<int?>("GroupId"),
                    GroupName = row.Field<string>("GroupName"),
                    InstrumentTypeName = row.Field<string>("InstrumentTypeName"),
                    InstrumentTypeId = row.Field<int?>("InstrumentTypeId"),
                    IsForeignMsg = row.Field<string>("IsForeignMsg"),
                    ISINNo = row.Field<string>("ISINNo"),
                    IsMarginLoanMsg = row.Field<string>("IsMarginLoanMsg"),
                    MarketLot = row.Field<int?>("MarketLot"),
                    MarketPrice = row.Field<decimal?>("MarketPrice"),
                    MarketPriceDateMsg = row.Field<string>("MarketPriceDateMsg"),
                    NAV = row.Field<decimal?>("NAV"),
                    PaidUpCapital = row.Field<decimal?>("PaidUpCapital"),
                    Phone = row.Field<string>("Phone"),
                    Premium = row.Field<decimal?>("Premium"),
                    ScripCode = row.Field<string>("ScripCode"),
                    SectorId = row.Field<int?>("SectorId"),
                    SectorName = row.Field<string>("SectorName"),
                    StatusMsg = row.Field<string>("StatusMsg"),
                    TradeIdCSE = row.Field<string>("TradeIdCSE"),
                    TradeIdDSE = row.Field<string>("TradeIdDSE")

                }).ToList();
                return Json(Companyinfo, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCompanyInfoEx(int MarginStatus)
        {
            try
            {
                List<CompanyInfoViewModel> Companyinfo = new List<CompanyInfoViewModel>();
                var param = new { MarginStatus = MarginStatus };
                var Master = spService.GetDataWithParameter(param, "SP_GetCompanyInfoEx");
                Companyinfo = Master.Tables[0].AsEnumerable()
                .Select(row => new CompanyInfoViewModel
                {
                    Id = row.Field<int>("Id"),
                    RowSl = row.Field<long>("RowSl"),
                    Address = row.Field<string>("Address"),
                    AuthorizeCapital = row.Field<decimal?>("AuthorizeCapital"),
                    CompanyDepositoryId = row.Field<int?>("CompanyDepositoryId"),
                    CompanyDepositoryShortName = row.Field<string>("CompanyDepositoryShortName"),
                    DepositoryCompanyName = row.Field<string>("DepositoryCompanyName"),
                    CompanyName = row.Field<string>("CompanyName"),
                    CompanyShortName = row.Field<string>("CompanyShortName"),
                    CountryId = row.Field<int?>("CountryId"),
                    CountryName = row.Field<string>("CountryName"),
                    Email = row.Field<string>("Email"),
                    WebAddress = row.Field<string>("WebAddress"),
                    EPS = row.Field<decimal?>("EPS"),
                    FaceValue = row.Field<decimal?>("FaceValue"),
                    Fax = row.Field<string>("Fax"),
                    GroupId = row.Field<int?>("GroupId"),
                    GroupName = row.Field<string>("GroupName"),
                    InstrumentTypeName = row.Field<string>("InstrumentTypeName"),
                    InstrumentTypeId = row.Field<int?>("InstrumentTypeId"),
                    IsForeignMsg = row.Field<string>("IsForeignMsg"),
                    ISINNo = row.Field<string>("ISINNo"),
                    IsMarginLoanMsg = row.Field<string>("IsMarginLoanMsg"),
                    MarketLot = row.Field<int?>("MarketLot"),
                    MarketPrice = row.Field<decimal?>("MarketPrice"),
                    MarketPriceDateMsg = row.Field<string>("MarketPriceDateMsg"),
                    NAV = row.Field<decimal?>("NAV"),
                    PaidUpCapital = row.Field<decimal?>("PaidUpCapital"),
                    Phone = row.Field<string>("Phone"),
                    Premium = row.Field<decimal?>("Premium"),
                    ScripCode = row.Field<string>("ScripCode"),
                    SectorId = row.Field<int?>("SectorId"),
                    SectorName = row.Field<string>("SectorName"),
                    StatusMsg = row.Field<string>("StatusMsg"),
                    TradeIdCSE = row.Field<string>("TradeIdCSE"),
                    TradeIdDSE = row.Field<string>("TradeIdDSE")

                }).ToList();
                return Json(Companyinfo, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        private void MapDropDownList(CompanyInfoViewModel model)
        {
            ////CountryList MarketSectorList  MarketTypeList MarketGroupList CompanyDepositoryList
            //CountryList
            var CountryList = lookupCountryService.GetAll();
            var Country_List = CountryList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.CountryName), Value = m.Id.ToString() });

            var Country = new List<SelectListItem>();
            Country.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Country.AddRange(Country_List);
            model.CountryList = Country;


            //// //MarketSectorList
            var MarketSectorList = marketSectorService.GetAll();
            var MarketSector_List = MarketSectorList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.SectorName), Value = m.Id.ToString() });
            // model.MarketSectorList = MarketSector_List;

            var MarketSector = new List<SelectListItem>();
            MarketSector.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            MarketSector.AddRange(MarketSector_List);
            model.MarketSectorList = MarketSector;

            //MarketInstrumentTypeList
            var MarketTypeList = marketInstrumentTypeService.GetAll();
            var MarketType_List = MarketTypeList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.InstrumentTypeName), Value = m.Id.ToString() });
            // model.MarketSectorList = MarketSector_List;

            var MarketType = new List<SelectListItem>();
            MarketType.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            MarketType.AddRange(MarketType_List);
            model.MarketInstrumentTypeList = MarketType;

            //MarketGroupList
            var MarketGroupList = marketGroupService.GetAll();
            var MarketGroup_List = MarketGroupList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.GroupName), Value = m.Id.ToString() });
            // model.MarketSectorList = MarketSector_List;

            var MarketGroup = new List<SelectListItem>();
            MarketGroup.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            MarketGroup.AddRange(MarketGroup_List);
            model.MarketGroupList = MarketGroup;

            //CompanyDepositoryList
            var CompanyDepositoryList = companyDepositoryService.GetAll();
            var CompanyDepository_List = CompanyDepositoryList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.CompanyDepositoryShortName), Value = m.Id.ToString() });
            // model.MarketSectorList = MarketSector_List;

            var CompanyDepository = new List<SelectListItem>();
            CompanyDepository.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            CompanyDepository.AddRange(CompanyDepository_List);
            model.CompanyDepositoryList = CompanyDepository;

        }



        #endregion

        #region Events

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexEx()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            var model = new CompanyInfoViewModel();
            MapDropDownList(model);
            return View(model);
        }
        [HttpPost]
        public JsonResult Create(CompanyInfoViewModel model)
        {
            var company = companyInfoService.GetAll().FirstOrDefault(x => x.CompanyName.ToUpper() == model.CompanyName.ToUpper() || x.TradeIdDSE.ToUpper() == model.TradeIdDSE.ToUpper());
            if (company != null)
            {
                if (company.CompanyName.ToUpper() == model.CompanyName.ToUpper())
                    return Json(new { Result = "ERROR", Message = "Duplicate Company Name." }, JsonRequestBehavior.AllowGet);
                if (company.TradeIdDSE.ToUpper() == model.TradeIdDSE.ToUpper())
                    return Json(new { Result = "ERROR", Message = "Duplicate  DSE TradeId." }, JsonRequestBehavior.AllowGet);
            }

            var entity = Mapper.Map<CompanyInfoViewModel, CompanyInformation>(model);
            try
            {
                entity.IsActive = true;
                entity.CreateDate = DateTime.Now;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                var Company_Info = companyInfoService.Create(entity);

                var GroupChange = new CompanyGroupChangeHistory()
                {
                    CompanyId = Company_Info.Id,
                    PresentGroupId = Company_Info.GroupId,
                    ValidFrom = DateTime.Today,
                    CreateDate = DateTime.Now,
                    CreatedUserId = SessionHelper.LoggedInUserId

                };

                companyGroupChangeHistoryService.Create(GroupChange);


                var loanMar = new CompanyLoanStatusChangeHistory();

                loanMar.CompanyId = Company_Info.Id;
                loanMar.IsMargin = model.IsMarginLoan;
                loanMar.ValidFrom = DateTime.Now;
                loanMar.CreatedUserId = SessionHelper.LoggedInUserId;
                loanMar.CreateDate = DateTime.Now;

                companyLoanStatusHistoryService.Create(loanMar);

                return Json(new { Result = "SUCCESS", data = entity, Message = "New company created successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", data = entity, Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Edit(int Id)
        {
            var comInfo = companyInfoService.GetById(Convert.ToInt32(Id));
            var entity = Mapper.Map<CompanyInformation, CompanyInfoViewModel>(comInfo);
            MapDropDownList(entity);
            return View(entity);
        }

        [HttpPost]
        public JsonResult Edit(CompanyInfoViewModel model)
        {
            var company = companyInfoService.GetAll().FirstOrDefault(x => (x.CompanyName.ToUpper() == model.CompanyName.ToUpper() || x.TradeIdDSE.ToUpper() == model.TradeIdDSE.ToUpper()) && x.Id != model.Id);
            if (company != null)
            {
                if (company.CompanyName.ToUpper() == model.CompanyName.ToUpper())
                    return Json(new { Result = "ERROR", Message = "Duplicate Company Name." }, JsonRequestBehavior.AllowGet);
                if (company.TradeIdDSE.ToUpper() == model.TradeIdDSE.ToUpper())
                    return Json(new { Result = "ERROR", Message = "Duplicate  DSE TradeId." }, JsonRequestBehavior.AllowGet);
            }
            var entity = Mapper.Map<CompanyInfoViewModel, CompanyInformation>(model);
            try
            {

                var Company = companyInfoService.GetById(Convert.ToInt32(entity.Id));

                if (model.GroupId != Company.GroupId)
                {
                    var GroupChange = new CompanyGroupChangeHistory()
                    {
                        CompanyId = model.Id,
                        PresentGroupId = model.GroupId,
                        PreviousGroupId = Company.GroupId,
                        ValidFrom = DateTime.Now,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    companyGroupChangeHistoryService.Create(GroupChange);
                }


                Company.CompanyName = entity.CompanyName;
                Company.CountryId = entity.CountryId;
                Company.Address = entity.Address;
                Company.AuthorizeCapital = entity.AuthorizeCapital;
                Company.CompanyDepositoryId = entity.CompanyDepositoryId;
                Company.Email = entity.Email;
                Company.EPS = entity.EPS;
                Company.FaceValue = entity.FaceValue;
                Company.Fax = entity.Fax;
                Company.GroupId = entity.GroupId;
                Company.ISINNo = entity.ISINNo;
                Company.MarketLot = entity.MarketLot;
                Company.MarketPrice = entity.MarketPrice;
                Company.MarketPriceDate = entity.MarketPriceDate;
                Company.CompanyShortName = entity.CompanyShortName;
                Company.NAV = entity.NAV;
                Company.PaidUpCapital = entity.PaidUpCapital;
                Company.Phone = entity.Phone;
                Company.SectorId = entity.SectorId;
                Company.Status = entity.Status;
                Company.TradeIdCSE = entity.TradeIdCSE;
                Company.TradeIdDSE = entity.TradeIdDSE;
                Company.UpdateDate = DateTime.Now;
                Company.UpdateUserId = SessionHelper.LoggedInUserId;

                companyInfoService.Update(Company);

                return Json(new { Result = "SUCCESS", data = entity, Message = "Company updated successfully." }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", data = entity, Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
