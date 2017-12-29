using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ITradeTransactionTypeRepository : IRepository<TradeTransactionType>
    {

    }
    public class TradeTransactionTypeRepository : RepositoryBaseCodeFirst<TradeTransactionType,UPMSDbContext>, ITradeTransactionTypeRepository
    {
        public TradeTransactionTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
