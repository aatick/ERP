using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IBrokerTraderRepository : IRepository<BrokerTrader>
    {

    }
    public class BrokerTraderRepository : RepositoryBaseCodeFirst<BrokerTrader,UPMSDbContext>, IBrokerTraderRepository
    {
        public BrokerTraderRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
