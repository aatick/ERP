using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("TradeTransactionCharge")]
    public class TradeTransactionCharge
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal DSECharge { get; set; }
        public decimal CSECharge { get; set; }
        public bool IsDSESlab { get; set; }
        public bool IsCSESlab { get; set; }
        public bool IsPercentage { get; set; }
        public decimal MinimumCharge { get; set; }
        public string ChargeType { get; set; }
        public string CalculationBasis { get; set; }
        public string CalculationMode { get; set; }
        public string EffectMode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public string TransactionTypeName { get; set; }
        [NotMapped]
        public string TransactionTypeShortName { get; set; }
    }
}
