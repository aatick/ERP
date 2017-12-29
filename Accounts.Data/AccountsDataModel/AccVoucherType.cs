using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounts.Data.AccountsDataModel
{
    [Table("AccVoucherType")]
    public partial class AccVoucherType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string VoucherType { get; set; }

        [Required]
        [StringLength(10)]
        public string VoucherTypeShortName { get; set; }

        
    }
}
