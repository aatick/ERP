using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data.CommonDataModel
{
    [Table("AccTrxMaster")]
    public partial class AccTrxMaster
    {
        public AccTrxMaster()
        {
            AccTrxDetails = new HashSet<AccTrxDetail>();
        }

        [Key]
        public long TrxMasterID { get; set; }
        public int? VoucherYear { get; set; }
        public int OfficeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime TrxDate { get; set; }

        public long? VoucherNo { get; set; }

        [StringLength(200)]
        public string VoucherDesc { get; set; }

        [StringLength(3)]
        public string VoucherType { get; set; }
        public int? VoucherTypeId { get; set; }
        public int? AccTransactionForId { get; set; }

        [StringLength(125)]
        public string Reference { get; set; }

        public bool? IsPosted { get; set; }
        public bool? IsYearlyClosing { get; set; }
        public bool? IsAutoVoucher { get; set; }
        public bool? IsRectify { get; set; }
        //public int? OrgID { get; set; }
        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        public int CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<AccTrxDetail> AccTrxDetails { get; set; }
    }
}
