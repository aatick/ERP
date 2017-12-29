namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySharePriceHistory")]
    public partial class CompanySharePriceHistory
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int MarketId { get; set; }

        [Required]
        [StringLength(20)]
        public string TradeCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        public decimal LastTradePrice { get; set; }

        public decimal HighestPrice { get; set; }

        public decimal LowestPrice { get; set; }

        public decimal ClosingPrice { get; set; }

        public int TotalNumberOfTrade { get; set; }

        public decimal Value { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public virtual CompanyInformation CompanyInformation { get; set; }

        public virtual MarketInformation MarketInformation { get; set; }
    }
}
