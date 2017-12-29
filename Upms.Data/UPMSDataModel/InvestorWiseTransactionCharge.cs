using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("InvestorWiseTransactionCharge")]
    public class InvestorWiseTransactionCharge
    {
        public int Id { get; set; }
        public int InvestorId { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool IsSlab { get; set; }
        public bool IsPercentage { get; set; }
        public decimal MinimumCharge { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }


        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 

    }
}
