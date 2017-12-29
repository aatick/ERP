using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.ViewModels;
using AutoMapper;
using ERP.Web.Helpers;
using Common.Data.CommonDataModel;
using System.Data;

namespace ERP.Web.Controllers
{
    public class OfficeController : BaseController
    {
        private readonly IBrokerInfoService brokerInfoService;
        private readonly ILookupDivisionService lookupDivisionService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly ILookupDistrictService lookupDistrictService;
        private readonly ILookupThanaService lookupThanaService;//sPService
        private readonly ISPService sPService;
        public OfficeController(IBrokerInfoService brokerInfoService
            , ILookupDivisionService lookupDivisionService
            , IBrokerBranchService brokerBranchService
            , ILookupDistrictService lookupDistrictService
            , ILookupThanaService lookupThanaService
            , ISPService sPService
            )
        {
            this.brokerInfoService = brokerInfoService;
            this.lookupDivisionService = lookupDivisionService;
            this.brokerBranchService = brokerBranchService;
            this.lookupDistrictService = lookupDistrictService;
            this.lookupThanaService = lookupThanaService;
            this.sPService = sPService;
        }


        #region Broker branch

        public ActionResult BrokerBranchList()
        {
            ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
            return View();
        }

        public ActionResult BrokerBranch()
        {
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            return View();
        }
        [HttpPost]
        public ActionResult BrokerBranch(BrokerBranchViewModel model)
        {

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult BrokerBranchEdit(int Id)
        {
            var DivisionId = 0;
            var DistrictId = 0;
            var comInfo = brokerBranchService.GetById(Convert.ToInt32(Id));
            if (comInfo.ThanaId != 0 && comInfo.ThanaId != null)
            {
                DivisionId = lookupDistrictService.GetById(lookupThanaService.GetById(Convert.ToInt32(comInfo.ThanaId)).DistrictId).DivisionId;
                DistrictId = lookupThanaService.GetById(Convert.ToInt32(comInfo.ThanaId)).DistrictId;
            }

            var entity = Mapper.Map<BrokerBranch, BrokerBranchViewModel>(comInfo);
            entity.DivisionId = DivisionId;
            entity.DistrictId = DistrictId;
            entity.Id = Id;
            BrokerBranchDDl(entity);

            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;

            return View(entity);
        }

        [HttpPost]
        public ActionResult BrokerBranchEdit(BrokerBranchViewModel model)
        {

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }



        public void BrokerBranchDDl(BrokerBranchViewModel model)
        {
            var BrokeList = brokerInfoService.GetAll();
            var Broke_List = BrokeList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.BrokerName), Value = m.Id.ToString() });

