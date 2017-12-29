namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccCollection")]
    public partial class AccCollection
    {
        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal VoucherNo { get; set; }
        public int? VoucherYear { get; set; }
        public int? InvestorId { get; set; }
        public int TransactionModeId { get; set; }

        [StringLength(20)]
        public string ChequeNo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ChequeDate { get; set; }

        public int? BankId { get; set; }

        public int? BankBranchId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime PostDate { get; set; }

        [StringLength(20)]
        public string InvestorGLAcc { get; set; }

        [StringLength(10)]
        public string DocRefNo { get; set; }

        [Column(TypeName = "numeric")]
        public int? BranchId { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        [StringLength(1)]
        public string ApproveStatus { get; set; }
        public int? ApproveBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ApproveDate { get; set; }

        [StringLength(1)]
        public string DepositStatus { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DepositDate { get; set; }
        public int? DepositBy { get; set; }
        public string RoutingNo { get; set; }
        public int? DepositBankId { get; set; }

        public int? DepositBankBranchId { get; set; }

        public int? AccId { get; set; }
        [StringLength(30)]
        public string BranchGLAccount { get; set; }
        [StringLength(30)]
        public string AccountNo { get; set; }

        [StringLength(20)]
        public string DepositSlipNo { get; set; }

      

        public bool? ClearanceStatus { get; set; }

        public DateTime? ClearanceDate { get; set; }
        public int? ClearDisHonourBy { get; set; }

        [StringLength(1)]
        public string GLProcessStatus { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? GLProcessStatusDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RecordVersion { get; set; }

        public int? CheckBy { get; set; }

        public DateTime? CheckDate { get; set; }

       

        [StringLength(1)]
        public string Auth_Status { get; set; }

        [StringLength(4)]
        public string ActionStatus { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Valuedate { get; set; }
        public int? IPODeclarationId { get; set; }
        public bool? IsClosedInvestor { get; set; }
        public int? ChequeStatusId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; }
        public int? ReferenceAccCollectionId { get; set; }
    }
}
