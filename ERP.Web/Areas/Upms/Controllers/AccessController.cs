using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class AccessController : BaseController
    {
        //services declaration
        private readonly ISPService spService;
        private readonly IReportInformationService reportInformationService;
        private readonly IReportAccessService reportAccessService;
        private readonly IAspNetUserService aspNetUserService;
        private readonly ITradeUploadFileInformationService tradeUploadFileInformationService;
        private readonly ITradeUploadFileAccessService tradeUploadFileAccessService;
        private readonly ITradeTransactionTypeService tradeTransactionTypeService;
        public AccessController(ISPService spService, IReportInformationService reportInformationService, IReportAccessService reportAccessService,
            IAspNetUserService aspNetUserService, ITradeUploadFileInformationService tradeUploadFileInformationService, ITradeUploadFileAccessService tradeUploadFileAccessService,
            ITradeTransactionTypeService tradeTransactionTypeService)
        {
            this.spService = spService;
            this.reportInformationService = reportInformationService;
            this.reportAccessService = reportAccessService;
            this.aspNetUserService = aspNetUserService;
            this.tradeUploadFileInformationService = tradeUploadFileInformationService;
            this.tradeUploadFileAccessService = tradeUploadFileAccessService;
            this.tradeTransactionTypeService = tradeTransactionTypeService;
        }
        

        public ActionResult UploadFileAccess()
        {
            var users =
                spService.GetDataWithoutParameter("SP_GET_ALL_USER").Tables[0].AsEnumerable()
                    .Select(x => new AspNetUser() { UserId = x.Field<int>(0), UserName = x.Field<string>(1) })
                    .ToList();
            var fileModule =
                spService.GetDataWithoutParameter("SP_GET_ALL_File_Module").Tables[0].AsEnumerable()
                    .Select(x => new AspNetSecurityModule() { AspNetSecurityModuleId = x.Field<int>(0), LinkText = x.Field<string>(1) })
                    .ToList();
            ViewBag.Users = users;
            ViewBag.FileModules = fileModule;
            ViewBag.Files = tradeUploadFileInformationService.GetAll().Where(x => x.IsActive).ToList();
            return View();
        }

        public void Get_AccessReport(int rptslnx, string InvestorCode, string UserCode, string cmbReportType)
        {

            if (rptslnx == 1)
            {
                var data =
               spService.GetDataWithParameter(
                   new
                   {
                       Qtype = rptslnx,
                       InvestorCode = "",
                       UserCode = ""
                   },
                   "Rpt_SpecialPermissionInvestorWise").Tables[0];
                ReportHelper.ShowReport(data, cmbReportType, "Rpt_SpecialPermissionInvestor.rpt", "Rpt_SpecialPermissionInvestor");
            }
            else if (rptslnx == 2)
            {
                var data =
               spService.GetDataWithParameter(
                   new
                   {
                       Qtype = rptslnx,
                       InvestorCode = InvestorCode,
                       UserCode = ""
                   },
                   "Rpt_SpecialPermissionInvestorWise").Tables[0];
                ReportHelper.ShowReport(data, cmbReportType, "Rpt_SpecialPermissionInvestorWise.rpt", "Rpt_SpecialPermissionInvestorWise");
            }
            else
            {
                var data =
               spService.GetDataWithParameter(
                   new
                   {
                       Qtype = rptslnx,
                       InvestorCode = "",
                       UserCode = UserCode
                   },
                   "Rpt_SpecialPermissionInvestorWise").Tables[0];
                ReportHelper.ShowReport(data, cmbReportType, "Rpt_SpecialPermissionUserWise.rpt", "Rpt_SpecialPermissionUserWise");
            }

        }


        public JsonResult SaveSpecialInvestorPermission(string InvestorCode, List<int> allUsers, List<int> allCheck, List<int> allPermissionId)
        {
            try
            {
                var Investor = spService.GetDataBySqlCommand("SELECT D.Id FROM InvestorDetails AS D WHERE D.InvestorCode = '" + InvestorCode + "' AND D.IsActive = 1").Tables[0];
                if (Investor.Rows.Count == 1)
                {
                    if (allUsers != null)
                    {
                        var Index = 0;
                        var InvestorId = Investor.Rows[0][0];
                        foreach (var u in allUsers)
                        {
                            var IsCheck = allCheck[Index];
                            var PermissionId = allPermissionId[Index];
                            if (IsCheck == 1 && PermissionId == 0) //New Check
                            {
                                var oldPer = spService.GetDataBySqlCommand("SELECT P.Id FROM SpecialInvestorPermission AS P WHERE P.UserId = " + u + " AND P.InvestorId = " + InvestorId + "").Tables[0];
                                if (oldPer.Rows.Count == 1) //Update
                                {
                                    spService.GetDataBySqlCommand("UPDATE SpecialInvestorPermission SET IsActive = 1 WHERE Id = " + Convert.ToInt32(oldPer.Rows[0][0]) + "");
                                }
                                else //Save
                                {
                                    spService.GetDataBySqlCommand("INSERT INTO SpecialInvestorPermission(UserId,InvestorId,IsActive,CreateDate,CreateUserId) VALUES(" + u + "," + InvestorId + ",1,GETDATE()," + SessionHelper.LoggedInUserId + ")");
                                }

                            }
                            else if (IsCheck == 0 && PermissionId != 0) //Permission Delete
                            {
                                spService.GetDataBySqlCommand("UPDATE SpecialInvestorPermission SET IsActive = 0 WHERE Id = " + PermissionId + "");
                            }

                            Index = Index + 1;
                        }
                    }
                    return Json(new { Result = "Success", Message = "Save successfull. " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "Error", Message = "Invalid Investor" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetSpecialInvestorUserPermission(string InvestorCode)
        {
            try
            {
                var param = new { InvestorCode = InvestorCode };
                var Inv_Info = spService.GetDataWithParameter(param, "Get_SpecialInvestorUserPermission");

                var PermiInfo = Inv_Info.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     RowSl = row.Field<string>("RowSl"),
                     UserId = row.Field<int>("UserId"),
                     emp_office_code = row.Field<string>("emp_office_code"),
                     UserName = row.Field<string>("emp_name"),
                     InvestorId = row.Field<int>("InvestorId"),
                     PermissionId = row.Field<int>("PermissionId"),
                 }).ToList();
                return Json(PermiInfo, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult InvAccess()
        {
            return View();
        }
        public ActionResult InvAccessRpt()
        {
            return View();
        }
        public ActionResult SpecialInvestorAccessReport()
        {
            return View();
        }
        
        public JsonResult GetAccessFileList(int userid)
        {
            var roleid = aspNetUserService.GetByUserId(userid).RoleId;
            var file = tradeUploadFileAccessService.GetAll().Where(x => x.UserId == userid && x.IsActive).ToList();
            var module = spService.GetDataWithParameter(new { USER_ROLE_ID = roleid }, "SP_GET_ALL_File_Module").Tables[0].AsEnumerable()
                    .Select(x => new AspNetSecurityModule() { AspNetSecurityModuleId = x.Field<int>(0), LinkText = x.Field<string>(1) })
                    .ToList();
            return Json(new { File = file, Module = module }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SetFileAccess(int userid, List<int> fileList)
        {
            var files = tradeUploadFileAccessService.GetAll().Where(x => x.UserId == userid).ToList();
            foreach (var aFile in files.Where(x => !fileList.Contains(x.FileId) && x.IsActive))
            {
                aFile.IsActive = false;
                aFile.UpdateDate = DateTime.Now;
                aFile.UpdateUserId = SessionHelper.LoggedInUserId;
                tradeUploadFileAccessService.Update(aFile);
            }
            foreach (var fileId in fileList)
            {
                var access = files.FirstOrDefault(x => x.FileId == fileId);
                if (access == null)
                {
                    tradeUploadFileAccessService.Create(new TradeUploadFileAccess()
                    {
                        UserId = userid,
                        FileId = fileId,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId,
                        IsActive = true
                    });
                }
                else
                {
                    if (!access.IsActive)
                    {
                        access.IsActive = true;
                        access.UpdateDate = DateTime.Now;
                        access.UpdateUserId = SessionHelper.LoggedInUserId;
                        tradeUploadFileAccessService.Update(access);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OnePercentShareSetup(List<int> fileList)
        {
            var types = tradeTransactionTypeService.GetAll().Where(x => x.IsShareType).ToList();
            foreach (var type in types)
            {
                if (fileList.Contains(type.Id))
                {
                    if (!type.IsOnePercent)
                    {
                        type.IsOnePercent = true;
                        type.UpdateDate = DateTime.Now;
                        type.UpdateUserId = SessionHelper.LoggedInUserId;
                        tradeTransactionTypeService.Update(type);
                    }
                }
                else
                {
                    if (type.IsOnePercent)
                    {
                        type.IsOnePercent = false;
                        type.UpdateDate = DateTime.Now;
                        type.UpdateUserId = SessionHelper.LoggedInUserId;
                        tradeTransactionTypeService.Update(type);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectCDBLFiles()
        {
            ViewBag.Files = tradeUploadFileInformationService.GetAll().ToList();
            return View();
        }

        public JsonResult SetCDBLFiles(List<int> fileList)
        {
            var files = tradeUploadFileInformationService.GetAll();

            foreach (var aFile in files)
            {
                aFile.IsActive = fileList.Contains(aFile.Id);
                tradeUploadFileInformationService.Update(aFile);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // ReSharper disable once InconsistentNaming
        public ActionResult SetOnePercentShare()
        {
            ViewBag.TransactionTypes = tradeTransactionTypeService.GetAll().Where(x => x.IsShareType).ToList();
            return View();
        }
    }
}