using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IVoucherSpecialAccessRepository : IRepository<AccVoucherEntrySpecialDateAccess>
    {

    }
    public class VoucherSpecialAccessRepository : RepositoryBaseCodeFirst<AccVoucherEntrySpecialDateAccess, AccountsDbContext>, IVoucherSpecialAccessRepository
    {
        public VoucherSpecialAccessRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
