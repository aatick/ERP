namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileTradeStatusCSE")]
    public partial class FileTradeStatusCSE
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string TraderCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OrderReferenceNo { get; set; }

        [StringLength(20)]
        public string CompanyShortName { get; set; }

        [StringLength(1)]
        public string TransactionType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Quantity { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UnitPrice { get; set; }

        [StringLength(10)]
        public string InvestorCode { get; set; }

        [StringLength(5)]
        public string Extra1 { get; set; }

        [StringLength(5)]
        public string Extra2 { get; set; }

        [StringLength(30)]
        public string HawlaReferenceNo { get; set; }

        [StringLength(12)]
        public string TransactionDate { get; set; }

        [StringLength(20)]
        public string TransactionTime { get; set; }

        [StringLength(12)]
        public string OrderDate { get; set; }

        [StringLength(20)]
        public string OrderTime { get; set; }

        [StringLength(5)]
        public string Flag { get; set; }
    }
}