            var Broker = new List<SelectListItem>();
            Broker.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Broker.AddRange(Broke_List);
            model.BrokeList = Broker;
        }

        public JsonResult GetBrokerBranchInfo()
        {
            List<BrokerBranchViewModel> BroBranch = new List<BrokerBranchViewModel>();
            var Master = sPService.GetDataWithoutParameter("SP_GetBrokerBranchInfo");
            BroBranch = Master.Tables[0].AsEnumerable()
            .Select(row => new BrokerBranchViewModel
            {
                RowSL = row.Field<long>("RowSL"),
                Id = row.Field<int>("Id"),
                Address = row.Field<string>("Address"),
                BrokerBranchName = row.Field<string>("BrokerBranchName"),
                BrokerId = row.Field<int>("BrokerId"),
                BrokerName = row.Field<string>("BrokerName"),
                ContactEmail = row.Field<string>("ContactEmail"),
                ContactFax = row.Field<string>("ContactFax"),
                ContactMobile = row.Field<string>("ContactMobile"),
                ContactName = row.Field<string>("ContactName"),
                Email = row.Field<string>("Email"),
                Fax = row.Field<string>("Fax"),
                Phone = row.Field<string>("Phone"),
                ThanaId = row.Field<int?>("ThanaId"),
                ThanaName = row.Field<string>("ThanaName")

            }).ToList();
            return Json(BroBranch, JsonRequestBehavior.AllowGet);


        }


        public JsonResult SaveBrokerBranchInfo(string BrokerBranchId, string BrokerId, string BranchName, string ThanaId, string Address, string Phone, string Email, string Fax, string ContactName, string ContactMobile, string ContactEmail, string ContactFax)
        {
            var result = 0;
            try
            {
                if (BrokerBranchId == "0")
                {
                    var broBranch = new BrokerBranch();

                    broBranch.BrokerId = Convert.ToInt32(BrokerId);
                    broBranch.BrokerBranchName = BranchName;
                    if (ThanaId != "" && ThanaId != null)
                    {
                        broBranch.ThanaId = Convert.ToInt32(ThanaId);
                    }

                    broBranch.Address = Address;
                    broBranch.Phone = Phone;
                    broBranch.Email = Email;
                    broBranch.Fax = Fax;
                    broBranch.ContactName = ContactName;
                    broBranch.ContactMobile = ContactMobile;
                    broBranch.ContactEmail = ContactEmail;
                    broBranch.ContactFax = ContactFax;
                    broBranch.IsActive = true;
                    broBranch.CreateDate = DateTime.Now;
                    broBranch.CreatedUserId = SessionHelper.LoggedInUserId;

                    brokerBranchService.Create(broBranch);
                }
                else
                {
                    var broBranch = brokerBranchService.GetById(Convert.ToInt32(BrokerBranchId));

                    broBranch.BrokerId = Convert.ToInt32(BrokerId);
                    broBranch.BrokerBranchName = BranchName;
                    if (ThanaId != "" && ThanaId != null)
                    {
                        broBranch.ThanaId = Convert.ToInt32(ThanaId);
                    }

                    broBranch.Address = Address;
                    broBranch.Phone = Phone;
                    broBranch.Email = Email;
                    broBranch.Fax = Fax;
                    broBranch.ContactName = ContactName;
                    broBranch.ContactMobile = ContactMobile;
                    broBranch.ContactEmail = ContactEmail;
                    broBranch.ContactFax = ContactFax;
                    broBranch.IsActive = true;
                    broBranch.UpdateDate = DateTime.Now;
                    broBranch.UpdateUserId = SessionHelper.LoggedInUserId;

                    brokerBranchService.Update(broBranch);
                }


                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion


        #region Broker Info

        public ActionResult BrokerList()
        {
            return View();
        }

        public ActionResult BrokerInfo()
        {
            var model = new BrokerInfoViewModel();
           // MapDropDownList(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult BrokerInfo(BrokerInfoViewModel model)
        {
            var entity = Mapper.Map<BrokerInfoViewModel, BrokerInformation>(model);
            try
            {
                //entity.IssueDate = ReportHelper.FormatDateToString(model.IssueDate);
                entity.IsActive = true;
                entity.CreateDate = DateTime.Now;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                var Company_Info = brokerInfoService.Create(entity);


                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BrokerinfoEdit(int Id)
        {
            var comInfo = brokerInfoService.GetById(Convert.ToInt32(Id));
            var entity = Mapper.Map<BrokerInformation, BrokerInfoViewModel>(comInfo);
            //MapDropDownList(entity);
            return View(entity);
        }

        [HttpPost]
        public ActionResult BrokerinfoEdit(BrokerInfoViewModel model)
        {

            var entity = Mapper.Map<BrokerInfoViewModel, BrokerInformation>(model);
            try
            {

                var broker = brokerInfoService.GetById(Convert.ToInt32(entity.Id));

                broker.BrokerCode = entity.BrokerCode;
                broker.AuthCapital = entity.AuthCapital;
                broker.BrokerName = entity.BrokerName;
                broker.DepositMode = entity.DepositMode;
                broker.FreeLimit = entity.FreeLimit;
                broker.IsOwner = entity.IsOwner;
                broker.IssueDate = entity.IssueDate;
                broker.PaidUpCapital = entity.PaidUpCapital;
                broker.RegistrationNo = entity.RegistrationNo;
                broker.BrokerShortName = entity.BrokerShortName;
                broker.TotalTrader = entity.TotalTrader;

                broker.UpdateDate = DateTime.Now;
                broker.UpdateUserId = SessionHelper.LoggedInUserId;

                brokerInfoService.Update(broker);

                // return RedirectToAction("BrokerList");

                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }


        //private void MapDropDownList(BrokerInfoViewModel model)
        //{

        //    // BrokerDepositoryList
        //    var BrokerDepositoryList = brokerDepositoryParticipatoryService.GetAll();
        //    var BrokerDepository_List = BrokerDepositoryList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.DepositoryParticipantName), Value = m.Id.ToString() });

        //    var BroDepository = new List<SelectListItem>();
        //    BroDepository.Add(new SelectListItem() { Text = "Please Select", Value = "" });
        //    BroDepository.AddRange(BrokerDepository_List);
        //    model.BrokerDepositoryList = BroDepository;

        //    //MarketTypeList
        //    var MarketList = marketInfoService.GetAll();
        //    var Market_List = MarketList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.ExchangeName), Value = m.Id.ToString() });

        //    var Market = new List<SelectListItem>();
        //    Market.Add(new SelectListItem() { Text = "Please Select", Value = "" });
        //    Market.AddRange(Market_List);
        //    model.MarketList = Market;
        //}

        public JsonResult GetBrokerInfo()
        {
            var Broker = brokerInfoService.GetAll();
            var BrokerList = Broker.OrderBy(o => o.BrokerName).Select(s => new BrokerInfoViewModel()
            {
                Id = s.Id,
                BrokerCode = s.BrokerCode,
                BrokerShortName = s.BrokerShortName,
                BrokerName = s.BrokerName,
                RegistrationNo = s.RegistrationNo,
                TotalTrader = s.TotalTrader,
                IsOwner = s.IsOwner,
                FreeLimit = s.FreeLimit,
                AuthCapital = s.AuthCapital,
                PaidUpCapital = s.PaidUpCapital,
                IssueDate = s.IssueDate,
                DepositMode = s.DepositMode,

            });

            return Json(BrokerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteBrokerInfo(string Id)
        {
            var Des = brokerInfoService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            brokerInfoService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
