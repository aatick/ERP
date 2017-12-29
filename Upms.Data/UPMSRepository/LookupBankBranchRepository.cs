using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ILookupBankBranchRepository : IRepository<LookupBankBranch>
    {

    }
    public class LookupBankBranchRepository : RepositoryBaseCodeFirst<LookupBankBranch,UPMSDbContext>, ILookupBankBranchRepository
    {
        public LookupBankBranchRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
