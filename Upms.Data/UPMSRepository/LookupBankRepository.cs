using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface ILookupBankRepository : IRepository<LookupBank>
    {

    }
    public class LookupBankRepository : RepositoryBaseCodeFirst<LookupBank,UPMSDbContext>, ILookupBankRepository
    {
        public LookupBankRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
