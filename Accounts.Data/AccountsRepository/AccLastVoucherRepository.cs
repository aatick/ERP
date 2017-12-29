using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccLastVoucherRepository : IRepository<AccLastVoucher>
    {
    }
    public class AccLastVoucherRepository : RepositoryBaseCodeFirst<AccLastVoucher, AccountsDbContext>, IAccLastVoucherRepository
    {
        public AccLastVoucherRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
