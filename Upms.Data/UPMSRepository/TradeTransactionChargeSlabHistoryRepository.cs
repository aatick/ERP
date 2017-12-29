using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ITradeTransactionChargeSlabHistoryRepository : IRepository<TradeTransactionChargeSlabHistory>
    {

    }
    public class TradeTransactionChargeSlabHistoryRepository : RepositoryBaseCodeFirst<TradeTransactionChargeSlabHistory,UPMSDbContext>, ITradeTransactionChargeSlabHistoryRepository
    {
        public TradeTransactionChargeSlabHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
