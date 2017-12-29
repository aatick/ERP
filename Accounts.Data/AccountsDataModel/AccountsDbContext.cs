using System.Data.Entity;

namespace Accounts.Data.AccountsDataModel
{
    public partial class AccountsDbContext : DbContext
    {
        public AccountsDbContext()
            : base("name=ERPDbContext")
        {
        }
        //accounts start
        public virtual DbSet<AccLastVoucher> AccLastVouchers { get; set; }
        
        

        public virtual DbSet<AccVoucherEntrySpecialDateAccess> AccVoucherEntrySpecialDateAccesses { get; set; }
        public virtual DbSet<AccVoucherEntrySpecialDateAccessHistory> AccVoucherEntrySpecialDateAccessHistories { get; set; }

        
        public virtual DbSet<AccTransactionFor> AccTransactionFors { get; set; }
        public virtual DbSet<AccVoucherType> AccVoucherTypes { get; set; }
        public virtual DbSet<AccSubledger> AccSubledgers { get; set; }

        //accounts end


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
