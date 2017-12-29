using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounts.Data.AccountsDataModel
{
    [Table("AccTransactionFor")]
    public partial class AccTransactionFor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionFor { get; set; }

        [Required]
        [StringLength(10)]
        public string TransactionForShortName { get; set; }

        
    }
}
