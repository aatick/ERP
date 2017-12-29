using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IVoucherSpecialAccessHistoryRepository : IRepository<AccVoucherEntrySpecialDateAccessHistory>
    {

    }
    public class VoucherSpecialAccessHistoryRepository : RepositoryBaseCodeFirst<AccVoucherEntrySpecialDateAccessHistory,AccountsDbContext>, IVoucherSpecialAccessHistoryRepository
    {
        public VoucherSpecialAccessHistoryRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
