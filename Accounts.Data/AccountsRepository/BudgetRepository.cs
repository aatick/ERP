using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IBudgetRepository : IRepository<Budget>
    {
    }
    public class BudgetRepository : RepositoryBaseCodeFirst<Budget,AccountsDbContext>, IBudgetRepository
    {
        public BudgetRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
