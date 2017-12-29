using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IBrokerBranchRepository : IRepository<BrokerBranch>
    {

    }
    public class BrokerBranchRepository : RepositoryBaseCodeFirst<BrokerBranch, CommonDbContext>, IBrokerBranchRepository
    {
        public BrokerBranchRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
