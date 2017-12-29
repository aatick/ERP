using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccSubLedgerRepository : IRepository<AccSubledger>
    {

    }
    public class AccSubLedgerRepository : RepositoryBaseCodeFirst<AccSubledger, AccountsDbContext>, IAccSubLedgerRepository
    {
        public AccSubLedgerRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
