using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IBrokerDPBranchRepository : IRepository<BrokerDPBranch>
    {

    }
    public class BrokerDPBranchRepository : RepositoryBaseCodeFirst<BrokerDPBranch, UPMSDbContext>, IBrokerDPBranchRepository
    {
        public BrokerDPBranchRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
