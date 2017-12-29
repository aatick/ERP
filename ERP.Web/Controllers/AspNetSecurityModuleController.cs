using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common.Service;
using Common.Service.StoredProcedure;
using DotNetOpenAuth.AspNet;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using AutoMapper;
using System.Data;

namespace ERP.Web.Controllers
{
    public class AspNetSecurityModuleController : BaseController
    {
        #region Variables

        private readonly IAspNetSecurityModuleService aspNetSecurityModuleService;
        private readonly IAspNetRoleService aspNetRoleService;
        private readonly IAspNetRoleModuleService aspNetRoleModuleService;
        private readonly IMenuSPService menuSPService;
        private readonly ISecurityService securityService;

        public AspNetSecurityModuleController(IAspNetSecurityModuleService aspNetSecurityModuleService
            , IAspNetRoleService aspNetRoleService, IMenuSPService menuSPService
            , IAspNetRoleModuleService aspNetRoleModuleService, ISecurityService securityService)
        {
            this.aspNetSecurityModuleService = aspNetSecurityModuleService;
            this.aspNetRoleService = aspNetRoleService;
            this.aspNetRoleModuleService = aspNetRoleModuleService;
            this.menuSPService = menuSPService;
            this.securityService = securityService;
        }

        #endregion


        #region Methods


        public JsonResult SaveSecurityLevel(Dictionary<string, string> allCheck, Dictionary<string, string> allDropDown, Dictionary<string, string> allMode, List<string> allModuleId, string RoleId) //, List<string> allModule List<string> allSecurityId
        {
            try
            {
                var Result = 0;
                var trx = allCheck;
                var trxId = 1;
                var ModuleIds = allModuleId.Where(w => int.TryParse(w, out trxId));

                foreach (var id in ModuleIds)
                {
                    var txtId = "txtId" + id;
                    var txtSecurityId = "txtSecurityId" + id;
                    var txtMode = "txtMode" + id;
                    int ChkId = 0;
                    int ddlId = 0;  //ddlId=allDropDownid == securityid
                    string mode = string.Empty;

                    if (allCheck.ContainsKey(txtId))
                        int.TryParse(allCheck[txtId], out ChkId);

                    if (allDropDown.ContainsKey(txtSecurityId))
                        int.TryParse(allDropDown[txtSecurityId], out ddlId);

                    if (allMode.ContainsKey(txtMode))
                        mode = allMode[txtMode];

                    if (mode == "S" && ChkId == 1)
                    {

                        var param = new { RoleId = RoleId, ModuleId = Convert.ToInt32(id) };

                        var ExRole = menuSPService.SP_GETAspNetRoleModuleIdIsactiveZero(param);


                        if (ExRole.Tables[0].Rows.Count == 1)
                        {
                            int NetRoleModuleId = Convert.ToInt32(ExRole.Tables[0].Rows[0][0]);
                            var param6 = new { AspNetRoleModuleId = NetRoleModuleId, SecurityLevelId = ddlId };
                            menuSPService.SP_UPDATEAspNetRoleModuleIsActive(param6);
                        }
                        else
                        { //save 
                            var param1 = new { RoleId = RoleId, ModuleId = Convert.ToInt32(id), SecurityLevelId = ddlId, IsActive = true, CreatedBy = LoggedInUserId.ToString(), CreateDate = DateTime.Now };

                            int Status = menuSPService.SaveAspNetRoleModule(param1);

                            Result = Status;
                        }


                    }
                    else if (mode == "U")
                    {
                        if (ChkId == 1)
                        {//row update                          

                            var param2 = new { RoleId = RoleId, ModuleId = Convert.ToInt32(id) };

                            var ExRole = menuSPService.SP_GETAspNetRoleModuleId(param2);

                            int AspNetRoleModuleId = Convert.ToInt32(ExRole.Tables[0].Rows[0][0]);

                            if (ExRole.Tables[0].Rows.Count == 1)
                            {
                                var param3 = new { AspNetRoleModuleId = AspNetRoleModuleId, SecurityLevelId = ddlId };
                                menuSPService.SP_UPDATEAspNetRoleSecurityLevelId(param3);
                            }
                            Result = 1;
                        }
                        else
                        {
                            var param4 = new { RoleId = RoleId, ModuleId = Convert.ToInt32(id) };
                            var ExRole = menuSPService.SP_GETAspNetRoleModuleId(param4);
                            int AspNetRoleModuleId = Convert.ToInt32(ExRole.Tables[0].Rows[0][0]);

                            var param3 = new { AspNetRoleModuleId = AspNetRoleModuleId };
                            menuSPService.SP_UPDATEAspNetRoleModule(param3);
                            Result = 1;
                        }
                    }
                }
                return Json(new { Result, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" });
            }
        }

        public void MapDropDownList(AspNetSecurityModuleViewModel model)
        {
            //Designation
            var rolenList = aspNetRoleService.GetAll();
            var roleDetails = rolenList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.Name), Value = m.Id.ToString() });
            model.RoleList = roleDetails;

            var RoleList = new List<SelectListItem>();
            RoleList.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            RoleList.AddRange(roleDetails);
            model.RoleList = RoleList;
        }


        public ActionResult GetAspNetSecurityModule(string RoleId, string ProjectShortName)
        {

            try
            {
                List<AspNetSecurityModuleViewModel> List_AspNetSecurityModuleViewModel = new List<AspNetSecurityModuleViewModel>();
                {
                    var param = new { RoleId = Convert.ToInt32(RoleId), ProjectShortName = ProjectShortName };
                    var RoleList = menuSPService.GetAspNetSecurityModuleRole(param);

                    List_AspNetSecurityModuleViewModel = RoleList.Tables[0].AsEnumerable()
                    .Select(row => new AspNetSecurityModuleViewModel
                    {
                        AspNetSecurityModuleId = row.Field<int>("AspNetSecurityModuleId"),
                        linkText = row.Field<string>("linkText"),
                        ParentName = row.Field<string>("ParentName"),
                        AspNetRoleModuleId = row.Field<int>("AspNetRoleModuleId"),
                        RoleId = row.Field<int>("RoleId"),
                        SecurityLevelId = row.Field<int>("SecurityLevelId"),
                        Mode = row.Field<string>("Mode")
                    }).ToList();
                }
                return Json(List_AspNetSecurityModuleViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region Events

        public ActionResult Index()
        {
            var entity = new AspNetSecurityModuleViewModel();
            MapDropDownList(entity);
            ViewBag.Projects = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, "0");
            return View(entity);

        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
