using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data.CommonDataModel
{
    [Table("AccTrxDetail")]
    public partial class AccTrxDetail
    {
        [Key]
        public long TrxDetailsID { get; set; }

        public long TrxMasterID { get; set; }
        public int? MarketId { get; set; }
        public int? CompanyId { get; set; }
        public int? AccID { get; set; }

        public decimal? Credit { get; set; }

        public decimal? Debit { get; set; }

        [StringLength(200)]
        public string Narration { get; set; }
        public int? InvestorId { get; set; }
        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }
        public int CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int? AccTransactionModeId { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string AccCode { get; set; }
        public int? TransactionTypeId { get; set; }
        public int? InvestorBranchId { get; set; }
        public virtual AccChart AccChart { get; set; }
        public int? SubLedgerId { get; set; }
        public string SubLedgerReference { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual AccTrxMaster AccTrxMaster { get; set; }
    }
}
