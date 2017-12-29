#region namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;


//using HRM.Service;
#endregion


namespace ERP.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        #region variables

        private readonly ISPService spService;
        private readonly IEmployeeDetailsService employeeDetailsService;
       // private readonly IEMPDocumentUploadService eMPDocumentUploadService;
        private readonly ILookupDistrictService lookupDistrictService;
        private readonly ILookupThanaService lookupThanaService;
        private readonly ILookupCountryService lookupCountryService;
        private readonly ILookupDesignationService lookupDesignationService;
        private readonly ILookupRelationService lookupRelationService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IBrokerDepartmentService brokerDepartmentService;
        public EmployeeController(ISPService spService
            , IEmployeeDetailsService employeeDetailsService
           // , IEMPDocumentUploadService eMPDocumentUploadService
            , ILookupDistrictService lookupDistrictService
            , ILookupThanaService lookupThanaService
            , ILookupCountryService lookupCountryService
            , ILookupDesignationService lookupDesignationService
            , ILookupRelationService lookupRelationService
            , IBrokerBranchService brokerBranchService
            , IBrokerDepartmentService brokerDepartmentService
            )
        {
            this.spService = spService;
            this.employeeDetailsService = employeeDetailsService;
           // this.eMPDocumentUploadService = eMPDocumentUploadService;
            this.lookupDistrictService = lookupDistrictService;
            this.lookupThanaService = lookupThanaService;
            this.lookupCountryService = lookupCountryService;
            this.lookupDesignationService = lookupDesignationService;
            this.lookupRelationService = lookupRelationService;
            this.brokerBranchService = brokerBranchService;
            this.brokerDepartmentService = brokerDepartmentService;
        }
        #endregion

        #region Methods



        public ActionResult EmployeeProfile(int EmployeeId)
        {
            var model = new BrokerEmployeeViewModel();
            var param = new { EmployeeId = EmployeeId };
            var empList = spService.GetDataWithParameter(param, "BrokerEmployeeProfile");

            model.Id = empList.Tables[0].Rows[0].Field<int>("EmployeeId");
            model.DesignationName = empList.Tables[0].Rows[0].Field<string>("DesignationName");
            model.DepartmentName = empList.Tables[0].Rows[0].Field<string>("DepartmentName");
            model.BrokerBranchName = empList.Tables[0].Rows[0].Field<string>("BrokerBranchName");
            // model.BrokerName = empList.Tables[0].Rows[0].Field<string>("BrokerName");
            model.PresentThanaName = empList.Tables[0].Rows[0].Field<string>("PresentThanaName");
            model.PermanentThanaName = empList.Tables[0].Rows[0].Field<string>("PermanentThanaName");

            model.EmployeeName = empList.Tables[0].Rows[0].Field<string>("EmployeeName");
            model.EmployeeCode = empList.Tables[0].Rows[0].Field<string>("EmployeeCode");
            model.Gender = empList.Tables[0].Rows[0].Field<string>("Gender");
            model.PresentAddress = empList.Tables[0].Rows[0].Field<string>("PresentAddress");

            model.PermanentAddress = empList.Tables[0].Rows[0].Field<string>("PermanentAddress");
            model.DateOfBirthMsg = empList.Tables[0].Rows[0].Field<string>("DateOfBirthMsg");
            model.JoiningDateMsg = empList.Tables[0].Rows[0].Field<string>("JoiningDateMsg");
            model.MotherName = empList.Tables[0].Rows[0].Field<string>("MotherName");

            model.FatherName = empList.Tables[0].Rows[0].Field<string>("FatherName");
            model.PhoneNo = empList.Tables[0].Rows[0].Field<string>("PhoneNo");
            model.Email = empList.Tables[0].Rows[0].Field<string>("Email");
            model.MaritialStatus = empList.Tables[0].Rows[0].Field<string>("MaritialStatus");

            model.AuthRepresentative = empList.Tables[0].Rows[0].Field<bool?>("AuthRepresentative");
            model.EmergencyContact = empList.Tables[0].Rows[0].Field<string>("EmergencyContact");


            return View(model);
        }

















        //  byte[] Photograph { get; set; }
        //public byte[] Signature { get; set; }


        //////public byte[] GetDocumentFromDataBase(int Id)
        //////{
        //////    var InvImg = eMPDocumentUploadService.GetById(Id);
        //////    if (InvImg != null)
        //////    {
        //////        var img = InvImg.document_image;
        //////        byte[] cover = img;
        //////        return cover;
        //////    }
        //////    else
        //////    {

        //////        byte[] cover = null;
        //////        return cover;
        //////    }
        //////}
        //////public ActionResult Retrieve_Employee_Document(int id)
        //////{
        //////    byte[] cover = GetDocumentFromDataBase(id);
        //////    if (cover != null)
        //////    {
        //////        MemoryStream ms = new MemoryStream(cover);
        //////        return File(ms.ToArray(), "image/png");
        //////    }
        //////    else
        //////    {
        //////        string strImgPathAbsolute = HttpContext.Server.MapPath("~/Images/BlankPage.png");
        //////        Image img = Image.FromFile(strImgPathAbsolute);
        //////        byte[] blnk;
        //////        using (MemoryStream ms = new MemoryStream())
        //////        {
        //////            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //////            blnk = ms.ToArray();
        //////        }

        //////        return File(blnk, "image/*");
        //////    }
        //////}


        public JsonResult Get_Employee_Document_Info(string EmployeeCode)
        {
            try
            {
                var param = new { EmployeeCode = EmployeeCode };
                var empList = spService.GetDataWithParameter(param, "Get_Employee_Document_Info");

                var Doc_Employee = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    Id = row.Field<long>("Id"),
                    emp_id = row.Field<int>("emp_id"),
                    EmployeeName = row.Field<string>("EmployeeName"),
                    document_type_id = row.Field<int>("document_type_id"),
                    Document_Name = row.Field<string>("Document_Name"),
                    remarks = row.Field<string>("remarks"),

                }).ToList();

                return Json(Doc_Employee, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        public byte[] GetImageFromDataBase(int Id)
        {
            var InvImg = employeeDetailsService.GetByEmpId(Id);
            if (InvImg != null)
            {
                var img = InvImg.EMP_PICTURE_IMAGE;
                byte[] cover = img;
                return cover;
            }
            else
            {

                byte[] cover = null;
                return cover;
            }
        }
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                MemoryStream ms = new MemoryStream(cover);
                return File(ms.ToArray(), "image/png");
            }
            else
            {
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/Images/blank-headshot.jpg");
                Image img = Image.FromFile(strImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*");
            }
        }


        public byte[] GetSignFromDataBase(int Id)
        {
            var InvImg = employeeDetailsService.GetByEmpId(Id);
            if (InvImg != null)
            {
                var img = InvImg.EMP_SIGN_IMAGE;
                byte[] cover = img;
                return cover;
            }
            else
            {

                byte[] cover = null;
                return cover;
            }
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
                string strImgPathAbsolute = HttpContext.Server.MapPath("~/Images/signature.png");
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

        public JsonResult Get_EmployeeInfo_Details_By_Code(string EmployeeCode)
        {
            try
            {
                var param = new { EmployeeCode = EmployeeCode };
                var empList = spService.GetDataWithParameter(param, "Get_EmployeeInfo_Details_By_Code").Tables[0];
                if (empList.Rows.Count == 0)
                {
                    return Json(new { Result = "NoFound", Message = "No employee found", EmployeeInfo = 0 }, JsonRequestBehavior.AllowGet);
                }
                var EmployeeInfo = empList.AsEnumerable()
                .Select(row => new
                {
                    emp_id = row.Field<int>("emp_id"),
                    emp_office_code = row.Field<string>("emp_office_code"),
                    emp_name = row.Field<string>("emp_name"),
                    OfficeEmail = row.Field<string>("OfficeEmail"),
                    emp_datetimeof_birth = row.Field<string>("emp_datetimeof_birth"),
                    emp_joining_datetime = row.Field<string>("emp_joining_datetime"),
                    emp_confirmation_datetime = row.Field<string>("emp_confirmation_datetime"),
                    emp_status_id = row.Field<decimal?>("emp_status_id"),
                    emp_job_id = row.Field<int?>("emp_job_id"),
                    emp_desg_id = row.Field<int?>("emp_desg_id"),
                    emp_branch_id = row.Field<int?>("emp_branch_id"),
                    emp_dept_id = row.Field<int?>("emp_dept_id"),
                    emp_desk_id = row.Field<int?>("emp_desk_id"),
                    emp_increment_flag = row.Field<decimal?>("emp_increment_flag"),
                    emp_noof_increment = row.Field<decimal?>("emp_noof_increment"),

                    //Detail 
                    DetailId = row.Field<int?>("DetailId"),
                    blood_group_id = row.Field<int?>("blood_group_id"),
                    religion_id = row.Field<int?>("religion_id"),
                    marital_status_id = row.Field<int?>("marital_status_id"),
                    emp_country_id = row.Field<int?>("emp_country_id"),
                    emp_gender = row.Field<string>("emp_gender"),
                    emp_father_name = row.Field<string>("emp_father_name"),
                    emp_mother_name = row.Field<string>("emp_mother_name"),
                    emp_spouse_name = row.Field<string>("emp_spouse_name"),
                    emp_spouse_dateofbirth = row.Field<string>("emp_spouse_dateofbirth"),
                    emp_present_address = row.Field<string>("emp_present_address"),
                    emp_present_district_id = row.Field<int?>("emp_present_district_id"),
                    emp_permanent_address = row.Field<string>("emp_permanent_address"),
                    emp_permanent_district_id = row.Field<int?>("emp_permanent_district_id"),


                    desg_name = row.Field<string>("DesignationName"),
                    Dept_name = row.Field<string>("DepartmentName"),
                    job_name = row.Field<string>("job_name"),
                    branch_name = row.Field<string>("BrokerBranchName"),
                    desk_name = row.Field<string>("desk_name"),

                    emp_phone_office = row.Field<string>("emp_phone_office"),
                    emp_phone_residance = row.Field<string>("emp_phone_residance"),
                    emp_mobile = row.Field<string>("emp_mobile"),
                    emp_office_mail_address = row.Field<string>("emp_office_mail_address"),
                    emp_personal_mail_address = row.Field<string>("emp_personal_mail_address"),
                    emp_nation_id_no = row.Field<string>("emp_nation_id_no"),
                    emp_passport_no = row.Field<string>("emp_passport_no"),
                    remarks = row.Field<string>("remarks"),
                    emp_present_thana_id = row.Field<int?>("emp_present_thana_id"),
                    emp_permanent_thana_id = row.Field<int?>("emp_permanent_thana_id"),
                    emp_spouse_contact_no = row.Field<string>("emp_spouse_contact_no"),
                    emergency_contact_person_name = row.Field<string>("emergency_contact_person_name"),
                    emergency_contact_no = row.Field<string>("emergency_contact_no"),
                    relation_emergency_person = row.Field<int?>("relation_emergency_person"),
                    IsSalaryDisburse = row.Field<int?>("IsSalaryDisburse")

                }).ToList();

                return Json(new { Result = "Success", Message = "", EmployeeInfo = EmployeeInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_EmployeeInfo_For_Edit(int EmployeeId)
        {
            try
            {
                var param = new { EmployeeId = EmployeeId };
                var empList = spService.GetDataWithParameter(param, "Get_EmployeeInfo_For_Edit");

                var EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    emp_id = row.Field<int>("emp_id"),
                    emp_office_code = row.Field<string>("emp_office_code"),
                    emp_name = row.Field<string>("emp_name"),
                    OfficeEmail = row.Field<string>("OfficeEmail"),
                    emp_datetimeof_birth = row.Field<string>("emp_datetimeof_birth"),
                    emp_joining_datetime = row.Field<string>("emp_joining_datetime"),
                    emp_confirmation_datetime = row.Field<string>("emp_confirmation_datetime"),
                    emp_status_id = row.Field<decimal?>("emp_status_id"),
                    emp_job_id = row.Field<int?>("emp_job_id"),
                    emp_desg_id = row.Field<int?>("emp_desg_id"),
                    emp_branch_id = row.Field<int?>("emp_branch_id"),
                    emp_dept_id = row.Field<int?>("emp_dept_id"),
                    emp_desk_id = row.Field<int?>("emp_desk_id"),
                    emp_increment_flag = row.Field<decimal?>("emp_increment_flag"),
                    emp_noof_increment = row.Field<decimal?>("emp_noof_increment"),

                    //Detail 
                    DetailId = row.Field<int?>("DetailId"),
                    blood_group_id = row.Field<int?>("blood_group_id"),
                    religion_id = row.Field<int?>("religion_id"),
                    marital_status_id = row.Field<int?>("marital_status_id"),
                    emp_country_id = row.Field<int?>("emp_country_id"),
                    emp_gender = row.Field<string>("emp_gender"),
                    emp_father_name = row.Field<string>("emp_father_name"),
                    emp_mother_name = row.Field<string>("emp_mother_name"),
                    emp_spouse_name = row.Field<string>("emp_spouse_name"),
                    emp_spouse_dateofbirth = row.Field<string>("emp_spouse_dateofbirth"),
                    emp_present_address = row.Field<string>("emp_present_address"),
                    emp_present_district_id = row.Field<int?>("emp_present_district_id"),
                    emp_permanent_address = row.Field<string>("emp_permanent_address"),
                    emp_permanent_district_id = row.Field<int?>("emp_permanent_district_id"),

                    emp_phone_office = row.Field<string>("emp_phone_office"),
                    emp_phone_residance = row.Field<string>("emp_phone_residance"),
                    emp_mobile = row.Field<string>("emp_mobile"),
                    emp_office_mail_address = row.Field<string>("emp_office_mail_address"),
                    emp_personal_mail_address = row.Field<string>("emp_personal_mail_address"),
                    emp_nation_id_no = row.Field<string>("emp_nation_id_no"),
                    emp_passport_no = row.Field<string>("emp_passport_no"),
                    remarks = row.Field<string>("remarks"),
                    emp_present_thana_id = row.Field<int?>("emp_present_thana_id"),
                    emp_permanent_thana_id = row.Field<int?>("emp_permanent_thana_id"),
                    emp_spouse_contact_no = row.Field<string>("emp_spouse_contact_no"),
                    emergency_contact_person_name = row.Field<string>("emergency_contact_person_name"),
                    emergency_contact_no = row.Field<string>("emergency_contact_no"),
                    relation_emergency_person = row.Field<int?>("relation_emergency_person"),
                    IsSalaryDisburse = row.Field<int?>("IsSalaryDisburse")

                }).ToList();

                return Json(EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //

        public JsonResult EmployeeInfoListForAutoComplete()
        {
            try
            {

                var empList = spService.GetDataBySqlCommand("SELECT E.emp_id,E.emp_office_code,E.emp_office_code +' - '+E.emp_name As EmployeeName FROM EMP_PROFILE AS E ");

                var List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {

                    emp_id = row.Field<int>("emp_id"),
                    emp_office_code = row.Field<string>("emp_office_code"),
                    EmployeeName = row.Field<string>("EmployeeName"),
                }).ToList();

                return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EmployeeInfoList(string EmployeeCode, string Employeename, int DesignationId, int BranchId, int DeptId, int BloodGroupId, int DeskId, int JobTypeId, int DistrictId, int ReligionId)
        {
            try
            {
                // EMP_Code  Emp_Name Branch_Id Department_Id BloodGroup_Id Desk_Id JobType_Id  District_Id  Religion_Id
                var param = new { EMP_Code = EmployeeCode, Emp_Name = Employeename, Designation_Id = DesignationId, Branch_Id = BranchId, Department_Id = DeptId, BloodGroup_Id = BloodGroupId, Desk_Id = DeskId, JobType_Id = JobTypeId, District_Id = DistrictId, Religion_Id = ReligionId };
                var empList = spService.GetDataWithParameter(param, "FSP_GET_EMP_PROFILE_INDIVIDUAL");

                var List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    //basic
                    RowSl = row.Field<string>("RowSl"),
                    EMP_ID = row.Field<int>("EMP_ID"),
                    EMP_OFFICE_CODE = row.Field<string>("EMP_OFFICE_CODE"),
                    EMP_NAME = row.Field<string>("EMP_NAME"),
                    EMP_datetimeOF_BIRTH = row.Field<string>("EMP_datetimeOF_BIRTH"),
                    EMP_JOINING_datetime = row.Field<string>("EMP_JOINING_datetime"),
                    EMP_CONFIRMATION_datetime = row.Field<string>("EMP_CONFIRMATION_datetime"),
                    EMP_JOB_NAME = row.Field<string>("EMP_JOB_NAME"),
                    EMP_DESG_NAME = row.Field<string>("EMP_DESG_NAME"),
                    EMP_BRANCH_NAME = row.Field<string>("EMP_BRANCH_NAME"),
                    EMP_DEPT_NAME = row.Field<string>("EMP_DEPT_NAME"),
                    EMP_DESK_NAME = row.Field<string>("EMP_DESK_NAME"),


                    //Details

                    emp_father_name = row.Field<string>("emp_father_name"),
                    emp_mother_name = row.Field<string>("emp_mother_name"),
                    blood_group_name = row.Field<string>("blood_group_name"),
                    emergency_contact_no = row.Field<string>("emergency_contact_no"),
                    emergency_contact_person_name = row.Field<string>("emergency_contact_person_name"),
                    CountryName = row.Field<string>("CountryName"),
                    emp_gender = row.Field<string>("emp_gender"),
                    emp_mobile = row.Field<string>("emp_mobile"),
                    emp_nation_id_no = row.Field<string>("emp_nation_id_no"),
                    emp_passport_no = row.Field<string>("emp_passport_no"),
                    OfficeEmail = row.Field<string>("OfficeEmail"),
                    emp_permanent_address = row.Field<string>("emp_permanent_address"),
                    PermanantDistrict = row.Field<string>("PermanantDistrict"),
                    emp_personal_mail_address = row.Field<string>("emp_personal_mail_address"),
                    emp_phone_office = row.Field<string>("emp_phone_office"),
                    emp_phone_residance = row.Field<string>("emp_phone_residance"),
                    emp_present_address = row.Field<string>("emp_present_address"),
                    PresentDistrict = row.Field<string>("PresentDistrict"),
                    emp_spouse_contact_no = row.Field<string>("emp_spouse_contact_no"),
                    emp_spouse_name = row.Field<string>("emp_spouse_name"),
                    marital_status_name = row.Field<string>("marital_status_name"),
                    religion_name = row.Field<string>("religion_name"),


                }).ToList();

                return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveEmployeeDetails(int EmployeeId, string FatherName, string MotherName, string Gender, int BloodGroupList, int ReligionList, int MaritalStatusList
            , string SpouseName, string SpouseDateOfBirth, string SpouseContactNo, int CountryList, string NationalId, string PassportNo, string LandPhoneNo, string SellPhoneNo
            , string LandPhoneNoOffice, string EmailOffice, string EmailPersonal, string EmergencyContactPerson, string EmergencyContactNo, int? RelationList, string Remarks
            , string PresentAddress, int? ddlDistrictlist, int? ddlThanalist, string PermanentAddress, int? ddlPerDistrictlist, int? ddlPerThanalist, EmployeeModel model)
        {
            try
            {
                //if (RelationList == "") { RelationList = null; };
                if (SpouseDateOfBirth == "01/01/1900")
                {
                    SpouseDateOfBirth = "";
                }
                var param = new
                {
                    PEMP_ID = EmployeeId,
                    PBLOOD_GROUP_ID = BloodGroupList,
                    PRELIGION_ID = ReligionList,
                    PMARITAL_STATUS_ID = MaritalStatusList,
                    PEMP_COUNTRY_ID = CountryList,
                    PEMP_GENDER = Gender,
                    PEMP_FATHER_NAME = FatherName,
                    PEMP_FATHER_STATUS = 1,
                    PEMP_MOTHER_NAME = MotherName,
                    PEMP_MOTHER_STATUS = 1,
                    PEMP_SPOUSE_NAME = SpouseName,
                    PEMP_SPOUSE_dateOFBIRTH = ReportHelper.FormatDateToString(SpouseDateOfBirth),
                    PEMP_PRESENT_ADDRESS = PresentAddress,
                    PEMP_PRESENT_DISTRICT_ID = ddlDistrictlist,
                    PEMP_PERMANENT_ADDRESS = PermanentAddress,
                    PEMP_PERMANENT_DISTRICT_ID = ddlPerDistrictlist,
                    PEMP_PHONE_OFFICE = LandPhoneNoOffice,
                    PEMP_PHONE_RESIDANCE = LandPhoneNo,
                    PEMP_MOBILE = SellPhoneNo,
                    PEMP_OFFICE_MAIL_ADDRESS = EmailOffice,
                    PEMP_PERSONAL_MAIL_ADDRESS = EmailPersonal,
                    PEMP_NATION_ID_NO = NationalId,
                    PEMP_PASSPORT_NO = PassportNo,
                    PEMP_PRESENT_THANA_ID = ddlThanalist,
                    PEMP_PERMANENT_THANA_ID = ddlPerThanalist,
                    PEMP_SPOUSE_CONTACT_NO = SpouseContactNo,
                    PEMERGENCY_CONTACT_PERSON_NAME = EmergencyContactPerson,
                    PEMERGENCY_CONTACT_NO = EmergencyContactNo,
                    PEMERGENCY_PERSON_RELATION = RelationList,
                    PREMARKS = Remarks,
                    PUSER_ID = SessionHelper.LoggedInUserId
                };
                spService.GetDataWithParameter(param, "FSP_ADD_UP_EMP_PERS_DET_PRO");


                //Image Upload

                var Emp = employeeDetailsService.GetByEmpId(EmployeeId);
                if (Emp != null)
                {
                    if (model.PhotographMsg != null)
                    {
                        byte[] data = new byte[model.PhotographMsg.ContentLength];
                        model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
                        Emp.EMP_PICTURE_IMAGE = data;
                    }
                    //else
                    //{
                    //    Emp.EMP_PICTURE_IMAGE = null;
                    //}
                    if (model.SignatureMsg != null)
                    {
                        byte[] data = new byte[model.SignatureMsg.ContentLength];
                        model.SignatureMsg.InputStream.Read(data, 0, model.SignatureMsg.ContentLength);
                        Emp.EMP_SIGN_IMAGE = data;
                    }
                    //else
                    //{
                    //    Emp.EMP_SIGN_IMAGE = null;
                    //}
                    employeeDetailsService.Update(Emp);
                }

                return Json(new { Result = "Success", EmployeeId = EmployeeId, Message = "Save successfull. " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", EmployeeId = "0", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveEmployeeDetailsEx(int EmployeeId, string FatherName, string MotherName, string Gender, int BloodGroupList, int ReligionList, int MaritalStatusList
            , string SpouseName, string SpouseDateOfBirth, string SpouseContactNo, int CountryList, string NationalId, string PassportNo, string LandPhoneNo, string SellPhoneNo
            , string LandPhoneNoOffice, string EmailOffice, string EmailPersonal, string EmergencyContactPerson, string EmergencyContactNo, int? RelationList, string Remarks
            , string PresentAddress, int? ddlDistrictlist, int? ddlThanalist, string PermanentAddress, int? ddlPerDistrictlist, int? ddlPerThanalist)
        {
            try
            {
                //if (RelationList == "") { RelationList = null; };
                if (SpouseDateOfBirth == "01/01/1900")
                {
                    SpouseDateOfBirth = "";
                }
                var param = new
                {
                    PEMP_ID = EmployeeId,
                    PBLOOD_GROUP_ID = BloodGroupList,
                    PRELIGION_ID = ReligionList,
                    PMARITAL_STATUS_ID = MaritalStatusList,
                    PEMP_COUNTRY_ID = CountryList,
                    PEMP_GENDER = Gender,
                    PEMP_FATHER_NAME = FatherName,
                    PEMP_FATHER_STATUS = 1,
                    PEMP_MOTHER_NAME = MotherName,
                    PEMP_MOTHER_STATUS = 1,
                    PEMP_SPOUSE_NAME = SpouseName,
                    PEMP_SPOUSE_dateOFBIRTH = ReportHelper.FormatDateToString(SpouseDateOfBirth),
                    PEMP_PRESENT_ADDRESS = PresentAddress,
                    PEMP_PRESENT_DISTRICT_ID = ddlDistrictlist,
                    PEMP_PERMANENT_ADDRESS = PermanentAddress,
                    PEMP_PERMANENT_DISTRICT_ID = ddlPerDistrictlist,
                    PEMP_PHONE_OFFICE = LandPhoneNoOffice,
                    PEMP_PHONE_RESIDANCE = LandPhoneNo,
                    PEMP_MOBILE = SellPhoneNo,
                    PEMP_OFFICE_MAIL_ADDRESS = EmailOffice,
                    PEMP_PERSONAL_MAIL_ADDRESS = EmailPersonal,
                    PEMP_NATION_ID_NO = NationalId,
                    PEMP_PASSPORT_NO = PassportNo,
                    PEMP_PRESENT_THANA_ID = ddlThanalist,
                    PEMP_PERMANENT_THANA_ID = ddlPerThanalist,
                    PEMP_SPOUSE_CONTACT_NO = SpouseContactNo,
                    PEMERGENCY_CONTACT_PERSON_NAME = EmergencyContactPerson,
                    PEMERGENCY_CONTACT_NO = EmergencyContactNo,
                    PEMERGENCY_PERSON_RELATION = RelationList,
                    PREMARKS = Remarks,
                    PUSER_ID = SessionHelper.LoggedInUserId
                };
                spService.GetDataWithParameter(param, "FSP_ADD_UP_EMP_PERS_DET_PRO");
                return Json(new { Result = "Success", EmployeeId = EmployeeId, Message = "Save successfull. " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", EmployeeId = "0", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveEmployeeBasic(int EmployeeId, string EmployeeCode, string EmployeeName, string OfficeMail, string empDateOfBirth, string DesignamtionList, string empJoiningDate,
                        string DepartmentList, string BranchList, string JobTypeList, int? DeskList, string DueDateofConfirmation, int WithIncrement)
        {
            try
            {
                var IncrFrag = 0;
                if (WithIncrement != 0) { IncrFrag = 1; };

                var param = new { PEMP_ID = EmployeeId, PEMP_OFFICE_CODE = EmployeeCode, PEMP_NAME = EmployeeName, OfficeMail = OfficeMail, PEMP_datetimeOF_BIRTH = ReportHelper.FormatDateToString(empDateOfBirth), PEMP_JOINING_datetime = ReportHelper.FormatDateToString(empJoiningDate), PEMP_CONFIRMATION_datetime = ReportHelper.FormatDateToString(DueDateofConfirmation), PEMP_JOB_ID = JobTypeList, PEMP_DESG_ID = DesignamtionList, PEMP_BRANCH_ID = BranchList, PEMP_DEPT_ID = DepartmentList, PEMP_DESK_ID = DeskList, PEMP_INCREMENT_FLAG = IncrFrag, PEMP_NOOF_INCREMENT = WithIncrement, PUSER_ID = SessionHelper.LoggedInUserId };
                spService.GetDataWithParameter(param, "FSP_ADD_UP_EMP_SETUP_JOINING");
                return Json(new { Result = "Success", EmployeeId = 1, Message = "Save successfull.  " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", EmployeeId = 0, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetDistrictList()
        {
            var DistrictList = lookupDistrictService.GetAll().ToList();//spService.GetDataWithParameter(new { PDIVISION_ID = -1, PDISTRICT_ID = -1 }, "FSP_GET_HR_DISTRICT").Tables[0].AsEnumerable()
            // .Select(row => new { DISTRICT_ID = row.Field<int>("DISTRICT_ID"), DISTRICT_NAME = row.Field<string>("DISTRICT_NAME").ToList() });
            return Json(DistrictList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetddlThanaList(string DistrictId)
        {
            var ThanaList = lookupThanaService.GetAll().Where(x => x.IsActive == true && x.DistrictId == Convert.ToInt32(DistrictId));//spService.GetDataWithParameter(new { PDISTRICT_ID = DistrictId, PTHANA_ID = -1 }, "FSP_GET_HR_THANA").Tables[0].AsEnumerable()
            //.Select(row => new { THANA_ID = row.Field<int>("THANA_ID"), THANA_NAME = row.Field<string>("THANA_NAME").ToList() });
            return Json(ThanaList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDesignationList(int JobTypeId)
        {
            var DesgList = lookupDesignationService.GetAll().Where(x => x.JobTypeId == JobTypeId && x.IsActive == true);//spService.GetDataBySqlCommand("SELECT D.desg_id,D.desg_name FROM HR_DESIGNATION AS D WHERE D.desg_enable_flag = 1 AND D.job_id = " + JobTypeId + "").Tables[0].AsEnumerable()
            //.Select(row => new { desg_id = row.Field<int>("desg_id"), desg_name = row.Field<string>("desg_name").ToList() });
            return Json(DesgList, JsonRequestBehavior.AllowGet);
        }
        public class EmployeeModel
        {
            public int EmployeeId { get; set; }
            public HttpPostedFileBase PhotographMsg { get; set; }
            public HttpPostedFileBase SignatureMsg { get; set; }
        }
        [HttpPost]
        public ActionResult EmployeeImageSave(int EmployeeId, EmployeeModel model)
        {
            var Empl = employeeDetailsService.GetByEmpId(EmployeeId);
            if (model.PhotographMsg != null)
            {

                byte[] data = new byte[model.PhotographMsg.ContentLength];
                model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
                Empl.EMP_PICTURE_IMAGE = data;

            }

            if (model.SignatureMsg != null)
            {
                byte[] data = new byte[model.SignatureMsg.ContentLength];
                model.SignatureMsg.InputStream.Read(data, 0, model.SignatureMsg.ContentLength);
                Empl.EMP_SIGN_IMAGE = data;
            }
            employeeDetailsService.Update(Empl);

            return Json(new { Result = "Success", Message = "Successfull." }, JsonRequestBehavior.AllowGet);
        }
        ////////[HttpPost]
        ////////public ActionResult EmployeeAttachmentSave(int EmployeeId, int DocumentTypeId, string Remarks, EmployeeModel model)
        ////////{
        ////////    var EmpAtt = eMPDocumentUploadService.GetAll().Where(x => x.emp_id == EmployeeId && x.document_type_id == DocumentTypeId);
        ////////    if (EmpAtt.Count() == 0) // Create
        ////////    {
        ////////        if (model.PhotographMsg != null)
        ////////        {
        ////////            var Empl = new EMP_Document_Upload();
        ////////            byte[] data = new byte[model.PhotographMsg.ContentLength];
        ////////            model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
        ////////            Empl.emp_id = EmployeeId;
        ////////            Empl.document_type_id = DocumentTypeId;
        ////////            Empl.document_image = data;
        ////////            Empl.remarks = Remarks;
        ////////            Empl.CreateDate = DateTime.Now;
        ////////            Empl.CreatedUserId = SessionHelper.LoggedInUserId;
        ////////            Empl.IsActive = true;
        ////////            eMPDocumentUploadService.Create(Empl);
        ////////        }
        ////////    }
        ////////    else //Update
        ////////    {
        ////////        if (model.PhotographMsg != null)
        ////////        {
        ////////            var Emp = EmpAtt.FirstOrDefault();
        ////////            byte[] data = new byte[model.PhotographMsg.ContentLength];
        ////////            model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
        ////////            // Emp.emp_id = EmployeeId;
        ////////            //Emp.document_type_id = DocumentTypeId;
        ////////            Emp.document_image = data;
        ////////            Emp.remarks = Remarks;
        ////////            Emp.CreateDate = DateTime.Now;
        ////////            Emp.CreatedUserId = SessionHelper.LoggedInUserId;
        ////////            Emp.IsActive = true;
        ////////            eMPDocumentUploadService.Update(Emp);
        ////////        }
        ////////    }


        ////////    return Json(new { Result = "Success", Message = "Successfull." }, JsonRequestBehavior.AllowGet);
        ////////}


        #endregion


        #region Actions
        public ActionResult Create(int EmployeeId)
        {
            //Basic
            ViewBag.JobTypeList = spService.GetDataWithParameter(new { PJOB_ID = -1 }, "FSP_GET_HR_JOB_TYPE").Tables[0].AsEnumerable().Select(row => new { JOB_ID = row.Field<int>("JOB_ID"), JOB_NAME = row.Field<string>("JOB_NAME") }).ToList();
            ViewBag.DeskList = spService.GetDataWithParameter(new { PDESK_ID = -1 }, "FSP_GET_HR_DESK").Tables[0].AsEnumerable().Select(row => new { DESK_ID = row.Field<int>("DESK_ID"), DESK_NAME = row.Field<string>("DESK_NAME") }).ToList();

            //brokerBranchService;brokerDepartmentService
            ViewBag.BranchList = brokerBranchService.GetAll().Where(x => x.IsActive == true).ToList();//spService.GetDataWithParameter(new { PBRANCH_ID = -1 }, "FSP_GET_HR_BRANCH_INFORMATION").Tables[0].AsEnumerable().Select(row => new { BRANCH_ID = row.Field<int>("BRANCH_ID"), BRANCH_NAME = row.Field<string>("BRANCH_NAME") }).ToList();
            ViewBag.DepartmentList = brokerDepartmentService.GetAll().Where(x => x.IsActive == true).ToList(); //spService.GetDataWithParameter(new { PDEPT_ID = -1 }, "FSP_GET_HR_DEPT").Tables[0].AsEnumerable().Select(row => new { DEPT_ID = row.Field<int>("DEPT_ID"), DEPT_NAME = row.Field<string>("DEPT_NAME") }).ToList();
            //ViewBag.DesignamtionList = spService.GetDataBySqlCommand("SELECT D.desg_id,D.desg_name FROM HR_DESIGNATION AS D WHERE D.desg_enable_flag = 1").Tables[0].AsEnumerable().Select(row => new { desg_id = row.Field<int>("desg_id"), desg_name = row.Field<string>("desg_name") }).ToList();

            //Details

            ViewBag.BloodGroupList = spService.GetDataWithParameter(new { PBLOOD_GROUP_ID = -1 }, "FSP_GET_HR_BLOOD_GROUP").Tables[0].AsEnumerable().Select(row => new { BLOOD_GROUP_ID = row.Field<int>("BLOOD_GROUP_ID"), BLOOD_GROUP_NAME = row.Field<string>("BLOOD_GROUP_NAME") }).ToList();
            ViewBag.ReligionList = spService.GetDataWithParameter(new { PRELIGION_ID = -1 }, "FSP_GET_HR_RELIGION").Tables[0].AsEnumerable().Select(row => new { RELIGION_ID = row.Field<int>("RELIGION_ID"), RELIGION_NAME = row.Field<string>("RELIGION_NAME") }).ToList();
            ViewBag.MaritalStatusList = spService.GetDataWithParameter(new { PMARITAL_STATUS_ID = -1 }, "FSP_GET_HR_MARITAL_STATUS").Tables[0].AsEnumerable().Select(row => new { MARITAL_STATUS_ID = row.Field<int>("MARITAL_STATUS_ID"), MARITAL_STATUS_NAME = row.Field<string>("MARITAL_STATUS_NAME") }).ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();//spService.GetDataWithParameter(new { PCOUNTRY_ID = -1 }, "FSP_GET_HR_COUNTRY").Tables[0].AsEnumerable().Select(row => new { COUNTRY_ID = row.Field<int>("COUNTRY_ID"), COUNTRY_NAME = row.Field<string>("COUNTRY_NAME") }).ToList();
            ViewBag.RelationList = lookupRelationService.GetAll().Where(x => x.IsActive == true).ToList();//spService.GetDataWithParameter(new { PNOMINEE_RELATION_ID = -1 }, "FSP_GET_HR_NOMINEE_RELATION").Tables[0].AsEnumerable().Select(row => new { NOMINEE_RELATION_ID = row.Field<int>("NOMINEE_RELATION_ID"), NOMINEE_RELATION_NAME = row.Field<string>("NOMINEE_RELATION_NAME") }).ToList();

            ViewData["HdnEmployeeId"] = EmployeeId;
            // Districtlist Thanalist PerDistrictlist PerThanalist
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;
            ViewData["DesignationList"] = items;
            return View();
        }
        public ActionResult Index()
        {
            ViewBag.JobTypeList = spService.GetDataWithParameter(new { PJOB_ID = -1 }, "FSP_GET_HR_JOB_TYPE").Tables[0].AsEnumerable().Select(row => new { JOB_ID = row.Field<int>("JOB_ID"), JOB_NAME = row.Field<string>("JOB_NAME") }).ToList();
            ViewBag.DeskList = spService.GetDataWithParameter(new { PDESK_ID = -1 }, "FSP_GET_HR_DESK").Tables[0].AsEnumerable().Select(row => new { DESK_ID = row.Field<int>("DESK_ID"), DESK_NAME = row.Field<string>("DESK_NAME") }).ToList();

            ViewBag.DesignationListSearch = lookupDesignationService.GetAll().Where(x => x.IsActive == true).ToList();

            ViewBag.BloodGroupList = spService.GetDataWithParameter(new { PBLOOD_GROUP_ID = -1 }, "FSP_GET_HR_BLOOD_GROUP").Tables[0].AsEnumerable().Select(row => new { BLOOD_GROUP_ID = row.Field<int>("BLOOD_GROUP_ID"), BLOOD_GROUP_NAME = row.Field<string>("BLOOD_GROUP_NAME") }).ToList();
            ViewBag.ReligionList = spService.GetDataWithParameter(new { PRELIGION_ID = -1 }, "FSP_GET_HR_RELIGION").Tables[0].AsEnumerable().Select(row => new { RELIGION_ID = row.Field<int>("RELIGION_ID"), RELIGION_NAME = row.Field<string>("RELIGION_NAME") }).ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();

            ViewBag.BranchList = brokerBranchService.GetAll().Where(x => x.IsActive == true).ToList();//spService.GetDataWithParameter(new { PBRANCH_ID = -1 }, "FSP_GET_HR_BRANCH_INFORMATION").Tables[0].AsEnumerable().Select(row => new { BRANCH_ID = row.Field<int>("BRANCH_ID"), BRANCH_NAME = row.Field<string>("BRANCH_NAME") }).ToList();
            ViewBag.DepartmentList = brokerDepartmentService.GetAll().Where(x => x.IsActive == true).ToList(); //spService.GetDataWithParameter(new { PDEPT_ID = -1 }, "FSP_GET_HR_DEPT").Tables[0].AsEnumerable().Select(row => new { DEPT_ID = row.Field<int>("DEPT_ID"), DEPT_NAME = row.Field<string>("DEPT_NAME") }).ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["PerDistrictlist"] = items;
            return View();
        }
        

        

        public ActionResult EmpBasic()
        {
            ViewBag.JobTypeList = spService.GetDataWithParameter(new { PJOB_ID = -1 }, "FSP_GET_HR_JOB_TYPE").Tables[0].AsEnumerable().Select(row => new { JOB_ID = row.Field<int>("JOB_ID"), JOB_NAME = row.Field<string>("JOB_NAME") }).ToList();
            ViewBag.DeskList = spService.GetDataWithParameter(new { PDESK_ID = -1 }, "FSP_GET_HR_DESK").Tables[0].AsEnumerable().Select(row => new { DESK_ID = row.Field<int>("DESK_ID"), DESK_NAME = row.Field<string>("DESK_NAME") }).ToList();

            ViewBag.DesignationListSearch = lookupDesignationService.GetAll().Where(x => x.IsActive == true).ToList();

            //DesignationList
            ViewBag.BloodGroupList = spService.GetDataWithParameter(new { PBLOOD_GROUP_ID = -1 }, "FSP_GET_HR_BLOOD_GROUP").Tables[0].AsEnumerable().Select(row => new { BLOOD_GROUP_ID = row.Field<int>("BLOOD_GROUP_ID"), BLOOD_GROUP_NAME = row.Field<string>("BLOOD_GROUP_NAME") }).ToList();
            ViewBag.ReligionList = spService.GetDataWithParameter(new { PRELIGION_ID = -1 }, "FSP_GET_HR_RELIGION").Tables[0].AsEnumerable().Select(row => new { RELIGION_ID = row.Field<int>("RELIGION_ID"), RELIGION_NAME = row.Field<string>("RELIGION_NAME") }).ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();

            ViewBag.BranchList = brokerBranchService.GetAll().Where(x => x.IsActive == true).ToList();//spService.GetDataWithParameter(new { PBRANCH_ID = -1 }, "FSP_GET_HR_BRANCH_INFORMATION").Tables[0].AsEnumerable().Select(row => new { BRANCH_ID = row.Field<int>("BRANCH_ID"), BRANCH_NAME = row.Field<string>("BRANCH_NAME") }).ToList();
            ViewBag.DepartmentList = brokerDepartmentService.GetAll().Where(x => x.IsActive == true).ToList(); //spService.GetDataWithParameter(new { PDEPT_ID = -1 }, "FSP_GET_HR_DEPT").Tables[0].AsEnumerable().Select(row => new { DEPT_ID = row.Field<int>("DEPT_ID"), DEPT_NAME = row.Field<string>("DEPT_NAME") }).ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["DesignationList"] = items;
            ViewData["PerDistrictlist"] = items;
            
            return View();
        }

        
        #endregion
       

       
    }
}
