using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IMarketInfoRepository : IRepository<MarketInformation>
    {

    }
    public class MarketInfoRepository : RepositoryBaseCodeFirst<MarketInformation,UPMSDbContext>, IMarketInfoRepository
    {
        public MarketInfoRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
