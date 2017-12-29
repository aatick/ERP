using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ITradeTransactionChargeHistoryRepository : IRepository<TradeTransactionChargeHistory>
    {

    }
    public class TradeTransactionChargeHistoryRepository : RepositoryBaseCodeFirst<TradeTransactionChargeHistory,UPMSDbContext>, ITradeTransactionChargeHistoryRepository
    {
        public TradeTransactionChargeHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
