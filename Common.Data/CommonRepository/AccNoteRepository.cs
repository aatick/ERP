using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IAccNoteRepository : IRepository<AccNote>
    {
    }
    public class AccNoteRepository : RepositoryBaseCodeFirst<AccNote, CommonDbContext>, IAccNoteRepository
    {
        public AccNoteRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
