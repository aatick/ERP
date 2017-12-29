using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IMarketGroupRepository : IRepository<MarketGroup>
    {

    }
    public class MarketGroupRepository : RepositoryBaseCodeFirst<MarketGroup,UPMSDbContext>, IMarketGroupRepository
    {
        public MarketGroupRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
