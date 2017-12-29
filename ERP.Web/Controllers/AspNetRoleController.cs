using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common.Data.CommonDataModel;
using Common.Service;
using DotNetOpenAuth.AspNet;
using ERP.Web.Controllers;
using ERP.Web.ViewModels;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using AutoMapper;

namespace ERP.Web.Controllers
{
    public class AspNetRoleController : BaseController
    {
        #region Variables

        private readonly IAspNetRoleService aspNetRoleService;

        public  AspNetRoleController(IAspNetRoleService aspNetRoleService)
        {
            this.aspNetRoleService = aspNetRoleService;
        }

        #endregion

        #region Methods

        public JsonResult GetRoleInformation()
        {
            var roleInfo = aspNetRoleService.GetAll();

           List<AspNetRoleViewModel> detail = new List<AspNetRoleViewModel>();
            foreach(var r in roleInfo)
            {
                var nam = r.Name;
                var roleId = r.Id;
                string str = r.DefaultLinkURL;

                string last = str.Substring(str.LastIndexOf('/') + 1);

                var GetRole = new AspNetRoleViewModel() { Id = roleId, Name = nam, DefaultLinkURL = last };
                detail.Add(GetRole);
            }
            var currentURLRecords = detail.ToList();

            return Json(currentURLRecords, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoleDelete(string  RoleId)
        {
            var entity = aspNetRoleService.GetById(Convert.ToInt32(RoleId));
            string Result = "OK";
            if (ModelState.IsValid)
            {
      
                //aspNetRoleService.Delete(entity.RoleId);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);
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

                var model = new AspNetRoleViewModel();
                return View(model);
        }

        [HttpPost]
        public ActionResult Create(AspNetRoleViewModel model)
        {            
                var entity = Mapper.Map<AspNetRoleViewModel, AspNetRole>(model);

                try
                {
                    var url = "~/" + entity.DefaultLinkURL;
                    string MaxRoleId = aspNetRoleService.GetMaxRoleId();
                    var RoleRe = new AspNetRole() { Name = entity.Name, DefaultLinkURL = url, Id = (Convert.ToInt32(MaxRoleId) + 1).ToString() };
                    aspNetRoleService.Create(RoleRe);

                    return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
                }                     
            
            }

        public ActionResult Edit(string id)
        {
                var role = aspNetRoleService.GetByRoleId(id);
                var entity = Mapper.Map<AspNetRole, AspNetRoleViewModel>(role);


                string str = entity.DefaultLinkURL;

                string last = str.Substring(str.LastIndexOf('/') + 1);

                var RoleRe = new AspNetRoleViewModel() { Id = id, Name = entity.Name, DefaultLinkURL = Convert.ToString(last) };
                return View(RoleRe);

            
        }


        [HttpPost]
        public ActionResult Edit(AspNetRoleViewModel model)
        {
           
                var entity = Mapper.Map<AspNetRoleViewModel, AspNetRole>(model);
                try
                {
                    var getRoleDetails = aspNetRoleService.GetByRoleId(entity.Id);

                    var url = "~/" + entity.DefaultLinkURL;

                    getRoleDetails.Name = entity.Name;
                    getRoleDetails.DefaultLinkURL = url;

                    aspNetRoleService.Update(getRoleDetails);

                    return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
                }
            
           
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

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
