using Accounts.Data.AccountsDataModel;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccBankBranchGLRepository : IRepository<AccBankBranchGL>
    {

    }
    public class AccBankBranchGLRepository : RepositoryBaseCodeFirst<AccBankBranchGL, AccountsDbContext>, IAccBankBranchGLRepository
    {
        public AccBankBranchGLRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
