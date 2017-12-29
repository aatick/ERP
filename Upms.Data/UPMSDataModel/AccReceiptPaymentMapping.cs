namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccReceiptPaymentMapping")]
    public partial class AccReceiptPaymentMapping
    {
        public int Id { get; set; }

        public int? BrokerBranchId { get; set; }

        public int? AccTransactionModeId { get; set; }

        public int? TransactionTypeId { get; set; }

        public bool? FormEntryType { get; set; }

        public int? AccIdDr { get; set; }

        [StringLength(150)]
        public string AccCodeDr { get; set; }

        [StringLength(150)]
        public string NarrationDr { get; set; }

        public int? AccIdCr { get; set; }

        [StringLength(150)]
        public string AccCodeCr { get; set; }

        [StringLength(150)]
        public string NarrationCr { get; set; }

        [StringLength(200)]
        public string TransactionTypeName { get; set; }

        [StringLength(2)]
        public string VoucherType { get; set; }

        public bool? IsValid { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidTo { get; set; }
    }
}
