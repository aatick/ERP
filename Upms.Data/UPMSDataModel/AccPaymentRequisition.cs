namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccPaymentRequisition")]
    public partial class AccPaymentRequisition
    {
        public int Id { get; set; }

        public int RequisitionNo { get; set; }

        public int InvestorId { get; set; }

        [Column(TypeName = "date")]
        public DateTime RequisitionDate { get; set; }

        public decimal Amount { get; set; }

        public bool IsPayment { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }

        public int CreateUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }

        public int? UpdateUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDate { get; set; }
        public bool IsApprove { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? ApproveUserId { get; set; }
        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 
    }
}
