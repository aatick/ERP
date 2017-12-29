namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IPOApplication")]
    public partial class IPOApplication
    {
        public int Id { get; set; }

        public int IPODeclarationId { get; set; }

        public int InvestorId { get; set; }

        public int? DraftId { get; set; }

        public int Lot { get; set; }

        public decimal? UnitPrice { get; set; }

        public int ShareQuantity { get; set; }

        public decimal AppliedAmount { get; set; }

        public int CurrencyId { get; set; }

        public bool? OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? OrderUserId { get; set; }

        public DateTime? OrderCancelDate { get; set; }

        public int? OrderCancelUserId { get; set; }

        public bool ApplicationStatus { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public int? ApplicationUserId { get; set; }

        public bool AllocationStatus { get; set; }

        public bool RefundStatus { get; set; }

        public int? AllocationQuantity { get; set; }

        public decimal RefundAmount { get; set; }

        public decimal FineAmount { get; set; }

        public DateTime? AllocationRefundDate { get; set; }

        public int? AllocationRefundUserId { get; set; }

        public bool? IsShareCredited { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreditedDate { get; set; }

        public int? InvestorTypeId { get; set; }
        public int? IpoGroupId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public int B_IPO_Application_Id { get; set; }

        public int TransactionTypeId { get; set; }
        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 
    }
}
