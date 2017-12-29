using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorBalanceDailyRepository : IRepository<InvestorBalanceDaily>
    {

    }
    public class InvestorBalanceDailyRepository : RepositoryBaseCodeFirst<InvestorBalanceDaily,UPMSDbContext>, IInvestorBalanceDailyRepository
    {
        public InvestorBalanceDailyRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
