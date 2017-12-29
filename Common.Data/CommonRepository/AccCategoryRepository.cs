using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IAccCategoryRepository : IRepository<AccCategory>
    {
    }
    public class AccCategoryRepository : RepositoryBaseCodeFirst<AccCategory, CommonDbContext>, IAccCategoryRepository
    {
        public AccCategoryRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
