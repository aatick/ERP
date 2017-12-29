//using UcasPortfolio.Data.DBDetailModels;

using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IAccTransactionModeRepository : IRepository<AccTransactionMode>
    {

    }
    public class AccTransactionModeRepository : RepositoryBaseCodeFirst<AccTransactionMode, CommonDbContext>, IAccTransactionModeRepository
    {
        public AccTransactionModeRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
