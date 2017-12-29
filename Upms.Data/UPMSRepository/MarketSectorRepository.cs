using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IMarketSectorRepository : IRepository<MarketSector>
    {

    }
    public class MarketSectorRepository : RepositoryBaseCodeFirst<MarketSector,UPMSDbContext>, IMarketSectorRepository
    {
        public MarketSectorRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
