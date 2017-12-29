using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("IPODeclaration")]
    public class IPODeclaration
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? IssueMethodId { get; set; }
        public bool IsIPORPO { get; set; }
        public int Lot { get; set; }
        public decimal FaceValue { get; set; }
        public decimal? Premium { get; set; }
        public decimal? Discount { get; set; }
        public DateTime ApplicationStartDate { get; set; }
        public DateTime ApplicationEndDate { get; set; }
        public DateTime NRBApplicationLastDate { get; set; }
        public string Status { get; set; }
        public string TradingCode { get; set; }
        public DateTime? DataUploadLastDate { get; set; }
        public DateTime? ResultDownloadDate { get; set; }
        public DateTime? BankLastDate { get; set; }
        public DateTime? ProspectusIssueDate { get; set; }
        public decimal? FreeShare { get; set; }
        public decimal? LockUptoThreeMonth { get; set; }
        public DateTime? LockUptoThreeMonthDate { get; set; }
        public decimal? LockUptoSixMonth { get; set; }
        public DateTime? LockUptoSixMonthDate { get; set; }
        public int MarketId { get; set; }
        public bool ClosingStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }

        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string IssueMethod { get; set; }
       // public string Mercket { get; set; }
    }
}
