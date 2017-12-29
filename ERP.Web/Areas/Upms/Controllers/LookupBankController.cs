//using ERP.Web.Filters;
//using ERP.Web.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class LookupBankController : BaseController
    {
        #region Variables

        private readonly ILookupBankService lookupBankService;
        private readonly ILookupDivisionService lookupDivisionService;
        private readonly ILookupDistrictService lookupDistrictService;
        private readonly ILookupBankBranchService lookupBankBranchService;
        private readonly ILookupThanaService lookupThanaService;
        private readonly ISPService spService;

        public LookupBankController(ILookupBankService lookupBankService, ILookupDivisionService lookupDivisionService, 
            ILookupDistrictService lookupDistrictService, ILookupBankBranchService lookupBankBranchService,
            ILookupThanaService lookupThanaService, ISPService spService)
        {
            this.lookupBankService = lookupBankService;
            this.lookupDivisionService = lookupDivisionService;
            this.lookupDistrictService = lookupDistrictService;
            this.lookupBankBranchService = lookupBankBranchService;
            this.lookupThanaService = lookupThanaService;
            this.spService = spService;
        }

        #endregion

        #region Mathods

            #region Bank
            public JsonResult GetBankInfo()
            {
                var BankInfo = lookupBankService.GetAll().OrderBy(x=>x.BankName);
                return Json(BankInfo, JsonRequestBehavior.AllowGet);
            }

            public JsonResult BankDelete(string Id)
            {
                var bank = lookupBankService.GetById(Convert.ToInt32(Id));


                bank.IsActive = false;
                bank.UpdateDate = DateTime.Now;
                bank.UpdateUserId = SessionHelper.LoggedInUserId;
                lookupBankService.Update(bank);
                var result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            public JsonResult SaveBankInfo(string BankName, string ShortName, string BankId)
            {
                var result = 0;
                try
                {

                    if (BankId == "0") //Save
                    {
                        var Desig = new LookupBank()
                        {
                            BankName = BankName,
                            BankShortName = ShortName,
                            IsActive = true,
                            CreateDate = DateTime.Now,
                            CreatedUserId = SessionHelper.LoggedInUserId
                        };
                        lookupBankService.Create(Desig);
                        result = 1;
                    }
                    else   // Edit
                    {
                        var Des = lookupBankService.GetById(Convert.ToInt32(BankId));

                        Des.BankName = BankName;
                        Des.BankShortName = ShortName;
                        Des.UpdateDate = DateTime.Now;
                        Des.UpdateUserId = SessionHelper.LoggedInUserId;
                        lookupBankService.Update(Des);
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



            #region Bank Branch


            public JsonResult GetBankwiseBranchList(string BankId)
            {
                try
                {
                    List<lookupBankBranchViewModel> lookupBankBranchModel = new List<lookupBankBranchViewModel>();
                    var param = new { BankId = Convert.ToInt32(BankId) };
                    var Master = spService.GetDataWithParameter(param, "SP_GetBankBranchInfo");
                    lookupBankBranchModel = Master.Tables[0].AsEnumerable()
                    .Select(row => new lookupBankBranchViewModel
                    {
                         RowSl = row.Field<long>("RowSl"),
                         Id = row.Field<int>("Id"),
                         ThanaId = row.Field<int?>("ThanaId"),
                         ThanaName = row.Field<string>("ThanaName"),
                         BankId = row.Field<int>("BankId"),
                         DistrictId = row.Field<int?>("DistrictId"),
                         DivitionId = row.Field<int?>("DivisionId"),
                         BankName = row.Field<string>("BankName"),
                         BranchName = row.Field<string>("BranchName"),
                         Address = row.Field<string>("Address"),
                         RoutingNo = row.Field<string>("RoutingNo")
                        

                    }).ToList();
                    return Json(lookupBankBranchModel, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }

            //public JsonResult GetBankwiseBranchList(string BankId)
            //{
            //    var Branch = lookupBankBranchService.GetAll().Where(b => b.BankId == Convert.ToInt32(BankId));

            //    var BranchList = Branch.OrderBy(o => o.BranchName).Select(s => new lookupBankBranchViewModel()
            //    {
            //        Id = s.Id,
            //        ThanaId = s.ThanaId,
            //       // ThanaName = s.LookupThana.ThanaName,
            //        BankId = s.BankId,
            //       // DistrictId = s.LookupThana.DistrictId,
            //       // DivitionId = lookupDistrictService.GetById(s.LookupThana.DistrictId).DivisionId,
            //        BankName = lookupBankService.GetById(s.BankId).BankName,
            //        BranchName = s.BranchName,
            //        Address = s.Address,
            //        RoutingNo = s.RoutingNo
            //    });

            //    return Json(BranchList, JsonRequestBehavior.AllowGet);
            //}

            public JsonResult BankBranchDelete(string Id)
            {
                var bran = lookupBankBranchService.GetById(Convert.ToInt32(Id));

                bran.IsActive = false;
                bran.UpdateDate = DateTime.Now;
                bran.UpdateUserId = SessionHelper.LoggedInUserId;
                lookupBankBranchService.Update(bran);
                var result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            public JsonResult SaveBankBranchInfo(string BankId, string BranchName, string ThanaId, string Address, string RoutingNo, string BankBranchId)
            {

                if (BankBranchId == "0")
                {
                    var bBranch = new LookupBankBranch()
                    {
                        BankId = Convert.ToInt32(BankId),
                        BranchName = BranchName,
                        ThanaId = Convert.ToInt32(ThanaId),
                        Address = Address,
                        RoutingNo = RoutingNo,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    lookupBankBranchService.Create(bBranch);
                }
                else
                {
                    var bran = lookupBankBranchService.GetById(Convert.ToInt32(BankBranchId));
                    bran.BankId = Convert.ToInt32(BankId);
                    bran.BranchName = BranchName;
                    bran.ThanaId = Convert.ToInt32(ThanaId);
                    bran.Address = Address;
                    bran.RoutingNo = RoutingNo;
                    bran.UpdateDate = DateTime.Now;
                    bran.UpdateUserId = SessionHelper.LoggedInUserId;
                    lookupBankBranchService.Update(bran);

                }
                var result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            #endregion

        #endregion

        #region Events

            public ActionResult Index()
            {
                return View();
            }
            public ActionResult branch()
            {
                ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
                ViewBag.BankList = lookupBankService.GetAll().ToList();
                IEnumerable<SelectListItem> items = new SelectList(" ");
                ViewData["Districtlist"] = items;
                ViewData["Thanalist"] = items;
                return View();
            }

        #endregion
    }
}
