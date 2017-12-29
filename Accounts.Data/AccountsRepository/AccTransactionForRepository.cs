using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccTransactionForRepository : IRepository<AccTransactionFor>
    {

    }
    public class AccTransactionForRepository : RepositoryBaseCodeFirst<AccTransactionFor, AccountsDbContext>, IAccTransactionForRepository
    {
        public AccTransactionForRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
