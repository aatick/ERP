namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TradeTransactionType")]
    public partial class TradeTransactionType
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string TransactionTypeShortName { get; set; }

        [Required]
        [StringLength(200)]
        public string TransactionTypeName { get; set; }
        public bool IsShareType { get; set; }
        public bool IsOnePercent { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
