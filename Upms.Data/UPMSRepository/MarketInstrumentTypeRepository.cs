using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IMarketInstrumentTypeRepository : IRepository<MarketInstrumentType>
    {

    }
    public class MarketInstrumentTypeRepository : RepositoryBaseCodeFirst<MarketInstrumentType,UPMSDbContext>, IMarketInstrumentTypeRepository
    {
        public MarketInstrumentTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
