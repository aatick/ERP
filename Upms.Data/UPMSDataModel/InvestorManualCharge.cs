namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorManualCharge")]
    public partial class InvestorManualCharge
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int TransactionTypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string TransactionMode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ChargeAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? PostingDate { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public bool ApproveStatus { get; set; }

        public int? ApproveBy { get; set; }

        public DateTime? ApproveDate { get; set; }

        public bool IsActive { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? BrokerBranchId { get; set; }

        public int? InvestorBranchId { get; set; }
    }
}
