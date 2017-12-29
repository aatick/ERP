using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IAccBankBranchGLRepository : IRepository<AccBankBranchGL>
    {

    }
    public class AccBankBranchGLRepository : RepositoryBaseCodeFirst<AccBankBranchGL, CommonDbContext>, IAccBankBranchGLRepository
    {
        public AccBankBranchGLRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
