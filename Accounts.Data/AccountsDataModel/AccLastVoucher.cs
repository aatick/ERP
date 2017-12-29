using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounts.Data.AccountsDataModel
{
    [Table("AccLastVoucher")] 
    public partial class AccLastVoucher
    {
        [Key]
        public int Id { get; set; }

        public int OfficeID { get; set; }

        [Required]
        [StringLength(50)]
        public string VoucherNo { get; set; }
    }
}
