namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorAccount")]
    public partial class InvestorAccount
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int? DPBranchId { get; set; }

        public int BOTypeId { get; set; }

        public int BOCategoryId { get; set; }

        public int? BrokerBranchId { get; set; }

        public int? InvestorTypeId { get; set; }

        public int? AccountTypeId { get; set; }

        public int? SubAccountTypeId { get; set; }

        public int? BankBranchId { get; set; }

        public int StatusId { get; set; }

        public int? OperationTypeId { get; set; }

        [StringLength(16)]
        public string BOID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BOAccountOpeningDate { get; set; }

        [StringLength(20)]
        public string TraderIdDSE { get; set; }

        [StringLength(20)]
        public string TraderIdCSE { get; set; }

        [StringLength(50)]
        public string BankAccountNo { get; set; }

        [StringLength(10)]
        public string OmnibusInvestorCode { get; set; }
        public decimal ComissionRate { get; set; }
        public decimal? CounterPartyRate { get; set; }
        //public decimal? BrokerageRate { get; set; }

        public decimal? LoanRatio { get; set; }

        public decimal? InterestRate { get; set; }

        public decimal? MaxLoan { get; set; }

        public bool IsSMSService { get; set; }

        public bool IsMailService { get; set; }

        public bool IsLinkBO { get; set; }

        [StringLength(20)]
        public string LinkBOAccount { get; set; }

        public bool IsEDC { get; set; }

        [StringLength(20)]
        public string IRN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
        public int? OfficeID { get; set; }
        public bool? TemporaryClose { get; set; }
        //public virtual BrokerBranch BrokerBranch { get; set; }

        //public virtual BrokerDPBranch BrokerDPBranch { get; set; }

        //public virtual InvestorAccountType InvestorAccountType { get; set; }

        //public virtual InvestorBOCategory InvestorBOCategory { get; set; }

        //public virtual InvestorBOType InvestorBOType { get; set; }

        //public virtual InvestorDetail InvestorDetail { get; set; }

        //public virtual InvestorOperationType InvestorOperationType { get; set; }

        //public virtual InvestorStatu InvestorStatu { get; set; }

        //public virtual InvestorSubAccountType InvestorSubAccountType { get; set; }

        //public virtual InvestorType InvestorType { get; set; }

        //public virtual LookupBankBranch LookupBankBranch { get; set; }
    }
}
