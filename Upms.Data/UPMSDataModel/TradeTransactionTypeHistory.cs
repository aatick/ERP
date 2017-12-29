namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TradeTransactionTypeHistory")]
    public partial class TradeTransactionTypeHistory
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string TransactionTypeShortName { get; set; }

        [Required]
        [StringLength(200)]
        public string TransactionTypeName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Charge { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CSECharge { get; set; }

        public bool? IsPercentage { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
