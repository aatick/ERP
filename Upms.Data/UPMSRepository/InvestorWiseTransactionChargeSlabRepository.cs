using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorWiseTransactionChargeSlabRepository : IRepository<InvestorWiseTransactionChargeSlab>
    {

    }
    public class InvestorWiseTransactionChargeSlabRepository : RepositoryBaseCodeFirst<InvestorWiseTransactionChargeSlab,UPMSDbContext>, IInvestorWiseTransactionChargeSlabRepository
    {
        public InvestorWiseTransactionChargeSlabRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
