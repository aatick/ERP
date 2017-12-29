using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounts.Data.AccountsDataModel
{
    [Table("AccSubledger")]
    public partial class AccSubledger
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SubledgerName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string SubledgerShortName { get; set; }

        
    }
}
