using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upms.Data.UPMSDataModel;
using Common.Data.Infrastructure;

namespace Upms.Data.UPMSRepository
{
    public interface ITradeTransactionChargeSlabRepository : IRepository<TradeTransactionChargeSlab>
    {

    }
    public class TradeTransactionChargeSlabRepository : RepositoryBaseCodeFirst<TradeTransactionChargeSlab,UPMSDbContext>, ITradeTransactionChargeSlabRepository
    {
        public TradeTransactionChargeSlabRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
