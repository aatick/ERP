using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorWiseTransactionChargeRepository : IRepository<InvestorWiseTransactionCharge>
    {

    }
    public class InvestorWiseTransactionChargeRepository : RepositoryBaseCodeFirst<InvestorWiseTransactionCharge,UPMSDbContext>, IInvestorWiseTransactionChargeRepository
    {
        public InvestorWiseTransactionChargeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
