using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorWiseTransactionChargeSlabHistoryRepository : IRepository<InvestorWiseTransactionChargeSlabHistory>
    {

    }
    public class InvestorWiseTransactionChargeSlabHistoryRepository : RepositoryBaseCodeFirst<InvestorWiseTransactionChargeSlabHistory,UPMSDbContext>, IInvestorWiseTransactionChargeSlabHistoryRepository
    {
        public InvestorWiseTransactionChargeSlabHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
