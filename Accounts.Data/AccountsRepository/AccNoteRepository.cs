using Accounts.Data.AccountsDataModel;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccNoteRepository : IRepository<AccNote>
    {
    }
    public class AccNoteRepository : RepositoryBaseCodeFirst<AccNote, AccountsDbContext>, IAccNoteRepository
    {
        public AccNoteRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
