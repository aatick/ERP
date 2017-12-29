namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccPayment")]
    public partial class AccPayment
    {
        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal VoucherNo { get; set; }
        public int? VoucherYear { get; set; }

        public int InvestorId { get; set; }

        public int TransactionModeId { get; set; }

        [StringLength(30)]
        public string ChequeNo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ChequeDate { get; set; }

        public int? BankId { get; set; }

        public int? BankBranchId { get; set; }
        public int? AccId { get; set; }

        [StringLength(30)]
        public string BranchGLAccount { get; set; }

        [StringLength(30)]
        public string AccountNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime PostDate { get; set; }

        [StringLength(30)]
        public string InvestorGLAccount { get; set; }

      

        [StringLength(20)]
        public string DocRefNo { get; set; }

        public int? BrokerBranchId { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        [StringLength(1)]
        public string ApproveStatus { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ApproveDate { get; set; }

        [StringLength(1)]
        public string GLProcessStatus { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? GLProcessDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Valuedate { get; set; }

        public bool IsAccountPayee { get; set; }
        public bool PrintStatus { get; set; }
        public bool? IsNegativeOrInsufficientBalanceWithdrawal { get; set; }
        public int CreateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsActive { get; set; }
        public int? InvestorBranchId { get; set; }
        public int? ReferenceAccCollectionId { get; set; }
        public bool? AutoEntryForCheckDishonour { get; set; }
        public int? RequisitionId { get; set; }
        public bool? ClearanceStatus { get; set; }
    }
}
