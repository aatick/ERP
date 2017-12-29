using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IMarketTypeRepository : IRepository<MarketType>
    {

    }
    public class MarketTypeRepository : RepositoryBaseCodeFirst<MarketType,UPMSDbContext>, IMarketTypeRepository
    {
        public MarketTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
