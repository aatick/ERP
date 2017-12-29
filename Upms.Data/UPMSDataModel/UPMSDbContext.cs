using Common.Data.CommonDataModel;

namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Upms.Data.UPMSDataModel;
    using System.Collections.Generic;
    using System.Data;


    public partial class UPMSDbContext : DbContext
    {
        public UPMSDbContext()
            : base("name=ERPDbContext")
        {
        }
        public virtual DbSet<AccCollection> AccCollections { get; set; }
        public virtual DbSet<AccPayment> AccPayments { get; set; }
        public virtual DbSet<AccPaymentRequisition> AccPaymentRequisitions { get; set; }
        public virtual DbSet<InvestorManualCharge> InvestorManualCharges { get; set; }
        public virtual DbSet<AccReceiptPaymentMapping> AccReceiptPaymentMappings { get; set; }

        public virtual DbSet<BrokerbranchDPBranchMapping> BrokerbranchDPBranchMappings { get; set; }

        public virtual DbSet<BrokerDepositoryPariticipant> BrokerDepositoryPariticipants { get; set; }
        public virtual DbSet<BrokerDPBranch> BrokerDPBranches { get; set; }

        public virtual DbSet<BrokerInformation> BrokerInformations { get; set; }
        public virtual DbSet<BrokerTrader> BrokerTraders { get; set; }
        public virtual DbSet<CompanyDepository> CompanyDepositories { get; set; }
        public virtual DbSet<CompanyEpsNavChangeHistory> CompanyEpsNavChangeHistories { get; set; }
        public virtual DbSet<CompanyGroupChangeHistory> CompanyGroupChangeHistories { get; set; }
        public virtual DbSet<CompanyLoanStatusChangeHistory> CompanyLoanStatusChangeHistories { get; set; }
        public virtual DbSet<CompanyInformation> CompanyInformations { get; set; }
        public virtual DbSet<CompanyListedToDepositoryCompany> CompanyListedToDepositoryCompanies { get; set; }
        public virtual DbSet<CompanySharePriceHistory> CompanySharePriceHistories { get; set; }
        public virtual DbSet<CheckDishonourCause> CheckDishonourCauses { get; set; }
        public virtual DbSet<EmailSMSInvestorAccess> EmailSMSInvestorAccesses { get; set; }
        public virtual DbSet<EmailSMSDPAccess> EmailSMSDPAccesses { get; set; }
        public virtual DbSet<FundTransferMaster> FundTransferMasters { get; set; }
        public virtual DbSet<InvestorCheckDishonourInformation> InvestorCheckDishonourInformations { get; set; }
        public virtual DbSet<InvestorAccount> InvestorAccounts { get; set; }
        public virtual DbSet<InvestorAccountType> InvestorAccountTypes { get; set; }
        public virtual DbSet<InvestorBOCategory> InvestorBOCategories { get; set; }
        public virtual DbSet<InvestorBOType> InvestorBOTypes { get; set; }
        public virtual DbSet<InvestorBrokerageRateHistory> InvestorBrokerageRateHistories { get; set; }
        public virtual DbSet<InvestorCompany> InvestorCompanies { get; set; }
        public virtual DbSet<InvestorBalanceDaily> InvestorBalanceDailies { get; set; }
        public virtual DbSet<InvestorBalanceHistory> InvestorBalanceHistories { get; set; }
        public virtual DbSet<InvestorDetail> InvestorDetails { get; set; }
        public virtual DbSet<InvestorInterestRateHistory> InvestorInterestRateHistories { get; set; }
        public virtual DbSet<InvestorIntroducer> InvestorIntroducers { get; set; }
        public virtual DbSet<InvestorJointHolder> InvestorJointHolders { get; set; }
        public virtual DbSet<InvestorNominee> InvestorNominees { get; set; }
        public virtual DbSet<InvestorNomineeGuardian> InvestorNomineeGuardians { get; set; }
        public virtual DbSet<InvestorOperationType> InvestorOperationTypes { get; set; }
        public virtual DbSet<InvestorPowerOfAttorney> InvestorPowerOfAttorneys { get; set; }
        public virtual DbSet<InvestorPowerOfAttorneyMapping> InvestorPowerOfAttorneyMappings { get; set; }
        public virtual DbSet<InvestorsStatus> InvestorStatus { get; set; }
        public virtual DbSet<InvestorSubAccountType> InvestorSubAccountTypes { get; set; }
        public virtual DbSet<InvestorType> InvestorTypes { get; set; }
        public virtual DbSet<IPOAllocation> IPOAllocations { get; set; }
        public virtual DbSet<IPOApplication> IPOApplications { get; set; }
        public virtual DbSet<IPODraft> IPODrafts { get; set; }
        public virtual DbSet<LookupBank> LookupBanks { get; set; }
        public virtual DbSet<LookupBankBranch> LookupBankBranches { get; set; }

        public virtual DbSet<MarketGroup> MarketGroups { get; set; }
        public virtual DbSet<MarketInformation> MarketInformations { get; set; }
        public virtual DbSet<MarketInstrumentType> MarketInstrumentTypes { get; set; }
        public virtual DbSet<MarketSector> MarketSectors { get; set; }
        public virtual DbSet<MarketType> MarketTypes { get; set; }

        public virtual DbSet<TradeDetailsDaily> TradeDetailsDailies { get; set; }
        public virtual DbSet<TradeDetailsHistory> TradeDetailsHistories { get; set; }
        public virtual DbSet<TradeTransactionType> TradeTransactionTypes { get; set; }
        public virtual DbSet<TradeTransactionTypeHistory> TradeTransactionTypeHistories { get; set; }
        public virtual DbSet<TradeUploadFileInformation> TradeUploadFileInformations { get; set; }
        public virtual DbSet<InvestorPortfolioDaily> InvestorPortfolioDailies { get; set; }
        public virtual DbSet<InvestorPortfolioHistory> InvestorPortfolioHistories { get; set; }
        public virtual DbSet<FileTradeStatusCSE> TradeFileCSEs { get; set; }
        public virtual DbSet<FileTradeStatusDSE> TradeFileDSEs { get; set; }
        public virtual DbSet<TradeUploadFileAccess> TradeUploadFileAccesses { get; set; }
        public virtual DbSet<IPOGroupDetail> IPOGroupDetails { get; set; }
        public virtual DbSet<IPOGroupMaster> IPOGroupMasters { get; set; }
        public virtual DbSet<InvestorTransactionDaily> InvestorTransactionDailies { get; set; }
        public virtual DbSet<InvestorWiseTransactionCharge> InvestorWiseTransactionCharges { get; set; }
        public virtual DbSet<InvestorWiseTransactionChargeHistory> InvestorWiseTransactionChargeHistories { get; set; }
        public virtual DbSet<InvestorWiseTransactionChargeSlab> InvestorWiseTransactionChargeSlabs { get; set; }
        public virtual DbSet<InvestorWiseTransactionChargeSlabHistory> InvestorWiseTransactionChargeSlabHistories { get; set; }
        //public virtual DbSet<InvestorManualCharge> InvestorManualCharges { get; set; }
        public virtual DbSet<TradeTransactionCharge> TradeTransactionCharges { get; set; }
        public virtual DbSet<TradeTransactionChargeHistory> TradeTransactionChargeHistories { get; set; }
        public virtual DbSet<TradeTransactionChargeSlab> TradeTransactionChargeSlabs { get; set; }
        public virtual DbSet<TradeTransactionChargeSlabHistory> TradeTransactionChargeSlabHistories { get; set; }
        public virtual DbSet<LookupCurrency> LookupCurrencies { get; set; }
        public virtual DbSet<IPOIssueMethod> IPOIssueMethods { get; set; }
        public virtual DbSet<IPODeclaration> IPODeclarations { get; set; }
        public virtual DbSet<IPOCurrencyMapping> IPOCurrencyMappings { get; set; }
        //upms end


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //upms start
            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.DPCode)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.BORefNo)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.DepositoryParticipantName)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.ContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.ContactNo)
                .IsUnicode(false);

            //modelBuilder.Entity<BrokerDepositoryPariticipant>()
            //    .Property(e => e.PayInOut)
            //    .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.ClearingAccDSE)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDepositoryPariticipant>()
                .Property(e => e.ClearingAccCSE)
                .IsUnicode(false);

            //modelBuilder.Entity<BrokerDepositoryPariticipant>()
            //    .Property(e => e.B_dp_gid)
            //    .IsUnicode(false);

            //modelBuilder.Entity<BrokerDepositoryPariticipant>()
            //    .HasMany(e => e.BrokerDPBranches)
            //    .WithRequired(e => e.BrokerDepositoryPariticipant)
            //    .HasForeignKey(e => e.DepositoryParticipantId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.DPBranchName)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.ContactName)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.ContactMobile)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.ContactEmail)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerDPBranch>()
                .Property(e => e.ContactFax)
                .IsUnicode(false);



            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.BrokerCode)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.BrokerShortName)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.BrokerName)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.RegistrationNo)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.FreeLimit)
                .HasPrecision(20, 4);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.AuthCapital)
                .HasPrecision(20, 4);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.PaidUpCapital)
                .HasPrecision(20, 4);

            modelBuilder.Entity<BrokerInformation>()
                .Property(e => e.DepositMode)
                .IsUnicode(false);

            ////modelBuilder.Entity<BrokerInformation>()
            ////    .Property(e => e.B_b_gid)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<BrokerInformation>()
            //    .HasMany(e => e.BrokerBranches)
            //    .WithRequired(e => e.BrokerInformation)
            //    .HasForeignKey(e => e.BrokerId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrokerTrader>()
                .Property(e => e.TraderCode)
                .IsUnicode(false);

            modelBuilder.Entity<BrokerTrader>()
                .Property(e => e.PCNo)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.CompanyDepositoryShortName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.DepositoryCompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.WebAddress)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.ContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.ContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDepository>()
                .Property(e => e.ContactEmail)
                .IsUnicode(false);

            ////modelBuilder.Entity<CompanyDepository>()
            ////    .Property(e => e.B_dep_Com_Code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<CompanyDepository>()
            //    .HasMany(e => e.CompanyListedToDepositoryCompanies)
            //    .WithRequired(e => e.CompanyDepository)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyEpsNavChangeHistory>()
                .Property(e => e.EPS)
                .HasPrecision(7, 4);

            modelBuilder.Entity<CompanyEpsNavChangeHistory>()
                .Property(e => e.NAV)
                .HasPrecision(25, 4);

            modelBuilder.Entity<CompanyEpsNavChangeHistory>()
                .Property(e => e.AuthorizeCapital)
                .HasPrecision(20, 4);

            modelBuilder.Entity<CompanyEpsNavChangeHistory>()
                .Property(e => e.PaidUpCapital)
                .HasPrecision(20, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.CompanyShortName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.WebAddress)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.TradeIdDSE)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.TradeIdCSE)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.EPS)
                .HasPrecision(7, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.NAV)
                .HasPrecision(25, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.ISINNo)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.Premium)
                .HasPrecision(9, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.AuthorizeCapital)
                .HasPrecision(20, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.PaidUpCapital)
                .HasPrecision(20, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.FaceValue)
                .HasPrecision(9, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.MarketPrice)
                .HasPrecision(20, 4);

            modelBuilder.Entity<CompanyInformation>()
                .Property(e => e.ScripCode)
                .IsUnicode(false);

            ////modelBuilder.Entity<CompanyInformation>()
            ////    .Property(e => e.B_Company_Code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<CompanyInformation>()
            //    .HasMany(e => e.CompanyEpsNavChangeHistories)
            //    .WithRequired(e => e.CompanyInformation)
            //    .HasForeignKey(e => e.CompanyId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CompanyInformation>()
            //    .HasMany(e => e.CompanyGroupChangeHistories)
            //    .WithRequired(e => e.CompanyInformation)
            //    .HasForeignKey(e => e.CompanyId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CompanyInformation>()
            //    .HasMany(e => e.CompanyListedToDepositoryCompanies)
            //    .WithRequired(e => e.CompanyInformation)
            //    .HasForeignKey(e => e.CompanyId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CompanyInformation>()
            //    .HasMany(e => e.CompanySharePriceHistories)
            //    .WithRequired(e => e.CompanyInformation)
            //    .HasForeignKey(e => e.CompanyId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanySharePriceHistory>()
                .Property(e => e.TradeCode)
                .IsUnicode(false);

            modelBuilder.Entity<CompanySharePriceHistory>()
                .Property(e => e.LastTradePrice)
                .HasPrecision(9, 3);

            modelBuilder.Entity<CompanySharePriceHistory>()
                .Property(e => e.HighestPrice)
                .HasPrecision(9, 3);

            modelBuilder.Entity<CompanySharePriceHistory>()
                .Property(e => e.LowestPrice)
                .HasPrecision(9, 3);

            modelBuilder.Entity<CompanySharePriceHistory>()
                .Property(e => e.ClosingPrice)
                .HasPrecision(9, 3);

            modelBuilder.Entity<CompanySharePriceHistory>()
                .Property(e => e.Value)
                .HasPrecision(9, 3);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.BOID)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.TraderIdDSE)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.TraderIdCSE)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.BankAccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.OmnibusInvestorCode)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorAccount>()
            //    .Property(e => e.BrokerageRate)
            //    .HasPrecision(7, 4);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.LoanRatio)
                .HasPrecision(7, 4);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.InterestRate)
                .HasPrecision(7, 4);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.MaxLoan)
                .HasPrecision(25, 4);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.LinkBOAccount)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccount>()
                .Property(e => e.IRN)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccountType>()
                .Property(e => e.AccountTypeShortName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorAccountType>()
                .Property(e => e.AccountTypeName)
                .IsUnicode(false);

            ////modelBuilder.Entity<InvestorAccountType>()
            ////    .Property(e => e.B_Acc_Type_Code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<InvestorAccountType>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithRequired(e => e.InvestorAccountType)
            //    .HasForeignKey(e => e.AccountTypeId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorAccountType>()
            //    .HasMany(e => e.InvestorSubAccountTypes)
            //    .WithRequired(e => e.InvestorAccountType)
            //    .HasForeignKey(e => e.AccountTypeId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorBOCategory>()
                .Property(e => e.BOCategoryName)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorBOCategory>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithRequired(e => e.InvestorBOCategory)
            //    .HasForeignKey(e => e.BOCategoryId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorBOType>()
                .Property(e => e.BOTypeName)
                .IsUnicode(false);

            ////modelBuilder.Entity<InvestorBOType>()
            ////    .Property(e => e.B_Op_Type_Code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<InvestorBOType>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithRequired(e => e.InvestorBOType)
            //    .HasForeignKey(e => e.BOTypeId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorBrokerageRateHistory>()
                .Property(e => e.BrokarageComissionRate)
                .HasPrecision(7, 4);

            modelBuilder.Entity<InvestorCompany>()
                .Property(e => e.ContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorCompany>()
                .Property(e => e.RegNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.InvestorCode)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.InvestorName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.PresentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.PhoneRes)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.PhoneOffice)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.SMSMobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.EmergencyContactNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.SystemEmail)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.NationalId)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.ETIN)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.DrivingLicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.BirthCertificateNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.PassportNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.PassportIssuePlace)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorDetail>()
                .Property(e => e.SpecialInstruction)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorBrokerageRateHistories)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorCompanies)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorNominees)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorInterestRateHistories)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorIntroducers)
            //    .WithOptional(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.IntroducerInvestorId);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorIntroducers1)
            //    .WithRequired(e => e.InvestorDetail1)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorJointHolders)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<InvestorDetail>()
            //    .HasMany(e => e.InvestorPowerOfAttorneyMappings)
            //    .WithRequired(e => e.InvestorDetail)
            //    .HasForeignKey(e => e.InvestorId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorInterestRateHistory>()
                .Property(e => e.InterestRate)
                .HasPrecision(7, 4);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.JoinHolderName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.PresentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.SMSMobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.SystemEmail)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.EmergencyContactNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.NationalId)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.DrivingLicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.BirthCertificateNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.PassportNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorJointHolder>()
                .Property(e => e.PassportIssuePlace)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.NomineeName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.PresentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.NationalId)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.DrivingLicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.BirthCertificateNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.PassportNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.PassportIssuePlace)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNominee>()
                .Property(e => e.Percentage)
                .HasPrecision(5, 2);

            //modelBuilder.Entity<InvestorNominee>()
            //    .HasMany(e => e.InvestorNomineeGuardians)
            //    .WithRequired(e => e.InvestorNominee)
            //    .HasForeignKey(e => e.NomineeId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.GuardianName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.PresentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.NationalId)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.DrivingLicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.BirthCertificateNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.PassportNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorNomineeGuardian>()
                .Property(e => e.PassportIssuePlace)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorOperationType>()
                .Property(e => e.OperationTypeName)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorOperationType>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithOptional(e => e.InvestorOperationType)
            //    .HasForeignKey(e => e.OperationTypeId);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.PowerOfAttorneyName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.FatherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.MotherName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.PresentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.PermanentAddress)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.NationalId)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.DrivingLicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.BirthCertificateNo)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorPowerOfAttorney>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorPowerOfAttorney>()
            //    .HasMany(e => e.InvestorPowerOfAttorneyMappings)
            //    .WithRequired(e => e.InvestorPowerOfAttorney)
            //    .HasForeignKey(e => e.PowerOfAttorneyId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorsStatus>()
                .Property(e => e.InvestorStatus)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorStatu>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithRequired(e => e.InvestorStatu)
            //    .HasForeignKey(e => e.StatusId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvestorSubAccountType>()
                .Property(e => e.SubAccountShortName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorSubAccountType>()
                .Property(e => e.SubAccountName)
                .IsUnicode(false);

            ////modelBuilder.Entity<InvestorSubAccountType>()
            ////    .Property(e => e.B_Sub_Acc_Code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<InvestorSubAccountType>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithOptional(e => e.InvestorSubAccountType)
            //    .HasForeignKey(e => e.SubAccountTypeId);

            modelBuilder.Entity<InvestorType>()
                .Property(e => e.InvestorTypeShortName)
                .IsUnicode(false);

            modelBuilder.Entity<InvestorType>()
                .Property(e => e.InvestorTypeName)
                .IsUnicode(false);

            //modelBuilder.Entity<InvestorType>()
            //    .HasMany(e => e.InvestorAccounts)
            //    .WithRequired(e => e.InvestorType)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<LookupBank>()
                .Property(e => e.BankShortName)
                .IsUnicode(false);

            modelBuilder.Entity<LookupBank>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            ////modelBuilder.Entity<LookupBank>()
            ////    .Property(e => e.B_Bank_Code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<LookupBank>()
            //    .HasMany(e => e.LookupBankBranches)
            //    .WithRequired(e => e.LookupBank)
            //    .HasForeignKey(e => e.BankId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<LookupBankBranch>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<LookupBankBranch>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<LookupBankBranch>()
                .Property(e => e.RoutingNo)
                .IsUnicode(false);



            modelBuilder.Entity<MarketGroup>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketGroup>()
                .Property(e => e.GroupName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            //modelBuilder.Entity<MarketGroup>()
            //    .HasMany(e => e.CompanyGroupChangeHistories)
            //    .WithRequired(e => e.MarketGroup)
            //    .HasForeignKey(e => e.PresentGroupId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<MarketGroup>()
            //    .HasMany(e => e.CompanyGroupChangeHistories1)
            //    .WithOptional(e => e.MarketGroup1)
            //    .HasForeignKey(e => e.PreviousGroupId);

            //modelBuilder.Entity<MarketGroup>()
            //    .HasMany(e => e.CompanyInformations)
            //    .WithOptional(e => e.MarketGroup)
            //    .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<MarketInformation>()
                .Property(e => e.MarketShortName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketInformation>()
                .Property(e => e.ExchangeName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketInformation>()
                .Property(e => e.MarketCode)
                .HasPrecision(2, 0);

            //modelBuilder.Entity<MarketInformation>()
            //    .HasMany(e => e.BrokerTraders)
            //    .WithRequired(e => e.MarketInformation)
            //    .HasForeignKey(e => e.MarketId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<MarketInformation>()
            //    .HasMany(e => e.CompanySharePriceHistories)
            //    .WithRequired(e => e.MarketInformation)
            //    .HasForeignKey(e => e.MarketId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarketInstrumentType>()
                .Property(e => e.InstrumentTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketInstrumentType>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            ////modelBuilder.Entity<MarketInstrumentType>()
            ////    .Property(e => e.B_inst_type_code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<MarketInstrumentType>()
            //    .HasMany(e => e.CompanyInformations)
            //    .WithOptional(e => e.MarketInstrumentType)
            //    .HasForeignKey(e => e.InstrumentTypeId);

            modelBuilder.Entity<MarketSector>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketSector>()
                .Property(e => e.SectorName)
                .IsUnicode(false);

            ////modelBuilder.Entity<MarketSector>()
            ////    .Property(e => e.B_maj_sec_code)
            ////    .IsUnicode(false);

            //modelBuilder.Entity<MarketSector>()
            //    .HasMany(e => e.CompanyInformations)
            //    .WithOptional(e => e.MarketSector)
            //    .HasForeignKey(e => e.SectorId);

            modelBuilder.Entity<MarketType>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<MarketType>()
                .Property(e => e.MarketTypeName)
                .IsUnicode(false);


            modelBuilder.Entity<InvestorWiseTransactionCharge>().Property(x => x.MinimumCharge).HasPrecision(18, 4);
            modelBuilder.Entity<InvestorWiseTransactionCharge>().Property(x => x.ChargeAmount).HasPrecision(18, 4);

            modelBuilder.Entity<InvestorWiseTransactionChargeHistory>().Property(x => x.MinimumCharge).HasPrecision(18, 4);
            modelBuilder.Entity<InvestorWiseTransactionChargeHistory>().Property(x => x.ChargeAmount).HasPrecision(18, 4);

            modelBuilder.Entity<InvestorWiseTransactionChargeSlab>().Property(x => x.AmountFrom).HasPrecision(18, 4);
            modelBuilder.Entity<InvestorWiseTransactionChargeSlab>().Property(x => x.AmountTo).HasPrecision(18, 4);
            modelBuilder.Entity<InvestorWiseTransactionChargeSlab>().Property(x => x.Charge).HasPrecision(18, 4);

            modelBuilder.Entity<InvestorWiseTransactionChargeSlabHistory>().Property(x => x.AmountFrom).HasPrecision(18, 4);
            modelBuilder.Entity<InvestorWiseTransactionChargeSlabHistory>().Property(x => x.AmountTo).HasPrecision(18, 4);
            modelBuilder.Entity<InvestorWiseTransactionChargeSlabHistory>().Property(x => x.Charge).HasPrecision(18, 4);

            modelBuilder.Entity<TradeTransactionCharge>().Property(x => x.MinimumCharge).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionCharge>().Property(x => x.DSECharge).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionCharge>().Property(x => x.CSECharge).HasPrecision(18, 4);

            modelBuilder.Entity<TradeTransactionChargeHistory>().Property(x => x.MinimumCharge).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionChargeHistory>().Property(x => x.DSECharge).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionChargeHistory>().Property(x => x.CSECharge).HasPrecision(18, 4);

            modelBuilder.Entity<TradeTransactionChargeSlab>().Property(x => x.AmountFrom).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionChargeSlab>().Property(x => x.AmountTo).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionChargeSlab>().Property(x => x.Charge).HasPrecision(18, 4);

            modelBuilder.Entity<TradeTransactionChargeSlabHistory>().Property(x => x.AmountFrom).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionChargeSlabHistory>().Property(x => x.AmountTo).HasPrecision(18, 4);
            modelBuilder.Entity<TradeTransactionChargeSlabHistory>().Property(x => x.Charge).HasPrecision(18, 4);

            modelBuilder.Entity<IPODeclaration>().Property(x => x.FaceValue).HasPrecision(5, 2);
            modelBuilder.Entity<IPODeclaration>().Property(x => x.Premium).HasPrecision(5, 2);
            modelBuilder.Entity<IPODeclaration>().Property(x => x.Discount).HasPrecision(5, 2);
            modelBuilder.Entity<IPODeclaration>().Property(x => x.FreeShare).HasPrecision(5, 2);
            modelBuilder.Entity<IPODeclaration>().Property(x => x.LockUptoThreeMonth).HasPrecision(5, 2);
            modelBuilder.Entity<IPODeclaration>().Property(x => x.LockUptoSixMonth).HasPrecision(5, 2);
            //upms end
        }
    }
}

