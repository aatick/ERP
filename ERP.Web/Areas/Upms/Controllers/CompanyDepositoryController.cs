//using ERP.Web.Models;

using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class CompanyDepositoryController :BaseController
    {

        #region Variables

        private readonly ICompanyDepositoryService companyDepositoryService;
        public CompanyDepositoryController(ICompanyDepositoryService companyDepositoryService)
        {
            this.companyDepositoryService = companyDepositoryService;
        }

        #endregion

        #region Methods

        public JsonResult DeleteCompanyDepository(string Id)
        {
            var depo = companyDepositoryService.GetById(Convert.ToInt32(Id));
            depo.IsActive = false;
            depo.UpdateDate = DateTime.Now;
            depo.UpdateUserId = SessionHelper.LoggedInUserId;
            companyDepositoryService.Update(depo);
            var result = 1;
            return Json(result,JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetCompanyDepositoryInfo()
        {
            //var depository = companyDepositoryService.GetAll().OrderByDescending(s=>s.DepositoryCompanyName);
            var depository = companyDepositoryService.GetAll().OrderBy(s => s.DepositoryCompanyName);
            return Json(depository,JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Events

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyDepositoryViewModel model)
        {
            var entity = Mapper.Map<CompanyDepositoryViewModel, CompanyDepository>(model);
            try
            {
                entity.IsActive = true;
                entity.CreateDate = DateTime.Now;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                companyDepositoryService.Create(entity);

               // return View();

                 return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
                //return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int Id)
        {
            var depo = companyDepositoryService.GetById(Convert.ToInt32(Id));
            var entity = Mapper.Map<CompanyDepository, CompanyDepositoryViewModel>(depo);
            return View(entity);
        }

        //
        // POST: /CompanyDepository/Edit/5
        [HttpPost]
        public ActionResult Edit(CompanyDepositoryViewModel model)
        {
            var entity = Mapper.Map<CompanyDepositoryViewModel, CompanyDepository>(model);
            try
            {
                var comdep = companyDepositoryService.GetById(Convert.ToInt32(entity.Id));

                comdep.DepositoryCompanyName = entity.DepositoryCompanyName;
                comdep.CompanyDepositoryShortName = entity.CompanyDepositoryShortName;
                comdep.Email = entity.Email;
                comdep.Address = entity.Address;
                comdep.Fax = entity.Fax;
                comdep.ContactPerson = entity.ContactPerson;
                comdep.ContactEmail = entity.ContactEmail;
                comdep.ContactPhone = entity.ContactPhone;
                comdep.IsActive = true;
                comdep.CreatedUserId = SessionHelper.LoggedInUserId;
                comdep.CreateDate = DateTime.Now;

                companyDepositoryService.Update(comdep);

                return RedirectToAction("Index");
              //  return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }       
        }
       
        #endregion
    }
}
