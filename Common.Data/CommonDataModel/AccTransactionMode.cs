using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data.CommonDataModel
{
    [Table("AccTransactionMode")]
    public partial class AccTransactionMode
    {
        public int Id { get; set; }

        [StringLength(5)]
        public string TransactionModeShortName { get; set; }

        [Required]
        [StringLength(150)]
        public string TransactionModeName { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
