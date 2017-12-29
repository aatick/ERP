//using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class BrokerController : BaseController
    {

        #region Variable

        private readonly IBrokerDepartmentService brokerDepartmentService;
        private readonly IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService;
        private readonly IBrokerInfoService brokerInfoService;
        // private readonly IBrokerTypeService brokerTypeService;
        private readonly IMarketInfoService marketInfoService;
        private readonly IBrokerEmployeeService brokerEmployeeService;
        private readonly ILookupDesignationService lookupDesignationService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly ILookupThanaService lookupThanaService;
        private readonly ILookupDivisionService lookupDivisionService;
        private readonly ISPService sPService;
        private readonly ILookupDistrictService lookupDistrictService;
        private readonly IBrokerTraderService brokerTraderService;
        private readonly IBrokerDPBranchService brokerDPBranchService;

        public BrokerController(IBrokerDepartmentService brokerDepartmentService, IBrokerDepositoryParticipatoryService brokerDepositoryParticipatoryService, IBrokerInfoService brokerInfoService,
            IMarketInfoService marketInfoService, IBrokerEmployeeService brokerEmployeeService, ILookupDesignationService lookupDesignationService,
            IBrokerBranchService brokerBranchService, ILookupThanaService lookupThanaService, ILookupDivisionService lookupDivisionService, ISPService sPService,
            ILookupDistrictService lookupDistrictService, IBrokerTraderService brokerTraderService, IBrokerDPBranchService brokerDPBranchService)
        {
            this.brokerDepartmentService = brokerDepartmentService;
            this.brokerDepositoryParticipatoryService = brokerDepositoryParticipatoryService;
            this.brokerInfoService = brokerInfoService;
            //this.brokerTypeService = brokerTypeService;
            this.marketInfoService = marketInfoService;
            this.brokerEmployeeService = brokerEmployeeService;
            this.lookupDesignationService = lookupDesignationService;
            this.brokerBranchService = brokerBranchService;
            this.lookupThanaService = lookupThanaService;
            this.lookupDivisionService = lookupDivisionService;
            this.sPService = sPService;
            this.lookupDistrictService = lookupDistrictService;
            this.brokerTraderService = brokerTraderService;
            this.brokerDPBranchService = brokerDPBranchService;
        }
        #endregion

        #region Methods

        #region Broker Department

        public ActionResult Department()
        {
            return View();
        }

        public JsonResult GetBrokerDepartment()
        {
            var DeptInfo = brokerDepartmentService.GetAll();
            return Json(DeptInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DepartmentNameDelete(string Id)
        {
            var Des = brokerDepartmentService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            brokerDepartmentService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveBrokerDepartment(string DepartmentName, string ShortName, string hdnBrokerDeptId)
        {
            var result = 0;
            try
            {

                if (hdnBrokerDeptId == "0") //Save
                {
                    var Desig = new BrokerDepartment()
                    {
                        DepartmentName = DepartmentName,
                        DepartmentShortName = ShortName,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    brokerDepartmentService.Create(Desig);
                    result = 1;
                }
                else   // Edit
                {
                    var Des = brokerDepartmentService.GetById(Convert.ToInt32(hdnBrokerDeptId));

                    Des.DepartmentName = DepartmentName;
                    Des.DepartmentShortName = ShortName;
                    Des.UpdateDate = DateTime.Now;
                    Des.UpdateUserId = SessionHelper.LoggedInUserId;
                    brokerDepartmentService.Update(Des);
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

        //#region Broker Info

        //public ActionResult BrokerList()
        //{
        //    return View();
        //}

        //public ActionResult BrokerInfo()
        //{
        //    var model = new BrokerInfoViewModel();
        //    MapDropDownList(model);
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult BrokerInfo(BrokerInfoViewModel model)
        //{
        //    var entity = Mapper.Map<BrokerInfoViewModel, BrokerInformation>(model);
        //    try
        //    {
        //        //entity.IssueDate = ReportHelper.FormatDateToString(model.IssueDate);
        //        entity.IsActive = true;
        //        entity.CreateDate = DateTime.Now;
        //        entity.CreatedUserId = SessionHelper.LoggedInUserId;
        //        var Company_Info = brokerInfoService.Create(entity);


        //        return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult BrokerinfoEdit(int Id)
        //{
        //    var comInfo = brokerInfoService.GetById(Convert.ToInt32(Id));
        //    var entity = Mapper.Map<BrokerInformation, BrokerInfoViewModel>(comInfo);
        //    MapDropDownList(entity);
        //    return View(entity);
        //}

        //[HttpPost]
        //public ActionResult BrokerinfoEdit(BrokerInfoViewModel model)
        //{

        //    var entity = Mapper.Map<BrokerInfoViewModel, BrokerInformation>(model);
        //    try
        //    {

        //        var broker = brokerInfoService.GetById(Convert.ToInt32(entity.Id));

        //        broker.BrokerCode = entity.BrokerCode;
        //        broker.AuthCapital = entity.AuthCapital;
        //        broker.BrokerName = entity.BrokerName;
        //        broker.DepositMode = entity.DepositMode;
        //        broker.FreeLimit = entity.FreeLimit;
        //        broker.IsOwner = entity.IsOwner;
        //        broker.IssueDate = entity.IssueDate;
        //        broker.PaidUpCapital = entity.PaidUpCapital;
        //        broker.RegistrationNo = entity.RegistrationNo;
        //        broker.BrokerShortName = entity.BrokerShortName;
        //        broker.TotalTrader = entity.TotalTrader;

        //        broker.UpdateDate = DateTime.Now;
        //        broker.UpdateUserId = SessionHelper.LoggedInUserId;

        //        brokerInfoService.Update(broker);

        //        // return RedirectToAction("BrokerList");

        //        return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
        //    }
        //}


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

        //public JsonResult DeleteBrokerInfo(string Id)
        //{
        //    var Des = brokerInfoService.GetById(Convert.ToInt32(Id));


        //    Des.IsActive = false;
        //    Des.UpdateDate = DateTime.Now;
        //    Des.UpdateUserId = SessionHelper.LoggedInUserId;
        //    brokerInfoService.Update(Des);
        //    var result = 1;
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region Broker Depository participant


        public ActionResult BroDepositoryList()
        {
            return View();
        }
        public ActionResult BroDepository()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BroDepository(BrokerDepositoryViewModel model)
        {
            var entity = Mapper.Map<BrokerDepositoryViewModel, BrokerDepositoryPariticipant>(model);
            try
            {


                entity.IsActive = true;
                entity.CreateDate = DateTime.Now;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                brokerDepositoryParticipatoryService.Create(entity);

                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BroDepositoryEdit(int Id)
        {
            var depo = brokerDepositoryParticipatoryService.GetById(Convert.ToInt32(Id));
            var entity = Mapper.Map<BrokerDepositoryPariticipant, BrokerDepositoryViewModel>(depo);
            return View(entity);
        }

        [HttpPost]
        public ActionResult BroDepositoryEdit(BrokerDepositoryViewModel model)
        {
            var entity = Mapper.Map<BrokerDepositoryViewModel, BrokerDepositoryPariticipant>(model);
            try
            {
                var comdep = brokerDepositoryParticipatoryService.GetById(Convert.ToInt32(entity.Id));

                comdep.Address = entity.Address;
                comdep.BORefNo = entity.BORefNo;
                comdep.ClearingAccCSE = entity.ClearingAccCSE;
                comdep.ClearingAccDSE = entity.ClearingAccDSE;
                comdep.ContactNo = entity.ContactNo;
                comdep.ContactPerson = entity.ContactPerson;
                comdep.DepositoryParticipantName = entity.DepositoryParticipantName;
                comdep.DPShortName = entity.DPShortName;
                comdep.DPCode = entity.DPCode;
                comdep.PayInOut = entity.PayInOut;
                comdep.IsActive = true;
                comdep.CreatedUserId = SessionHelper.LoggedInUserId;
                comdep.CreateDate = DateTime.Now;

                brokerDepositoryParticipatoryService.Update(comdep);


                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetBrokerDepositoryInfo()
        {
            var depository = brokerDepositoryParticipatoryService.GetAll().OrderByDescending(s => s.Id);
            return Json(depository, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Broker Employee

        //Rpt_BrokerEmployeeInfo
        

        public JsonResult BrokerEmployeeInfoList(string EmployeeCode, string EmployeeName)
        {
            try
            {
                List<BrokerEmployeeViewModel> List_EmployeeInfo = new List<BrokerEmployeeViewModel>();
                var param = new { EmployeeCode = EmployeeCode, EmployeeName = EmployeeName };
                var empList = sPService.GetDataWithParameter(param, "SP_GetBrokerEmployeeInfo");

                List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new BrokerEmployeeViewModel
                {
                    RowSl = row.Field<long>("RowSl"),
                    Id = row.Field<int>("Id"),
                    DesignationName = row.Field<string>("DesignationName"),
                    DepartmentName = row.Field<string>("DepartmentName"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName"),
                    PresentThanaName = row.Field<string>("PresentThanaName"),
                    Title = row.Field<string>("Title"),
                    EmployeeName = row.Field<string>("EmployeeName"),
                    EmployeeCode = row.Field<string>("EmployeeCode"),
                    Gender = row.Field<string>("Gender"),
                    PresentAddress = row.Field<string>("PresentAddress"),
                    PermanentAddress = row.Field<string>("PermanentAddress"),
                    DateOfBirthMsg = row.Field<string>("DateOfBirthMsg"),
                    JoiningDateMsg = row.Field<string>("JoiningDateMsg"),
                    FatherName = row.Field<string>("FatherName"),
                    MotherName = row.Field<string>("MotherName"),
                    PhoneNo = row.Field<string>("PhoneNo"),
                    Email = row.Field<string>("Email"),
                    MaritialStatus = row.Field<string>("MaritialStatus"),
                    EmergencyContact = row.Field<string>("EmergencyContact"),

                }).ToList();

                return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckEmpCode(string EmpCode)
        {
            var emp = brokerEmployeeService.GetAll().Where(s => s.EmployeeCode == EmpCode);
            var result = "0";
            if (emp.Count() == 0)
            {
                result = "1";
            }
            else
            {
                result = "0";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public byte[] GetImageFromDataBase(int Id)
        {
            var emptImg = brokerEmployeeService.GetById(Id);
            var img = emptImg.Photograph;
            byte[] cover = img;
            return cover;
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/*");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/images/blank-headshot.jpg");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*"); ;
            }
        }

        public byte[] GetSignFromDataBase(int Id)
        {
            var emptImg = brokerEmployeeService.GetById(Id);
            var img = emptImg.Signature;
            byte[] cover = img;
            return cover;
        }
        public ActionResult RetrieveSign(int id)
        {
            byte[] cover = GetSignFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/*");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/images/signature.png");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*"); ;
            }
        }

        public ActionResult BrokerEmployeeList()
        {
            return View();
        }

        public ActionResult BrokerEmployeeInfo()
        {
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.PerDivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;
            var model = new BrokerEmployeeViewModel();
            DropDownListForBrokerEmployee(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult BrokerEmployeeInfo(BrokerEmployeeViewModel model)
        {
            var entity = Mapper.Map<BrokerEmployeeViewModel, BrokerEmployee>(model);
            try
            {
                if (model.PhotographMsg != null)
                {
                    byte[] data = new byte[model.PhotographMsg.ContentLength];
                    model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
                    entity.Photograph = data;
                }
                else
                {
                    entity.Photograph = null;
                }

                if (model.SignatureMsg != null)
                {
                    byte[] data = new byte[model.SignatureMsg.ContentLength];
                    model.SignatureMsg.InputStream.Read(data, 0, model.SignatureMsg.ContentLength);
                    entity.Signature = data;
                }
                else
                {
                    entity.Signature = null;
                }

                if (model.PresentThanaId == 0)
                {
                    entity.PresentThanaId = null;
                }
                if (model.PermanentThanaId == 0)
                {
                    entity.PermanentThanaId = null;
                }
                entity.IsActive = true;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                entity.CreateDate = DateTime.Now;
                entity.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                //entity.CreateBy = LoggedInUserId;

                brokerEmployeeService.Create(entity);


                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return View(model);
                //return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BrokerEmployeeInfoEdit(int Id)
        {

            var PreDivisionId = 0;
            var PreDistrictId = 0;
            var PerDivisionId = 0;
            var PerDistrictId = 0;

            var empInfo = brokerEmployeeService.GetById(Convert.ToInt32(Id));
            if (empInfo.PresentThanaId != 0 && empInfo.PresentThanaId != null)
            {
                PreDivisionId = lookupDistrictService.GetById(lookupThanaService.GetById(Convert.ToInt32(empInfo.PresentThanaId)).DistrictId).DivisionId;
                PreDistrictId = lookupThanaService.GetById(Convert.ToInt32(empInfo.PresentThanaId)).DistrictId;
            }

            if (empInfo.PermanentThanaId != 0 && empInfo.PermanentThanaId != null)
            {
                PerDivisionId = lookupDistrictService.GetById(lookupThanaService.GetById(Convert.ToInt32(empInfo.PermanentThanaId)).DistrictId).DivisionId;
                PerDistrictId = lookupThanaService.GetById(Convert.ToInt32(empInfo.PermanentThanaId)).DistrictId;
            }


            var entity = Mapper.Map<BrokerEmployee, BrokerEmployeeViewModel>(empInfo);
            entity.JoiningDate = empInfo.JoiningDate;
            entity.DateOfBirth = empInfo.DateOfBirth;
            entity.PreDivisionId = PreDivisionId;
            entity.PerDivisionId = PerDivisionId;
            entity.PreDistrictId = PreDistrictId;
            entity.PerDistrictId = PerDistrictId;

            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.PerDivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;
            //var model = new BrokerEmployeeViewModel();
            DropDownListForBrokerEmployee(entity);
            return View(entity);
        }

        [HttpPost]
        public ActionResult BrokerEmployeeInfoEdit(BrokerEmployeeViewModel model)
        {
            var entity = Mapper.Map<BrokerEmployeeViewModel, BrokerEmployee>(model);
            try
            {

                var broker = brokerEmployeeService.GetById(Convert.ToInt32(entity.Id));


                broker.DesignationId = entity.DesignationId;
                broker.DepartmentId = entity.DepartmentId;
                broker.BrokerBranchId = entity.BrokerBranchId;
                if (entity.PresentThanaId == 0)
                {
                    broker.PresentThanaId = null;
                }
                else
                {
                    broker.PresentThanaId = entity.PresentThanaId;
                }
                if (entity.PermanentThanaId == 0)
                {
                    broker.PermanentThanaId = null;
                }
                else
                {
                    broker.PermanentThanaId = entity.PermanentThanaId;
                }
                broker.Title = entity.Title;
                broker.EmployeeName = entity.EmployeeName;
                broker.Gender = entity.Gender;
                broker.PresentAddress = entity.PresentAddress;
                broker.PermanentAddress = entity.PermanentAddress;
                if (entity.DateOfBirth.ToString() != "")
                {
                    broker.DateOfBirth = entity.DateOfBirth;
                }
                else
                {
                    broker.DateOfBirth = null;
                }
                if (entity.JoiningDate.ToString() != "")
                {
                    broker.JoiningDate = entity.JoiningDate;
                }
                else
                {
                    broker.JoiningDate = null;
                }
                //broker.JoiningDate = ReportHelper.FormatDateToString(entity.JoiningDate.ToString());
                broker.FatherName = entity.FatherName;

                broker.MotherName = entity.MotherName;
                broker.PhoneNo = entity.PhoneNo;
                broker.Email = entity.Email;
                broker.MaritialStatus = entity.MaritialStatus;

                broker.AuthRepresentative = entity.AuthRepresentative;
                broker.EmergencyContact = entity.EmergencyContact;
                broker.BrokerBranchId = entity.BrokerBranchId;
                broker.UpdateDate = DateTime.Now;
                broker.UpdateUserId = SessionHelper.LoggedInUserId;


                if (model.PhotographMsg != null)
                {
                    byte[] data = new byte[model.PhotographMsg.ContentLength];
                    if (data != null)
                    {
                        model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
                        broker.Photograph = data;
                    }
                }
                if (model.SignatureMsg != null)
                {
                    byte[] data = new byte[model.SignatureMsg.ContentLength];
                    if (data != null)
                    {
                        model.SignatureMsg.InputStream.Read(data, 0, model.SignatureMsg.ContentLength);
                        broker.Signature = data;
                    }
                }

                brokerEmployeeService.Update(broker);

                // return RedirectToAction("BrokerEmployeeList");
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }



        private void DropDownListForBrokerEmployee(BrokerEmployeeViewModel model)
        {
            ////DesignatioList DepartmentList  BrokerBranchList 
            //BrokerTypeList
            var DesignatioList = lookupDesignationService.GetAll();
            var Designatio_List = DesignatioList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.DesignationName), Value = m.Id.ToString() });

            var Designation = new List<SelectListItem>();
            Designation.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Designation.AddRange(Designatio_List);
            model.DesignatioList = Designation;


            //Department List
            var DepartmentList = brokerDepartmentService.GetAll();
            var Department_List = DepartmentList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.DepartmentName), Value = m.Id.ToString() });

            var Departmen = new List<SelectListItem>();
            Departmen.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Departmen.AddRange(Department_List);
            model.DepartmentList = Departmen;

            ////BrokerBranchList
            var BrokerBranchList = brokerBranchService.GetAll();
            var BrokerBranch_List = BrokerBranchList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.BrokerBranchName), Value = m.Id.ToString() });

            var BrokerBranch = new List<SelectListItem>();
            BrokerBranch.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            BrokerBranch.AddRange(BrokerBranch_List);
            model.BrokerBranchList = BrokerBranch;

            var genderList = new List<SelectListItem>();
            genderList.Add(new SelectListItem() { Text = "Male", Value = "Male" });
            genderList.Add(new SelectListItem() { Text = "Female", Value = "Female" });
            model.GenderList = genderList;

            var MaritalStatusList = new List<SelectListItem>();//
            MaritalStatusList.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            MaritalStatusList.Add(new SelectListItem() { Text = "Married", Value = "Married" });
            MaritalStatusList.Add(new SelectListItem() { Text = "Unmarried", Value = "Unmarried" });
            MaritalStatusList.Add(new SelectListItem() { Text = "Divorce", Value = "Divorce" });
            model.MaritalStatusList = MaritalStatusList;


        }
        #endregion


        //#region Broker branch



        //public ActionResult BrokerBranchList()
        //{
        //    ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
        //    return View();
        //}

        //public ActionResult BrokerBranch()
        //{
        //    ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
        //    ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
        //    IEnumerable<SelectListItem> items = new SelectList(" ");
        //    ViewData["Districtlist"] = items;
        //    ViewData["Thanalist"] = items;
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult BrokerBranch(BrokerBranchViewModel model)
        //{

        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult BrokerBranchEdit(int Id)
        //{
        //    var DivisionId = 0;
        //    var DistrictId = 0;
        //    var comInfo = brokerBranchService.GetById(Convert.ToInt32(Id));
        //    if (comInfo.ThanaId != 0 && comInfo.ThanaId != null)
        //    {
        //        DivisionId = lookupDistrictService.GetById(lookupThanaService.GetById(Convert.ToInt32(comInfo.ThanaId)).DistrictId).DivisionId;
        //        DistrictId = lookupThanaService.GetById(Convert.ToInt32(comInfo.ThanaId)).DistrictId;
        //    }

        //    var entity = Mapper.Map<BrokerBranch, BrokerBranchViewModel>(comInfo);
        //    entity.DivisionId = DivisionId;
        //    entity.DistrictId = DistrictId;
        //    entity.Id = Id;
        //    BrokerBranchDDl(entity);

        //    ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
        //    ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
        //    IEnumerable<SelectListItem> items = new SelectList(" ");
        //    ViewData["Districtlist"] = items;
        //    ViewData["Thanalist"] = items;

        //    return View(entity);
        //}

        //[HttpPost]
        //public ActionResult BrokerBranchEdit(BrokerBranchViewModel model)
        //{

        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}



        //public void BrokerBranchDDl(BrokerBranchViewModel model)
        //{
        //    var BrokeList = brokerInfoService.GetAll();
        //    var Broke_List = BrokeList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.BrokerName), Value = m.Id.ToString() });

        //    var Broker = new List<SelectListItem>();
        //    Broker.Add(new SelectListItem() { Text = "Please Select", Value = "" });
        //    Broker.AddRange(Broke_List);
        //    model.BrokeList = Broker;
        //}

        //public JsonResult GetBrokerBranchInfo()
        //{
        //    List<BrokerBranchViewModel> BroBranch = new List<BrokerBranchViewModel>();
        //    var Master = sPService.GetDataWithoutParameter("SP_GetBrokerBranchInfo");
        //    BroBranch = Master.Tables[0].AsEnumerable()
        //    .Select(row => new BrokerBranchViewModel
        //    {
        //        RowSL = row.Field<long>("RowSL"),
        //        Id = row.Field<int>("Id"),
        //        Address = row.Field<string>("Address"),
        //        BrokerBranchName = row.Field<string>("BrokerBranchName"),
        //        BrokerId = row.Field<int>("BrokerId"),
        //        BrokerName = row.Field<string>("BrokerName"),
        //        ContactEmail = row.Field<string>("ContactEmail"),
        //        ContactFax = row.Field<string>("ContactFax"),
        //        ContactMobile = row.Field<string>("ContactMobile"),
        //        ContactName = row.Field<string>("ContactName"),
        //        Email = row.Field<string>("Email"),
        //        Fax = row.Field<string>("Fax"),
        //        Phone = row.Field<string>("Phone"),
        //        ThanaId = row.Field<int?>("ThanaId"),
        //        ThanaName = row.Field<string>("ThanaName")

        //    }).ToList();
        //    return Json(BroBranch, JsonRequestBehavior.AllowGet);


        //}


        //public JsonResult SaveBrokerBranchInfo(string BrokerBranchId, string BrokerId, string BranchName, string ThanaId, string Address, string Phone, string Email, string Fax, string ContactName, string ContactMobile, string ContactEmail, string ContactFax)
        //{
        //    var result = 0;
        //    try
        //    {
        //        if (BrokerBranchId == "0")
        //        {
        //            var broBranch = new BrokerBranch();

        //            broBranch.BrokerId = Convert.ToInt32(BrokerId);
        //            broBranch.BrokerBranchName = BranchName;
        //            if (ThanaId != "" && ThanaId != null)
        //            {
        //                broBranch.ThanaId = Convert.ToInt32(ThanaId);
        //            }

        //            broBranch.Address = Address;
        //            broBranch.Phone = Phone;
        //            broBranch.Email = Email;
        //            broBranch.Fax = Fax;
        //            broBranch.ContactName = ContactName;
        //            broBranch.ContactMobile = ContactMobile;
        //            broBranch.ContactEmail = ContactEmail;
        //            broBranch.ContactFax = ContactFax;
        //            broBranch.IsActive = true;
        //            broBranch.CreateDate = DateTime.Now;
        //            broBranch.CreatedUserId = SessionHelper.LoggedInUserId;

        //            brokerBranchService.Create(broBranch);
        //        }
        //        else
        //        {
        //            var broBranch = brokerBranchService.GetById(Convert.ToInt32(BrokerBranchId));

        //            broBranch.BrokerId = Convert.ToInt32(BrokerId);
        //            broBranch.BrokerBranchName = BranchName;
        //            if (ThanaId != "" && ThanaId != null)
        //            {
        //                broBranch.ThanaId = Convert.ToInt32(ThanaId);
        //            }

        //            broBranch.Address = Address;
        //            broBranch.Phone = Phone;
        //            broBranch.Email = Email;
        //            broBranch.Fax = Fax;
        //            broBranch.ContactName = ContactName;
        //            broBranch.ContactMobile = ContactMobile;
        //            broBranch.ContactEmail = ContactEmail;
        //            broBranch.ContactFax = ContactFax;
        //            broBranch.IsActive = true;
        //            broBranch.UpdateDate = DateTime.Now;
        //            broBranch.UpdateUserId = SessionHelper.LoggedInUserId;

        //            brokerBranchService.Update(broBranch);
        //        }


        //        result = 1;
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        result = 0;
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}


        //#endregion


        #region Broker Trader

        public JsonResult GetBrokerTraderList()
        {
            try
            {
                List<BrokerTraderViewModel> List_TraderInfo = new List<BrokerTraderViewModel>();

                var empList = sPService.GetDataWithoutParameter("SP_GetBrokerTraderInfo");

                List_TraderInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new BrokerTraderViewModel
                {
                    RowSl = row.Field<long>("RowSl"),
                    Id = row.Field<int>("Id"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName"),
                    ExchangeName = row.Field<string>("ExchangeName"),
                    EmployeeName = row.Field<string>("EmployeeName"),
                    TraderCode = row.Field<string>("TraderCode"),
                    PCNo = row.Field<string>("PCNo"),
                    ValidFromMsg = row.Field<string>("ValidFromMsg"),
                    ValidToMsg = row.Field<string>("ValidToMsg"),

                }).ToList();

                return Json(List_TraderInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void DropDownListForBrokerTrader(BrokerTraderViewModel model)
        {
            //EmployeeList
            var EmployeeList = brokerEmployeeService.GetAll();
            var Employee_List = EmployeeList.Select(m => new SelectListItem() { Text = string.Format("{0} {1}", m.EmployeeCode, m.EmployeeName), Value = m.Id.ToString() });

            var Employee = new List<SelectListItem>();
            Employee.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Employee.AddRange(Employee_List);
            model.EmployeeList = Employee;


            //MarketTypeList
            var MarketList = marketInfoService.GetAll();
            var Market_List = MarketList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.ExchangeName), Value = m.Id.ToString() });

            var Market = new List<SelectListItem>();
            Market.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Market.AddRange(Market_List);
            model.MarketList = Market;

            ////BrokerBranchList
            var BrokerBranchList = brokerBranchService.GetAll();
            var BrokerBranch_List = BrokerBranchList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.BrokerBranchName), Value = m.Id.ToString() });

            var BrokerBranch = new List<SelectListItem>();
            BrokerBranch.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            BrokerBranch.AddRange(BrokerBranch_List);
            model.BrokerBranchList = BrokerBranch;
        }

        public ActionResult BrokerTraderList()
        {
            return View();
        }
        public ActionResult BrokerTrader()
        {
            var traderCodes =
                sPService.GetDataBySqlCommand("SELECT DISTINCT TraderCode FROM TraderCodes").Tables[0].AsEnumerable()
                    .Select(x => x.Field<string>(0))
                    .ToList();
            ViewBag.TraderCodes = traderCodes;
            var model = new BrokerTraderViewModel();
            DropDownListForBrokerTrader(model);
            return View(model);
        }

        [HttpPost]
        public JsonResult BrokerTrader(BrokerTraderViewModel model)
        {
            var entity = Mapper.Map<BrokerTraderViewModel, BrokerTrader>(model);
            if (brokerTraderService.GetAll().FirstOrDefault(x => x.IsActive && x.TraderCode.ToLower() == model.TraderCode.ToLower()) != null)
                return Json(new { Status = false, Message = "This trader code has already been assigned." }, JsonRequestBehavior.AllowGet);
            try
            {
                entity.IsActive = true;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                entity.CreateDate = DateTime.Now;
                entity.BrokerBranchId = SessionHelper.LoggedInOfficeId;

                brokerTraderService.Create(entity);


                return Json(new { Status = true, Message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult TraderDeactivate(int id)
        {
            var trader = brokerTraderService.GetById(id);
            if (trader != null)
            {
                trader.IsActive = false;
                trader.UpdateDate = DateTime.Now;
                trader.UpdateUserId = SessionHelper.LoggedInUserId;
                trader.BrokerBranchId = SessionHelper.LoggedInOfficeId;
            }
            brokerTraderService.Update(trader);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BrokerTraderEdit(int Id)
        {
            var trader = brokerTraderService.GetById(Convert.ToInt32(Id));

            var entity = Mapper.Map<BrokerTrader, BrokerTraderViewModel>(trader);
            DropDownListForBrokerTrader(entity);
            return View(entity);
        }

        [HttpPost]
        public ActionResult BrokerTraderEdit(BrokerTraderViewModel model)
        {
            var entity = Mapper.Map<BrokerTraderViewModel, BrokerTrader>(model);
            try
            {

                var broker = brokerTraderService.GetById(Convert.ToInt32(entity.Id));

                broker.EmployeeId = entity.EmployeeId;
                broker.MarketId = entity.MarketId;
                broker.PCNo = entity.PCNo;
                broker.TraderCode = entity.TraderCode;
                broker.ValidFrom = entity.ValidFrom;
                broker.ValidTo = entity.ValidTo;
                broker.BrokerBranchId = SessionHelper.LoggedInOfficeId;

                broker.UpdateDate = DateTime.Now;
                broker.UpdateUserId = SessionHelper.LoggedInUserId;



                brokerTraderService.Update(broker);

                //return RedirectToAction("BrokerTraderList");
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Broker DP Branch



        public ActionResult BrokerDPBranchList()
        {
            ViewBag.BrokerDepositoryList = brokerDepositoryParticipatoryService.GetAll().ToList();
            return View();
        }

        public ActionResult BrokerDPBranch()
        {
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.BrokerDepositoryList = brokerDepositoryParticipatoryService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            return View();
        }
        [HttpPost]
        public ActionResult BrokerDPBranch(BrokerBranchViewModel model)
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

        public ActionResult BrokerDPBranchEdit(int Id)
        {
            var DivisionId = 0;
            var DistrictId = 0;
            var comInfo = brokerDPBranchService.GetById(Convert.ToInt32(Id));
            if (comInfo.ThanaId != 0 && comInfo.ThanaId != null)
            {
                DivisionId = lookupDistrictService.GetById(lookupThanaService.GetById(Convert.ToInt32(comInfo.ThanaId)).DistrictId).DivisionId;
                DistrictId = lookupThanaService.GetById(Convert.ToInt32(comInfo.ThanaId)).DistrictId;
            }

            var entity = Mapper.Map<BrokerDPBranch, BrokerDPBranchViewModel>(comInfo);
            entity.DivisionId = DivisionId;
            entity.DistrictId = DistrictId;
            BrokerDPBranchDDl(entity);

            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.BrokerDepositoryList = brokerDepositoryParticipatoryService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;

            return View(entity);
        }

        [HttpPost]
        public ActionResult BrokerDPBranchEdit(BrokerDPBranchViewModel model)
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



        public void BrokerDPBranchDDl(BrokerDPBranchViewModel model)
        {
            var BrokeList = brokerDepositoryParticipatoryService.GetAll();
            var Broke_List = BrokeList.Select(m => new SelectListItem() { Text = string.Format("{0}", m.DepositoryParticipantName), Value = m.Id.ToString() });

            var Broker = new List<SelectListItem>();
            Broker.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            Broker.AddRange(Broke_List);
            model.BrokeDepositoryList = Broker;
        }

        public JsonResult GetBrokerDPBranchInfo()
        {

            List<BrokerDPBranchViewModel> BroBranchDP = new List<BrokerDPBranchViewModel>();
            var Master = sPService.GetDataWithoutParameter("SP_GetBroker_DP_BranchInfo");
            BroBranchDP = Master.Tables[0].AsEnumerable()
            .Select(row => new BrokerDPBranchViewModel
            {
                RowSL = row.Field<long>("RowSL"),
                Id = row.Field<int>("Id"),
                Address = row.Field<string>("Address"),
                DPBranchName = row.Field<string>("DPBranchName"),
                DepositoryParticipantId = row.Field<int>("DepositoryParticipantId"),
                DepositoryParticipantName = row.Field<string>("DepositoryParticipantName"),
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
            return Json(BroBranchDP, JsonRequestBehavior.AllowGet);


        }


        public JsonResult SaveBrokerDPBranchInfo(string DPBranchId, string DepositoryParticipantId, string BranchName, string ThanaId, string Address, string Phone, string Email, string Fax, string ContactName, string ContactMobile, string ContactEmail, string ContactFax)
        {
            var result = 0;
            try
            {
                if (DPBranchId == "0")
                {
                    var broBranch = new BrokerDPBranch();

                    broBranch.DepositoryParticipantId = Convert.ToInt32(DepositoryParticipantId);
                    broBranch.DPBranchName = BranchName;
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

                    brokerDPBranchService.Create(broBranch);
                }
                else
                {
                    var broBranch = brokerDPBranchService.GetById(Convert.ToInt32(DPBranchId));

                    broBranch.DepositoryParticipantId = Convert.ToInt32(DepositoryParticipantId);
                    broBranch.DPBranchName = BranchName;
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

                    brokerDPBranchService.Update(broBranch);
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

        #region Set Employee Target

        public ActionResult SetEmployeeTarget()
        {
            ViewBag.Employee = brokerEmployeeService.GetAll().ToList();
            return View();
        }

        public JsonResult UpdateEmployeeTarget(int id, string joiningDate, decimal dailyTarget, decimal fundAcquisition, int isCalculate)
        {
            try
            {
                var employee = brokerEmployeeService.GetById(id);
                if (!string.IsNullOrEmpty(joiningDate))
                {
                    employee.JoiningDate = ReportHelper.FormatNullableDate(joiningDate);
                }
                employee.DailyTradeTarget = dailyTarget;
                employee.FundAcquisitionTargetOfYear = fundAcquisition;
                employee.IsCalculate = isCalculate == 1;
                brokerEmployeeService.Update(employee);
                return Json(new { Status = true, Message = "Employee target updated successfully." },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return
                    Json(
                        new
                        {
                            Status = false,
                            Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                        },
                        JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #endregion



    }
}
