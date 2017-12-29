namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorBalanceHistory")]
    public partial class InvestorBalanceHistory
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        public decimal ImmaturedBalance { get; set; }

        public decimal ClearingChequeBalance { get; set; }

        public decimal ClosingBalance { get; set; }

        public decimal LoanRatio { get; set; }

        public int StatusId { get; set; }

        public int? AccountTypeId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public int BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 
    }
}
