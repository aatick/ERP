using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data.CommonDataModel
{
   // using UcasPortfolioCodeFirstMigration.Db;
    
    [Table("AccNote")]
    public partial class AccNote
    {
        [Key]
        public int NoteID { get; set; }

        public int NoteNo { get; set; }

        [StringLength(150)]
        public string NoteName { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? InActiveDate { get; set; }

        [Required]
        [StringLength(35)]
        public string CreateUser { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }
        public int OrgID { get; set; }
        //public virtual Organization Organization { get; set; }
    }
}

