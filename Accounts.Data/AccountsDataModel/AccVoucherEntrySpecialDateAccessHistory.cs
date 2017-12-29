using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounts.Data.AccountsDataModel
{
    [Table("AccVoucherEntrySpecialDateAccessHistory")]
    public partial class AccVoucherEntrySpecialDateAccessHistory
    {
        public int Id { get; set; }

        public int VoucherAccessId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateTo { get; set; }

        public int OfficeId { get; set; }

        [StringLength(500)]
        public string SpecialAccessReason { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public bool IsActive { get; set; }

        public int? UpdateUserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDate { get; set; }
    }
}
