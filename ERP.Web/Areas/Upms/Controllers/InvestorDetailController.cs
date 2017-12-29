//using UcasPortfolio.Data;
//using Microsoft.AspNet.Identity;
//using System.Data.Entity.Validation;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
    public class InvestorDetailController : BaseController
    {
        #region Variables

        private readonly IInvestorInfoService investorInfoService;
        private readonly IInvestorAccountService investorAccountService;
        private readonly ILookupOccupationService lookupOccupationService;
        private readonly ILookupDivisionService lookupDivisionService;
        private readonly ILookupDistrictService lookupDistrictService;
        private readonly ILookupCountryService lookupCountryService;

        private readonly IBrokerDPBranchService brokerDPBranchService;
        private readonly IInvestorBOTypeService investorBOTypeService;
        private readonly IInvestorBOCategoryService investorBOCategoryService;
        private readonly IBrokerInfoService brokerInfoService;
        private readonly IBrokerBranchService brokerBranchService;

        private readonly IInvestorTypeService investorTypeService;
        private readonly IInvestorAccountTypeService investorAccountTypeService;
        private readonly IInvestorSubAccountTypeService investorSubAccountTypeService;
        private readonly ILookupBankService lookupBankService;
        private readonly ILookupBankBranchService lookupBankBranchService;


        private readonly IInvestorStatusService investorStatusService;
        private readonly IInvestorOperationService investorOperationService;
        private readonly ISPService sPService;
        private readonly IInvestorIntroducerService investorIntroducerService;

        private readonly ILookupRelationService lookupRelationService;
        private readonly IInvestorNomineeService investorNomineeService;
        private readonly INomineeGuardianService nomineeGuardianService;

        private readonly IInvestorCompanyService investorCompanyService;
        private readonly IInvestorJoinHolderService investorJoinHolderService;
        private readonly IBrokerEmployeeService brokerEmployeeService;
        private readonly ILookupThanaService lookupThanaService;

        private readonly IInvestorWiseTransactionChargeService investorWiseTransactionChargeService;
        private readonly IInvestorWiseTransactionChargeSlabService investorWiseTransactionChargeSlabService;
        private readonly IInvestorWiseTransactionChargeHistoryService investorWiseTransactionChargeHistoryService;
        private readonly IInvestorWiseTransactionChargeSlabHistoryService investorWiseTransactionChargeSlabHistoryService;

        private readonly ITradeTransactionChargeService tradeTransactionChargeService;
        private readonly ITradeTransactionChargeSlabService tradeTransactionChargeSlabService;
        private readonly ITradeTransactionChargeHistoryService tradeTransactionChargeHistoryService;
        private readonly ITradeTransactionChargeSlabHistoryService tradeTransactionChargeSlabHistoryService;

        private readonly IOrganizationService organizationService;

        private readonly IMarketInfoService marketInfoService;
        private readonly IInvestorPowerOfAttorneyService investorPowerOfAttorneyService;
        private readonly IInvestorPowerOfAttorneyMappingService investorPowerOfAttorneyMappingService;
        public InvestorDetailController(IInvestorInfoService investorInfoService, IInvestorAccountService investorAccountService, ILookupOccupationService lookupOccupationService, ILookupDivisionService lookupDivisionService, ILookupDistrictService lookupDistrictService, ILookupCountryService lookupCountryService,
            IBrokerDPBranchService brokerDPBranchService, IInvestorBOTypeService investorBOTypeService, IInvestorBOCategoryService investorBOCategoryService, IBrokerInfoService brokerInfoService, IBrokerBranchService brokerBranchService,
            IInvestorTypeService investorTypeService, IInvestorAccountTypeService investorAccountTypeService, IInvestorSubAccountTypeService investorSubAccountTypeService, ILookupBankService lookupBankService, ILookupBankBranchService lookupBankBranchService,
            IInvestorStatusService investorStatusService, IInvestorOperationService investorOperationService, ISPService sPService, IInvestorIntroducerService investorIntroducerService, ILookupRelationService lookupRelationService,
            IInvestorNomineeService investorNomineeService, INomineeGuardianService nomineeGuardianService, IInvestorCompanyService investorCompanyService, IInvestorJoinHolderService investorJoinHolderService, IBrokerEmployeeService brokerEmployeeService, ILookupThanaService lookupThanaService,
            IInvestorWiseTransactionChargeService investorWiseTransactionChargeService, IInvestorWiseTransactionChargeSlabService investorWiseTransactionChargeSlabService,
            IInvestorWiseTransactionChargeHistoryService investorWiseTransactionChargeHistoryService, IInvestorWiseTransactionChargeSlabHistoryService investorWiseTransactionChargeSlabHistoryService,
            ITradeTransactionChargeService tradeTransactionChargeService, ITradeTransactionChargeSlabService tradeTransactionChargeSlabService,
            ITradeTransactionChargeHistoryService tradeTransactionChargeHistoryService, ITradeTransactionChargeSlabHistoryService tradeTransactionChargeSlabHistoryService,
            IMarketInfoService marketInfoService, IOrganizationService organizationService, IInvestorPowerOfAttorneyService investorPowerOfAttorneyService, IInvestorPowerOfAttorneyMappingService investorPowerOfAttorneyMappingService
            )
        {
            this.investorInfoService = investorInfoService;
            this.investorAccountService = investorAccountService;
            this.lookupOccupationService = lookupOccupationService;
            this.lookupDivisionService = lookupDivisionService;
            this.lookupDistrictService = lookupDistrictService;
            this.lookupCountryService = lookupCountryService;

            this.brokerDPBranchService = brokerDPBranchService;
            this.investorBOTypeService = investorBOTypeService;
            this.investorBOCategoryService = investorBOCategoryService;
            this.brokerInfoService = brokerInfoService;
            this.brokerBranchService = brokerBranchService;

            this.investorTypeService = investorTypeService;
            this.investorAccountTypeService = investorAccountTypeService;
            this.investorSubAccountTypeService = investorSubAccountTypeService;
            this.lookupBankService = lookupBankService;
            this.lookupBankBranchService = lookupBankBranchService;

            this.investorStatusService = investorStatusService;
            this.investorOperationService = investorOperationService;
            this.sPService = sPService;
            this.investorIntroducerService = investorIntroducerService;

            this.lookupRelationService = lookupRelationService;
            this.investorNomineeService = investorNomineeService;
            this.nomineeGuardianService = nomineeGuardianService;
            this.investorCompanyService = investorCompanyService;

            this.investorJoinHolderService = investorJoinHolderService;
            this.brokerEmployeeService = brokerEmployeeService;
            this.lookupThanaService = lookupThanaService;

            this.investorWiseTransactionChargeService = investorWiseTransactionChargeService;
            this.investorWiseTransactionChargeSlabService = investorWiseTransactionChargeSlabService;
            this.investorWiseTransactionChargeHistoryService = investorWiseTransactionChargeHistoryService;
            this.investorWiseTransactionChargeSlabHistoryService = investorWiseTransactionChargeSlabHistoryService;

            this.tradeTransactionChargeService = tradeTransactionChargeService;
            this.tradeTransactionChargeSlabService = tradeTransactionChargeSlabService;
            this.tradeTransactionChargeHistoryService = tradeTransactionChargeHistoryService;
            this.tradeTransactionChargeSlabHistoryService = tradeTransactionChargeSlabHistoryService;

            this.marketInfoService = marketInfoService;
            this.organizationService = organizationService;
            this.investorPowerOfAttorneyService = investorPowerOfAttorneyService;
            this.investorPowerOfAttorneyMappingService = investorPowerOfAttorneyMappingService;
        }

        #endregion

        

        public JsonResult SaveAccountReOpen(int InvestorId)
        {
            try
            {
                var invAcc = investorAccountService.GetByInvestorId(InvestorId);

                if (invAcc != null)
                {
                    invAcc.StatusId = 2;
                    invAcc.TemporaryClose = false;
                    invAcc.UpdateDate = DateTime.Now;
                    invAcc.UpdateUserId = SessionHelper.LoggedInUserId;
                    investorAccountService.Update(invAcc);

                    return Json(new { Result = "Success", Message = "Investor re-open successfull" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "Error", Message = "Investor not found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        #region Account Close
        public JsonResult SaveAccountClosing(int InvestorId)
        {
            try
            {
                var invAcc = investorAccountService.GetByInvestorId(InvestorId);

                if (invAcc != null)
                {
                    invAcc.StatusId = 3;
                    invAcc.TemporaryClose = true;
                    invAcc.UpdateDate = DateTime.Now;
                    invAcc.UpdateUserId = SessionHelper.LoggedInUserId;
                    investorAccountService.Update(invAcc);

                    return Json(new { Result = "Success", Message = "Investor close successfull" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "Error", Message = "Investor not found" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Result = "Error", Message =ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion
        public JsonResult GetInvestorInfo_For_AccountClose(string InvestorCode)
        {
            try
            {
                var param = new { InvestorCode = InvestorCode };
                var InvestorInfoEX = sPService.GetDataWithParameter(param, "GetInvestorwisePortfolioAndCurrentBalance_AccountClose").Tables[0];
                var InvestorInfo = InvestorInfoEX
                .AsEnumerable()
                .Select(row => new
                {// InvestorStatus CurrentBalance ImmaturedBalance ClearingChequeBalance ClosingBalance BuyQuantity SaleQuantity TotalQuantity  SaleableQuantity
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorStatus = row.Field<string>("InvestorStatus"),
                    CurrentBalance = row.Field<decimal>("CurrentBalance"),
                    ImmaturedBalance = row.Field<decimal>("ImmaturedBalance"),
                    ClearingChequeBalance = row.Field<decimal>("ClearingChequeBalance"),
                    ClosingBalance = row.Field<decimal>("ClosingBalance"),
                    BuyQuantity = row.Field<int>("BuyQuantity"),
                    SaleQuantity = row.Field<int>("SaleQuantity"),
                    TotalQuantity = row.Field<int>("TotalQuantity"),
                    SaleableQuantity = row.Field<int>("SaleableQuantity"),
                    IsGroupLeader = row.Field<string>("IsGroupLeader"),
                    ClosingCharge = row.Field<decimal>("ClosingCharge"),
                    NonApproveFundTransfer = row.Field<decimal>("NonApproveFundTransfer")
                }).ToList();
                return Json(new { Result = "Success",message = "" ,InvestorInfo = InvestorInfo }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = "Error", message = ex.Message, InvestorInfo = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

       

        #region Investor List
        //GetInvestorInfo
        public JsonResult GetInvestorWiseIntroducerInfo(string InvestorId)
        {
            try
            {
                List<InvestorDetailViewModel> Introducer = new List<InvestorDetailViewModel>();
                var param = new { InvestorId = Convert.ToInt32(InvestorId) };
                var InvList = sPService.GetDataWithParameter(param, "GetInvestorWiseIntroducerInfo");

                Introducer = InvList.Tables[0].AsEnumerable()
                .Select(row => new InvestorDetailViewModel
                {
                    Id = row.Field<int>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    IntroducerName = row.Field<string>("IntroducerName"),
                    EmployeeId = row.Field<int?>("EmployeeId"),

                }).ToList();

                return Json(Introducer, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetInvestorByCode(string code)
        {
            var data = sPService.GetDataWithParameter(new { INVESTOR_CODE = code, IS_CHECK = 1 }, "SP_GET_INVESTOR_NAME_BY_CODE").Tables[0];
            if (data.Rows[0][0].ToString() == "1")
            {
                return Json(new { Status = "ERROR", Message = "This investor code already exists." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "SUCCESS", Message = "" }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetInvestorWiseNomineeInfo(string InvestorId)
        {
            try
            {
                //List<InvestorDetailViewModel> InvAccount = new List<InvestorDetailViewModel>();
                var param = new { InvestorId = Convert.ToInt32(InvestorId) };
                var InvJoinList = sPService.GetDataWithParameter(param, "GetInvestorWiseNomineeInfo");

                var InvAccount = InvJoinList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    Id = row.Field<int>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    //InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    BirthCertificateNo = row.Field<string>("BirthCertificateNo"),
                    CountryName = row.Field<string>("CountryName"),
                    NomineeDateOfBirthMsg = row.Field<string>("DateOfBirthMsg"),
                    DateOfBirthMsg = row.Field<string>("DateOfBirth"),
                    DrivingLicenseNo = row.Field<string>("DrivingLicenseNo"),
                    Email = row.Field<string>("Email"),
                    FatherName = row.Field<string>("FatherName"),
                    // Id,,,,,, ,,,, , ,
                    Gender = row.Field<string>("Gender"),
                    HasPassportMsg = row.Field<string>("HasPassportMsg"),
                    IsMinorMsg = row.Field<string>("IsMinorMsg"),

                    IsResidentMsg = row.Field<string>("IsResidentMsg"),
                    Mobile = row.Field<string>("Mobile"),
                    MotherName = row.Field<string>("MotherName"),

                    NomineeName = row.Field<string>("NomineeName"),
                    NationalId = row.Field<string>("NationalId"),
                    Occupationname = row.Field<string>("Occupationname"),

                    PassportExpireDateMsg = row.Field<string>("PassportExpireDateMsg"),
                    PassportExpireDate = row.Field<string>("PassportExpireDate"),
                    PassportIssueDateMsg = row.Field<string>("PassportIssueDateMsg"),
                    PassportIssueDate = row.Field<string>("PassportIssueDate"),
                    PassportIssuePlace = row.Field<string>("PassportIssuePlace"),

                    PassportNo = row.Field<string>("PassportNo"),
                    Percentage = row.Field<decimal>("Percentage"),
                    PermanentAddress = row.Field<string>("PermanentAddress"),

                    PermanentThanaName = row.Field<string>("PermanentThanaName"),
                    PermanentDistrictName = row.Field<string>("PermanentDistrictName"),
                    Phone = row.Field<string>("Phone"),

                    PresentAddress = row.Field<string>("PresentAddress"),
                    PresentThanaName = row.Field<string>("PresentThanaName"),
                    PresentDistrictName = row.Field<string>("PresentDistrictName"),

                    RelationName = row.Field<string>("Relation")
                }).ToList();

                return Json(InvAccount, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetInvestorWiseAccountInfo(string InvestorId)
        {
            try
            {
                List<InvestorDetailViewModel> InvAccount = new List<InvestorDetailViewModel>();
                var param = new { InvestorId = Convert.ToInt32(InvestorId) };
                var InvJoinList = sPService.GetDataWithParameter(param, "GetInvestorWiseAccountInfo");

                InvAccount = InvJoinList.Tables[0].AsEnumerable()
                .Select(row => new InvestorDetailViewModel
                {
                    Id = row.Field<int>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    DPBranchName = row.Field<string>("DPBranchName"),
                    BOTypeName = row.Field<string>("BOTypeName"),
                    BOCategoryName = row.Field<string>("BOCategoryName"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName"),
                    BrokerName = row.Field<string>("BrokerName"),
                    InvestorTypeName = row.Field<string>("InvestorTypeName"),

                    AccountTypeName = row.Field<string>("AccountTypeName"),
                    SubAccountName = row.Field<string>("SubAccountName"),
                    BranchName = row.Field<string>("BranchName"),

                    BankName = row.Field<string>("BankName"),
                    InvestorStatus = row.Field<string>("InvestorStatus"),
                    OperationTypeName = row.Field<string>("OperationTypeName"),

                    BOID = row.Field<string>("BOID"),
                    BOAccountOpeningDateMsg = row.Field<string>("BOAccountOpeningDateMsg"),
                    TraderIdCSE = row.Field<string>("TraderIdCSE"),

                    TraderIdDSE = row.Field<string>("TraderIdDSE"),
                    BankAccountNo = row.Field<string>("BankAccountNo"),
                    OmnibusInvestorCode = row.Field<string>("OmnibusInvestorCode"),

                    ComissionRate = row.Field<decimal>("ComissionRate"),
                    CounterPartyRate = row.Field<decimal?>("CounterPartyRate"),
                    LoanRatio = row.Field<decimal?>("LoanRatio"),

                    InterestRate = row.Field<decimal?>("InterestRate"),
                    MaxLoan = row.Field<decimal?>("MaxLoan"),
                    IsSMSTradeMsg = row.Field<string>("IsSMSTradeMsg"),

                    IsMailPortfolioMsg = row.Field<string>("IsMailPortfolioMsg"),
                    IsLinkBOMsg = row.Field<string>("IsLinkBOMsg"),
                    LinkBOAccount = row.Field<string>("LinkBOAccount"),

                    IsEDCMsg = row.Field<string>("IsEDCMsg"),
                    IRN = row.Field<string>("IRN")


                }).ToList();

                return Json(InvAccount, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetInvestorWiseJoinHolder(string InvestorId)
        {
            try
            {
                List<InvestorDetailViewModel> JoinHolder = new List<InvestorDetailViewModel>();
                var param = new { InvestorId = Convert.ToInt32(InvestorId) };
                var InvJoinList = sPService.GetDataWithParameter(param, "GetInvestorWiseJoinHolder");

                JoinHolder = InvJoinList.Tables[0].AsEnumerable()
                .Select(row => new InvestorDetailViewModel
                {

                    Id = row.Field<int>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    Occupationname = row.Field<string>("Occupation"),
                    CountryName = row.Field<string>("CountryName"),
                    PresentAddress = row.Field<string>("PresentAddress"),
                    JoinHolderName = row.Field<string>("JoinHolderName"),
                    PresentThanaName = row.Field<string>("PresentThanaName"),
                    PresentDistrictName = row.Field<string>("PresentDistrictName"),

                    FatherName = row.Field<string>("FatherName"),
                    MotherName = row.Field<string>("MotherName"),
                    DateOfBirthMsg = row.Field<string>("DateOfBirthMsg"),

                    Gender = row.Field<string>("Gender"),
                    PermanentAddress = row.Field<string>("PermanentAddress"),
                    PermanentThanaName = row.Field<string>("PermanentThanaName"),

                    PermanentDistrictName = row.Field<string>("PermanentDistrictName"),
                    Phone = row.Field<string>("Phone"),
                    Mobile = row.Field<string>("Mobile"),

                    SMSMobile = row.Field<string>("SMSMobile"),
                    SystemEmail = row.Field<string>("SystemEmail"),
                    EmergencyContactNo = row.Field<string>("EmergencyContactNo"),

                    NationalId = row.Field<string>("NationalId"),
                    DrivingLicenseNo = row.Field<string>("DrivingLicenseNo"),
                    BirthCertificateNo = row.Field<string>("BirthCertificateNo"),

                    IsResidentMsg = row.Field<string>("IsResidentMsg"),
                    HasPassportMsg = row.Field<string>("HasPassportMsg"),
                    PassportNo = row.Field<string>("PassportNo"),

                    PassportIssueDateMsg = row.Field<string>("PassportIssueDateMsg"),
                    PassportExpireDateMsg = row.Field<string>("PassportExpireDateMsg"),
                    PassportIssuePlace = row.Field<string>("PassportNo")


                }).ToList();

                return Json(JoinHolder, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var InvImg = investorInfoService.GetById(Id);
            var img = InvImg.Photograph;
            byte[] cover = img;
            return cover;
        }

        // MemoryStream ms = new MemoryStream(data)
        //return File(ms.ToArray(), "image/png");

        //public Stream GetImageFromDataBase(int Id)
        //{
        //    var InvImg = investorInfoService.GetById(Id);
        //    var img = InvImg.Photograph;
        //    byte[] cover = img;
        //   // Stream cover = img;
        //    if (cover != null)
        //    {
        //        return new MemoryStream((byte[])cover);
        //    }
        //   else
        //    {
        //        return null;
        //    }
        //    //return cover;
        //}





        //public DataSet getUserImage(int Id)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        DbCommand db = dbcon.GetStoredProcCommand("GetImage");
        //        dbcon.AddInParameter(db, "@Id", DbType.Int16, Id);
        //        db.CommandType = CommandType.StoredProcedure;
        //        return ds = dbconstr.ExecuteDataSet(dbCmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ds = null;
        //    }
        //}


        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            //Stream cover = GetImageFromDataBase(id);
            if (cover != null)
            {


                MemoryStream ms = new MemoryStream(cover);
                return File(ms.ToArray(), "image/png");
                //return new MemoryStream((byte[])cover);
                // return File(cover, "image/*");
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
            var InvImg = investorInfoService.GetById(Id);
            var img = InvImg.Signature;
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

        public byte[] ComGetImageFromDataBase(int Id)
        {
            var InvImg = investorCompanyService.GetByInvestorId(Id);
            if (InvImg != null)
            {
                var img = InvImg.Photograph;
                byte[] cover = img;
                return cover;
            }
            else
            {
                byte[] cover = null;
                return cover;
            }
        }
        public ActionResult ComRetrieveImage(int id)
        {
            byte[] cover = ComGetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/*");
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

                return File(blnk, "image/*"); ;
            }
        }

        public byte[] ComSignGetImageFromDataBase(int Id)
        {
            var InvImg = investorCompanyService.GetByInvestorId(Id);
            if (InvImg != null)
            {
                var img = InvImg.Signature;
                byte[] cover = img;
                return cover;
            }
            else
            {

                byte[] cover = null;
                return cover;
            }

        }
        public ActionResult ComSignRetrieveImage(int id)
        {
            byte[] cover = ComSignGetImageFromDataBase(id);
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

        //

        public byte[] JoinGetImageFromDataBase(int Id)
        {
            var InvImg = investorJoinHolderService.GetByInvestorId(Id);
            if (InvImg != null)
            {
                var img = InvImg.Photograph;
                byte[] cover = img;
                return cover;
            }
            else
            {
                byte[] cover = null;
                return cover;
            }

        }
        public ActionResult JoinRetrieveImage(int id)
        {
            byte[] cover = JoinGetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/*");
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

                return File(blnk, "image/*"); ;
            }
        }

        public byte[] JoinSignGetImageFromDataBase(int Id)
        {
            var InvImg = investorJoinHolderService.GetByInvestorId(Id);

            var img = InvImg.Signature;
            byte[] cover = img;
            return cover;
        }
        public ActionResult JoinSignRetrieveImage(int id)
        {
            byte[] cover = JoinSignGetImageFromDataBase(id);
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
        public JsonResult GetInvestorInfo(string InvestorCode, string MobileNo, string InvestorName)
        {
            try
            {
                List<InvestorDetailViewModel> List_Investor = new List<InvestorDetailViewModel>();
                var param = new { InvestorCode = InvestorCode, MobileNo = MobileNo, InvestorName = InvestorName };
                var InvList = sPService.GetDataWithParameter(param, "GetInvestorInfoEx");

                List_Investor = InvList.Tables[0].AsEnumerable()
                .Select(row => new InvestorDetailViewModel
                {
                    RowSl = row.Field<long>("RowSl"),
                    Id = row.Field<int>("Id"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    PhoneRes = row.Field<string>("PhoneRes"),
                    Mobile = row.Field<string>("Mobile"),
                    EmergencyContactNo = row.Field<string>("EmergencyContactNo"),
                    StatusName = row.Field<string>("InvestorStatus"),
                    BrokerBranchName = row.Field<string>("BrokerBranchName")
                }).ToList();

                var json = Json(List_Investor, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;


                //return Json(List_Investor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Power Of Attorney

        public JsonResult EditPowerOfAttorneyEndDate(int Id, string ToDate)
        {
            var atto = investorPowerOfAttorneyMappingService.GetById(Id);
            atto.ValidTo = Convert.ToDateTime(ReportHelper.FormatDateToString(ToDate));
            atto.IsValid = false;
            atto.UpdateDate = DateTime.Now;
            atto.UpdateUserId = SessionHelper.LoggedInUserId;
            investorPowerOfAttorneyMappingService.Update(atto);

            return Json(new { Result = "Ok"},JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveInvestorPowerOfAttorney(string PowerOfAttorneyId, string InvestorId, string DateFrom)
        {
            try
            {
                var attor = investorPowerOfAttorneyMappingService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(InvestorId) && x.ValidTo == null).FirstOrDefault();

                if (attor != null)
                {
                    attor.ValidTo = Convert.ToDateTime(ReportHelper.FormatDateToString(DateFrom)).AddDays(-1);
                    investorPowerOfAttorneyMappingService.Update(attor);
                }

                var AttorMap = new InvestorPowerOfAttorneyMapping();

                AttorMap.InvestorId = Convert.ToInt32(InvestorId);
                AttorMap.PowerOfAttorneyId = Convert.ToInt32(PowerOfAttorneyId);
                AttorMap.ValidFrom = Convert.ToDateTime(ReportHelper.FormatDateToString(DateFrom));
                AttorMap.IsValid = true;
                AttorMap.IsActive = true;
                AttorMap.CreateDate = DateTime.Now;
                AttorMap.CreatedUserId = SessionHelper.LoggedInUserId;

                investorPowerOfAttorneyMappingService.Create(AttorMap);


                return Json(new { Result = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        public JsonResult FindInvestorPowerOfAttorney(string Code,string Name,string Mobile)
        {
            var param = new {Code=Code,Name=Name,Mobile=Mobile};
            var poAttList = sPService.GetDataWithParameter(param, "FindInvestorPowerOfAttorney").Tables[0];
            if (poAttList.Rows.Count >= 1)
            {
                var Result = new { Id = poAttList.Rows[0]["Id"], PowerOfAttorneyName = poAttList.Rows[0]["PowerOfAttorneyName"] };
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Result = new { Id = 0, PowerOfAttorneyName = "" };
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
          
        }//SaveInvestorPowerOfAttorney
        public JsonResult GetInvestorwisePowerOfAttorneyInfo(string InvestorCode)
        {
            try
            {
               
                var param = new { InvestorCode = InvestorCode};
                var poAttList = sPService.GetDataWithParameter(param, "GetInvestorwisePowerOfAttorneyInfo")
                .Tables[0].AsEnumerable()
                .Select(row => new 
                {
                    RowSl = row.Field<string>("RowSl"),//
                    Id = row.Field<int?>("Id"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorName = row.Field<string>("InvestorName"),
                    PowerOfAttorneyId = row.Field<int?>("PowerOfAttorneyId"),
                    PowerOfAttorneyName = row.Field<string>("PowerOfAttorneyName"),
                    ValidFrom = row.Field<string>("ValidFrom"),
                    ValidTo = row.Field<string>("ValidTo"),
                }).ToList();


                return Json(poAttList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetNewPowerOfAttorneyCode()
        {
            string new_code = "";

            var atrLastId = investorPowerOfAttorneyService.GetAll().ToList();
            if (atrLastId.Count==0)
            {
                new_code = "000001";
            }
            else
            {
                var arr = atrLastId.Select(d => d.Id).Max();
                long last_code = arr + 1;
                new_code = last_code.ToString().PadLeft(6, '0');
            }

            return new_code;
        }


        public byte[] GetImageFromDataBaseOfAttorney(int Id)
        {
            var InvImg = investorPowerOfAttorneyService.GetById(Id);
            var img = InvImg.Photograph;
            byte[] cover = img;
            return cover;
        }

        public ActionResult RetrieveImageOfAttorney(int id)
        {
            byte[] cover = GetImageFromDataBaseOfAttorney(id);
            //Stream cover = GetImageFromDataBase(id);
            if (cover != null)
            {


                MemoryStream ms = new MemoryStream(cover);
                return File(ms.ToArray(), "image/png");
                //return new MemoryStream((byte[])cover);
                // return File(cover, "image/*");
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
        public ActionResult RetrieveSignOfAttorney(int id)
        {
            byte[] cover = GetSignFromDataBaseAttorney(id);
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

        public byte[] GetSignFromDataBaseAttorney(int Id)
        {
            var InvImg = investorPowerOfAttorneyService.GetById(Id);
            var img = InvImg.Signature;
            byte[] cover = img;
            return cover;
        }



        public JsonResult GetPowerOfAttorneyList()
        {
            
           //,,,,,,,,,
            try
            {
                var AttorneyList = sPService.GetDataWithoutParameter("GetPowerOfAttorneyList")

                 .Tables[0].AsEnumerable()
                .Select(row => new 
                {
                    RowSl = row.Field<string>("RowSl"),
                    Id = row.Field<int>("Id"),
                    PresentThanaName = row.Field<string>("PresentThanaName"),
                    PresentDistrictName = row.Field<string>("PresentDistrictName"),
                    PermanentThanaName = row.Field<string>("PermanentThanaName"),
                    PermanentDistrictName = row.Field<string>("PermanentDistrictName"),
                    Occupation = row.Field<string>("Occupation"),

                    CountryName = row.Field<string>("CountryName"),
                    PowerOfAttorneyName = row.Field<string>("PowerOfAttorneyName"),
                    PowerOfAttorneyCode = row.Field<string>("PowerOfAttorneyCode"),
                    FatherName = row.Field<string>("FatherName"),
                    MotherName = row.Field<string>("MotherName"),
                    DateOfBirthMsg = row.Field<string>("DateOfBirthMsg"),

                    Gender = row.Field<string>("Gender"),
                    PresentAddress = row.Field<string>("PresentAddress"),
                    PermanentAddress = row.Field<string>("PermanentAddress"),
                    Phone = row.Field<string>("Phone"),
                    Mobile = row.Field<string>("Mobile"),

                    Email = row.Field<string>("Email"),
                    NationalId = row.Field<string>("NationalId"),
                    DrivingLicenseNo = row.Field<string>("DrivingLicenseNo"),
                    BirthCertificateNo = row.Field<string>("BirthCertificateNo")


                }).ToList();

                 var json = Json(AttorneyList, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;


                //return Json(List_Investor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Introducer


        public JsonResult InvestorIntroducerSave(string InvestorId, string IntroducerId, string IntroType)
        {
            var result = 0;
            try
            {
                if (InvestorId != "" && InvestorId != null && IntroducerId != "" && IntroducerId != null)
                {
                    var InvIntro = new InvestorIntroducer();

                    InvIntro.InvestorId = Convert.ToInt32(InvestorId);
                    if (IntroType == "Inv")
                    {
                        InvIntro.IntroducerInvestorId = Convert.ToInt32(IntroducerId);
                    }
                    else
                    {
                        InvIntro.EmployeeId = Convert.ToInt32(IntroducerId);
                    }

                    InvIntro.CreatedUserId = SessionHelper.LoggedInUserId;
                    InvIntro.CreateDate = DateTime.Now;
                    InvIntro.IsActive = true;

                    result = investorIntroducerService.Create(InvIntro).InvestorId;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }//


        public JsonResult GetIntroducerEmpInfo(string EmployeeCode)
        {
           // List<InvestorDetailViewModel> lstEntity = null;
           
            var lstEntity = sPService.GetDataBySqlCommand("SELECT E.Id,CONVERT(VARCHAR,ISNULL(E.EmployeeCode,0)) +' - '+E.EmployeeName AS EmployeeName,E.PhoneNo  FROM BrokerEmployee AS E WHERE E.IsActive = 1 AND E.EmployeeCode = '" + EmployeeCode + "'").Tables[0];

            var Emp = lstEntity.AsEnumerable().Select(x => new {
                Id = x.Field<int>("Id"),
                EmployeeName = x.Field<string>("EmployeeName"),
                PhoneNo = x.Field<string>("PhoneNo"),
            
            }).ToList();
          

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIntroducerInfo(string IntroducerCode)
        {
            //List<InvestorDetailViewModel> lstEntity = null;
            //string sqlString = string.Empty;
            //sqlString = string.Format("SELECT Inv.Id,Inv.InvestorCode+' - '+Inv.InvestorName AS InvestorName,Inv.Mobile,Inv.PresentAddress FROM InvestorDetails AS Inv WHERE Inv.IsActive = 1 AND Inv.InvestorCode = '{0}'", IntroducerCode);

            //SqlDataReader reader = SqlHelper.ExecuteReader(ERP.Web.Helpers.Constants.ConnectionString, CommandType.Text, sqlString);

            //lstEntity = ObjectMapHelper<InvestorDetailViewModel>.MapObject(reader);

            var lstEntity = sPService.GetDataBySqlCommand("SELECT D.Id,D.InvestorCode +'-'+D.InvestorName AS InvestorName,D.PhoneRes,D.Mobile,D.PresentAddress FROM InvestorDetails AS D WHERE (D.IsActive = 1 AND D.InvestorCode IN (SELECT CODE FROM dbo.SplitAndFormatInvestorCode('" + IntroducerCode + "'))) ").Tables[0];

            var Inves = lstEntity.AsEnumerable().Select(x => new
            {
                Id = x.Field<int>("Id"),
                InvestorName = x.Field<string>("InvestorName"),
                Mobile = x.Field<string>("Mobile"),
                PresentAddress = x.Field<string>("PresentAddress"),

            }).ToList();



            return Json(Inves, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Intruducer_Edit

        public JsonResult InvestorIntroducerEdit(int Id, int InvestorId, int IntroducerId,string IntroType)
        {
            try
            {
                if(Id == 0) //Save
                {
                    var intro = new InvestorIntroducer();
                    intro.InvestorId = InvestorId;
                    if (IntroType == "Inv")
                    {
                        intro.IntroducerInvestorId = IntroducerId;
                    }
                    else
                    {
                        intro.EmployeeId = IntroducerId;
                    }
                    intro.IsActive = true;
                    intro.CreateDate = DateTime.Now;
                    intro.CreatedUserId = SessionHelper.LoggedInUserId;
                    intro.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    intro.InvestorBranchId = Convert.ToInt32(sPService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                    investorIntroducerService.Create(intro);
                }
                else //Edit
                {
                    var intro = investorIntroducerService.GetById(Id);
                    intro.InvestorId = InvestorId;
                    if (IntroType == "Inv")
                    {
                        intro.IntroducerInvestorId = IntroducerId;
                    }
                    else
                    {
                        intro.EmployeeId = IntroducerId;
                    }
                   // intro.IsActive = true;
                    intro.UpdateDate = DateTime.Now;
                    intro.UpdateUserId = SessionHelper.LoggedInUserId;
                   // intro.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    //intro.InvestorBranchId = Convert.ToInt32(sPService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + InvestorId + ")").Tables[0].Rows[0][0]);
                    investorIntroducerService.Update(intro);
                }


                return Json(new { Result = "Ok", InvestorId = InvestorId.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = ex.InnerException.Message, InvestorId = "0" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Investor Account

        public JsonResult InvestorAccountSave(string InvestorId, string DPBranchId, string BOTypeId, string BOCategoryId, string BrokerBranchId, string InvestorTypeId, string AccountTypeId, string SubAccountTypeId, string BankBranchId, string StatusId, string OperationTypeId, string BOAccountOpeningDate, string TraderIdDSE, string TraderIdCSE, string BankAccountNo, string OmnibusInvestorCode, string ComissionRate, string CounterPartyRate, string LoanRatio, string InterestRate, string MaxLoan, string IsMailPortfolio, string IsLinkBO, string LinkBOAccount, string IsEDC, string IRN, string IsSMSTrade, string BOID)
        {
            var result = 0;
            try
            {
                var InvstAccount = new InvestorAccount();

                InvstAccount.InvestorId = Convert.ToInt32(InvestorId);//
                if (DPBranchId != null && DPBranchId != "")
                    InvstAccount.DPBranchId = Convert.ToInt32(DPBranchId);
                InvstAccount.BOTypeId = Convert.ToInt32(BOTypeId);//
                InvstAccount.BOCategoryId = Convert.ToInt32(BOCategoryId);//

                InvstAccount.BrokerBranchId = Convert.ToInt32(BrokerBranchId);//
                InvstAccount.InvestorTypeId = Convert.ToInt32(InvestorTypeId);//
                InvstAccount.AccountTypeId = Convert.ToInt32(AccountTypeId);//
                if (SubAccountTypeId != null && SubAccountTypeId != "" && SubAccountTypeId != "0")
                    InvstAccount.SubAccountTypeId = Convert.ToInt32(SubAccountTypeId);
                if (!string.IsNullOrEmpty(BankBranchId))
                    InvstAccount.BankBranchId = Convert.ToInt32(BankBranchId);
                InvstAccount.StatusId = Convert.ToInt32(StatusId);//
                if (!string.IsNullOrEmpty(OperationTypeId))
                    InvstAccount.OperationTypeId = Convert.ToInt32(OperationTypeId);
                if (!string.IsNullOrEmpty(BOAccountOpeningDate))
                    InvstAccount.BOAccountOpeningDate = Convert.ToDateTime(BOAccountOpeningDate);
                InvstAccount.TraderIdDSE = TraderIdDSE;
                InvstAccount.TraderIdCSE = TraderIdCSE;

                InvstAccount.BankAccountNo = BankAccountNo;
                InvstAccount.OmnibusInvestorCode = OmnibusInvestorCode;
                InvstAccount.ComissionRate = Convert.ToDecimal(ComissionRate);//
                if (!string.IsNullOrEmpty(CounterPartyRate))
                    InvstAccount.CounterPartyRate = Convert.ToDecimal(CounterPartyRate);
                if (!string.IsNullOrEmpty(LoanRatio))
                    InvstAccount.LoanRatio = Convert.ToDecimal(LoanRatio);
                if (!string.IsNullOrEmpty(InterestRate))
                    InvstAccount.InterestRate = Convert.ToDecimal(InterestRate);

                if (!string.IsNullOrEmpty(MaxLoan))
                    InvstAccount.MaxLoan = Convert.ToDecimal(MaxLoan);
                InvstAccount.IsMailService = Convert.ToBoolean(IsMailPortfolio);//
                InvstAccount.IsLinkBO = Convert.ToBoolean(IsLinkBO);//
                InvstAccount.LinkBOAccount = LinkBOAccount;
                InvstAccount.IsEDC = Convert.ToBoolean(IsEDC);//
                InvstAccount.IRN = IRN;
                InvstAccount.IsSMSService = Convert.ToBoolean(IsSMSTrade);//
                InvstAccount.BOID = BOID;

                InvstAccount.CreatedUserId = SessionHelper.LoggedInUserId;
                InvstAccount.CreateDate = DateTime.Now;
                InvstAccount.IsActive = true;
                InvstAccount.OfficeID = SessionHelper.LoggedInOfficeId;

                result = investorAccountService.Create(InvstAccount).InvestorId;


                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetBrokerBranchList(string BrokerId)
        {
            var InvBroBranch = brokerBranchService.GetAll().Where(s => s.BrokerId == Convert.ToInt32(BrokerId)).ToList();
            return Json(InvBroBranch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubAccountList(string AccountTypeId)
        {
            var SubAccount = investorSubAccountTypeService.GetAll().Where(s => s.AccountTypeId == Convert.ToInt32(AccountTypeId)).ToList();
            return Json(SubAccount, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBankBranchList(string BankId)
        {
            var BankBrach = lookupBankBranchService.GetAll().Where(s => s.BankId == Convert.ToInt32(BankId)).ToList();
            return Json(BankBrach, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region InvestorWiseCharge

        public JsonResult GetInvestorNameByCode(string investorCode)
        {
            var investor =
                sPService.GetDataWithParameter(new { INVESTOR_CODE = investorCode }, "SP_GET_INVESTOR_NAME_BY_CODE")
                    .Tables[0].AsEnumerable()
                    .Select(x => new { Id = x.Field<int>(0), Code = x.Field<string>(1), Name = x.Field<string>(2) })
                    .ToList();
            if (investor.Count == 0)
                return Json(new { Status = false, Data = "", Charges = "" }, JsonRequestBehavior.AllowGet);
            var charge = sPService.GetDataWithParameter(new { SEARCH_STRING = "", ROW_NO = 100, PAGE_NO = 1, CODE = investor[0].Code }, "SP_GET_INVESTOR_CHARGE_DETAILS").Tables[0].AsEnumerable().Select(x => new InvestorCharge()
            {
                InvestorId = x.Field<int>(0),
                ChargeId = x.Field<int>(1),
                InvestorCode = x.Field<string>(2),
                InvestorName = x.Field<string>(3),
                ChargeName = x.Field<string>(4),
                ChargeAmount = x.Field<decimal>(5),
                MinimumCharge = x.Field<decimal>(6),
                IsPercentage = x.Field<bool>(7),
                IsSlab = x.Field<bool>(8),
                TotalRecord = x.Field<int>(9)
            }).ToList();
            return Json(new { Status = true, Data = investor[0], Charges = charge }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetInvestorWiseCharge(InvestorWiseTransactionCharge charge,
            List<InvestorWiseTransactionChargeSlab> slabList)
        {
            var exist = investorWiseTransactionChargeService.GetAll()
                .FirstOrDefault(x => x.InvestorId == charge.InvestorId && x.TransactionTypeId == charge.TransactionTypeId);
            if (exist == null)
            {
                charge.CreateDate = DateTime.Now;
                charge.CreatedUserId = SessionHelper.LoggedInUserId;
                charge.IsActive = true;
                charge.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                charge.InvestorBranchId = investorAccountService.GetByInvestorId(charge.InvestorId).BrokerBranchId;
                investorWiseTransactionChargeService.Create(charge);
            }
            else
            {
                var history = new InvestorWiseTransactionChargeHistory()
                {
                    InvestorId = exist.InvestorId,
                    TransactionTypeId = exist.TransactionTypeId,
                    ChargeAmount = exist.ChargeAmount,
                    CreateDate = DateTime.Now,
                    CreatedUserId = SessionHelper.LoggedInUserId,
                    IsActive = true,
                    IsPercentage = exist.IsPercentage,
                    IsSlab = exist.IsSlab,
                    MinimumCharge = exist.MinimumCharge,
                    ValidFrom = exist.CreateDate,
                    ValidTo = DateTime.Now,
                    BrokerBranchId = exist.BrokerBranchId,
                    InvestorBranchId = exist.InvestorBranchId
                };
                if (history.ChargeAmount != charge.ChargeAmount || history.IsSlab != charge.IsSlab
                    || history.IsPercentage != charge.IsPercentage || history.MinimumCharge != charge.MinimumCharge)
                {
                    investorWiseTransactionChargeHistoryService.Create(history);
                    exist.ChargeAmount = charge.ChargeAmount;
                    exist.MinimumCharge = charge.MinimumCharge;
                    exist.IsPercentage = charge.IsPercentage;
                    exist.IsSlab = charge.IsSlab;
                    exist.UpdateDate = DateTime.Now;
                    exist.UpdateUserId = SessionHelper.LoggedInUserId;
                    investorWiseTransactionChargeService.Update(exist);
                }
            }

            if (slabList == null) return Json(true, JsonRequestBehavior.AllowGet);
            var slabExist =
                investorWiseTransactionChargeSlabService.GetAll()
                    .Where(x => x.InvestorId == charge.InvestorId && x.TransactionTypeId == charge.TransactionTypeId)
                    .ToList();
            if (slabExist.Count > 0)
            {
                foreach (var transactionChargeSlab in slabExist)
                {
                    var slabHistory = new InvestorWiseTransactionChargeSlabHistory()
                    {
                        Charge = transactionChargeSlab.Charge,
                        AmountTo = transactionChargeSlab.AmountTo,
                        AmountFrom = transactionChargeSlab.AmountFrom,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId,
                        InvestorId = transactionChargeSlab.InvestorId,
                        IsActive = true,
                        IsPercentage = transactionChargeSlab.IsPercentage,
                        TransactionTypeId = transactionChargeSlab.TransactionTypeId,
                        ValidFrom = transactionChargeSlab.CreateDate,
                        ValidTo = DateTime.Now,
                        BrokerBranchId = transactionChargeSlab.BrokerBranchId,
                        InvestorBranchId = transactionChargeSlab.InvestorBranchId
                    };
                    investorWiseTransactionChargeSlabHistoryService.Create(slabHistory);
                    investorWiseTransactionChargeSlabService.Delete(transactionChargeSlab.Id);
                }
            }
            foreach (var slab in slabList)
            {
                slab.CreateDate = DateTime.Now;
                slab.CreatedUserId = SessionHelper.LoggedInUserId;
                slab.IsActive = true;
                slab.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                slab.InvestorBranchId = investorAccountService.GetByInvestorId(slab.InvestorId).BrokerBranchId;
                investorWiseTransactionChargeSlabService.Create(slab);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvestorCharge(string searchText, int row, int page)
        {
            var investor = sPService.GetDataWithParameter(new { SEARCH_STRING = searchText, ROW_NO = row, PAGE_NO = page },
                "SP_GET_INVESTOR_CHARGE_DETAILS").Tables[0].AsEnumerable().Select(x => new InvestorCharge()
                {
                    InvestorId = x.Field<int>(0),
                    ChargeId = x.Field<int>(1),
                    InvestorCode = x.Field<string>(2),
                    InvestorName = x.Field<string>(3),
                    ChargeName = x.Field<string>(4),
                    ChargeAmount = x.Field<decimal>(5),
                    MinimumCharge = x.Field<decimal>(6),
                    IsPercentage = x.Field<bool>(7),
                    IsSlab = x.Field<bool>(8),
                    TotalRecord = x.Field<int>(9)
                }).ToList();
            return Json(investor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetGeneralCharge(TradeTransactionCharge charge, List<TradeTransactionChargeSlab> slabList)
        {
            var exist =
                tradeTransactionChargeService.GetAll()
                    .FirstOrDefault(x => x.TransactionTypeId == charge.TransactionTypeId);
            if (exist == null)
            {
                charge.CreateDate = DateTime.Now;
                charge.CreatedUserId = SessionHelper.LoggedInUserId;
                charge.IsActive = true;
                tradeTransactionChargeService.Create(charge);
            }
            else
            {
                var history = new TradeTransactionChargeHistory()
                {
                    CSECharge = exist.CSECharge,
                    CreateDate = DateTime.Now,
                    CreatedUserId = SessionHelper.LoggedInUserId,
                    DSECharge = exist.DSECharge,
                    IsActive = true,
                    IsCSESlab = exist.IsCSESlab,
                    IsDSESlab = exist.IsDSESlab,
                    IsPercentage = exist.IsPercentage,
                    ChargeType = exist.ChargeType,
                    CalculationBasis = exist.CalculationBasis,
                    CalculationMode = exist.CalculationMode,
                    EffectMode = exist.EffectMode,
                    MinimumCharge = exist.MinimumCharge,
                    TransactionTypeId = exist.TransactionTypeId,
                    ValidFrom = exist.CreateDate,
                    ValidTo = DateTime.Today
                };
                if (history.CSECharge != charge.CSECharge || history.DSECharge != charge.DSECharge
                    || history.IsDSESlab != charge.IsDSESlab || history.IsCSESlab != charge.IsCSESlab
                    || history.IsPercentage != charge.IsPercentage || history.MinimumCharge != charge.MinimumCharge
                    || history.ChargeType != charge.ChargeType || history.CalculationBasis != charge.CalculationBasis
                    || history.CalculationMode != exist.CalculationMode || history.EffectMode != exist.EffectMode)
                {
                    tradeTransactionChargeHistoryService.Create(history);
                    exist.CSECharge = charge.CSECharge;
                    exist.DSECharge = charge.DSECharge;
                    exist.IsCSESlab = charge.IsCSESlab;
                    exist.IsDSESlab = charge.IsDSESlab;
                    exist.IsPercentage = charge.IsPercentage;
                    exist.MinimumCharge = charge.MinimumCharge;
                    exist.ChargeType = charge.ChargeType;
                    exist.CalculationBasis = charge.CalculationBasis;
                    exist.CalculationMode = charge.CalculationMode;
                    exist.EffectMode = charge.EffectMode;
                    exist.UpdateDate = DateTime.Now;
                    exist.UpdateUserId = SessionHelper.LoggedInUserId;
                    tradeTransactionChargeService.Update(exist);
                }
            }
            if (!charge.IsDSESlab && !charge.IsCSESlab) return Json(true, JsonRequestBehavior.AllowGet);
            if (slabList == null) return Json(true, JsonRequestBehavior.AllowGet);
            var dseId = marketInfoService.GetAll().FirstOrDefault(x => x.MarketShortName == "DSE").Id;
            var cseId = marketInfoService.GetAll().FirstOrDefault(x => x.MarketShortName == "CSE").Id;
            if ((!charge.IsDSESlab || slabList.Where(x => x.MarketId == dseId).ToList().Count == 0) &&
                (!charge.IsCSESlab || slabList.Where(x => x.MarketId == cseId).ToList().Count == 0))
                return Json(true, JsonRequestBehavior.AllowGet);
            var existingSlabList =
                tradeTransactionChargeSlabService.GetAll()
                    .Where(x => x.TransactionTypeId == charge.TransactionTypeId && slabList.FirstOrDefault(t => t.Id == x.Id) == null)
                    .ToList();
            foreach (var slab in existingSlabList)
            {
                var slabHistory = new TradeTransactionChargeSlabHistory()
                {
                    Charge = slab.Charge,
                    AccountTypeId = slab.AccountTypeId,
                    AmountTo = slab.AmountTo,
                    AmountFrom = slab.AmountFrom,
                    MarketId = slab.MarketId,
                    CreateDate = DateTime.Now,
                    CreatedUserId = SessionHelper.LoggedInUserId,
                    IsActive = true,
                    IsPercentage = slab.IsPercentage,
                    TransactionTypeId = slab.TransactionTypeId,
                    ValidFrom = slab.CreateDate,
                    ValidTo = DateTime.Now
                };
                tradeTransactionChargeSlabHistoryService.Create(slabHistory);
                tradeTransactionChargeSlabService.Delete(slab.Id);
            }
            foreach (var slab in slabList)
            {
                var existing = tradeTransactionChargeSlabService.GetById(slab.Id);
                slab.AccountTypeId = slab.AccountTypeId == 0 ? null : slab.AccountTypeId;
                if (existing == null)
                {
                    slab.CreateDate = DateTime.Now;
                    slab.CreatedUserId = SessionHelper.LoggedInUserId;
                    slab.IsActive = true;
                    tradeTransactionChargeSlabService.Create(slab);
                }
                else
                {
                    var slabHistory = new TradeTransactionChargeSlabHistory()
                    {
                        Charge = existing.Charge,
                        AccountTypeId = existing.AccountTypeId,
                        AmountTo = existing.AmountTo,
                        AmountFrom = existing.AmountFrom,
                        MarketId = existing.MarketId,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId,
                        IsActive = true,
                        IsPercentage = existing.IsPercentage,
                        TransactionTypeId = existing.TransactionTypeId,
                        ValidFrom = existing.CreateDate,
                        ValidTo = DateTime.Now
                    };
                    if (slabHistory.Charge != slab.Charge || slabHistory.AccountTypeId != slab.AccountTypeId
                        || slabHistory.AmountFrom != slab.AmountFrom || slabHistory.AmountTo != slab.AmountTo
                        || slabHistory.MarketId != slab.MarketId ||
                        slabHistory.IsPercentage != slab.IsPercentage)
                    {
                        tradeTransactionChargeSlabHistoryService.Create(slabHistory);
                        existing.IsPercentage = slab.IsPercentage;
                        existing.MarketId = slab.MarketId;
                        existing.AccountTypeId = slab.AccountTypeId == 0 ? null : slab.AccountTypeId;
                        existing.AmountFrom = slab.AmountFrom;
                        existing.AmountTo = slab.AmountTo;
                        existing.Charge = slab.Charge;
                        existing.UpdateDate = DateTime.Now;
                        existing.UpdateUserId = SessionHelper.LoggedInUserId;
                        tradeTransactionChargeSlabService.Update(existing);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGeneralCharge(int chargeId)
        {
            var charge = tradeTransactionChargeService.GetAll().FirstOrDefault(x => x.TransactionTypeId == chargeId);
            var slab = tradeTransactionChargeSlabService.GetAll().Where(x => x.TransactionTypeId == chargeId).OrderBy(x => x.MarketId).ThenBy(x => x.AccountTypeId).ThenBy(x => x.AmountFrom).ToList();
            return Json(new { Charge = charge, SlabList = slab }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region nominee
        public JsonResult GetNomineeListInfo(string NomineeInvestorId)
        {
            List<InvestorDetailViewModel> NomineeInfo = new List<InvestorDetailViewModel>();
            var param = new { InvestorId = Convert.ToInt32(NomineeInvestorId) };
            var Master = sPService.GetDataWithParameter(param, "GetInvestorNomineeInfo");
            NomineeInfo = Master.Tables[0].AsEnumerable()
            .Select(row => new InvestorDetailViewModel
            {
                Id = row.Field<int>("Id"),

                InvestorId = row.Field<int>("InvestorId"),
                RelationName = row.Field<string>("RelationName"),
                NomineeName = row.Field<string>("NomineeName"),
                Percentage = row.Field<decimal>("Percentage"),
                Mobile = row.Field<string>("Mobile"),

            }).ToList();
            return Json(NomineeInfo, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Actions


        public ActionResult PoAtSet()
        {
            return View();
        }
        public ActionResult PoAList()
        {
            return View();
        }
        public ActionResult PowerOfAttorneyEdit(int Id)
        { 
            ViewBag.PerDivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.OccupationList = lookupOccupationService.GetAll().ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList("");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;


            var power = investorPowerOfAttorneyService.GetById(Convert.ToInt32(Id));
            var model = Mapper.Map<InvestorPowerOfAttorney, PowerofAttorneyViewModel>(power);

            if (power.PresentThanaId != null && power.PresentThanaId != 0)
            {
                var preDisId = string.Empty;
                preDisId = power.PresentThanaId.ToString();
                var DistrictId = lookupThanaService.GetById(Convert.ToInt32(preDisId)).DistrictId;
                model.PresentDistrictId = DistrictId;// lookupThanaService.GetById(Convert.ToInt32(preDisId)).DistrictId;

                model.PresentDivisionId = lookupDistrictService.GetById(Convert.ToInt32(DistrictId)).DivisionId;
            }
            // PresentDistrictId      PresentDivisionId PermanentDivisionId PermanentDistrictId

            if (power.PermanentThanaId != null && power.PermanentThanaId != 0)
            {
                var perDisId = string.Empty;
                perDisId = power.PermanentThanaId.ToString();
                var PerDistrictId = lookupThanaService.GetById(Convert.ToInt32(perDisId)).DistrictId;
                model.PermanentDistrictId = PerDistrictId;
                model.PermanentDivisionId = lookupDistrictService.GetById(Convert.ToInt32(PerDistrictId)).DivisionId;

            }
            //if(power.DateOfBirth != null)ToString("dd-MMM-yyyy");
             ////model.DateOfBirth = Convert.ToDateTime(ReportHelper.FormatDateToString(power.DateOfBirth.ToShortDateString()));
            model.DateOfBirth = power.DateOfBirth.ToString("dd-MM-yyyy");

            return View(model);
        }
        [HttpPost]
        public ActionResult PowerOfAttorneyEdit(PowerofAttorneyViewModel model)
        {
            var entity = investorPowerOfAttorneyService.GetById(model.Id);
            try
            {

                if (model.PhotographMsg != null)
                {
                    byte[] data = new byte[model.PhotographMsg.ContentLength];
                    model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
                    entity.Photograph = data;
                }
              
                if (model.SignatureMsg != null)
                {
                    byte[] data = new byte[model.SignatureMsg.ContentLength];
                    model.SignatureMsg.InputStream.Read(data, 0, model.SignatureMsg.ContentLength);
                    entity.Signature = data;
                }
               

                entity.BirthCertificateNo = model.BirthCertificateNo;
                entity.CountryId = model.CountryId;
                //entity.DateOfBirth = Convert.ToDateTime(ReportHelper.FormatDateToString(model.DateOfBirth.ToShortDateString()));// model.DateOfBirth;
                entity.DateOfBirth = Convert.ToDateTime(ReportHelper.FormatDateToString(model.DateOfBirth));
                entity.DrivingLicenseNo = model.DrivingLicenseNo;
                entity.Email = model.Email;
                entity.FatherName = model.FatherName;
                entity.Gender = model.Gender;
                entity.PowerOfAttorneyName = model.PowerOfAttorneyName;
                entity.Mobile = model.Mobile;
                entity.MotherName = model.MotherName;
                entity.NationalId = model.NationalId;
                entity.OccupationId = model.OccupationId;
                entity.PermanentAddress = model.PermanentAddress;
                entity.PermanentThanaId = model.PermanentThanaId;
                entity.Phone = model.Phone;
                entity.PresentAddress = model.PresentAddress;
                entity.PresentThanaId = model.PresentThanaId;
                entity.Remarks = model.Remarks;

                entity.IsActive = true;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                entity.CreateDate = DateTime.Now;

                investorPowerOfAttorneyService.Update(entity);



                return Json(new { data = entity, Message = "Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity, Message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message) }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PowerOfAttorney()
        {
            ViewBag.PerDivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.OccupationList = lookupOccupationService.GetAll().ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList("");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;
            return View();
        }
          [HttpPost]
        public ActionResult PowerOfAttorney(PowerofAttorneyViewModel model)
        {
            var entity = new InvestorPowerOfAttorney();
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
                    entity.PowerOfAttorneyCode = GetNewPowerOfAttorneyCode();
                    entity.BirthCertificateNo = model.BirthCertificateNo;
                    entity.CountryId = model.CountryId;
                    //entity.DateOfBirth = Convert.ToDateTime(ReportHelper.FormatDateToString(model.DateOfBirth.ToShortDateString()));
                    entity.DateOfBirth = Convert.ToDateTime(model.DateOfBirth);
                    entity.DrivingLicenseNo = model.DrivingLicenseNo;
                    entity.Email = model.Email;
                    entity.FatherName = model.FatherName;
                    entity.Gender = model.Gender;
                    entity.PowerOfAttorneyName = model.PowerOfAttorneyName;
                    entity.Mobile = model.Mobile;
                    entity.MotherName = model.MotherName;
                    entity.NationalId = model.NationalId;
                    entity.OccupationId = model.OccupationId;
                    entity.PermanentAddress = model.PermanentAddress;
                    entity.PermanentThanaId = model.PermanentThanaId;
                    entity.Phone = model.Phone;
                    entity.PresentAddress = model.PresentAddress;
                    entity.PresentThanaId = model.PresentThanaId;
                    entity.Remarks = model.Remarks;

                    entity.IsActive = true;
                    entity.CreatedUserId = SessionHelper.LoggedInUserId;
                    entity.CreateDate = DateTime.Now;

                    investorPowerOfAttorneyService.Create(entity);



                return Json(new { data = entity, Message = "Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity, Message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message) }, JsonRequestBehavior.AllowGet);
            }
        }
      
        public ActionResult Close()
        {
            return View();
        }
        public ActionResult Reopen()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.PerDivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.OccupationList = lookupOccupationService.GetAll().ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();

            ViewBag.DPBranchlist = brokerDPBranchService.GetAll().ToList();
            ViewBag.BOTypelist = investorBOTypeService.GetAll().ToList();
            ViewBag.BOCategorylist = investorBOCategoryService.GetAll().ToList();
            ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
            ViewBag.InvestorTypeList = investorTypeService.GetAll().ToList();
            ViewBag.AccountTypelist = investorAccountTypeService.GetAll().ToList();
            ViewBag.Banklist = lookupBankService.GetAll().ToList();
            ViewBag.InvestorStatuslist = investorStatusService.GetAll().ToList();
            ViewBag.InvestorOperationlist = investorOperationService.GetAll().ToList();
            ViewBag.RelationList = lookupRelationService.GetAll().ToList();


            IEnumerable<SelectListItem> items = new SelectList("");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;

            ViewData["BrokerBranchlist"] = items;
            ViewData["SubAccountTypelist"] = items;
            ViewData["BankBranchlist"] = items;

            //ViewData["Statuslist"] = items;
            // ViewData["OperationTypelist"] = items;
            ViewData["BOIDlist"] = items;


            return View();

        }
        [HttpPost]
        public ActionResult Create(InvestorDetailViewModel model)
        {
            var entity = new InvestorDetail();
            try
            {

                if (model.IsCompany == 0)
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

                    entity.BirthCertificateNo = model.BirthCertificateNo;
                    entity.CountryId = model.ComCountryId;
                    entity.DateOfBirth = model.DateOfBirth;
                    entity.DrivingLicenseNo = model.DrivingLicenseNo;
                    entity.Email = model.Email;
                    entity.EmergencyContactNo = model.EmergencyContactNo;
                    entity.ETIN = model.ETIN;
                    entity.FatherName = model.FatherName;
                    entity.Fax = model.Fax;
                    entity.Gender = model.Gender;
                    entity.HasPassport = model.HasPassport;
                    entity.InvestorCode = model.InvestorCode;
                    var organization = organizationService.GetAll().FirstOrDefault();
                    if (organization.HasPaddingInInvestorCode)
                    {
                        entity.InvestorCode = model.InvestorCode.PadLeft(organization.LengthOfInvestorCode, '0');
                    }
                    entity.InvestorName = model.InvestorName;
                    entity.IsResident = model.IsResident;
                    entity.Mobile = model.Mobile;
                    entity.MotherName = model.MotherName;
                    entity.NationalId = model.NationalId;
                    entity.OccupationId = model.OccupationId;
                    entity.PassportExpireDate = model.PassportExpireDate;
                    entity.PassportIssueDate = model.PassportIssueDate;
                    entity.PassportIssuePlace = model.PassportIssuePlace;
                    entity.PassportNo = model.PassportNo;
                    entity.PermanentAddress = model.PermanentAddress;
                    entity.PermanentThanaId = model.PermanentThanaId;
                    entity.PhoneOffice = model.PhoneOffice;
                    entity.PhoneRes = model.PhoneRes;
                    entity.PresentAddress = model.PresentAddress;
                    entity.PresentThanaId = model.PresentThanaId;
                    entity.SMSMobile = model.SMSMobile;
                    entity.SpecialInstruction = model.SpecialInstruction;
                    entity.SystemEmail = model.SystemEmail;
                    entity.Title = model.Title;
                    entity.IsCompany = 0;

                    entity.IsActive = true;
                    entity.CreatedUserId = SessionHelper.LoggedInUserId;
                    entity.CreateDate = DateTime.Now;
                    //entity.CreateBy = LoggedInUserId;

                    investorInfoService.Create(entity);


                }
                else
                {
                    entity.CountryId = model.ComCountryId;
                    entity.DateOfBirth = model.ComDateOfBirth;
                    entity.Email = model.ComEmail;
                    entity.EmergencyContactNo = model.ComEmergencyContactNo;
                    entity.Fax = model.ComFax;
                    entity.InvestorCode = model.ComInvestorCode;
                    entity.InvestorName = model.ComInvestorName;
                    entity.Mobile = model.ComMobile;
                    entity.PhoneOffice = model.ComPhoneOffice;
                    entity.PresentAddress = model.ComPresentAddress;
                    entity.PresentThanaId = model.ComPresentThanaId;
                    entity.SMSMobile = model.ComSMSMobile;
                    entity.SystemEmail = model.ComSystemEmail;
                    entity.IsActive = true;
                    entity.CreatedUserId = SessionHelper.LoggedInUserId;
                    entity.CreateDate = DateTime.Now;
                    entity.IsCompany = 1;

                    var InvestorId = investorInfoService.Create(entity).Id;


                    var ComCon = new InvestorCompany();
                    ComCon.InvestorId = InvestorId;
                    ComCon.ContactPerson = model.ContactPerson;
                    ComCon.RegNo = model.RegNo;
                    ComCon.RegisteredDate = model.RegisteredDate;
                    ComCon.IsActive = true;
                    ComCon.CreatedUserId = SessionHelper.LoggedInUserId;
                    ComCon.CreateDate = DateTime.Now;

                    if (model.ComPhotographMsg != null)
                    {
                        byte[] data = new byte[model.ComPhotographMsg.ContentLength];
                        model.ComPhotographMsg.InputStream.Read(data, 0, model.ComPhotographMsg.ContentLength);
                        ComCon.Photograph = data;
                    }
                    else
                    {
                        ComCon.Photograph = null;
                    }

                    if (model.ComSignatureMsg != null)
                    {
                        byte[] data = new byte[model.ComSignatureMsg.ContentLength];
                        model.ComSignatureMsg.InputStream.Read(data, 0, model.ComSignatureMsg.ContentLength);
                        ComCon.Signature = data;
                    }
                    else
                    {
                        ComCon.Signature = null;
                    }
                    investorCompanyService.Create(ComCon);
                }


                return Json(new { data = entity, Message = "Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity, Message = (ex.InnerException == null ? ex.Message : ex.InnerException.Message) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult NomineeCreate(InvestorDetailViewModel model)
        {
            // var entity = Mapper.Map<InvestorDetailViewModel, InvestorNominee>(model);
            var entity = new InvestorNominee();
            try
            {
                if (model.NomineePhotographMsg != null)
                {
                    byte[] data = new byte[model.NomineePhotographMsg.ContentLength];
                    model.NomineePhotographMsg.InputStream.Read(data, 0, model.NomineePhotographMsg.ContentLength);
                    entity.Photograph = data;
                }
                //else
                //{
                //    entity.Photograph = null;
                //}

                if (model.NomineeSignatureMsg != null)
                {
                    byte[] data = new byte[model.NomineeSignatureMsg.ContentLength];
                    model.NomineeSignatureMsg.InputStream.Read(data, 0, model.NomineeSignatureMsg.ContentLength);
                    entity.Signature = data;
                }
                //else
                //{
                //    entity.Signature = null;
                //}

                entity.NomineeName = model.NomineeName;
                entity.BirthCertificateNo = model.NomineeBirthCertificateNo;
                entity.CountryId = model.NomineeCountryId;
                entity.DateOfBirth = model.NomineeDateOfBirth;
                entity.DrivingLicenseNo = model.NomineeDrivingLicenseNo;
                entity.Email = model.NomineeEmail;
                entity.FatherName = model.NomineeFatherName;
                entity.Gender = model.NomineeGender;
                entity.HasPassport = model.NomineeHasPassport;
                entity.InvestorId = model.NomineeInvestorId;
                entity.IsMinor = model.IsMinor;
                entity.IsResident = model.NomineeIsResident;
                entity.Mobile = model.NomineeMobile;
                entity.MotherName = model.NomineeMotherName;
                entity.NationalId = model.NomineeNationalId;
                entity.OccupationId = model.NomineeOccupationId;
                entity.PassportExpireDate = model.NomineePassportExpireDate;
                entity.PassportIssueDate = model.NomineePassportIssueDate;
                entity.PassportIssuePlace = model.NomineePassportIssuePlace;
                entity.PassportNo = model.NomineePassportNo;
                entity.Percentage = model.Percentage;
                entity.PermanentAddress = model.NomineePermanentAddress;
                entity.PermanentThanaId = model.NomineePermanentThanaId;
                entity.Phone = model.NomineePhone;
                entity.PresentAddress = model.NomineePresentAddress;
                entity.PresentThanaId = model.NomineePresentThanaId;
                entity.RelationId = model.RelationId;


                entity.IsActive = true;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                entity.CreateDate = DateTime.Now;


                var NomineeId = investorNomineeService.Create(entity).Id;

                if (model.IsMinor == true)
                {
                    var entity2 = new InvestorNomineeGuardian();
                    entity2.Gender = model.GuardianGender;
                    entity2.NomineeId = NomineeId;
                    entity2.GuardianName = model.GuardianName;
                    entity2.BirthCertificateNo = model.GuardianBirthCertificateNo;
                    entity2.CountryId = (model.GuardianCountryId == 0 ? (int?)null : model.GuardianCountryId);
                    entity2.DateOfBirth = model.GuardianDateOfBirth;
                    entity2.DrivingLicenseNo = model.GuardianDrivingLicenseNo;
                    entity2.Email = model.GuardianEmail;
                    entity2.FatherName = model.GuardianFatherName;
                    entity2.Gender = model.GuardianGender;
                    entity2.HasPassport = model.GuardianHasPassport;
                    entity2.IsResident = model.GuardianIsResident;
                    entity2.Mobile = model.GuardianMobile;
                    entity2.MotherName = model.GuardianMotherName;
                    entity2.NationalId = model.GuardianNationalId;
                    entity2.OccupationId = (model.GuardianOccupationId == 0 ? (int?)null : model.GuardianOccupationId);
                    entity2.PassportExpireDate = model.GuardianPassportExpireDate;
                    entity2.PassportIssueDate = model.GuardianPassportIssueDate;
                    entity2.PassportIssuePlace = model.GuardianPassportIssuePlace;
                    entity2.PassportNo = model.GuardianPassportNo;
                    entity2.PermanentAddress = model.GuardianPermanentAddress;
                    entity2.PermanentThanaId = (model.GuardianPermanentThanaId == 0 ? (int?)null : model.GuardianPermanentThanaId);
                    entity2.Phone = model.GuardianPhone;
                    entity2.PresentAddress = model.GuardianPresentAddress;
                    entity2.PresentThanaId = (model.GuardianPresentThanaId == 0 ? (int?)null : model.GuardianPresentThanaId);


                    entity2.IsActive = true;
                    entity2.CreatedUserId = SessionHelper.LoggedInUserId;
                    entity2.CreateDate = DateTime.Now;


                    if (model.GuardianPhotographMsg != null)
                    {
                        byte[] data = new byte[model.GuardianPhotographMsg.ContentLength];
                        model.GuardianPhotographMsg.InputStream.Read(data, 0, model.GuardianPhotographMsg.ContentLength);
                        entity2.Photograph = data;
                    }
                    //else
                    //{
                    //    entity2.Photograph = null;
                    //}

                    if (model.GuardianSignatureMsg != null)
                    {
                        byte[] data = new byte[model.GuardianSignatureMsg.ContentLength];
                        model.GuardianSignatureMsg.InputStream.Read(data, 0, model.GuardianSignatureMsg.ContentLength);
                        entity2.Signature = data;
                    }
                    //else
                    //{
                    //    entity2.Signature = null;
                    //}

                    nomineeGuardianService.Create(entity2);
                }
                //nomineeGuardianService


                entity.InvestorId = model.NomineeInvestorId;

                return Json(entity, JsonRequestBehavior.AllowGet);
            }
            //catch (DbEntityValidationException e)
            //{
            //    var str = "";
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        str += "Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:";
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            str += "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
            //        }
            //    }
            //    return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            //}
            catch (Exception ex)
            {
                entity.InvestorId = model.NomineeInvestorId;
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult JoinHolder(InvestorDetailViewModel model)
        {
            // var entity = Mapper.Map<InvestorDetailViewModel, InvestorNominee>(model);
            var entity = new InvestorJointHolder();
            try
            {
                if (model.JoinPhotographMsg != null)
                {
                    byte[] data = new byte[model.JoinPhotographMsg.ContentLength];
                    model.JoinPhotographMsg.InputStream.Read(data, 0, model.JoinPhotographMsg.ContentLength);
                    entity.Photograph = data;
                }
                else
                {
                    entity.Photograph = null;
                }

                if (model.JoinSignatureMsg != null)
                {
                    byte[] data = new byte[model.JoinSignatureMsg.ContentLength];
                    model.JoinSignatureMsg.InputStream.Read(data, 0, model.JoinSignatureMsg.ContentLength);
                    entity.Signature = data;
                }
                else
                {
                    entity.Signature = null;
                }
                entity.InvestorId = model.JoinInvestorId;
                entity.JoinHolderName = model.JoinHolderName;
                entity.BirthCertificateNo = model.JoinBirthCertificateNo;
                entity.CountryId = model.JoinCountryId;
                entity.DateOfBirth = model.JoinDateOfBirth;
                entity.DrivingLicenseNo = model.JoinDrivingLicenseNo;
                entity.Email = model.JoinEmail;
                entity.EmergencyContactNo = model.JoinEmergencyContactNo;
                entity.FatherName = model.JoinFatherName;
                entity.Gender = model.JoinGender;
                entity.HasPassport = model.JoinHasPassport;
                // entity.InvestorId = model.NomineeInvestorId;
                entity.IsResident = model.JoinIsResident;
                entity.Mobile = model.JoinMobile;
                entity.MotherName = model.JoinMotherName;
                entity.NationalId = model.JoinNationalId;
                entity.OccupationId = model.JoinOccupationId;
                entity.PassportExpireDate = model.JoinPassportExpireDate;
                entity.PassportIssueDate = model.JoinPassportIssueDate;
                entity.PassportIssuePlace = model.JoinPassportIssuePlace;
                entity.PassportNo = model.JoinPassportNo;
                entity.PermanentAddress = model.JoinPermanentAddress;
                entity.PermanentThanaId = model.JoinPermanentThanaId;
                entity.Phone = model.JoinPhone;
                entity.PresentAddress = model.JoinPresentAddress;
                entity.PresentThanaId = model.JoinPresentThanaId;
                entity.Title = model.JoinTitle;
                entity.SMSMobile = model.JoinSMSMobile;
                entity.SystemEmail = model.JoinSystemEmail;




                entity.IsActive = true;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;
                entity.CreateDate = DateTime.Now;


                investorJoinHolderService.Create(entity);

                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            var detail = investorInfoService.GetById(id);
            var JoinHolder = investorJoinHolderService.GetByInvestorId(id);
            var Introducer = investorIntroducerService.GetByInvestorId(id);
            var Account = investorAccountService.GetByInvestorId(id);
            var model = new InvestorDetailViewModel();

            #region InvDetail

            if (detail.IsCompany != 1)
            {
                model.InvestorId = detail.Id;
                model.OccupationId = detail.OccupationId;
                model.CountryId = detail.CountryId;

                model.PresentThanaId = detail.PresentThanaId;
                model.PermanentThanaId = detail.PermanentThanaId;

                if (detail.PresentThanaId != null && detail.PresentThanaId != 0)
                {
                    var preDisId = string.Empty;
                    preDisId = detail.PresentThanaId.ToString();
                    var DistrictId = lookupThanaService.GetById(Convert.ToInt32(preDisId)).DistrictId;
                    model.PresentDistrictId = DistrictId;// lookupThanaService.GetById(Convert.ToInt32(preDisId)).DistrictId;

                    model.PresentDivisionId = lookupDistrictService.GetById(Convert.ToInt32(DistrictId)).DivisionId;
                }
                // PresentDistrictId      PresentDivisionId PermanentDivisionId PermanentDistrictId

                if (detail.PermanentThanaId != null && detail.PermanentThanaId != 0)
                {
                    var perDisId = string.Empty;
                    perDisId = detail.PermanentThanaId.ToString();
                    var PerDistrictId = lookupThanaService.GetById(Convert.ToInt32(perDisId)).DistrictId;
                    model.PermanentDistrictId = PerDistrictId;
                    model.PermanentDivisionId = lookupDistrictService.GetById(Convert.ToInt32(PerDistrictId)).DivisionId;

                }

                model.InvestorCode = detail.InvestorCode;
                model.Title = detail.Title;
                model.InvestorName = detail.InvestorName;
                model.FatherName = detail.FatherName;
                model.MotherName = detail.MotherName;
                model.DateOfBirth = detail.DateOfBirth;
                model.Gender = detail.Gender;
                model.PresentAddress = detail.PresentAddress;
                model.PermanentAddress = detail.PermanentAddress;
                model.IsResident = detail.IsResident;
                model.PhoneRes = detail.PhoneRes;
                model.PhoneOffice = detail.PhoneOffice;
                model.Mobile = detail.Mobile;
                model.SMSMobile = detail.SMSMobile;
                model.EmergencyContactNo = detail.EmergencyContactNo;
                model.Email = detail.Email;
                model.SystemEmail = detail.SystemEmail;
                model.Fax = detail.Fax;
                model.NationalId = detail.NationalId;
                model.ETIN = detail.ETIN;
                model.DrivingLicenseNo = detail.DrivingLicenseNo;
                model.BirthCertificateNo = detail.BirthCertificateNo;
                model.HasPassport = detail.HasPassport;
                model.PassportNo = detail.PassportNo;
                model.PassportIssueDate = detail.PassportIssueDate;
                model.PassportExpireDate = detail.PassportExpireDate;
                model.PassportIssuePlace = detail.PassportIssuePlace;
                model.SpecialInstruction = detail.SpecialInstruction;
                model.IsCompany = detail.IsCompany;
            }
            else
            {
                //    Company           

                model.ComFax = detail.Fax;
                model.ComSystemEmail = detail.SystemEmail;
                model.ComEmail = detail.Email;
                model.ComEmergencyContactNo = detail.EmergencyContactNo;
                model.ComSMSMobile = detail.SMSMobile;
                model.ComMobile = detail.Mobile;
                model.ComPhoneOffice = detail.PhoneOffice;
                model.ComPresentAddress = detail.PresentAddress;
                model.ComCountryId = detail.CountryId;
                model.ComPresentThanaId = detail.PresentThanaId;

                model.ComInvestorCode = detail.InvestorCode;
                model.ComInvestorName = detail.InvestorName;
                model.ComDateOfBirth = detail.DateOfBirth;
                model.IsCompany = detail.IsCompany;

                var ComCon = investorCompanyService.GetByInvestorId(detail.Id);
                model.ContactPerson = ComCon.ContactPerson;
                model.RegNo = ComCon.RegNo;
                model.RegisteredDate = ComCon.RegisteredDate;
            }


            #endregion

            ////            
            #region Account

            if (Account != null)
            {
                model.AccountId = Account.Id;
                model.AccInvestorId = Account.InvestorId;
                model.DPBranchId = Account.DPBranchId;
                model.BOTypeId = Account.BOTypeId;
                model.BOCategoryId = Account.BOCategoryId;
                if (Account.BrokerBranchId != null)
                {
                    var brokerbranId = Account.BrokerBranchId.ToString();
                    model.BrokerId = brokerBranchService.GetById(Convert.ToInt32(brokerbranId)).BrokerId;
                }

                model.BrokerBranchId = Account.BrokerBranchId;
                //model.BrokerId = brokerBranchService.GetById(Account.BrokerBranchId).BrokerId;
                model.InvestorTypeId = Account.InvestorTypeId;
                model.AccountTypeId = Account.AccountTypeId;
                model.SubAccountTypeId = Account.SubAccountTypeId;
                model.BankBranchId = Account.BankBranchId;
                if (Account.BankBranchId != null)
                {
                    var bbranId = Account.BankBranchId.ToString();
                    model.BankId = lookupBankBranchService.GetById(Convert.ToInt32(bbranId)).BankId;
                }

                model.StatusId = Account.StatusId;
                model.OperationTypeId = Account.OperationTypeId;
                model.BOID = Account.BOID;
                model.BOAccountOpeningDate = Account.BOAccountOpeningDate;
                model.TraderIdDSE = Account.TraderIdDSE;
                model.TraderIdCSE = Account.TraderIdCSE;
                model.BankAccountNo = Account.BankAccountNo;
                model.OmnibusInvestorCode = Account.OmnibusInvestorCode;
                model.ComissionRate = Account.ComissionRate;
                model.CounterPartyRate = Account.CounterPartyRate;
                model.CounterPartyRate = Account.CounterPartyRate;
                model.LoanRatio = Account.LoanRatio;
                model.InterestRate = Account.InterestRate;
                model.MaxLoan = Account.MaxLoan;
                model.IsSMSTrade = Account.IsSMSService;
                model.IsMailPortfolio = Account.IsMailService;
                model.IsLinkBO = Account.IsLinkBO;
                model.LinkBOAccount = Account.LinkBOAccount;
                model.IsEDC = Account.IsEDC;
                model.IRN = Account.IRN;


            }
            else
            {
                model.AccInvestorId = detail.Id;
            }
            #endregion

            #region JoinHolder

            if (JoinHolder != null)
            {
                model.JoinHolderId = JoinHolder.Id;
                model.JoinInvestorId = JoinHolder.InvestorId;
                model.JoinOccupationId = JoinHolder.OccupationId;
                model.JoinCountryId = JoinHolder.CountryId;
                model.JoinPresentThanaId = JoinHolder.PresentThanaId;
                model.JoinPermanentThanaId = JoinHolder.PermanentThanaId;

                if (JoinHolder.PresentThanaId != null)
                {
                    var preDisId = string.Empty;
                    preDisId = detail.PresentThanaId.ToString();
                    var DistrictId = lookupThanaService.GetById(Convert.ToInt32(preDisId)).DistrictId;
                    model.JoinPresentDistrictId = DistrictId;// lookupThanaService.GetById(Convert.ToInt32(preDisId)).DistrictId;

                    model.JoinPresentDivisionId = lookupDistrictService.GetById(Convert.ToInt32(DistrictId)).DivisionId;
                }
                // PresentDistrictId      PresentDivisionId PermanentDivisionId PermanentDistrictId

                if (JoinHolder.PermanentThanaId != null)
                {
                    var perDisId = string.Empty;
                    perDisId = detail.PermanentThanaId.ToString();
                    var PerDistrictId = lookupThanaService.GetById(Convert.ToInt32(perDisId)).DistrictId;
                    model.JoinPermanentDistrictId = PerDistrictId;
                    model.JoinPermanentDivisionId = lookupDistrictService.GetById(Convert.ToInt32(PerDistrictId)).DivisionId;

                }

                model.JoinTitle = JoinHolder.Title;
                model.JoinHolderName = JoinHolder.JoinHolderName;
                model.JoinFatherName = JoinHolder.FatherName;
                model.JoinMotherName = JoinHolder.MotherName;
                model.JoinDateOfBirth = JoinHolder.DateOfBirth;
                model.JoinGender = JoinHolder.Gender;
                model.JoinPresentAddress = JoinHolder.PresentAddress;
                model.JoinPermanentAddress = JoinHolder.PermanentAddress;

                model.JoinIsResident = JoinHolder.IsResident;
                model.JoinPhone = JoinHolder.Phone;
                model.JoinMobile = JoinHolder.Mobile;
                model.JoinSMSMobile = JoinHolder.SMSMobile;
                model.JoinEmergencyContactNo = JoinHolder.EmergencyContactNo;
                model.JoinEmail = JoinHolder.Email;
                model.JoinSystemEmail = JoinHolder.SystemEmail;
                model.JoinNationalId = JoinHolder.NationalId;
                model.JoinDrivingLicenseNo = JoinHolder.DrivingLicenseNo;
                model.JoinBirthCertificateNo = JoinHolder.BirthCertificateNo;
                model.JoinHasPassport = JoinHolder.HasPassport;
                model.JoinPassportNo = JoinHolder.PassportNo;
                model.JoinPassportIssueDate = JoinHolder.PassportIssueDate;
                model.JoinPassportExpireDate = JoinHolder.PassportExpireDate;
                model.JoinPassportIssuePlace = JoinHolder.PassportIssuePlace;
            }
            else
            {
                model.JoinInvestorId = detail.Id;
            }
            #endregion


            #region Introducer
            if (Introducer != null)
            {
                model.IntroducerId = Introducer.Id;
                model.IntroInvestorId = Introducer.InvestorId;
                model.IntroEmployeeId = Introducer.EmployeeId;
                model.IntroducerInvestorId = Introducer.IntroducerInvestorId;

                if (Introducer.InvestorId == null)
                {
                    model.IntroducerType = "Emp";
                }
                else
                {
                    model.IntroducerType = "Ivn";
                }


                var InvIntroID = string.Empty;
                var EmployeeID = string.Empty;

                if (Introducer.IntroducerInvestorId != null)
                {
                    InvIntroID = Introducer.IntroducerInvestorId.ToString(); ;
                    model.IntroducerInvestorName = investorInfoService.GetById(Convert.ToInt32(InvIntroID)).InvestorName;
                    model.IntroducerContactNo = investorInfoService.GetById(Convert.ToInt32(InvIntroID)).Mobile;
                    model.IntroducerCode = investorInfoService.GetById(Convert.ToInt32(InvIntroID)).InvestorCode;
                }
                if (Introducer.EmployeeId != null)
                {
                    EmployeeID = Introducer.EmployeeId.ToString();
                    model.IntroducerInvestorName = brokerEmployeeService.GetById(Convert.ToInt32(EmployeeID)).EmployeeName;
                    model.IntroducerContactNo = brokerEmployeeService.GetById(Convert.ToInt32(EmployeeID)).PhoneNo;
                    model.IntroducerCode = brokerEmployeeService.GetById(Convert.ToInt32(EmployeeID)).EmployeeCode.ToString();
                }
            }
            else
            {
                model.IntroducerId = 0;
                model.IntroducerType = "Ivn";
                model.IntroInvestorId = id;

            }
            #endregion


            #region Dropdown

            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.PerDivisionList = lookupDivisionService.GetAll().ToList();
            ViewBag.OccupationList = lookupOccupationService.GetAll().ToList();
            ViewBag.CountryList = lookupCountryService.GetAll().ToList();

            ViewBag.DPBranchlist = brokerDPBranchService.GetAll().ToList();
            ViewBag.BOTypelist = investorBOTypeService.GetAll().ToList();
            ViewBag.BOCategorylist = investorBOCategoryService.GetAll().ToList();
            ViewBag.BrokerList = brokerInfoService.GetAll().ToList();
            ViewBag.InvestorTypeList = investorTypeService.GetAll().ToList();
            ViewBag.AccountTypelist = investorAccountTypeService.GetAll().ToList();
            ViewBag.Banklist = lookupBankService.GetAll().ToList();
            ViewBag.InvestorStatuslist = investorStatusService.GetAll().ToList();
            ViewBag.InvestorOperationlist = investorOperationService.GetAll().ToList();
            ViewBag.RelationList = lookupRelationService.GetAll().ToList();


            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            ViewData["PerDistrictlist"] = items;
            ViewData["PerThanalist"] = items;
            ViewData["BrokerBranchlist"] = items;
            ViewData["SubAccountTypelist"] = items;
            ViewData["BankBranchlist"] = items;
            ViewData["BOIDlist"] = items;
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult InvestorDetailEdit(InvestorDetailViewModel model)
        {
            try
            {
                var Detail = investorInfoService.GetById(model.InvestorId);
                if (Detail.IsCompany != 1)
                {
                    Detail.BirthCertificateNo = model.BirthCertificateNo;
                    Detail.CountryId = model.CountryId;
                    Detail.DateOfBirth = model.DateOfBirth;
                    Detail.DrivingLicenseNo = model.DrivingLicenseNo;
                    Detail.Email = model.Email;
                    Detail.EmergencyContactNo = model.EmergencyContactNo;
                    Detail.ETIN = model.ETIN;
                    Detail.FatherName = model.FatherName;
                    Detail.Fax = model.Fax;
                    Detail.Gender = model.Gender;
                    Detail.HasPassport = model.HasPassport;
                    Detail.InvestorName = model.InvestorName;
                    Detail.IsResident = model.IsResident;
                    Detail.Mobile = model.Mobile;
                    Detail.MotherName = model.MotherName;
                    Detail.NationalId = model.NationalId;
                    Detail.OccupationId = model.OccupationId;
                    Detail.PassportExpireDate = model.PassportExpireDate;
                    Detail.PassportIssueDate = model.PassportIssueDate;
                    Detail.PassportIssuePlace = model.PassportIssuePlace;
                    Detail.PassportNo = model.PassportNo;
                    Detail.PermanentAddress = model.PermanentAddress;
                    Detail.PermanentThanaId = model.PermanentThanaId;
                    Detail.PhoneOffice = model.PhoneOffice;
                    Detail.PhoneRes = model.PhoneRes;
                    Detail.PresentAddress = model.PresentAddress;
                    Detail.PresentThanaId = model.PresentThanaId;
                    Detail.SMSMobile = model.SMSMobile;
                    Detail.SpecialInstruction = model.SpecialInstruction;
                    Detail.SystemEmail = model.SystemEmail;
                    Detail.Title = model.Title;
                    Detail.UpdateDate = DateTime.Now;
                    Detail.UpdateUserId = SessionHelper.LoggedInUserId;

                    if (model.PhotographMsg != null)
                    {
                        byte[] data = new byte[model.PhotographMsg.ContentLength];
                        model.PhotographMsg.InputStream.Read(data, 0, model.PhotographMsg.ContentLength);
                        Detail.Photograph = data;
                    }

                    if (model.SignatureMsg != null)
                    {
                        byte[] data = new byte[model.SignatureMsg.ContentLength];
                        model.SignatureMsg.InputStream.Read(data, 0, model.SignatureMsg.ContentLength);
                        Detail.Signature = data;
                    }

                    investorInfoService.Update(Detail);
                }
                else
                {
                    Detail.CountryId = model.ComCountryId;
                    Detail.DateOfBirth = model.ComDateOfBirth;
                    Detail.Email = model.ComEmail;
                    Detail.EmergencyContactNo = model.ComEmergencyContactNo;
                    Detail.Fax = model.ComFax;
                    Detail.InvestorCode = model.ComInvestorCode;
                    Detail.InvestorName = model.ComInvestorName;
                    Detail.Mobile = model.ComMobile;
                    Detail.PhoneOffice = model.ComPhoneOffice;
                    Detail.PresentAddress = model.ComPresentAddress;
                    Detail.PresentThanaId = model.ComPresentThanaId;
                    Detail.SMSMobile = model.ComSMSMobile;
                    Detail.SystemEmail = model.ComSystemEmail;
                    Detail.IsActive = true;
                    Detail.UpdateUserId = SessionHelper.LoggedInUserId;
                    Detail.UpdateDate = DateTime.Now;
                    Detail.IsCompany = 1;

                    investorInfoService.Update(Detail);
                    // var InvestorId = investorInfoService.Create(Detail).Id;


                    var ComCon = investorCompanyService.GetByInvestorId(Detail.Id);
                    ComCon.ContactPerson = model.ContactPerson;
                    ComCon.RegNo = model.RegNo;
                    ComCon.RegisteredDate = model.RegisteredDate;
                    ComCon.IsActive = true;
                    ComCon.UpdateUserId = SessionHelper.LoggedInUserId;
                    ComCon.UpdateDate = DateTime.Now;

                    if (model.ComPhotographMsg != null)
                    {
                        byte[] data = new byte[model.ComPhotographMsg.ContentLength];
                        model.ComPhotographMsg.InputStream.Read(data, 0, model.ComPhotographMsg.ContentLength);
                        ComCon.Photograph = data;
                    }
                    else
                    {
                        ComCon.Photograph = null;
                    }

                    if (model.ComSignatureMsg != null)
                    {
                        byte[] data = new byte[model.ComSignatureMsg.ContentLength];
                        model.ComSignatureMsg.InputStream.Read(data, 0, model.ComSignatureMsg.ContentLength);
                        ComCon.Signature = data;
                    }
                    else
                    {
                        ComCon.Signature = null;
                    }

                    investorCompanyService.Update(ComCon);
                }
                return Json(new { Result = "Success", Message = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


         [HttpPost]
        public ActionResult InvestorAccountEdit(InvestorDetailViewModel model)
        {

            try
            {
                if (model.AccountId != null && model.AccountId != 0)
                {
                    var Acc = investorAccountService.GetById(model.AccountId);

                    Acc.AccountTypeId = model.AccountTypeId;
                    Acc.BankAccountNo = model.BankAccountNo;
                    Acc.BankBranchId = model.BankBranchId;
                    Acc.BOAccountOpeningDate = model.BOAccountOpeningDate;
                    Acc.BOCategoryId = model.BOCategoryId;
                    Acc.BOID = model.BOID;
                    Acc.BOTypeId = model.BOTypeId;
                    Acc.BrokerBranchId = model.BrokerBranchId;
                    Acc.ComissionRate = model.ComissionRate;
                    Acc.CounterPartyRate = model.CounterPartyRate;
                    Acc.DPBranchId = model.DPBranchId;
                    Acc.InterestRate = model.InterestRate;
                    Acc.InvestorTypeId = model.InvestorTypeId;
                    Acc.IRN = model.IRN;
                    Acc.IsEDC = model.IsEDC;
                    Acc.IsLinkBO = model.IsLinkBO;
                    Acc.IsMailService = model.IsMailPortfolio;
                    Acc.IsSMSService = model.IsSMSTrade;
                    Acc.LinkBOAccount = model.LinkBOAccount;
                    Acc.LoanRatio = model.LoanRatio;
                    Acc.MaxLoan = model.MaxLoan;
                    Acc.OmnibusInvestorCode = model.OmnibusInvestorCode;
                    Acc.OperationTypeId = model.OperationTypeId;
                    Acc.StatusId = model.StatusId;
                    Acc.SubAccountTypeId = model.SubAccountTypeId;
                    Acc.TraderIdCSE = model.TraderIdCSE;
                    Acc.TraderIdDSE = model.TraderIdDSE;
                    Acc.UpdateDate = DateTime.Now;
                    Acc.UpdateUserId = SessionHelper.LoggedInUserId;
                    Acc.OfficeID = SessionHelper.LoggedInOfficeId;
                    investorAccountService.Update(Acc);
                }
                else
                {
                    var Accnt = new InvestorAccount();

                    Accnt.InvestorId = model.AccInvestorId;
                    Accnt.AccountTypeId = model.AccountTypeId;
                    Accnt.BankAccountNo = model.BankAccountNo;
                    Accnt.BankBranchId = model.BankBranchId;
                    Accnt.BOAccountOpeningDate = model.BOAccountOpeningDate;
                    Accnt.BOCategoryId = model.BOCategoryId;
                    Accnt.BOID = model.BOID;
                    Accnt.BOTypeId = model.BOTypeId;
                    Accnt.BrokerBranchId = model.BrokerBranchId;
                    Accnt.ComissionRate = model.ComissionRate;
                    Accnt.CounterPartyRate = model.CounterPartyRate;
                    Accnt.DPBranchId = model.DPBranchId;
                    Accnt.InterestRate = model.InterestRate;
                    Accnt.InvestorTypeId = model.InvestorTypeId;
                    Accnt.IRN = model.IRN;
                    Accnt.IsEDC = model.IsEDC;
                    Accnt.IsLinkBO = model.IsLinkBO;
                    Accnt.IsMailService = model.IsMailPortfolio;
                    Accnt.IsSMSService = model.IsSMSTrade;
                    Accnt.LinkBOAccount = model.LinkBOAccount;
                    Accnt.LoanRatio = model.LoanRatio;
                    Accnt.MaxLoan = model.MaxLoan;
                    Accnt.OmnibusInvestorCode = model.OmnibusInvestorCode;
                    Accnt.OperationTypeId = model.OperationTypeId;
                    Accnt.StatusId = model.StatusId;
                    Accnt.SubAccountTypeId = model.SubAccountTypeId;
                    Accnt.TraderIdCSE = model.TraderIdCSE;
                    Accnt.TraderIdDSE = model.TraderIdDSE;
                    Accnt.CreateDate = DateTime.Now;
                    Accnt.CreatedUserId = SessionHelper.LoggedInUserId;
                    Accnt.IsActive = true;
                    Accnt.OfficeID = SessionHelper.LoggedInOfficeId;
                    investorAccountService.Create(Accnt);


                }
                return Json(new { Result = "Success", Message = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


         [HttpPost]
        public ActionResult InvestorJoinHolderEdit(InvestorDetailViewModel model)
        {
             try
             {
                 if (model.JoinHolderId != 0)
                 {
                     var Join = investorJoinHolderService.GetById(model.JoinHolderId);

                     Join.BirthCertificateNo = model.BirthCertificateNo;
                     Join.CountryId = model.JoinCountryId;
                     Join.CreateDate = DateTime.Now;
                     Join.CreatedUserId = SessionHelper.LoggedInUserId;
                     Join.DateOfBirth = model.JoinDateOfBirth;
                     Join.DrivingLicenseNo = model.JoinDrivingLicenseNo;
                     Join.Email = model.JoinEmail;
                     Join.EmergencyContactNo = model.JoinEmergencyContactNo;
                     Join.FatherName = model.JoinFatherName;
                     Join.Gender = model.JoinGender;
                     Join.HasPassport = model.JoinHasPassport;
                     Join.IsActive = true;
                     Join.IsResident = model.JoinIsResident;
                     Join.JoinHolderName = model.JoinHolderName;
                     Join.Mobile = model.JoinMobile;
                     Join.MotherName = model.JoinMotherName;
                     Join.NationalId = model.JoinNationalId;
                     Join.OccupationId = model.JoinOccupationId;
                     Join.PassportExpireDate = model.JoinPassportExpireDate;
                     Join.PassportIssueDate = model.JoinPassportIssueDate;
                     Join.PassportIssuePlace = model.JoinPassportIssuePlace;
                     Join.PassportNo = model.JoinPassportNo;
                     Join.PermanentAddress = model.JoinPermanentAddress;
                     Join.PermanentThanaId = model.JoinPermanentThanaId;
                     Join.Phone = model.JoinPhone;
                     Join.PresentThanaId = model.JoinPresentThanaId;
                     Join.SMSMobile = model.JoinSMSMobile;
                     Join.SystemEmail = model.JoinSystemEmail;
                     Join.Title = model.JoinTitle;
                     Join.UpdateDate = DateTime.Now;
                     Join.UpdateUserId = SessionHelper.LoggedInUserId;

                     if (model.JoinPhotographMsg != null)
                     {
                         byte[] data = new byte[model.JoinPhotographMsg.ContentLength];
                         model.JoinPhotographMsg.InputStream.Read(data, 0, model.JoinPhotographMsg.ContentLength);
                         Join.Photograph = data;
                     }
                     //else
                     //{
                     //    Join.Photograph = null;
                     //}

                     if (model.JoinSignatureMsg != null)
                     {
                         byte[] data = new byte[model.JoinSignatureMsg.ContentLength];
                         model.JoinSignatureMsg.InputStream.Read(data, 0, model.JoinSignatureMsg.ContentLength);
                         Join.Signature = data;
                     }
                     //else
                     //{
                     //    Join.Signature = null;
                     //}

                     investorJoinHolderService.Update(Join);
                 }
                 else
                 {
                     var JoinInv = new InvestorJointHolder();

                     JoinInv.InvestorId = model.JoinInvestorId;
                     JoinInv.BirthCertificateNo = model.BirthCertificateNo;
                     JoinInv.CountryId = model.JoinCountryId;
                     JoinInv.CreateDate = DateTime.Now;
                     JoinInv.CreatedUserId = SessionHelper.LoggedInUserId;
                     JoinInv.DateOfBirth = model.JoinDateOfBirth;
                     JoinInv.DrivingLicenseNo = model.JoinDrivingLicenseNo;
                     JoinInv.Email = model.JoinEmail;
                     JoinInv.EmergencyContactNo = model.JoinEmergencyContactNo;
                     JoinInv.FatherName = model.JoinFatherName;
                     JoinInv.Gender = model.JoinGender;
                     JoinInv.HasPassport = model.JoinHasPassport;
                     JoinInv.IsActive = true;
                     JoinInv.IsResident = model.JoinIsResident;
                     JoinInv.JoinHolderName = model.JoinHolderName;
                     JoinInv.Mobile = model.JoinMobile;
                     JoinInv.MotherName = model.JoinMotherName;
                     JoinInv.NationalId = model.JoinNationalId;
                     JoinInv.OccupationId = model.JoinOccupationId;
                     JoinInv.PassportExpireDate = model.JoinPassportExpireDate;
                     JoinInv.PassportIssueDate = model.JoinPassportIssueDate;
                     JoinInv.PassportIssuePlace = model.JoinPassportIssuePlace;
                     JoinInv.PassportNo = model.JoinPassportNo;
                     JoinInv.PermanentAddress = model.JoinPermanentAddress;
                     JoinInv.PermanentThanaId = model.JoinPermanentThanaId;
                     JoinInv.Phone = model.JoinPhone;
                     JoinInv.PresentThanaId = model.JoinPresentThanaId;
                     JoinInv.SMSMobile = model.JoinSMSMobile;
                     JoinInv.SystemEmail = model.JoinSystemEmail;
                     JoinInv.Title = model.JoinTitle;
                     //JoinInv.UpdateDate = DateTime.Now;
                     //JoinInv.UpdateUserId = SessionHelper.LoggedInUserId;

                     if (model.JoinPhotographMsg != null)
                     {
                         byte[] data = new byte[model.JoinPhotographMsg.ContentLength];
                         model.JoinPhotographMsg.InputStream.Read(data, 0, model.JoinPhotographMsg.ContentLength);
                         JoinInv.Photograph = data;
                     }
                     else
                     {
                         JoinInv.Photograph = null;
                     }

                     if (model.JoinSignatureMsg != null)
                     {
                         byte[] data = new byte[model.JoinSignatureMsg.ContentLength];
                         model.JoinSignatureMsg.InputStream.Read(data, 0, model.JoinSignatureMsg.ContentLength);
                         JoinInv.Signature = data;
                     }
                     else
                     {
                         JoinInv.Signature = null;
                     }

                     investorJoinHolderService.Create(JoinInv);

                 }
                 return Json(new { Result = "Success", Message = "Ok" }, JsonRequestBehavior.AllowGet);
             }
             catch (Exception ex)
             {
                 return Json(new { Result = "Fail", Message = ex.Message }, JsonRequestBehavior.AllowGet);
             }
 
        }

         //[HttpPost]
         //public ActionResult NomineeEdit(InvestorDetailViewModel model)
         //{
         //    // var entity = Mapper.Map<InvestorDetailViewModel, InvestorNominee>(model);
         //    var entity = new InvestorNominee();
         //    try
         //    {
         //        if (model.NomineePhotographMsg != null)
         //        {
         //            byte[] data = new byte[model.NomineePhotographMsg.ContentLength];
         //            model.NomineePhotographMsg.InputStream.Read(data, 0, model.NomineePhotographMsg.ContentLength);
         //            entity.Photograph = data;
         //        }
         //        else
         //        {
         //            entity.Photograph = null;
         //        }

         //        if (model.NomineeSignatureMsg != null)
         //        {
         //            byte[] data = new byte[model.NomineeSignatureMsg.ContentLength];
         //            model.NomineeSignatureMsg.InputStream.Read(data, 0, model.NomineeSignatureMsg.ContentLength);
         //            entity.Signature = data;
         //        }
         //        else
         //        {
         //            entity.Signature = null;
         //        }

         //        entity.NomineeName = model.NomineeName;
         //        entity.BirthCertificateNo = model.NomineeBirthCertificateNo;
         //        entity.CountryId = model.NomineeCountryId;
         //        entity.DateOfBirth = model.NomineeDateOfBirth;
         //        entity.DrivingLicenseNo = model.NomineeDrivingLicenseNo;
         //        entity.Email = model.NomineeEmail;
         //        entity.FatherName = model.NomineeFatherName;
         //        entity.Gender = model.NomineeGender;
         //        entity.HasPassport = model.NomineeHasPassport;
         //        entity.InvestorId = model.NomineeInvestorId;
         //        entity.IsMinor = model.IsMinor;
         //        entity.IsResident = model.NomineeIsResident;
         //        entity.Mobile = model.NomineeMobile;
         //        entity.MotherName = model.NomineeMotherName;
         //        entity.NationalId = model.NomineeNationalId;
         //        entity.OccupationId = model.NomineeOccupationId;
         //        entity.PassportExpireDate = model.NomineePassportExpireDate;
         //        entity.PassportIssueDate = model.NomineePassportIssueDate;
         //        entity.PassportIssuePlace = model.NomineePassportIssuePlace;
         //        entity.PassportNo = model.NomineePassportNo;
         //        entity.Percentage = model.Percentage;
         //        entity.PermanentAddress = model.NomineePermanentAddress;
         //        entity.PermanentThanaId = model.NomineePermanentThanaId;
         //        entity.Phone = model.NomineePhone;
         //        entity.PresentAddress = model.NomineePresentAddress;
         //        entity.PresentThanaId = model.NomineePresentThanaId;
         //        entity.RelationId = model.RelationId;


         //        entity.IsActive = true;
         //        entity.CreatedUserId = SessionHelper.LoggedInUserId;
         //        entity.CreateDate = DateTime.Now;


         //        var NomineeId = investorNomineeService.Create(entity).Id;

         //        if (model.IsMinor == true)
         //        {
         //            var entity2 = new InvestorNomineeGuardian();
         //            entity2.Gender = model.GuardianGender;
         //            entity2.NomineeId = NomineeId;
         //            entity2.GuardianName = model.GuardianName;
         //            entity2.BirthCertificateNo = model.GuardianBirthCertificateNo;
         //            entity2.CountryId = (model.GuardianCountryId == 0 ? (int?)null : model.GuardianCountryId);
         //            entity2.DateOfBirth = model.GuardianDateOfBirth;
         //            entity2.DrivingLicenseNo = model.GuardianDrivingLicenseNo;
         //            entity2.Email = model.GuardianEmail;
         //            entity2.FatherName = model.GuardianFatherName;
         //            entity2.Gender = model.GuardianGender;
         //            entity2.HasPassport = model.GuardianHasPassport;
         //            entity2.IsResident = model.GuardianIsResident;
         //            entity2.Mobile = model.GuardianMobile;
         //            entity2.MotherName = model.GuardianMotherName;
         //            entity2.NationalId = model.GuardianNationalId;
         //            entity2.OccupationId = (model.GuardianOccupationId == 0 ? (int?)null : model.GuardianOccupationId);
         //            entity2.PassportExpireDate = model.GuardianPassportExpireDate;
         //            entity2.PassportIssueDate = model.GuardianPassportIssueDate;
         //            entity2.PassportIssuePlace = model.GuardianPassportIssuePlace;
         //            entity2.PassportNo = model.GuardianPassportNo;
         //            entity2.PermanentAddress = model.GuardianPermanentAddress;
         //            entity2.PermanentThanaId = (model.GuardianPermanentThanaId == 0 ? (int?)null : model.GuardianPermanentThanaId);
         //            entity2.Phone = model.GuardianPhone;
         //            entity2.PresentAddress = model.GuardianPresentAddress;
         //            entity2.PresentThanaId = (model.GuardianPresentThanaId == 0 ? (int?)null : model.GuardianPresentThanaId);


         //            entity2.IsActive = true;
         //            entity2.CreatedUserId = SessionHelper.LoggedInUserId;
         //            entity2.CreateDate = DateTime.Now;


         //            if (model.GuardianPhotographMsg != null)
         //            {
         //                byte[] data = new byte[model.GuardianPhotographMsg.ContentLength];
         //                model.GuardianPhotographMsg.InputStream.Read(data, 0, model.GuardianPhotographMsg.ContentLength);
         //                entity2.Photograph = data;
         //            }
         //            else
         //            {
         //                entity2.Photograph = null;
         //            }

         //            if (model.GuardianSignatureMsg != null)
         //            {
         //                byte[] data = new byte[model.GuardianSignatureMsg.ContentLength];
         //                model.GuardianSignatureMsg.InputStream.Read(data, 0, model.GuardianSignatureMsg.ContentLength);
         //                entity2.Signature = data;
         //            }
         //            else
         //            {
         //                entity2.Signature = null;
         //            }

         //            nomineeGuardianService.Create(entity2);
         //        }
         //        //nomineeGuardianService


         //        entity.InvestorId = model.NomineeInvestorId;

         //        return Json(entity, JsonRequestBehavior.AllowGet);
         //    }
             
         //    catch (Exception ex)
         //    {
         //        entity.InvestorId = model.NomineeInvestorId;
         //        return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
         //    }
         //}

        public ActionResult InvestorWiseChargeSetup(string code, int? cid)
        {
            var charge =
                sPService.GetDataBySqlCommand(
                    "SELECT DISTINCT T.Id,T.TransactionTypeName FROM TradeTransactionCharge C INNER JOIN TradeTransactionType T ON T.Id=C.TransactionTypeId")
                    .Tables[0].AsEnumerable()
                    .Select(
                        x => new TradeTransactionType() { Id = x.Field<int>(0), TransactionTypeName = x.Field<string>(1) })
                    .ToList();
            ViewBag.Charges = charge;
            ViewBag.InvestorCharge = null;
            ViewBag.ChargeSlab = null;
            if (code == null || cid == null) return View();
            var allCharges = sPService.GetDataWithParameter(new { SEARCH_STRING = "", ROW_NO = 100, PAGE_NO = 1, CODE = code }, "SP_GET_INVESTOR_CHARGE_DETAILS").Tables[0].AsEnumerable().Select(x => new InvestorCharge()
            {
                InvestorId = x.Field<int>(0),
                ChargeId = x.Field<int>(1),
                InvestorCode = x.Field<string>(2),
                InvestorName = x.Field<string>(3),
                ChargeName = x.Field<string>(4),
                ChargeAmount = x.Field<decimal>(5),
                MinimumCharge = x.Field<decimal>(6),
                IsPercentage = x.Field<bool>(7),
                IsSlab = x.Field<bool>(8),
                TotalRecord = x.Field<int>(9)
            }).ToList();
            var investorCharge = allCharges.FirstOrDefault(x => x.ChargeId == cid);
            ViewBag.InvestorCharge = investorCharge;
            ViewBag.AllCharges = allCharges;
            if (!investorCharge.IsSlab) return View();
            var chargeSlab = investorWiseTransactionChargeSlabService.GetAll()
                .Where(
                    x =>
                        x.InvestorId == investorCharge.InvestorId &&
                        x.TransactionTypeId == investorCharge.ChargeId)
                .ToList();
            ViewBag.ChargeSlab = chargeSlab;
            return View();
        }

        public ActionResult ChargeList()
        {
            var charge = sPService.GetDataWithParameter(new { SEARCH_STRING = "", ROW_NO = 10, PAGE_NO = 1 }, "SP_GET_INVESTOR_CHARGE_DETAILS").Tables[0].AsEnumerable().Select(x => new InvestorCharge()
            {
                InvestorId = x.Field<int>(0),
                ChargeId = x.Field<int>(1),
                InvestorCode = x.Field<string>(2),
                InvestorName = x.Field<string>(3),
                ChargeName = x.Field<string>(4),
                ChargeAmount = x.Field<decimal>(5),
                MinimumCharge = x.Field<decimal>(6),
                IsPercentage = x.Field<bool>(7),
                IsSlab = x.Field<bool>(8),
                TotalRecord = x.Field<int>(9)
            }).ToList();
            ViewBag.Charges = charge;
            return View();
        }

        public ActionResult ChargeSetup()
        {
            var accountType = investorAccountTypeService.GetAll().ToList();
            var markets = marketInfoService.GetAll().ToList();
            var charge =
                sPService.GetDataBySqlCommand(
                    "SELECT DISTINCT T.Id,T.TransactionTypeName FROM TradeTransactionCharge C INNER JOIN TradeTransactionType T ON T.Id=C.TransactionTypeId")
                    .Tables[0].AsEnumerable()
                    .Select(
                        x => new TradeTransactionType() { Id = x.Field<int>(0), TransactionTypeName = x.Field<string>(1) })
                    .ToList();
            ViewBag.Charges = charge;
            ViewBag.Markets = markets;
            ViewBag.AccountTypes = accountType;
            var allCharges = tradeTransactionChargeService.GetAll().ToList();
            foreach (var transactionCharge in allCharges)
            {
                transactionCharge.TransactionTypeName =
                    charge.FirstOrDefault(x => x.Id == transactionCharge.TransactionTypeId).TransactionTypeName;
            }
            ViewBag.AllCharges = allCharges;
            return View();
        }

        #endregion


    }
}
