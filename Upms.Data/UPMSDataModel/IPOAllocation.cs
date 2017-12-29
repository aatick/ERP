namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IPOAllocation")]
    public partial class IPOAllocation
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int CompanyId { get; set; }

        public int TransactionTypeId { get; set; }

        public int AllocationQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AllocationDate { get; set; }

        public int AllocationUserId { get; set; }

        public int FreeShareQuantity { get; set; }

        public int? FirstLockQuantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstLockUptoDate { get; set; }

        public int? SecondLockQuantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SecondLockUptoDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
