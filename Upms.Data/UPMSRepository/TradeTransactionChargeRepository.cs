using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ITradeTransactionChargeRepository : IRepository<TradeTransactionCharge>
    {

    }
    public class TradeTransactionChargeRepository : RepositoryBaseCodeFirst<TradeTransactionCharge,UPMSDbContext>, ITradeTransactionChargeRepository
    {
        public TradeTransactionChargeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
