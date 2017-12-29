namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorPortfolioHistory")]
    public partial class InvestorPortfolioHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int CompanyId { get; set; }

        public int MarketId { get; set; }

        public int TransactionTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        public TimeSpan TransactionTime { get; set; }

        public int TodaysQuantity { get; set; }

        public int ActualQuantity { get; set; }

        public int SaleableQuantity { get; set; }

        public decimal TodaysAverageUnitPrice { get; set; }

        public decimal ActualAverageUnitPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MaturedDate { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal CreditAmount { get; set; }

        public decimal CommissionRate { get; set; }

        public decimal CommissionAmount { get; set; }

        public decimal CounterPartyRate { get; set; }

        public decimal HawlaCharge { get; set; }

        public decimal LagaCharge { get; set; }

        public decimal AITCharge { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public virtual CompanyInformation CompanyInformation { get; set; }

        public virtual InvestorDetail InvestorDetail { get; set; }

        public virtual MarketInformation MarketInformation { get; set; }

        public virtual TradeTransactionType TradeTransactionType { get; set; }
    }
}
