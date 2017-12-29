using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data.CommonDataModel
{
    [Table("AccBankBranchGL")]
    public partial class AccBankBranchGL
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }

        [StringLength(50)]
        public string AccountNo { get; set; }

        [StringLength(50)]
        public string BranchGLAccountNo { get; set; }
        public int? AccId { get; set; }

        [StringLength(20)]
        public string br_cih_no { get; set; }
        public int IsDLRAccount { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
