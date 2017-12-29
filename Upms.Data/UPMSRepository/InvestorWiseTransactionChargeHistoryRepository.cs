using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorWiseTransactionChargeHistoryRepository : IRepository<InvestorWiseTransactionChargeHistory>
    {

    }
    public class InvestorWiseTransactionChargeHistoryRepository : RepositoryBaseCodeFirst<InvestorWiseTransactionChargeHistory,UPMSDbContext>, IInvestorWiseTransactionChargeHistoryRepository
    {
        public InvestorWiseTransactionChargeHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
