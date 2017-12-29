using System;
using System.Web;

namespace ERP.Web.ViewModels
{
    public class InvestorDetailViewModel
    {


        public string EditOpenMode { get; set; }

        #region Investor Detail
        public int Id { get; set; }
        public int JoinInvestorId { get; set; }
        public long RowSl { get; set; }
        public int IntroInvestorId { get; set; }
        public int InvestorId { get; set; }
        public int AccInvestorId { get; set; }
        public int? OccupationId { get; set; }
        public int? CountryId { get; set; }
        public int? PresentThanaId { get; set; }
        public int? PermanentThanaId { get; set; }
        public string InvestorCode { get; set; }
        public string Title { get; set; }
        public string InvestorName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int? IsCompany { get; set; }
        public bool IsResident { get; set; }
        public string PhoneRes { get; set; }
        public string PhoneOffice { get; set; }
        public string Mobile { get; set; }
        public string SMSMobile { get; set; }
        public string EmergencyContactNo { get; set; }
        public string Email { get; set; }
        public string SystemEmail { get; set; }
        public string Fax { get; set; }
        public string NationalId { get; set; }
        public string ETIN { get; set; }
        public string DrivingLicenseNo { get; set; }
        public string BirthCertificateNo { get; set; }
        public bool HasPassport { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpireDate { get; set; }
        public string PassportIssuePlace { get; set; }
        public string SpecialInstruction { get; set; }
        public byte[] Photograph { get; set; }
        public byte[] Signature { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }


        public string StatusName { get; set; }

        public HttpPostedFileBase PhotographMsg { get; set; }
        public HttpPostedFileBase SignatureMsg { get; set; }

        #endregion


        #region Investor Account
        public int AccountId { get; set; }
        public int AccountInvestorId { get; set; }
        public int? DPBranchId { get; set; }
        public int BOTypeId { get; set; }
        public int BOCategoryId { get; set; }
        public int? BrokerBranchId { get; set; }
        public int BrokerId { get; set; }
        public int? InvestorTypeId { get; set; }
        public int? AccountTypeId { get; set; }
        public int? SubAccountTypeId { get; set; }
        public int? BankBranchId { get; set; }
        public int? BankId { get; set; }
        //
        //AccountId AccInvestorId DPBranchId BOTypeId BOCategoryId  BrokerBranchId InvestorTypeId AccountTypeId SubAccountTypeId  BankBranchId 
        public int StatusId { get; set; }
        public int? OperationTypeId { get; set; }
        public string BOID { get; set; }
        public DateTime? BOAccountOpeningDate { get; set; }
        public string TraderIdDSE { get; set; }
        public string TraderIdCSE { get; set; }
        public string BankAccountNo { get; set; }
        public string OmnibusInvestorCode { get; set; }
        public decimal ComissionRate { get; set; }
        //StatusId OperationTypeId BOID  BOAccountOpeningDate  TraderIdDSE  TraderIdCSE BankAccountNo  OmnibusInvestorCode ComissionRate
        public decimal? CounterPartyRate { get; set; }
        public decimal? LoanRatio { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? MaxLoan { get; set; }
        public bool IsSMSTrade { get; set; }
        public bool IsMailPortfolio { get; set; }
        public bool IsLinkBO { get; set; }
        public string LinkBOAccount { get; set; }
        public bool IsEDC { get; set; }
        public string IRN { get; set; }
        //CounterPartyRate  CounterPartyRate LoanRatio InterestRate MaxLoan IsSMSTrade  IsMailPortfolio IsLinkBO LinkBOAccount IsEDC IRN
        #endregion

        #region Nominee
        public bool IsMinor { get; set; }
        public string IsMinorMsg { get; set; }
        public int RelationId { get; set; }
        public int NomineeOccupationId { get; set; }
        public string RelationName { get; set; }
        public decimal Percentage { get; set; }
        public int NomineeCountryId { get; set; }
        public string NomineeName { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNo { get; set; }
        public string NomineeGender { get; set; }

        public int NomineePresentThanaId { get; set; }
        public int NomineePermanentThanaId { get; set; }

        public int NomineeInvestorId { get; set; }
        public string NomineeFatherName { get; set; }
        public string NomineeMotherName { get; set; }
        public DateTime NomineeDateOfBirth { get; set; }
        public string NomineeDateOfBirthMsg { get; set; }
        public string NomineePresentAddress { get; set; }
        public string NomineePermanentAddress { get; set; }
        public bool NomineeIsResident { get; set; }
        public string NomineeIsResidentMsg { get; set; }
        public string NomineePhone { get; set; }
        public string NomineeMobile { get; set; }
        public string NomineeEmail { get; set; }
        public string NomineeNationalId { get; set; }
        public string NomineeDrivingLicenseNo { get; set; }
        public string NomineeBirthCertificateNo { get; set; }
        public bool NomineeHasPassport { get; set; }
        public string NomineeHasPassportMsg { get; set; }
        public string NomineePassportNo { get; set; }
        public DateTime? NomineePassportIssueDate { get; set; }
        public DateTime? NomineePassportExpireDate { get; set; }
        public string NomineePassportIssuePlace { get; set; }
        public HttpPostedFileBase NomineePhotographMsg { get; set; }
        public HttpPostedFileBase NomineeSignatureMsg { get; set; }
        #endregion


        #region nominee Guardian

        public int NomineeId { get; set; }
        public int GuardianPresentThanaId { get; set; }
        public int GuardianPermanentThanaId { get; set; }
        public int GuardianOccupationId { get; set; }
        public string GuardianOccupationName { get; set; }
        public int GuardianCountryId { get; set; }
        public string GuardianCountryName { get; set; }
        public string GuardianName { get; set; }

        public string GuardianFatherName { get; set; }
        public string GuardianMotherName { get; set; }
        public DateTime GuardianDateOfBirth { get; set; }
        public string GuardianDateOfBirthMsg { get; set; }
        public string GuardianGender { get; set; }
        public string GuardianPresentAddress { get; set; }
        public string GuardianPermanentAddress { get; set; }
        public string GuardianPhone { get; set; }

        public string GuardianMobile { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianNationalId { get; set; }
        public string GuardianDrivingLicenseNo { get; set; }
        public string GuardianBirthCertificateNo { get; set; }
        public bool GuardianIsResident { get; set; }
        public string GuardianIsResidentMsg { get; set; }
        public bool GuardianHasPassport { get; set; }
        public string GuardianHasPassportMsg { get; set; }
        public int GuardianId { get; set; }

        public string GuardianPassportNo { get; set; }
        public DateTime? GuardianPassportIssueDate { get; set; }
        public string GuardianPassportExpireDateMsg { get; set; }
        public string GuardianPassportIssueDateMsg { get; set; }
        public DateTime? GuardianPassportExpireDate { get; set; }
        public string GuardianPassportIssuePlace { get; set; }
        public HttpPostedFileBase GuardianPhotographMsg { get; set; }
        public HttpPostedFileBase GuardianSignatureMsg { get; set; }

        #endregion

        #region Investor Company
        public string ContactPerson { get; set; }
        public string RegNo { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public HttpPostedFileBase ComPhotographMsg { get; set; }
        public HttpPostedFileBase ComSignatureMsg { get; set; }

        public int? ComCountryId { get; set; }
        public int? ComPresentThanaId { get; set; }
        public string ComInvestorCode { get; set; }
        public string ComInvestorName { get; set; }
        public DateTime? ComDateOfBirth { get; set; }
        public string ComPresentAddress { get; set; }
        public string ComPhoneOffice { get; set; }
        public string ComMobile { get; set; }
        public string ComSMSMobile { get; set; }
        public string ComEmergencyContactNo { get; set; }
        public string ComEmail { get; set; }
        public string ComSystemEmail { get; set; }
        public string ComFax { get; set; }

        #endregion

        #region JoinHolder

        public int JoinHolderId { get; set; }
        public int? JoinOccupationId { get; set; }
        public int? JoinCountryId { get; set; }
        public string Occupationname { get; set; }
        public string CountryName { get; set; }
        public int? JoinPresentThanaId { get; set; }
        public int? JoinPermanentThanaId { get; set; }
        public string JoinTitle { get; set; }
        public string JoinHolderName { get; set; }
        public string JoinFatherName { get; set; }
        public string JoinMotherName { get; set; }
        public DateTime? JoinDateOfBirth { get; set; }
        public string JoinGender { get; set; }
        public string JoinPresentAddress { get; set; }
        public string JoinPermanentAddress { get; set; }
        public string JoinPhone { get; set; }
        public string JoinMobile { get; set; }
        public string JoinEmail { get; set; }
        public string JoinSMSMobile { get; set; }
        public string JoinSystemEmail { get; set; }
        public string JoinEmergencyContactNo { get; set; }
        public string JoinNationalId { get; set; }
        public string JoinDrivingLicenseNo { get; set; }
        public string JoinBirthCertificateNo { get; set; }
        public bool JoinIsResident { get; set; }
        public bool JoinHasPassport { get; set; }
        public string JoinPassportNo { get; set; }
        public DateTime? JoinPassportIssueDate { get; set; }
        public DateTime? JoinPassportExpireDate { get; set; }
        public string JoinPassportIssuePlace { get; set; }
        public HttpPostedFileBase JoinPhotographMsg { get; set; }
        public HttpPostedFileBase JoinSignatureMsg { get; set; }


        public int? JoinPresentDistrictId { get; set; }
        public int? JoinPresentDivisionId { get; set; }
        public int? JoinPermanentDivisionId { get; set; }
        public int? JoinPermanentDistrictId { get; set; }

        #endregion


        #region Introducer

        // public int IntroInvestorId { get; set; }

        public int IntroducerId { get; set; }
        public int? IntroducerInvestorId { get; set; }
        public string IntroducerInvestorName { get; set; }
        public int? IntroEmployeeId { get; set; }
        public string IntroducerType { get; set; }
        public string IntroducerCode { get; set; }
        public string IntroducerContactNo { get; set; }
        public string IntoducerEmployeeName { get; set; }

        #endregion


        #region Common
        // //   

        public string PresentThanaName { get; set; }
        public string PresentDistrictName { get; set; }
        public int? PresentDistrictId { get; set; }
        public int? PresentDivisionId { get; set; }
        public string DateOfBirthMsg { get; set; }
        public int? PermanentDivisionId { get; set; }
        public int? PermanentDistrictId { get; set; }
        public string PermanentThanaName { get; set; }
        public string PermanentDistrictName { get; set; }
        public string Phone { get; set; }

        public string IsResidentMsg { get; set; }
        public string HasPassportMsg { get; set; }
        public string PassportIssueDateMsg { get; set; }

        public string PassportExpireDateMsg { get; set; }

        public string IntroducerName { get; set; }
        public int? EmployeeId { get; set; }
        //

        // public decimal? LoanRatio { get; set; }
        // public decimal? InterestRate { get; set; }
        //public decimal? MaxLoan { get; set; }

        public string IsSMSTradeMsg { get; set; }

        public string IsMailPortfolioMsg { get; set; }
        public string IsLinkBOMsg { get; set; }
        // public string LinkBOAccount { get; set; }
        public string IsEDCMsg { get; set; }
        //  public string IRN { get; set; }
        public string InvestorStatus { get; set; }
        public string OperationTypeName { get; set; }
        //public string BOID { get; set; }
        public string BOAccountOpeningDateMsg { get; set; }
        // public string TraderIdCSE { get; set; }
        //public string TraderIdDSE { get; set; }
        //public string BankAccountNo { get; set; }
        // public string OmnibusInvestorCode { get; set; }
        // public decimal ComissionRate { get; set; }
        //  public decimal? CounterPartyRate { get; set; }
        public string DPBranchName { get; set; }
        public string BOTypeName { get; set; }
        public string BOCategoryName { get; set; }
        public string BrokerBranchName { get; set; }
        public string BrokerName { get; set; }
        public string InvestorTypeName { get; set; }
        public string AccountTypeName { get; set; }
        public string SubAccountName { get; set; }
        public string BranchName { get; set; }
        public string BankName { get; set; }
        //public string IsMinorMsg { get; set; }

        #endregion
    }
}