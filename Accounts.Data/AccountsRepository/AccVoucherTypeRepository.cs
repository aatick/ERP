using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccVoucherTypeRepository : IRepository<AccVoucherType>
    {

    }
    public class AccVoucherTypeRepository : RepositoryBaseCodeFirst<AccVoucherType, AccountsDbContext>, IAccVoucherTypeRepository
    {
        public AccVoucherTypeRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
